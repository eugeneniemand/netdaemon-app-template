using System.Reactive.Subjects;
using System.Reactive;
using Microsoft.Extensions.Logging;
using NetDaemon.Helpers;
using Niemand.Tests.Mocks;

namespace Niemand.Tests.AlexaPromptPoller;

public class AlexaPromptPollerFacts(TestScheduler scheduler, AlexaMock alexa, ILogger<Helpers.AlexaPromptPoller> logger)
{
    [Fact]
    public void PromptIsSentWhenTriggerFires()
    {
        // Arrange
        var trigger = new Subject<Unit>();
        var responses = new Subject<PromptResponse>();
        var poller = new Helpers.AlexaPromptPoller(scheduler, alexa, logger)
            .AddTrigger(trigger)
            .SetMediaPlayer("media_player.dining")
            .SetPrompt("Should I turn on the lights?", "test_event_1");

        // Act
        poller.Subscribe();
        trigger.OnNext(Unit.Default);

        // Assert
        // Verify Alexa.Prompt was called with correct parameters
        alexa.PromptCallCount.Should().Be(1, "One prompt should be sent");
        alexa.PromptHistory.Should().Contain(
            ("media_player.dining", "Should I turn on the lights?", "test_event_1")
        );

        trigger.OnCompleted();
    }

    [Fact]
    public void TriggerIsIgnoredDuringCooldown()
    {
        // Arrange
        var trigger = new Subject<Unit>();
        var responses = new Subject<PromptResponse>();
        var poller = new Helpers.AlexaPromptPoller(scheduler, alexa, logger)
            .AddTrigger(trigger)
            .SetMediaPlayer("media_player.dining")
            .SetPrompt("Should I turn on the lights?", "test_event_1")
            .WithCooldown(TimeSpan.FromSeconds(60));

        var subscription = poller.Subscribe();

        // Act - First trigger should go through
        scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);
        trigger.OnNext(Unit.Default);
        var state1 = poller.GetState();
        var promptCountAfterFirst = alexa.PromptCallCount;

        // Act - Second trigger within cooldown should be ignored
        scheduler.AdvanceBy(TimeSpan.FromSeconds(30).Ticks);
        trigger.OnNext(Unit.Default);
        var state2 = poller.GetState();
        var promptCountAfterSecond = alexa.PromptCallCount;

        // Assert
        state1.IsWaitingForResponse.Should().BeTrue();
        promptCountAfterFirst.Should().Be(1, "First trigger should send a prompt");
        
        state2.IsWaitingForResponse.Should().BeTrue(); // Still waiting, not a new prompt
        state2.CooldownRemaining.Should().BeGreaterThan(TimeSpan.Zero);
        promptCountAfterSecond.Should().Be(1, "Second trigger within cooldown should not send a prompt");

