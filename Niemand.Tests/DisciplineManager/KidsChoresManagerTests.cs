using FluentAssertions;
using Microsoft.Extensions.Logging;
using NetDaemon.AppModel;
using NetDaemon.Extensions.MqttEntityManager;
using NetDaemon.HassModel.Entities;
using Niemand.Tests.Mocks;
using Stateless.Graph;
using System.Collections.Generic;
using System.Text.Json;
using Xunit;

namespace Niemand.Tests.DisciplineManager;

/// <summary>
/// BDD-style test suite for the KidsChoresManager chore status handling.
/// Tests verify that oscillating state changes are handled correctly by both
/// HandleBatchedChoreStatus and SubscribeToSensorType methods.
/// </summary>
public class KidsChoresManagerTests : IDisposable
{
    private readonly KidsChoresManagerSut _sut;
    private readonly StateChangeManager _state;
    private readonly TestEntityBuilder _entityBuilder;
    private readonly AlexaMock _alexaMock;

    // Mock entities
    private Entity? _choreStatusSensor;
    private InputBooleanEntity? _announcements;

    public KidsChoresManagerTests(
        IHaContext ha,
        IServices services,
        TestScheduler scheduler,
        StateChangeManager state,
        IMqttEntityManager entityManager,
        AlexaMock alexaMock,
        ILogger<KidsChoresManager> logger,
        TestEntityBuilder entityBuilder)
    {
        _state = state;
        _entityBuilder = entityBuilder;
        _alexaMock = alexaMock;

        // Setup mock entities before SUT initialization
        SetupMockEntities();

        _sut = new KidsChoresManagerSut(ha, services, scheduler, state, entityManager, alexaMock, logger, entityBuilder);
        _sut.Init();
    }

    private void SetupMockEntities()
    {
        // Create the chore status sensor
        _choreStatusSensor = _entityBuilder.CreateEntity<Entity>("sensor.jayden_choreops_chore_status", "pending");

        // Create announcements input_boolean
        _announcements = _entityBuilder.CreateInputBooleanEntity("input_boolean.announcements", "on");

        // Initialize states
        _state.Change(_announcements, "on");
        _state.Change(_choreStatusSensor, "pending");
    }

    public void Dispose()
    {
        _sut.Cleanup();
    }

    // ============================================================================
    // OSCILLATING STATE CHANGE TESTS
    // ============================================================================

    /// <summary>
    /// Given chore status sensors exist and announcements are enabled
    /// When state changes occur (oscillating between states)
    /// Then subscription processes changes with correct throttling and batching
    /// </summary>
    [Fact]
    public void GivenChoreStatusOscillates_WhenStateChangesMultipleTimes_ThenSubscriptionProcessesCorrectly()
    {
        // Arrange
        var choreStatusSensor = _entityBuilder.CreateEntity<Entity>("sensor.jayden_choreops_chore_status", "pending");
        _alexaMock.AddMockMediaPlayer("media_player.kitchen", "downstairs");
        _state.Change(choreStatusSensor, "pending");

        // Act - Simulate oscillating state changes
        _state.Change(choreStatusSensor, new EntityState
        {
            EntityId = choreStatusSensor.EntityId,
            State = "overdue",
            AttributesJson = StateChangeManager.ToAttributeJson(
                new
                {
                    user_name = "Jayden",
                    chore_name = "Homework"
                })
        });
        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);