        subscription.Dispose();
        trigger.OnCompleted();
    }

    [Fact]
    public void TriggerIsAcceptedAfterCooldownExpires()
    {
        // Arrange
        var trigger = new Subject<Unit>();
        var responses = new Subject<PromptResponse>();
        var cooldownMs = 60;
        var poller = new Helpers.AlexaPromptPoller(scheduler, alexa, logger)
            .AddTrigger(trigger)
            .SetMediaPlayer("media_player.dining")
            .SetPrompt("Should I turn on the lights?", "test_event_1")
            .WithCooldown(TimeSpan.FromSeconds(cooldownMs));

        var subscription = poller.Subscribe();

        // Act - First trigger
        trigger.OnNext(Unit.Default);
        var initialTime = scheduler.Now;
        var promptCountAfterFirst = alexa.PromptCallCount;

        // Advance past cooldown
        scheduler.AdvanceBy(TimeSpan.FromSeconds(cooldownMs + 1).Ticks);

        // Second trigger should be accepted
        trigger.OnNext(Unit.Default);
        var state = poller.GetState();
        var promptCountAfterSecond = alexa.PromptCallCount;

        // Assert - Last prompt time should have advanced
        state.LastPromptTime.Should().Be(scheduler.Now);
        promptCountAfterFirst.Should().Be(1, "First trigger should send a prompt");
        promptCountAfterSecond.Should().Be(2, "Second trigger after cooldown should send a prompt");

        subscription.Dispose();
        trigger.OnCompleted();
    }

    [Fact]
    public void ResponseHandlerIsCalledForYesResponse()
    {
        // Arrange
        var trigger = new Subject<Unit>();
        var handlerCalled = false;
        PromptResponse? capturedResponse = null;

        var poller = new Helpers.AlexaPromptPoller(scheduler, alexa, logger)
            .AddTrigger(trigger)
            .SetMediaPlayer("media_player.dining")
            .SetPrompt("Should I turn on the lights?", "test_event_1")
            .OnResponseYes(response =>
            {
                handlerCalled = true;
                capturedResponse = response;
            });

        var subscription = poller.Subscribe();

        // Act
        trigger.OnNext(Unit.Default);
        alexa.QueueResponse(new PromptResponse
        {
            EventId = "test_event_1",
            ResponseType = PromptResponseType.ResponseYes,
            ResponsePersonName = "Eugene"
        });

        // Assert
        handlerCalled.Should().BeTrue();
        capturedResponse?.ResponseType.Should().Be(PromptResponseType.ResponseYes);
        capturedResponse?.ResponsePersonName.Should().Be("Eugene");

        subscription.Dispose();
        trigger.OnCompleted();
    }

    [Fact]
    public void ResponseHandlerIsCalledForYesResponseForPromptConfig()
    {
        // Arrange
        var trigger = new Subject<Unit>();
        var handlerCalled = false;
        PromptResponse? capturedResponse = null;

        var poller = new Helpers.AlexaPromptPoller(scheduler, alexa, logger)
            .AddTrigger(trigger)            
            .SetPrompt(new Alexa.Config() { Message = "Should I turn on the lights?", Entity = "media_player.dining", EventId = "test_event_1" })
            .OnResponseYes(response =>
            {
                handlerCalled = true;
                capturedResponse = response;
            });

        var subscription = poller.Subscribe();

        // Act
        trigger.OnNext(Unit.Default);
        alexa.QueueResponse(new PromptResponse
        {
            EventId = "test_event_1",
            ResponseType = PromptResponseType.ResponseYes,
            ResponsePersonName = "Eugene"
        });

        // Assert
        handlerCalled.Should().BeTrue();
        capturedResponse?.ResponseType.Should().Be(PromptResponseType.ResponseYes);
        capturedResponse?.ResponsePersonName.Should().Be("Eugene");

        subscription.Dispose();
        trigger.OnCompleted();
    }

    [Fact]
    public void ResponseHandlerIsCalledForNoResponse()
    {
        // Arrange
        var trigger = new Subject<Unit>();
        var handlerCalled = false;

        var poller = new Helpers.AlexaPromptPoller(scheduler, alexa, logger)
            .AddTrigger(trigger)
            .SetMediaPlayer("media_player.dining")
            .SetPrompt("Should I turn on the lights?", "test_event_1")
            .OnResponseNo(_ => handlerCalled = true);

        var subscription = poller.Subscribe();

        // Act
        trigger.OnNext(Unit.Default);
        alexa.QueueResponse(new PromptResponse
        {
            EventId = "test_event_1",
            ResponseType = PromptResponseType.ResponseNo,
            ResponsePersonName = "Eugene"
        });

        // Assert
        handlerCalled.Should().BeTrue();

        subscription.Dispose();
        trigger.OnCompleted();
    }

    [Fact]
    public void DefaultHandlerIsCalledWhenNoSpecificHandlerExists()
    {
        // Arrange
        var trigger = new Subject<Unit>();
        var handlerCalled = false;

        var poller = new Helpers.AlexaPromptPoller(scheduler, alexa, logger)
            .AddTrigger(trigger)
            .SetMediaPlayer("media_player.dining")
            .SetPrompt("Should I turn on the lights?", "test_event_1")
            .OnResponseDefault(_ => handlerCalled = true);

        var subscription = poller.Subscribe();

        // Act
        trigger.OnNext(Unit.Default);
        alexa.QueueResponse(new PromptResponse
        {
            EventId = "test_event_1",
            ResponseType = PromptResponseType.ResponseSelect,
            ResponsePersonName = "Eugene"
        });

        // Assert
        handlerCalled.Should().BeTrue();

        subscription.Dispose();
        trigger.OnCompleted();
    }

    [Fact]
    public void SpecificHandlerTakesPrecedenceOverDefaultHandler()
    {
        // Arrange
        var trigger = new Subject<Unit>();
        var specificHandlerCalled = false;
        var defaultHandlerCalled = false;

        var poller = new Helpers.AlexaPromptPoller(scheduler, alexa, logger)
            .AddTrigger(trigger)
            .SetMediaPlayer("media_player.dining")
            .SetPrompt("Should I turn on the lights?", "test_event_1")
            .OnResponseYes(_ => specificHandlerCalled = true)
            .OnResponseDefault(_ => defaultHandlerCalled = true);

        var subscription = poller.Subscribe();

        // Act
        trigger.OnNext(Unit.Default);
        alexa.QueueResponse(new PromptResponse
        {
            EventId = "test_event_1",
            ResponseType = PromptResponseType.ResponseYes,
            ResponsePersonName = "Eugene"
        });

        // Assert
        specificHandlerCalled.Should().BeTrue();
        defaultHandlerCalled.Should().BeFalse();

        subscription.Dispose();
        trigger.OnCompleted();
    }

    [Fact]
    public void MultipleTriggersAreMergedAndAccepted()
    {
        // Arrange
        var trigger1 = new Subject<Unit>();
        var trigger2 = new Subject<Unit>();
        var responses = new Subject<PromptResponse>();
        var promptsSent = 0;

        var poller = new Helpers.AlexaPromptPoller(scheduler, alexa, logger)
            .AddTrigger(trigger1)
            .AddTrigger(trigger2)
            .SetMediaPlayer("media_player.dining")
            .SetPrompt("Should I turn on the lights?", "test_event_1")
            .WithCooldown(TimeSpan.FromSeconds(0));

        var subscription = poller.Subscribe();

        // Act - Fire first trigger
        trigger1.OnNext(Unit.Default);
        promptsSent++;

        // Advance past cooldown
        scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);

        // Fire second trigger
        trigger2.OnNext(Unit.Default);
        promptsSent++;

        // Assert
        promptsSent.Should().Be(2);

        subscription.Dispose();
        trigger1.OnCompleted();
        trigger2.OnCompleted();
        responses.OnCompleted();
    }

    [Fact]
    public void GenericTriggerIsConvertedToUnit()
    {
        // Arrange
        var trigger = new Subject<long>();
        var responses = new Subject<PromptResponse>();

        var poller = new Helpers.AlexaPromptPoller(scheduler, alexa, logger)
            .AddTrigger(trigger)
            .SetMediaPlayer("media_player.dining")
            .SetPrompt("Should I turn on the lights?", "test_event_1");

        var subscription = poller.Subscribe();

        // Act
        trigger.OnNext(1);
        trigger.OnNext(2);
        trigger.OnNext(3);

        // Assert - Poller should be waiting for response after first trigger
        var state = poller.GetState();
        state.IsWaitingForResponse.Should().BeTrue();

        subscription.Dispose();
        trigger.OnCompleted();
        responses.OnCompleted();
    }

    [Fact]
    public void ConditionalTriggerOnlyFiresWhenPredicateIsTrue()
    {
        // Arrange
        var trigger = new Subject<bool>();
        var responses = new Subject<PromptResponse>();

        var poller = new Helpers.AlexaPromptPoller(scheduler, alexa, logger)
            .AddTrigger(trigger, value => value == true)
            .SetMediaPlayer("media_player.dining")
            .SetPrompt("Should I turn on the lights?", "test_event_1");

        var subscription = poller.Subscribe();

        // Act - Fire with false (should not trigger)
        trigger.OnNext(false);
        var state1 = poller.GetState();

        // Advance past cooldown
        scheduler.AdvanceBy(TimeSpan.FromMinutes(1).Ticks);

        // Fire with true (should trigger)
        trigger.OnNext(true);
        var state2 = poller.GetState();

        // Assert
        state1.IsWaitingForResponse.Should().BeFalse();
        state2.IsWaitingForResponse.Should().BeTrue();

        subscription.Dispose();
        trigger.OnCompleted();
        responses.OnCompleted();
    }   

    [Fact]
    public void DailyResetTriggersResetOfState()
    {
        // Arrange
        var mainTrigger = new Subject<Unit>();
        var dailyReset = new Subject<Unit>();
        var responses = new Subject<PromptResponse>();

        var poller = new Helpers.AlexaPromptPoller(scheduler, alexa, logger)
            .AddTrigger(mainTrigger)
            .SetMediaPlayer("media_player.dining")
            .SetPrompt("Should I turn on the lights?", "test_event_1")
            .WithCooldown(TimeSpan.FromSeconds(60))
            .WithDailyReset(dailyReset);

        var subscription = poller.Subscribe();

        // Act - First trigger
        mainTrigger.OnNext(Unit.Default);
        var stateAfterFirstTrigger = poller.GetState();

        // Trigger daily reset
        dailyReset.OnNext(Unit.Default);
        var stateAfterReset = poller.GetState();

        // Assert
        stateAfterFirstTrigger.IsWaitingForResponse.Should().BeTrue();
        stateAfterReset.LastPromptTime.Should().Be(DateTimeOffset.MinValue);
        stateAfterReset.IsWaitingForResponse.Should().BeFalse();

        subscription.Dispose();
        mainTrigger.OnCompleted();
        dailyReset.OnCompleted();
        responses.OnCompleted();
    }

    [Fact]
    public void ExternalResetTriggersResetOfState()
    {
        // Arrange
        var trigger = new Subject<Unit>();
        var responses = new Subject<PromptResponse>();

        var poller = new Helpers.AlexaPromptPoller(scheduler, alexa, logger)
            .AddTrigger(trigger)
            .SetMediaPlayer("media_player.dining")
            .SetPrompt("Should I turn on the lights?", "test_event_1");

        var subscription = poller.Subscribe();

        // Act - First trigger
        trigger.OnNext(Unit.Default);
        var stateBeforeReset = poller.GetState();

        // Externally reset
        poller.ResetState();
        var stateAfterReset = poller.GetState();

        // Assert
        stateBeforeReset.IsWaitingForResponse.Should().BeTrue();
        stateAfterReset.LastPromptTime.Should().Be(DateTimeOffset.MinValue);
        stateAfterReset.IsWaitingForResponse.Should().BeFalse();
        stateAfterReset.IsAcknowledged.Should().BeFalse();

        subscription.Dispose();
        trigger.OnCompleted();
        responses.OnCompleted();
    }

    [Fact]
    public void ExternalResetObservableTriggersResetOfState()
    {
        // Arrange
        var trigger = new Subject<Unit>();
        var externalResetTrigger = new Subject<Unit>();
        var responses = new Subject<PromptResponse>();

        var poller = new Helpers.AlexaPromptPoller(scheduler, alexa, logger)
            .AddTrigger(trigger)
            .AddExternalResetTrigger<Unit>(externalResetTrigger)
            .SetMediaPlayer("media_player.dining")
            .SetPrompt("Should I turn on the lights?", "test_event_1");

        var subscription = poller.Subscribe();

        // Act - First trigger
        trigger.OnNext(Unit.Default);
        var stateBeforeReset = poller.GetState();

        // Externally reset
        
        externalResetTrigger.OnNext(Unit.Default);
        var stateAfterReset = poller.GetState();

        // Assert
        stateBeforeReset.IsWaitingForResponse.Should().BeTrue();
        stateAfterReset.LastPromptTime.Should().Be(DateTimeOffset.MinValue);
        stateAfterReset.IsWaitingForResponse.Should().BeFalse();
        stateAfterReset.IsAcknowledged.Should().BeFalse();

        subscription.Dispose();
        trigger.OnCompleted();
        responses.OnCompleted();
    }

    [Fact]
    public void AcknowledgementStopsAllFurtherPrompts()
    {
        // Arrange
        var trigger = new Subject<Unit>();
        var responses = new Subject<PromptResponse>();

        var poller = new Helpers.AlexaPromptPoller(scheduler, alexa, logger)
            .AddTrigger(trigger)
            .SetMediaPlayer("media_player.dining")
            .SetPrompt("Should I turn on the lights?", "test_event_1")
            .WithCooldown(TimeSpan.FromSeconds(0));

        var subscription = poller.Subscribe();

        // Act - First trigger should work
        trigger.OnNext(Unit.Default);
        var stateBeforeAcknowledge = poller.GetState();

        // Acknowledge
        poller.Acknowledge();
        var stateAfterAcknowledge = poller.GetState();

        // Try to trigger again (should be ignored)
        scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);
        trigger.OnNext(Unit.Default);
        var stateAfterSecondTrigger = poller.GetState();

        // Assert
        stateBeforeAcknowledge.IsWaitingForResponse.Should().BeTrue();
        stateAfterAcknowledge.IsAcknowledged.Should().BeTrue();
        stateAfterSecondTrigger.IsAcknowledged.Should().BeTrue();

        subscription.Dispose();
        trigger.OnCompleted();
        responses.OnCompleted();
    }

    [Fact]
    public void OnResponseNotYesHandlesAllButYes()
    {
        // Arrange
        var trigger = new Subject<Unit>();
        var handledResponses = new List<PromptResponseType>();

        var poller = new Helpers.AlexaPromptPoller(scheduler, alexa, logger)
            .AddTrigger(trigger)
            .SetMediaPlayer("media_player.dining")
            .SetPrompt("Should I turn on the lights?", "test_event_1")
            .OnResponseNotYes(response => handledResponses.Add(response.ResponseType));

        var subscription = poller.Subscribe();

        // Act
        trigger.OnNext(Unit.Default);
        alexa.QueueResponse(new PromptResponse
        {
            EventId = "test_event_1",
            ResponseType = PromptResponseType.ResponseNo,
            ResponsePersonName = "Eugene"
        });

        // Trigger again for different response
        scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);
        trigger.OnNext(Unit.Default);
        alexa.QueueResponse(new PromptResponse
        {
            EventId = "test_event_1",
            ResponseType = PromptResponseType.ResponseNone,
            ResponsePersonName = "Eugene"
        });

        // Assert
        handledResponses.Should().Contain(PromptResponseType.ResponseNo);
        handledResponses.Should().Contain(PromptResponseType.ResponseNone);

        subscription.Dispose();
        trigger.OnCompleted();

    }

    [Fact]
    public void OnResponseNotNoHandlesAllButNo()
    {
        // Arrange
        var trigger = new Subject<Unit>();
        var responses = new Subject<PromptResponse>();
        var handledResponses = new List<PromptResponseType>();

        var poller = new Helpers.AlexaPromptPoller(scheduler, alexa, logger)
            .AddTrigger(trigger)
            .SetMediaPlayer("media_player.dining")
            .SetPrompt("Should I turn on the lights?", "test_event_1")
            .OnResponseNotNo(response => handledResponses.Add(response.ResponseType));

        var subscription = poller.Subscribe();

        // Act
        trigger.OnNext(Unit.Default);
        alexa.QueueResponse(new PromptResponse
        {
            EventId = "test_event_1",
            ResponseType = PromptResponseType.ResponseYes,
            ResponsePersonName = "Eugene"
        });

        // Trigger again for different response
        scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);
        trigger.OnNext(Unit.Default);
        alexa.QueueResponse(new PromptResponse
        {
            EventId = "test_event_1",
            ResponseType = PromptResponseType.ResponseNone,
            ResponsePersonName = "Eugene"
        });

        // Assert
        handledResponses.Should().Contain(PromptResponseType.ResponseYes);
        handledResponses.Should().Contain(PromptResponseType.ResponseNone);

        subscription.Dispose();
        trigger.OnCompleted();
        responses.OnCompleted();
    }

    [Fact]
    public void OnResponseNotExcludedTypeHandlesAllButSpecified()
    {
        // Arrange
        var trigger = new Subject<Unit>();
        var handledResponses = new List<PromptResponseType>();

        var poller = new Helpers.AlexaPromptPoller(scheduler, alexa, logger)
            .AddTrigger(trigger)
            .SetMediaPlayer("media_player.dining")
            .SetPrompt("Should I turn on the lights?", "test_event_1")
            .OnResponseNot(PromptResponseType.ResponseYes, response => handledResponses.Add(response.ResponseType));

        var subscription = poller.Subscribe();

        // Act - Fire ResponseYes (should not be handled)
        trigger.OnNext(Unit.Default);
        alexa.QueueResponse(new PromptResponse
        {
            EventId = "test_event_1",
            ResponseType = PromptResponseType.ResponseYes,
            ResponsePersonName = "Eugene"
        });

        // Trigger again for different response (should be handled)
        scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);
        trigger.OnNext(Unit.Default);
        alexa.QueueResponse(new PromptResponse
        {
            EventId = "test_event_1",
            ResponseType = PromptResponseType.ResponseNo,
            ResponsePersonName = "Eugene"
        });

        // Assert
        handledResponses.Should().NotContain(PromptResponseType.ResponseYes);
        handledResponses.Should().Contain(PromptResponseType.ResponseNo);

        subscription.Dispose();
        trigger.OnCompleted();
    }

    [Fact]
    public void ResponseForDifferentEventIdIsIgnored()
    {
        // Arrange
        var trigger = new Subject<Unit>();
        var handlerCalled = false;

        var poller = new Helpers.AlexaPromptPoller(scheduler, alexa, logger)
            .AddTrigger(trigger)
            .SetMediaPlayer("media_player.dining")
            .SetPrompt("Should I turn on the lights?", "test_event_1")
            .OnResponseYes(_ => handlerCalled = true);

        var subscription = poller.Subscribe();

        // Act
        trigger.OnNext(Unit.Default);
        alexa.QueueResponse(new PromptResponse
        {
            EventId = "different_event_id",
            ResponseType = PromptResponseType.ResponseYes,
            ResponsePersonName = "Eugene"
        });

        // Assert
        handlerCalled.Should().BeFalse();

        subscription.Dispose();
        trigger.OnCompleted();
    }

    [Fact]
    public void CooldownRemainingIsCalculatedCorrectly()
    {
        // Arrange
        var trigger = new Subject<Unit>();
        var responses = new Subject<PromptResponse>();
        var cooldownSeconds = 60;

        var poller = new Helpers.AlexaPromptPoller(scheduler, alexa, logger)
            .AddTrigger(trigger)
            .SetMediaPlayer("media_player.dining")
            .SetPrompt("Should I turn on the lights?", "test_event_1")
            .WithCooldown(TimeSpan.FromSeconds(cooldownSeconds));

        var subscription = poller.Subscribe();

        // Act
        trigger.OnNext(Unit.Default);
        var stateAtStart = poller.GetState();

        scheduler.AdvanceBy(TimeSpan.FromSeconds(20).Ticks);
        var stateAt20Seconds = poller.GetState();

        scheduler.AdvanceBy(TimeSpan.FromSeconds(30).Ticks);
        var stateAt50Seconds = poller.GetState();

        // Assert
        stateAtStart.CooldownRemaining.Should().BeLessThanOrEqualTo(TimeSpan.FromSeconds(cooldownSeconds));
        stateAt20Seconds.CooldownRemaining.Should().BeLessThanOrEqualTo(TimeSpan.FromSeconds(40));
        stateAt50Seconds.CooldownRemaining.Should().BeLessThanOrEqualTo(TimeSpan.FromSeconds(10));

        subscription.Dispose();
        trigger.OnCompleted();
        responses.OnCompleted();
    }

    [Fact]
    public void ValidationFailsWhenMediaPlayerNotSet()
    {
        // Arrange
        var trigger = new Subject<Unit>();
        var responses = new Subject<PromptResponse>();

        var poller = new Helpers.AlexaPromptPoller(scheduler, alexa, logger)
            .AddTrigger(trigger)
            .SetPrompt("Should I turn on the lights?", "test_event_1");

        // Act & Assert
        var action = () => poller.Subscribe();
        action.Should().Throw<InvalidOperationException>()
            .WithMessage("*MediaPlayer*");

        trigger.OnCompleted();
        responses.OnCompleted();
    }

    [Fact]
    public void ValidationFailsWhenPromptNotSet()
    {
        // Arrange
        var trigger = new Subject<Unit>();
        var responses = new Subject<PromptResponse>();

        var poller = new Helpers.AlexaPromptPoller(scheduler, alexa, logger)
            .AddTrigger(trigger)
            .SetMediaPlayer("media_player.dining");

        // Act & Assert
        var action = () => poller.Subscribe();
        action.Should().Throw<InvalidOperationException>()
            .WithMessage("*Prompt*");

        trigger.OnCompleted();
        responses.OnCompleted();
    }

    [Fact]
    public void ValidationFailsWhenNoTriggersAdded()
    {
        // Arrange
        var responses = new Subject<PromptResponse>();

        var poller = new Helpers.AlexaPromptPoller(scheduler, alexa, logger)
            .SetMediaPlayer("media_player.dining")
            .SetPrompt("Should I turn on the lights?", "test_event_1");

        // Act & Assert
        var action = () => poller.Subscribe();
        action.Should().Throw<InvalidOperationException>()
            .WithMessage("*trigger*");

        responses.OnCompleted();
    }

    [Fact]
    public void TriggersWorkAgainAfterAcknowledgementAndDailyReset()
    {
        // Arrange - This test simulates: acknowledgement yesterday -> daily reset at 6am -> motion today
        var trigger = new Subject<Unit>();
        var dailyReset = new Subject<Unit>();
        var responses = new Subject<PromptResponse>();

        var poller = new Helpers.AlexaPromptPoller(scheduler, alexa, logger)
            .AddTrigger(trigger)
            .SetMediaPlayer("media_player.dining")
            .SetPrompt("Should I turn on the lights?", "test_event_1")
            .WithCooldown(TimeSpan.FromSeconds(0))
            .WithDailyReset(dailyReset);

        // Add response handler after poller is created
        poller.OnResponseYes(response =>
        {
            poller.Acknowledge();
        });

        var subscription = poller.Subscribe();

        // Act - First trigger works
        trigger.OnNext(Unit.Default);
        var stateAfterFirstTrigger = poller.GetState();

        // Acknowledge (user said yes)
        poller.Acknowledge();
        var stateAfterAcknowledge = poller.GetState();

        // Advance time and trigger daily reset (6am next day)
        scheduler.AdvanceBy(TimeSpan.FromHours(12).Ticks);
        dailyReset.OnNext(Unit.Default);
        var stateAfterDailyReset = poller.GetState();

        // Now trigger again (motion detected)
        trigger.OnNext(Unit.Default);
        var stateAfterSecondTrigger = poller.GetState();

        // Assert
        stateAfterFirstTrigger.IsWaitingForResponse.Should().BeTrue("First trigger should activate the prompt");
        stateAfterAcknowledge.IsAcknowledged.Should().BeTrue("Acknowledgement should set the acknowledged flag");
        stateAfterDailyReset.IsAcknowledged.Should().BeFalse("Daily reset should clear acknowledged flag");
        stateAfterDailyReset.LastPromptTime.Should().Be(DateTimeOffset.MinValue, "Daily reset should clear cooldown");
        stateAfterSecondTrigger.IsWaitingForResponse.Should().BeTrue("Trigger after reset should work and activate the prompt");

        subscription.Dispose();
        trigger.OnCompleted();
        dailyReset.OnCompleted();
        responses.OnCompleted();
    }

    [Fact]
    public void StatefulTriggerFiresWhenBothStateAndMotionOccur()
    {
        // Arrange
        var motionTrigger = new Subject<Unit>();
        var postboxState = new Subject<bool>();
        var responses = new Subject<PromptResponse>();

        var poller = new Helpers.AlexaPromptPoller(scheduler, alexa, logger)
            .AddTrigger(motionTrigger)
            .AddStatefulTrigger(postboxState, state => state)
            .SetMediaPlayer("media_player.dining")
            .SetPrompt("You have mail. Have you collected it?", "postbox_mail")
            .WithCooldown(TimeSpan.FromSeconds(0));

        var subscription = poller.Subscribe();

        // Act - Motion without postbox state should not trigger
        motionTrigger.OnNext(Unit.Default);
        var state1 = poller.GetState();

        // Set postbox state to true
        postboxState.OnNext(true);
        scheduler.AdvanceBy(TimeSpan.FromMilliseconds(100).Ticks);

        // Motion with postbox state should trigger
        motionTrigger.OnNext(Unit.Default);
        var state2 = poller.GetState();

        // Assert
        state1.IsWaitingForResponse.Should().BeFalse("Motion without postbox state should not trigger");
        state2.IsWaitingForResponse.Should().BeTrue("Motion with postbox state should trigger");

        subscription.Dispose();
        motionTrigger.OnCompleted();
        postboxState.OnCompleted();
        responses.OnCompleted();
    }

    [Fact]
    public void StatefulTriggerResetWhenAcknowledged()
    {
        // Arrange
        var motionTrigger = new Subject<Unit>();
        var postboxState = new Subject<bool>();
        var responses = new Subject<PromptResponse>();

        var poller = new Helpers.AlexaPromptPoller(scheduler, alexa, logger)
            .AddTrigger(motionTrigger)
            .AddStatefulTrigger(postboxState, state => state)
            .SetMediaPlayer("media_player.dining")
            .SetPrompt("You have mail. Have you collected it?", "postbox_mail")
            .WithCooldown(TimeSpan.FromSeconds(0));

        var subscription = poller.Subscribe();

        // Act - Set postbox state and trigger motion
        postboxState.OnNext(true);
        scheduler.AdvanceBy(TimeSpan.FromMilliseconds(100).Ticks);
        
        motionTrigger.OnNext(Unit.Default);
        var stateAfterTrigger = poller.GetState();

        // Postbox closes, but flag should still be true (sticky)
        postboxState.OnNext(false);
        scheduler.AdvanceBy(TimeSpan.FromMilliseconds(100).Ticks);

        // Acknowledge the prompt
        poller.Acknowledge();
        scheduler.AdvanceBy(TimeSpan.FromMilliseconds(100).Ticks);

        // Try motion again - should not fire because state was reset by acknowledge
        motionTrigger.OnNext(Unit.Default);
        var stateAfterAcknowledge = poller.GetState();

        // Reset the poller
        poller.ResetState();
        scheduler.AdvanceBy(TimeSpan.FromMilliseconds(100).Ticks);

        // Postbox opens again and motion occurs - should fire because flag was reset and postbox is true again
        postboxState.OnNext(true);
        scheduler.AdvanceBy(TimeSpan.FromMilliseconds(100).Ticks);
        motionTrigger.OnNext(Unit.Default);
        var stateAfterReopen = poller.GetState();

        // Assert
        stateAfterTrigger.IsWaitingForResponse.Should().BeTrue("Initial trigger should work");
        stateAfterAcknowledge.IsWaitingForResponse.Should().BeFalse("Motion after acknowledge should not trigger (poller acknowledged)");
        stateAfterReopen.IsWaitingForResponse.Should().BeTrue("After reset with postbox open, motion should trigger again");

        subscription.Dispose();
        motionTrigger.OnCompleted();
        postboxState.OnCompleted();
        responses.OnCompleted();
    }

    [Fact]
    public void StatefulTriggerAllowsMultipleMotionsBeforeAcknowledge()
    {
        // Arrange
        var motionTrigger = new Subject<Unit>();
        var postboxState = new Subject<bool>();
        var responses = new Subject<PromptResponse>();

        var poller = new Helpers.AlexaPromptPoller(scheduler, alexa, logger)
            .AddTrigger(motionTrigger)
            .AddStatefulTrigger(postboxState, state => state)
            .SetMediaPlayer("media_player.dining")
            .SetPrompt("You have mail. Have you collected it?", "postbox_mail")
            .WithCooldown(TimeSpan.FromSeconds(0));

        var subscription = poller.Subscribe();

        // Act - Set postbox state
        postboxState.OnNext(true);
        scheduler.AdvanceBy(TimeSpan.FromMilliseconds(100).Ticks);

        // First motion trigger
        motionTrigger.OnNext(Unit.Default);
        var state1 = poller.GetState();

        // Many hours pass... trigger motion again (postbox flag still set)
        scheduler.AdvanceBy(TimeSpan.FromHours(5).Ticks);
        motionTrigger.OnNext(Unit.Default);
        var state2 = poller.GetState();

        // Assert
        state1.IsWaitingForResponse.Should().BeTrue("First motion should trigger");
        state2.IsWaitingForResponse.Should().BeTrue("Motion hours later should still trigger (state flag persists)");

        subscription.Dispose();
        motionTrigger.OnCompleted();
        postboxState.OnCompleted();
        responses.OnCompleted();
    }

    [Fact]
    public void MultipleStatefulTriggersWithOr()
    {
        // Arrange
        var motionTrigger = new Subject<Unit>();
        var postboxState = new Subject<bool>();
        var doorState = new Subject<bool>();
        var responses = new Subject<PromptResponse>();

        var poller = new Helpers.AlexaPromptPoller(scheduler, alexa, logger)
            .AddTrigger(motionTrigger)
            .AddStatefulTrigger(postboxState, state => state)
            .AddStatefulTrigger(doorState, state => state)
            .SetMediaPlayer("media_player.dining")
            .SetPrompt("Event detected. Have you handled it?", "event_check")
            .WithCooldown(TimeSpan.FromSeconds(0));

        var subscription = poller.Subscribe();

        // Act - Only postbox state set
        postboxState.OnNext(true);
        scheduler.AdvanceBy(TimeSpan.FromMilliseconds(100).Ticks);
        motionTrigger.OnNext(Unit.Default);
        var state1 = poller.GetState();

        // Reset and try with only door state
        poller.ResetState();
        scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);
        doorState.OnNext(true);
        scheduler.AdvanceBy(TimeSpan.FromMilliseconds(100).Ticks);
        motionTrigger.OnNext(Unit.Default);
        var state2 = poller.GetState();

        // Assert - Both should trigger independently
        state1.IsWaitingForResponse.Should().BeTrue("Postbox state should allow trigger");
        state2.IsWaitingForResponse.Should().BeTrue("Door state should also allow trigger");

        subscription.Dispose();
        motionTrigger.OnCompleted();
        postboxState.OnCompleted();
        doorState.OnCompleted();
        responses.OnCompleted();
    }

    [Fact]
    public void StatefulTriggerFiresAgainAfterCooldownWithResponseNone()
    {
        // Gherkin Syntax:
        // Feature: Stateful Trigger Re-fires After Cooldown with Timeout Response
        // Scenario: Post-box reopens and motion triggers new prompt after cooldown expires
        //
        // Given: A poller with motion trigger, post-box state trigger, and 30-second cooldown
        // And: OnResponseNotYes handler configured (non-acknowledgement)
        //
        // When: Post-box opens and motion is detected
        // Then: First prompt is sent at t=0
        //
        // When: Response times out at t=20s (ResponseNone received, no user acknowledgement)
        // Then: IsWaitingForResponse becomes false
        // And: IsAcknowledged remains false (timeout != acknowledgement)
        //
        // When: Post-box closes at t=25s
        // Then: The state flag remains sticky (true) from the initial trigger
        //
        // When: Motion occurs at t=26s (still in cooldown period)
        // Then: No new prompt is sent (cooldown blocks it)
        //
        // When: Motion occurs at t=31s (past 30-second cooldown)
        // Then: Second prompt is sent (cooldown expired, state flag still sticky)
        // And: Two prompts total should have been queued
        //
        // Arrange - Simulates postbox scenario: postbox opened, motion detected -> prompt
        // -> timeout/ResponseNone -> more motion after cooldown should re-prompt if postbox was opened
        var motionTrigger = new Subject<Unit>();
        var postboxState = new Subject<bool>();
        var cooldownSeconds = 30;

        var poller = new Helpers.AlexaPromptPoller(scheduler, alexa, logger)
            .AddTrigger(motionTrigger)
            .AddStatefulTrigger(postboxState, state => state)
            .SetMediaPlayer("media_player.dining")
            .SetPrompt("You have mail. Have you collected it?", "postbox_mail")
            .WithCooldown(TimeSpan.FromSeconds(cooldownSeconds))
            .OnResponseNotYes(_ => { /* Just log or announce - don't acknowledge */ });

        var subscription = poller.Subscribe();

        // Act - Postbox opens (state = true)
        postboxState.OnNext(true);
        scheduler.AdvanceBy(TimeSpan.FromMilliseconds(100).Ticks);

        // Motion detected -> prompt sent at t=0
        motionTrigger.OnNext(Unit.Default);
        var stateAfterPrompt = poller.GetState();
        var promptCountAfterMotion = alexa.PromptCallCount;

        // Response comes back at t=20s (timeout)
        scheduler.AdvanceBy(TimeSpan.FromSeconds(20).Ticks);
        alexa.QueueResponse(new PromptResponse
        {
            EventId = "postbox_mail",
            ResponseType = PromptResponseType.ResponseNone,
            ResponsePersonName = "UNKNOWN"
        });
        var stateAfterResponse = poller.GetState();

        // Postbox closes at t=25s, but flag should remain true (sticky)
        scheduler.AdvanceBy(TimeSpan.FromSeconds(5).Ticks);
        postboxState.OnNext(false);
        scheduler.AdvanceBy(TimeSpan.FromMilliseconds(100).Ticks);

        // More motion at t=26s (still in cooldown)
        motionTrigger.OnNext(Unit.Default);
        var stateAt26s = poller.GetState();
        var promptCountAt26s = alexa.PromptCallCount;

        // More motion at t=31s (past cooldown - should trigger again because flag is sticky)
        scheduler.AdvanceBy(TimeSpan.FromSeconds(5).Ticks);
        motionTrigger.OnNext(Unit.Default);
        var stateAt31s = poller.GetState();
        var promptCountAt31s = alexa.PromptCallCount;

        // Assert
        stateAfterPrompt.IsWaitingForResponse.Should().BeTrue("Prompt should be sent after motion");
        promptCountAfterMotion.Should().Be(1, "First motion should trigger one prompt");
        
        stateAfterResponse.IsWaitingForResponse.Should().BeFalse("ResponseNone should clear waiting flag");
        stateAfterResponse.IsAcknowledged.Should().BeFalse("ResponseNone should NOT acknowledge (user didn't confirm)");
        
        stateAt26s.IsWaitingForResponse.Should().BeFalse("Motion at t=26s (in cooldown) should be blocked by cooldown, not by missing flag");
        promptCountAt26s.Should().Be(1, "Motion at t=26s should NOT send a prompt (still in cooldown)");
        
        stateAt31s.IsWaitingForResponse.Should().BeTrue("Motion at t=31s (past cooldown) should trigger new prompt even though postbox closed");
        promptCountAt31s.Should().Be(2, "Motion at t=31s should send a second prompt (past cooldown)");

        // Verify the prompt content
        alexa.PromptHistory.Should().HaveCount(2);
        alexa.PromptHistory[0].Should().Be(("media_player.dining", "You have mail. Have you collected it?", "postbox_mail"));
        alexa.PromptHistory[1].Should().Be(("media_player.dining", "You have mail. Have you collected it?", "postbox_mail"));

        subscription.Dispose();
        motionTrigger.OnCompleted();
        postboxState.OnCompleted();
    }
}