        _state.Change(choreStatusSensor, new EntityState
        {
            EntityId = choreStatusSensor.EntityId,
            State = "due",
            AttributesJson = StateChangeManager.ToAttributeJson(
                new
                {
                    user_name = "Jayden",
                    chore_name = "Homework"
                })
        });
        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);

        _state.Change(choreStatusSensor, new EntityState
        {
            EntityId = choreStatusSensor.EntityId,
            State = "overdue",
            AttributesJson = StateChangeManager.ToAttributeJson(
                new
                {
                    user_name = "Jayden",
                    chore_name = "Homework"
                })
        });

        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(10).Ticks);


        // Assert - Verify the final state is processed with announcement
        _alexaMock.AnnounceConfigCalls.Count.Should().Be(1);
        _alexaMock.AnnounceConfigCalls.First().Message.Should().Be(
            "<amazon:emotion name='disappointed' intensity='high'>Jayden, your Homework is overdue and you are losing points</amazon:emotion>");
    }

    /// <summary>
    /// Given state oscillates between "Due" and "Overdue"
    /// When final state settles
    /// Then subscription correctly identifies final state for announcement logic
    /// </summary>
    [Fact]
    public void GivenChoreStatusOscillatesToDue_WhenStateSettles_ThenSubscriptionIdentifiesFinalState()
    {
        // Arrange
        var choreStatusSensor = _entityBuilder.CreateEntity<Entity>("sensor.aaron_choreops_chore_status", "pending");
        _alexaMock.AddMockMediaPlayer("media_player.kitchen", "downstairs");
        _state.Change(choreStatusSensor, "pending");

        // Act - Simulate oscillating state changes ending in "due"
        _state.Change(choreStatusSensor, new EntityState
        {
            EntityId = choreStatusSensor.EntityId,
            State = "due",
            AttributesJson = StateChangeManager.ToAttributeJson(
                new
                {
                    user_name = "Aaron",
                    chore_name = "Dishes"
                })
        });
        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);

        _state.Change(choreStatusSensor, new EntityState
        {
            EntityId = choreStatusSensor.EntityId,
            State = "overdue",
            AttributesJson = StateChangeManager.ToAttributeJson(
                new
                {
                    user_name = "Aaron",
                    chore_name = "Dishes"
                })
        });
        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);

        _state.Change(choreStatusSensor, new EntityState
        {
            EntityId = choreStatusSensor.EntityId,
            State = "due",
            AttributesJson = StateChangeManager.ToAttributeJson(
                new
                {
                    user_name = "Aaron",
                    chore_name = "Dishes"
                })
        });
        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);

        // Advance scheduler to process the batched subscription
        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(40).Ticks);

        // Assert - Verify final state is processed with announcement
        _alexaMock.AnnounceConfigCalls.Should().BeEmpty();
    }

    /// <summary>
    /// Given multiple children have oscillating chore states
    /// When states are processed together
    /// Then subscription batches them correctly
    /// </summary>
    [Fact]
    public void GivenMultipleChildrenWithOscillatingStates_WhenProcessedTogether_ThenSubscriptionBatchesCorrectly()
    {
        // Arrange
        var jaydenSensor = _entityBuilder.CreateEntity<Entity>("sensor.jayden_choreops_chore_status", "pending");
        var gabrielSensor = _entityBuilder.CreateEntity<Entity>("sensor.gabriel_choreops_chore_status", "pending");

        _alexaMock.AddMockMediaPlayer("media_player.kitchen", "downstairs");

        _state.Change(jaydenSensor, "pending");
        _state.Change(gabrielSensor, "pending");

        // Act - Both change to overdue

        _state.Change(jaydenSensor, new EntityState
        {
            EntityId = jaydenSensor.EntityId,
            State = "overdue",
            AttributesJson = StateChangeManager.ToAttributeJson(
                new
                {
                    user_name = "Jayden",
                    chore_name = "Shower"
                })
        });

        _state.Change(gabrielSensor, new EntityState
        {
            EntityId = gabrielSensor.EntityId,
            State = "overdue",
            AttributesJson = StateChangeManager.ToAttributeJson(
                new
                {
                    user_name = "Gabriel",
                    chore_name = "Shower"
                })
        });

        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);

        // Advance for throttling and batching
        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(40).Ticks);

        // Assert - Verify batching logic completed
        _alexaMock.AnnounceConfigCalls.First().Message.Should().Be(
            "<amazon:emotion name='disappointed' intensity='high'>Gabriel and Jayden, your Shower is overdue and you are losing points</amazon:emotion>");
    }

    /// <summary>
    /// Given multiple children have oscillating chore states
    /// When states are processed together
    /// Then subscription batches them correctly
    /// </summary>
    [Fact]
    public void GivenMultipleChildrenWithDifferentChores_WhenProcessedTogether_ThenSubscriptionBatchesCorrectly()
    {
        // Arrange
        var jaydenSensor = _entityBuilder.CreateEntity<Entity>("sensor.jayden_choreops_chore_status", "pending");
        var jaydenSensor2 = _entityBuilder.CreateEntity<Entity>("sensor.jayden_choreops_chore2_status", "pending");
        var aaronSensor = _entityBuilder.CreateEntity<Entity>("sensor.aaron_choreops_chore_status", "pending");
        var gabrielSensor = _entityBuilder.CreateEntity<Entity>("sensor.gabriel_choreops_chore_status", "pending");

        _alexaMock.AddMockMediaPlayer("media_player.kitchen", "downstairs");

        _state.Change(jaydenSensor, "pending");
        _state.Change(jaydenSensor2, "pending");
        _state.Change(aaronSensor, "pending");
        _state.Change(gabrielSensor, "pending");

        // Act - Both change to overdue

        _state.Change(jaydenSensor, new EntityState
        {
            EntityId = jaydenSensor.EntityId,
            State = "overdue",
            AttributesJson = StateChangeManager.ToAttributeJson(
                new
                {
                    user_name = "Jayden",
                    chore_name = "Shower"
                })
        });

        _state.Change(jaydenSensor2, new EntityState
        {
            EntityId = jaydenSensor2.EntityId,
            State = "overdue",
            AttributesJson = StateChangeManager.ToAttributeJson(
                new
                {
                    user_name = "Jayden",
                    chore_name = "Getting Ready"
                })
        });

        _state.Change(aaronSensor, new EntityState
        {
            EntityId = aaronSensor.EntityId,
            State = "overdue",
            AttributesJson = StateChangeManager.ToAttributeJson(
                new
                {
                    user_name = "Aaron",
                    chore_name = "Getting Ready"
                })
        });

        _state.Change(gabrielSensor, new EntityState
        {
            EntityId = gabrielSensor.EntityId,
            State = "overdue",
            AttributesJson = StateChangeManager.ToAttributeJson(
                new
                {
                    user_name = "Gabriel",
                    chore_name = "Dishwasher"
                })
        });

        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);

        // Advance for throttling and batching
        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(40).Ticks);

        // Assert - Verify batching logic completed
        _alexaMock.AnnounceConfigCalls.First().Message.Should().Be(
            "<amazon:emotion name='disappointed' intensity='high'>Aaron and Gabriel and Jayden, your Dishwasher and Getting Ready and Shower is overdue and you are losing points</amazon:emotion>");    }

    /// <summary>
    /// Given announcements are disabled
    /// When chore status changes
    /// Then no announcement is made
    /// </summary>
    [Fact]
    public void GivenAnnouncementsAreDisabled_WhenChoreStatusChanges_ThenNoAnnouncementIsMade()
    {
        // Arrange
        SetupChoreStatusSensorAttributes("jayden", "Homework");
        _state.Change(_announcements!, "off");

        // Act
        _state.Change(_choreStatusSensor!, "overdue");
        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);
        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(2.5).Ticks);

        // Assert
        _alexaMock.AnnounceConfigCalls.Should().BeEmpty(
            "No announcement should be made when announcements are disabled");
    }

    /// <summary>
    /// Given a chore status sensor with missing attributes
    /// When state changes occur
    /// Then the method handles the error gracefully without throwing
    /// </summary>
    [Fact]
    public void GivenChoreStatusSensorMissingAttributes_WhenStateChanges_ThenHandlesGracefullyWithoutThrowing()
    {
        // Arrange
        var sensorWithoutAttributes = _entityBuilder.CreateEntity<Entity>("sensor.unknown_choreops_chore_status", "pending");
        _state.Change(sensorWithoutAttributes, "pending");
        // Intentionally don't set attributes

        // Act & Assert - Should not throw
        var ex = Record.Exception(() =>
        {
            _state.Change(sensorWithoutAttributes, "overdue");
            _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);
            _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(2.5).Ticks);
        });

        ex.Should().BeNull(
            "Method should handle missing attributes gracefully without throwing");
    }

    private void SetupChoreStatusSensorAttributes(string kidName, string choreName, Entity? entity = null)
    {
        var targetEntity = entity ?? _choreStatusSensor;
        if (targetEntity == null)
            throw new InvalidOperationException("Chore status sensor not initialized");

        // Note: The test framework's StateChangeManager should handle attributes through the Entity
        // We simply ensure the entity exists with a state change to trigger the subscription
        _state.Change(targetEntity, "pending");
    }
}
