using System;
using System.Collections.Generic;

namespace Niemand;

public interface IEntities
{
    AlarmControlPanelEntities AlarmControlPanel { get; }

    AutomationEntities Automation { get; }

    BinarySensorEntities BinarySensor { get; }

    CalendarEntities Calendar { get; }

    CameraEntities Camera { get; }

    ClimateEntities Climate { get; }

    CoverEntities Cover { get; }

    DeviceTrackerEntities DeviceTracker { get; }

    GroupEntities Group { get; }

    InputBooleanEntities InputBoolean { get; }

    InputNumberEntities InputNumber { get; }

    InputSelectEntities InputSelect { get; }

    InputTextEntities InputText { get; }

    LightEntities Light { get; }

    LockEntities Lock { get; }

    MediaPlayerEntities MediaPlayer { get; }

    NumberEntities Number { get; }

    OctopusagileEntities Octopusagile { get; }

    PersonEntities Person { get; }

    RemoteEntities Remote { get; }

    ScriptEntities Script { get; }

    SelectEntities Select { get; }

    SensorEntities Sensor { get; }

    SunEntities Sun { get; }

    SwitchEntities Switch { get; }

    TimerEntities Timer { get; }

    WeatherEntities Weather { get; }

    ZoneEntities Zone { get; }
}

public class Entities : IEntities
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public Entities(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public AlarmControlPanelEntities AlarmControlPanel => new(_haContext);
    public AutomationEntities Automation => new(_haContext);
    public BinarySensorEntities BinarySensor => new(_haContext);
    public CalendarEntities Calendar => new(_haContext);
    public CameraEntities Camera => new(_haContext);
    public ClimateEntities Climate => new(_haContext);
    public CoverEntities Cover => new(_haContext);
    public DeviceTrackerEntities DeviceTracker => new(_haContext);
    public GroupEntities Group => new(_haContext);
    public InputBooleanEntities InputBoolean => new(_haContext);
    public InputNumberEntities InputNumber => new(_haContext);
    public InputSelectEntities InputSelect => new(_haContext);
    public InputTextEntities InputText => new(_haContext);
    public LightEntities Light => new(_haContext);
    public LockEntities Lock => new(_haContext);
    public MediaPlayerEntities MediaPlayer => new(_haContext);
    public NumberEntities Number => new(_haContext);
    public OctopusagileEntities Octopusagile => new(_haContext);
    public PersonEntities Person => new(_haContext);
    public RemoteEntities Remote => new(_haContext);
    public ScriptEntities Script => new(_haContext);
    public SelectEntities Select => new(_haContext);
    public SensorEntities Sensor => new(_haContext);
    public SunEntities Sun => new(_haContext);
    public SwitchEntities Switch => new(_haContext);
    public TimerEntities Timer => new(_haContext);
    public WeatherEntities Weather => new(_haContext);
    public ZoneEntities Zone => new(_haContext);
}

public class AlarmControlPanelEntities
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public AlarmControlPanelEntities(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public AlarmControlPanelEntity Alarmo => new(_haContext, "alarm_control_panel.alarmo");
}

public class AutomationEntities
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public AutomationEntities(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public AutomationEntity AlaramArrivingHome => new(_haContext, "automation.alaram_arriving_home");
    public AutomationEntity AlaramLeavingHome => new(_haContext, "automation.alaram_leaving_home");
    public AutomationEntity AlarmTriggered => new(_haContext, "automation.alarm_triggered");
    public AutomationEntity ArriveHome => new(_haContext, "automation.arrive_home");
    public AutomationEntity ControllerIkeaE1524E18105ButtonRemote => new(_haContext, "automation.controller_ikea_e1524_e1810_5_button_remote");
    public AutomationEntity DingBlinksOffice => new(_haContext, "automation.ding_blinks_office");
    public AutomationEntity DoorChime => new(_haContext, "automation.door_chime");
    public AutomationEntity DoorLockChecks => new(_haContext, "automation.door_lock_checks");
    public AutomationEntity DoorLockChecks2 => new(_haContext, "automation.door_lock_checks_2");
    public AutomationEntity DoorNotify => new(_haContext, "automation.door_notify");
    public AutomationEntity Doorbell => new(_haContext, "automation.doorbell");
    public AutomationEntity DryerDoneResponse => new(_haContext, "automation.dryer_done_response");
    public AutomationEntity DryerNotification => new(_haContext, "automation.dryer_notification");
    public AutomationEntity DryerReset => new(_haContext, "automation.dryer_reset");
    public AutomationEntity FrontDoorMotion => new(_haContext, "automation.front_door_motion");
    public AutomationEntity GrannyArrives => new(_haContext, "automation.granny_arrives");
    public AutomationEntity HassStarted => new(_haContext, "automation.hass_started");
    public AutomationEntity HouseModeRequest => new(_haContext, "automation.house_mode_request");
    public AutomationEntity HouseModeResponse => new(_haContext, "automation.house_mode_response");
    public AutomationEntity IkeaOpenCloseRemote => new(_haContext, "automation.ikea_open_close_remote");
    public AutomationEntity InBed => new(_haContext, "automation.in_bed");
    public AutomationEntity JaydenAlarmTrigger => new(_haContext, "automation.jayden_alarm_trigger");
    public AutomationEntity KeepJaydenInBed => new(_haContext, "automation.keep_jayden_in_bed");
    public AutomationEntity LaundryDoneRequest => new(_haContext, "automation.laundry_done_request");
    public AutomationEntity LaundryDoneResponse => new(_haContext, "automation.laundry_done_response");
    public AutomationEntity LeaveHome => new(_haContext, "automation.leave_home");
    public AutomationEntity NightModeRequest => new(_haContext, "automation.night_mode_request");
    public AutomationEntity NightModeResponse => new(_haContext, "automation.night_mode_response");
    public AutomationEntity NotifyOutOfBed => new(_haContext, "automation.notify_out_of_bed");
    public AutomationEntity OfficeDoorLeftOpenRequest => new(_haContext, "automation.office_door_left_open_request");
    public AutomationEntity OfficeDoorLeftOpenResponse => new(_haContext, "automation.office_door_left_open_response");
    public AutomationEntity ReEnablePiHole => new(_haContext, "automation.re_enable_pi_hole");
    public AutomationEntity RefreshAudiData => new(_haContext, "automation.refresh_audi_data");
    public AutomationEntity RemoteTrial => new(_haContext, "automation.remote_trial");
    public AutomationEntity ResetRadiatorMode => new(_haContext, "automation.reset_radiator_mode");
    public AutomationEntity SetRingSnapshotIntervalOnStartup => new(_haContext, "automation.set_ring_snapshot_interval_on_startup");
    public AutomationEntity SnoozeRingChime => new(_haContext, "automation.snooze_ring_chime");
    public AutomationEntity SnowMachine => new(_haContext, "automation.snow_machine");
    public AutomationEntity UpdateNetdaemonAndRestartContainer => new(_haContext, "automation.update_netdaemon_and_restart_container");
    public AutomationEntity UtilitiesAcknowledged => new(_haContext, "automation.utilities_acknowledged");
    public AutomationEntity Wakeup => new(_haContext, "automation.wakeup");
    public AutomationEntity WashingDoneResponse => new(_haContext, "automation.washing_done_response");
    public AutomationEntity WashingNotification => new(_haContext, "automation.washing_notification");
    public AutomationEntity WashingReset => new(_haContext, "automation.washing_reset");
}

public class BinarySensorEntities
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public BinarySensorEntities(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public BinarySensorEntity AaronMotion => new(_haContext, "binary_sensor.aaron_motion");
    public BinarySensorEntity AaronMotionOccupancy => new(_haContext, "binary_sensor.aaron_motion_occupancy");
    public BinarySensorEntity AubreciaFrontDoorDing => new(_haContext, "binary_sensor.aubrecia_front_door_ding");
    public BinarySensorEntity AubreciaFrontDoorMotion => new(_haContext, "binary_sensor.aubrecia_front_door_motion");
    public BinarySensorEntity AudiQ7Bonnet => new(_haContext, "binary_sensor.audi_q7_bonnet");
    public BinarySensorEntity AudiQ7Boot => new(_haContext, "binary_sensor.audi_q7_boot");
    public BinarySensorEntity AudiQ7BootLock => new(_haContext, "binary_sensor.audi_q7_boot_lock");
    public BinarySensorEntity AudiQ7Doors => new(_haContext, "binary_sensor.audi_q7_doors");
    public BinarySensorEntity AudiQ7DoorsLock => new(_haContext, "binary_sensor.audi_q7_doors_lock");
    public BinarySensorEntity AudiQ7ParkingLight => new(_haContext, "binary_sensor.audi_q7_parking_light");
    public BinarySensorEntity AudiQ7Windows => new(_haContext, "binary_sensor.audi_q7_windows");
    public BinarySensorEntity BackDoor => new(_haContext, "binary_sensor.back_door");
    public BinarySensorEntity BathroomMotion => new(_haContext, "binary_sensor.bathroom_motion");
    public BinarySensorEntity BathroomMotionOccupancy => new(_haContext, "binary_sensor.bathroom_motion_occupancy");
    public BinarySensorEntity BlindLoungeOverheating => new(_haContext, "binary_sensor.blind_lounge_overheating");
    public BinarySensorEntity Dining => new(_haContext, "binary_sensor.dining");
    public BinarySensorEntity DiningDoor => new(_haContext, "binary_sensor.dining_door");
    public BinarySensorEntity DiningMotion => new(_haContext, "binary_sensor.dining_motion");
    public BinarySensorEntity DiningOccupancy => new(_haContext, "binary_sensor.dining_occupancy");
    public BinarySensorEntity EntranceMotion => new(_haContext, "binary_sensor.entrance_motion");
    public BinarySensorEntity EntranceMotionOccupancy => new(_haContext, "binary_sensor.entrance_motion_occupancy");
    public BinarySensorEntity EugeneLaptopInUse => new(_haContext, "binary_sensor.eugene_laptop_in_use");
    public BinarySensorEntity EugenesIphoneFocus => new(_haContext, "binary_sensor.eugenes_iphone_focus");
    public BinarySensorEntity FrontDoor => new(_haContext, "binary_sensor.front_door");
    public BinarySensorEntity GarageBackDoor => new(_haContext, "binary_sensor.garage_back_door");
    public BinarySensorEntity GardenMotion2 => new(_haContext, "binary_sensor.garden_motion_2");
    public BinarySensorEntity HaileySIphoneFocus => new(_haContext, "binary_sensor.hailey_s_iphone_focus");
    public BinarySensorEntity HaileysMacbookAirActive => new(_haContext, "binary_sensor.haileys_macbook_air_active");
    public BinarySensorEntity HaileysMacbookAirCameraInUse => new(_haContext, "binary_sensor.haileys_macbook_air_camera_in_use");
    public BinarySensorEntity HaileysMacbookAirFacetimeHdCameraBuiltIn => new(_haContext, "binary_sensor.haileys_macbook_air_facetime_hd_camera_built_in");
    public BinarySensorEntity HaileysMacbookAirKrispMicrophone => new(_haContext, "binary_sensor.haileys_macbook_air_krisp_microphone");
    public BinarySensorEntity HaileysMacbookAirMacbookAirMicrophone => new(_haContext, "binary_sensor.haileys_macbook_air_macbook_air_microphone");
    public BinarySensorEntity HaileysMacbookAirMicrophoneInUse => new(_haContext, "binary_sensor.haileys_macbook_air_microphone_in_use");
    public BinarySensorEntity HaileysMacbookAirPltLegend => new(_haContext, "binary_sensor.haileys_macbook_air_plt_legend");
    public BinarySensorEntity HaileysMacbookAirUsbPnpAudioDevice => new(_haContext, "binary_sensor.haileys_macbook_air_usb_pnp_audio_device");
    public BinarySensorEntity Hallway => new(_haContext, "binary_sensor.hallway");
    public BinarySensorEntity HomeOccupied => new(_haContext, "binary_sensor.home_occupied");
    public BinarySensorEntity JaydenMotion => new(_haContext, "binary_sensor.jayden_motion");
    public BinarySensorEntity JaydenMotionOccupancy => new(_haContext, "binary_sensor.jayden_motion_occupancy");
    public BinarySensorEntity JohanFrontDoorDing => new(_haContext, "binary_sensor.johan_front_door_ding");
    public BinarySensorEntity JohanFrontDoorMotion => new(_haContext, "binary_sensor.johan_front_door_motion");
    public BinarySensorEntity KeTradfriOpenCloseRemote3dcb2efeOnOff => new(_haContext, "binary_sensor.ke_tradfri_open_close_remote_3dcb2efe_on_off");
    public BinarySensorEntity Kitchen => new(_haContext, "binary_sensor.kitchen");
    public BinarySensorEntity KitchenMotion => new(_haContext, "binary_sensor.kitchen_motion");
    public BinarySensorEntity KitchenMotionOccupancy => new(_haContext, "binary_sensor.kitchen_motion_occupancy");
    public BinarySensorEntity Landing => new(_haContext, "binary_sensor.landing");
    public BinarySensorEntity LandingMotion => new(_haContext, "binary_sensor.landing_motion");
    public BinarySensorEntity LandingMotionOccupancy => new(_haContext, "binary_sensor.landing_motion_occupancy");
    public BinarySensorEntity LandingSmoke => new(_haContext, "binary_sensor.landing_smoke");
    public BinarySensorEntity LgTvInUse => new(_haContext, "binary_sensor.lg_tv_in_use");
    public BinarySensorEntity Lounge => new(_haContext, "binary_sensor.lounge");
    public BinarySensorEntity LoungeDoor => new(_haContext, "binary_sensor.lounge_door");
    public BinarySensorEntity LoungeLeftWindow => new(_haContext, "binary_sensor.lounge_left_window");
    public BinarySensorEntity LoungeMotion => new(_haContext, "binary_sensor.lounge_motion");
    public BinarySensorEntity LoungeMotionOccupancy => new(_haContext, "binary_sensor.lounge_motion_occupancy");
    public BinarySensorEntity LoungeRightWindow => new(_haContext, "binary_sensor.lounge_right_window");
    public BinarySensorEntity LumiLumiSensorMagnetAq256141203OnOff => new(_haContext, "binary_sensor.lumi_lumi_sensor_magnet_aq2_56141203_on_off");
    public BinarySensorEntity LumiLumiSensorMagnetAq283903a03OnOff => new(_haContext, "binary_sensor.lumi_lumi_sensor_magnet_aq2_83903a03_on_off");
    public BinarySensorEntity LumiLumiSensorMagnetAq28c913a03OnOff => new(_haContext, "binary_sensor.lumi_lumi_sensor_magnet_aq2_8c913a03_on_off");
    public BinarySensorEntity LumiLumiSensorMagnetAq2OnOff => new(_haContext, "binary_sensor.lumi_lumi_sensor_magnet_aq2_on_off");
    public BinarySensorEntity MasterMotion => new(_haContext, "binary_sensor.master_motion");
    public BinarySensorEntity MasterMotionOccupancy => new(_haContext, "binary_sensor.master_motion_occupancy");
    public BinarySensorEntity MyWallPanelAcPlugged => new(_haContext, "binary_sensor.my_wall_panel_ac_plugged");
    public BinarySensorEntity MyWallPanelCharging => new(_haContext, "binary_sensor.my_wall_panel_charging");
    public BinarySensorEntity MyWallPanelMotionDetected => new(_haContext, "binary_sensor.my_wall_panel_motion_detected");
    public BinarySensorEntity MyWallPanelUsbPlugged => new(_haContext, "binary_sensor.my_wall_panel_usb_plugged");
    public BinarySensorEntity NiemandDriveMotion => new(_haContext, "binary_sensor.niemand_drive_motion");
    public BinarySensorEntity NiemandDriveMotion2 => new(_haContext, "binary_sensor.niemand_drive_motion_2");
    public BinarySensorEntity NiemandFrontDoorDing => new(_haContext, "binary_sensor.niemand_front_door_ding");
    public BinarySensorEntity NiemandFrontDoorDing2 => new(_haContext, "binary_sensor.niemand_front_door_ding_2");
    public BinarySensorEntity NiemandFrontDoorMotion => new(_haContext, "binary_sensor.niemand_front_door_motion");
    public BinarySensorEntity NiemandFrontDoorMotion2 => new(_haContext, "binary_sensor.niemand_front_door_motion_2");
    public BinarySensorEntity NiemandGarageMotion => new(_haContext, "binary_sensor.niemand_garage_motion");
    public BinarySensorEntity NiemandGarageMotion2 => new(_haContext, "binary_sensor.niemand_garage_motion_2");
    public BinarySensorEntity NiemandGardenMotion => new(_haContext, "binary_sensor.niemand_garden_motion");
    public BinarySensorEntity NiemandGardenMotion2 => new(_haContext, "binary_sensor.niemand_garden_motion_2");
    public BinarySensorEntity NiemandSideMotion => new(_haContext, "binary_sensor.niemand_side_motion");
    public BinarySensorEntity NiemandSideMotion2 => new(_haContext, "binary_sensor.niemand_side_motion_2");
    public BinarySensorEntity OfficeDoor => new(_haContext, "binary_sensor.office_door");
    public BinarySensorEntity OfficeMotion => new(_haContext, "binary_sensor.office_motion");
    public BinarySensorEntity OfficeMotionOccupancy => new(_haContext, "binary_sensor.office_motion_occupancy");
    public BinarySensorEntity PingGoogle => new(_haContext, "binary_sensor.ping_google");
    public BinarySensorEntity PingKonnectedAddOn => new(_haContext, "binary_sensor.ping_konnected_add_on");
    public BinarySensorEntity PingKonnectedMain => new(_haContext, "binary_sensor.ping_konnected_main");
    public BinarySensorEntity PlayroomMotion => new(_haContext, "binary_sensor.playroom_motion");
    public BinarySensorEntity PlayroomMotionOccupancy => new(_haContext, "binary_sensor.playroom_motion_occupancy");
    public BinarySensorEntity RemoteUi => new(_haContext, "binary_sensor.remote_ui");
    public BinarySensorEntity Study => new(_haContext, "binary_sensor.study");
    public BinarySensorEntity ToiletMotion => new(_haContext, "binary_sensor.toilet_motion");
    public BinarySensorEntity ToiletMotionOccupancy => new(_haContext, "binary_sensor.toilet_motion_occupancy");
    public BinarySensorEntity ToiletWindow => new(_haContext, "binary_sensor.toilet_window");
    public BinarySensorEntity Updater => new(_haContext, "binary_sensor.updater");
    public BinarySensorEntity UtilityMotion => new(_haContext, "binary_sensor.utility_motion");
    public BinarySensorEntity UtilityMotionOccupancy => new(_haContext, "binary_sensor.utility_motion_occupancy");
}

public class CalendarEntities
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public CalendarEntities(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public CalendarEntity Home => new(_haContext, "calendar.home");
}

public class CameraEntities
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public CameraEntities(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public CameraEntity AubreciaFrontDoor => new(_haContext, "camera.aubrecia_front_door");
    public CameraEntity Garden2 => new(_haContext, "camera.garden_2");
    public CameraEntity JohanFrontDoor => new(_haContext, "camera.johan_front_door");
    public CameraEntity NiemandDrive => new(_haContext, "camera.niemand_drive");
    public CameraEntity NiemandDriveSnapshot => new(_haContext, "camera.niemand_drive_snapshot");
    public CameraEntity NiemandFrontDoor => new(_haContext, "camera.niemand_front_door");
    public CameraEntity NiemandFrontDoorSnapshot => new(_haContext, "camera.niemand_front_door_snapshot");
    public CameraEntity NiemandGarage => new(_haContext, "camera.niemand_garage");
    public CameraEntity NiemandGarageSnapshot => new(_haContext, "camera.niemand_garage_snapshot");
    public CameraEntity NiemandGarden => new(_haContext, "camera.niemand_garden");
    public CameraEntity NiemandGardenSnapshot => new(_haContext, "camera.niemand_garden_snapshot");
    public CameraEntity NiemandSide => new(_haContext, "camera.niemand_side");
    public CameraEntity NiemandSideSnapshot => new(_haContext, "camera.niemand_side_snapshot");
    public CameraEntity RingDriveVideo => new(_haContext, "camera.ring_drive_video");
    public CameraEntity RingFrontDoorVideo => new(_haContext, "camera.ring_front_door_video");
    public CameraEntity RingGarageVideo => new(_haContext, "camera.ring_garage_video");
    public CameraEntity RingGardenVideo => new(_haContext, "camera.ring_garden_video");
    public CameraEntity RingSideVideo => new(_haContext, "camera.ring_side_video");
}

public class ClimateEntities
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public ClimateEntities(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public ClimateEntity Bedroom1 => new(_haContext, "climate.bedroom_1");
    public ClimateEntity Bedroom2 => new(_haContext, "climate.bedroom_2");
    public ClimateEntity Bedroom3 => new(_haContext, "climate.bedroom_3");
    public ClimateEntity Bedroom4 => new(_haContext, "climate.bedroom_4");
    public ClimateEntity Lounge => new(_haContext, "climate.lounge");
    public ClimateEntity Office => new(_haContext, "climate.office");
    public ClimateEntity WiserBoys => new(_haContext, "climate.wiser_boys");
    public ClimateEntity WiserDining => new(_haContext, "climate.wiser_dining");
    public ClimateEntity WiserEntrance => new(_haContext, "climate.wiser_entrance");
    public ClimateEntity WiserGuestRoom => new(_haContext, "climate.wiser_guest_room");
    public ClimateEntity WiserLanding => new(_haContext, "climate.wiser_landing");
    public ClimateEntity WiserLounge => new(_haContext, "climate.wiser_lounge");
    public ClimateEntity WiserLoungeBay => new(_haContext, "climate.wiser_lounge_bay");
    public ClimateEntity WiserMaster => new(_haContext, "climate.wiser_master");
    public ClimateEntity WiserOffice => new(_haContext, "climate.wiser_office");
    public ClimateEntity WiserPlayroom => new(_haContext, "climate.wiser_playroom");
    public ClimateEntity WiserUtility => new(_haContext, "climate.wiser_utility");
}

public class CoverEntities
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public CoverEntities(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public CoverEntity BlindLounge => new(_haContext, "cover.blind_lounge");
    public CoverEntity LandingBlind => new(_haContext, "cover.landing_blind");
}

public class DeviceTrackerEntities
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public DeviceTrackerEntities(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public DeviceTrackerEntity E247d4d7d6c90Mysimplelink => new(_haContext, "device_tracker.247d4d7d6c90_mysimplelink");
    public DeviceTrackerEntity AaronEcho => new(_haContext, "device_tracker.aaron_echo");
    public DeviceTrackerEntity AsglhWl19140 => new(_haContext, "device_tracker.asglh_wl_19140");
    public DeviceTrackerEntity Aubrecia => new(_haContext, "device_tracker.aubrecia");
    public DeviceTrackerEntity Aubreciasiphone => new(_haContext, "device_tracker.aubreciasiphone");
    public DeviceTrackerEntity AudiQ7Position => new(_haContext, "device_tracker.audi_q7_position");
    public DeviceTrackerEntity Bedroom1Ac => new(_haContext, "device_tracker.bedroom_1_ac");
    public DeviceTrackerEntity Bedroom2Ac => new(_haContext, "device_tracker.bedroom_2_ac");
    public DeviceTrackerEntity Bedroom3Ac => new(_haContext, "device_tracker.bedroom_3_ac");
    public DeviceTrackerEntity Bedroom4Ac => new(_haContext, "device_tracker.bedroom_4_ac");
    public DeviceTrackerEntity ChristmasIndoor1558 => new(_haContext, "device_tracker.christmas_indoor_1558");
    public DeviceTrackerEntity DesktopIpurn8t => new(_haContext, "device_tracker.desktop_ipurn8t");
    public DeviceTrackerEntity Dining => new(_haContext, "device_tracker.dining");
    public DeviceTrackerEntity DiningEcho => new(_haContext, "device_tracker.dining_echo");
    public DeviceTrackerEntity Dryer => new(_haContext, "device_tracker.dryer");
    public DeviceTrackerEntity Entrance => new(_haContext, "device_tracker.entrance");
    public DeviceTrackerEntity Esp6b7081 => new(_haContext, "device_tracker.esp_6b7081");
    public DeviceTrackerEntity Esp6b7a3a => new(_haContext, "device_tracker.esp_6b7a3a");
    public DeviceTrackerEntity EufyRobovac => new(_haContext, "device_tracker.eufy_robovac");
    public DeviceTrackerEntity EufyRobovac2 => new(_haContext, "device_tracker.eufy_robovac_2");
    public DeviceTrackerEntity EugeneDesktop => new(_haContext, "device_tracker.eugene_desktop");
    public DeviceTrackerEntity EugeneIphoneIp => new(_haContext, "device_tracker.eugene_iphone_ip");
    public DeviceTrackerEntity EugenesIphone => new(_haContext, "device_tracker.eugenes_iphone");
    public DeviceTrackerEntity EugenesIphone2 => new(_haContext, "device_tracker.eugenes_iphone_2");
    public DeviceTrackerEntity Eugenesplewatch => new(_haContext, "device_tracker.eugenesplewatch");
    public DeviceTrackerEntity FloorLight2086 => new(_haContext, "device_tracker.floor_light_2086");
    public DeviceTrackerEntity Foscam => new(_haContext, "device_tracker.foscam");
    public DeviceTrackerEntity GalaxyS8 => new(_haContext, "device_tracker.galaxy_s8");
    public DeviceTrackerEntity GarageEcho => new(_haContext, "device_tracker.garage_echo");
    public DeviceTrackerEntity GardenFloodlights => new(_haContext, "device_tracker.garden_floodlights");
    public DeviceTrackerEntity HaileyIphoneIp => new(_haContext, "device_tracker.hailey_iphone_ip");
    public DeviceTrackerEntity HaileySIphone => new(_haContext, "device_tracker.hailey_s_iphone");
    public DeviceTrackerEntity HaileysAir => new(_haContext, "device_tracker.haileys_air");
    public DeviceTrackerEntity HaileysIphone => new(_haContext, "device_tracker.haileys_iphone");
    public DeviceTrackerEntity HaileysMacbookAir => new(_haContext, "device_tracker.haileys_macbook_air");
    public DeviceTrackerEntity HostTwo => new(_haContext, "device_tracker.host_two");
    public DeviceTrackerEntity HuaweiPSmart201986203 => new(_haContext, "device_tracker.huawei_p_smart_2019_86203");
    public DeviceTrackerEntity Jayden => new(_haContext, "device_tracker.jayden");
    public DeviceTrackerEntity JaydenAppletv => new(_haContext, "device_tracker.jayden_appletv");
    public DeviceTrackerEntity JaydenBedside4734 => new(_haContext, "device_tracker.jayden_bedside_4734");
    public DeviceTrackerEntity JaydenEcho => new(_haContext, "device_tracker.jayden_echo");
    public DeviceTrackerEntity Kitchen => new(_haContext, "device_tracker.kitchen");
    public DeviceTrackerEntity KitchenEcho => new(_haContext, "device_tracker.kitchen_echo");
    public DeviceTrackerEntity KonnectedAddon => new(_haContext, "device_tracker.konnected_addon");
    public DeviceTrackerEntity KonnectedMain => new(_haContext, "device_tracker.konnected_main");
    public DeviceTrackerEntity Landing => new(_haContext, "device_tracker.landing");
    public DeviceTrackerEntity LgLounge => new(_haContext, "device_tracker.lg_lounge");
    public DeviceTrackerEntity LivingRoom => new(_haContext, "device_tracker.living_room");
    public DeviceTrackerEntity Lounge => new(_haContext, "device_tracker.lounge");
    public DeviceTrackerEntity LoungeAc => new(_haContext, "device_tracker.lounge_ac");
    public DeviceTrackerEntity LoungeBlind => new(_haContext, "device_tracker.lounge_blind");
    public DeviceTrackerEntity LoungeEcho => new(_haContext, "device_tracker.lounge_echo");
    public DeviceTrackerEntity MasterEcho => new(_haContext, "device_tracker.master_echo");
    public DeviceTrackerEntity MasterTele => new(_haContext, "device_tracker.master_tele");
    public DeviceTrackerEntity OfficeAc => new(_haContext, "device_tracker.office_ac");
    public DeviceTrackerEntity OfficeEcho => new(_haContext, "device_tracker.office_echo");
    public DeviceTrackerEntity OutsideDrive => new(_haContext, "device_tracker.outside_drive");
    public DeviceTrackerEntity OutsideGarage => new(_haContext, "device_tracker.outside_garage");
    public DeviceTrackerEntity PlayroomEcho => new(_haContext, "device_tracker.playroom_echo");
    public DeviceTrackerEntity Porch => new(_haContext, "device_tracker.porch");
    public DeviceTrackerEntity Raspberrypi => new(_haContext, "device_tracker.raspberrypi");
    public DeviceTrackerEntity RaspberrypiCups => new(_haContext, "device_tracker.raspberrypi_cups");
    public DeviceTrackerEntity Ringhpcam49 => new(_haContext, "device_tracker.ringhpcam_49");
    public DeviceTrackerEntity Ringhpcam4c => new(_haContext, "device_tracker.ringhpcam_4c");
    public DeviceTrackerEntity RingproD6 => new(_haContext, "device_tracker.ringpro_d6");
    public DeviceTrackerEntity Ringstickupcam94 => new(_haContext, "device_tracker.ringstickupcam_94");
    public DeviceTrackerEntity Ringstickupcam9b => new(_haContext, "device_tracker.ringstickupcam_9b");
    public DeviceTrackerEntity RmminiD92b62 => new(_haContext, "device_tracker.rmmini_d9_2b_62");
    public DeviceTrackerEntity Shelly155e8b5 => new(_haContext, "device_tracker.shelly1_55e8b5");
    public DeviceTrackerEntity SmartPlug1 => new(_haContext, "device_tracker.smart_plug_1");
    public DeviceTrackerEntity SmartPlug2 => new(_haContext, "device_tracker.smart_plug_2");
    public DeviceTrackerEntity SmartPlug4 => new(_haContext, "device_tracker.smart_plug_4");
    public DeviceTrackerEntity Sonoszp => new(_haContext, "device_tracker.sonoszp");
    public DeviceTrackerEntity Sonoszp2 => new(_haContext, "device_tracker.sonoszp_2");
    public DeviceTrackerEntity SuspectDevice => new(_haContext, "device_tracker.suspect_device");
    public DeviceTrackerEntity SuspectHuawei => new(_haContext, "device_tracker.suspect_huawei");
    public DeviceTrackerEntity Unifi0a1eCf173cBfDefault => new(_haContext, "device_tracker.unifi_0a_1e_cf_17_3c_bf_default");
    public DeviceTrackerEntity Unifi2668Db030cBeDefault => new(_haContext, "device_tracker.unifi_26_68_db_03_0c_be_default");
    public DeviceTrackerEntity Unifi26Fd74Eb3d99Default => new(_haContext, "device_tracker.unifi_26_fd_74_eb_3d_99_default");
    public DeviceTrackerEntity Unifi2aD7E18c21A6Default => new(_haContext, "device_tracker.unifi_2a_d7_e1_8c_21_a6_default");
    public DeviceTrackerEntity Unifi347e5cD68b20Default => new(_haContext, "device_tracker.unifi_34_7e_5c_d6_8b_20_default");
    public DeviceTrackerEntity Unifi625c7c18D2BfDefault => new(_haContext, "device_tracker.unifi_62_5c_7c_18_d2_bf_default");
    public DeviceTrackerEntity Unifi7aB7F6160d9cDefault => new(_haContext, "device_tracker.unifi_7a_b7_f6_16_0d_9c_default");
    public DeviceTrackerEntity Unifi8603C83f3f3aDefault => new(_haContext, "device_tracker.unifi_86_03_c8_3f_3f_3a_default");
    public DeviceTrackerEntity Unifi8a513f0f9323Default => new(_haContext, "device_tracker.unifi_8a_51_3f_0f_93_23_default");
    public DeviceTrackerEntity UnifiA8E3EeDdD898Default => new(_haContext, "device_tracker.unifi_a8_e3_ee_dd_d8_98_default");
    public DeviceTrackerEntity UnifiBa7aDe6312CbDefault => new(_haContext, "device_tracker.unifi_ba_7a_de_63_12_cb_default");
    public DeviceTrackerEntity UnifiC24cDdA852FeDefault => new(_haContext, "device_tracker.unifi_c2_4c_dd_a8_52_fe_default");
    public DeviceTrackerEntity UnifiC25413325fF5Default => new(_haContext, "device_tracker.unifi_c2_54_13_32_5f_f5_default");
    public DeviceTrackerEntity UnifiDcA632Dc56AfDefault => new(_haContext, "device_tracker.unifi_dc_a6_32_dc_56_af_default");
    public DeviceTrackerEntity UnifiEa7f17B856D1Default => new(_haContext, "device_tracker.unifi_ea_7f_17_b8_56_d1_default");
    public DeviceTrackerEntity UnifiFe5a3957E388Default => new(_haContext, "device_tracker.unifi_fe_5a_39_57_e3_88_default");
    public DeviceTrackerEntity Upstairs => new(_haContext, "device_tracker.upstairs");
    public DeviceTrackerEntity WallpanelFireHd8 => new(_haContext, "device_tracker.wallpanel_fire_hd8");
    public DeviceTrackerEntity Washer => new(_haContext, "device_tracker.washer");
    public DeviceTrackerEntity Wiserheat031c5e => new(_haContext, "device_tracker.wiserheat031c5e");
}

public class GroupEntities
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public GroupEntities(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public GroupEntity Christmas => new(_haContext, "group.christmas");
}

public class InputBooleanEntities
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public InputBooleanEntities(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public InputBooleanEntity AlaramPanic => new(_haContext, "input_boolean.alaram_panic");
    public InputBooleanEntity BackDoorChecked => new(_haContext, "input_boolean.back_door_checked");
    public InputBooleanEntity DiningDoorChecked => new(_haContext, "input_boolean.dining_door_checked");
    public InputBooleanEntity DryerAck => new(_haContext, "input_boolean.dryer_ack");
    public InputBooleanEntity FrontDoorChecked => new(_haContext, "input_boolean.front_door_checked");
    public InputBooleanEntity GarageBackDoorChecked => new(_haContext, "input_boolean.garage_back_door_checked");
    public InputBooleanEntity KidsInBed => new(_haContext, "input_boolean.kids_in_bed");
    public InputBooleanEntity LoungeDoorChecked => new(_haContext, "input_boolean.lounge_door_checked");
    public InputBooleanEntity LoungeMotionLightsDisabled => new(_haContext, "input_boolean.lounge_motion_lights_disabled");
    public InputBooleanEntity NotifyEugeneTelegram => new(_haContext, "input_boolean.notify_eugene_telegram");
    public InputBooleanEntity WasherAck => new(_haContext, "input_boolean.washer_ack");
}

public class InputNumberEntities
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public InputNumberEntities(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public InputNumberEntity AaronLuxLimit => new(_haContext, "input_number.aaron_lux_limit");
    public InputNumberEntity BoysLuxLimit => new(_haContext, "input_number.boys_lux_limit");
    public InputNumberEntity DiningLuxLimit => new(_haContext, "input_number.dining_lux_limit");
    public InputNumberEntity EntranceLuxLimit => new(_haContext, "input_number.entrance_lux_limit");
    public InputNumberEntity HallwayLuxLimit => new(_haContext, "input_number.hallway_lux_limit");
    public InputNumberEntity JaydenLuxLimit => new(_haContext, "input_number.jayden_lux_limit");
    public InputNumberEntity KitchenLuxLimit => new(_haContext, "input_number.kitchen_lux_limit");
    public InputNumberEntity LandingLuxLimit => new(_haContext, "input_number.landing_lux_limit");
    public InputNumberEntity LoungeLuxLimit => new(_haContext, "input_number.lounge_lux_limit");
    public InputNumberEntity MasterLuxLimit => new(_haContext, "input_number.master_lux_limit");
    public InputNumberEntity OfficeLuxLimit => new(_haContext, "input_number.office_lux_limit");
    public InputNumberEntity PlayroomLuxLimit => new(_haContext, "input_number.playroom_lux_limit");
    public InputNumberEntity RingChimeSnoozeMinutes => new(_haContext, "input_number.ring_chime_snooze_minutes");
    public InputNumberEntity RingChimeVolume => new(_haContext, "input_number.ring_chime_volume");
    public InputNumberEntity TimerIntervalMinutes => new(_haContext, "input_number.timer_interval_minutes");
    public InputNumberEntity ToiletLuxLimit => new(_haContext, "input_number.toilet_lux_limit");
    public InputNumberEntity UtilityLuxLimit => new(_haContext, "input_number.utility_lux_limit");
}

public class InputSelectEntities
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public InputSelectEntities(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public InputSelectEntity HouseMode => new(_haContext, "input_select.house_mode");
    public InputSelectEntity TimerSpeaker => new(_haContext, "input_select.timer_speaker");
}

public class InputTextEntities
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public InputTextEntities(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public InputTextEntity AlexaActionableNotification => new(_haContext, "input_text.alexa_actionable_notification");
    public InputTextEntity ImText => new(_haContext, "input_text.im_text");
    public InputTextEntity TtsText => new(_haContext, "input_text.tts_text");
}

public class LightEntities
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public LightEntities(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public LightEntity Aaron => new(_haContext, "light.aaron");
    public LightEntity Aaron1 => new(_haContext, "light.aaron_1");
    public LightEntity Aaron2 => new(_haContext, "light.aaron_2");
    public LightEntity Aaron3 => new(_haContext, "light.aaron_3");
    public LightEntity Aaron4 => new(_haContext, "light.aaron_4");
    public LightEntity ChristmasIndoorSonoff => new(_haContext, "light.christmas_indoor_sonoff");
    public LightEntity Dining => new(_haContext, "light.dining");
    public LightEntity Dining1 => new(_haContext, "light.dining_1");
    public LightEntity Dining2 => new(_haContext, "light.dining_2");
    public LightEntity Dining3 => new(_haContext, "light.dining_3");
    public LightEntity Dining4 => new(_haContext, "light.dining_4");
    public LightEntity Dining5 => new(_haContext, "light.dining_5");
    public LightEntity DiningWall => new(_haContext, "light.dining_wall");
    public LightEntity DiningWall1 => new(_haContext, "light.dining_wall_1");
    public LightEntity DiningWall2 => new(_haContext, "light.dining_wall_2");
    public LightEntity Downstairs => new(_haContext, "light.downstairs");
    public LightEntity Entrance => new(_haContext, "light.entrance");
    public LightEntity Floor => new(_haContext, "light.floor");
    public LightEntity GardenLight2 => new(_haContext, "light.garden_light_2");
    public LightEntity Hallway => new(_haContext, "light.hallway");
    public LightEntity Jayden => new(_haContext, "light.jayden");
    public LightEntity Jayden1 => new(_haContext, "light.jayden_1");
    public LightEntity Jayden2 => new(_haContext, "light.jayden_2");
    public LightEntity Jayden3 => new(_haContext, "light.jayden_3");
    public LightEntity Jayden4 => new(_haContext, "light.jayden_4");
    public LightEntity JaydenAlarm => new(_haContext, "light.jayden_alarm");
    public LightEntity Kitchen => new(_haContext, "light.kitchen");
    public LightEntity Kitchen1 => new(_haContext, "light.kitchen_1");
    public LightEntity Kitchen2 => new(_haContext, "light.kitchen_2");
    public LightEntity Kitchen3 => new(_haContext, "light.kitchen_3");
    public LightEntity Kitchen4 => new(_haContext, "light.kitchen_4");
    public LightEntity Kitchen5 => new(_haContext, "light.kitchen_5");
    public LightEntity Kitchen6 => new(_haContext, "light.kitchen_6");
    public LightEntity Landing => new(_haContext, "light.landing");
    public LightEntity Landing1 => new(_haContext, "light.landing_1");
    public LightEntity LandingDay => new(_haContext, "light.landing_day");
    public LightEntity LandingNight => new(_haContext, "light.landing_night");
    public LightEntity Lounge => new(_haContext, "light.lounge");
    public LightEntity LoungeBackLevelLightColorOnOff => new(_haContext, "light.lounge_back_level_light_color_on_off");
    public LightEntity LoungeFrontLevelLightColorOnOff => new(_haContext, "light.lounge_front_level_light_color_on_off");
    public LightEntity Master => new(_haContext, "light.master");
    public LightEntity Master1 => new(_haContext, "light.master_1");
    public LightEntity Master2 => new(_haContext, "light.master_2");
    public LightEntity Master3 => new(_haContext, "light.master_3");
    public LightEntity Master4 => new(_haContext, "light.master_4");
    public LightEntity Master5 => new(_haContext, "light.master_5");
    public LightEntity NiemandDriveLight => new(_haContext, "light.niemand_drive_light");
    public LightEntity NiemandDriveLight2 => new(_haContext, "light.niemand_drive_light_2");
    public LightEntity NiemandGardenLight => new(_haContext, "light.niemand_garden_light");
    public LightEntity NiemandGardenLight2 => new(_haContext, "light.niemand_garden_light_2");
    public LightEntity Office => new(_haContext, "light.office");
    public LightEntity Office1 => new(_haContext, "light.office_1");
    public LightEntity Office3 => new(_haContext, "light.office_3");
    public LightEntity Outside => new(_haContext, "light.outside");
    public LightEntity OutsideBack => new(_haContext, "light.outside_back");
    public LightEntity OutsideDrive => new(_haContext, "light.outside_drive");
    public LightEntity OutsideFront => new(_haContext, "light.outside_front");
    public LightEntity OutsideGarage => new(_haContext, "light.outside_garage");
    public LightEntity OutsideGardenFlood => new(_haContext, "light.outside_garden_flood");
    public LightEntity Playroom => new(_haContext, "light.playroom");
    public LightEntity Porch => new(_haContext, "light.porch");
    public LightEntity Toilet => new(_haContext, "light.toilet");
    public LightEntity Upstairs => new(_haContext, "light.upstairs");
    public LightEntity Utility => new(_haContext, "light.utility");
    public LightEntity Utility1 => new(_haContext, "light.utility_1");
    public LightEntity Utility2 => new(_haContext, "light.utility_2");
    public LightEntity Utility3 => new(_haContext, "light.utility_3");
}

public class LockEntities
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public LockEntities(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public LockEntity AudiQ7DoorLock => new(_haContext, "lock.audi_q7_door_lock");
}

public class MediaPlayerEntities
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public MediaPlayerEntities(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public MediaPlayerEntity Aaron => new(_haContext, "media_player.aaron");
    public MediaPlayerEntity BedroomAppleTv => new(_haContext, "media_player.bedroom_apple_tv");
    public MediaPlayerEntity BedroomAppleTv2 => new(_haContext, "media_player.bedroom_apple_tv_2");
    public MediaPlayerEntity Dining => new(_haContext, "media_player.dining");
    public MediaPlayerEntity Downstairs => new(_haContext, "media_player.downstairs");
    public MediaPlayerEntity EugeneS2ndEchoDot => new(_haContext, "media_player.eugene_s_2nd_echo_dot");
    public MediaPlayerEntity EugeneS5thEchoDot => new(_haContext, "media_player.eugene_s_5th_echo_dot");
    public MediaPlayerEntity EugeneSFire => new(_haContext, "media_player.eugene_s_fire");
    public MediaPlayerEntity EugeneSLgOledWebos2021Tv => new(_haContext, "media_player.eugene_s_lg_oled_webos_2021_tv");
    public MediaPlayerEntity EugeneSSonosArc => new(_haContext, "media_player.eugene_s_sonos_arc");
    public MediaPlayerEntity Everywhere2 => new(_haContext, "media_player.everywhere_2");
    public MediaPlayerEntity Jayden => new(_haContext, "media_player.jayden");
    public MediaPlayerEntity Kitchen => new(_haContext, "media_player.kitchen");
    public MediaPlayerEntity LivingRoom => new(_haContext, "media_player.living_room");
    public MediaPlayerEntity LivingRoom3 => new(_haContext, "media_player.living_room_3");
    public MediaPlayerEntity Lounge => new(_haContext, "media_player.lounge");
    public MediaPlayerEntity LoungeGroup => new(_haContext, "media_player.lounge_group");
    public MediaPlayerEntity LoungeSonos => new(_haContext, "media_player.lounge_sonos");
    public MediaPlayerEntity LoungeTv => new(_haContext, "media_player.lounge_tv");
    public MediaPlayerEntity Master => new(_haContext, "media_player.master");
    public MediaPlayerEntity MasterTv2 => new(_haContext, "media_player.master_tv_2");
    public MediaPlayerEntity MasterTvAlexa => new(_haContext, "media_player.master_tv_alexa");
    public MediaPlayerEntity Office => new(_haContext, "media_player.office");
    public MediaPlayerEntity Playroom => new(_haContext, "media_player.playroom");
    public MediaPlayerEntity ThisDevice => new(_haContext, "media_player.this_device");
    public MediaPlayerEntity ThisDevice2 => new(_haContext, "media_player.this_device_2");
    public MediaPlayerEntity Upstairs => new(_haContext, "media_player.upstairs");
}

public class NumberEntities
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public NumberEntities(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public NumberEntity DownstairsSnoozeMinutes => new(_haContext, "number.downstairs_snooze_minutes");
    public NumberEntity DownstairsVolume => new(_haContext, "number.downstairs_volume");
    public NumberEntity NiemandDriveSnapshotInterval => new(_haContext, "number.niemand_drive_snapshot_interval");
    public NumberEntity NiemandFrontDoorSnapshotInterval => new(_haContext, "number.niemand_front_door_snapshot_interval");
    public NumberEntity NiemandGarageSnapshotInterval => new(_haContext, "number.niemand_garage_snapshot_interval");
    public NumberEntity NiemandGardenSnapshotInterval => new(_haContext, "number.niemand_garden_snapshot_interval");
    public NumberEntity NiemandSideSnapshotInterval => new(_haContext, "number.niemand_side_snapshot_interval");
    public NumberEntity RingChimeSnoozeMinutes => new(_haContext, "number.ring_chime_snooze_minutes");
    public NumberEntity RingChimeVolume => new(_haContext, "number.ring_chime_volume");
}

public class OctopusagileEntities
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public OctopusagileEntities(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public OctopusagileEntity AllRates => new(_haContext, "octopusagile.all_rates");
    public OctopusagileEntity AvgRateExcPeak => new(_haContext, "octopusagile.avg_rate_exc_peak");
    public OctopusagileEntity AvgRateIncPeak => new(_haContext, "octopusagile.avg_rate_inc_peak");
    public OctopusagileEntity HalfHourTimerLastupdate => new(_haContext, "octopusagile.half_hour_timer_lastupdate");
    public OctopusagileEntity HalfHourTimerNextupdate => new(_haContext, "octopusagile.half_hour_timer_nextupdate");
    public OctopusagileEntity MonthlyCost => new(_haContext, "octopusagile.monthly_cost");
    public OctopusagileEntity MonthlyUsage => new(_haContext, "octopusagile.monthly_usage");
    public OctopusagileEntity Rates => new(_haContext, "octopusagile.rates");
    public OctopusagileEntity RegionCode => new(_haContext, "octopusagile.region_code");
    public OctopusagileEntity Startdate => new(_haContext, "octopusagile.startdate");
    public OctopusagileEntity Timers => new(_haContext, "octopusagile.timers");
    public OctopusagileEntity UpdateConsumptionLastupdate => new(_haContext, "octopusagile.update_consumption_lastupdate");
    public OctopusagileEntity UpdateConsumptionNextupdate => new(_haContext, "octopusagile.update_consumption_nextupdate");
    public OctopusagileEntity UpdateTimersLastupdate => new(_haContext, "octopusagile.update_timers_lastupdate");
    public OctopusagileEntity UpdateTimersNextupdate => new(_haContext, "octopusagile.update_timers_nextupdate");
    public OctopusagileEntity YearlyCost => new(_haContext, "octopusagile.yearly_cost");
    public OctopusagileEntity YearlyUsage => new(_haContext, "octopusagile.yearly_usage");
}

public class PersonEntities
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public PersonEntities(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public PersonEntity Aubrecia => new(_haContext, "person.aubrecia");
    public PersonEntity Eugene => new(_haContext, "person.eugene");
    public PersonEntity Hailey => new(_haContext, "person.hailey");
    public PersonEntity Netdaemon => new(_haContext, "person.netdaemon");
}

public class RemoteEntities
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public RemoteEntities(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public RemoteEntity BedroomAppleTv => new(_haContext, "remote.bedroom_apple_tv");
    public RemoteEntity BedroomAppleTv2 => new(_haContext, "remote.bedroom_apple_tv_2");
    public RemoteEntity LivingRoom => new(_haContext, "remote.living_room");
    public RemoteEntity LivingRoom2 => new(_haContext, "remote.living_room_2");
    public RemoteEntity LivingRoom3 => new(_haContext, "remote.living_room_3");
    public RemoteEntity RmMini3Remote => new(_haContext, "remote.rm_mini_3_remote");
}

public class ScriptEntities
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public ScriptEntities(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public ScriptEntity ActivateAlexaActionableNotification => new(_haContext, "script.activate_alexa_actionable_notification");
    public ScriptEntity ArriveHome => new(_haContext, "script.arrive_home");
    public ScriptEntity ImText => new(_haContext, "script.im_text");
    public ScriptEntity RingMqttInterval => new(_haContext, "script.ring_mqtt_interval");
    public ScriptEntity Tts => new(_haContext, "script.tts");
    public ScriptEntity TtsText => new(_haContext, "script.tts_text");
    public ScriptEntity Weather => new(_haContext, "script.weather");
}

public class SelectEntities
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public SelectEntities(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public SelectEntity NiemandDriveEventSelect => new(_haContext, "select.niemand_drive_event_select");
    public SelectEntity NiemandFrontDoorEventSelect => new(_haContext, "select.niemand_front_door_event_select");
    public SelectEntity NiemandGarageEventSelect => new(_haContext, "select.niemand_garage_event_select");
    public SelectEntity NiemandGardenEventSelect => new(_haContext, "select.niemand_garden_event_select");
    public SelectEntity NiemandSideEventSelect => new(_haContext, "select.niemand_side_event_select");
}

public class SensorEntities
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public SensorEntities(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public SensorEntity E247d4d7d6c90MysimplelinkRx => new(_haContext, "sensor.247d4d7d6c90_mysimplelink_rx");
    public SensorEntity E247d4d7d6c90MysimplelinkTx => new(_haContext, "sensor.247d4d7d6c90_mysimplelink_tx");
    public SensorEntity E247d4d7d6c90MysimplelinkUptime => new(_haContext, "sensor.247d4d7d6c90_mysimplelink_uptime");
    public SensorEntity Aaron1LightLastSeen => new(_haContext, "sensor.aaron_1_light_last_seen");
    public SensorEntity Aaron2LightLastSeen => new(_haContext, "sensor.aaron_2_light_last_seen");
    public SensorEntity Aaron3LightLastSeen => new(_haContext, "sensor.aaron_3_light_last_seen");
    public SensorEntity Aaron4LightLastSeen => new(_haContext, "sensor.aaron_4_light_last_seen");
    public SensorEntity AaronEchoRx => new(_haContext, "sensor.aaron_echo_rx");
    public SensorEntity AaronEchoTx => new(_haContext, "sensor.aaron_echo_tx");
    public SensorEntity AaronEchoUptime => new(_haContext, "sensor.aaron_echo_uptime");
    public SensorEntity AaronLux => new(_haContext, "sensor.aaron_lux");
    public SensorEntity AaronMotionBattery => new(_haContext, "sensor.aaron_motion_battery");
    public SensorEntity AaronMotionLastSeen => new(_haContext, "sensor.aaron_motion_last_seen");
    public SensorEntity AaronNextAlarm => new(_haContext, "sensor.aaron_next_alarm");
    public SensorEntity AaronNextReminder => new(_haContext, "sensor.aaron_next_reminder");
    public SensorEntity AaronNextTimer => new(_haContext, "sensor.aaron_next_timer");
    public SensorEntity AccuweatherHomeCloudCeiling => new(_haContext, "sensor.accuweather_home_cloud_ceiling");
    public SensorEntity AccuweatherHomeHoursOfSun0d => new(_haContext, "sensor.accuweather_home_hours_of_sun_0d");
    public SensorEntity AccuweatherHomeHoursOfSun1d => new(_haContext, "sensor.accuweather_home_hours_of_sun_1d");
    public SensorEntity AccuweatherHomeHoursOfSun2d => new(_haContext, "sensor.accuweather_home_hours_of_sun_2d");
    public SensorEntity AccuweatherHomeHoursOfSun3d => new(_haContext, "sensor.accuweather_home_hours_of_sun_3d");
    public SensorEntity AccuweatherHomeHoursOfSun4d => new(_haContext, "sensor.accuweather_home_hours_of_sun_4d");
    public SensorEntity AccuweatherHomePrecipitation => new(_haContext, "sensor.accuweather_home_precipitation");
    public SensorEntity AccuweatherHomePressureTendency => new(_haContext, "sensor.accuweather_home_pressure_tendency");
    public SensorEntity AccuweatherHomeRealfeelTemperature => new(_haContext, "sensor.accuweather_home_realfeel_temperature");
    public SensorEntity AccuweatherHomeRealfeelTemperatureMax0d => new(_haContext, "sensor.accuweather_home_realfeel_temperature_max_0d");
    public SensorEntity AccuweatherHomeRealfeelTemperatureMax1d => new(_haContext, "sensor.accuweather_home_realfeel_temperature_max_1d");
    public SensorEntity AccuweatherHomeRealfeelTemperatureMax2d => new(_haContext, "sensor.accuweather_home_realfeel_temperature_max_2d");
    public SensorEntity AccuweatherHomeRealfeelTemperatureMax3d => new(_haContext, "sensor.accuweather_home_realfeel_temperature_max_3d");
    public SensorEntity AccuweatherHomeRealfeelTemperatureMax4d => new(_haContext, "sensor.accuweather_home_realfeel_temperature_max_4d");
    public SensorEntity AccuweatherHomeRealfeelTemperatureMin0d => new(_haContext, "sensor.accuweather_home_realfeel_temperature_min_0d");
    public SensorEntity AccuweatherHomeRealfeelTemperatureMin1d => new(_haContext, "sensor.accuweather_home_realfeel_temperature_min_1d");
    public SensorEntity AccuweatherHomeRealfeelTemperatureMin2d => new(_haContext, "sensor.accuweather_home_realfeel_temperature_min_2d");
    public SensorEntity AccuweatherHomeRealfeelTemperatureMin3d => new(_haContext, "sensor.accuweather_home_realfeel_temperature_min_3d");
    public SensorEntity AccuweatherHomeRealfeelTemperatureMin4d => new(_haContext, "sensor.accuweather_home_realfeel_temperature_min_4d");
    public SensorEntity AccuweatherHomeThunderstormProbabilityDay0d => new(_haContext, "sensor.accuweather_home_thunderstorm_probability_day_0d");
    public SensorEntity AccuweatherHomeThunderstormProbabilityDay1d => new(_haContext, "sensor.accuweather_home_thunderstorm_probability_day_1d");
    public SensorEntity AccuweatherHomeThunderstormProbabilityDay2d => new(_haContext, "sensor.accuweather_home_thunderstorm_probability_day_2d");
    public SensorEntity AccuweatherHomeThunderstormProbabilityDay3d => new(_haContext, "sensor.accuweather_home_thunderstorm_probability_day_3d");
    public SensorEntity AccuweatherHomeThunderstormProbabilityDay4d => new(_haContext, "sensor.accuweather_home_thunderstorm_probability_day_4d");
    public SensorEntity AccuweatherHomeThunderstormProbabilityNight0d => new(_haContext, "sensor.accuweather_home_thunderstorm_probability_night_0d");
    public SensorEntity AccuweatherHomeThunderstormProbabilityNight1d => new(_haContext, "sensor.accuweather_home_thunderstorm_probability_night_1d");
    public SensorEntity AccuweatherHomeThunderstormProbabilityNight2d => new(_haContext, "sensor.accuweather_home_thunderstorm_probability_night_2d");
    public SensorEntity AccuweatherHomeThunderstormProbabilityNight3d => new(_haContext, "sensor.accuweather_home_thunderstorm_probability_night_3d");
    public SensorEntity AccuweatherHomeThunderstormProbabilityNight4d => new(_haContext, "sensor.accuweather_home_thunderstorm_probability_night_4d");
    public SensorEntity AccuweatherHomeUvIndex => new(_haContext, "sensor.accuweather_home_uv_index");
    public SensorEntity AccuweatherHomeUvIndex0d => new(_haContext, "sensor.accuweather_home_uv_index_0d");
    public SensorEntity AccuweatherHomeUvIndex1d => new(_haContext, "sensor.accuweather_home_uv_index_1d");
    public SensorEntity AccuweatherHomeUvIndex2d => new(_haContext, "sensor.accuweather_home_uv_index_2d");
    public SensorEntity AccuweatherHomeUvIndex3d => new(_haContext, "sensor.accuweather_home_uv_index_3d");
    public SensorEntity AccuweatherHomeUvIndex4d => new(_haContext, "sensor.accuweather_home_uv_index_4d");
    public SensorEntity AccuweatherHomeWind => new(_haContext, "sensor.accuweather_home_wind");
    public SensorEntity AccuweatherHomeWindDay0d => new(_haContext, "sensor.accuweather_home_wind_day_0d");
    public SensorEntity AccuweatherHomeWindDay1d => new(_haContext, "sensor.accuweather_home_wind_day_1d");
    public SensorEntity AccuweatherHomeWindDay2d => new(_haContext, "sensor.accuweather_home_wind_day_2d");
    public SensorEntity AccuweatherHomeWindDay3d => new(_haContext, "sensor.accuweather_home_wind_day_3d");
    public SensorEntity AccuweatherHomeWindDay4d => new(_haContext, "sensor.accuweather_home_wind_day_4d");
    public SensorEntity AccuweatherHomeWindNight0d => new(_haContext, "sensor.accuweather_home_wind_night_0d");
    public SensorEntity AccuweatherHomeWindNight1d => new(_haContext, "sensor.accuweather_home_wind_night_1d");
    public SensorEntity AccuweatherHomeWindNight2d => new(_haContext, "sensor.accuweather_home_wind_night_2d");
    public SensorEntity AccuweatherHomeWindNight3d => new(_haContext, "sensor.accuweather_home_wind_night_3d");
    public SensorEntity AccuweatherHomeWindNight4d => new(_haContext, "sensor.accuweather_home_wind_night_4d");
    public SensorEntity AlexaActionableNotificationPrompt => new(_haContext, "sensor.alexa_actionable_notification_prompt");
    public SensorEntity AsglhWl19140Rx => new(_haContext, "sensor.asglh_wl_19140_rx");
    public SensorEntity AsglhWl19140Tx => new(_haContext, "sensor.asglh_wl_19140_tx");
    public SensorEntity AsglhWl19140Uptime => new(_haContext, "sensor.asglh_wl_19140_uptime");
    public SensorEntity AubreciaActivity => new(_haContext, "sensor.aubrecia_activity");
    public SensorEntity AubreciaAverageActivePace => new(_haContext, "sensor.aubrecia_average_active_pace");
    public SensorEntity AubreciaBatteryLevel => new(_haContext, "sensor.aubrecia_battery_level");
    public SensorEntity AubreciaBatteryState => new(_haContext, "sensor.aubrecia_battery_state");
    public SensorEntity AubreciaBssid => new(_haContext, "sensor.aubrecia_bssid");
    public SensorEntity AubreciaConnectionType => new(_haContext, "sensor.aubrecia_connection_type");
    public SensorEntity AubreciaDistance => new(_haContext, "sensor.aubrecia_distance");
    public SensorEntity AubreciaFloorsAscended => new(_haContext, "sensor.aubrecia_floors_ascended");
    public SensorEntity AubreciaFloorsDescended => new(_haContext, "sensor.aubrecia_floors_descended");
    public SensorEntity AubreciaFrontDoorBattery => new(_haContext, "sensor.aubrecia_front_door_battery");
    public SensorEntity AubreciaFrontDoorLastActivity => new(_haContext, "sensor.aubrecia_front_door_last_activity");
    public SensorEntity AubreciaFrontDoorLastDing => new(_haContext, "sensor.aubrecia_front_door_last_ding");
    public SensorEntity AubreciaFrontDoorLastMotion => new(_haContext, "sensor.aubrecia_front_door_last_motion");
    public SensorEntity AubreciaFrontDoorVolume => new(_haContext, "sensor.aubrecia_front_door_volume");
    public SensorEntity AubreciaGeocodedLocation => new(_haContext, "sensor.aubrecia_geocoded_location");
    public SensorEntity AubreciaLastUpdateTrigger => new(_haContext, "sensor.aubrecia_last_update_trigger");
    public SensorEntity AubreciaSim1 => new(_haContext, "sensor.aubrecia_sim_1");
    public SensorEntity AubreciaSim2 => new(_haContext, "sensor.aubrecia_sim_2");
    public SensorEntity AubreciaSsid => new(_haContext, "sensor.aubrecia_ssid");
    public SensorEntity AubreciaSteps => new(_haContext, "sensor.aubrecia_steps");
    public SensorEntity AubreciaStorage => new(_haContext, "sensor.aubrecia_storage");
    public SensorEntity AubreciasiphoneRx => new(_haContext, "sensor.aubreciasiphone_rx");
    public SensorEntity AubreciasiphoneTx => new(_haContext, "sensor.aubreciasiphone_tx");
    public SensorEntity AubreciasiphoneUptime => new(_haContext, "sensor.aubreciasiphone_uptime");
    public SensorEntity AudiQ7DoorsBootState => new(_haContext, "sensor.audi_q7_doors_boot_state");
    public SensorEntity AudiQ7LastUpdate => new(_haContext, "sensor.audi_q7_last_update");
    public SensorEntity AudiQ7MileageKm => new(_haContext, "sensor.audi_q7_mileage_km");
    public SensorEntity AudiQ7MileageMi => new(_haContext, "sensor.audi_q7_mileage_mi");
    public SensorEntity AudiQ7Model => new(_haContext, "sensor.audi_q7_model");
    public SensorEntity AudiQ7OilChangeDistanceKm => new(_haContext, "sensor.audi_q7_oil_change_distance_km");
    public SensorEntity AudiQ7OilChangeDistanceMi => new(_haContext, "sensor.audi_q7_oil_change_distance_mi");
    public SensorEntity AudiQ7OilChangeTime => new(_haContext, "sensor.audi_q7_oil_change_time");
    public SensorEntity AudiQ7OilLevel => new(_haContext, "sensor.audi_q7_oil_level");
    public SensorEntity AudiQ7RangeKm => new(_haContext, "sensor.audi_q7_range_km");
    public SensorEntity AudiQ7RangeMi => new(_haContext, "sensor.audi_q7_range_mi");
    public SensorEntity AudiQ7ServiceInspectionDistanceKm => new(_haContext, "sensor.audi_q7_service_inspection_distance_km");
    public SensorEntity AudiQ7ServiceInspectionDistanceMi => new(_haContext, "sensor.audi_q7_service_inspection_distance_mi");
    public SensorEntity AudiQ7ServiceInspectionTime => new(_haContext, "sensor.audi_q7_service_inspection_time");
    public SensorEntity AudiQ7TankLevel => new(_haContext, "sensor.audi_q7_tank_level");
    public SensorEntity AverageLuxDownstairs => new(_haContext, "sensor.average_lux_downstairs");
    public SensorEntity AverageLuxUpstairs => new(_haContext, "sensor.average_lux_upstairs");
    public SensorEntity AveragePingKonnectedAddonCount => new(_haContext, "sensor.average_ping_konnected_addon_count");
    public SensorEntity AveragePingKonnectedAddonRatio => new(_haContext, "sensor.average_ping_konnected_addon_ratio");
    public SensorEntity AveragePingKonnectedGoogleCount => new(_haContext, "sensor.average_ping_konnected_google_count");
    public SensorEntity AveragePingKonnectedGoogleRatio => new(_haContext, "sensor.average_ping_konnected_google_ratio");
    public SensorEntity AveragePingKonnectedMainCount => new(_haContext, "sensor.average_ping_konnected_main_count");
    public SensorEntity AveragePingKonnectedMainRatio => new(_haContext, "sensor.average_ping_konnected_main_ratio");
    public SensorEntity BathroomLux => new(_haContext, "sensor.bathroom_lux");
    public SensorEntity BathroomMotionBattery => new(_haContext, "sensor.bathroom_motion_battery");
    public SensorEntity BathroomMotionLastSeen => new(_haContext, "sensor.bathroom_motion_last_seen");
    public SensorEntity Bedroom1AcRx => new(_haContext, "sensor.bedroom_1_ac_rx");
    public SensorEntity Bedroom1AcTx => new(_haContext, "sensor.bedroom_1_ac_tx");
    public SensorEntity Bedroom1AcUptime => new(_haContext, "sensor.bedroom_1_ac_uptime");
    public SensorEntity Bedroom1Energy => new(_haContext, "sensor.bedroom_1_energy");
    public SensorEntity Bedroom1RoomTemperature => new(_haContext, "sensor.bedroom_1_room_temperature");
    public SensorEntity Bedroom2AcRx => new(_haContext, "sensor.bedroom_2_ac_rx");
    public SensorEntity Bedroom2AcTx => new(_haContext, "sensor.bedroom_2_ac_tx");
    public SensorEntity Bedroom2AcUptime => new(_haContext, "sensor.bedroom_2_ac_uptime");
    public SensorEntity Bedroom2Energy => new(_haContext, "sensor.bedroom_2_energy");
    public SensorEntity Bedroom2RoomTemperature => new(_haContext, "sensor.bedroom_2_room_temperature");
    public SensorEntity Bedroom3AcRx => new(_haContext, "sensor.bedroom_3_ac_rx");
    public SensorEntity Bedroom3AcTx => new(_haContext, "sensor.bedroom_3_ac_tx");
    public SensorEntity Bedroom3AcUptime => new(_haContext, "sensor.bedroom_3_ac_uptime");
    public SensorEntity Bedroom3Energy => new(_haContext, "sensor.bedroom_3_energy");
    public SensorEntity Bedroom3RoomTemperature => new(_haContext, "sensor.bedroom_3_room_temperature");
    public SensorEntity Bedroom4AcRx => new(_haContext, "sensor.bedroom_4_ac_rx");
    public SensorEntity Bedroom4AcTx => new(_haContext, "sensor.bedroom_4_ac_tx");
    public SensorEntity Bedroom4AcUptime => new(_haContext, "sensor.bedroom_4_ac_uptime");
    public SensorEntity Bedroom4Energy => new(_haContext, "sensor.bedroom_4_energy");
    public SensorEntity Bedroom4RoomTemperature => new(_haContext, "sensor.bedroom_4_room_temperature");
    public SensorEntity BlindLoungeEnergy => new(_haContext, "sensor.blind_lounge_energy");
    public SensorEntity ChristmasIndoor1558Rx => new(_haContext, "sensor.christmas_indoor_1558_rx");
    public SensorEntity ChristmasIndoor1558Tx => new(_haContext, "sensor.christmas_indoor_1558_tx");
    public SensorEntity ChristmasIndoor1558Uptime => new(_haContext, "sensor.christmas_indoor_1558_uptime");
    public SensorEntity CircadianValues => new(_haContext, "sensor.circadian_values");
    public SensorEntity DesktopIpurn8tRx => new(_haContext, "sensor.desktop_ipurn8t_rx");
    public SensorEntity DesktopIpurn8tTx => new(_haContext, "sensor.desktop_ipurn8t_tx");
    public SensorEntity DesktopIpurn8tUptime => new(_haContext, "sensor.desktop_ipurn8t_uptime");
    public SensorEntity DiningDoorLastSeen => new(_haContext, "sensor.dining_door_last_seen");
    public SensorEntity DiningEchoRx => new(_haContext, "sensor.dining_echo_rx");
    public SensorEntity DiningEchoTx => new(_haContext, "sensor.dining_echo_tx");
    public SensorEntity DiningEchoUptime => new(_haContext, "sensor.dining_echo_uptime");
    public SensorEntity DiningLux => new(_haContext, "sensor.dining_lux");
    public SensorEntity DiningMotionBattery => new(_haContext, "sensor.dining_motion_battery");
    public SensorEntity DiningMotionLastSeen => new(_haContext, "sensor.dining_motion_last_seen");
    public SensorEntity DiningRx => new(_haContext, "sensor.dining_rx");
    public SensorEntity DiningTx => new(_haContext, "sensor.dining_tx");
    public SensorEntity DiningUptime => new(_haContext, "sensor.dining_uptime");
    public SensorEntity DiningWall1LightLastSeen => new(_haContext, "sensor.dining_wall_1_light_last_seen");
    public SensorEntity DiningWall2LightLastSeen => new(_haContext, "sensor.dining_wall_2_light_last_seen");
    public SensorEntity Dining1LightLastSeen => new(_haContext, "sensor.dining1_light_last_seen");
    public SensorEntity Dining2LightLastSeen => new(_haContext, "sensor.dining2_light_last_seen");
    public SensorEntity Dining3LightLastSeen => new(_haContext, "sensor.dining3_light_last_seen");
    public SensorEntity Dining4LightLastSeen => new(_haContext, "sensor.dining4_light_last_seen");
    public SensorEntity Dining5LightLastSeen => new(_haContext, "sensor.dining5_light_last_seen");
    public SensorEntity DishwasherEnergy => new(_haContext, "sensor.dishwasher_energy");
    public SensorEntity DishwasherPower => new(_haContext, "sensor.dishwasher_power");
    public SensorEntity DownstairsInfo => new(_haContext, "sensor.downstairs_info");
    public SensorEntity DownstairsVolume => new(_haContext, "sensor.downstairs_volume");
    public SensorEntity DownstairsWireless => new(_haContext, "sensor.downstairs_wireless");
    public SensorEntity DriveEnergy => new(_haContext, "sensor.drive_energy");
    public SensorEntity DriveEnergySpent => new(_haContext, "sensor.drive_energy_spent");
    public SensorEntity DriveMeter => new(_haContext, "sensor.drive_meter");
    public SensorEntity DrivePower => new(_haContext, "sensor.drive_power");
    public SensorEntity DriveSnapshotLastUpdated => new(_haContext, "sensor.drive_snapshot_last_updated");
    public SensorEntity DriveTemp => new(_haContext, "sensor.drive_temp");
    public SensorEntity DryerEnergy => new(_haContext, "sensor.dryer_energy");
    public SensorEntity DryerPower => new(_haContext, "sensor.dryer_power");
    public SensorEntity DryerRx => new(_haContext, "sensor.dryer_rx");
    public SensorEntity DryerTx => new(_haContext, "sensor.dryer_tx");
    public SensorEntity DryerUptime => new(_haContext, "sensor.dryer_uptime");
    public SensorEntity ElectricConsumptionToday => new(_haContext, "sensor.electric_consumption_today");
    public SensorEntity ElectricConsumptionYear => new(_haContext, "sensor.electric_consumption_year");
    public SensorEntity ElectricCostToday => new(_haContext, "sensor.electric_cost_today");
    public SensorEntity ElectricTariffRate => new(_haContext, "sensor.electric_tariff_rate");
    public SensorEntity ElectricTariffStanding => new(_haContext, "sensor.electric_tariff_standing");
    public SensorEntity Entrance1LightLastSeen => new(_haContext, "sensor.entrance_1_light_last_seen");
    public SensorEntity Entrance2LightLastSeen => new(_haContext, "sensor.entrance_2_light_last_seen");
    public SensorEntity EntranceLux => new(_haContext, "sensor.entrance_lux");
    public SensorEntity EntranceMotionBattery => new(_haContext, "sensor.entrance_motion_battery");
    public SensorEntity EntranceMotionLastSeen => new(_haContext, "sensor.entrance_motion_last_seen");
    public SensorEntity EntranceRx => new(_haContext, "sensor.entrance_rx");
    public SensorEntity EntranceTx => new(_haContext, "sensor.entrance_tx");
    public SensorEntity EntranceUptime => new(_haContext, "sensor.entrance_uptime");
    public SensorEntity Esp6b7081Rx => new(_haContext, "sensor.esp_6b7081_rx");
    public SensorEntity Esp6b7081Tx => new(_haContext, "sensor.esp_6b7081_tx");
    public SensorEntity Esp6b7081Uptime => new(_haContext, "sensor.esp_6b7081_uptime");
    public SensorEntity Esp6b7a3aRx => new(_haContext, "sensor.esp_6b7a3a_rx");
    public SensorEntity Esp6b7a3aTx => new(_haContext, "sensor.esp_6b7a3a_tx");
    public SensorEntity Esp6b7a3aUptime => new(_haContext, "sensor.esp_6b7a3a_uptime");
    public SensorEntity EufyRobovacRx => new(_haContext, "sensor.eufy_robovac_rx");
    public SensorEntity EufyRobovacRx2 => new(_haContext, "sensor.eufy_robovac_rx_2");
    public SensorEntity EufyRobovacTx => new(_haContext, "sensor.eufy_robovac_tx");
    public SensorEntity EufyRobovacTx2 => new(_haContext, "sensor.eufy_robovac_tx_2");
    public SensorEntity EufyRobovacUptime => new(_haContext, "sensor.eufy_robovac_uptime");
    public SensorEntity EufyRobovacUptime2 => new(_haContext, "sensor.eufy_robovac_uptime_2");
    public SensorEntity EugeneDesktopRx => new(_haContext, "sensor.eugene_desktop_rx");
    public SensorEntity EugeneDesktopTx => new(_haContext, "sensor.eugene_desktop_tx");
    public SensorEntity EugeneDesktopUptime => new(_haContext, "sensor.eugene_desktop_uptime");
    public SensorEntity EugeneS2ndEchoDotNextAlarm => new(_haContext, "sensor.eugene_s_2nd_echo_dot_next_alarm");
    public SensorEntity EugeneS2ndEchoDotNextReminder => new(_haContext, "sensor.eugene_s_2nd_echo_dot_next_reminder");
    public SensorEntity EugeneS2ndEchoDotNextTimer => new(_haContext, "sensor.eugene_s_2nd_echo_dot_next_timer");
    public SensorEntity EugeneS3rdEchoDotNextAlarm => new(_haContext, "sensor.eugene_s_3rd_echo_dot_next_alarm");
    public SensorEntity EugeneS3rdEchoDotNextReminder => new(_haContext, "sensor.eugene_s_3rd_echo_dot_next_reminder");
    public SensorEntity EugeneS3rdEchoDotNextTimer => new(_haContext, "sensor.eugene_s_3rd_echo_dot_next_timer");
    public SensorEntity EugeneSLgOledWebos2021TvNextAlarm => new(_haContext, "sensor.eugene_s_lg_oled_webos_2021_tv_next_alarm");
    public SensorEntity EugeneSLgOledWebos2021TvNextReminder => new(_haContext, "sensor.eugene_s_lg_oled_webos_2021_tv_next_reminder");
    public SensorEntity EugeneSLgOledWebos2021TvNextTimer => new(_haContext, "sensor.eugene_s_lg_oled_webos_2021_tv_next_timer");
    public SensorEntity EugeneSLgWebos2020TvNextAlarm => new(_haContext, "sensor.eugene_s_lg_webos_2020_tv_next_alarm");
    public SensorEntity EugeneSLgWebos2020TvNextReminder => new(_haContext, "sensor.eugene_s_lg_webos_2020_tv_next_reminder");
    public SensorEntity EugeneSLgWebos2020TvNextTimer => new(_haContext, "sensor.eugene_s_lg_webos_2020_tv_next_timer");
    public SensorEntity EugeneSSonosArcNextAlarm => new(_haContext, "sensor.eugene_s_sonos_arc_next_alarm");
    public SensorEntity EugeneSSonosArcNextReminder => new(_haContext, "sensor.eugene_s_sonos_arc_next_reminder");
    public SensorEntity EugeneSSonosArcNextTimer => new(_haContext, "sensor.eugene_s_sonos_arc_next_timer");
    public SensorEntity EugenesIphoneActivity => new(_haContext, "sensor.eugenes_iphone_activity");
    public SensorEntity EugenesIphoneAverageActivePace => new(_haContext, "sensor.eugenes_iphone_average_active_pace");
    public SensorEntity EugenesIphoneBatteryLevel => new(_haContext, "sensor.eugenes_iphone_battery_level");
    public SensorEntity EugenesIphoneBatteryState => new(_haContext, "sensor.eugenes_iphone_battery_state");
    public SensorEntity EugenesIphoneBssid => new(_haContext, "sensor.eugenes_iphone_bssid");
    public SensorEntity EugenesIphoneConnectionType => new(_haContext, "sensor.eugenes_iphone_connection_type");
    public SensorEntity EugenesIphoneDistance => new(_haContext, "sensor.eugenes_iphone_distance");
    public SensorEntity EugenesIphoneFloorsAscended => new(_haContext, "sensor.eugenes_iphone_floors_ascended");
    public SensorEntity EugenesIphoneFloorsDescended => new(_haContext, "sensor.eugenes_iphone_floors_descended");
    public SensorEntity EugenesIphoneGeocodedLocation => new(_haContext, "sensor.eugenes_iphone_geocoded_location");
    public SensorEntity EugenesIphoneLastUpdateTrigger => new(_haContext, "sensor.eugenes_iphone_last_update_trigger");
    public SensorEntity EugenesIphoneRx => new(_haContext, "sensor.eugenes_iphone_rx");
    public SensorEntity EugenesIphoneSim1 => new(_haContext, "sensor.eugenes_iphone_sim_1");
    public SensorEntity EugenesIphoneSim2 => new(_haContext, "sensor.eugenes_iphone_sim_2");
    public SensorEntity EugenesIphoneSsid => new(_haContext, "sensor.eugenes_iphone_ssid");
    public SensorEntity EugenesIphoneSteps => new(_haContext, "sensor.eugenes_iphone_steps");
    public SensorEntity EugenesIphoneStorage => new(_haContext, "sensor.eugenes_iphone_storage");
    public SensorEntity EugenesIphoneTx => new(_haContext, "sensor.eugenes_iphone_tx");
    public SensorEntity EugenesIphoneUptime => new(_haContext, "sensor.eugenes_iphone_uptime");
    public SensorEntity EugenesplewatchRx => new(_haContext, "sensor.eugenesplewatch_rx");
    public SensorEntity EugenesplewatchTx => new(_haContext, "sensor.eugenesplewatch_tx");
    public SensorEntity EugenesplewatchUptime => new(_haContext, "sensor.eugenesplewatch_uptime");
    public SensorEntity FloorLight2086Rx => new(_haContext, "sensor.floor_light_2086_rx");
    public SensorEntity FloorLight2086Tx => new(_haContext, "sensor.floor_light_2086_tx");
    public SensorEntity FloorLight2086Uptime => new(_haContext, "sensor.floor_light_2086_uptime");
    public SensorEntity FloorLightLastSeen => new(_haContext, "sensor.floor_light_last_seen");
    public SensorEntity FoscamRx => new(_haContext, "sensor.foscam_rx");
    public SensorEntity FoscamTx => new(_haContext, "sensor.foscam_tx");
    public SensorEntity FoscamUptime => new(_haContext, "sensor.foscam_uptime");
    public SensorEntity FrontDoorSnapshotLastUpdated => new(_haContext, "sensor.front_door_snapshot_last_updated");
    public SensorEntity GalaxyS8Rx => new(_haContext, "sensor.galaxy_s8_rx");
    public SensorEntity GalaxyS8Tx => new(_haContext, "sensor.galaxy_s8_tx");
    public SensorEntity GalaxyS8Uptime => new(_haContext, "sensor.galaxy_s8_uptime");
    public SensorEntity GarageBackDoorLastSeen => new(_haContext, "sensor.garage_back_door_last_seen");
    public SensorEntity GarageEchoRx => new(_haContext, "sensor.garage_echo_rx");
    public SensorEntity GarageEchoTx => new(_haContext, "sensor.garage_echo_tx");
    public SensorEntity GarageEchoUptime => new(_haContext, "sensor.garage_echo_uptime");
    public SensorEntity GarageNextAlarm => new(_haContext, "sensor.garage_next_alarm");
    public SensorEntity GarageNextReminder => new(_haContext, "sensor.garage_next_reminder");
    public SensorEntity GarageNextTimer => new(_haContext, "sensor.garage_next_timer");
    public SensorEntity GarageSnapshotLastUpdated => new(_haContext, "sensor.garage_snapshot_last_updated");
    public SensorEntity GardenBattery2 => new(_haContext, "sensor.garden_battery_2");
    public SensorEntity GardenFloodlightsRx => new(_haContext, "sensor.garden_floodlights_rx");
    public SensorEntity GardenFloodlightsTx => new(_haContext, "sensor.garden_floodlights_tx");
    public SensorEntity GardenFloodlightsUptime => new(_haContext, "sensor.garden_floodlights_uptime");
    public SensorEntity GardenLastActivity2 => new(_haContext, "sensor.garden_last_activity_2");
    public SensorEntity GardenLastMotion2 => new(_haContext, "sensor.garden_last_motion_2");
    public SensorEntity GardenSnapshotLastUpdated => new(_haContext, "sensor.garden_snapshot_last_updated");
    public SensorEntity GardenVolume2 => new(_haContext, "sensor.garden_volume_2");
    public SensorEntity GasConsumptionToday => new(_haContext, "sensor.gas_consumption_today");
    public SensorEntity GasConsumptionYear => new(_haContext, "sensor.gas_consumption_year");
    public SensorEntity GasCostToday => new(_haContext, "sensor.gas_cost_today");
    public SensorEntity GasTariffRate => new(_haContext, "sensor.gas_tariff_rate");
    public SensorEntity GasTariffStanding => new(_haContext, "sensor.gas_tariff_standing");
    public SensorEntity Hacs => new(_haContext, "sensor.hacs");
    public SensorEntity HaileySIphoneActivity => new(_haContext, "sensor.hailey_s_iphone_activity");
    public SensorEntity HaileySIphoneAverageActivePace => new(_haContext, "sensor.hailey_s_iphone_average_active_pace");
    public SensorEntity HaileySIphoneBatteryLevel => new(_haContext, "sensor.hailey_s_iphone_battery_level");
    public SensorEntity HaileySIphoneBatteryState => new(_haContext, "sensor.hailey_s_iphone_battery_state");
    public SensorEntity HaileySIphoneBssid => new(_haContext, "sensor.hailey_s_iphone_bssid");
    public SensorEntity HaileySIphoneConnectionType => new(_haContext, "sensor.hailey_s_iphone_connection_type");
    public SensorEntity HaileySIphoneDistance => new(_haContext, "sensor.hailey_s_iphone_distance");
    public SensorEntity HaileySIphoneFloorsAscended => new(_haContext, "sensor.hailey_s_iphone_floors_ascended");
    public SensorEntity HaileySIphoneFloorsDescended => new(_haContext, "sensor.hailey_s_iphone_floors_descended");
    public SensorEntity HaileySIphoneGeocodedLocation => new(_haContext, "sensor.hailey_s_iphone_geocoded_location");
    public SensorEntity HaileySIphoneLastUpdateTrigger => new(_haContext, "sensor.hailey_s_iphone_last_update_trigger");
    public SensorEntity HaileySIphoneSim1 => new(_haContext, "sensor.hailey_s_iphone_sim_1");
    public SensorEntity HaileySIphoneSim2 => new(_haContext, "sensor.hailey_s_iphone_sim_2");
    public SensorEntity HaileySIphoneSsid => new(_haContext, "sensor.hailey_s_iphone_ssid");
    public SensorEntity HaileySIphoneSteps => new(_haContext, "sensor.hailey_s_iphone_steps");
    public SensorEntity HaileySIphoneStorage => new(_haContext, "sensor.hailey_s_iphone_storage");
    public SensorEntity HaileysAirRx => new(_haContext, "sensor.haileys_air_rx");
    public SensorEntity HaileysAirTx => new(_haContext, "sensor.haileys_air_tx");
    public SensorEntity HaileysAirUptime => new(_haContext, "sensor.haileys_air_uptime");
    public SensorEntity HaileysIphoneRx => new(_haContext, "sensor.haileys_iphone_rx");
    public SensorEntity HaileysIphoneTx => new(_haContext, "sensor.haileys_iphone_tx");
    public SensorEntity HaileysIphoneUptime => new(_haContext, "sensor.haileys_iphone_uptime");
    public SensorEntity HaileysMacbookAirActiveCamera => new(_haContext, "sensor.haileys_macbook_air_active_camera");
    public SensorEntity HaileysMacbookAirActiveMicrophone => new(_haContext, "sensor.haileys_macbook_air_active_microphone");
    public SensorEntity HaileysMacbookAirBssid => new(_haContext, "sensor.haileys_macbook_air_bssid");
    public SensorEntity HaileysMacbookAirConnectionType => new(_haContext, "sensor.haileys_macbook_air_connection_type");
    public SensorEntity HaileysMacbookAirDisplays => new(_haContext, "sensor.haileys_macbook_air_displays");
    public SensorEntity HaileysMacbookAirFrontmostApp => new(_haContext, "sensor.haileys_macbook_air_frontmost_app");
    public SensorEntity HaileysMacbookAirGeocodedLocation => new(_haContext, "sensor.haileys_macbook_air_geocoded_location");
    public SensorEntity HaileysMacbookAirInternalBatteryLevel => new(_haContext, "sensor.haileys_macbook_air_internal_battery_level");
    public SensorEntity HaileysMacbookAirInternalBatteryState => new(_haContext, "sensor.haileys_macbook_air_internal_battery_state");
    public SensorEntity HaileysMacbookAirLastUpdateTrigger => new(_haContext, "sensor.haileys_macbook_air_last_update_trigger");
    public SensorEntity HaileysMacbookAirPrimaryDisplayId => new(_haContext, "sensor.haileys_macbook_air_primary_display_id");
    public SensorEntity HaileysMacbookAirPrimaryDisplayName => new(_haContext, "sensor.haileys_macbook_air_primary_display_name");
    public SensorEntity HaileysMacbookAirSsid => new(_haContext, "sensor.haileys_macbook_air_ssid");
    public SensorEntity HaileysMacbookAirStorage => new(_haContext, "sensor.haileys_macbook_air_storage");
    public SensorEntity HpColorLaserjet4500HpijsPcl331812 => new(_haContext, "sensor.hp_color_laserjet_4500_hpijs_pcl3_3_18_12");
    public SensorEntity HuaweiPSmart201986203Rx => new(_haContext, "sensor.huawei_p_smart_2019_86203_rx");
    public SensorEntity HuaweiPSmart201986203Tx => new(_haContext, "sensor.huawei_p_smart_2019_86203_tx");
    public SensorEntity HuaweiPSmart201986203Uptime => new(_haContext, "sensor.huawei_p_smart_2019_86203_uptime");
    public SensorEntity IkeaOfSwedenTradfriRemoteControl580e51fePower => new(_haContext, "sensor.ikea_of_sweden_tradfri_remote_control_580e51fe_power");
    public SensorEntity IkeaOfSwedenTradfriRemoteControlD73648fePower => new(_haContext, "sensor.ikea_of_sweden_tradfri_remote_control_d73648fe_power");
    public SensorEntity JaydenAppletvRx => new(_haContext, "sensor.jayden_appletv_rx");
    public SensorEntity JaydenAppletvTx => new(_haContext, "sensor.jayden_appletv_tx");
    public SensorEntity JaydenAppletvUptime => new(_haContext, "sensor.jayden_appletv_uptime");
    public SensorEntity JaydenBedside4734Rx => new(_haContext, "sensor.jayden_bedside_4734_rx");
    public SensorEntity JaydenBedside4734Tx => new(_haContext, "sensor.jayden_bedside_4734_tx");
    public SensorEntity JaydenBedside4734Uptime => new(_haContext, "sensor.jayden_bedside_4734_uptime");
    public SensorEntity JaydenEchoRx => new(_haContext, "sensor.jayden_echo_rx");
    public SensorEntity JaydenEchoTx => new(_haContext, "sensor.jayden_echo_tx");
    public SensorEntity JaydenEchoUptime => new(_haContext, "sensor.jayden_echo_uptime");
    public SensorEntity JaydenLux => new(_haContext, "sensor.jayden_lux");
    public SensorEntity JaydenMotionBattery => new(_haContext, "sensor.jayden_motion_battery");
    public SensorEntity JaydenMotionLastSeen => new(_haContext, "sensor.jayden_motion_last_seen");
    public SensorEntity JaydenNextAlarm => new(_haContext, "sensor.jayden_next_alarm");
    public SensorEntity JaydenNextAlarm2 => new(_haContext, "sensor.jayden_next_alarm_2");
    public SensorEntity JaydenNextReminder => new(_haContext, "sensor.jayden_next_reminder");
    public SensorEntity JaydenNextReminder2 => new(_haContext, "sensor.jayden_next_reminder_2");
    public SensorEntity JaydenNextTimer => new(_haContext, "sensor.jayden_next_timer");
    public SensorEntity JaydenNextTimer2 => new(_haContext, "sensor.jayden_next_timer_2");
    public SensorEntity JaydenRx => new(_haContext, "sensor.jayden_rx");
    public SensorEntity JaydenTx => new(_haContext, "sensor.jayden_tx");
    public SensorEntity JaydenUptime => new(_haContext, "sensor.jayden_uptime");
    public SensorEntity JohanFrontDoorBattery => new(_haContext, "sensor.johan_front_door_battery");
    public SensorEntity JohanFrontDoorLastActivity => new(_haContext, "sensor.johan_front_door_last_activity");
    public SensorEntity JohanFrontDoorLastDing => new(_haContext, "sensor.johan_front_door_last_ding");
    public SensorEntity JohanFrontDoorLastMotion => new(_haContext, "sensor.johan_front_door_last_motion");
    public SensorEntity JohanFrontDoorVolume => new(_haContext, "sensor.johan_front_door_volume");
    public SensorEntity KeTradfriOpenCloseRemote3dcb2efePower => new(_haContext, "sensor.ke_tradfri_open_close_remote_3dcb2efe_power");
    public SensorEntity Kitchen1LightLastSeen => new(_haContext, "sensor.kitchen_1_light_last_seen");
    public SensorEntity Kitchen2LightLastSeen => new(_haContext, "sensor.kitchen_2_light_last_seen");
    public SensorEntity Kitchen3LightLastSeen => new(_haContext, "sensor.kitchen_3_light_last_seen");
    public SensorEntity Kitchen4LightLastSeen => new(_haContext, "sensor.kitchen_4_light_last_seen");
    public SensorEntity Kitchen5LightLastSeen => new(_haContext, "sensor.kitchen_5_light_last_seen");
    public SensorEntity Kitchen6LightLastSeen => new(_haContext, "sensor.kitchen_6_light_last_seen");
    public SensorEntity KitchenEchoRx => new(_haContext, "sensor.kitchen_echo_rx");
    public SensorEntity KitchenEchoTx => new(_haContext, "sensor.kitchen_echo_tx");
    public SensorEntity KitchenEchoUptime => new(_haContext, "sensor.kitchen_echo_uptime");
    public SensorEntity KitchenLux => new(_haContext, "sensor.kitchen_lux");
    public SensorEntity KitchenMotionBattery => new(_haContext, "sensor.kitchen_motion_battery");
    public SensorEntity KitchenNextAlarm => new(_haContext, "sensor.kitchen_next_alarm");
    public SensorEntity KitchenNextReminder => new(_haContext, "sensor.kitchen_next_reminder");
    public SensorEntity KitchenNextTimer => new(_haContext, "sensor.kitchen_next_timer");
    public SensorEntity KonnectedAddonRx => new(_haContext, "sensor.konnected_addon_rx");
    public SensorEntity KonnectedAddonTx => new(_haContext, "sensor.konnected_addon_tx");
    public SensorEntity KonnectedAddonUptime => new(_haContext, "sensor.konnected_addon_uptime");
    public SensorEntity KonnectedMainRx => new(_haContext, "sensor.konnected_main_rx");
    public SensorEntity KonnectedMainTx => new(_haContext, "sensor.konnected_main_tx");
    public SensorEntity KonnectedMainUptime => new(_haContext, "sensor.konnected_main_uptime");
    public SensorEntity LandingBlindBattery => new(_haContext, "sensor.landing_blind_battery");
    public SensorEntity LandingBlindLink => new(_haContext, "sensor.landing_blind_link");
    public SensorEntity LandingBlindPower => new(_haContext, "sensor.landing_blind_power");
    public SensorEntity LandingLux => new(_haContext, "sensor.landing_lux");
    public SensorEntity LandingMotionBattery => new(_haContext, "sensor.landing_motion_battery");
    public SensorEntity LandingMotionLastSeen => new(_haContext, "sensor.landing_motion_last_seen");
    public SensorEntity LandingRx => new(_haContext, "sensor.landing_rx");
    public SensorEntity LandingTx => new(_haContext, "sensor.landing_tx");
    public SensorEntity LandingUptime => new(_haContext, "sensor.landing_uptime");
    public SensorEntity LgLoungeRx => new(_haContext, "sensor.lg_lounge_rx");
    public SensorEntity LgLoungeTx => new(_haContext, "sensor.lg_lounge_tx");
    public SensorEntity LgLoungeUptime => new(_haContext, "sensor.lg_lounge_uptime");
    public SensorEntity LivingRoomRx => new(_haContext, "sensor.living_room_rx");
    public SensorEntity LivingRoomTx => new(_haContext, "sensor.living_room_tx");
    public SensorEntity LivingRoomUptime => new(_haContext, "sensor.living_room_uptime");
    public SensorEntity LoungeAcRx => new(_haContext, "sensor.lounge_ac_rx");
    public SensorEntity LoungeAcTx => new(_haContext, "sensor.lounge_ac_tx");
    public SensorEntity LoungeAcUptime => new(_haContext, "sensor.lounge_ac_uptime");
    public SensorEntity LoungeBackLightLastSeen => new(_haContext, "sensor.lounge_back_light_last_seen");
    public SensorEntity LoungeBlindRx => new(_haContext, "sensor.lounge_blind_rx");
    public SensorEntity LoungeBlindTx => new(_haContext, "sensor.lounge_blind_tx");
    public SensorEntity LoungeBlindUptime => new(_haContext, "sensor.lounge_blind_uptime");
    public SensorEntity LoungeDoorLastSeen => new(_haContext, "sensor.lounge_door_last_seen");
    public SensorEntity LoungeEchoRx => new(_haContext, "sensor.lounge_echo_rx");
    public SensorEntity LoungeEchoTx => new(_haContext, "sensor.lounge_echo_tx");
    public SensorEntity LoungeEchoUptime => new(_haContext, "sensor.lounge_echo_uptime");
    public SensorEntity LoungeEnergy => new(_haContext, "sensor.lounge_energy");
    public SensorEntity LoungeFrontLightLastSeen => new(_haContext, "sensor.lounge_front_light_last_seen");
    public SensorEntity LoungeGroupNextAlarm => new(_haContext, "sensor.lounge_group_next_alarm");
    public SensorEntity LoungeGroupNextReminder => new(_haContext, "sensor.lounge_group_next_reminder");
    public SensorEntity LoungeGroupNextTimer => new(_haContext, "sensor.lounge_group_next_timer");
    public SensorEntity LoungeLeftWindowLastSeen => new(_haContext, "sensor.lounge_left_window_last_seen");
    public SensorEntity LoungeLux => new(_haContext, "sensor.lounge_lux");
    public SensorEntity LoungeMotionBattery => new(_haContext, "sensor.lounge_motion_battery");
    public SensorEntity LoungeRightWindowLastSeen => new(_haContext, "sensor.lounge_right_window_last_seen");
    public SensorEntity LoungeRoomTemperature => new(_haContext, "sensor.lounge_room_temperature");
    public SensorEntity LoungeSonosNextReminder => new(_haContext, "sensor.lounge_sonos_next_reminder");
    public SensorEntity LumiLumiSensorMagnetAq238f0ec02Power => new(_haContext, "sensor.lumi_lumi_sensor_magnet_aq2_38f0ec02_power");
    public SensorEntity LumiLumiSensorMagnetAq256141203Power => new(_haContext, "sensor.lumi_lumi_sensor_magnet_aq2_56141203_power");
    public SensorEntity LumiLumiSensorMagnetAq283903a03Power => new(_haContext, "sensor.lumi_lumi_sensor_magnet_aq2_83903a03_power");
    public SensorEntity LumiLumiSensorMagnetAq28c913a03Power => new(_haContext, "sensor.lumi_lumi_sensor_magnet_aq2_8c913a03_power");
    public SensorEntity LumiLumiSensorMagnetAq29e0b1203Power => new(_haContext, "sensor.lumi_lumi_sensor_magnet_aq2_9e0b1203_power");
    public SensorEntity LumiLumiSensorMagnetAq2E6b02103Power => new(_haContext, "sensor.lumi_lumi_sensor_magnet_aq2_e6b02103_power");
    public SensorEntity LumiLumiSensorMagnetAq2Power => new(_haContext, "sensor.lumi_lumi_sensor_magnet_aq2_power");
    public SensorEntity Master1LightLastSeen => new(_haContext, "sensor.master_1_light_last_seen");
    public SensorEntity Master2LightLastSeen => new(_haContext, "sensor.master_2_light_last_seen");
    public SensorEntity Master3LightLastSeen => new(_haContext, "sensor.master_3_light_last_seen");
    public SensorEntity Master4LightLastSeen => new(_haContext, "sensor.master_4_light_last_seen");
    public SensorEntity MasterEchoRx => new(_haContext, "sensor.master_echo_rx");
    public SensorEntity MasterEchoTx => new(_haContext, "sensor.master_echo_tx");
    public SensorEntity MasterEchoUptime => new(_haContext, "sensor.master_echo_uptime");
    public SensorEntity MasterLux => new(_haContext, "sensor.master_lux");
    public SensorEntity MasterMotionBattery => new(_haContext, "sensor.master_motion_battery");
    public SensorEntity MasterMotionLastSeen => new(_haContext, "sensor.master_motion_last_seen");
    public SensorEntity MasterNextAlarm => new(_haContext, "sensor.master_next_alarm");
    public SensorEntity MasterNextReminder => new(_haContext, "sensor.master_next_reminder");
    public SensorEntity MasterNextTimer => new(_haContext, "sensor.master_next_timer");
    public SensorEntity MasterTeleRx => new(_haContext, "sensor.master_tele_rx");
    public SensorEntity MasterTeleTx => new(_haContext, "sensor.master_tele_tx");
    public SensorEntity MasterTeleUptime => new(_haContext, "sensor.master_tele_uptime");
    public SensorEntity MyWallPanelBatteryLevel => new(_haContext, "sensor.my_wall_panel_battery_level");
    public SensorEntity MyWallPanelLight => new(_haContext, "sensor.my_wall_panel_light");
    public SensorEntity NeerslagBuienalarmRegenData => new(_haContext, "sensor.neerslag_buienalarm_regen_data");
    public SensorEntity NeerslagBuienradarRegenData => new(_haContext, "sensor.neerslag_buienradar_regen_data");
    public SensorEntity NetdaemonStatus => new(_haContext, "sensor.netdaemon_status");
    public SensorEntity NiemandDriveBattery => new(_haContext, "sensor.niemand_drive_battery");
    public SensorEntity NiemandDriveInfo => new(_haContext, "sensor.niemand_drive_info");
    public SensorEntity NiemandDriveLastActivity => new(_haContext, "sensor.niemand_drive_last_activity");
    public SensorEntity NiemandDriveLastMotion => new(_haContext, "sensor.niemand_drive_last_motion");
    public SensorEntity NiemandDriveVolume => new(_haContext, "sensor.niemand_drive_volume");
    public SensorEntity NiemandDriveWireless => new(_haContext, "sensor.niemand_drive_wireless");
    public SensorEntity NiemandFrontDoorBattery => new(_haContext, "sensor.niemand_front_door_battery");
    public SensorEntity NiemandFrontDoorInfo => new(_haContext, "sensor.niemand_front_door_info");
    public SensorEntity NiemandFrontDoorLastActivity => new(_haContext, "sensor.niemand_front_door_last_activity");
    public SensorEntity NiemandFrontDoorLastDing => new(_haContext, "sensor.niemand_front_door_last_ding");
    public SensorEntity NiemandFrontDoorLastMotion => new(_haContext, "sensor.niemand_front_door_last_motion");
    public SensorEntity NiemandFrontDoorVolume => new(_haContext, "sensor.niemand_front_door_volume");
    public SensorEntity NiemandFrontDoorWireless => new(_haContext, "sensor.niemand_front_door_wireless");
    public SensorEntity NiemandGarageBattery => new(_haContext, "sensor.niemand_garage_battery");
    public SensorEntity NiemandGarageBattery2 => new(_haContext, "sensor.niemand_garage_battery_2");
    public SensorEntity NiemandGarageInfo => new(_haContext, "sensor.niemand_garage_info");
    public SensorEntity NiemandGarageLastActivity => new(_haContext, "sensor.niemand_garage_last_activity");
    public SensorEntity NiemandGarageLastMotion => new(_haContext, "sensor.niemand_garage_last_motion");
    public SensorEntity NiemandGarageVolume => new(_haContext, "sensor.niemand_garage_volume");
    public SensorEntity NiemandGarageWireless => new(_haContext, "sensor.niemand_garage_wireless");
    public SensorEntity NiemandGardenBattery => new(_haContext, "sensor.niemand_garden_battery");
    public SensorEntity NiemandGardenInfo => new(_haContext, "sensor.niemand_garden_info");
    public SensorEntity NiemandGardenLastActivity => new(_haContext, "sensor.niemand_garden_last_activity");
    public SensorEntity NiemandGardenLastMotion => new(_haContext, "sensor.niemand_garden_last_motion");
    public SensorEntity NiemandGardenVolume => new(_haContext, "sensor.niemand_garden_volume");
    public SensorEntity NiemandGardenWireless => new(_haContext, "sensor.niemand_garden_wireless");
    public SensorEntity NiemandSideBattery => new(_haContext, "sensor.niemand_side_battery");
    public SensorEntity NiemandSideBattery2 => new(_haContext, "sensor.niemand_side_battery_2");
    public SensorEntity NiemandSideInfo => new(_haContext, "sensor.niemand_side_info");
    public SensorEntity NiemandSideLastActivity => new(_haContext, "sensor.niemand_side_last_activity");
    public SensorEntity NiemandSideLastMotion => new(_haContext, "sensor.niemand_side_last_motion");
    public SensorEntity NiemandSideVolume => new(_haContext, "sensor.niemand_side_volume");
    public SensorEntity NiemandSideWireless => new(_haContext, "sensor.niemand_side_wireless");
    public SensorEntity OctopusAgileCurrentRate => new(_haContext, "sensor.octopus_agile_current_rate");
    public SensorEntity OctopusAgileMinRate => new(_haContext, "sensor.octopus_agile_min_rate");
    public SensorEntity OctopusAgileNextRate => new(_haContext, "sensor.octopus_agile_next_rate");
    public SensorEntity OctopusAgilePreviousRate => new(_haContext, "sensor.octopus_agile_previous_rate");
    public SensorEntity OfficeAcRx => new(_haContext, "sensor.office_ac_rx");
    public SensorEntity OfficeAcTx => new(_haContext, "sensor.office_ac_tx");
    public SensorEntity OfficeAcUptime => new(_haContext, "sensor.office_ac_uptime");
    public SensorEntity OfficeDoorLastSeen => new(_haContext, "sensor.office_door_last_seen");
    public SensorEntity OfficeEchoRx => new(_haContext, "sensor.office_echo_rx");
    public SensorEntity OfficeEchoTx => new(_haContext, "sensor.office_echo_tx");
    public SensorEntity OfficeEchoUptime => new(_haContext, "sensor.office_echo_uptime");
    public SensorEntity OfficeEnergy => new(_haContext, "sensor.office_energy");
    public SensorEntity OfficeLux => new(_haContext, "sensor.office_lux");
    public SensorEntity OfficeMotionBattery => new(_haContext, "sensor.office_motion_battery");
    public SensorEntity OfficeMotionLastSeen => new(_haContext, "sensor.office_motion_last_seen");
    public SensorEntity OfficeNextAlarm => new(_haContext, "sensor.office_next_alarm");
    public SensorEntity OfficeNextReminder => new(_haContext, "sensor.office_next_reminder");
    public SensorEntity OfficeNextTimer => new(_haContext, "sensor.office_next_timer");
    public SensorEntity OfficeRoomTemperature => new(_haContext, "sensor.office_room_temperature");
    public SensorEntity OutsideDriveRx => new(_haContext, "sensor.outside_drive_rx");
    public SensorEntity OutsideDriveTx => new(_haContext, "sensor.outside_drive_tx");
    public SensorEntity OutsideDriveUptime => new(_haContext, "sensor.outside_drive_uptime");
    public SensorEntity OutsideGarageRx => new(_haContext, "sensor.outside_garage_rx");
    public SensorEntity OutsideGarageTx => new(_haContext, "sensor.outside_garage_tx");
    public SensorEntity OutsideGarageUptime => new(_haContext, "sensor.outside_garage_uptime");
    public SensorEntity PiHoleAdsBlockedToday => new(_haContext, "sensor.pi_hole_ads_blocked_today");
    public SensorEntity PiHoleAdsPercentageBlockedToday => new(_haContext, "sensor.pi_hole_ads_percentage_blocked_today");
    public SensorEntity PiHoleDnsQueriesCached => new(_haContext, "sensor.pi_hole_dns_queries_cached");
    public SensorEntity PiHoleDnsQueriesForwarded => new(_haContext, "sensor.pi_hole_dns_queries_forwarded");
    public SensorEntity PiHoleDnsQueriesToday => new(_haContext, "sensor.pi_hole_dns_queries_today");
    public SensorEntity PiHoleDnsUniqueClients => new(_haContext, "sensor.pi_hole_dns_unique_clients");
    public SensorEntity PiHoleDnsUniqueDomains => new(_haContext, "sensor.pi_hole_dns_unique_domains");
    public SensorEntity PiHoleDomainsBlocked => new(_haContext, "sensor.pi_hole_domains_blocked");
    public SensorEntity PiHoleSeenClients => new(_haContext, "sensor.pi_hole_seen_clients");
    public SensorEntity PlayroomEchoRx => new(_haContext, "sensor.playroom_echo_rx");
    public SensorEntity PlayroomEchoTx => new(_haContext, "sensor.playroom_echo_tx");
    public SensorEntity PlayroomEchoUptime => new(_haContext, "sensor.playroom_echo_uptime");
    public SensorEntity PlayroomLightLastSeen => new(_haContext, "sensor.playroom_light_last_seen");
    public SensorEntity PlayroomLux => new(_haContext, "sensor.playroom_lux");
    public SensorEntity PlayroomMotionBattery => new(_haContext, "sensor.playroom_motion_battery");
    public SensorEntity PlayroomMotionLastSeen => new(_haContext, "sensor.playroom_motion_last_seen");
    public SensorEntity Plug1Current => new(_haContext, "sensor.plug_1_current");
    public SensorEntity Plug1Energy => new(_haContext, "sensor.plug_1_energy");
    public SensorEntity Plug1EnergyStats => new(_haContext, "sensor.plug_1_energy_stats");
    public SensorEntity Plug1Voltage => new(_haContext, "sensor.plug_1_voltage");
    public SensorEntity Plug2Current => new(_haContext, "sensor.plug_2_current");
    public SensorEntity Plug2Energy => new(_haContext, "sensor.plug_2_energy");
    public SensorEntity Plug2Voltage => new(_haContext, "sensor.plug_2_voltage");
    public SensorEntity Plug3Current => new(_haContext, "sensor.plug_3_current");
    public SensorEntity Plug3Energy => new(_haContext, "sensor.plug_3_energy");
    public SensorEntity Plug3Voltage => new(_haContext, "sensor.plug_3_voltage");
    public SensorEntity Plug4Current => new(_haContext, "sensor.plug_4_current");
    public SensorEntity Plug4Energy => new(_haContext, "sensor.plug_4_energy");
    public SensorEntity Plug4EnergyStats => new(_haContext, "sensor.plug_4_energy_stats");
    public SensorEntity Plug4Voltage => new(_haContext, "sensor.plug_4_voltage");
    public SensorEntity Plug5Current => new(_haContext, "sensor.plug_5_current");
    public SensorEntity Plug5Energy => new(_haContext, "sensor.plug_5_energy");
    public SensorEntity Plug5Voltage => new(_haContext, "sensor.plug_5_voltage");
    public SensorEntity PorchRx => new(_haContext, "sensor.porch_rx");
    public SensorEntity PorchTx => new(_haContext, "sensor.porch_tx");
    public SensorEntity PorchUptime => new(_haContext, "sensor.porch_uptime");
    public SensorEntity RandomLight => new(_haContext, "sensor.random_light");
    public SensorEntity RaspberrypiCupsRx => new(_haContext, "sensor.raspberrypi_cups_rx");
    public SensorEntity RaspberrypiCupsTx => new(_haContext, "sensor.raspberrypi_cups_tx");
    public SensorEntity RaspberrypiCupsUptime => new(_haContext, "sensor.raspberrypi_cups_uptime");
    public SensorEntity RaspberrypiRx => new(_haContext, "sensor.raspberrypi_rx");
    public SensorEntity RaspberrypiTx => new(_haContext, "sensor.raspberrypi_tx");
    public SensorEntity RaspberrypiUptime => new(_haContext, "sensor.raspberrypi_uptime");
    public SensorEntity Remote1Action => new(_haContext, "sensor.remote_1_action");
    public SensorEntity Remote1Battery => new(_haContext, "sensor.remote_1_battery");
    public SensorEntity Remote1Link => new(_haContext, "sensor.remote_1_link");
    public SensorEntity Remote2Action => new(_haContext, "sensor.remote_2_action");
    public SensorEntity Remote2Battery => new(_haContext, "sensor.remote_2_battery");
    public SensorEntity Remote2Link => new(_haContext, "sensor.remote_2_link");
    public SensorEntity Remote3Action => new(_haContext, "sensor.remote_3_action");
    public SensorEntity Remote3Battery => new(_haContext, "sensor.remote_3_battery");
    public SensorEntity Remote3Click => new(_haContext, "sensor.remote_3_click");
    public SensorEntity Remote3Link => new(_haContext, "sensor.remote_3_link");
    public SensorEntity Ringhpcam49Rx => new(_haContext, "sensor.ringhpcam_49_rx");
    public SensorEntity Ringhpcam49Tx => new(_haContext, "sensor.ringhpcam_49_tx");
    public SensorEntity Ringhpcam49Uptime => new(_haContext, "sensor.ringhpcam_49_uptime");
    public SensorEntity Ringhpcam4cRx => new(_haContext, "sensor.ringhpcam_4c_rx");
    public SensorEntity Ringhpcam4cTx => new(_haContext, "sensor.ringhpcam_4c_tx");
    public SensorEntity Ringhpcam4cUptime => new(_haContext, "sensor.ringhpcam_4c_uptime");
    public SensorEntity RingproD6Rx => new(_haContext, "sensor.ringpro_d6_rx");
    public SensorEntity RingproD6Tx => new(_haContext, "sensor.ringpro_d6_tx");
    public SensorEntity RingproD6Uptime => new(_haContext, "sensor.ringpro_d6_uptime");
    public SensorEntity Ringstickupcam94Rx => new(_haContext, "sensor.ringstickupcam_94_rx");
    public SensorEntity Ringstickupcam94Tx => new(_haContext, "sensor.ringstickupcam_94_tx");
    public SensorEntity Ringstickupcam94Uptime => new(_haContext, "sensor.ringstickupcam_94_uptime");
    public SensorEntity Ringstickupcam9bRx => new(_haContext, "sensor.ringstickupcam_9b_rx");
    public SensorEntity Ringstickupcam9bTx => new(_haContext, "sensor.ringstickupcam_9b_tx");
    public SensorEntity Ringstickupcam9bUptime => new(_haContext, "sensor.ringstickupcam_9b_uptime");
    public SensorEntity RmminiD92b62Rx => new(_haContext, "sensor.rmmini_d9_2b_62_rx");
    public SensorEntity RmminiD92b62Tx => new(_haContext, "sensor.rmmini_d9_2b_62_tx");
    public SensorEntity RmminiD92b62Uptime => new(_haContext, "sensor.rmmini_d9_2b_62_uptime");
    public SensorEntity RoomPresenceAaron => new(_haContext, "sensor.room_presence_aaron");
    public SensorEntity RoomPresenceBoys => new(_haContext, "sensor.room_presence_boys");
    public SensorEntity RoomPresenceDining => new(_haContext, "sensor.room_presence_dining");
    public SensorEntity RoomPresenceEntrance => new(_haContext, "sensor.room_presence_entrance");
    public SensorEntity RoomPresenceFish => new(_haContext, "sensor.room_presence_fish");
    public SensorEntity RoomPresenceHallway => new(_haContext, "sensor.room_presence_hallway");
    public SensorEntity RoomPresenceJayden => new(_haContext, "sensor.room_presence_jayden");
    public SensorEntity RoomPresenceKitchen => new(_haContext, "sensor.room_presence_kitchen");
    public SensorEntity RoomPresenceLanding => new(_haContext, "sensor.room_presence_landing");
    public SensorEntity RoomPresenceLounge => new(_haContext, "sensor.room_presence_lounge");
    public SensorEntity RoomPresenceMaster => new(_haContext, "sensor.room_presence_master");
    public SensorEntity RoomPresencePlayroom => new(_haContext, "sensor.room_presence_playroom");
    public SensorEntity RoomPresencePorch => new(_haContext, "sensor.room_presence_porch");
    public SensorEntity RoomPresenceStudy => new(_haContext, "sensor.room_presence_study");
    public SensorEntity RoomPresenceToilet => new(_haContext, "sensor.room_presence_toilet");
    public SensorEntity RoomPresenceUtility => new(_haContext, "sensor.room_presence_utility");
    public SensorEntity Rx => new(_haContext, "sensor.rx");
    public SensorEntity Rx10 => new(_haContext, "sensor.rx_10");
    public SensorEntity Rx11 => new(_haContext, "sensor.rx_11");
    public SensorEntity Rx12 => new(_haContext, "sensor.rx_12");
    public SensorEntity Rx13 => new(_haContext, "sensor.rx_13");
    public SensorEntity Rx14 => new(_haContext, "sensor.rx_14");
    public SensorEntity Rx15 => new(_haContext, "sensor.rx_15");
    public SensorEntity Rx16 => new(_haContext, "sensor.rx_16");
    public SensorEntity Rx2 => new(_haContext, "sensor.rx_2");
    public SensorEntity Rx3 => new(_haContext, "sensor.rx_3");
    public SensorEntity Rx4 => new(_haContext, "sensor.rx_4");
    public SensorEntity Rx5 => new(_haContext, "sensor.rx_5");
    public SensorEntity Rx6 => new(_haContext, "sensor.rx_6");
    public SensorEntity Rx7 => new(_haContext, "sensor.rx_7");
    public SensorEntity Rx8 => new(_haContext, "sensor.rx_8");
    public SensorEntity Rx9 => new(_haContext, "sensor.rx_9");
    public SensorEntity Shelly155e8b5Rx => new(_haContext, "sensor.shelly1_55e8b5_rx");
    public SensorEntity Shelly155e8b5Tx => new(_haContext, "sensor.shelly1_55e8b5_tx");
    public SensorEntity Shelly155e8b5Uptime => new(_haContext, "sensor.shelly1_55e8b5_uptime");
    public SensorEntity Shellyswitch25E5a1d2Power => new(_haContext, "sensor.shellyswitch25_e5a1d2_power");
    public SensorEntity SideSnapshotLastUpdated => new(_haContext, "sensor.side_snapshot_last_updated");
    public SensorEntity SmartPlug1Rx => new(_haContext, "sensor.smart_plug_1_rx");
    public SensorEntity SmartPlug1Tx => new(_haContext, "sensor.smart_plug_1_tx");
    public SensorEntity SmartPlug1Uptime => new(_haContext, "sensor.smart_plug_1_uptime");
    public SensorEntity SmartPlug2Rx => new(_haContext, "sensor.smart_plug_2_rx");
    public SensorEntity SmartPlug2Tx => new(_haContext, "sensor.smart_plug_2_tx");
    public SensorEntity SmartPlug2Uptime => new(_haContext, "sensor.smart_plug_2_uptime");
    public SensorEntity SmartPlug4Rx => new(_haContext, "sensor.smart_plug_4_rx");
    public SensorEntity SmartPlug4Tx => new(_haContext, "sensor.smart_plug_4_tx");
    public SensorEntity SmartPlug4Uptime => new(_haContext, "sensor.smart_plug_4_uptime");
    public SensorEntity SonoszpRx => new(_haContext, "sensor.sonoszp_rx");
    public SensorEntity SonoszpRx2 => new(_haContext, "sensor.sonoszp_rx_2");
    public SensorEntity SonoszpTx => new(_haContext, "sensor.sonoszp_tx");
    public SensorEntity SonoszpTx2 => new(_haContext, "sensor.sonoszp_tx_2");
    public SensorEntity SonoszpUptime => new(_haContext, "sensor.sonoszp_uptime");
    public SensorEntity SonoszpUptime2 => new(_haContext, "sensor.sonoszp_uptime_2");
    public SensorEntity SpeedtestDownload => new(_haContext, "sensor.speedtest_download");
    public SensorEntity SpeedtestPing => new(_haContext, "sensor.speedtest_ping");
    public SensorEntity SpeedtestUpload => new(_haContext, "sensor.speedtest_upload");
    public SensorEntity SprinklerLeftEnergy => new(_haContext, "sensor.sprinkler_left_energy");
    public SensorEntity SprinklerLeftPower => new(_haContext, "sensor.sprinkler_left_power");
    public SensorEntity SprinklerRightEnergy => new(_haContext, "sensor.sprinkler_right_energy");
    public SensorEntity SprinklerRightPower => new(_haContext, "sensor.sprinkler_right_power");
    public SensorEntity Study1LightLastSeen => new(_haContext, "sensor.study1_light_last_seen");
    public SensorEntity Study2LightLastSeen => new(_haContext, "sensor.study2_light_last_seen");
    public SensorEntity Study3LightLastSeen => new(_haContext, "sensor.study3_light_last_seen");
    public SensorEntity SuspectDeviceRx => new(_haContext, "sensor.suspect_device_rx");
    public SensorEntity SuspectDeviceTx => new(_haContext, "sensor.suspect_device_tx");
    public SensorEntity SuspectDeviceUptime => new(_haContext, "sensor.suspect_device_uptime");
    public SensorEntity SuspectHuaweiRx => new(_haContext, "sensor.suspect_huawei_rx");
    public SensorEntity SuspectHuaweiTx => new(_haContext, "sensor.suspect_huawei_tx");
    public SensorEntity SuspectHuaweiUptime => new(_haContext, "sensor.suspect_huawei_uptime");
    public SensorEntity TemplateLastMotion => new(_haContext, "sensor.template_last_motion");
    public SensorEntity TemplateLastMotionDownstairs => new(_haContext, "sensor.template_last_motion_downstairs");
    public SensorEntity TemplateLastMotionUpstairs => new(_haContext, "sensor.template_last_motion_upstairs");
    public SensorEntity ThisDeviceNextAlarm => new(_haContext, "sensor.this_device_next_alarm");
    public SensorEntity ThisDeviceNextAlarm2 => new(_haContext, "sensor.this_device_next_alarm_2");
    public SensorEntity ThisDeviceNextReminder => new(_haContext, "sensor.this_device_next_reminder");
    public SensorEntity ThisDeviceNextReminder2 => new(_haContext, "sensor.this_device_next_reminder_2");
    public SensorEntity ThisDeviceNextTimer => new(_haContext, "sensor.this_device_next_timer");
    public SensorEntity ThisDeviceNextTimer2 => new(_haContext, "sensor.this_device_next_timer_2");
    public SensorEntity ToiletLightLastSeen => new(_haContext, "sensor.toilet_light_last_seen");
    public SensorEntity ToiletLux => new(_haContext, "sensor.toilet_lux");
    public SensorEntity ToiletMotionBattery => new(_haContext, "sensor.toilet_motion_battery");
    public SensorEntity ToiletMotionLastSeen => new(_haContext, "sensor.toilet_motion_last_seen");
    public SensorEntity ToiletWindowLastSeen => new(_haContext, "sensor.toilet_window_last_seen");
    public SensorEntity TumbleDryerDeltaenergy => new(_haContext, "sensor.tumble_dryer_deltaenergy");
    public SensorEntity TumbleDryerDeltaenergy2 => new(_haContext, "sensor.tumble_dryer_deltaenergy_2");
    public SensorEntity TumbleDryerDryerCompletionTime => new(_haContext, "sensor.tumble_dryer_dryer_completion_time");
    public SensorEntity TumbleDryerDryerJobState => new(_haContext, "sensor.tumble_dryer_dryer_job_state");
    public SensorEntity TumbleDryerDryerMachineState => new(_haContext, "sensor.tumble_dryer_dryer_machine_state");
    public SensorEntity TumbleDryerEnergy => new(_haContext, "sensor.tumble_dryer_energy");
    public SensorEntity TumbleDryerEnergy2 => new(_haContext, "sensor.tumble_dryer_energy_2");
    public SensorEntity TumbleDryerEnergysaved => new(_haContext, "sensor.tumble_dryer_energysaved");
    public SensorEntity TumbleDryerEnergysaved2 => new(_haContext, "sensor.tumble_dryer_energysaved_2");
    public SensorEntity TumbleDryerPower => new(_haContext, "sensor.tumble_dryer_power");
    public SensorEntity TumbleDryerPower2 => new(_haContext, "sensor.tumble_dryer_power_2");
    public SensorEntity TumbleDryerPowerenergy => new(_haContext, "sensor.tumble_dryer_powerenergy");
    public SensorEntity TumbleDryerPowerenergy2 => new(_haContext, "sensor.tumble_dryer_powerenergy_2");
    public SensorEntity Tx => new(_haContext, "sensor.tx");
    public SensorEntity Tx10 => new(_haContext, "sensor.tx_10");
    public SensorEntity Tx11 => new(_haContext, "sensor.tx_11");
    public SensorEntity Tx12 => new(_haContext, "sensor.tx_12");
    public SensorEntity Tx13 => new(_haContext, "sensor.tx_13");
    public SensorEntity Tx14 => new(_haContext, "sensor.tx_14");
    public SensorEntity Tx15 => new(_haContext, "sensor.tx_15");
    public SensorEntity Tx16 => new(_haContext, "sensor.tx_16");
    public SensorEntity Tx2 => new(_haContext, "sensor.tx_2");
    public SensorEntity Tx3 => new(_haContext, "sensor.tx_3");
    public SensorEntity Tx4 => new(_haContext, "sensor.tx_4");
    public SensorEntity Tx5 => new(_haContext, "sensor.tx_5");
    public SensorEntity Tx6 => new(_haContext, "sensor.tx_6");
    public SensorEntity Tx7 => new(_haContext, "sensor.tx_7");
    public SensorEntity Tx8 => new(_haContext, "sensor.tx_8");
    public SensorEntity Tx9 => new(_haContext, "sensor.tx_9");
    public SensorEntity Ty012047432cf4326b7081 => new(_haContext, "sensor.ty012047432cf4326b7081");
    public SensorEntity Ty012047432cf4326b70812 => new(_haContext, "sensor.ty012047432cf4326b7081_2");
    public SensorEntity Ty012047432cf4326b70813 => new(_haContext, "sensor.ty012047432cf4326b7081_3");
    public SensorEntity Ty012047432cf4326b7a3a => new(_haContext, "sensor.ty012047432cf4326b7a3a");
    public SensorEntity Ty012047432cf4326b7a3a2 => new(_haContext, "sensor.ty012047432cf4326b7a3a_2");
    public SensorEntity Ty012047432cf4326b7a3a3 => new(_haContext, "sensor.ty012047432cf4326b7a3a_3");
    public SensorEntity Ty012047435002915e9eb5 => new(_haContext, "sensor.ty012047435002915e9eb5");
    public SensorEntity Ty012047435002915e9eb52 => new(_haContext, "sensor.ty012047435002915e9eb5_2");
    public SensorEntity Ty012047435002915e9eb53 => new(_haContext, "sensor.ty012047435002915e9eb5_3");
    public SensorEntity Uptime => new(_haContext, "sensor.uptime");
    public SensorEntity Uptime10 => new(_haContext, "sensor.uptime_10");
    public SensorEntity Uptime11 => new(_haContext, "sensor.uptime_11");
    public SensorEntity Uptime12 => new(_haContext, "sensor.uptime_12");
    public SensorEntity Uptime13 => new(_haContext, "sensor.uptime_13");
    public SensorEntity Uptime14 => new(_haContext, "sensor.uptime_14");
    public SensorEntity Uptime15 => new(_haContext, "sensor.uptime_15");
    public SensorEntity Uptime16 => new(_haContext, "sensor.uptime_16");
    public SensorEntity Uptime2 => new(_haContext, "sensor.uptime_2");
    public SensorEntity Uptime3 => new(_haContext, "sensor.uptime_3");
    public SensorEntity Uptime4 => new(_haContext, "sensor.uptime_4");
    public SensorEntity Uptime5 => new(_haContext, "sensor.uptime_5");
    public SensorEntity Uptime6 => new(_haContext, "sensor.uptime_6");
    public SensorEntity Uptime7 => new(_haContext, "sensor.uptime_7");
    public SensorEntity Uptime8 => new(_haContext, "sensor.uptime_8");
    public SensorEntity Uptime9 => new(_haContext, "sensor.uptime_9");
    public SensorEntity Utility1LightLastSeen => new(_haContext, "sensor.utility_1_light_last_seen");
    public SensorEntity Utility2LightLastSeen => new(_haContext, "sensor.utility_2_light_last_seen");
    public SensorEntity Utility3LightLastSeen => new(_haContext, "sensor.utility_3_light_last_seen");
    public SensorEntity UtilityLux => new(_haContext, "sensor.utility_lux");
    public SensorEntity UtilityMotionBattery => new(_haContext, "sensor.utility_motion_battery");
    public SensorEntity UtilityMotionLastSeen => new(_haContext, "sensor.utility_motion_last_seen");
    public SensorEntity WallpanelFireHd8Rx => new(_haContext, "sensor.wallpanel_fire_hd8_rx");
    public SensorEntity WallpanelFireHd8Tx => new(_haContext, "sensor.wallpanel_fire_hd8_tx");
    public SensorEntity WallpanelFireHd8Uptime => new(_haContext, "sensor.wallpanel_fire_hd8_uptime");
    public SensorEntity WallpanelNextAlarm => new(_haContext, "sensor.wallpanel_next_alarm");
    public SensorEntity WallpanelNextReminder => new(_haContext, "sensor.wallpanel_next_reminder");
    public SensorEntity WallpanelNextTimer => new(_haContext, "sensor.wallpanel_next_timer");
    public SensorEntity WasherRx => new(_haContext, "sensor.washer_rx");
    public SensorEntity WasherTx => new(_haContext, "sensor.washer_tx");
    public SensorEntity WasherUptime => new(_haContext, "sensor.washer_uptime");
    public SensorEntity WashingMachineDeltaenergy => new(_haContext, "sensor.washing_machine_deltaenergy");
    public SensorEntity WashingMachineDeltaenergy2 => new(_haContext, "sensor.washing_machine_deltaenergy_2");
    public SensorEntity WashingMachineEnergy => new(_haContext, "sensor.washing_machine_energy");
    public SensorEntity WashingMachineEnergy2 => new(_haContext, "sensor.washing_machine_energy_2");
    public SensorEntity WashingMachineEnergysaved => new(_haContext, "sensor.washing_machine_energysaved");
    public SensorEntity WashingMachineEnergysaved2 => new(_haContext, "sensor.washing_machine_energysaved_2");
    public SensorEntity WashingMachinePower => new(_haContext, "sensor.washing_machine_power");
    public SensorEntity WashingMachinePower2 => new(_haContext, "sensor.washing_machine_power_2");
    public SensorEntity WashingMachinePowerenergy => new(_haContext, "sensor.washing_machine_powerenergy");
    public SensorEntity WashingMachinePowerenergy2 => new(_haContext, "sensor.washing_machine_powerenergy_2");
    public SensorEntity WashingMachineWasherCompletionTime => new(_haContext, "sensor.washing_machine_washer_completion_time");
    public SensorEntity WashingMachineWasherJobState => new(_haContext, "sensor.washing_machine_washer_job_state");
    public SensorEntity WashingMachineWasherMachineState => new(_haContext, "sensor.washing_machine_washer_machine_state");
    public SensorEntity WiserCloudStatus => new(_haContext, "sensor.wiser_cloud_status");
    public SensorEntity WiserHeathub => new(_haContext, "sensor.wiser_heathub");
    public SensorEntity WiserHeating => new(_haContext, "sensor.wiser_heating");
    public SensorEntity WiserHotWater => new(_haContext, "sensor.wiser_hot_water");
    public SensorEntity WiserItrvBoys => new(_haContext, "sensor.wiser_itrv_boys");
    public SensorEntity WiserItrvBoysBatteryLevel => new(_haContext, "sensor.wiser_itrv_boys_battery_level");
    public SensorEntity WiserItrvDining => new(_haContext, "sensor.wiser_itrv_dining");
    public SensorEntity WiserItrvDiningBatteryLevel => new(_haContext, "sensor.wiser_itrv_dining_battery_level");
    public SensorEntity WiserItrvEntrance => new(_haContext, "sensor.wiser_itrv_entrance");
    public SensorEntity WiserItrvEntranceBatteryLevel => new(_haContext, "sensor.wiser_itrv_entrance_battery_level");
    public SensorEntity WiserItrvGuestRoom => new(_haContext, "sensor.wiser_itrv_guest_room");
    public SensorEntity WiserItrvGuestRoomBatteryLevel => new(_haContext, "sensor.wiser_itrv_guest_room_battery_level");
    public SensorEntity WiserItrvLanding => new(_haContext, "sensor.wiser_itrv_landing");
    public SensorEntity WiserItrvLandingBatteryLevel => new(_haContext, "sensor.wiser_itrv_landing_battery_level");
    public SensorEntity WiserItrvLounge => new(_haContext, "sensor.wiser_itrv_lounge");
    public SensorEntity WiserItrvLoungeBatteryLevel => new(_haContext, "sensor.wiser_itrv_lounge_battery_level");
    public SensorEntity WiserItrvLoungeBay => new(_haContext, "sensor.wiser_itrv_lounge_bay");
    public SensorEntity WiserItrvLoungeBayBatteryLevel => new(_haContext, "sensor.wiser_itrv_lounge_bay_battery_level");
    public SensorEntity WiserItrvMaster => new(_haContext, "sensor.wiser_itrv_master");
    public SensorEntity WiserItrvMasterBatteryLevel => new(_haContext, "sensor.wiser_itrv_master_battery_level");
    public SensorEntity WiserItrvOffice => new(_haContext, "sensor.wiser_itrv_office");
    public SensorEntity WiserItrvOfficeBatteryLevel => new(_haContext, "sensor.wiser_itrv_office_battery_level");
    public SensorEntity WiserItrvPlayroom => new(_haContext, "sensor.wiser_itrv_playroom");
    public SensorEntity WiserItrvPlayroomBatteryLevel => new(_haContext, "sensor.wiser_itrv_playroom_battery_level");
    public SensorEntity WiserItrvUtility => new(_haContext, "sensor.wiser_itrv_utility");
    public SensorEntity WiserItrvUtilityBatteryLevel => new(_haContext, "sensor.wiser_itrv_utility_battery_level");
    public SensorEntity WiserOperationMode => new(_haContext, "sensor.wiser_operation_mode");
    public SensorEntity WiserRoomstatKitchen => new(_haContext, "sensor.wiser_roomstat_kitchen");
    public SensorEntity WiserRoomstatKitchenBatteryLevel => new(_haContext, "sensor.wiser_roomstat_kitchen_battery_level");
    public SensorEntity Wiserheat031c5eRx => new(_haContext, "sensor.wiserheat031c5e_rx");
    public SensorEntity Wiserheat031c5eTx => new(_haContext, "sensor.wiserheat031c5e_tx");
    public SensorEntity Wiserheat031c5eUptime => new(_haContext, "sensor.wiserheat031c5e_uptime");
}

public class SunEntities
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public SunEntities(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public SunEntity Sun => new(_haContext, "sun.sun");
}

public class SwitchEntities
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public SwitchEntities(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public SwitchEntity AaronDoNotDisturbSwitch => new(_haContext, "switch.aaron_do_not_disturb_switch");
    public SwitchEntity AaronRepeatSwitch => new(_haContext, "switch.aaron_repeat_switch");
    public SwitchEntity AaronShuffleSwitch => new(_haContext, "switch.aaron_shuffle_switch");
    public SwitchEntity AdaptiveLightingAdaptBrightnessEntrance => new(_haContext, "switch.adaptive_lighting_adapt_brightness_entrance");
    public SwitchEntity AdaptiveLightingAdaptBrightnessFloor => new(_haContext, "switch.adaptive_lighting_adapt_brightness_floor");
    public SwitchEntity AdaptiveLightingAdaptBrightnessHallway => new(_haContext, "switch.adaptive_lighting_adapt_brightness_hallway");
    public SwitchEntity AdaptiveLightingAdaptBrightnessJayden => new(_haContext, "switch.adaptive_lighting_adapt_brightness_jayden");
    public SwitchEntity AdaptiveLightingAdaptBrightnessKitchen => new(_haContext, "switch.adaptive_lighting_adapt_brightness_kitchen");
    public SwitchEntity AdaptiveLightingAdaptBrightnessLanding => new(_haContext, "switch.adaptive_lighting_adapt_brightness_landing");
    public SwitchEntity AdaptiveLightingAdaptBrightnessLounge => new(_haContext, "switch.adaptive_lighting_adapt_brightness_lounge");
    public SwitchEntity AdaptiveLightingAdaptBrightnessStudy => new(_haContext, "switch.adaptive_lighting_adapt_brightness_study");
    public SwitchEntity AdaptiveLightingAdaptBrightnessToilet => new(_haContext, "switch.adaptive_lighting_adapt_brightness_toilet");
    public SwitchEntity AdaptiveLightingAdaptBrightnessUtility => new(_haContext, "switch.adaptive_lighting_adapt_brightness_utility");
    public SwitchEntity AdaptiveLightingAdaptColorEntrance => new(_haContext, "switch.adaptive_lighting_adapt_color_entrance");
    public SwitchEntity AdaptiveLightingAdaptColorFloor => new(_haContext, "switch.adaptive_lighting_adapt_color_floor");
    public SwitchEntity AdaptiveLightingAdaptColorHallway => new(_haContext, "switch.adaptive_lighting_adapt_color_hallway");
    public SwitchEntity AdaptiveLightingAdaptColorJayden => new(_haContext, "switch.adaptive_lighting_adapt_color_jayden");
    public SwitchEntity AdaptiveLightingAdaptColorKitchen => new(_haContext, "switch.adaptive_lighting_adapt_color_kitchen");
    public SwitchEntity AdaptiveLightingAdaptColorLanding => new(_haContext, "switch.adaptive_lighting_adapt_color_landing");
    public SwitchEntity AdaptiveLightingAdaptColorLounge => new(_haContext, "switch.adaptive_lighting_adapt_color_lounge");
    public SwitchEntity AdaptiveLightingAdaptColorStudy => new(_haContext, "switch.adaptive_lighting_adapt_color_study");
    public SwitchEntity AdaptiveLightingAdaptColorToilet => new(_haContext, "switch.adaptive_lighting_adapt_color_toilet");
    public SwitchEntity AdaptiveLightingAdaptColorUtility => new(_haContext, "switch.adaptive_lighting_adapt_color_utility");
    public SwitchEntity AdaptiveLightingEntrance => new(_haContext, "switch.adaptive_lighting_entrance");
    public SwitchEntity AdaptiveLightingFloor => new(_haContext, "switch.adaptive_lighting_floor");
    public SwitchEntity AdaptiveLightingHallway => new(_haContext, "switch.adaptive_lighting_hallway");
    public SwitchEntity AdaptiveLightingJayden => new(_haContext, "switch.adaptive_lighting_jayden");
    public SwitchEntity AdaptiveLightingKitchen => new(_haContext, "switch.adaptive_lighting_kitchen");
    public SwitchEntity AdaptiveLightingLanding => new(_haContext, "switch.adaptive_lighting_landing");
    public SwitchEntity AdaptiveLightingLounge => new(_haContext, "switch.adaptive_lighting_lounge");
    public SwitchEntity AdaptiveLightingSleepModeEntrance => new(_haContext, "switch.adaptive_lighting_sleep_mode_entrance");
    public SwitchEntity AdaptiveLightingSleepModeFloor => new(_haContext, "switch.adaptive_lighting_sleep_mode_floor");
    public SwitchEntity AdaptiveLightingSleepModeHallway => new(_haContext, "switch.adaptive_lighting_sleep_mode_hallway");
    public SwitchEntity AdaptiveLightingSleepModeJayden => new(_haContext, "switch.adaptive_lighting_sleep_mode_jayden");
    public SwitchEntity AdaptiveLightingSleepModeKitchen => new(_haContext, "switch.adaptive_lighting_sleep_mode_kitchen");
    public SwitchEntity AdaptiveLightingSleepModeLanding => new(_haContext, "switch.adaptive_lighting_sleep_mode_landing");
    public SwitchEntity AdaptiveLightingSleepModeLounge => new(_haContext, "switch.adaptive_lighting_sleep_mode_lounge");
    public SwitchEntity AdaptiveLightingSleepModeStudy => new(_haContext, "switch.adaptive_lighting_sleep_mode_study");
    public SwitchEntity AdaptiveLightingSleepModeToilet => new(_haContext, "switch.adaptive_lighting_sleep_mode_toilet");
    public SwitchEntity AdaptiveLightingSleepModeUtility => new(_haContext, "switch.adaptive_lighting_sleep_mode_utility");
    public SwitchEntity AdaptiveLightingStudy => new(_haContext, "switch.adaptive_lighting_study");
    public SwitchEntity AdaptiveLightingToilet => new(_haContext, "switch.adaptive_lighting_toilet");
    public SwitchEntity AdaptiveLightingUtility => new(_haContext, "switch.adaptive_lighting_utility");
    public SwitchEntity AlarmBeepInfinate => new(_haContext, "switch.alarm_beep_infinate");
    public SwitchEntity AlarmBeepOne => new(_haContext, "switch.alarm_beep_one");
    public SwitchEntity AlarmBeepThree => new(_haContext, "switch.alarm_beep_three");
    public SwitchEntity AlarmBeepTwo => new(_haContext, "switch.alarm_beep_two");
    public SwitchEntity AlarmSirenBeepTwo2 => new(_haContext, "switch.alarm_siren_beep_two_2");
    public SwitchEntity ChimePlayDing => new(_haContext, "switch.chime_play_ding");
    public SwitchEntity ChimePlayMotion => new(_haContext, "switch.chime_play_motion");
    public SwitchEntity ChimeSnooze => new(_haContext, "switch.chime_snooze");
    public SwitchEntity ChristmasIndoorSonoff => new(_haContext, "switch.christmas_indoor_sonoff");
    public SwitchEntity CircadianLightingCircadianAaron => new(_haContext, "switch.circadian_lighting_circadian_aaron");
    public SwitchEntity CircadianLightingCircadianArron => new(_haContext, "switch.circadian_lighting_circadian_arron");
    public SwitchEntity CircadianLightingCircadianDining => new(_haContext, "switch.circadian_lighting_circadian_dining");
    public SwitchEntity CircadianLightingCircadianMaster => new(_haContext, "switch.circadian_lighting_circadian_master");
    public SwitchEntity DownstairsDoNotDisturbSwitch => new(_haContext, "switch.downstairs_do_not_disturb_switch");
    public SwitchEntity DownstairsPlayDingSound => new(_haContext, "switch.downstairs_play_ding_sound");
    public SwitchEntity DownstairsPlayMotionSound => new(_haContext, "switch.downstairs_play_motion_sound");
    public SwitchEntity DownstairsRepeatSwitch => new(_haContext, "switch.downstairs_repeat_switch");
    public SwitchEntity DownstairsShuffleSwitch => new(_haContext, "switch.downstairs_shuffle_switch");
    public SwitchEntity DownstairsSnooze => new(_haContext, "switch.downstairs_snooze");
    public SwitchEntity Entrance => new(_haContext, "switch.entrance");
    public SwitchEntity EugeneS2ndEchoDotDoNotDisturbSwitch => new(_haContext, "switch.eugene_s_2nd_echo_dot_do_not_disturb_switch");
    public SwitchEntity EugeneS2ndEchoDotRepeatSwitch => new(_haContext, "switch.eugene_s_2nd_echo_dot_repeat_switch");
    public SwitchEntity EugeneS2ndEchoDotShuffleSwitch => new(_haContext, "switch.eugene_s_2nd_echo_dot_shuffle_switch");
    public SwitchEntity EugeneS3rdEchoDotDoNotDisturbSwitch => new(_haContext, "switch.eugene_s_3rd_echo_dot_do_not_disturb_switch");
    public SwitchEntity EugeneS3rdEchoDotRepeatSwitch => new(_haContext, "switch.eugene_s_3rd_echo_dot_repeat_switch");
    public SwitchEntity EugeneS3rdEchoDotShuffleSwitch => new(_haContext, "switch.eugene_s_3rd_echo_dot_shuffle_switch");
    public SwitchEntity EugeneSLgOledWebos2021TvDoNotDisturbSwitch => new(_haContext, "switch.eugene_s_lg_oled_webos_2021_tv_do_not_disturb_switch");
    public SwitchEntity EugeneSLgOledWebos2021TvRepeatSwitch => new(_haContext, "switch.eugene_s_lg_oled_webos_2021_tv_repeat_switch");
    public SwitchEntity EugeneSLgOledWebos2021TvShuffleSwitch => new(_haContext, "switch.eugene_s_lg_oled_webos_2021_tv_shuffle_switch");
    public SwitchEntity EugeneSLgWebos2020TvDoNotDisturbSwitch => new(_haContext, "switch.eugene_s_lg_webos_2020_tv_do_not_disturb_switch");
    public SwitchEntity EugeneSLgWebos2020TvRepeatSwitch => new(_haContext, "switch.eugene_s_lg_webos_2020_tv_repeat_switch");
    public SwitchEntity EugeneSLgWebos2020TvShuffleSwitch => new(_haContext, "switch.eugene_s_lg_webos_2020_tv_shuffle_switch");
    public SwitchEntity EugeneSSonosArcDoNotDisturbSwitch => new(_haContext, "switch.eugene_s_sonos_arc_do_not_disturb_switch");
    public SwitchEntity EugeneSSonosArcRepeatSwitch => new(_haContext, "switch.eugene_s_sonos_arc_repeat_switch");
    public SwitchEntity EugeneSSonosArcShuffleSwitch => new(_haContext, "switch.eugene_s_sonos_arc_shuffle_switch");
    public SwitchEntity EverywhereDoNotDisturbSwitch => new(_haContext, "switch.everywhere_do_not_disturb_switch");
    public SwitchEntity EverywhereRepeatSwitch => new(_haContext, "switch.everywhere_repeat_switch");
    public SwitchEntity EverywhereShuffleSwitch => new(_haContext, "switch.everywhere_shuffle_switch");
    public SwitchEntity FloorSonoff => new(_haContext, "switch.floor_sonoff");
    public SwitchEntity GarageDoNotDisturbSwitch => new(_haContext, "switch.garage_do_not_disturb_switch");
    public SwitchEntity GarageRepeatSwitch => new(_haContext, "switch.garage_repeat_switch");
    public SwitchEntity GarageShuffleSwitch => new(_haContext, "switch.garage_shuffle_switch");
    public SwitchEntity GardenSiren2 => new(_haContext, "switch.garden_siren_2");
    public SwitchEntity Jayden => new(_haContext, "switch.jayden");
    public SwitchEntity JaydenAppletv => new(_haContext, "switch.jayden_appletv");
    public SwitchEntity JaydenBedside => new(_haContext, "switch.jayden_bedside");
    public SwitchEntity JaydenDoNotDisturbSwitch => new(_haContext, "switch.jayden_do_not_disturb_switch");
    public SwitchEntity JaydenDoNotDisturbSwitch2 => new(_haContext, "switch.jayden_do_not_disturb_switch_2");
    public SwitchEntity JaydenRaspberrypi => new(_haContext, "switch.jayden_raspberrypi");
    public SwitchEntity JaydenRepeatSwitch => new(_haContext, "switch.jayden_repeat_switch");
    public SwitchEntity JaydenRepeatSwitch2 => new(_haContext, "switch.jayden_repeat_switch_2");
    public SwitchEntity JaydenShuffleSwitch => new(_haContext, "switch.jayden_shuffle_switch");
    public SwitchEntity JaydenShuffleSwitch2 => new(_haContext, "switch.jayden_shuffle_switch_2");
    public SwitchEntity KitchenDoNotDisturbSwitch => new(_haContext, "switch.kitchen_do_not_disturb_switch");
    public SwitchEntity KitchenRepeatSwitch => new(_haContext, "switch.kitchen_repeat_switch");
    public SwitchEntity KitchenShuffleSwitch => new(_haContext, "switch.kitchen_shuffle_switch");
    public SwitchEntity LandingNight => new(_haContext, "switch.landing_night");
    public SwitchEntity LgTv => new(_haContext, "switch.lg_tv");
    public SwitchEntity LightManagerAaron => new(_haContext, "switch.light_manager_aaron");
    public SwitchEntity LightManagerDining => new(_haContext, "switch.light_manager_dining");
    public SwitchEntity LightManagerEntrance => new(_haContext, "switch.light_manager_entrance");
    public SwitchEntity LightManagerFish => new(_haContext, "switch.light_manager_fish");
    public SwitchEntity LightManagerHallway => new(_haContext, "switch.light_manager_hallway");
    public SwitchEntity LightManagerJayden => new(_haContext, "switch.light_manager_jayden");
    public SwitchEntity LightManagerKitchen => new(_haContext, "switch.light_manager_kitchen");
    public SwitchEntity LightManagerLanding => new(_haContext, "switch.light_manager_landing");
    public SwitchEntity LightManagerLounge => new(_haContext, "switch.light_manager_lounge");
    public SwitchEntity LightManagerMaster => new(_haContext, "switch.light_manager_master");
    public SwitchEntity LightManagerPlayroom => new(_haContext, "switch.light_manager_playroom");
    public SwitchEntity LightManagerPorch => new(_haContext, "switch.light_manager_porch");
    public SwitchEntity LightManagerStudy => new(_haContext, "switch.light_manager_study");
    public SwitchEntity LightManagerToilet => new(_haContext, "switch.light_manager_toilet");
    public SwitchEntity LightManagerUtility => new(_haContext, "switch.light_manager_utility");
    public SwitchEntity LoungeGroupDoNotDisturbSwitch => new(_haContext, "switch.lounge_group_do_not_disturb_switch");
    public SwitchEntity LoungeGroupRepeatSwitch => new(_haContext, "switch.lounge_group_repeat_switch");
    public SwitchEntity LoungeGroupShuffleSwitch => new(_haContext, "switch.lounge_group_shuffle_switch");
    public SwitchEntity LoungeLgtv => new(_haContext, "switch.lounge_lgtv");
    public SwitchEntity LoungeLgtv0 => new(_haContext, "switch.lounge_lgtv_0");
    public SwitchEntity LoungeLgtv1 => new(_haContext, "switch.lounge_lgtv_1");
    public SwitchEntity LoungeLgtv169 => new(_haContext, "switch.lounge_lgtv_16_9");
    public SwitchEntity LoungeLgtv2 => new(_haContext, "switch.lounge_lgtv_2");
    public SwitchEntity LoungeLgtv3 => new(_haContext, "switch.lounge_lgtv_3");
    public SwitchEntity LoungeLgtv4 => new(_haContext, "switch.lounge_lgtv_4");
    public SwitchEntity LoungeLgtv43 => new(_haContext, "switch.lounge_lgtv_4_3");
    public SwitchEntity LoungeLgtv5 => new(_haContext, "switch.lounge_lgtv_5");
    public SwitchEntity LoungeLgtv6 => new(_haContext, "switch.lounge_lgtv_6");
    public SwitchEntity LoungeLgtv7 => new(_haContext, "switch.lounge_lgtv_7");
    public SwitchEntity LoungeLgtv8 => new(_haContext, "switch.lounge_lgtv_8");
    public SwitchEntity LoungeLgtv9 => new(_haContext, "switch.lounge_lgtv_9");
    public SwitchEntity LoungeLgtvAProgram => new(_haContext, "switch.lounge_lgtv_a_program");
    public SwitchEntity LoungeLgtvApc => new(_haContext, "switch.lounge_lgtv_apc");
    public SwitchEntity LoungeLgtvArc => new(_haContext, "switch.lounge_lgtv_arc");
    public SwitchEntity LoungeLgtvBack => new(_haContext, "switch.lounge_lgtv_back");
    public SwitchEntity LoungeLgtvBack2 => new(_haContext, "switch.lounge_lgtv_back_2");
    public SwitchEntity LoungeLgtvBrightness => new(_haContext, "switch.lounge_lgtv_brightness");
    public SwitchEntity LoungeLgtvBrightness2 => new(_haContext, "switch.lounge_lgtv_brightness_2");
    public SwitchEntity LoungeLgtvCaption => new(_haContext, "switch.lounge_lgtv_caption");
    public SwitchEntity LoungeLgtvChannel => new(_haContext, "switch.lounge_lgtv_channel");
    public SwitchEntity LoungeLgtvChannel2 => new(_haContext, "switch.lounge_lgtv_channel_2");
    public SwitchEntity LoungeLgtvColor1Red => new(_haContext, "switch.lounge_lgtv_color_1_red");
    public SwitchEntity LoungeLgtvColor2Green => new(_haContext, "switch.lounge_lgtv_color_2_green");
    public SwitchEntity LoungeLgtvColor3Yellow => new(_haContext, "switch.lounge_lgtv_color_3_yellow");
    public SwitchEntity LoungeLgtvColor4Blue => new(_haContext, "switch.lounge_lgtv_color_4_blue");
    public SwitchEntity LoungeLgtvComponent1 => new(_haContext, "switch.lounge_lgtv_component_1");
    public SwitchEntity LoungeLgtvComponet => new(_haContext, "switch.lounge_lgtv_componet");
    public SwitchEntity LoungeLgtvCursorDown => new(_haContext, "switch.lounge_lgtv_cursor_down");
    public SwitchEntity LoungeLgtvCursorEnter => new(_haContext, "switch.lounge_lgtv_cursor_enter");
    public SwitchEntity LoungeLgtvCursorLeft => new(_haContext, "switch.lounge_lgtv_cursor_left");
    public SwitchEntity LoungeLgtvCursorOk => new(_haContext, "switch.lounge_lgtv_cursor_ok");
    public SwitchEntity LoungeLgtvCursorRight => new(_haContext, "switch.lounge_lgtv_cursor_right");
    public SwitchEntity LoungeLgtvCursorUp => new(_haContext, "switch.lounge_lgtv_cursor_up");
    public SwitchEntity LoungeLgtvDash => new(_haContext, "switch.lounge_lgtv_dash");
    public SwitchEntity LoungeLgtvDasp => new(_haContext, "switch.lounge_lgtv_dasp");
    public SwitchEntity LoungeLgtvDown => new(_haContext, "switch.lounge_lgtv_down");
    public SwitchEntity LoungeLgtvDvi => new(_haContext, "switch.lounge_lgtv_dvi");
    public SwitchEntity LoungeLgtvEnter => new(_haContext, "switch.lounge_lgtv_enter");
    public SwitchEntity LoungeLgtvExit => new(_haContext, "switch.lounge_lgtv_exit");
    public SwitchEntity LoungeLgtvFav => new(_haContext, "switch.lounge_lgtv_fav");
    public SwitchEntity LoungeLgtvFlashback => new(_haContext, "switch.lounge_lgtv_flashback");
    public SwitchEntity LoungeLgtvFrc => new(_haContext, "switch.lounge_lgtv_frc");
    public SwitchEntity LoungeLgtvGuide => new(_haContext, "switch.lounge_lgtv_guide");
    public SwitchEntity LoungeLgtvIIi => new(_haContext, "switch.lounge_lgtv_i_ii");
    public SwitchEntity LoungeLgtvInfo => new(_haContext, "switch.lounge_lgtv_info");
    public SwitchEntity LoungeLgtvInput => new(_haContext, "switch.lounge_lgtv_input");
    public SwitchEntity LoungeLgtvInputDA => new(_haContext, "switch.lounge_lgtv_input_d_a");
    public SwitchEntity LoungeLgtvLeft => new(_haContext, "switch.lounge_lgtv_left");
    public SwitchEntity LoungeLgtvList => new(_haContext, "switch.lounge_lgtv_list");
    public SwitchEntity LoungeLgtvMemoryErase => new(_haContext, "switch.lounge_lgtv_memory_erase");
    public SwitchEntity LoungeLgtvMenu => new(_haContext, "switch.lounge_lgtv_menu");
    public SwitchEntity LoungeLgtvMts => new(_haContext, "switch.lounge_lgtv_mts");
    public SwitchEntity LoungeLgtvMultimedia => new(_haContext, "switch.lounge_lgtv_multimedia");
    public SwitchEntity LoungeLgtvMute => new(_haContext, "switch.lounge_lgtv_mute");
    public SwitchEntity LoungeLgtvPipCh => new(_haContext, "switch.lounge_lgtv_pip_ch");
    public SwitchEntity LoungeLgtvPipCh2 => new(_haContext, "switch.lounge_lgtv_pip_ch_2");
    public SwitchEntity LoungeLgtvPipDw => new(_haContext, "switch.lounge_lgtv_pip_dw");
    public SwitchEntity LoungeLgtvPipInput => new(_haContext, "switch.lounge_lgtv_pip_input");
    public SwitchEntity LoungeLgtvPower => new(_haContext, "switch.lounge_lgtv_power");
    public SwitchEntity LoungeLgtvPowerOff => new(_haContext, "switch.lounge_lgtv_power_off");
    public SwitchEntity LoungeLgtvPowerOn => new(_haContext, "switch.lounge_lgtv_power_on");
    public SwitchEntity LoungeLgtvQView => new(_haContext, "switch.lounge_lgtv_q_view");
    public SwitchEntity LoungeLgtvRatio => new(_haContext, "switch.lounge_lgtv_ratio");
    public SwitchEntity LoungeLgtvReview => new(_haContext, "switch.lounge_lgtv_review");
    public SwitchEntity LoungeLgtvRgb => new(_haContext, "switch.lounge_lgtv_rgb");
    public SwitchEntity LoungeLgtvRight => new(_haContext, "switch.lounge_lgtv_right");
    public SwitchEntity LoungeLgtvSimplink => new(_haContext, "switch.lounge_lgtv_simplink");
    public SwitchEntity LoungeLgtvSleep => new(_haContext, "switch.lounge_lgtv_sleep");
    public SwitchEntity LoungeLgtvSplitZoom => new(_haContext, "switch.lounge_lgtv_split_zoom");
    public SwitchEntity LoungeLgtvSubtitle => new(_haContext, "switch.lounge_lgtv_subtitle");
    public SwitchEntity LoungeLgtvSwap => new(_haContext, "switch.lounge_lgtv_swap");
    public SwitchEntity LoungeLgtvText => new(_haContext, "switch.lounge_lgtv_text");
    public SwitchEntity LoungeLgtvTextHold => new(_haContext, "switch.lounge_lgtv_text_hold");
    public SwitchEntity LoungeLgtvTextIndex => new(_haContext, "switch.lounge_lgtv_text_index");
    public SwitchEntity LoungeLgtvTextReveal => new(_haContext, "switch.lounge_lgtv_text_reveal");
    public SwitchEntity LoungeLgtvTextTime => new(_haContext, "switch.lounge_lgtv_text_time");
    public SwitchEntity LoungeLgtvTextUpdate => new(_haContext, "switch.lounge_lgtv_text_update");
    public SwitchEntity LoungeLgtvTv => new(_haContext, "switch.lounge_lgtv_tv");
    public SwitchEntity LoungeLgtvTvGuide => new(_haContext, "switch.lounge_lgtv_tv_guide");
    public SwitchEntity LoungeLgtvTvInput => new(_haContext, "switch.lounge_lgtv_tv_input");
    public SwitchEntity LoungeLgtvTvRadio => new(_haContext, "switch.lounge_lgtv_tv_radio");
    public SwitchEntity LoungeLgtvTvVideo => new(_haContext, "switch.lounge_lgtv_tv_video");
    public SwitchEntity LoungeLgtvTvVideo2 => new(_haContext, "switch.lounge_lgtv_tv_video_2");
    public SwitchEntity LoungeLgtvUp => new(_haContext, "switch.lounge_lgtv_up");
    public SwitchEntity LoungeLgtvVideo1 => new(_haContext, "switch.lounge_lgtv_video_1");
    public SwitchEntity LoungeLgtvVideo12 => new(_haContext, "switch.lounge_lgtv_video_1_2");
    public SwitchEntity LoungeLgtvVideo2 => new(_haContext, "switch.lounge_lgtv_video_2");
    public SwitchEntity LoungeLgtvVolume => new(_haContext, "switch.lounge_lgtv_volume");
    public SwitchEntity LoungeLgtvVolume2 => new(_haContext, "switch.lounge_lgtv_volume_2");
    public SwitchEntity LoungeLgtvWindowPosition => new(_haContext, "switch.lounge_lgtv_window_position");
    public SwitchEntity LoungeLgtvWindowSize => new(_haContext, "switch.lounge_lgtv_window_size");
    public SwitchEntity LoungeSonosDoNotDisturbSwitch => new(_haContext, "switch.lounge_sonos_do_not_disturb_switch");
    public SwitchEntity LoungeSonosRepeatSwitch => new(_haContext, "switch.lounge_sonos_repeat_switch");
    public SwitchEntity LoungeSonosShuffleSwitch => new(_haContext, "switch.lounge_sonos_shuffle_switch");
    public SwitchEntity MasterDoNotDisturbSwitch => new(_haContext, "switch.master_do_not_disturb_switch");
    public SwitchEntity MasterRepeatSwitch => new(_haContext, "switch.master_repeat_switch");
    public SwitchEntity MasterShuffleSwitch => new(_haContext, "switch.master_shuffle_switch");
    public SwitchEntity NetdaemonAutomationsapp => new(_haContext, "switch.netdaemon_automationsapp");
    public SwitchEntity NetdaemonDebugApp => new(_haContext, "switch.netdaemon_debug_app");
    public SwitchEntity NetdaemonHelloWorldApp => new(_haContext, "switch.netdaemon_hello_world_app");
    public SwitchEntity NetdaemonHouse => new(_haContext, "switch.netdaemon_house");
    public SwitchEntity NetdaemonHousemodeapp => new(_haContext, "switch.netdaemon_housemodeapp");
    public SwitchEntity NetdaemonLightsApp => new(_haContext, "switch.netdaemon_lights_app");
    public SwitchEntity NetdaemonLightsmanager => new(_haContext, "switch.netdaemon_lightsmanager");
    public SwitchEntity NetdaemonNiemandLights => new(_haContext, "switch.netdaemon_niemand_lights");
    public SwitchEntity NetdaemonNiemandNotificationEngine => new(_haContext, "switch.netdaemon_niemand_notification_engine");
    public SwitchEntity NetdaemonPresence => new(_haContext, "switch.netdaemon_presence");
    public SwitchEntity NetdaemonWakeupsimulator => new(_haContext, "switch.netdaemon_wakeupsimulator");
    public SwitchEntity NiemandDriveEventStream => new(_haContext, "switch.niemand_drive_event_stream");
    public SwitchEntity NiemandDriveLiveStream => new(_haContext, "switch.niemand_drive_live_stream");
    public SwitchEntity NiemandDriveSiren => new(_haContext, "switch.niemand_drive_siren");
    public SwitchEntity NiemandDriveSiren2 => new(_haContext, "switch.niemand_drive_siren_2");
    public SwitchEntity NiemandFrontDoorEventStream => new(_haContext, "switch.niemand_front_door_event_stream");
    public SwitchEntity NiemandFrontDoorLiveStream => new(_haContext, "switch.niemand_front_door_live_stream");
    public SwitchEntity NiemandGarageEventStream => new(_haContext, "switch.niemand_garage_event_stream");
    public SwitchEntity NiemandGarageLiveStream => new(_haContext, "switch.niemand_garage_live_stream");
    public SwitchEntity NiemandGarageSiren => new(_haContext, "switch.niemand_garage_siren");
    public SwitchEntity NiemandGarageSiren2 => new(_haContext, "switch.niemand_garage_siren_2");
    public SwitchEntity NiemandGardenEventStream => new(_haContext, "switch.niemand_garden_event_stream");
    public SwitchEntity NiemandGardenLiveStream => new(_haContext, "switch.niemand_garden_live_stream");
    public SwitchEntity NiemandGardenSiren => new(_haContext, "switch.niemand_garden_siren");
    public SwitchEntity NiemandGardenSiren2 => new(_haContext, "switch.niemand_garden_siren_2");
    public SwitchEntity NiemandSideEventStream => new(_haContext, "switch.niemand_side_event_stream");
    public SwitchEntity NiemandSideLiveStream => new(_haContext, "switch.niemand_side_live_stream");
    public SwitchEntity NiemandSideSiren => new(_haContext, "switch.niemand_side_siren");
    public SwitchEntity NiemandSideSiren2 => new(_haContext, "switch.niemand_side_siren_2");
    public SwitchEntity OfficeDoNotDisturbSwitch => new(_haContext, "switch.office_do_not_disturb_switch");
    public SwitchEntity OfficeRepeatSwitch => new(_haContext, "switch.office_repeat_switch");
    public SwitchEntity OfficeShuffleSwitch => new(_haContext, "switch.office_shuffle_switch");
    public SwitchEntity OfficeSkylight => new(_haContext, "switch.office_skylight");
    public SwitchEntity PiHole => new(_haContext, "switch.pi_hole");
    public SwitchEntity Plug1 => new(_haContext, "switch.plug_1");
    public SwitchEntity Plug2 => new(_haContext, "switch.plug_2");
    public SwitchEntity Plug3 => new(_haContext, "switch.plug_3");
    public SwitchEntity Plug4 => new(_haContext, "switch.plug_4");
    public SwitchEntity Plug5 => new(_haContext, "switch.plug_5");
    public SwitchEntity RoomPresenceEnabledAaron => new(_haContext, "switch.room_presence_enabled_aaron");
    public SwitchEntity RoomPresenceEnabledBoys => new(_haContext, "switch.room_presence_enabled_boys");
    public SwitchEntity RoomPresenceEnabledDining => new(_haContext, "switch.room_presence_enabled_dining");
    public SwitchEntity RoomPresenceEnabledEntrance => new(_haContext, "switch.room_presence_enabled_entrance");
    public SwitchEntity RoomPresenceEnabledFish => new(_haContext, "switch.room_presence_enabled_fish");
    public SwitchEntity RoomPresenceEnabledHallway => new(_haContext, "switch.room_presence_enabled_hallway");
    public SwitchEntity RoomPresenceEnabledJayden => new(_haContext, "switch.room_presence_enabled_jayden");
    public SwitchEntity RoomPresenceEnabledKitchen => new(_haContext, "switch.room_presence_enabled_kitchen");
    public SwitchEntity RoomPresenceEnabledLanding => new(_haContext, "switch.room_presence_enabled_landing");
    public SwitchEntity RoomPresenceEnabledLounge => new(_haContext, "switch.room_presence_enabled_lounge");
    public SwitchEntity RoomPresenceEnabledMaster => new(_haContext, "switch.room_presence_enabled_master");
    public SwitchEntity RoomPresenceEnabledPlayroom => new(_haContext, "switch.room_presence_enabled_playroom");
    public SwitchEntity RoomPresenceEnabledPorch => new(_haContext, "switch.room_presence_enabled_porch");
    public SwitchEntity RoomPresenceEnabledStudy => new(_haContext, "switch.room_presence_enabled_study");
    public SwitchEntity RoomPresenceEnabledToilet => new(_haContext, "switch.room_presence_enabled_toilet");
    public SwitchEntity RoomPresenceEnabledUtility => new(_haContext, "switch.room_presence_enabled_utility");
    public SwitchEntity Schedule5a4aba2 => new(_haContext, "switch.schedule_5a4aba_2");
    public SwitchEntity Schedule5ed2d5 => new(_haContext, "switch.schedule_5ed2d5");
    public SwitchEntity Schedule7b0cda2 => new(_haContext, "switch.schedule_7b0cda_2");
    public SwitchEntity ScheduleE5d71d => new(_haContext, "switch.schedule_e5d71d");
    public SwitchEntity ScheduleTurnTvOff => new(_haContext, "switch.schedule_turn_tv_off");
    public SwitchEntity Siren => new(_haContext, "switch.siren");
    public SwitchEntity SonosLoungeCrossfade => new(_haContext, "switch.sonos_lounge_crossfade");
    public SwitchEntity SonosLoungeNightSound => new(_haContext, "switch.sonos_lounge_night_sound");
    public SwitchEntity SonosLoungeSpeechEnhancement => new(_haContext, "switch.sonos_lounge_speech_enhancement");
    public SwitchEntity ThisDeviceDoNotDisturbSwitch => new(_haContext, "switch.this_device_do_not_disturb_switch");
    public SwitchEntity ThisDeviceDoNotDisturbSwitch2 => new(_haContext, "switch.this_device_do_not_disturb_switch_2");
    public SwitchEntity TumbleDryer => new(_haContext, "switch.tumble_dryer");
    public SwitchEntity TuyaSocket2 => new(_haContext, "switch.tuya_socket_2");
    public SwitchEntity Ty012047432cf4326b7a3a => new(_haContext, "switch.ty012047432cf4326b7a3a");
    public SwitchEntity Ty012047435002915e9eb5 => new(_haContext, "switch.ty012047435002915e9eb5");
    public SwitchEntity UpstairsDoNotDisturbSwitch => new(_haContext, "switch.upstairs_do_not_disturb_switch");
    public SwitchEntity UpstairsRepeatSwitch => new(_haContext, "switch.upstairs_repeat_switch");
    public SwitchEntity UpstairsShuffleSwitch => new(_haContext, "switch.upstairs_shuffle_switch");
    public SwitchEntity WallpanelDoNotDisturbSwitch => new(_haContext, "switch.wallpanel_do_not_disturb_switch");
    public SwitchEntity WashingMachine => new(_haContext, "switch.washing_machine");
    public SwitchEntity WiserAwayMode => new(_haContext, "switch.wiser_away_mode");
    public SwitchEntity WiserAwayModeAffectsHotWater => new(_haContext, "switch.wiser_away_mode_affects_hot_water");
    public SwitchEntity WiserComfortMode => new(_haContext, "switch.wiser_comfort_mode");
    public SwitchEntity WiserEcoMode => new(_haContext, "switch.wiser_eco_mode");
    public SwitchEntity WiserValveProtection => new(_haContext, "switch.wiser_valve_protection");
}

public class TimerEntities
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public TimerEntities(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public TimerEntity BackDoor => new(_haContext, "timer.back_door");
    public TimerEntity Bathroom => new(_haContext, "timer.bathroom");
    public TimerEntity BoysBedroom => new(_haContext, "timer.boys_bedroom");
    public TimerEntity Dining => new(_haContext, "timer.dining");
    public TimerEntity DiningDoor => new(_haContext, "timer.dining_door");
    public TimerEntity Ensuite => new(_haContext, "timer.ensuite");
    public TimerEntity Entrance => new(_haContext, "timer.entrance");
    public TimerEntity Fish => new(_haContext, "timer.fish");
    public TimerEntity FrontDoor => new(_haContext, "timer.front_door");
    public TimerEntity GarageDoor => new(_haContext, "timer.garage_door");
    public TimerEntity GuestBedroom => new(_haContext, "timer.guest_bedroom");
    public TimerEntity Kitchen => new(_haContext, "timer.kitchen");
    public TimerEntity Landing => new(_haContext, "timer.landing");
    public TimerEntity Lounge => new(_haContext, "timer.lounge");
    public TimerEntity LoungeDoor => new(_haContext, "timer.lounge_door");
    public TimerEntity MasterBedroom => new(_haContext, "timer.master_bedroom");
    public TimerEntity Office => new(_haContext, "timer.office");
    public TimerEntity PlayRoom => new(_haContext, "timer.play_room");
    public TimerEntity Porch => new(_haContext, "timer.porch");
    public TimerEntity Toilet => new(_haContext, "timer.toilet");
    public TimerEntity Utility => new(_haContext, "timer.utility");
}

public class WeatherEntities
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public WeatherEntities(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public WeatherEntity AccuweatherHome => new(_haContext, "weather.accuweather_home");
}

public class ZoneEntities
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public ZoneEntities(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public ZoneEntity Home => new(_haContext, "zone.home");
    public ZoneEntity MumHome => new(_haContext, "zone.mum_home");
}

public record AlarmControlPanelEntity : NetDaemon.HassModel.Entities.Entity<AlarmControlPanelEntity, NetDaemon.HassModel.Entities.EntityState<AlarmControlPanelAttributes>, AlarmControlPanelAttributes>
{
    public AlarmControlPanelEntity(NetDaemon.HassModel.Common.IHaContext haContext, string entityId) : base(haContext, entityId)
    {
    }
}

public record AutomationEntity : NetDaemon.HassModel.Entities.Entity<AutomationEntity, NetDaemon.HassModel.Entities.EntityState<AutomationAttributes>, AutomationAttributes>
{
    public AutomationEntity(NetDaemon.HassModel.Common.IHaContext haContext, string entityId) : base(haContext, entityId)
    {
    }
}

public record BinarySensorEntity : NetDaemon.HassModel.Entities.Entity<BinarySensorEntity, NetDaemon.HassModel.Entities.EntityState<BinarySensorAttributes>, BinarySensorAttributes>
{
    public BinarySensorEntity(NetDaemon.HassModel.Common.IHaContext haContext, string entityId) : base(haContext, entityId)
    {
    }
}

public record CalendarEntity : NetDaemon.HassModel.Entities.Entity<CalendarEntity, NetDaemon.HassModel.Entities.EntityState<CalendarAttributes>, CalendarAttributes>
{
    public CalendarEntity(NetDaemon.HassModel.Common.IHaContext haContext, string entityId) : base(haContext, entityId)
    {
    }
}

public record CameraEntity : NetDaemon.HassModel.Entities.Entity<CameraEntity, NetDaemon.HassModel.Entities.EntityState<CameraAttributes>, CameraAttributes>
{
    public CameraEntity(NetDaemon.HassModel.Common.IHaContext haContext, string entityId) : base(haContext, entityId)
    {
    }
}

public record ClimateEntity : NetDaemon.HassModel.Entities.Entity<ClimateEntity, NetDaemon.HassModel.Entities.EntityState<ClimateAttributes>, ClimateAttributes>
{
    public ClimateEntity(NetDaemon.HassModel.Common.IHaContext haContext, string entityId) : base(haContext, entityId)
    {
    }
}

public record CoverEntity : NetDaemon.HassModel.Entities.Entity<CoverEntity, NetDaemon.HassModel.Entities.EntityState<CoverAttributes>, CoverAttributes>
{
    public CoverEntity(NetDaemon.HassModel.Common.IHaContext haContext, string entityId) : base(haContext, entityId)
    {
    }
}

public record DeviceTrackerEntity : NetDaemon.HassModel.Entities.Entity<DeviceTrackerEntity, NetDaemon.HassModel.Entities.EntityState<DeviceTrackerAttributes>, DeviceTrackerAttributes>
{
    public DeviceTrackerEntity(NetDaemon.HassModel.Common.IHaContext haContext, string entityId) : base(haContext, entityId)
    {
    }
}

public record GroupEntity : NetDaemon.HassModel.Entities.Entity<GroupEntity, NetDaemon.HassModel.Entities.EntityState<GroupAttributes>, GroupAttributes>
{
    public GroupEntity(NetDaemon.HassModel.Common.IHaContext haContext, string entityId) : base(haContext, entityId)
    {
    }
}

public record InputBooleanEntity : NetDaemon.HassModel.Entities.Entity<InputBooleanEntity, NetDaemon.HassModel.Entities.EntityState<InputBooleanAttributes>, InputBooleanAttributes>
{
    public InputBooleanEntity(NetDaemon.HassModel.Common.IHaContext haContext, string entityId) : base(haContext, entityId)
    {
    }
}

public record InputNumberEntity : NetDaemon.HassModel.Entities.Entity<InputNumberEntity, NetDaemon.HassModel.Entities.EntityState<InputNumberAttributes>, InputNumberAttributes>
{
    public InputNumberEntity(NetDaemon.HassModel.Common.IHaContext haContext, string entityId) : base(haContext, entityId)
    {
    }
}

public record InputSelectEntity : NetDaemon.HassModel.Entities.Entity<InputSelectEntity, NetDaemon.HassModel.Entities.EntityState<InputSelectAttributes>, InputSelectAttributes>
{
    public InputSelectEntity(NetDaemon.HassModel.Common.IHaContext haContext, string entityId) : base(haContext, entityId)
    {
    }
}

public record InputTextEntity : NetDaemon.HassModel.Entities.Entity<InputTextEntity, NetDaemon.HassModel.Entities.EntityState<InputTextAttributes>, InputTextAttributes>
{
    public InputTextEntity(NetDaemon.HassModel.Common.IHaContext haContext, string entityId) : base(haContext, entityId)
    {
    }
}

public record LightEntity : NetDaemon.HassModel.Entities.Entity<LightEntity, NetDaemon.HassModel.Entities.EntityState<LightAttributes>, LightAttributes>
{
    public LightEntity(NetDaemon.HassModel.Common.IHaContext haContext, string entityId) : base(haContext, entityId)
    {
    }
}

public record LockEntity : NetDaemon.HassModel.Entities.Entity<LockEntity, NetDaemon.HassModel.Entities.EntityState<LockAttributes>, LockAttributes>
{
    public LockEntity(NetDaemon.HassModel.Common.IHaContext haContext, string entityId) : base(haContext, entityId)
    {
    }
}

public record MediaPlayerEntity : NetDaemon.HassModel.Entities.Entity<MediaPlayerEntity, NetDaemon.HassModel.Entities.EntityState<MediaPlayerAttributes>, MediaPlayerAttributes>
{
    public MediaPlayerEntity(NetDaemon.HassModel.Common.IHaContext haContext, string entityId) : base(haContext, entityId)
    {
    }
}

public record NumberEntity : NetDaemon.HassModel.Entities.Entity<NumberEntity, NetDaemon.HassModel.Entities.EntityState<NumberAttributes>, NumberAttributes>
{
    public NumberEntity(NetDaemon.HassModel.Common.IHaContext haContext, string entityId) : base(haContext, entityId)
    {
    }
}

public record OctopusagileEntity : NetDaemon.HassModel.Entities.Entity<OctopusagileEntity, NetDaemon.HassModel.Entities.EntityState<OctopusagileAttributes>, OctopusagileAttributes>
{
    public OctopusagileEntity(NetDaemon.HassModel.Common.IHaContext haContext, string entityId) : base(haContext, entityId)
    {
    }
}

public record PersonEntity : NetDaemon.HassModel.Entities.Entity<PersonEntity, NetDaemon.HassModel.Entities.EntityState<PersonAttributes>, PersonAttributes>
{
    public PersonEntity(NetDaemon.HassModel.Common.IHaContext haContext, string entityId) : base(haContext, entityId)
    {
    }
}

public record RemoteEntity : NetDaemon.HassModel.Entities.Entity<RemoteEntity, NetDaemon.HassModel.Entities.EntityState<RemoteAttributes>, RemoteAttributes>
{
    public RemoteEntity(NetDaemon.HassModel.Common.IHaContext haContext, string entityId) : base(haContext, entityId)
    {
    }
}

public record ScriptEntity : NetDaemon.HassModel.Entities.Entity<ScriptEntity, NetDaemon.HassModel.Entities.EntityState<ScriptAttributes>, ScriptAttributes>
{
    public ScriptEntity(NetDaemon.HassModel.Common.IHaContext haContext, string entityId) : base(haContext, entityId)
    {
    }
}

public record SelectEntity : NetDaemon.HassModel.Entities.Entity<SelectEntity, NetDaemon.HassModel.Entities.EntityState<SelectAttributes>, SelectAttributes>
{
    public SelectEntity(NetDaemon.HassModel.Common.IHaContext haContext, string entityId) : base(haContext, entityId)
    {
    }
}

public record SensorEntity : NetDaemon.HassModel.Entities.Entity<SensorEntity, NetDaemon.HassModel.Entities.EntityState<SensorAttributes>, SensorAttributes>
{
    public SensorEntity(NetDaemon.HassModel.Common.IHaContext haContext, string entityId) : base(haContext, entityId)
    {
    }
}

public record SunEntity : NetDaemon.HassModel.Entities.Entity<SunEntity, NetDaemon.HassModel.Entities.EntityState<SunAttributes>, SunAttributes>
{
    public SunEntity(NetDaemon.HassModel.Common.IHaContext haContext, string entityId) : base(haContext, entityId)
    {
    }
}

public record SwitchEntity : NetDaemon.HassModel.Entities.Entity<SwitchEntity, NetDaemon.HassModel.Entities.EntityState<SwitchAttributes>, SwitchAttributes>
{
    public SwitchEntity(NetDaemon.HassModel.Common.IHaContext haContext, string entityId) : base(haContext, entityId)
    {
    }
}

public record TimerEntity : NetDaemon.HassModel.Entities.Entity<TimerEntity, NetDaemon.HassModel.Entities.EntityState<TimerAttributes>, TimerAttributes>
{
    public TimerEntity(NetDaemon.HassModel.Common.IHaContext haContext, string entityId) : base(haContext, entityId)
    {
    }
}

public record WeatherEntity : NetDaemon.HassModel.Entities.Entity<WeatherEntity, NetDaemon.HassModel.Entities.EntityState<WeatherAttributes>, WeatherAttributes>
{
    public WeatherEntity(NetDaemon.HassModel.Common.IHaContext haContext, string entityId) : base(haContext, entityId)
    {
    }
}

public record ZoneEntity : NetDaemon.HassModel.Entities.Entity<ZoneEntity, NetDaemon.HassModel.Entities.EntityState<ZoneAttributes>, ZoneAttributes>
{
    public ZoneEntity(NetDaemon.HassModel.Common.IHaContext haContext, string entityId) : base(haContext, entityId)
    {
    }
}

public record AlarmControlPanelAttributes
{
    [System.Text.Json.Serialization.JsonPropertyNameAttribute("arm_mode")]
    public object? ArmMode { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("bypassed_sensors")]
    public object? BypassedSensors { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("changed_by")]
    public object? ChangedBy { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("code_arm_required")]
    public bool? CodeArmRequired { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("code_disarm_required")]
    public bool? CodeDisarmRequired { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("code_format")]
    public object? CodeFormat { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("delay")]
    public object? Delay { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("expiration")]
    public object? Expiration { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("friendly_name")]
    public string? FriendlyName { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("icon")]
    public string? Icon { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("open_sensors")]
    public object? OpenSensors { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("supported_features")]
    public double? SupportedFeatures { get; init; }
}

public record AutomationAttributes
{
    [System.Text.Json.Serialization.JsonPropertyNameAttribute("current")]
    public double? Current { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("friendly_name")]
    public string? FriendlyName { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("id")]
    public string? Id { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("last_triggered")]
    public string? LastTriggered { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("mode")]
    public string? Mode { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("restored")]
    public bool? Restored { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("supported_features")]
    public double? SupportedFeatures { get; init; }
}

public record BinarySensorAttributes
{
    [System.Text.Json.Serialization.JsonPropertyNameAttribute("attribution")]
    public string? Attribution { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("device_class")]
    public string? DeviceClass { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Fast User Switched")]
    public bool? FastUserSwitched { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("friendly_name")]
    public string? FriendlyName { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("icon")]
    public string? Icon { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Idle")]
    public bool? Idle { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("lastDing")]
    public double? LastDing { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("lastDingTime")]
    public string? LastDingTime { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("lastMotion")]
    public double? LastMotion { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("lastMotionTime")]
    public string? LastMotionTime { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("last_updated")]
    public string? LastUpdated { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Locked")]
    public bool? Locked { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Manufacturer")]
    public string? Manufacturer { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("motionDetectionEnabled")]
    public bool? MotionDetectionEnabled { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("newest_version")]
    public string? NewestVersion { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("personDetected")]
    public bool? PersonDetected { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("release_notes")]
    public string? ReleaseNotes { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("restored")]
    public bool? Restored { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("round_trip_time_avg")]
    public double? RoundTripTimeAvg { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("round_trip_time_max")]
    public double? RoundTripTimeMax { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("round_trip_time_mdev")]
    public string? RoundTripTimeMdev { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("round_trip_time_min")]
    public double? RoundTripTimeMin { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Screen Off")]
    public bool? ScreenOff { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Screensaver")]
    public bool? Screensaver { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Sleeping")]
    public bool? Sleeping { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("supported_features")]
    public double? SupportedFeatures { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Terminating")]
    public bool? Terminating { get; init; }
}

public record CalendarAttributes
{
    [System.Text.Json.Serialization.JsonPropertyNameAttribute("friendly_name")]
    public string? FriendlyName { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("offset_reached")]
    public bool? OffsetReached { get; init; }
}

public record CameraAttributes
{
    [System.Text.Json.Serialization.JsonPropertyNameAttribute("access_token")]
    public string? AccessToken { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("attribution")]
    public string? Attribution { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("entity_picture")]
    public string? EntityPicture { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("friendly_name")]
    public string? FriendlyName { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("frontend_stream_type")]
    public string? FrontendStreamType { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("last_video_id")]
    public double? LastVideoId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("stream_type")]
    public string? StreamType { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("supported_features")]
    public double? SupportedFeatures { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("timestamp")]
    public double? Timestamp { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("video_url")]
    public string? VideoUrl { get; init; }
}

public record ClimateAttributes
{
    [System.Text.Json.Serialization.JsonPropertyNameAttribute("away_mode_supressed")]
    public bool? AwayModeSupressed { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("boost_end")]
    public string? BoostEnd { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("boost_remaining")]
    public double? BoostRemaining { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("control_output_state")]
    public string? ControlOutputState { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("current_temperature")]
    public double? CurrentTemperature { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("fan_mode")]
    public string? FanMode { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("fan_modes")]
    public object? FanModes { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("friendly_name")]
    public string? FriendlyName { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("heating_rate")]
    public double? HeatingRate { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("hvac_action")]
    public string? HvacAction { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("hvac_modes")]
    public object? HvacModes { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("icon")]
    public string? Icon { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("max_temp")]
    public double? MaxTemp { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("min_temp")]
    public double? MinTemp { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("percentage_demand")]
    public double? PercentageDemand { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("preset_mode")]
    public string? PresetMode { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("preset_modes")]
    public object? PresetModes { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("supported_features")]
    public double? SupportedFeatures { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("swing_mode")]
    public string? SwingMode { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("swing_modes")]
    public object? SwingModes { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("target_temp_step")]
    public double? TargetTempStep { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("temperature")]
    public double? Temperature { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("vane_horizontal")]
    public string? VaneHorizontal { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("vane_horizontal_positions")]
    public object? VaneHorizontalPositions { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("vane_vertical")]
    public string? VaneVertical { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("vane_vertical_positions")]
    public object? VaneVerticalPositions { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("window_detection_active")]
    public bool? WindowDetectionActive { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("window_state")]
    public string? WindowState { get; init; }
}

public record CoverAttributes
{
    [System.Text.Json.Serialization.JsonPropertyNameAttribute("current_position")]
    public double? CurrentPosition { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("device_class")]
    public string? DeviceClass { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("friendly_name")]
    public string? FriendlyName { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("supported_features")]
    public double? SupportedFeatures { get; init; }
}

public record DeviceTrackerAttributes
{
    [System.Text.Json.Serialization.JsonPropertyNameAttribute("altitude")]
    public double? Altitude { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("ap_mac")]
    public string? ApMac { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("authorized")]
    public bool? Authorized { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("battery_level")]
    public double? BatteryLevel { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("essid")]
    public string? Essid { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("friendly_name")]
    public string? FriendlyName { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("gps_accuracy")]
    public double? GpsAccuracy { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("hostname")]
    public string? Hostname { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("host_name")]
    public string? HostName { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("icon")]
    public string? Icon { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("ip")]
    public string? Ip { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("is_11r")]
    public bool? Is11r { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("is_guest")]
    public bool? IsGuest { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("_is_guest_by_uap")]
    public bool? IsGuestByUap { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("is_wired")]
    public bool? IsWired { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("latitude")]
    public double? Latitude { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("longitude")]
    public double? Longitude { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("mac")]
    public string? Mac { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("name")]
    public string? Name { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("note")]
    public string? Note { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("oui")]
    public string? Oui { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("qos_policy_applied")]
    public bool? QosPolicyApplied { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("radio")]
    public string? Radio { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("radio_proto")]
    public string? RadioProto { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("restored")]
    public bool? Restored { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("source_type")]
    public string? SourceType { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("supported_features")]
    public double? SupportedFeatures { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("vertical_accuracy")]
    public double? VerticalAccuracy { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("vlan")]
    public double? Vlan { get; init; }
}

public record GroupAttributes
{
    [System.Text.Json.Serialization.JsonPropertyNameAttribute("entity_id")]
    public object? EntityId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("friendly_name")]
    public string? FriendlyName { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("order")]
    public double? Order { get; init; }
}

public record InputBooleanAttributes
{
    [System.Text.Json.Serialization.JsonPropertyNameAttribute("editable")]
    public bool? Editable { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("icon")]
    public string? Icon { get; init; }
}

public record InputNumberAttributes
{
    [System.Text.Json.Serialization.JsonPropertyNameAttribute("editable")]
    public bool? Editable { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("initial")]
    public object? Initial { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("max")]
    public double? Max { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("min")]
    public double? Min { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("mode")]
    public string? Mode { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("restored")]
    public bool? Restored { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("step")]
    public double? Step { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("supported_features")]
    public double? SupportedFeatures { get; init; }
}

public record InputSelectAttributes
{
    [System.Text.Json.Serialization.JsonPropertyNameAttribute("editable")]
    public bool? Editable { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("friendly_name")]
    public string? FriendlyName { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("options")]
    public object? Options { get; init; }
}

public record InputTextAttributes
{
    [System.Text.Json.Serialization.JsonPropertyNameAttribute("editable")]
    public bool? Editable { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("max")]
    public double? Max { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("min")]
    public double? Min { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("mode")]
    public string? Mode { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("pattern")]
    public object? Pattern { get; init; }
}

public record LightAttributes
{
    [System.Text.Json.Serialization.JsonPropertyNameAttribute("attribution")]
    public string? Attribution { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("brightness")]
    public double? Brightness { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("color_mode")]
    public string? ColorMode { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("color_temp")]
    public double? ColorTemp { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("entity_id")]
    public object? EntityId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("friendly_name")]
    public string? FriendlyName { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("hs_color")]
    public object? HsColor { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("icon")]
    public string? Icon { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("max_mireds")]
    public double? MaxMireds { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("min_mireds")]
    public double? MinMireds { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("off_brightness")]
    public object? OffBrightness { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("rgb_color")]
    public object? RgbColor { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("supported_color_modes")]
    public object? SupportedColorModes { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("supported_features")]
    public double? SupportedFeatures { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("xy_color")]
    public object? XyColor { get; init; }
}

public record LockAttributes
{
    [System.Text.Json.Serialization.JsonPropertyNameAttribute("friendly_name")]
    public string? FriendlyName { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("restored")]
    public bool? Restored { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("supported_features")]
    public double? SupportedFeatures { get; init; }
}

public record MediaPlayerAttributes
{
    [System.Text.Json.Serialization.JsonPropertyNameAttribute("app_id")]
    public string? AppId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("app_name")]
    public string? AppName { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("available")]
    public bool? Available { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("bass_level")]
    public double? BassLevel { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("bluetooth_list")]
    public object? BluetoothList { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("connected_bluetooth")]
    public string? ConnectedBluetooth { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("device_class")]
    public string? DeviceClass { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("entity_picture")]
    public string? EntityPicture { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("entity_picture_local")]
    public string? EntityPictureLocal { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("friendly_name")]
    public string? FriendlyName { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("icon")]
    public string? Icon { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("is_volume_muted")]
    public bool? IsVolumeMuted { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("last_called")]
    public bool? LastCalled { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("last_called_summary")]
    public string? LastCalledSummary { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("last_called_timestamp")]
    public double? LastCalledTimestamp { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("media_album_name")]
    public string? MediaAlbumName { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("media_artist")]
    public string? MediaArtist { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("media_content_type")]
    public string? MediaContentType { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("media_duration")]
    public double? MediaDuration { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("media_position")]
    public double? MediaPosition { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("media_position_updated_at")]
    public string? MediaPositionUpdatedAt { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("media_title")]
    public string? MediaTitle { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("repeat")]
    public string? Repeat { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("restored")]
    public bool? Restored { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("shuffle")]
    public bool? Shuffle { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("sonos_group")]
    public object? SonosGroup { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("source")]
    public string? Source { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("source_list")]
    public object? SourceList { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("supported_features")]
    public double? SupportedFeatures { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("treble_level")]
    public double? TrebleLevel { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("volume_level")]
    public double? VolumeLevel { get; init; }
}

public record NumberAttributes
{
    [System.Text.Json.Serialization.JsonPropertyNameAttribute("friendly_name")]
    public string? FriendlyName { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("icon")]
    public string? Icon { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("max")]
    public double? Max { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("min")]
    public double? Min { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("mode")]
    public string? Mode { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("step")]
    public double? Step { get; init; }
}

public record OctopusagileAttributes
{
    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-02T19:00:00Z")]
    public double? HA20211202T190000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-02T19:30:00Z")]
    public double? HA20211202T193000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-02T20:00:00Z")]
    public double? HA20211202T200000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-02T20:30:00Z")]
    public double? HA20211202T203000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-02T21:00:00Z")]
    public double? HA20211202T210000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-02T21:30:00Z")]
    public double? HA20211202T213000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-02T22:00:00Z")]
    public double? HA20211202T220000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-02T22:30:00Z")]
    public double? HA20211202T223000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-02T23:00:00Z")]
    public double? HA20211202T230000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-02T23:30:00Z")]
    public double? HA20211202T233000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T00:00:00Z")]
    public double? HA20211203T000000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T00:30:00Z")]
    public double? HA20211203T003000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T01:00:00Z")]
    public double? HA20211203T010000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T01:30:00Z")]
    public double? HA20211203T013000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T02:00:00Z")]
    public double? HA20211203T020000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T02:30:00Z")]
    public double? HA20211203T023000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T03:00:00Z")]
    public double? HA20211203T030000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T03:30:00Z")]
    public double? HA20211203T033000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T04:00:00Z")]
    public double? HA20211203T040000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T04:30:00Z")]
    public double? HA20211203T043000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T05:00:00Z")]
    public double? HA20211203T050000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T05:30:00Z")]
    public double? HA20211203T053000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T06:00:00Z")]
    public double? HA20211203T060000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T06:30:00Z")]
    public double? HA20211203T063000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T07:00:00Z")]
    public double? HA20211203T070000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T07:30:00Z")]
    public double? HA20211203T073000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T08:00:00Z")]
    public double? HA20211203T080000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T08:30:00Z")]
    public double? HA20211203T083000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T09:00:00Z")]
    public double? HA20211203T090000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T09:30:00Z")]
    public double? HA20211203T093000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T10:00:00Z")]
    public double? HA20211203T100000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T10:30:00Z")]
    public double? HA20211203T103000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T11:00:00Z")]
    public double? HA20211203T110000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T11:30:00Z")]
    public double? HA20211203T113000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T12:00:00Z")]
    public double? HA20211203T120000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T12:30:00Z")]
    public double? HA20211203T123000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T13:00:00Z")]
    public double? HA20211203T130000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T13:30:00Z")]
    public double? HA20211203T133000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T14:00:00Z")]
    public double? HA20211203T140000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T14:30:00Z")]
    public double? HA20211203T143000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T15:00:00Z")]
    public double? HA20211203T150000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T15:30:00Z")]
    public double? HA20211203T153000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T16:00:00Z")]
    public double? HA20211203T160000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T16:30:00Z")]
    public double? HA20211203T163000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T17:00:00Z")]
    public double? HA20211203T170000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T17:30:00Z")]
    public double? HA20211203T173000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T18:00:00Z")]
    public double? HA20211203T180000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T18:30:00Z")]
    public double? HA20211203T183000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T19:00:00Z")]
    public double? HA20211203T190000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T19:30:00Z")]
    public double? HA20211203T193000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T20:00:00Z")]
    public double? HA20211203T200000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T20:30:00Z")]
    public double? HA20211203T203000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T21:00:00Z")]
    public double? HA20211203T210000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T21:30:00Z")]
    public double? HA20211203T213000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T22:00:00Z")]
    public double? HA20211203T220000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("2021-12-03T22:30:00Z")]
    public double? HA20211203T223000Z { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("icon")]
    public string? Icon { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("timers")]
    public object? Timers { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("unit_of_measurement")]
    public string? UnitOfMeasurement { get; init; }
}

public record PersonAttributes
{
    [System.Text.Json.Serialization.JsonPropertyNameAttribute("editable")]
    public bool? Editable { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("entity_picture")]
    public string? EntityPicture { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("friendly_name")]
    public string? FriendlyName { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("gps_accuracy")]
    public double? GpsAccuracy { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("id")]
    public string? Id { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("latitude")]
    public double? Latitude { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("longitude")]
    public double? Longitude { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("source")]
    public string? Source { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("user_id")]
    public string? UserId { get; init; }
}

public record RemoteAttributes
{
    [System.Text.Json.Serialization.JsonPropertyNameAttribute("friendly_name")]
    public string? FriendlyName { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("restored")]
    public bool? Restored { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("supported_features")]
    public double? SupportedFeatures { get; init; }
}

public record ScriptAttributes
{
    [System.Text.Json.Serialization.JsonPropertyNameAttribute("current")]
    public double? Current { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("friendly_name")]
    public string? FriendlyName { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("last_triggered")]
    public string? LastTriggered { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("max")]
    public double? Max { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("mode")]
    public string? Mode { get; init; }
}

public record SelectAttributes
{
    [System.Text.Json.Serialization.JsonPropertyNameAttribute("eventId")]
    public string? EventId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("friendly_name")]
    public string? FriendlyName { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("options")]
    public object? Options { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("recordingUrl")]
    public string? RecordingUrl { get; init; }
}

public record SensorAttributes
{
    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Active Camera")]
    public object? ActiveCamera { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("ActiveEntities")]
    public string? ActiveEntities { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Active Microphone")]
    public object? ActiveMicrophone { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Administrative Area")]
    public string? AdministrativeArea { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("All Camera")]
    public object? AllCamera { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("All Microphone")]
    public object? AllMicrophone { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Allows VoIP")]
    public bool? AllowsVoIP { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("answered")]
    public bool? Answered { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Areas Of Interest")]
    public string? AreasOfInterest { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("attribution")]
    public string? Attribution { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Available")]
    public string? Available { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Available (Important)")]
    public string? AvailableImportant { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Available (Opportunistic)")]
    public string? AvailableOpportunistic { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("average_change")]
    public object? AverageChange { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("AwayModeTemperature")]
    public double? AwayModeTemperature { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("BatteryHealth")]
    public string? BatteryHealth { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("BatteryHealthCondition")]
    public string? BatteryHealthCondition { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("battery_level")]
    public string? BatteryLevel_0 { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("batteryLevel")]
    public string? BatteryLevel_1 { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("battery_percent")]
    public double? BatteryPercent { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Battery Provides Time Remaining")]
    public bool? BatteryProvidesTimeRemaining { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("battery_voltage")]
    public object? BatteryVoltage { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Bundle Identifier")]
    public string? BundleIdentifier { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("bytes_received")]
    public double? BytesReceived { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("bytes_sent")]
    public double? BytesSent { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Carrier ID")]
    public string? CarrierID { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Carrier Name")]
    public string? CarrierName { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("category")]
    public string? Category { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("change")]
    public object? Change { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("change_rate")]
    public object? ChangeRate { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("colortemp")]
    public double? Colortemp { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("command_set")]
    public object? CommandSet { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Confidence")]
    public string? Confidence { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("ControlEntityIds")]
    public object? ControlEntityIds { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("controller_reception_RSSI")]
    public double? ControllerReceptionRSSI { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("count")]
    public double? Count { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Country")]
    public string? Country { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("count_sensors")]
    public double? CountSensors { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("created_at")]
    public string? CreatedAt { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("cron pattern")]
    public string? Cronpattern { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Current")]
    public double? Current { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Current Capacity")]
    public double? CurrentCapacity { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Current Radio Technology")]
    public string? CurrentRadioTechnology { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("data")]
    public object? Data { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("DesignCycleCount")]
    public double? DesignCycleCount { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("device_class")]
    public string? DeviceClass { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("device_lock_enabled")]
    public bool? DeviceLockEnabled { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("device_reception_LQI")]
    public double? DeviceReceptionLQI { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("device_reception_RSSI")]
    public double? DeviceReceptionRSSI { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("direction")]
    public string? Direction { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("dismissed")]
    public string? Dismissed { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("displayed_signal_strength")]
    public string? DisplayedSignalStrength { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Display IDs")]
    public object? DisplayIDs { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Display Names")]
    public object? DisplayNames { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("duration")]
    public double? Duration { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("event")]
    public string? Event { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("EventEntity")]
    public string? EventEntity { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Expiry")]
    public string? Expiry { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("firmware")]
    public string? Firmware { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("firmwareStatus")]
    public string? FirmwareStatus { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("friendly_name")]
    public string? FriendlyName { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Hardware Address")]
    public string? HardwareAddress { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Hardware Serial Number")]
    public string? HardwareSerialNumber { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("hub_route")]
    public string? HubRoute { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("humidity")]
    public double? Humidity { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("icon")]
    public string? Icon { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("info")]
    public string? Info { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Inland Water")]
    public string? InlandWater { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("integration")]
    public string? Integration { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Is Charged")]
    public bool? IsCharged { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Is Charging")]
    public bool? IsCharging { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Is Hidden")]
    public bool? IsHidden { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("ISO Country Code")]
    public string? ISOCountryCode { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Is Present")]
    public bool? IsPresent { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("KeepAliveEntities")]
    public string? KeepAliveEntities { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("last")]
    public double? Last { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("last_changed")]
    public string? LastChanged { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("last_entity_id")]
    public string? LastEntityId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("last_period")]
    public double? LastPeriod { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("last_reset")]
    public string? LastReset { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("lastUpdate")]
    public string? LastUpdate { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("last_updated")]
    public string? LastUpdated { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Launch Date")]
    public string? LaunchDate { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("level")]
    public string? Level { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Locality")]
    public string? Locality { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("location")]
    public string? Location_0 { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Location")]
    public object? Location_1 { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Low Power Mode")]
    public bool? LowPowerMode { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("max_age")]
    public string? MaxAge { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Max Capacity")]
    public double? MaxCapacity { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("max_entity_id")]
    public string? MaxEntityId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("max_value")]
    public object? MaxValue { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("mean")]
    public object? Mean { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("median")]
    public object? Median { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("meter_period")]
    public string? MeterPeriod { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("min_age")]
    public string? MinAge { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("min_entity_id")]
    public string? MinEntityId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("min_value")]
    public object? MinValue { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Mobile Country Code")]
    public string? MobileCountryCode { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Mobile Network Code")]
    public string? MobileNetworkCode { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("model_identifier")]
    public string? ModelIdentifier { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Name")]
    public string? Name { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("NightControlEntityIds")]
    public object? NightControlEntityIds { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("node_id")]
    public double? NodeId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("number_of_loaded_apps")]
    public double? NumberOfLoadedApps { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("number_of_running_apps")]
    public double? NumberOfRunningApps { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Ocean")]
    public string? Ocean { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Optimized Battery Charging Engaged")]
    public bool? OptimizedBatteryChargingEngaged { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Owns Menu Bar")]
    public bool? OwnsMenuBar { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("parent_node_id")]
    public double? ParentNodeId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("percentage_demand_Channel-1")]
    public double? PercentageDemandChannel1 { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Postal Code")]
    public string? PostalCode { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Power Source ID")]
    public double? PowerSourceID { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Power Source State")]
    public string? PowerSourceState { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("PresenceEntityIds")]
    public object? PresenceEntityIds { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("prior_value")]
    public string? PriorValue { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("process_timestamp")]
    public string? ProcessTimestamp { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("product_type")]
    public string? ProductType { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("quantiles")]
    public object? Quantiles { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("RandomDuration")]
    public double? RandomDuration { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("RandomEntityId")]
    public string? RandomEntityId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("RandomStates")]
    public object? RandomStates { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("recording_status")]
    public string? RecordingStatus { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("recurrence")]
    public object? Recurrence { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("reminder")]
    public object? Reminder { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("repositories")]
    public object? Repositories { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("restored")]
    public bool? Restored { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("rgb_color")]
    public object? RgbColor { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("room_ids_Channel-1")]
    public object? RoomIdsChannel1 { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("sampling_size")]
    public double? SamplingSize { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("serial")]
    public object? Serial { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("serial_number")]
    public string? SerialNumber { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("server_country")]
    public string? ServerCountry { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("server_id")]
    public string? ServerId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("server_name")]
    public string? ServerName { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("sorted_active")]
    public string? SortedActive { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("sorted_all")]
    public string? SortedAll { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("source")]
    public string? Source { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("standard_deviation")]
    public object? StandardDeviation { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("state_class")]
    public string? StateClass { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("state_message")]
    public string? StateMessage { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("state_reason")]
    public object? StateReason { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("status")]
    public string? Status { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("still_Image_URL")]
    public string? StillImageURL { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("stream_Source")]
    public string? StreamSource { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Sub Administrative Area")]
    public string? SubAdministrativeArea { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Sub Locality")]
    public string? SubLocality { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Sub Thoroughfare")]
    public string? SubThoroughfare { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("supported_features")]
    public double? SupportedFeatures { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("temperature")]
    public double? Temperature { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("text")]
    public string? Text { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Thoroughfare")]
    public string? Thoroughfare { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Time to Empty")]
    public double? TimetoEmpty { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Time to Full Charge")]
    public double? TimetoFullCharge { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Time Zone")]
    public string? TimeZone { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("total")]
    public object? Total_0 { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Total")]
    public string? Total_1 { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("total_active")]
    public double? TotalActive { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("total_all")]
    public double? TotalAll { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Transport Type")]
    public string? TransportType { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("type")]
    public object? Type_0 { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Type")]
    public string? Type_1 { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Types")]
    public object? Types { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("unit_of_measurement")]
    public string? UnitOfMeasurement { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("uri_supported")]
    public string? UriSupported { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("value")]
    public string? Value { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("variance")]
    public object? Variance { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("vendor")]
    public string? Vendor { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("version")]
    public string? Version { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("wirelessNetwork")]
    public string? WirelessNetwork { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("wirelessSignal")]
    public double? WirelessSignal { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("xy_color")]
    public object? XyColor { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("zigbee_channel")]
    public double? ZigbeeChannel { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Zones")]
    public object? Zones { get; init; }
}

public record SunAttributes
{
    [System.Text.Json.Serialization.JsonPropertyNameAttribute("azimuth")]
    public double? Azimuth { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("elevation")]
    public double? Elevation { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("friendly_name")]
    public string? FriendlyName { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("next_dawn")]
    public string? NextDawn { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("next_dusk")]
    public string? NextDusk { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("next_midnight")]
    public string? NextMidnight { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("next_noon")]
    public string? NextNoon { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("next_rising")]
    public string? NextRising { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("next_setting")]
    public string? NextSetting { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("rising")]
    public bool? Rising { get; init; }
}

public record SwitchAttributes
{
    [System.Text.Json.Serialization.JsonPropertyNameAttribute("actions")]
    public object? Actions { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("ActivePresenceSensors")]
    public object? ActivePresenceSensors { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("assumed_state")]
    public bool? AssumedState { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("attribution")]
    public string? Attribution { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("AwayModeTemperature")]
    public double? AwayModeTemperature { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("brightness")]
    public double? Brightness { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("brightness_pct")]
    public double? BrightnessPct { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("color_temp_kelvin")]
    public double? ColorTempKelvin { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("color_temp_mired")]
    public double? ColorTempMired { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("ControlEntityIds")]
    public object? ControlEntityIds { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("current_slot")]
    public double? CurrentSlot { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("device_class")]
    public string? DeviceClass { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("EventEntity")]
    public string? EventEntity { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("Expiry")]
    public string? Expiry { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("friendly_name")]
    public string? FriendlyName { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("hs_color")]
    public object? HsColor { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("icon")]
    public string? Icon { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("integration")]
    public string? Integration { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("manual_control")]
    public object? ManualControl { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("minutes_remaining")]
    public double? MinutesRemaining { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("next_slot")]
    public double? NextSlot { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("next_trigger")]
    public string? NextTrigger { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("PresenceEntityIds")]
    public object? PresenceEntityIds { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("rgb_color")]
    public object? RgbColor { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("runtime_info")]
    public object? RuntimeInfo { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("State")]
    public string? State { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("status")]
    public string? Status { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("sun_position")]
    public double? SunPosition { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("tags")]
    public object? Tags { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("times")]
    public object? Times { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("weekdays")]
    public object? Weekdays { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("xy_color")]
    public object? XyColor { get; init; }
}

public record TimerAttributes
{
    [System.Text.Json.Serialization.JsonPropertyNameAttribute("icon")]
    public string? Icon { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("restored")]
    public bool? Restored { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("supported_features")]
    public double? SupportedFeatures { get; init; }
}

public record WeatherAttributes
{
    [System.Text.Json.Serialization.JsonPropertyNameAttribute("attribution")]
    public string? Attribution { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("forecast")]
    public object? Forecast { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("friendly_name")]
    public string? FriendlyName { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("humidity")]
    public double? Humidity { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("ozone")]
    public double? Ozone { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("pressure")]
    public double? Pressure { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("temperature")]
    public double? Temperature { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("visibility")]
    public double? Visibility { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("wind_bearing")]
    public double? WindBearing { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("wind_speed")]
    public double? WindSpeed { get; init; }
}

public record ZoneAttributes
{
    [System.Text.Json.Serialization.JsonPropertyNameAttribute("editable")]
    public bool? Editable { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("friendly_name")]
    public string? FriendlyName { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("icon")]
    public string? Icon { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("latitude")]
    public double? Latitude { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("longitude")]
    public double? Longitude { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("passive")]
    public bool? Passive { get; init; }

    [System.Text.Json.Serialization.JsonPropertyNameAttribute("radius")]
    public double? Radius { get; init; }
}

public interface IServices
{
    AdaptiveLightingServices AdaptiveLighting { get; }

    AlarmControlPanelServices AlarmControlPanel { get; }

    AlarmoServices Alarmo { get; }

    AlexaMediaServices AlexaMedia { get; }

    AudiconnectServices Audiconnect { get; }

    AutomationServices Automation { get; }

    CameraServices Camera { get; }

    CircadianLightingServices CircadianLighting { get; }

    ClimateServices Climate { get; }

    CloudServices Cloud { get; }

    ConfiguratorServices Configurator { get; }

    CounterServices Counter { get; }

    CoverServices Cover { get; }

    DeviceTrackerServices DeviceTracker { get; }

    FanServices Fan { get; }

    FfmpegServices Ffmpeg { get; }

    FrontendServices Frontend { get; }

    GenericServices Generic { get; }

    GroupServices Group { get; }

    HistoryStatsServices HistoryStats { get; }

    HomeassistantServices Homeassistant { get; }

    HumidifierServices Humidifier { get; }

    InputBooleanServices InputBoolean { get; }

    InputDatetimeServices InputDatetime { get; }

    InputNumberServices InputNumber { get; }

    InputSelectServices InputSelect { get; }

    InputTextServices InputText { get; }

    LightServices Light { get; }

    LockServices Lock { get; }

    LogbookServices Logbook { get; }

    LoggerServices Logger { get; }

    MediaPlayerServices MediaPlayer { get; }

    MelcloudServices Melcloud { get; }

    MinMaxServices MinMax { get; }

    MqttServices Mqtt { get; }

    NetdaemonServices Netdaemon { get; }

    NotifyServices Notify { get; }

    NumberServices Number { get; }

    OctopusagileServices Octopusagile { get; }

    PersistentNotificationServices PersistentNotification { get; }

    PersonServices Person { get; }

    PiHoleServices PiHole { get; }

    PingServices Ping { get; }

    RecorderServices Recorder { get; }

    RemoteServices Remote { get; }

    RingServices Ring { get; }

    SceneServices Scene { get; }

    SchedulerServices Scheduler { get; }

    ScriptServices Script { get; }

    SelectServices Select { get; }

    SonosServices Sonos { get; }

    SpeedtestdotnetServices Speedtestdotnet { get; }

    StatisticsServices Statistics { get; }

    SwitchServices Switch { get; }

    SystemLogServices SystemLog { get; }

    TelegramServices Telegram { get; }

    TelegramBotServices TelegramBot { get; }

    TemplateServices Template { get; }

    TimerServices Timer { get; }

    TtsServices Tts { get; }

    UnifiServices Unifi { get; }

    UtilityMeterServices UtilityMeter { get; }

    VacuumServices Vacuum { get; }

    WakeOnLanServices WakeOnLan { get; }

    WaterHeaterServices WaterHeater { get; }

    WebostvServices Webostv { get; }

    WiserServices Wiser { get; }

    ZhaServices Zha { get; }

    ZoneServices Zone { get; }
}

public class Services : IServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public Services(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public AdaptiveLightingServices AdaptiveLighting => new(_haContext);
    public AlarmControlPanelServices AlarmControlPanel => new(_haContext);
    public AlarmoServices Alarmo => new(_haContext);
    public AlexaMediaServices AlexaMedia => new(_haContext);
    public AudiconnectServices Audiconnect => new(_haContext);
    public AutomationServices Automation => new(_haContext);
    public CameraServices Camera => new(_haContext);
    public CircadianLightingServices CircadianLighting => new(_haContext);
    public ClimateServices Climate => new(_haContext);
    public CloudServices Cloud => new(_haContext);
    public ConfiguratorServices Configurator => new(_haContext);
    public CounterServices Counter => new(_haContext);
    public CoverServices Cover => new(_haContext);
    public DeviceTrackerServices DeviceTracker => new(_haContext);
    public FanServices Fan => new(_haContext);
    public FfmpegServices Ffmpeg => new(_haContext);
    public FrontendServices Frontend => new(_haContext);
    public GenericServices Generic => new(_haContext);
    public GroupServices Group => new(_haContext);
    public HistoryStatsServices HistoryStats => new(_haContext);
    public HomeassistantServices Homeassistant => new(_haContext);
    public HumidifierServices Humidifier => new(_haContext);
    public InputBooleanServices InputBoolean => new(_haContext);
    public InputDatetimeServices InputDatetime => new(_haContext);
    public InputNumberServices InputNumber => new(_haContext);
    public InputSelectServices InputSelect => new(_haContext);
    public InputTextServices InputText => new(_haContext);
    public LightServices Light => new(_haContext);
    public LockServices Lock => new(_haContext);
    public LogbookServices Logbook => new(_haContext);
    public LoggerServices Logger => new(_haContext);
    public MediaPlayerServices MediaPlayer => new(_haContext);
    public MelcloudServices Melcloud => new(_haContext);
    public MinMaxServices MinMax => new(_haContext);
    public MqttServices Mqtt => new(_haContext);
    public NetdaemonServices Netdaemon => new(_haContext);
    public NotifyServices Notify => new(_haContext);
    public NumberServices Number => new(_haContext);
    public OctopusagileServices Octopusagile => new(_haContext);
    public PersistentNotificationServices PersistentNotification => new(_haContext);
    public PersonServices Person => new(_haContext);
    public PiHoleServices PiHole => new(_haContext);
    public PingServices Ping => new(_haContext);
    public RecorderServices Recorder => new(_haContext);
    public RemoteServices Remote => new(_haContext);
    public RingServices Ring => new(_haContext);
    public SceneServices Scene => new(_haContext);
    public SchedulerServices Scheduler => new(_haContext);
    public ScriptServices Script => new(_haContext);
    public SelectServices Select => new(_haContext);
    public SonosServices Sonos => new(_haContext);
    public SpeedtestdotnetServices Speedtestdotnet => new(_haContext);
    public StatisticsServices Statistics => new(_haContext);
    public SwitchServices Switch => new(_haContext);
    public SystemLogServices SystemLog => new(_haContext);
    public TelegramServices Telegram => new(_haContext);
    public TelegramBotServices TelegramBot => new(_haContext);
    public TemplateServices Template => new(_haContext);
    public TimerServices Timer => new(_haContext);
    public TtsServices Tts => new(_haContext);
    public UnifiServices Unifi => new(_haContext);
    public UtilityMeterServices UtilityMeter => new(_haContext);
    public VacuumServices Vacuum => new(_haContext);
    public WakeOnLanServices WakeOnLan => new(_haContext);
    public WaterHeaterServices WaterHeater => new(_haContext);
    public WebostvServices Webostv => new(_haContext);
    public WiserServices Wiser => new(_haContext);
    public ZhaServices Zha => new(_haContext);
    public ZoneServices Zone => new(_haContext);
}

public class AdaptiveLightingServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public AdaptiveLightingServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Apply(AdaptiveLightingApplyParameters data)
    {
        _haContext.CallService("adaptive_lighting", "apply", null, data);
    }

    public void Apply(string? @entityId = null, string? @lights = null, string? @transition = null, string? @adaptBrightness = null, string? @adaptColor = null, string? @preferRgbColor = null, string? @turnOnLights = null)
    {
        _haContext.CallService("adaptive_lighting", "apply", null, new
            {
                @entity_id = @entityId, @lights, @transition, @adapt_brightness = @adaptBrightness, @adapt_color = @adaptColor, @prefer_rgb_color = @preferRgbColor, @turn_on_lights = @turnOnLights
            }
        );
    }

    public void SetManualControl(AdaptiveLightingSetManualControlParameters data)
    {
        _haContext.CallService("adaptive_lighting", "set_manual_control", null, data);
    }

    public void SetManualControl(string? @entityId = null, string? @manualControl = null, string? @lights = null)
    {
        _haContext.CallService("adaptive_lighting", "set_manual_control", null, new
            {
                @entity_id = @entityId, @manual_control = @manualControl, @lights
            }
        );
    }
}

public record AdaptiveLightingApplyParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("entityId")]
    public string? EntityId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("lights")]
    public string? Lights { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("transition")]
    public string? Transition { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("adaptBrightness")]
    public string? AdaptBrightness { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("adaptColor")]
    public string? AdaptColor { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("preferRgbColor")]
    public string? PreferRgbColor { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("turnOnLights")]
    public string? TurnOnLights { get; init; }
}

public record AdaptiveLightingSetManualControlParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("entityId")]
    public string? EntityId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("manualControl")]
    public string? ManualControl { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("lights")]
    public string? Lights { get; init; }
}

public class AlarmControlPanelServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public AlarmControlPanelServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void AlarmArmAway(NetDaemon.HassModel.Entities.ServiceTarget target, AlarmControlPanelAlarmArmAwayParameters data)
    {
        _haContext.CallService("alarm_control_panel", "alarm_arm_away", target, data);
    }

    public void AlarmArmAway(NetDaemon.HassModel.Entities.ServiceTarget target, string? @code = null)
    {
        _haContext.CallService("alarm_control_panel", "alarm_arm_away", target, new
            {
                @code
            }
        );
    }

    public void AlarmArmCustomBypass(NetDaemon.HassModel.Entities.ServiceTarget target, AlarmControlPanelAlarmArmCustomBypassParameters data)
    {
        _haContext.CallService("alarm_control_panel", "alarm_arm_custom_bypass", target, data);
    }

    public void AlarmArmCustomBypass(NetDaemon.HassModel.Entities.ServiceTarget target, string? @code = null)
    {
        _haContext.CallService("alarm_control_panel", "alarm_arm_custom_bypass", target, new
            {
                @code
            }
        );
    }

    public void AlarmArmHome(NetDaemon.HassModel.Entities.ServiceTarget target, AlarmControlPanelAlarmArmHomeParameters data)
    {
        _haContext.CallService("alarm_control_panel", "alarm_arm_home", target, data);
    }

    public void AlarmArmHome(NetDaemon.HassModel.Entities.ServiceTarget target, string? @code = null)
    {
        _haContext.CallService("alarm_control_panel", "alarm_arm_home", target, new
            {
                @code
            }
        );
    }

    public void AlarmArmNight(NetDaemon.HassModel.Entities.ServiceTarget target, AlarmControlPanelAlarmArmNightParameters data)
    {
        _haContext.CallService("alarm_control_panel", "alarm_arm_night", target, data);
    }

    public void AlarmArmNight(NetDaemon.HassModel.Entities.ServiceTarget target, string? @code = null)
    {
        _haContext.CallService("alarm_control_panel", "alarm_arm_night", target, new
            {
                @code
            }
        );
    }

    public void AlarmArmVacation(NetDaemon.HassModel.Entities.ServiceTarget target, AlarmControlPanelAlarmArmVacationParameters data)
    {
        _haContext.CallService("alarm_control_panel", "alarm_arm_vacation", target, data);
    }

    public void AlarmArmVacation(NetDaemon.HassModel.Entities.ServiceTarget target, string? @code = null)
    {
        _haContext.CallService("alarm_control_panel", "alarm_arm_vacation", target, new
            {
                @code
            }
        );
    }

    public void AlarmDisarm(NetDaemon.HassModel.Entities.ServiceTarget target, AlarmControlPanelAlarmDisarmParameters data)
    {
        _haContext.CallService("alarm_control_panel", "alarm_disarm", target, data);
    }

    public void AlarmDisarm(NetDaemon.HassModel.Entities.ServiceTarget target, string? @code = null)
    {
        _haContext.CallService("alarm_control_panel", "alarm_disarm", target, new
            {
                @code
            }
        );
    }

    public void AlarmTrigger(NetDaemon.HassModel.Entities.ServiceTarget target, AlarmControlPanelAlarmTriggerParameters data)
    {
        _haContext.CallService("alarm_control_panel", "alarm_trigger", target, data);
    }

    public void AlarmTrigger(NetDaemon.HassModel.Entities.ServiceTarget target, string? @code = null)
    {
        _haContext.CallService("alarm_control_panel", "alarm_trigger", target, new
            {
                @code
            }
        );
    }
}

public record AlarmControlPanelAlarmArmAwayParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("code")]
    public string? Code { get; init; }
}

public record AlarmControlPanelAlarmArmCustomBypassParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("code")]
    public string? Code { get; init; }
}

public record AlarmControlPanelAlarmArmHomeParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("code")]
    public string? Code { get; init; }
}

public record AlarmControlPanelAlarmArmNightParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("code")]
    public string? Code { get; init; }
}

public record AlarmControlPanelAlarmArmVacationParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("code")]
    public string? Code { get; init; }
}

public record AlarmControlPanelAlarmDisarmParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("code")]
    public string? Code { get; init; }
}

public record AlarmControlPanelAlarmTriggerParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("code")]
    public string? Code { get; init; }
}

public class AlarmoServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public AlarmoServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Arm(AlarmoArmParameters data)
    {
        _haContext.CallService("alarmo", "arm", null, data);
    }

    public void Arm(string @entityId, string? @code = null, string? @mode = null, bool? @skipDelay = null, bool? @force = null)
    {
        _haContext.CallService("alarmo", "arm", null, new
            {
                @entity_id = @entityId, @code, @mode, @skip_delay = @skipDelay, @force
            }
        );
    }

    public void Disarm(AlarmoDisarmParameters data)
    {
        _haContext.CallService("alarmo", "disarm", null, data);
    }

    public void Disarm(string @entityId, string? @code = null)
    {
        _haContext.CallService("alarmo", "disarm", null, new
            {
                @entity_id = @entityId, @code
            }
        );
    }
}

public record AlarmoArmParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("entityId")]
    public string? EntityId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("code")]
    public string? Code { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("mode")]
    public string? Mode { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("skipDelay")]
    public bool? SkipDelay { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("force")]
    public bool? Force { get; init; }
}

public record AlarmoDisarmParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("entityId")]
    public string? EntityId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("code")]
    public string? Code { get; init; }
}

public class AlexaMediaServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public AlexaMediaServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void ClearHistory(AlexaMediaClearHistoryParameters data)
    {
        _haContext.CallService("alexa_media", "clear_history", null, data);
    }

    public void ClearHistory(string? @email = null, string? @entries = null)
    {
        _haContext.CallService("alexa_media", "clear_history", null, new
            {
                @email, @entries
            }
        );
    }

    public void ForceLogout(AlexaMediaForceLogoutParameters data)
    {
        _haContext.CallService("alexa_media", "force_logout", null, data);
    }

    public void ForceLogout(string? @email = null)
    {
        _haContext.CallService("alexa_media", "force_logout", null, new
            {
                @email
            }
        );
    }

    public void UpdateLastCalled(AlexaMediaUpdateLastCalledParameters data)
    {
        _haContext.CallService("alexa_media", "update_last_called", null, data);
    }

    public void UpdateLastCalled(string? @email = null)
    {
        _haContext.CallService("alexa_media", "update_last_called", null, new
            {
                @email
            }
        );
    }
}

public record AlexaMediaClearHistoryParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("email")]
    public string? Email { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("entries")]
    public string? Entries { get; init; }
}

public record AlexaMediaForceLogoutParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("email")]
    public string? Email { get; init; }
}

public record AlexaMediaUpdateLastCalledParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("email")]
    public string? Email { get; init; }
}

public class AudiconnectServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public AudiconnectServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void ExecuteVehicleAction(AudiconnectExecuteVehicleActionParameters data)
    {
        _haContext.CallService("audiconnect", "execute_vehicle_action", null, data);
    }

    public void ExecuteVehicleAction(string? @vin = null, string? @action = null)
    {
        _haContext.CallService("audiconnect", "execute_vehicle_action", null, new
            {
                @vin, @action
            }
        );
    }

    public void RefreshData()
    {
        _haContext.CallService("audiconnect", "refresh_data", null);
    }
}

public record AudiconnectExecuteVehicleActionParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("vin")]
    public string? Vin { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("action")]
    public string? Action { get; init; }
}

public class AutomationServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public AutomationServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Reload()
    {
        _haContext.CallService("automation", "reload", null);
    }

    public void Toggle(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("automation", "toggle", target);
    }

    public void Trigger(NetDaemon.HassModel.Entities.ServiceTarget target, AutomationTriggerParameters data)
    {
        _haContext.CallService("automation", "trigger", target, data);
    }

    public void Trigger(NetDaemon.HassModel.Entities.ServiceTarget target, bool? @skipCondition = null)
    {
        _haContext.CallService("automation", "trigger", target, new
            {
                @skip_condition = @skipCondition
            }
        );
    }

    public void TurnOff(NetDaemon.HassModel.Entities.ServiceTarget target, AutomationTurnOffParameters data)
    {
        _haContext.CallService("automation", "turn_off", target, data);
    }

    public void TurnOff(NetDaemon.HassModel.Entities.ServiceTarget target, bool? @stopActions = null)
    {
        _haContext.CallService("automation", "turn_off", target, new
            {
                @stop_actions = @stopActions
            }
        );
    }

    public void TurnOn(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("automation", "turn_on", target);
    }
}

public record AutomationTriggerParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("skipCondition")]
    public bool? SkipCondition { get; init; }
}

public record AutomationTurnOffParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("stopActions")]
    public bool? StopActions { get; init; }
}

public class CameraServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public CameraServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void DisableMotionDetection(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("camera", "disable_motion_detection", target);
    }

    public void EnableMotionDetection(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("camera", "enable_motion_detection", target);
    }

    public void PlayStream(NetDaemon.HassModel.Entities.ServiceTarget target, CameraPlayStreamParameters data)
    {
        _haContext.CallService("camera", "play_stream", target, data);
    }

    public void PlayStream(NetDaemon.HassModel.Entities.ServiceTarget target, string @mediaPlayer, string? @format = null)
    {
        _haContext.CallService("camera", "play_stream", target, new
            {
                @media_player = @mediaPlayer, @format
            }
        );
    }

    public void Record(NetDaemon.HassModel.Entities.ServiceTarget target, CameraRecordParameters data)
    {
        _haContext.CallService("camera", "record", target, data);
    }

    public void Record(NetDaemon.HassModel.Entities.ServiceTarget target, string @filename, long? @duration = null, long? @lookback = null)
    {
        _haContext.CallService("camera", "record", target, new
            {
                @filename, @duration, @lookback
            }
        );
    }

    public void Snapshot(NetDaemon.HassModel.Entities.ServiceTarget target, CameraSnapshotParameters data)
    {
        _haContext.CallService("camera", "snapshot", target, data);
    }

    public void Snapshot(NetDaemon.HassModel.Entities.ServiceTarget target, string @filename)
    {
        _haContext.CallService("camera", "snapshot", target, new
            {
                @filename
            }
        );
    }

    public void TurnOff(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("camera", "turn_off", target);
    }

    public void TurnOn(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("camera", "turn_on", target);
    }
}

public record CameraPlayStreamParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("mediaPlayer")]
    public string? MediaPlayer { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("format")]
    public string? Format { get; init; }
}

public record CameraRecordParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("filename")]
    public string? Filename { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("duration")]
    public long? Duration { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("lookback")]
    public long? Lookback { get; init; }
}

public record CameraSnapshotParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("filename")]
    public string? Filename { get; init; }
}

public class CircadianLightingServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public CircadianLightingServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void ValuesUpdate()
    {
        _haContext.CallService("circadian_lighting", "values_update", null);
    }
}

public class ClimateServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public ClimateServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void SetAuxHeat(NetDaemon.HassModel.Entities.ServiceTarget target, ClimateSetAuxHeatParameters data)
    {
        _haContext.CallService("climate", "set_aux_heat", target, data);
    }

    public void SetAuxHeat(NetDaemon.HassModel.Entities.ServiceTarget target, bool @auxHeat)
    {
        _haContext.CallService("climate", "set_aux_heat", target, new
            {
                @aux_heat = @auxHeat
            }
        );
    }

    public void SetFanMode(NetDaemon.HassModel.Entities.ServiceTarget target, ClimateSetFanModeParameters data)
    {
        _haContext.CallService("climate", "set_fan_mode", target, data);
    }

    public void SetFanMode(NetDaemon.HassModel.Entities.ServiceTarget target, string @fanMode)
    {
        _haContext.CallService("climate", "set_fan_mode", target, new
            {
                @fan_mode = @fanMode
            }
        );
    }

    public void SetHumidity(NetDaemon.HassModel.Entities.ServiceTarget target, ClimateSetHumidityParameters data)
    {
        _haContext.CallService("climate", "set_humidity", target, data);
    }

    public void SetHumidity(NetDaemon.HassModel.Entities.ServiceTarget target, long @humidity)
    {
        _haContext.CallService("climate", "set_humidity", target, new
            {
                @humidity
            }
        );
    }

    public void SetHvacMode(NetDaemon.HassModel.Entities.ServiceTarget target, ClimateSetHvacModeParameters data)
    {
        _haContext.CallService("climate", "set_hvac_mode", target, data);
    }

    public void SetHvacMode(NetDaemon.HassModel.Entities.ServiceTarget target, string? @hvacMode = null)
    {
        _haContext.CallService("climate", "set_hvac_mode", target, new
            {
                @hvac_mode = @hvacMode
            }
        );
    }

    public void SetPresetMode(NetDaemon.HassModel.Entities.ServiceTarget target, ClimateSetPresetModeParameters data)
    {
        _haContext.CallService("climate", "set_preset_mode", target, data);
    }

    public void SetPresetMode(NetDaemon.HassModel.Entities.ServiceTarget target, string @presetMode)
    {
        _haContext.CallService("climate", "set_preset_mode", target, new
            {
                @preset_mode = @presetMode
            }
        );
    }

    public void SetSwingMode(NetDaemon.HassModel.Entities.ServiceTarget target, ClimateSetSwingModeParameters data)
    {
        _haContext.CallService("climate", "set_swing_mode", target, data);
    }

    public void SetSwingMode(NetDaemon.HassModel.Entities.ServiceTarget target, string @swingMode)
    {
        _haContext.CallService("climate", "set_swing_mode", target, new
            {
                @swing_mode = @swingMode
            }
        );
    }

    public void SetTemperature(NetDaemon.HassModel.Entities.ServiceTarget target, ClimateSetTemperatureParameters data)
    {
        _haContext.CallService("climate", "set_temperature", target, data);
    }

    public void SetTemperature(NetDaemon.HassModel.Entities.ServiceTarget target, double? @temperature = null, double? @targetTempHigh = null, double? @targetTempLow = null, string? @hvacMode = null)
    {
        _haContext.CallService("climate", "set_temperature", target, new
            {
                @temperature, @target_temp_high = @targetTempHigh, @target_temp_low = @targetTempLow, @hvac_mode = @hvacMode
            }
        );
    }

    public void TurnOff(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("climate", "turn_off", target);
    }

    public void TurnOn(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("climate", "turn_on", target);
    }
}

public record ClimateSetAuxHeatParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("auxHeat")]
    public bool? AuxHeat { get; init; }
}

public record ClimateSetFanModeParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("fanMode")]
    public string? FanMode { get; init; }
}

public record ClimateSetHumidityParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("humidity")]
    public long? Humidity { get; init; }
}

public record ClimateSetHvacModeParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("hvacMode")]
    public string? HvacMode { get; init; }
}

public record ClimateSetPresetModeParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("presetMode")]
    public string? PresetMode { get; init; }
}

public record ClimateSetSwingModeParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("swingMode")]
    public string? SwingMode { get; init; }
}

public record ClimateSetTemperatureParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("temperature")]
    public double? Temperature { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("targetTempHigh")]
    public double? TargetTempHigh { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("targetTempLow")]
    public double? TargetTempLow { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("hvacMode")]
    public string? HvacMode { get; init; }
}

public class CloudServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public CloudServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void RemoteConnect()
    {
        _haContext.CallService("cloud", "remote_connect", null);
    }

    public void RemoteDisconnect()
    {
        _haContext.CallService("cloud", "remote_disconnect", null);
    }
}

public class ConfiguratorServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public ConfiguratorServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Configure()
    {
        _haContext.CallService("configurator", "configure", null);
    }
}

public class CounterServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public CounterServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Configure(NetDaemon.HassModel.Entities.ServiceTarget target, CounterConfigureParameters data)
    {
        _haContext.CallService("counter", "configure", target, data);
    }

    public void Configure(NetDaemon.HassModel.Entities.ServiceTarget target, long? @minimum = null, long? @maximum = null, long? @step = null, long? @initial = null, long? @value = null)
    {
        _haContext.CallService("counter", "configure", target, new
            {
                @minimum, @maximum, @step, @initial, @value
            }
        );
    }

    public void Decrement(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("counter", "decrement", target);
    }

    public void Increment(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("counter", "increment", target);
    }

    public void Reset(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("counter", "reset", target);
    }
}

public record CounterConfigureParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("minimum")]
    public long? Minimum { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("maximum")]
    public long? Maximum { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("step")]
    public long? Step { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("initial")]
    public long? Initial { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("value")]
    public long? Value { get; init; }
}

public class CoverServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public CoverServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void CloseCover(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("cover", "close_cover", target);
    }

    public void CloseCoverTilt(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("cover", "close_cover_tilt", target);
    }

    public void OpenCover(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("cover", "open_cover", target);
    }

    public void OpenCoverTilt(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("cover", "open_cover_tilt", target);
    }

    public void SetCoverPosition(NetDaemon.HassModel.Entities.ServiceTarget target, CoverSetCoverPositionParameters data)
    {
        _haContext.CallService("cover", "set_cover_position", target, data);
    }

    public void SetCoverPosition(NetDaemon.HassModel.Entities.ServiceTarget target, long @position)
    {
        _haContext.CallService("cover", "set_cover_position", target, new
            {
                @position
            }
        );
    }

    public void SetCoverTiltPosition(NetDaemon.HassModel.Entities.ServiceTarget target, CoverSetCoverTiltPositionParameters data)
    {
        _haContext.CallService("cover", "set_cover_tilt_position", target, data);
    }

    public void SetCoverTiltPosition(NetDaemon.HassModel.Entities.ServiceTarget target, long @tiltPosition)
    {
        _haContext.CallService("cover", "set_cover_tilt_position", target, new
            {
                @tilt_position = @tiltPosition
            }
        );
    }

    public void StopCover(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("cover", "stop_cover", target);
    }

    public void StopCoverTilt(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("cover", "stop_cover_tilt", target);
    }

    public void Toggle(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("cover", "toggle", target);
    }

    public void ToggleCoverTilt(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("cover", "toggle_cover_tilt", target);
    }
}

public record CoverSetCoverPositionParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("position")]
    public long? Position { get; init; }
}

public record CoverSetCoverTiltPositionParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("tiltPosition")]
    public long? TiltPosition { get; init; }
}

public class DeviceTrackerServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public DeviceTrackerServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void See(DeviceTrackerSeeParameters data)
    {
        _haContext.CallService("device_tracker", "see", null, data);
    }

    public void See(string? @mac = null, string? @devId = null, string? @hostName = null, string? @locationName = null, object? @gps = null, long? @gpsAccuracy = null, long? @battery = null)
    {
        _haContext.CallService("device_tracker", "see", null, new
            {
                @mac, @dev_id = @devId, @host_name = @hostName, @location_name = @locationName, @gps, @gps_accuracy = @gpsAccuracy, @battery
            }
        );
    }
}

public record DeviceTrackerSeeParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("mac")]
    public string? Mac { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("devId")]
    public string? DevId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("hostName")]
    public string? HostName { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("locationName")]
    public string? LocationName { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("gps")]
    public object? Gps { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("gpsAccuracy")]
    public long? GpsAccuracy { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("battery")]
    public long? Battery { get; init; }
}

public class FanServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public FanServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void DecreaseSpeed(NetDaemon.HassModel.Entities.ServiceTarget target, FanDecreaseSpeedParameters data)
    {
        _haContext.CallService("fan", "decrease_speed", target, data);
    }

    public void DecreaseSpeed(NetDaemon.HassModel.Entities.ServiceTarget target, long? @percentageStep = null)
    {
        _haContext.CallService("fan", "decrease_speed", target, new
            {
                @percentage_step = @percentageStep
            }
        );
    }

    public void IncreaseSpeed(NetDaemon.HassModel.Entities.ServiceTarget target, FanIncreaseSpeedParameters data)
    {
        _haContext.CallService("fan", "increase_speed", target, data);
    }

    public void IncreaseSpeed(NetDaemon.HassModel.Entities.ServiceTarget target, long? @percentageStep = null)
    {
        _haContext.CallService("fan", "increase_speed", target, new
            {
                @percentage_step = @percentageStep
            }
        );
    }

    public void Oscillate(NetDaemon.HassModel.Entities.ServiceTarget target, FanOscillateParameters data)
    {
        _haContext.CallService("fan", "oscillate", target, data);
    }

    public void Oscillate(NetDaemon.HassModel.Entities.ServiceTarget target, bool @oscillating)
    {
        _haContext.CallService("fan", "oscillate", target, new
            {
                @oscillating
            }
        );
    }

    public void SetDirection(NetDaemon.HassModel.Entities.ServiceTarget target, FanSetDirectionParameters data)
    {
        _haContext.CallService("fan", "set_direction", target, data);
    }

    public void SetDirection(NetDaemon.HassModel.Entities.ServiceTarget target, string @direction)
    {
        _haContext.CallService("fan", "set_direction", target, new
            {
                @direction
            }
        );
    }

    public void SetPercentage(NetDaemon.HassModel.Entities.ServiceTarget target, FanSetPercentageParameters data)
    {
        _haContext.CallService("fan", "set_percentage", target, data);
    }

    public void SetPercentage(NetDaemon.HassModel.Entities.ServiceTarget target, long @percentage)
    {
        _haContext.CallService("fan", "set_percentage", target, new
            {
                @percentage
            }
        );
    }

    public void SetPresetMode(NetDaemon.HassModel.Entities.ServiceTarget target, FanSetPresetModeParameters data)
    {
        _haContext.CallService("fan", "set_preset_mode", target, data);
    }

    public void SetPresetMode(NetDaemon.HassModel.Entities.ServiceTarget target, string @presetMode)
    {
        _haContext.CallService("fan", "set_preset_mode", target, new
            {
                @preset_mode = @presetMode
            }
        );
    }

    public void SetSpeed(NetDaemon.HassModel.Entities.ServiceTarget target, FanSetSpeedParameters data)
    {
        _haContext.CallService("fan", "set_speed", target, data);
    }

    public void SetSpeed(NetDaemon.HassModel.Entities.ServiceTarget target, string @speed)
    {
        _haContext.CallService("fan", "set_speed", target, new
            {
                @speed
            }
        );
    }

    public void Toggle(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("fan", "toggle", target);
    }

    public void TurnOff(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("fan", "turn_off", target);
    }

    public void TurnOn(NetDaemon.HassModel.Entities.ServiceTarget target, FanTurnOnParameters data)
    {
        _haContext.CallService("fan", "turn_on", target, data);
    }

    public void TurnOn(NetDaemon.HassModel.Entities.ServiceTarget target, string? @speed = null, long? @percentage = null, string? @presetMode = null)
    {
        _haContext.CallService("fan", "turn_on", target, new
            {
                @speed, @percentage, @preset_mode = @presetMode
            }
        );
    }
}

public record FanDecreaseSpeedParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("percentageStep")]
    public long? PercentageStep { get; init; }
}

public record FanIncreaseSpeedParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("percentageStep")]
    public long? PercentageStep { get; init; }
}

public record FanOscillateParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("oscillating")]
    public bool? Oscillating { get; init; }
}

public record FanSetDirectionParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("direction")]
    public string? Direction { get; init; }
}

public record FanSetPercentageParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("percentage")]
    public long? Percentage { get; init; }
}

public record FanSetPresetModeParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("presetMode")]
    public string? PresetMode { get; init; }
}

public record FanSetSpeedParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("speed")]
    public string? Speed { get; init; }
}

public record FanTurnOnParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("speed")]
    public string? Speed { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("percentage")]
    public long? Percentage { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("presetMode")]
    public string? PresetMode { get; init; }
}

public class FfmpegServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public FfmpegServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Restart(FfmpegRestartParameters data)
    {
        _haContext.CallService("ffmpeg", "restart", null, data);
    }

    public void Restart(string? @entityId = null)
    {
        _haContext.CallService("ffmpeg", "restart", null, new
            {
                @entity_id = @entityId
            }
        );
    }

    public void Start(FfmpegStartParameters data)
    {
        _haContext.CallService("ffmpeg", "start", null, data);
    }

    public void Start(string? @entityId = null)
    {
        _haContext.CallService("ffmpeg", "start", null, new
            {
                @entity_id = @entityId
            }
        );
    }

    public void Stop(FfmpegStopParameters data)
    {
        _haContext.CallService("ffmpeg", "stop", null, data);
    }

    public void Stop(string? @entityId = null)
    {
        _haContext.CallService("ffmpeg", "stop", null, new
            {
                @entity_id = @entityId
            }
        );
    }
}

public record FfmpegRestartParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("entityId")]
    public string? EntityId { get; init; }
}

public record FfmpegStartParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("entityId")]
    public string? EntityId { get; init; }
}

public record FfmpegStopParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("entityId")]
    public string? EntityId { get; init; }
}

public class FrontendServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public FrontendServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void ReloadThemes()
    {
        _haContext.CallService("frontend", "reload_themes", null);
    }

    public void SetTheme(FrontendSetThemeParameters data)
    {
        _haContext.CallService("frontend", "set_theme", null, data);
    }

    public void SetTheme(string @name, string? @mode = null)
    {
        _haContext.CallService("frontend", "set_theme", null, new
            {
                @name, @mode
            }
        );
    }
}

public record FrontendSetThemeParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("name")]
    public string? Name { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("mode")]
    public string? Mode { get; init; }
}

public class GenericServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public GenericServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Reload()
    {
        _haContext.CallService("generic", "reload", null);
    }
}

public class GroupServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public GroupServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Reload()
    {
        _haContext.CallService("group", "reload", null);
    }

    public void Remove(GroupRemoveParameters data)
    {
        _haContext.CallService("group", "remove", null, data);
    }

    public void Remove(object @objectId)
    {
        _haContext.CallService("group", "remove", null, new
            {
                @object_id = @objectId
            }
        );
    }

    public void Set(GroupSetParameters data)
    {
        _haContext.CallService("group", "set", null, data);
    }

    public void Set(string @objectId, string? @name = null, string? @icon = null, object? @entities = null, object? @addEntities = null, bool? @all = null)
    {
        _haContext.CallService("group", "set", null, new
            {
                @object_id = @objectId, @name, @icon, @entities, @add_entities = @addEntities, @all
            }
        );
    }
}

public record GroupRemoveParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("objectId")]
    public object? ObjectId { get; init; }
}

public record GroupSetParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("objectId")]
    public string? ObjectId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("name")]
    public string? Name { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("icon")]
    public string? Icon { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("entities")]
    public object? Entities { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("addEntities")]
    public object? AddEntities { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("all")]
    public bool? All { get; init; }
}

public class HistoryStatsServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public HistoryStatsServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Reload()
    {
        _haContext.CallService("history_stats", "reload", null);
    }
}

public class HomeassistantServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public HomeassistantServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void CheckConfig()
    {
        _haContext.CallService("homeassistant", "check_config", null);
    }

    public void ReloadConfigEntry(NetDaemon.HassModel.Entities.ServiceTarget target, HomeassistantReloadConfigEntryParameters data)
    {
        _haContext.CallService("homeassistant", "reload_config_entry", target, data);
    }

    public void ReloadConfigEntry(NetDaemon.HassModel.Entities.ServiceTarget target, string? @entryId = null)
    {
        _haContext.CallService("homeassistant", "reload_config_entry", target, new
            {
                @entry_id = @entryId
            }
        );
    }

    public void ReloadCoreConfig()
    {
        _haContext.CallService("homeassistant", "reload_core_config", null);
    }

    public void Restart()
    {
        _haContext.CallService("homeassistant", "restart", null);
    }

    public void SavePersistentStates()
    {
        _haContext.CallService("homeassistant", "save_persistent_states", null);
    }

    public void SetLocation(HomeassistantSetLocationParameters data)
    {
        _haContext.CallService("homeassistant", "set_location", null, data);
    }

    public void SetLocation(string @latitude, string @longitude)
    {
        _haContext.CallService("homeassistant", "set_location", null, new
            {
                @latitude, @longitude
            }
        );
    }

    public void Stop()
    {
        _haContext.CallService("homeassistant", "stop", null);
    }

    public void Toggle(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("homeassistant", "toggle", target);
    }

    public void TurnOff(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("homeassistant", "turn_off", target);
    }

    public void TurnOn(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("homeassistant", "turn_on", target);
    }

    public void UpdateEntity(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("homeassistant", "update_entity", target);
    }
}

public record HomeassistantReloadConfigEntryParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("entryId")]
    public string? EntryId { get; init; }
}

public record HomeassistantSetLocationParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("latitude")]
    public string? Latitude { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("longitude")]
    public string? Longitude { get; init; }
}

public class HumidifierServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public HumidifierServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void SetHumidity(NetDaemon.HassModel.Entities.ServiceTarget target, HumidifierSetHumidityParameters data)
    {
        _haContext.CallService("humidifier", "set_humidity", target, data);
    }

    public void SetHumidity(NetDaemon.HassModel.Entities.ServiceTarget target, long @humidity)
    {
        _haContext.CallService("humidifier", "set_humidity", target, new
            {
                @humidity
            }
        );
    }

    public void SetMode(NetDaemon.HassModel.Entities.ServiceTarget target, HumidifierSetModeParameters data)
    {
        _haContext.CallService("humidifier", "set_mode", target, data);
    }

    public void SetMode(NetDaemon.HassModel.Entities.ServiceTarget target, string @mode)
    {
        _haContext.CallService("humidifier", "set_mode", target, new
            {
                @mode
            }
        );
    }

    public void Toggle(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("humidifier", "toggle", target);
    }

    public void TurnOff(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("humidifier", "turn_off", target);
    }

    public void TurnOn(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("humidifier", "turn_on", target);
    }
}

public record HumidifierSetHumidityParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("humidity")]
    public long? Humidity { get; init; }
}

public record HumidifierSetModeParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("mode")]
    public string? Mode { get; init; }
}

public class InputBooleanServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public InputBooleanServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Reload()
    {
        _haContext.CallService("input_boolean", "reload", null);
    }

    public void Toggle(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("input_boolean", "toggle", target);
    }

    public void TurnOff(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("input_boolean", "turn_off", target);
    }

    public void TurnOn(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("input_boolean", "turn_on", target);
    }
}

public class InputDatetimeServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public InputDatetimeServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Reload()
    {
        _haContext.CallService("input_datetime", "reload", null);
    }

    public void SetDatetime(NetDaemon.HassModel.Entities.ServiceTarget target, InputDatetimeSetDatetimeParameters data)
    {
        _haContext.CallService("input_datetime", "set_datetime", target, data);
    }

    public void SetDatetime(NetDaemon.HassModel.Entities.ServiceTarget target, string? @date = null, DateTime? @time = null, string? @datetime = null, long? @timestamp = null)
    {
        _haContext.CallService("input_datetime", "set_datetime", target, new
            {
                @date, @time, @datetime, @timestamp
            }
        );
    }
}

public record InputDatetimeSetDatetimeParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("date")]
    public string? Date { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("time")]
    public DateTime? Time { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("datetime")]
    public string? Datetime { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("timestamp")]
    public long? Timestamp { get; init; }
}

public class InputNumberServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public InputNumberServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Decrement(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("input_number", "decrement", target);
    }

    public void Increment(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("input_number", "increment", target);
    }

    public void Reload()
    {
        _haContext.CallService("input_number", "reload", null);
    }

    public void SetValue(NetDaemon.HassModel.Entities.ServiceTarget target, InputNumberSetValueParameters data)
    {
        _haContext.CallService("input_number", "set_value", target, data);
    }

    public void SetValue(NetDaemon.HassModel.Entities.ServiceTarget target, double @value)
    {
        _haContext.CallService("input_number", "set_value", target, new
            {
                @value
            }
        );
    }
}

public record InputNumberSetValueParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("value")]
    public double? Value { get; init; }
}

public class InputSelectServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public InputSelectServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Reload()
    {
        _haContext.CallService("input_select", "reload", null);
    }

    public void SelectFirst(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("input_select", "select_first", target);
    }

    public void SelectLast(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("input_select", "select_last", target);
    }

    public void SelectNext(NetDaemon.HassModel.Entities.ServiceTarget target, InputSelectSelectNextParameters data)
    {
        _haContext.CallService("input_select", "select_next", target, data);
    }

    public void SelectNext(NetDaemon.HassModel.Entities.ServiceTarget target, bool? @cycle = null)
    {
        _haContext.CallService("input_select", "select_next", target, new
            {
                @cycle
            }
        );
    }

    public void SelectOption(NetDaemon.HassModel.Entities.ServiceTarget target, InputSelectSelectOptionParameters data)
    {
        _haContext.CallService("input_select", "select_option", target, data);
    }

    public void SelectOption(NetDaemon.HassModel.Entities.ServiceTarget target, string @option)
    {
        _haContext.CallService("input_select", "select_option", target, new
            {
                @option
            }
        );
    }

    public void SelectPrevious(NetDaemon.HassModel.Entities.ServiceTarget target, InputSelectSelectPreviousParameters data)
    {
        _haContext.CallService("input_select", "select_previous", target, data);
    }

    public void SelectPrevious(NetDaemon.HassModel.Entities.ServiceTarget target, bool? @cycle = null)
    {
        _haContext.CallService("input_select", "select_previous", target, new
            {
                @cycle
            }
        );
    }

    public void SetOptions(NetDaemon.HassModel.Entities.ServiceTarget target, InputSelectSetOptionsParameters data)
    {
        _haContext.CallService("input_select", "set_options", target, data);
    }

    public void SetOptions(NetDaemon.HassModel.Entities.ServiceTarget target, object @options)
    {
        _haContext.CallService("input_select", "set_options", target, new
            {
                @options
            }
        );
    }
}

public record InputSelectSelectNextParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("cycle")]
    public bool? Cycle { get; init; }
}

public record InputSelectSelectOptionParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("option")]
    public string? Option { get; init; }
}

public record InputSelectSelectPreviousParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("cycle")]
    public bool? Cycle { get; init; }
}

public record InputSelectSetOptionsParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("options")]
    public object? Options { get; init; }
}

public class InputTextServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public InputTextServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Reload()
    {
        _haContext.CallService("input_text", "reload", null);
    }

    public void SetValue(NetDaemon.HassModel.Entities.ServiceTarget target, InputTextSetValueParameters data)
    {
        _haContext.CallService("input_text", "set_value", target, data);
    }

    public void SetValue(NetDaemon.HassModel.Entities.ServiceTarget target, string @value)
    {
        _haContext.CallService("input_text", "set_value", target, new
            {
                @value
            }
        );
    }
}

public record InputTextSetValueParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("value")]
    public string? Value { get; init; }
}

public class LightServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public LightServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Toggle(NetDaemon.HassModel.Entities.ServiceTarget target, LightToggleParameters data)
    {
        _haContext.CallService("light", "toggle", target, data);
    }

    public void Toggle(NetDaemon.HassModel.Entities.ServiceTarget target, long? @transition = null, object? @rgbColor = null, string? @colorName = null, object? @hsColor = null, object? @xyColor = null, long? @colorTemp = null, long? @kelvin = null, long? @whiteValue = null, long? @brightness = null, long? @brightnessPct = null, string? @profile = null, string? @flash = null, string? @effect = null)
    {
        _haContext.CallService("light", "toggle", target, new
            {
                @transition, @rgb_color = @rgbColor, @color_name = @colorName, @hs_color = @hsColor, @xy_color = @xyColor, @color_temp = @colorTemp, @kelvin, @white_value = @whiteValue, @brightness, @brightness_pct = @brightnessPct, @profile, @flash, @effect
            }
        );
    }

    public void TurnOff(NetDaemon.HassModel.Entities.ServiceTarget target, LightTurnOffParameters data)
    {
        _haContext.CallService("light", "turn_off", target, data);
    }

    public void TurnOff(NetDaemon.HassModel.Entities.ServiceTarget target, long? @transition = null, string? @flash = null)
    {
        _haContext.CallService("light", "turn_off", target, new
            {
                @transition, @flash
            }
        );
    }

    public void TurnOn(NetDaemon.HassModel.Entities.ServiceTarget target, LightTurnOnParameters data)
    {
        _haContext.CallService("light", "turn_on", target, data);
    }

    public void TurnOn(NetDaemon.HassModel.Entities.ServiceTarget target, long? @transition = null, object? @rgbColor = null, object? @rgbwColor = null, object? @rgbwwColor = null, string? @colorName = null, object? @hsColor = null, object? @xyColor = null, long? @colorTemp = null, long? @kelvin = null, long? @brightness = null, long? @brightnessPct = null, long? @brightnessStep = null, long? @brightnessStepPct = null, long? @white = null, string? @profile = null, string? @flash = null, string? @effect = null)
    {
        _haContext.CallService("light", "turn_on", target, new
            {
                @transition, @rgb_color = @rgbColor, @rgbw_color = @rgbwColor, @rgbww_color = @rgbwwColor, @color_name = @colorName, @hs_color = @hsColor, @xy_color = @xyColor, @color_temp = @colorTemp, @kelvin, @brightness, @brightness_pct = @brightnessPct, @brightness_step = @brightnessStep, @brightness_step_pct = @brightnessStepPct, @white, @profile, @flash, @effect
            }
        );
    }
}

public record LightToggleParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("transition")]
    public long? Transition { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("rgbColor")]
    public object? RgbColor { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("colorName")]
    public string? ColorName { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("hsColor")]
    public object? HsColor { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("xyColor")]
    public object? XyColor { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("colorTemp")]
    public long? ColorTemp { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("kelvin")]
    public long? Kelvin { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("whiteValue")]
    public long? WhiteValue { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("brightness")]
    public long? Brightness { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("brightnessPct")]
    public long? BrightnessPct { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("profile")]
    public string? Profile { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("flash")]
    public string? Flash { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("effect")]
    public string? Effect { get; init; }
}

public record LightTurnOffParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("transition")]
    public long? Transition { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("flash")]
    public string? Flash { get; init; }
}

public record LightTurnOnParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("transition")]
    public long? Transition { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("rgbColor")]
    public object? RgbColor { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("rgbwColor")]
    public object? RgbwColor { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("rgbwwColor")]
    public object? RgbwwColor { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("colorName")]
    public string? ColorName { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("hsColor")]
    public object? HsColor { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("xyColor")]
    public object? XyColor { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("colorTemp")]
    public long? ColorTemp { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("kelvin")]
    public long? Kelvin { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("brightness")]
    public long? Brightness { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("brightnessPct")]
    public long? BrightnessPct { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("brightnessStep")]
    public long? BrightnessStep { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("brightnessStepPct")]
    public long? BrightnessStepPct { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("white")]
    public long? White { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("profile")]
    public string? Profile { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("flash")]
    public string? Flash { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("effect")]
    public string? Effect { get; init; }
}

public class LockServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public LockServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Lock(NetDaemon.HassModel.Entities.ServiceTarget target, LockLockParameters data)
    {
        _haContext.CallService("lock", "lock", target, data);
    }

    public void Lock(NetDaemon.HassModel.Entities.ServiceTarget target, string? @code = null)
    {
        _haContext.CallService("lock", "lock", target, new
            {
                @code
            }
        );
    }

    public void Open(NetDaemon.HassModel.Entities.ServiceTarget target, LockOpenParameters data)
    {
        _haContext.CallService("lock", "open", target, data);
    }

    public void Open(NetDaemon.HassModel.Entities.ServiceTarget target, string? @code = null)
    {
        _haContext.CallService("lock", "open", target, new
            {
                @code
            }
        );
    }

    public void Unlock(NetDaemon.HassModel.Entities.ServiceTarget target, LockUnlockParameters data)
    {
        _haContext.CallService("lock", "unlock", target, data);
    }

    public void Unlock(NetDaemon.HassModel.Entities.ServiceTarget target, string? @code = null)
    {
        _haContext.CallService("lock", "unlock", target, new
            {
                @code
            }
        );
    }
}

public record LockLockParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("code")]
    public string? Code { get; init; }
}

public record LockOpenParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("code")]
    public string? Code { get; init; }
}

public record LockUnlockParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("code")]
    public string? Code { get; init; }
}

public class LogbookServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public LogbookServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Log(LogbookLogParameters data)
    {
        _haContext.CallService("logbook", "log", null, data);
    }

    public void Log(string @name, string @message, string? @entityId = null, string? @domain = null)
    {
        _haContext.CallService("logbook", "log", null, new
            {
                @name, @message, @entity_id = @entityId, @domain
            }
        );
    }
}

public record LogbookLogParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("name")]
    public string? Name { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("entityId")]
    public string? EntityId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("domain")]
    public string? Domain { get; init; }
}

public class LoggerServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public LoggerServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void SetDefaultLevel(LoggerSetDefaultLevelParameters data)
    {
        _haContext.CallService("logger", "set_default_level", null, data);
    }

    public void SetDefaultLevel(string? @level = null)
    {
        _haContext.CallService("logger", "set_default_level", null, new
            {
                @level
            }
        );
    }

    public void SetLevel()
    {
        _haContext.CallService("logger", "set_level", null);
    }
}

public record LoggerSetDefaultLevelParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("level")]
    public string? Level { get; init; }
}

public class MediaPlayerServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public MediaPlayerServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void ClearPlaylist(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("media_player", "clear_playlist", target);
    }

    public void Join(NetDaemon.HassModel.Entities.ServiceTarget target, MediaPlayerJoinParameters data)
    {
        _haContext.CallService("media_player", "join", target, data);
    }

    public void Join(NetDaemon.HassModel.Entities.ServiceTarget target, object? @groupMembers = null)
    {
        _haContext.CallService("media_player", "join", target, new
            {
                @group_members = @groupMembers
            }
        );
    }

    public void MediaNextTrack(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("media_player", "media_next_track", target);
    }

    public void MediaPause(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("media_player", "media_pause", target);
    }

    public void MediaPlay(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("media_player", "media_play", target);
    }

    public void MediaPlayPause(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("media_player", "media_play_pause", target);
    }

    public void MediaPreviousTrack(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("media_player", "media_previous_track", target);
    }

    public void MediaSeek(NetDaemon.HassModel.Entities.ServiceTarget target, MediaPlayerMediaSeekParameters data)
    {
        _haContext.CallService("media_player", "media_seek", target, data);
    }

    public void MediaSeek(NetDaemon.HassModel.Entities.ServiceTarget target, double @seekPosition)
    {
        _haContext.CallService("media_player", "media_seek", target, new
            {
                @seek_position = @seekPosition
            }
        );
    }

    public void MediaStop(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("media_player", "media_stop", target);
    }

    public void PlayMedia(NetDaemon.HassModel.Entities.ServiceTarget target, MediaPlayerPlayMediaParameters data)
    {
        _haContext.CallService("media_player", "play_media", target, data);
    }

    public void PlayMedia(NetDaemon.HassModel.Entities.ServiceTarget target, string @mediaContentId, string @mediaContentType)
    {
        _haContext.CallService("media_player", "play_media", target, new
            {
                @media_content_id = @mediaContentId, @media_content_type = @mediaContentType
            }
        );
    }

    public void RepeatSet(NetDaemon.HassModel.Entities.ServiceTarget target, MediaPlayerRepeatSetParameters data)
    {
        _haContext.CallService("media_player", "repeat_set", target, data);
    }

    public void RepeatSet(NetDaemon.HassModel.Entities.ServiceTarget target, string @repeat)
    {
        _haContext.CallService("media_player", "repeat_set", target, new
            {
                @repeat
            }
        );
    }

    public void SelectSoundMode(NetDaemon.HassModel.Entities.ServiceTarget target, MediaPlayerSelectSoundModeParameters data)
    {
        _haContext.CallService("media_player", "select_sound_mode", target, data);
    }

    public void SelectSoundMode(NetDaemon.HassModel.Entities.ServiceTarget target, string? @soundMode = null)
    {
        _haContext.CallService("media_player", "select_sound_mode", target, new
            {
                @sound_mode = @soundMode
            }
        );
    }

    public void SelectSource(NetDaemon.HassModel.Entities.ServiceTarget target, MediaPlayerSelectSourceParameters data)
    {
        _haContext.CallService("media_player", "select_source", target, data);
    }

    public void SelectSource(NetDaemon.HassModel.Entities.ServiceTarget target, string @source)
    {
        _haContext.CallService("media_player", "select_source", target, new
            {
                @source
            }
        );
    }

    public void ShuffleSet(NetDaemon.HassModel.Entities.ServiceTarget target, MediaPlayerShuffleSetParameters data)
    {
        _haContext.CallService("media_player", "shuffle_set", target, data);
    }

    public void ShuffleSet(NetDaemon.HassModel.Entities.ServiceTarget target, bool @shuffle)
    {
        _haContext.CallService("media_player", "shuffle_set", target, new
            {
                @shuffle
            }
        );
    }

    public void Toggle(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("media_player", "toggle", target);
    }

    public void TurnOff(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("media_player", "turn_off", target);
    }

    public void TurnOn(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("media_player", "turn_on", target);
    }

    public void Unjoin(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("media_player", "unjoin", target);
    }

    public void VolumeDown(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("media_player", "volume_down", target);
    }

    public void VolumeMute(NetDaemon.HassModel.Entities.ServiceTarget target, MediaPlayerVolumeMuteParameters data)
    {
        _haContext.CallService("media_player", "volume_mute", target, data);
    }

    public void VolumeMute(NetDaemon.HassModel.Entities.ServiceTarget target, bool @isVolumeMuted)
    {
        _haContext.CallService("media_player", "volume_mute", target, new
            {
                @is_volume_muted = @isVolumeMuted
            }
        );
    }

    public void VolumeSet(NetDaemon.HassModel.Entities.ServiceTarget target, MediaPlayerVolumeSetParameters data)
    {
        _haContext.CallService("media_player", "volume_set", target, data);
    }

    public void VolumeSet(NetDaemon.HassModel.Entities.ServiceTarget target, double @volumeLevel)
    {
        _haContext.CallService("media_player", "volume_set", target, new
            {
                @volume_level = @volumeLevel
            }
        );
    }

    public void VolumeUp(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("media_player", "volume_up", target);
    }
}

public record MediaPlayerJoinParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("groupMembers")]
    public object? GroupMembers { get; init; }
}

public record MediaPlayerMediaSeekParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("seekPosition")]
    public double? SeekPosition { get; init; }
}

public record MediaPlayerPlayMediaParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("mediaContentId")]
    public string? MediaContentId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("mediaContentType")]
    public string? MediaContentType { get; init; }
}

public record MediaPlayerRepeatSetParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("repeat")]
    public string? Repeat { get; init; }
}

public record MediaPlayerSelectSoundModeParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("soundMode")]
    public string? SoundMode { get; init; }
}

public record MediaPlayerSelectSourceParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("source")]
    public string? Source { get; init; }
}

public record MediaPlayerShuffleSetParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("shuffle")]
    public bool? Shuffle { get; init; }
}

public record MediaPlayerVolumeMuteParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("isVolumeMuted")]
    public bool? IsVolumeMuted { get; init; }
}

public record MediaPlayerVolumeSetParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("volumeLevel")]
    public double? VolumeLevel { get; init; }
}

public class MelcloudServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public MelcloudServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void SetVaneHorizontal(NetDaemon.HassModel.Entities.ServiceTarget target, MelcloudSetVaneHorizontalParameters data)
    {
        _haContext.CallService("melcloud", "set_vane_horizontal", target, data);
    }

    public void SetVaneHorizontal(NetDaemon.HassModel.Entities.ServiceTarget target, string @position)
    {
        _haContext.CallService("melcloud", "set_vane_horizontal", target, new
            {
                @position
            }
        );
    }

    public void SetVaneVertical(NetDaemon.HassModel.Entities.ServiceTarget target, MelcloudSetVaneVerticalParameters data)
    {
        _haContext.CallService("melcloud", "set_vane_vertical", target, data);
    }

    public void SetVaneVertical(NetDaemon.HassModel.Entities.ServiceTarget target, string @position)
    {
        _haContext.CallService("melcloud", "set_vane_vertical", target, new
            {
                @position
            }
        );
    }
}

public record MelcloudSetVaneHorizontalParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("position")]
    public string? Position { get; init; }
}

public record MelcloudSetVaneVerticalParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("position")]
    public string? Position { get; init; }
}

public class MinMaxServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public MinMaxServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Reload()
    {
        _haContext.CallService("min_max", "reload", null);
    }
}

public class MqttServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public MqttServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Dump(MqttDumpParameters data)
    {
        _haContext.CallService("mqtt", "dump", null, data);
    }

    public void Dump(string? @topic = null, long? @duration = null)
    {
        _haContext.CallService("mqtt", "dump", null, new
            {
                @topic, @duration
            }
        );
    }

    public void Publish(MqttPublishParameters data)
    {
        _haContext.CallService("mqtt", "publish", null, data);
    }

    public void Publish(string @topic, string? @payload = null, object? @payloadTemplate = null, string? @qos = null, bool? @retain = null)
    {
        _haContext.CallService("mqtt", "publish", null, new
            {
                @topic, @payload, @payload_template = @payloadTemplate, @qos, @retain
            }
        );
    }

    public void Reload()
    {
        _haContext.CallService("mqtt", "reload", null);
    }
}

public record MqttDumpParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("topic")]
    public string? Topic { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("duration")]
    public long? Duration { get; init; }
}

public record MqttPublishParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("topic")]
    public string? Topic { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("payload")]
    public string? Payload { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("payloadTemplate")]
    public object? PayloadTemplate { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("qos")]
    public string? Qos { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("retain")]
    public bool? Retain { get; init; }
}

public class NetdaemonServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public NetdaemonServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void EntityCreate(NetdaemonEntityCreateParameters data)
    {
        _haContext.CallService("netdaemon", "entity_create", null, data);
    }

    public void EntityCreate(string? @entityId = null, string? @state = null, string? @icon = null, string? @unit = null, string? @attributes = null)
    {
        _haContext.CallService("netdaemon", "entity_create", null, new
            {
                @entity_id = @entityId, @state, @icon, @unit, @attributes
            }
        );
    }

    public void EntityRemove(NetdaemonEntityRemoveParameters data)
    {
        _haContext.CallService("netdaemon", "entity_remove", null, data);
    }

    public void EntityRemove(string? @entityId = null)
    {
        _haContext.CallService("netdaemon", "entity_remove", null, new
            {
                @entity_id = @entityId
            }
        );
    }

    public void EntityUpdate(NetdaemonEntityUpdateParameters data)
    {
        _haContext.CallService("netdaemon", "entity_update", null, data);
    }

    public void EntityUpdate(string? @entityId = null, string? @state = null, string? @icon = null, string? @unit = null, string? @attributes = null)
    {
        _haContext.CallService("netdaemon", "entity_update", null, new
            {
                @entity_id = @entityId, @state, @icon, @unit, @attributes
            }
        );
    }

    public void NotificationengineNotify()
    {
        _haContext.CallService("netdaemon", "notificationengine_notify", null);
    }

    public void RegisterService(NetdaemonRegisterServiceParameters data)
    {
        _haContext.CallService("netdaemon", "register_service", null, data);
    }

    public void RegisterService(string? @class = null, string? @method = null)
    {
        _haContext.CallService("netdaemon", "register_service", null, new
            {
                @class, @method
            }
        );
    }

    public void ReloadApps()
    {
        _haContext.CallService("netdaemon", "reload_apps", null);
    }
}

public record NetdaemonEntityCreateParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("entityId")]
    public string? EntityId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("state")]
    public string? State { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("icon")]
    public string? Icon { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("unit")]
    public string? Unit { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("attributes")]
    public string? Attributes { get; init; }
}

public record NetdaemonEntityRemoveParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("entityId")]
    public string? EntityId { get; init; }
}

public record NetdaemonEntityUpdateParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("entityId")]
    public string? EntityId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("state")]
    public string? State { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("icon")]
    public string? Icon { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("unit")]
    public string? Unit { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("attributes")]
    public string? Attributes { get; init; }
}

public record NetdaemonRegisterServiceParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("class")]
    public string? Class { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("method")]
    public string? Method { get; init; }
}

public class NotifyServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public NotifyServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void AlexaMedia(NotifyAlexaMediaParameters data)
    {
        _haContext.CallService("notify", "alexa_media", null, data);
    }

    public void AlexaMedia(string @message, string? @title = null, object? @target = null, object? @data = null)
    {
        _haContext.CallService("notify", "alexa_media", null, new
            {
                @message, @title, @target, @data
            }
        );
    }

    public void AlexaMediaAaron(NotifyAlexaMediaAaronParameters data)
    {
        _haContext.CallService("notify", "alexa_media_aaron", null, data);
    }

    public void AlexaMediaAaron(string @message, string? @title = null, object? @target = null, object? @data = null)
    {
        _haContext.CallService("notify", "alexa_media_aaron", null, new
            {
                @message, @title, @target, @data
            }
        );
    }

    public void AlexaMediaBoseQc35Ii(NotifyAlexaMediaBoseQc35IiParameters data)
    {
        _haContext.CallService("notify", "alexa_media_bose_qc35_ii", null, data);
    }

    public void AlexaMediaBoseQc35Ii(string @message, string? @title = null, object? @target = null, object? @data = null)
    {
        _haContext.CallService("notify", "alexa_media_bose_qc35_ii", null, new
            {
                @message, @title, @target, @data
            }
        );
    }

    public void AlexaMediaDining(NotifyAlexaMediaDiningParameters data)
    {
        _haContext.CallService("notify", "alexa_media_dining", null, data);
    }

    public void AlexaMediaDining(string @message, string? @title = null, object? @target = null, object? @data = null)
    {
        _haContext.CallService("notify", "alexa_media_dining", null, new
            {
                @message, @title, @target, @data
            }
        );
    }

    public void AlexaMediaDownstairs(NotifyAlexaMediaDownstairsParameters data)
    {
        _haContext.CallService("notify", "alexa_media_downstairs", null, data);
    }

    public void AlexaMediaDownstairs(string @message, string? @title = null, object? @target = null, object? @data = null)
    {
        _haContext.CallService("notify", "alexa_media_downstairs", null, new
            {
                @message, @title, @target, @data
            }
        );
    }

    public void AlexaMediaEugeneS2ndEchoDot(NotifyAlexaMediaEugeneS2ndEchoDotParameters data)
    {
        _haContext.CallService("notify", "alexa_media_eugene_s_2nd_echo_dot", null, data);
    }

    public void AlexaMediaEugeneS2ndEchoDot(string @message, string? @title = null, object? @target = null, object? @data = null)
    {
        _haContext.CallService("notify", "alexa_media_eugene_s_2nd_echo_dot", null, new
            {
                @message, @title, @target, @data
            }
        );
    }

    public void AlexaMediaEugeneS5thEchoDot(NotifyAlexaMediaEugeneS5thEchoDotParameters data)
    {
        _haContext.CallService("notify", "alexa_media_eugene_s_5th_echo_dot", null, data);
    }

    public void AlexaMediaEugeneS5thEchoDot(string @message, string? @title = null, object? @target = null, object? @data = null)
    {
        _haContext.CallService("notify", "alexa_media_eugene_s_5th_echo_dot", null, new
            {
                @message, @title, @target, @data
            }
        );
    }

    public void AlexaMediaEugeneSFire(NotifyAlexaMediaEugeneSFireParameters data)
    {
        _haContext.CallService("notify", "alexa_media_eugene_s_fire", null, data);
    }

    public void AlexaMediaEugeneSFire(string @message, string? @title = null, object? @target = null, object? @data = null)
    {
        _haContext.CallService("notify", "alexa_media_eugene_s_fire", null, new
            {
                @message, @title, @target, @data
            }
        );
    }

    public void AlexaMediaEugeneSLgOledWebos2021Tv(NotifyAlexaMediaEugeneSLgOledWebos2021TvParameters data)
    {
        _haContext.CallService("notify", "alexa_media_eugene_s_lg_oled_webos_2021_tv", null, data);
    }

    public void AlexaMediaEugeneSLgOledWebos2021Tv(string @message, string? @title = null, object? @target = null, object? @data = null)
    {
        _haContext.CallService("notify", "alexa_media_eugene_s_lg_oled_webos_2021_tv", null, new
            {
                @message, @title, @target, @data
            }
        );
    }

    public void AlexaMediaEugeneSSonosArc(NotifyAlexaMediaEugeneSSonosArcParameters data)
    {
        _haContext.CallService("notify", "alexa_media_eugene_s_sonos_arc", null, data);
    }

    public void AlexaMediaEugeneSSonosArc(string @message, string? @title = null, object? @target = null, object? @data = null)
    {
        _haContext.CallService("notify", "alexa_media_eugene_s_sonos_arc", null, new
            {
                @message, @title, @target, @data
            }
        );
    }

    public void AlexaMediaEverywhere2(NotifyAlexaMediaEverywhere2Parameters data)
    {
        _haContext.CallService("notify", "alexa_media_everywhere_2", null, data);
    }

    public void AlexaMediaEverywhere2(string @message, string? @title = null, object? @target = null, object? @data = null)
    {
        _haContext.CallService("notify", "alexa_media_everywhere_2", null, new
            {
                @message, @title, @target, @data
            }
        );
    }

    public void AlexaMediaJayden(NotifyAlexaMediaJaydenParameters data)
    {
        _haContext.CallService("notify", "alexa_media_jayden", null, data);
    }

    public void AlexaMediaJayden(string @message, string? @title = null, object? @target = null, object? @data = null)
    {
        _haContext.CallService("notify", "alexa_media_jayden", null, new
            {
                @message, @title, @target, @data
            }
        );
    }

    public void AlexaMediaKitchen(NotifyAlexaMediaKitchenParameters data)
    {
        _haContext.CallService("notify", "alexa_media_kitchen", null, data);
    }

    public void AlexaMediaKitchen(string @message, string? @title = null, object? @target = null, object? @data = null)
    {
        _haContext.CallService("notify", "alexa_media_kitchen", null, new
            {
                @message, @title, @target, @data
            }
        );
    }

    public void AlexaMediaLastCalled(NotifyAlexaMediaLastCalledParameters data)
    {
        _haContext.CallService("notify", "alexa_media_last_called", null, data);
    }

    public void AlexaMediaLastCalled(string @message, string? @title = null, object? @target = null, object? @data = null)
    {
        _haContext.CallService("notify", "alexa_media_last_called", null, new
            {
                @message, @title, @target, @data
            }
        );
    }

    public void AlexaMediaLoungeGroup(NotifyAlexaMediaLoungeGroupParameters data)
    {
        _haContext.CallService("notify", "alexa_media_lounge_group", null, data);
    }

    public void AlexaMediaLoungeGroup(string @message, string? @title = null, object? @target = null, object? @data = null)
    {
        _haContext.CallService("notify", "alexa_media_lounge_group", null, new
            {
                @message, @title, @target, @data
            }
        );
    }

    public void AlexaMediaLoungeSonos(NotifyAlexaMediaLoungeSonosParameters data)
    {
        _haContext.CallService("notify", "alexa_media_lounge_sonos", null, data);
    }

    public void AlexaMediaLoungeSonos(string @message, string? @title = null, object? @target = null, object? @data = null)
    {
        _haContext.CallService("notify", "alexa_media_lounge_sonos", null, new
            {
                @message, @title, @target, @data
            }
        );
    }

    public void AlexaMediaMaster(NotifyAlexaMediaMasterParameters data)
    {
        _haContext.CallService("notify", "alexa_media_master", null, data);
    }

    public void AlexaMediaMaster(string @message, string? @title = null, object? @target = null, object? @data = null)
    {
        _haContext.CallService("notify", "alexa_media_master", null, new
            {
                @message, @title, @target, @data
            }
        );
    }

    public void AlexaMediaMasterTvAlexa(NotifyAlexaMediaMasterTvAlexaParameters data)
    {
        _haContext.CallService("notify", "alexa_media_master_tv_alexa", null, data);
    }

    public void AlexaMediaMasterTvAlexa(string @message, string? @title = null, object? @target = null, object? @data = null)
    {
        _haContext.CallService("notify", "alexa_media_master_tv_alexa", null, new
            {
                @message, @title, @target, @data
            }
        );
    }

    public void AlexaMediaOffice(NotifyAlexaMediaOfficeParameters data)
    {
        _haContext.CallService("notify", "alexa_media_office", null, data);
    }

    public void AlexaMediaOffice(string @message, string? @title = null, object? @target = null, object? @data = null)
    {
        _haContext.CallService("notify", "alexa_media_office", null, new
            {
                @message, @title, @target, @data
            }
        );
    }

    public void AlexaMediaPlayroom(NotifyAlexaMediaPlayroomParameters data)
    {
        _haContext.CallService("notify", "alexa_media_playroom", null, data);
    }

    public void AlexaMediaPlayroom(string @message, string? @title = null, object? @target = null, object? @data = null)
    {
        _haContext.CallService("notify", "alexa_media_playroom", null, new
            {
                @message, @title, @target, @data
            }
        );
    }

    public void AlexaMediaThisDevice(NotifyAlexaMediaThisDeviceParameters data)
    {
        _haContext.CallService("notify", "alexa_media_this_device", null, data);
    }

    public void AlexaMediaThisDevice(string @message, string? @title = null, object? @target = null, object? @data = null)
    {
        _haContext.CallService("notify", "alexa_media_this_device", null, new
            {
                @message, @title, @target, @data
            }
        );
    }

    public void AlexaMediaThisDevice2(NotifyAlexaMediaThisDevice2Parameters data)
    {
        _haContext.CallService("notify", "alexa_media_this_device_2", null, data);
    }

    public void AlexaMediaThisDevice2(string @message, string? @title = null, object? @target = null, object? @data = null)
    {
        _haContext.CallService("notify", "alexa_media_this_device_2", null, new
            {
                @message, @title, @target, @data
            }
        );
    }

    public void AlexaMediaUpstairs(NotifyAlexaMediaUpstairsParameters data)
    {
        _haContext.CallService("notify", "alexa_media_upstairs", null, data);
    }

    public void AlexaMediaUpstairs(string @message, string? @title = null, object? @target = null, object? @data = null)
    {
        _haContext.CallService("notify", "alexa_media_upstairs", null, new
            {
                @message, @title, @target, @data
            }
        );
    }

    public void Eugene(NotifyEugeneParameters data)
    {
        _haContext.CallService("notify", "eugene", null, data);
    }

    public void Eugene(string @message, string? @title = null, object? @target = null, object? @data = null)
    {
        _haContext.CallService("notify", "eugene", null, new
            {
                @message, @title, @target, @data
            }
        );
    }

    public void Hailey(NotifyHaileyParameters data)
    {
        _haContext.CallService("notify", "hailey", null, data);
    }

    public void Hailey(string @message, string? @title = null, object? @target = null, object? @data = null)
    {
        _haContext.CallService("notify", "hailey", null, new
            {
                @message, @title, @target, @data
            }
        );
    }

    public void LoungeTv(NotifyLoungeTvParameters data)
    {
        _haContext.CallService("notify", "lounge_tv", null, data);
    }

    public void LoungeTv(string @message, string? @title = null, object? @target = null, object? @data = null)
    {
        _haContext.CallService("notify", "lounge_tv", null, new
            {
                @message, @title, @target, @data
            }
        );
    }

    public void MasterTv(NotifyMasterTvParameters data)
    {
        _haContext.CallService("notify", "master_tv", null, data);
    }

    public void MasterTv(string @message, string? @title = null, object? @target = null, object? @data = null)
    {
        _haContext.CallService("notify", "master_tv", null, new
            {
                @message, @title, @target, @data
            }
        );
    }

    public void MobileAppEugenesIphone(NotifyMobileAppEugenesIphoneParameters data)
    {
        _haContext.CallService("notify", "mobile_app_eugenes_iphone", null, data);
    }

    public void MobileAppEugenesIphone(string @message, string? @title = null, object? @target = null, object? @data = null)
    {
        _haContext.CallService("notify", "mobile_app_eugenes_iphone", null, new
            {
                @message, @title, @target, @data
            }
        );
    }

    public void MobileAppHaileySIphone(NotifyMobileAppHaileySIphoneParameters data)
    {
        _haContext.CallService("notify", "mobile_app_hailey_s_iphone", null, data);
    }

    public void MobileAppHaileySIphone(string @message, string? @title = null, object? @target = null, object? @data = null)
    {
        _haContext.CallService("notify", "mobile_app_hailey_s_iphone", null, new
            {
                @message, @title, @target, @data
            }
        );
    }

    public void MobileAppHaileysMacbookAir(NotifyMobileAppHaileysMacbookAirParameters data)
    {
        _haContext.CallService("notify", "mobile_app_haileys_macbook_air", null, data);
    }

    public void MobileAppHaileysMacbookAir(string @message, string? @title = null, object? @target = null, object? @data = null)
    {
        _haContext.CallService("notify", "mobile_app_haileys_macbook_air", null, new
            {
                @message, @title, @target, @data
            }
        );
    }

    public void MobileAppIphone(NotifyMobileAppIphoneParameters data)
    {
        _haContext.CallService("notify", "mobile_app_iphone", null, data);
    }

    public void MobileAppIphone(string @message, string? @title = null, object? @target = null, object? @data = null)
    {
        _haContext.CallService("notify", "mobile_app_iphone", null, new
            {
                @message, @title, @target, @data
            }
        );
    }

    public void Notify(NotifyNotifyParameters data)
    {
        _haContext.CallService("notify", "notify", null, data);
    }

    public void Notify(string @message, string? @title = null, object? @target = null, object? @data = null)
    {
        _haContext.CallService("notify", "notify", null, new
            {
                @message, @title, @target, @data
            }
        );
    }

    public void PersistentNotification(NotifyPersistentNotificationParameters data)
    {
        _haContext.CallService("notify", "persistent_notification", null, data);
    }

    public void PersistentNotification(string @message, string? @title = null)
    {
        _haContext.CallService("notify", "persistent_notification", null, new
            {
                @message, @title
            }
        );
    }

    public void Twinstead(NotifyTwinsteadParameters data)
    {
        _haContext.CallService("notify", "twinstead", null, data);
    }

    public void Twinstead(string @message, string? @title = null, object? @target = null, object? @data = null)
    {
        _haContext.CallService("notify", "twinstead", null, new
            {
                @message, @title, @target, @data
            }
        );
    }
}

public record NotifyAlexaMediaParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("data")]
    public object? Data { get; init; }
}

public record NotifyAlexaMediaAaronParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("data")]
    public object? Data { get; init; }
}

public record NotifyAlexaMediaBoseQc35IiParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("data")]
    public object? Data { get; init; }
}

public record NotifyAlexaMediaDiningParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("data")]
    public object? Data { get; init; }
}

public record NotifyAlexaMediaDownstairsParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("data")]
    public object? Data { get; init; }
}

public record NotifyAlexaMediaEugeneS2ndEchoDotParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("data")]
    public object? Data { get; init; }
}

public record NotifyAlexaMediaEugeneS5thEchoDotParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("data")]
    public object? Data { get; init; }
}

public record NotifyAlexaMediaEugeneSFireParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("data")]
    public object? Data { get; init; }
}

public record NotifyAlexaMediaEugeneSLgOledWebos2021TvParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("data")]
    public object? Data { get; init; }
}

public record NotifyAlexaMediaEugeneSSonosArcParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("data")]
    public object? Data { get; init; }
}

public record NotifyAlexaMediaEverywhere2Parameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("data")]
    public object? Data { get; init; }
}

public record NotifyAlexaMediaJaydenParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("data")]
    public object? Data { get; init; }
}

public record NotifyAlexaMediaKitchenParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("data")]
    public object? Data { get; init; }
}

public record NotifyAlexaMediaLastCalledParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("data")]
    public object? Data { get; init; }
}

public record NotifyAlexaMediaLoungeGroupParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("data")]
    public object? Data { get; init; }
}

public record NotifyAlexaMediaLoungeSonosParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("data")]
    public object? Data { get; init; }
}

public record NotifyAlexaMediaMasterParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("data")]
    public object? Data { get; init; }
}

public record NotifyAlexaMediaMasterTvAlexaParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("data")]
    public object? Data { get; init; }
}

public record NotifyAlexaMediaOfficeParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("data")]
    public object? Data { get; init; }
}

public record NotifyAlexaMediaPlayroomParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("data")]
    public object? Data { get; init; }
}

public record NotifyAlexaMediaThisDeviceParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("data")]
    public object? Data { get; init; }
}

public record NotifyAlexaMediaThisDevice2Parameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("data")]
    public object? Data { get; init; }
}

public record NotifyAlexaMediaUpstairsParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("data")]
    public object? Data { get; init; }
}

public record NotifyEugeneParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("data")]
    public object? Data { get; init; }
}

public record NotifyHaileyParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("data")]
    public object? Data { get; init; }
}

public record NotifyLoungeTvParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("data")]
    public object? Data { get; init; }
}

public record NotifyMasterTvParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("data")]
    public object? Data { get; init; }
}

public record NotifyMobileAppEugenesIphoneParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("data")]
    public object? Data { get; init; }
}

public record NotifyMobileAppHaileySIphoneParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("data")]
    public object? Data { get; init; }
}

public record NotifyMobileAppHaileysMacbookAirParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("data")]
    public object? Data { get; init; }
}

public record NotifyMobileAppIphoneParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("data")]
    public object? Data { get; init; }
}

public record NotifyNotifyParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("data")]
    public object? Data { get; init; }
}

public record NotifyPersistentNotificationParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }
}

public record NotifyTwinsteadParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("data")]
    public object? Data { get; init; }
}

public class NumberServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public NumberServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void SetValue(NetDaemon.HassModel.Entities.ServiceTarget target, NumberSetValueParameters data)
    {
        _haContext.CallService("number", "set_value", target, data);
    }

    public void SetValue(NetDaemon.HassModel.Entities.ServiceTarget target, string? @value = null)
    {
        _haContext.CallService("number", "set_value", target, new
            {
                @value
            }
        );
    }
}

public record NumberSetValueParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("value")]
    public string? Value { get; init; }
}

public class OctopusagileServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public OctopusagileServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void HalfHour()
    {
        _haContext.CallService("octopusagile", "half_hour", null);
    }

    public void UpdateConsumption()
    {
        _haContext.CallService("octopusagile", "update_consumption", null);
    }

    public void UpdateTimers()
    {
        _haContext.CallService("octopusagile", "update_timers", null);
    }
}

public class PersistentNotificationServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public PersistentNotificationServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Create(PersistentNotificationCreateParameters data)
    {
        _haContext.CallService("persistent_notification", "create", null, data);
    }

    public void Create(string @message, string? @title = null, string? @notificationId = null)
    {
        _haContext.CallService("persistent_notification", "create", null, new
            {
                @message, @title, @notification_id = @notificationId
            }
        );
    }

    public void Dismiss(PersistentNotificationDismissParameters data)
    {
        _haContext.CallService("persistent_notification", "dismiss", null, data);
    }

    public void Dismiss(string @notificationId)
    {
        _haContext.CallService("persistent_notification", "dismiss", null, new
            {
                @notification_id = @notificationId
            }
        );
    }

    public void MarkRead(PersistentNotificationMarkReadParameters data)
    {
        _haContext.CallService("persistent_notification", "mark_read", null, data);
    }

    public void MarkRead(string @notificationId)
    {
        _haContext.CallService("persistent_notification", "mark_read", null, new
            {
                @notification_id = @notificationId
            }
        );
    }
}

public record PersistentNotificationCreateParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("notificationId")]
    public string? NotificationId { get; init; }
}

public record PersistentNotificationDismissParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("notificationId")]
    public string? NotificationId { get; init; }
}

public record PersistentNotificationMarkReadParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("notificationId")]
    public string? NotificationId { get; init; }
}

public class PersonServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public PersonServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Reload()
    {
        _haContext.CallService("person", "reload", null);
    }
}

public class PiHoleServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public PiHoleServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Disable(NetDaemon.HassModel.Entities.ServiceTarget target, PiHoleDisableParameters data)
    {
        _haContext.CallService("pi_hole", "disable", target, data);
    }

    public void Disable(NetDaemon.HassModel.Entities.ServiceTarget target, string @duration)
    {
        _haContext.CallService("pi_hole", "disable", target, new
            {
                @duration
            }
        );
    }
}

public record PiHoleDisableParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("duration")]
    public string? Duration { get; init; }
}

public class PingServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public PingServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Reload()
    {
        _haContext.CallService("ping", "reload", null);
    }
}

public class RecorderServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public RecorderServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Disable()
    {
        _haContext.CallService("recorder", "disable", null);
    }

    public void Enable()
    {
        _haContext.CallService("recorder", "enable", null);
    }

    public void Purge(RecorderPurgeParameters data)
    {
        _haContext.CallService("recorder", "purge", null, data);
    }

    public void Purge(long? @keepDays = null, bool? @repack = null, bool? @applyFilter = null)
    {
        _haContext.CallService("recorder", "purge", null, new
            {
                @keep_days = @keepDays, @repack, @apply_filter = @applyFilter
            }
        );
    }

    public void PurgeEntities(NetDaemon.HassModel.Entities.ServiceTarget target, RecorderPurgeEntitiesParameters data)
    {
        _haContext.CallService("recorder", "purge_entities", target, data);
    }

    public void PurgeEntities(NetDaemon.HassModel.Entities.ServiceTarget target, object? @domains = null, object? @entityGlobs = null)
    {
        _haContext.CallService("recorder", "purge_entities", target, new
            {
                @domains, @entity_globs = @entityGlobs
            }
        );
    }
}

public record RecorderPurgeParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("keepDays")]
    public long? KeepDays { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("repack")]
    public bool? Repack { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("applyFilter")]
    public bool? ApplyFilter { get; init; }
}

public record RecorderPurgeEntitiesParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("domains")]
    public object? Domains { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("entityGlobs")]
    public object? EntityGlobs { get; init; }
}

public class RemoteServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public RemoteServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void DeleteCommand(NetDaemon.HassModel.Entities.ServiceTarget target, RemoteDeleteCommandParameters data)
    {
        _haContext.CallService("remote", "delete_command", target, data);
    }

    public void DeleteCommand(NetDaemon.HassModel.Entities.ServiceTarget target, object @command, string? @device = null)
    {
        _haContext.CallService("remote", "delete_command", target, new
            {
                @device, @command
            }
        );
    }

    public void LearnCommand(NetDaemon.HassModel.Entities.ServiceTarget target, RemoteLearnCommandParameters data)
    {
        _haContext.CallService("remote", "learn_command", target, data);
    }

    public void LearnCommand(NetDaemon.HassModel.Entities.ServiceTarget target, string? @device = null, object? @command = null, string? @commandType = null, bool? @alternative = null, long? @timeout = null)
    {
        _haContext.CallService("remote", "learn_command", target, new
            {
                @device, @command, @command_type = @commandType, @alternative, @timeout
            }
        );
    }

    public void SendCommand(NetDaemon.HassModel.Entities.ServiceTarget target, RemoteSendCommandParameters data)
    {
        _haContext.CallService("remote", "send_command", target, data);
    }

    public void SendCommand(NetDaemon.HassModel.Entities.ServiceTarget target, string @command, string? @device = null, long? @numRepeats = null, double? @delaySecs = null, double? @holdSecs = null)
    {
        _haContext.CallService("remote", "send_command", target, new
            {
                @device, @command, @num_repeats = @numRepeats, @delay_secs = @delaySecs, @hold_secs = @holdSecs
            }
        );
    }

    public void Toggle(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("remote", "toggle", target);
    }

    public void TurnOff(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("remote", "turn_off", target);
    }

    public void TurnOn(NetDaemon.HassModel.Entities.ServiceTarget target, RemoteTurnOnParameters data)
    {
        _haContext.CallService("remote", "turn_on", target, data);
    }

    public void TurnOn(NetDaemon.HassModel.Entities.ServiceTarget target, string? @activity = null)
    {
        _haContext.CallService("remote", "turn_on", target, new
            {
                @activity
            }
        );
    }
}

public record RemoteDeleteCommandParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("device")]
    public string? Device { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("command")]
    public object? Command { get; init; }
}

public record RemoteLearnCommandParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("device")]
    public string? Device { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("command")]
    public object? Command { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("commandType")]
    public string? CommandType { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("alternative")]
    public bool? Alternative { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("timeout")]
    public long? Timeout { get; init; }
}

public record RemoteSendCommandParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("device")]
    public string? Device { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("command")]
    public string? Command { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("numRepeats")]
    public long? NumRepeats { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("delaySecs")]
    public double? DelaySecs { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("holdSecs")]
    public double? HoldSecs { get; init; }
}

public record RemoteTurnOnParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("activity")]
    public string? Activity { get; init; }
}

public class RingServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public RingServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Update()
    {
        _haContext.CallService("ring", "update", null);
    }
}

public class SceneServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public SceneServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Apply(SceneApplyParameters data)
    {
        _haContext.CallService("scene", "apply", null, data);
    }

    public void Apply(object @entities, long? @transition = null)
    {
        _haContext.CallService("scene", "apply", null, new
            {
                @entities, @transition
            }
        );
    }

    public void Create(SceneCreateParameters data)
    {
        _haContext.CallService("scene", "create", null, data);
    }

    public void Create(string @sceneId, object? @entities = null, object? @snapshotEntities = null)
    {
        _haContext.CallService("scene", "create", null, new
            {
                @scene_id = @sceneId, @entities, @snapshot_entities = @snapshotEntities
            }
        );
    }

    public void Reload()
    {
        _haContext.CallService("scene", "reload", null);
    }

    public void TurnOn(NetDaemon.HassModel.Entities.ServiceTarget target, SceneTurnOnParameters data)
    {
        _haContext.CallService("scene", "turn_on", target, data);
    }

    public void TurnOn(NetDaemon.HassModel.Entities.ServiceTarget target, long? @transition = null)
    {
        _haContext.CallService("scene", "turn_on", target, new
            {
                @transition
            }
        );
    }
}

public record SceneApplyParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("entities")]
    public object? Entities { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("transition")]
    public long? Transition { get; init; }
}

public record SceneCreateParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("sceneId")]
    public string? SceneId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("entities")]
    public object? Entities { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("snapshotEntities")]
    public object? SnapshotEntities { get; init; }
}

public record SceneTurnOnParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("transition")]
    public long? Transition { get; init; }
}

public class SchedulerServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public SchedulerServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Add(SchedulerAddParameters data)
    {
        _haContext.CallService("scheduler", "add", null, data);
    }

    public void Add(object @timeslots, string @repeatType, object? @weekdays = null, object? @startDate = null, object? @endDate = null, string? @name = null)
    {
        _haContext.CallService("scheduler", "add", null, new
            {
                @weekdays, @start_date = @startDate, @end_date = @endDate, @timeslots, @repeat_type = @repeatType, @name
            }
        );
    }

    public void Copy(SchedulerCopyParameters data)
    {
        _haContext.CallService("scheduler", "copy", null, data);
    }

    public void Copy(string @entityId, string? @name = null)
    {
        _haContext.CallService("scheduler", "copy", null, new
            {
                @entity_id = @entityId, @name
            }
        );
    }

    public void Edit(SchedulerEditParameters data)
    {
        _haContext.CallService("scheduler", "edit", null, data);
    }

    public void Edit(string @entityId, object @timeslots, string @repeatType, object? @weekdays = null, object? @startDate = null, object? @endDate = null, string? @name = null)
    {
        _haContext.CallService("scheduler", "edit", null, new
            {
                @entity_id = @entityId, @weekdays, @start_date = @startDate, @end_date = @endDate, @timeslots, @repeat_type = @repeatType, @name
            }
        );
    }

    public void Remove(SchedulerRemoveParameters data)
    {
        _haContext.CallService("scheduler", "remove", null, data);
    }

    public void Remove(string @entityId)
    {
        _haContext.CallService("scheduler", "remove", null, new
            {
                @entity_id = @entityId
            }
        );
    }

    public void RunAction(SchedulerRunActionParameters data)
    {
        _haContext.CallService("scheduler", "run_action", null, data);
    }

    public void RunAction(string @entityId, DateTime? @time = null)
    {
        _haContext.CallService("scheduler", "run_action", null, new
            {
                @entity_id = @entityId, @time
            }
        );
    }
}

public record SchedulerAddParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("weekdays")]
    public object? Weekdays { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("startDate")]
    public object? StartDate { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("endDate")]
    public object? EndDate { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("timeslots")]
    public object? Timeslots { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("repeatType")]
    public string? RepeatType { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("name")]
    public string? Name { get; init; }
}

public record SchedulerCopyParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("entityId")]
    public string? EntityId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("name")]
    public string? Name { get; init; }
}

public record SchedulerEditParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("entityId")]
    public string? EntityId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("weekdays")]
    public object? Weekdays { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("startDate")]
    public object? StartDate { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("endDate")]
    public object? EndDate { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("timeslots")]
    public object? Timeslots { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("repeatType")]
    public string? RepeatType { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("name")]
    public string? Name { get; init; }
}

public record SchedulerRemoveParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("entityId")]
    public string? EntityId { get; init; }
}

public record SchedulerRunActionParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("entityId")]
    public string? EntityId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("time")]
    public DateTime? Time { get; init; }
}

public class ScriptServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public ScriptServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void ActivateAlexaActionableNotification(ScriptActivateAlexaActionableNotificationParameters data)
    {
        _haContext.CallService("script", "activate_alexa_actionable_notification", null, data);
    }

    public void ActivateAlexaActionableNotification(string? @text = null, string? @eventId = null, string? @alexaDevice = null)
    {
        _haContext.CallService("script", "activate_alexa_actionable_notification", null, new
            {
                @text, @event_id = @eventId, @alexa_device = @alexaDevice
            }
        );
    }

    public void ArriveHome()
    {
        _haContext.CallService("script", "arrive_home", null);
    }

    public void ImText()
    {
        _haContext.CallService("script", "im_text", null);
    }

    public void Reload()
    {
        _haContext.CallService("script", "reload", null);
    }

    public void RingMqttInterval()
    {
        _haContext.CallService("script", "ring_mqtt_interval", null);
    }

    public void Toggle(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("script", "toggle", target);
    }

    public void Tts()
    {
        _haContext.CallService("script", "tts", null);
    }

    public void TtsText()
    {
        _haContext.CallService("script", "tts_text", null);
    }

    public void TurnOff(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("script", "turn_off", target);
    }

    public void TurnOn(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("script", "turn_on", target);
    }

    public void Weather()
    {
        _haContext.CallService("script", "weather", null);
    }
}

public record ScriptActivateAlexaActionableNotificationParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("text")]
    public string? Text { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("eventId")]
    public string? EventId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("alexaDevice")]
    public string? AlexaDevice { get; init; }
}

public class SelectServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public SelectServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void SelectOption(NetDaemon.HassModel.Entities.ServiceTarget target, SelectSelectOptionParameters data)
    {
        _haContext.CallService("select", "select_option", target, data);
    }

    public void SelectOption(NetDaemon.HassModel.Entities.ServiceTarget target, string @option)
    {
        _haContext.CallService("select", "select_option", target, new
            {
                @option
            }
        );
    }
}

public record SelectSelectOptionParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("option")]
    public string? Option { get; init; }
}

public class SonosServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public SonosServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void ClearSleepTimer(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("sonos", "clear_sleep_timer", target);
    }

    public void Join(SonosJoinParameters data)
    {
        _haContext.CallService("sonos", "join", null, data);
    }

    public void Join(string @master, string @entityId)
    {
        _haContext.CallService("sonos", "join", null, new
            {
                @master, @entity_id = @entityId
            }
        );
    }

    public void PlayQueue(NetDaemon.HassModel.Entities.ServiceTarget target, SonosPlayQueueParameters data)
    {
        _haContext.CallService("sonos", "play_queue", target, data);
    }

    public void PlayQueue(NetDaemon.HassModel.Entities.ServiceTarget target, long? @queuePosition = null)
    {
        _haContext.CallService("sonos", "play_queue", target, new
            {
                @queue_position = @queuePosition
            }
        );
    }

    public void RemoveFromQueue(NetDaemon.HassModel.Entities.ServiceTarget target, SonosRemoveFromQueueParameters data)
    {
        _haContext.CallService("sonos", "remove_from_queue", target, data);
    }

    public void RemoveFromQueue(NetDaemon.HassModel.Entities.ServiceTarget target, long? @queuePosition = null)
    {
        _haContext.CallService("sonos", "remove_from_queue", target, new
            {
                @queue_position = @queuePosition
            }
        );
    }

    public void Restore(SonosRestoreParameters data)
    {
        _haContext.CallService("sonos", "restore", null, data);
    }

    public void Restore(string? @entityId = null, bool? @withGroup = null)
    {
        _haContext.CallService("sonos", "restore", null, new
            {
                @entity_id = @entityId, @with_group = @withGroup
            }
        );
    }

    public void SetOption(NetDaemon.HassModel.Entities.ServiceTarget target, SonosSetOptionParameters data)
    {
        _haContext.CallService("sonos", "set_option", target, data);
    }

    public void SetOption(NetDaemon.HassModel.Entities.ServiceTarget target, long? @bassLevel = null, long? @trebleLevel = null)
    {
        _haContext.CallService("sonos", "set_option", target, new
            {
                @bass_level = @bassLevel, @treble_level = @trebleLevel
            }
        );
    }

    public void SetSleepTimer(NetDaemon.HassModel.Entities.ServiceTarget target, SonosSetSleepTimerParameters data)
    {
        _haContext.CallService("sonos", "set_sleep_timer", target, data);
    }

    public void SetSleepTimer(NetDaemon.HassModel.Entities.ServiceTarget target, long? @sleepTime = null)
    {
        _haContext.CallService("sonos", "set_sleep_timer", target, new
            {
                @sleep_time = @sleepTime
            }
        );
    }

    public void Snapshot(SonosSnapshotParameters data)
    {
        _haContext.CallService("sonos", "snapshot", null, data);
    }

    public void Snapshot(string? @entityId = null, bool? @withGroup = null)
    {
        _haContext.CallService("sonos", "snapshot", null, new
            {
                @entity_id = @entityId, @with_group = @withGroup
            }
        );
    }

    public void Unjoin(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("sonos", "unjoin", target);
    }

    public void UpdateAlarm(NetDaemon.HassModel.Entities.ServiceTarget target, SonosUpdateAlarmParameters data)
    {
        _haContext.CallService("sonos", "update_alarm", target, data);
    }

    public void UpdateAlarm(NetDaemon.HassModel.Entities.ServiceTarget target, long @alarmId, DateTime? @time = null, double? @volume = null, bool? @enabled = null, bool? @includeLinkedZones = null)
    {
        _haContext.CallService("sonos", "update_alarm", target, new
            {
                @alarm_id = @alarmId, @time, @volume, @enabled, @include_linked_zones = @includeLinkedZones
            }
        );
    }
}

public record SonosJoinParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("master")]
    public string? Master { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("entityId")]
    public string? EntityId { get; init; }
}

public record SonosPlayQueueParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("queuePosition")]
    public long? QueuePosition { get; init; }
}

public record SonosRemoveFromQueueParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("queuePosition")]
    public long? QueuePosition { get; init; }
}

public record SonosRestoreParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("entityId")]
    public string? EntityId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("withGroup")]
    public bool? WithGroup { get; init; }
}

public record SonosSetOptionParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("bassLevel")]
    public long? BassLevel { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("trebleLevel")]
    public long? TrebleLevel { get; init; }
}

public record SonosSetSleepTimerParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("sleepTime")]
    public long? SleepTime { get; init; }
}

public record SonosSnapshotParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("entityId")]
    public string? EntityId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("withGroup")]
    public bool? WithGroup { get; init; }
}

public record SonosUpdateAlarmParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("alarmId")]
    public long? AlarmId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("time")]
    public DateTime? Time { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("volume")]
    public double? Volume { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("enabled")]
    public bool? Enabled { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("includeLinkedZones")]
    public bool? IncludeLinkedZones { get; init; }
}

public class SpeedtestdotnetServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public SpeedtestdotnetServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Speedtest()
    {
        _haContext.CallService("speedtestdotnet", "speedtest", null);
    }
}

public class StatisticsServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public StatisticsServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Reload()
    {
        _haContext.CallService("statistics", "reload", null);
    }
}

public class SwitchServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public SwitchServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Toggle(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("switch", "toggle", target);
    }

    public void TurnOff(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("switch", "turn_off", target);
    }

    public void TurnOn(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("switch", "turn_on", target);
    }
}

public class SystemLogServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public SystemLogServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Clear()
    {
        _haContext.CallService("system_log", "clear", null);
    }

    public void Write(SystemLogWriteParameters data)
    {
        _haContext.CallService("system_log", "write", null, data);
    }

    public void Write(string @message, string? @level = null, string? @logger = null)
    {
        _haContext.CallService("system_log", "write", null, new
            {
                @message, @level, @logger
            }
        );
    }
}

public record SystemLogWriteParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("level")]
    public string? Level { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("logger")]
    public string? Logger { get; init; }
}

public class TelegramServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public TelegramServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Reload()
    {
        _haContext.CallService("telegram", "reload", null);
    }
}

public class TelegramBotServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public TelegramBotServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void AnswerCallbackQuery(TelegramBotAnswerCallbackQueryParameters data)
    {
        _haContext.CallService("telegram_bot", "answer_callback_query", null, data);
    }

    public void AnswerCallbackQuery(string @message, string @callbackQueryId, bool @showAlert, long? @timeout = null)
    {
        _haContext.CallService("telegram_bot", "answer_callback_query", null, new
            {
                @message, @callback_query_id = @callbackQueryId, @show_alert = @showAlert, @timeout
            }
        );
    }

    public void DeleteMessage(TelegramBotDeleteMessageParameters data)
    {
        _haContext.CallService("telegram_bot", "delete_message", null, data);
    }

    public void DeleteMessage(string @messageId, string @chatId)
    {
        _haContext.CallService("telegram_bot", "delete_message", null, new
            {
                @message_id = @messageId, @chat_id = @chatId
            }
        );
    }

    public void EditCaption(TelegramBotEditCaptionParameters data)
    {
        _haContext.CallService("telegram_bot", "edit_caption", null, data);
    }

    public void EditCaption(string @messageId, string @chatId, string @caption, object? @inlineKeyboard = null)
    {
        _haContext.CallService("telegram_bot", "edit_caption", null, new
            {
                @message_id = @messageId, @chat_id = @chatId, @caption, @inline_keyboard = @inlineKeyboard
            }
        );
    }

    public void EditMessage(TelegramBotEditMessageParameters data)
    {
        _haContext.CallService("telegram_bot", "edit_message", null, data);
    }

    public void EditMessage(string @messageId, string @chatId, string? @message = null, string? @title = null, string? @parseMode = null, bool? @disableWebPagePreview = null, object? @inlineKeyboard = null)
    {
        _haContext.CallService("telegram_bot", "edit_message", null, new
            {
                @message_id = @messageId, @chat_id = @chatId, @message, @title, @parse_mode = @parseMode, @disable_web_page_preview = @disableWebPagePreview, @inline_keyboard = @inlineKeyboard
            }
        );
    }

    public void EditReplymarkup(TelegramBotEditReplymarkupParameters data)
    {
        _haContext.CallService("telegram_bot", "edit_replymarkup", null, data);
    }

    public void EditReplymarkup(string @messageId, string @chatId, object @inlineKeyboard)
    {
        _haContext.CallService("telegram_bot", "edit_replymarkup", null, new
            {
                @message_id = @messageId, @chat_id = @chatId, @inline_keyboard = @inlineKeyboard
            }
        );
    }

    public void LeaveChat()
    {
        _haContext.CallService("telegram_bot", "leave_chat", null);
    }

    public void SendAnimation(TelegramBotSendAnimationParameters data)
    {
        _haContext.CallService("telegram_bot", "send_animation", null, data);
    }

    public void SendAnimation(string? @url = null, string? @file = null, string? @caption = null, string? @username = null, string? @password = null, string? @authentication = null, object? @target = null, string? @parseMode = null, bool? @disableNotification = null, bool? @verifySsl = null, long? @timeout = null, object? @keyboard = null, object? @inlineKeyboard = null)
    {
        _haContext.CallService("telegram_bot", "send_animation", null, new
            {
                @url, @file, @caption, @username, @password, @authentication, @target, @parse_mode = @parseMode, @disable_notification = @disableNotification, @verify_ssl = @verifySsl, @timeout, @keyboard, @inline_keyboard = @inlineKeyboard
            }
        );
    }

    public void SendDocument(TelegramBotSendDocumentParameters data)
    {
        _haContext.CallService("telegram_bot", "send_document", null, data);
    }

    public void SendDocument(string? @url = null, string? @file = null, string? @caption = null, string? @username = null, string? @password = null, string? @authentication = null, object? @target = null, string? @parseMode = null, bool? @disableNotification = null, bool? @verifySsl = null, long? @timeout = null, object? @keyboard = null, object? @inlineKeyboard = null, string? @messageTag = null)
    {
        _haContext.CallService("telegram_bot", "send_document", null, new
            {
                @url, @file, @caption, @username, @password, @authentication, @target, @parse_mode = @parseMode, @disable_notification = @disableNotification, @verify_ssl = @verifySsl, @timeout, @keyboard, @inline_keyboard = @inlineKeyboard, @message_tag = @messageTag
            }
        );
    }

    public void SendLocation(TelegramBotSendLocationParameters data)
    {
        _haContext.CallService("telegram_bot", "send_location", null, data);
    }

    public void SendLocation(double @latitude, double @longitude, object? @target = null, bool? @disableNotification = null, long? @timeout = null, object? @keyboard = null, object? @inlineKeyboard = null, string? @messageTag = null)
    {
        _haContext.CallService("telegram_bot", "send_location", null, new
            {
                @latitude, @longitude, @target, @disable_notification = @disableNotification, @timeout, @keyboard, @inline_keyboard = @inlineKeyboard, @message_tag = @messageTag
            }
        );
    }

    public void SendMessage(TelegramBotSendMessageParameters data)
    {
        _haContext.CallService("telegram_bot", "send_message", null, data);
    }

    public void SendMessage(string @message, string? @title = null, object? @target = null, string? @parseMode = null, bool? @disableNotification = null, bool? @disableWebPagePreview = null, long? @timeout = null, object? @keyboard = null, object? @inlineKeyboard = null, string? @messageTag = null)
    {
        _haContext.CallService("telegram_bot", "send_message", null, new
            {
                @message, @title, @target, @parse_mode = @parseMode, @disable_notification = @disableNotification, @disable_web_page_preview = @disableWebPagePreview, @timeout, @keyboard, @inline_keyboard = @inlineKeyboard, @message_tag = @messageTag
            }
        );
    }

    public void SendPhoto(TelegramBotSendPhotoParameters data)
    {
        _haContext.CallService("telegram_bot", "send_photo", null, data);
    }

    public void SendPhoto(string? @url = null, string? @file = null, string? @caption = null, string? @username = null, string? @password = null, string? @authentication = null, object? @target = null, string? @parseMode = null, bool? @disableNotification = null, bool? @verifySsl = null, long? @timeout = null, object? @keyboard = null, object? @inlineKeyboard = null, string? @messageTag = null)
    {
        _haContext.CallService("telegram_bot", "send_photo", null, new
            {
                @url, @file, @caption, @username, @password, @authentication, @target, @parse_mode = @parseMode, @disable_notification = @disableNotification, @verify_ssl = @verifySsl, @timeout, @keyboard, @inline_keyboard = @inlineKeyboard, @message_tag = @messageTag
            }
        );
    }

    public void SendSticker(TelegramBotSendStickerParameters data)
    {
        _haContext.CallService("telegram_bot", "send_sticker", null, data);
    }

    public void SendSticker(string? @url = null, string? @file = null, string? @username = null, string? @password = null, string? @authentication = null, object? @target = null, bool? @disableNotification = null, bool? @verifySsl = null, long? @timeout = null, object? @keyboard = null, object? @inlineKeyboard = null, string? @messageTag = null)
    {
        _haContext.CallService("telegram_bot", "send_sticker", null, new
            {
                @url, @file, @username, @password, @authentication, @target, @disable_notification = @disableNotification, @verify_ssl = @verifySsl, @timeout, @keyboard, @inline_keyboard = @inlineKeyboard, @message_tag = @messageTag
            }
        );
    }

    public void SendVideo(TelegramBotSendVideoParameters data)
    {
        _haContext.CallService("telegram_bot", "send_video", null, data);
    }

    public void SendVideo(string? @url = null, string? @file = null, string? @caption = null, string? @username = null, string? @password = null, string? @authentication = null, object? @target = null, string? @parseMode = null, bool? @disableNotification = null, bool? @verifySsl = null, long? @timeout = null, object? @keyboard = null, object? @inlineKeyboard = null, string? @messageTag = null)
    {
        _haContext.CallService("telegram_bot", "send_video", null, new
            {
                @url, @file, @caption, @username, @password, @authentication, @target, @parse_mode = @parseMode, @disable_notification = @disableNotification, @verify_ssl = @verifySsl, @timeout, @keyboard, @inline_keyboard = @inlineKeyboard, @message_tag = @messageTag
            }
        );
    }

    public void SendVoice(TelegramBotSendVoiceParameters data)
    {
        _haContext.CallService("telegram_bot", "send_voice", null, data);
    }

    public void SendVoice(string? @url = null, string? @file = null, string? @caption = null, string? @username = null, string? @password = null, string? @authentication = null, object? @target = null, bool? @disableNotification = null, bool? @verifySsl = null, long? @timeout = null, object? @keyboard = null, object? @inlineKeyboard = null, string? @messageTag = null)
    {
        _haContext.CallService("telegram_bot", "send_voice", null, new
            {
                @url, @file, @caption, @username, @password, @authentication, @target, @disable_notification = @disableNotification, @verify_ssl = @verifySsl, @timeout, @keyboard, @inline_keyboard = @inlineKeyboard, @message_tag = @messageTag
            }
        );
    }
}

public record TelegramBotAnswerCallbackQueryParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("callbackQueryId")]
    public string? CallbackQueryId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("showAlert")]
    public bool? ShowAlert { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("timeout")]
    public long? Timeout { get; init; }
}

public record TelegramBotDeleteMessageParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("messageId")]
    public string? MessageId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("chatId")]
    public string? ChatId { get; init; }
}

public record TelegramBotEditCaptionParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("messageId")]
    public string? MessageId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("chatId")]
    public string? ChatId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("caption")]
    public string? Caption { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("inlineKeyboard")]
    public object? InlineKeyboard { get; init; }
}

public record TelegramBotEditMessageParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("messageId")]
    public string? MessageId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("chatId")]
    public string? ChatId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("parseMode")]
    public string? ParseMode { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("disableWebPagePreview")]
    public bool? DisableWebPagePreview { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("inlineKeyboard")]
    public object? InlineKeyboard { get; init; }
}

public record TelegramBotEditReplymarkupParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("messageId")]
    public string? MessageId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("chatId")]
    public string? ChatId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("inlineKeyboard")]
    public object? InlineKeyboard { get; init; }
}

public record TelegramBotSendAnimationParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("url")]
    public string? Url { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("file")]
    public string? File { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("caption")]
    public string? Caption { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("username")]
    public string? Username { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("password")]
    public string? Password { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("authentication")]
    public string? Authentication { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("parseMode")]
    public string? ParseMode { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("disableNotification")]
    public bool? DisableNotification { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("verifySsl")]
    public bool? VerifySsl { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("timeout")]
    public long? Timeout { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("keyboard")]
    public object? Keyboard { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("inlineKeyboard")]
    public object? InlineKeyboard { get; init; }
}

public record TelegramBotSendDocumentParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("url")]
    public string? Url { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("file")]
    public string? File { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("caption")]
    public string? Caption { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("username")]
    public string? Username { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("password")]
    public string? Password { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("authentication")]
    public string? Authentication { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("parseMode")]
    public string? ParseMode { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("disableNotification")]
    public bool? DisableNotification { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("verifySsl")]
    public bool? VerifySsl { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("timeout")]
    public long? Timeout { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("keyboard")]
    public object? Keyboard { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("inlineKeyboard")]
    public object? InlineKeyboard { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("messageTag")]
    public string? MessageTag { get; init; }
}

public record TelegramBotSendLocationParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("disableNotification")]
    public bool? DisableNotification { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("timeout")]
    public long? Timeout { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("keyboard")]
    public object? Keyboard { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("inlineKeyboard")]
    public object? InlineKeyboard { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("messageTag")]
    public string? MessageTag { get; init; }
}

public record TelegramBotSendMessageParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("title")]
    public string? Title { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("parseMode")]
    public string? ParseMode { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("disableNotification")]
    public bool? DisableNotification { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("disableWebPagePreview")]
    public bool? DisableWebPagePreview { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("timeout")]
    public long? Timeout { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("keyboard")]
    public object? Keyboard { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("inlineKeyboard")]
    public object? InlineKeyboard { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("messageTag")]
    public string? MessageTag { get; init; }
}

public record TelegramBotSendPhotoParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("url")]
    public string? Url { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("file")]
    public string? File { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("caption")]
    public string? Caption { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("username")]
    public string? Username { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("password")]
    public string? Password { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("authentication")]
    public string? Authentication { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("parseMode")]
    public string? ParseMode { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("disableNotification")]
    public bool? DisableNotification { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("verifySsl")]
    public bool? VerifySsl { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("timeout")]
    public long? Timeout { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("keyboard")]
    public object? Keyboard { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("inlineKeyboard")]
    public object? InlineKeyboard { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("messageTag")]
    public string? MessageTag { get; init; }
}

public record TelegramBotSendStickerParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("url")]
    public string? Url { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("file")]
    public string? File { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("username")]
    public string? Username { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("password")]
    public string? Password { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("authentication")]
    public string? Authentication { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("disableNotification")]
    public bool? DisableNotification { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("verifySsl")]
    public bool? VerifySsl { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("timeout")]
    public long? Timeout { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("keyboard")]
    public object? Keyboard { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("inlineKeyboard")]
    public object? InlineKeyboard { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("messageTag")]
    public string? MessageTag { get; init; }
}

public record TelegramBotSendVideoParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("url")]
    public string? Url { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("file")]
    public string? File { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("caption")]
    public string? Caption { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("username")]
    public string? Username { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("password")]
    public string? Password { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("authentication")]
    public string? Authentication { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("parseMode")]
    public string? ParseMode { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("disableNotification")]
    public bool? DisableNotification { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("verifySsl")]
    public bool? VerifySsl { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("timeout")]
    public long? Timeout { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("keyboard")]
    public object? Keyboard { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("inlineKeyboard")]
    public object? InlineKeyboard { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("messageTag")]
    public string? MessageTag { get; init; }
}

public record TelegramBotSendVoiceParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("url")]
    public string? Url { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("file")]
    public string? File { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("caption")]
    public string? Caption { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("username")]
    public string? Username { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("password")]
    public string? Password { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("authentication")]
    public string? Authentication { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("target")]
    public object? Target { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("disableNotification")]
    public bool? DisableNotification { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("verifySsl")]
    public bool? VerifySsl { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("timeout")]
    public long? Timeout { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("keyboard")]
    public object? Keyboard { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("inlineKeyboard")]
    public object? InlineKeyboard { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("messageTag")]
    public string? MessageTag { get; init; }
}

public class TemplateServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public TemplateServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Reload()
    {
        _haContext.CallService("template", "reload", null);
    }
}

public class TimerServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public TimerServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Cancel(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("timer", "cancel", target);
    }

    public void Finish(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("timer", "finish", target);
    }

    public void Pause(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("timer", "pause", target);
    }

    public void Reload()
    {
        _haContext.CallService("timer", "reload", null);
    }

    public void Start(NetDaemon.HassModel.Entities.ServiceTarget target, TimerStartParameters data)
    {
        _haContext.CallService("timer", "start", target, data);
    }

    public void Start(NetDaemon.HassModel.Entities.ServiceTarget target, string? @duration = null)
    {
        _haContext.CallService("timer", "start", target, new
            {
                @duration
            }
        );
    }
}

public record TimerStartParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("duration")]
    public string? Duration { get; init; }
}

public class TtsServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public TtsServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void ClearCache()
    {
        _haContext.CallService("tts", "clear_cache", null);
    }

    public void CloudSay(TtsCloudSayParameters data)
    {
        _haContext.CallService("tts", "cloud_say", null, data);
    }

    public void CloudSay(string @entityId, string @message, bool? @cache = null, string? @language = null, object? @options = null)
    {
        _haContext.CallService("tts", "cloud_say", null, new
            {
                @entity_id = @entityId, @message, @cache, @language, @options
            }
        );
    }
}

public record TtsCloudSayParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("entityId")]
    public string? EntityId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public string? Message { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("cache")]
    public bool? Cache { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("language")]
    public string? Language { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("options")]
    public object? Options { get; init; }
}

public class UnifiServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public UnifiServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void ReconnectClient(UnifiReconnectClientParameters data)
    {
        _haContext.CallService("unifi", "reconnect_client", null, data);
    }

    public void ReconnectClient(string @deviceId)
    {
        _haContext.CallService("unifi", "reconnect_client", null, new
            {
                @device_id = @deviceId
            }
        );
    }

    public void RemoveClients()
    {
        _haContext.CallService("unifi", "remove_clients", null);
    }
}

public record UnifiReconnectClientParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("deviceId")]
    public string? DeviceId { get; init; }
}

public class UtilityMeterServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public UtilityMeterServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Calibrate(NetDaemon.HassModel.Entities.ServiceTarget target, UtilityMeterCalibrateParameters data)
    {
        _haContext.CallService("utility_meter", "calibrate", target, data);
    }

    public void Calibrate(NetDaemon.HassModel.Entities.ServiceTarget target, string @value)
    {
        _haContext.CallService("utility_meter", "calibrate", target, new
            {
                @value
            }
        );
    }
}

public record UtilityMeterCalibrateParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("value")]
    public string? Value { get; init; }
}

public class VacuumServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public VacuumServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void CleanSpot(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("vacuum", "clean_spot", target);
    }

    public void Locate(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("vacuum", "locate", target);
    }

    public void Pause(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("vacuum", "pause", target);
    }

    public void ReturnToBase(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("vacuum", "return_to_base", target);
    }

    public void SendCommand(NetDaemon.HassModel.Entities.ServiceTarget target, VacuumSendCommandParameters data)
    {
        _haContext.CallService("vacuum", "send_command", target, data);
    }

    public void SendCommand(NetDaemon.HassModel.Entities.ServiceTarget target, string @command, object? @params = null)
    {
        _haContext.CallService("vacuum", "send_command", target, new
            {
                @command, @params
            }
        );
    }

    public void SetFanSpeed(NetDaemon.HassModel.Entities.ServiceTarget target, VacuumSetFanSpeedParameters data)
    {
        _haContext.CallService("vacuum", "set_fan_speed", target, data);
    }

    public void SetFanSpeed(NetDaemon.HassModel.Entities.ServiceTarget target, string @fanSpeed)
    {
        _haContext.CallService("vacuum", "set_fan_speed", target, new
            {
                @fan_speed = @fanSpeed
            }
        );
    }

    public void Start(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("vacuum", "start", target);
    }

    public void StartPause(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("vacuum", "start_pause", target);
    }

    public void Stop(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("vacuum", "stop", target);
    }

    public void Toggle()
    {
        _haContext.CallService("vacuum", "toggle", null);
    }

    public void TurnOff(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("vacuum", "turn_off", target);
    }

    public void TurnOn(NetDaemon.HassModel.Entities.ServiceTarget target)
    {
        _haContext.CallService("vacuum", "turn_on", target);
    }
}

public record VacuumSendCommandParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("command")]
    public string? Command { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("params")]
    public object? Params { get; init; }
}

public record VacuumSetFanSpeedParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("fanSpeed")]
    public string? FanSpeed { get; init; }
}

public class WakeOnLanServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public WakeOnLanServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void SendMagicPacket(WakeOnLanSendMagicPacketParameters data)
    {
        _haContext.CallService("wake_on_lan", "send_magic_packet", null, data);
    }

    public void SendMagicPacket(string @mac, string? @broadcastAddress = null, long? @broadcastPort = null)
    {
        _haContext.CallService("wake_on_lan", "send_magic_packet", null, new
            {
                @mac, @broadcast_address = @broadcastAddress, @broadcast_port = @broadcastPort
            }
        );
    }
}

public record WakeOnLanSendMagicPacketParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("mac")]
    public string? Mac { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("broadcastAddress")]
    public string? BroadcastAddress { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("broadcastPort")]
    public long? BroadcastPort { get; init; }
}

public class WaterHeaterServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public WaterHeaterServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void SetAwayMode(NetDaemon.HassModel.Entities.ServiceTarget target, WaterHeaterSetAwayModeParameters data)
    {
        _haContext.CallService("water_heater", "set_away_mode", target, data);
    }

    public void SetAwayMode(NetDaemon.HassModel.Entities.ServiceTarget target, bool @awayMode)
    {
        _haContext.CallService("water_heater", "set_away_mode", target, new
            {
                @away_mode = @awayMode
            }
        );
    }

    public void SetOperationMode(NetDaemon.HassModel.Entities.ServiceTarget target, WaterHeaterSetOperationModeParameters data)
    {
        _haContext.CallService("water_heater", "set_operation_mode", target, data);
    }

    public void SetOperationMode(NetDaemon.HassModel.Entities.ServiceTarget target, string @operationMode)
    {
        _haContext.CallService("water_heater", "set_operation_mode", target, new
            {
                @operation_mode = @operationMode
            }
        );
    }

    public void SetTemperature(NetDaemon.HassModel.Entities.ServiceTarget target, WaterHeaterSetTemperatureParameters data)
    {
        _haContext.CallService("water_heater", "set_temperature", target, data);
    }

    public void SetTemperature(NetDaemon.HassModel.Entities.ServiceTarget target, double @temperature, string? @operationMode = null)
    {
        _haContext.CallService("water_heater", "set_temperature", target, new
            {
                @temperature, @operation_mode = @operationMode
            }
        );
    }

    public void TurnOff()
    {
        _haContext.CallService("water_heater", "turn_off", null);
    }

    public void TurnOn()
    {
        _haContext.CallService("water_heater", "turn_on", null);
    }
}

public record WaterHeaterSetAwayModeParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("awayMode")]
    public bool? AwayMode { get; init; }
}

public record WaterHeaterSetOperationModeParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("operationMode")]
    public string? OperationMode { get; init; }
}

public record WaterHeaterSetTemperatureParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("temperature")]
    public double? Temperature { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("operationMode")]
    public string? OperationMode { get; init; }
}

public class WebostvServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public WebostvServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Button(WebostvButtonParameters data)
    {
        _haContext.CallService("webostv", "button", null, data);
    }

    public void Button(string @entityId, string @button)
    {
        _haContext.CallService("webostv", "button", null, new
            {
                @entity_id = @entityId, @button
            }
        );
    }

    public void Command(WebostvCommandParameters data)
    {
        _haContext.CallService("webostv", "command", null, data);
    }

    public void Command(string @entityId, string @command, object? @payload = null)
    {
        _haContext.CallService("webostv", "command", null, new
            {
                @entity_id = @entityId, @command, @payload
            }
        );
    }

    public void SelectSoundOutput(WebostvSelectSoundOutputParameters data)
    {
        _haContext.CallService("webostv", "select_sound_output", null, data);
    }

    public void SelectSoundOutput(string @entityId, string @soundOutput)
    {
        _haContext.CallService("webostv", "select_sound_output", null, new
            {
                @entity_id = @entityId, @sound_output = @soundOutput
            }
        );
    }
}

public record WebostvButtonParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("entityId")]
    public string? EntityId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("button")]
    public string? Button { get; init; }
}

public record WebostvCommandParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("entityId")]
    public string? EntityId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("command")]
    public string? Command { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("payload")]
    public object? Payload { get; init; }
}

public record WebostvSelectSoundOutputParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("entityId")]
    public string? EntityId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("soundOutput")]
    public string? SoundOutput { get; init; }
}

public class WiserServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public WiserServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void BoostHeating(WiserBoostHeatingParameters data)
    {
        _haContext.CallService("wiser", "boost_heating", null, data);
    }

    public void BoostHeating(string? @entityId = null, string? @timePeriod = null, string? @temperature = null, string? @temperatureDelta = null)
    {
        _haContext.CallService("wiser", "boost_heating", null, new
            {
                @entity_id = @entityId, @time_period = @timePeriod, @temperature, @temperature_delta = @temperatureDelta
            }
        );
    }

    public void CopySchedule(WiserCopyScheduleParameters data)
    {
        _haContext.CallService("wiser", "copy_schedule", null, data);
    }

    public void CopySchedule(string? @entityId = null, string? @toEntityId = null)
    {
        _haContext.CallService("wiser", "copy_schedule", null, new
            {
                @entity_id = @entityId, @to_entity_id = @toEntityId
            }
        );
    }

    public void GetSchedule(WiserGetScheduleParameters data)
    {
        _haContext.CallService("wiser", "get_schedule", null, data);
    }

    public void GetSchedule(string? @entityId = null, string? @filename = null)
    {
        _haContext.CallService("wiser", "get_schedule", null, new
            {
                @entity_id = @entityId, @filename
            }
        );
    }

    public void SetHotwaterMode(WiserSetHotwaterModeParameters data)
    {
        _haContext.CallService("wiser", "set_hotwater_mode", null, data);
    }

    public void SetHotwaterMode(string? @hotwaterMode = null)
    {
        _haContext.CallService("wiser", "set_hotwater_mode", null, new
            {
                @hotwater_mode = @hotwaterMode
            }
        );
    }

    public void SetSchedule(WiserSetScheduleParameters data)
    {
        _haContext.CallService("wiser", "set_schedule", null, data);
    }

    public void SetSchedule(string? @entityId = null, string? @filename = null)
    {
        _haContext.CallService("wiser", "set_schedule", null, new
            {
                @entity_id = @entityId, @filename
            }
        );
    }

    public void SetSmartplugMode(WiserSetSmartplugModeParameters data)
    {
        _haContext.CallService("wiser", "set_smartplug_mode", null, data);
    }

    public void SetSmartplugMode(string? @entityId = null, string? @plugMode = null)
    {
        _haContext.CallService("wiser", "set_smartplug_mode", null, new
            {
                @entity_id = @entityId, @plug_mode = @plugMode
            }
        );
    }
}

public record WiserBoostHeatingParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("entityId")]
    public string? EntityId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("timePeriod")]
    public string? TimePeriod { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("temperature")]
    public string? Temperature { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("temperatureDelta")]
    public string? TemperatureDelta { get; init; }
}

public record WiserCopyScheduleParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("entityId")]
    public string? EntityId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("toEntityId")]
    public string? ToEntityId { get; init; }
}

public record WiserGetScheduleParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("entityId")]
    public string? EntityId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("filename")]
    public string? Filename { get; init; }
}

public record WiserSetHotwaterModeParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("hotwaterMode")]
    public string? HotwaterMode { get; init; }
}

public record WiserSetScheduleParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("entityId")]
    public string? EntityId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("filename")]
    public string? Filename { get; init; }
}

public record WiserSetSmartplugModeParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("entityId")]
    public string? EntityId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("plugMode")]
    public string? PlugMode { get; init; }
}

public class ZhaServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public ZhaServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void ClearLockUserCode(NetDaemon.HassModel.Entities.ServiceTarget target, ZhaClearLockUserCodeParameters data)
    {
        _haContext.CallService("zha", "clear_lock_user_code", target, data);
    }

    public void ClearLockUserCode(NetDaemon.HassModel.Entities.ServiceTarget target, string @codeSlot)
    {
        _haContext.CallService("zha", "clear_lock_user_code", target, new
            {
                @code_slot = @codeSlot
            }
        );
    }

    public void DisableLockUserCode(NetDaemon.HassModel.Entities.ServiceTarget target, ZhaDisableLockUserCodeParameters data)
    {
        _haContext.CallService("zha", "disable_lock_user_code", target, data);
    }

    public void DisableLockUserCode(NetDaemon.HassModel.Entities.ServiceTarget target, string @codeSlot)
    {
        _haContext.CallService("zha", "disable_lock_user_code", target, new
            {
                @code_slot = @codeSlot
            }
        );
    }

    public void EnableLockUserCode(NetDaemon.HassModel.Entities.ServiceTarget target, ZhaEnableLockUserCodeParameters data)
    {
        _haContext.CallService("zha", "enable_lock_user_code", target, data);
    }

    public void EnableLockUserCode(NetDaemon.HassModel.Entities.ServiceTarget target, string @codeSlot)
    {
        _haContext.CallService("zha", "enable_lock_user_code", target, new
            {
                @code_slot = @codeSlot
            }
        );
    }

    public void IssueZigbeeClusterCommand(ZhaIssueZigbeeClusterCommandParameters data)
    {
        _haContext.CallService("zha", "issue_zigbee_cluster_command", null, data);
    }

    public void IssueZigbeeClusterCommand(string @ieee, long @endpointId, long @clusterId, long @command, string @commandType, string? @clusterType = null, object? @args = null, string? @manufacturer = null)
    {
        _haContext.CallService("zha", "issue_zigbee_cluster_command", null, new
            {
                @ieee, @endpoint_id = @endpointId, @cluster_id = @clusterId, @cluster_type = @clusterType, @command, @command_type = @commandType, @args, @manufacturer
            }
        );
    }

    public void IssueZigbeeGroupCommand(ZhaIssueZigbeeGroupCommandParameters data)
    {
        _haContext.CallService("zha", "issue_zigbee_group_command", null, data);
    }

    public void IssueZigbeeGroupCommand(string @group, long @clusterId, long @command, string? @clusterType = null, object? @args = null, string? @manufacturer = null)
    {
        _haContext.CallService("zha", "issue_zigbee_group_command", null, new
            {
                @group, @cluster_id = @clusterId, @cluster_type = @clusterType, @command, @args, @manufacturer
            }
        );
    }

    public void Permit(ZhaPermitParameters data)
    {
        _haContext.CallService("zha", "permit", null, data);
    }

    public void Permit(long? @duration = null, string? @ieee = null, string? @sourceIeee = null, string? @installCode = null, string? @qrCode = null)
    {
        _haContext.CallService("zha", "permit", null, new
            {
                @duration, @ieee, @source_ieee = @sourceIeee, @install_code = @installCode, @qr_code = @qrCode
            }
        );
    }

    public void Remove(ZhaRemoveParameters data)
    {
        _haContext.CallService("zha", "remove", null, data);
    }

    public void Remove(string @ieee)
    {
        _haContext.CallService("zha", "remove", null, new
            {
                @ieee
            }
        );
    }

    public void SetLockUserCode(NetDaemon.HassModel.Entities.ServiceTarget target, ZhaSetLockUserCodeParameters data)
    {
        _haContext.CallService("zha", "set_lock_user_code", target, data);
    }

    public void SetLockUserCode(NetDaemon.HassModel.Entities.ServiceTarget target, string @codeSlot, string @userCode)
    {
        _haContext.CallService("zha", "set_lock_user_code", target, new
            {
                @code_slot = @codeSlot, @user_code = @userCode
            }
        );
    }

    public void SetZigbeeClusterAttribute(ZhaSetZigbeeClusterAttributeParameters data)
    {
        _haContext.CallService("zha", "set_zigbee_cluster_attribute", null, data);
    }

    public void SetZigbeeClusterAttribute(string @ieee, long @endpointId, long @clusterId, long @attribute, string @value, string? @clusterType = null, string? @manufacturer = null)
    {
        _haContext.CallService("zha", "set_zigbee_cluster_attribute", null, new
            {
                @ieee, @endpoint_id = @endpointId, @cluster_id = @clusterId, @cluster_type = @clusterType, @attribute, @value, @manufacturer
            }
        );
    }

    public void WarningDeviceSquawk(ZhaWarningDeviceSquawkParameters data)
    {
        _haContext.CallService("zha", "warning_device_squawk", null, data);
    }

    public void WarningDeviceSquawk(string @ieee, long? @mode = null, long? @strobe = null, long? @level = null)
    {
        _haContext.CallService("zha", "warning_device_squawk", null, new
            {
                @ieee, @mode, @strobe, @level
            }
        );
    }

    public void WarningDeviceWarn(ZhaWarningDeviceWarnParameters data)
    {
        _haContext.CallService("zha", "warning_device_warn", null, data);
    }

    public void WarningDeviceWarn(string @ieee, long? @mode = null, long? @strobe = null, long? @level = null, long? @duration = null, long? @dutyCycle = null, long? @intensity = null)
    {
        _haContext.CallService("zha", "warning_device_warn", null, new
            {
                @ieee, @mode, @strobe, @level, @duration, @duty_cycle = @dutyCycle, @intensity
            }
        );
    }
}

public record ZhaClearLockUserCodeParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("codeSlot")]
    public string? CodeSlot { get; init; }
}

public record ZhaDisableLockUserCodeParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("codeSlot")]
    public string? CodeSlot { get; init; }
}

public record ZhaEnableLockUserCodeParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("codeSlot")]
    public string? CodeSlot { get; init; }
}

public record ZhaIssueZigbeeClusterCommandParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("ieee")]
    public string? Ieee { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("endpointId")]
    public long? EndpointId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("clusterId")]
    public long? ClusterId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("clusterType")]
    public string? ClusterType { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("command")]
    public long? Command { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("commandType")]
    public string? CommandType { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("args")]
    public object? Args { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("manufacturer")]
    public string? Manufacturer { get; init; }
}

public record ZhaIssueZigbeeGroupCommandParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("group")]
    public string? Group { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("clusterId")]
    public long? ClusterId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("clusterType")]
    public string? ClusterType { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("command")]
    public long? Command { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("args")]
    public object? Args { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("manufacturer")]
    public string? Manufacturer { get; init; }
}

public record ZhaPermitParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("duration")]
    public long? Duration { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("ieee")]
    public string? Ieee { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("sourceIeee")]
    public string? SourceIeee { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("installCode")]
    public string? InstallCode { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("qrCode")]
    public string? QrCode { get; init; }
}

public record ZhaRemoveParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("ieee")]
    public string? Ieee { get; init; }
}

public record ZhaSetLockUserCodeParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("codeSlot")]
    public string? CodeSlot { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("userCode")]
    public string? UserCode { get; init; }
}

public record ZhaSetZigbeeClusterAttributeParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("ieee")]
    public string? Ieee { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("endpointId")]
    public long? EndpointId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("clusterId")]
    public long? ClusterId { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("clusterType")]
    public string? ClusterType { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("attribute")]
    public long? Attribute { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("value")]
    public string? Value { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("manufacturer")]
    public string? Manufacturer { get; init; }
}

public record ZhaWarningDeviceSquawkParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("ieee")]
    public string? Ieee { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("mode")]
    public long? Mode { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("strobe")]
    public long? Strobe { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("level")]
    public long? Level { get; init; }
}

public record ZhaWarningDeviceWarnParameters
{
    [System.Text.Json.Serialization.JsonPropertyName("ieee")]
    public string? Ieee { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("mode")]
    public long? Mode { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("strobe")]
    public long? Strobe { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("level")]
    public long? Level { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("duration")]
    public long? Duration { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("dutyCycle")]
    public long? DutyCycle { get; init; }

    [System.Text.Json.Serialization.JsonPropertyName("intensity")]
    public long? Intensity { get; init; }
}

public class ZoneServices
{
    private readonly NetDaemon.HassModel.Common.IHaContext _haContext;

    public ZoneServices(NetDaemon.HassModel.Common.IHaContext haContext)
    {
        _haContext = haContext;
    }

    public void Reload()
    {
        _haContext.CallService("zone", "reload", null);
    }
}

public static class AlarmControlPanelEntityExtensionMethods
{
    public static void AlarmArmAway(this AlarmControlPanelEntity entity, AlarmControlPanelAlarmArmAwayParameters data)
    {
        entity.CallService("alarm_arm_away", data);
    }

    public static void AlarmArmAway(this AlarmControlPanelEntity entity, string? @code = null)
    {
        entity.CallService("alarm_arm_away", new
            {
                @code
            }
        );
    }

    public static void AlarmArmCustomBypass(this AlarmControlPanelEntity entity, AlarmControlPanelAlarmArmCustomBypassParameters data)
    {
        entity.CallService("alarm_arm_custom_bypass", data);
    }

    public static void AlarmArmCustomBypass(this AlarmControlPanelEntity entity, string? @code = null)
    {
        entity.CallService("alarm_arm_custom_bypass", new
            {
                @code
            }
        );
    }

    public static void AlarmArmHome(this AlarmControlPanelEntity entity, AlarmControlPanelAlarmArmHomeParameters data)
    {
        entity.CallService("alarm_arm_home", data);
    }

    public static void AlarmArmHome(this AlarmControlPanelEntity entity, string? @code = null)
    {
        entity.CallService("alarm_arm_home", new
            {
                @code
            }
        );
    }

    public static void AlarmArmNight(this AlarmControlPanelEntity entity, AlarmControlPanelAlarmArmNightParameters data)
    {
        entity.CallService("alarm_arm_night", data);
    }

    public static void AlarmArmNight(this AlarmControlPanelEntity entity, string? @code = null)
    {
        entity.CallService("alarm_arm_night", new
            {
                @code
            }
        );
    }

    public static void AlarmArmVacation(this AlarmControlPanelEntity entity, AlarmControlPanelAlarmArmVacationParameters data)
    {
        entity.CallService("alarm_arm_vacation", data);
    }

    public static void AlarmArmVacation(this AlarmControlPanelEntity entity, string? @code = null)
    {
        entity.CallService("alarm_arm_vacation", new
            {
                @code
            }
        );
    }

    public static void AlarmDisarm(this AlarmControlPanelEntity entity, AlarmControlPanelAlarmDisarmParameters data)
    {
        entity.CallService("alarm_disarm", data);
    }

    public static void AlarmDisarm(this AlarmControlPanelEntity entity, string? @code = null)
    {
        entity.CallService("alarm_disarm", new
            {
                @code
            }
        );
    }

    public static void AlarmTrigger(this AlarmControlPanelEntity entity, AlarmControlPanelAlarmTriggerParameters data)
    {
        entity.CallService("alarm_trigger", data);
    }

    public static void AlarmTrigger(this AlarmControlPanelEntity entity, string? @code = null)
    {
        entity.CallService("alarm_trigger", new
            {
                @code
            }
        );
    }
}

public static class AutomationEntityExtensionMethods
{
    public static void Toggle(this AutomationEntity entity)
    {
        entity.CallService("toggle");
    }

    public static void Trigger(this AutomationEntity entity, AutomationTriggerParameters data)
    {
        entity.CallService("trigger", data);
    }

    public static void Trigger(this AutomationEntity entity, bool? @skipCondition = null)
    {
        entity.CallService("trigger", new
            {
                @skip_condition = @skipCondition
            }
        );
    }

    public static void TurnOff(this AutomationEntity entity, AutomationTurnOffParameters data)
    {
        entity.CallService("turn_off", data);
    }

    public static void TurnOff(this AutomationEntity entity, bool? @stopActions = null)
    {
        entity.CallService("turn_off", new
            {
                @stop_actions = @stopActions
            }
        );
    }

    public static void TurnOn(this AutomationEntity entity)
    {
        entity.CallService("turn_on");
    }
}

public static class CameraEntityExtensionMethods
{
    public static void DisableMotionDetection(this CameraEntity entity)
    {
        entity.CallService("disable_motion_detection");
    }

    public static void EnableMotionDetection(this CameraEntity entity)
    {
        entity.CallService("enable_motion_detection");
    }

    public static void PlayStream(this CameraEntity entity, CameraPlayStreamParameters data)
    {
        entity.CallService("play_stream", data);
    }

    public static void PlayStream(this CameraEntity entity, string @mediaPlayer, string? @format = null)
    {
        entity.CallService("play_stream", new
            {
                @media_player = @mediaPlayer, @format
            }
        );
    }

    public static void Record(this CameraEntity entity, CameraRecordParameters data)
    {
        entity.CallService("record", data);
    }

    public static void Record(this CameraEntity entity, string @filename, long? @duration = null, long? @lookback = null)
    {
        entity.CallService("record", new
            {
                @filename, @duration, @lookback
            }
        );
    }

    public static void Snapshot(this CameraEntity entity, CameraSnapshotParameters data)
    {
        entity.CallService("snapshot", data);
    }

    public static void Snapshot(this CameraEntity entity, string @filename)
    {
        entity.CallService("snapshot", new
            {
                @filename
            }
        );
    }

    public static void TurnOff(this CameraEntity entity)
    {
        entity.CallService("turn_off");
    }

    public static void TurnOn(this CameraEntity entity)
    {
        entity.CallService("turn_on");
    }
}

public static class ClimateEntityExtensionMethods
{
    public static void SetAuxHeat(this ClimateEntity entity, ClimateSetAuxHeatParameters data)
    {
        entity.CallService("set_aux_heat", data);
    }

    public static void SetAuxHeat(this ClimateEntity entity, bool @auxHeat)
    {
        entity.CallService("set_aux_heat", new
            {
                @aux_heat = @auxHeat
            }
        );
    }

    public static void SetFanMode(this ClimateEntity entity, ClimateSetFanModeParameters data)
    {
        entity.CallService("set_fan_mode", data);
    }

    public static void SetFanMode(this ClimateEntity entity, string @fanMode)
    {
        entity.CallService("set_fan_mode", new
            {
                @fan_mode = @fanMode
            }
        );
    }

    public static void SetHumidity(this ClimateEntity entity, ClimateSetHumidityParameters data)
    {
        entity.CallService("set_humidity", data);
    }

    public static void SetHumidity(this ClimateEntity entity, long @humidity)
    {
        entity.CallService("set_humidity", new
            {
                @humidity
            }
        );
    }

    public static void SetHvacMode(this ClimateEntity entity, ClimateSetHvacModeParameters data)
    {
        entity.CallService("set_hvac_mode", data);
    }

    public static void SetHvacMode(this ClimateEntity entity, string? @hvacMode = null)
    {
        entity.CallService("set_hvac_mode", new
            {
                @hvac_mode = @hvacMode
            }
        );
    }

    public static void SetPresetMode(this ClimateEntity entity, ClimateSetPresetModeParameters data)
    {
        entity.CallService("set_preset_mode", data);
    }

    public static void SetPresetMode(this ClimateEntity entity, string @presetMode)
    {
        entity.CallService("set_preset_mode", new
            {
                @preset_mode = @presetMode
            }
        );
    }

    public static void SetSwingMode(this ClimateEntity entity, ClimateSetSwingModeParameters data)
    {
        entity.CallService("set_swing_mode", data);
    }

    public static void SetSwingMode(this ClimateEntity entity, string @swingMode)
    {
        entity.CallService("set_swing_mode", new
            {
                @swing_mode = @swingMode
            }
        );
    }

    public static void SetTemperature(this ClimateEntity entity, ClimateSetTemperatureParameters data)
    {
        entity.CallService("set_temperature", data);
    }

    public static void SetTemperature(this ClimateEntity entity, double? @temperature = null, double? @targetTempHigh = null, double? @targetTempLow = null, string? @hvacMode = null)
    {
        entity.CallService("set_temperature", new
            {
                @temperature, @target_temp_high = @targetTempHigh, @target_temp_low = @targetTempLow, @hvac_mode = @hvacMode
            }
        );
    }

    public static void TurnOff(this ClimateEntity entity)
    {
        entity.CallService("turn_off");
    }

    public static void TurnOn(this ClimateEntity entity)
    {
        entity.CallService("turn_on");
    }
}

public static class CoverEntityExtensionMethods
{
    public static void CloseCover(this CoverEntity entity)
    {
        entity.CallService("close_cover");
    }

    public static void CloseCoverTilt(this CoverEntity entity)
    {
        entity.CallService("close_cover_tilt");
    }

    public static void OpenCover(this CoverEntity entity)
    {
        entity.CallService("open_cover");
    }

    public static void OpenCoverTilt(this CoverEntity entity)
    {
        entity.CallService("open_cover_tilt");
    }

    public static void SetCoverPosition(this CoverEntity entity, CoverSetCoverPositionParameters data)
    {
        entity.CallService("set_cover_position", data);
    }

    public static void SetCoverPosition(this CoverEntity entity, long @position)
    {
        entity.CallService("set_cover_position", new
            {
                @position
            }
        );
    }

    public static void SetCoverTiltPosition(this CoverEntity entity, CoverSetCoverTiltPositionParameters data)
    {
        entity.CallService("set_cover_tilt_position", data);
    }

    public static void SetCoverTiltPosition(this CoverEntity entity, long @tiltPosition)
    {
        entity.CallService("set_cover_tilt_position", new
            {
                @tilt_position = @tiltPosition
            }
        );
    }

    public static void StopCover(this CoverEntity entity)
    {
        entity.CallService("stop_cover");
    }

    public static void StopCoverTilt(this CoverEntity entity)
    {
        entity.CallService("stop_cover_tilt");
    }

    public static void Toggle(this CoverEntity entity)
    {
        entity.CallService("toggle");
    }

    public static void ToggleCoverTilt(this CoverEntity entity)
    {
        entity.CallService("toggle_cover_tilt");
    }
}

public static class InputBooleanEntityExtensionMethods
{
    public static void Toggle(this InputBooleanEntity entity)
    {
        entity.CallService("toggle");
    }

    public static void TurnOff(this InputBooleanEntity entity)
    {
        entity.CallService("turn_off");
    }

    public static void TurnOn(this InputBooleanEntity entity)
    {
        entity.CallService("turn_on");
    }
}

public static class InputNumberEntityExtensionMethods
{
    public static void Decrement(this InputNumberEntity entity)
    {
        entity.CallService("decrement");
    }

    public static void Increment(this InputNumberEntity entity)
    {
        entity.CallService("increment");
    }

    public static void SetValue(this InputNumberEntity entity, InputNumberSetValueParameters data)
    {
        entity.CallService("set_value", data);
    }

    public static void SetValue(this InputNumberEntity entity, double @value)
    {
        entity.CallService("set_value", new
            {
                @value
            }
        );
    }
}

public static class InputSelectEntityExtensionMethods
{
    public static void SelectFirst(this InputSelectEntity entity)
    {
        entity.CallService("select_first");
    }

    public static void SelectLast(this InputSelectEntity entity)
    {
        entity.CallService("select_last");
    }

    public static void SelectNext(this InputSelectEntity entity, InputSelectSelectNextParameters data)
    {
        entity.CallService("select_next", data);
    }

    public static void SelectNext(this InputSelectEntity entity, bool? @cycle = null)
    {
        entity.CallService("select_next", new
            {
                @cycle
            }
        );
    }

    public static void SelectOption(this InputSelectEntity entity, InputSelectSelectOptionParameters data)
    {
        entity.CallService("select_option", data);
    }

    public static void SelectOption(this InputSelectEntity entity, string @option)
    {
        entity.CallService("select_option", new
            {
                @option
            }
        );
    }

    public static void SelectPrevious(this InputSelectEntity entity, InputSelectSelectPreviousParameters data)
    {
        entity.CallService("select_previous", data);
    }

    public static void SelectPrevious(this InputSelectEntity entity, bool? @cycle = null)
    {
        entity.CallService("select_previous", new
            {
                @cycle
            }
        );
    }

    public static void SetOptions(this InputSelectEntity entity, InputSelectSetOptionsParameters data)
    {
        entity.CallService("set_options", data);
    }

    public static void SetOptions(this InputSelectEntity entity, object @options)
    {
        entity.CallService("set_options", new
            {
                @options
            }
        );
    }
}

public static class InputTextEntityExtensionMethods
{
    public static void SetValue(this InputTextEntity entity, InputTextSetValueParameters data)
    {
        entity.CallService("set_value", data);
    }

    public static void SetValue(this InputTextEntity entity, string @value)
    {
        entity.CallService("set_value", new
            {
                @value
            }
        );
    }
}

public static class LightEntityExtensionMethods
{
    public static void Toggle(this LightEntity entity, LightToggleParameters data)
    {
        entity.CallService("toggle", data);
    }

    public static void Toggle(this LightEntity entity, long? @transition = null, object? @rgbColor = null, string? @colorName = null, object? @hsColor = null, object? @xyColor = null, long? @colorTemp = null, long? @kelvin = null, long? @whiteValue = null, long? @brightness = null, long? @brightnessPct = null, string? @profile = null, string? @flash = null, string? @effect = null)
    {
        entity.CallService("toggle", new
            {
                @transition, @rgb_color = @rgbColor, @color_name = @colorName, @hs_color = @hsColor, @xy_color = @xyColor, @color_temp = @colorTemp, @kelvin, @white_value = @whiteValue, @brightness, @brightness_pct = @brightnessPct, @profile, @flash, @effect
            }
        );
    }

    public static void TurnOff(this LightEntity entity, LightTurnOffParameters data)
    {
        entity.CallService("turn_off", data);
    }

    public static void TurnOff(this LightEntity entity, long? @transition = null, string? @flash = null)
    {
        entity.CallService("turn_off", new
            {
                @transition, @flash
            }
        );
    }

    public static void TurnOn(this LightEntity entity, LightTurnOnParameters data)
    {
        entity.CallService("turn_on", data);
    }

    public static void TurnOn(this LightEntity entity, long? @transition = null, object? @rgbColor = null, object? @rgbwColor = null, object? @rgbwwColor = null, string? @colorName = null, object? @hsColor = null, object? @xyColor = null, long? @colorTemp = null, long? @kelvin = null, long? @brightness = null, long? @brightnessPct = null, long? @brightnessStep = null, long? @brightnessStepPct = null, long? @white = null, string? @profile = null, string? @flash = null, string? @effect = null)
    {
        entity.CallService("turn_on", new
            {
                @transition, @rgb_color = @rgbColor, @rgbw_color = @rgbwColor, @rgbww_color = @rgbwwColor, @color_name = @colorName, @hs_color = @hsColor, @xy_color = @xyColor, @color_temp = @colorTemp, @kelvin, @brightness, @brightness_pct = @brightnessPct, @brightness_step = @brightnessStep, @brightness_step_pct = @brightnessStepPct, @white, @profile, @flash, @effect
            }
        );
    }
}

public static class LockEntityExtensionMethods
{
    public static void Lock(this LockEntity entity, LockLockParameters data)
    {
        entity.CallService("lock", data);
    }

    public static void Lock(this LockEntity entity, string? @code = null)
    {
        entity.CallService("lock", new
            {
                @code
            }
        );
    }

    public static void Open(this LockEntity entity, LockOpenParameters data)
    {
        entity.CallService("open", data);
    }

    public static void Open(this LockEntity entity, string? @code = null)
    {
        entity.CallService("open", new
            {
                @code
            }
        );
    }

    public static void Unlock(this LockEntity entity, LockUnlockParameters data)
    {
        entity.CallService("unlock", data);
    }

    public static void Unlock(this LockEntity entity, string? @code = null)
    {
        entity.CallService("unlock", new
            {
                @code
            }
        );
    }
}

public static class MediaPlayerEntityExtensionMethods
{
    public static void ClearPlaylist(this MediaPlayerEntity entity)
    {
        entity.CallService("clear_playlist");
    }

    public static void Join(this MediaPlayerEntity entity, MediaPlayerJoinParameters data)
    {
        entity.CallService("join", data);
    }

    public static void Join(this MediaPlayerEntity entity, object? @groupMembers = null)
    {
        entity.CallService("join", new
            {
                @group_members = @groupMembers
            }
        );
    }

    public static void MediaNextTrack(this MediaPlayerEntity entity)
    {
        entity.CallService("media_next_track");
    }

    public static void MediaPause(this MediaPlayerEntity entity)
    {
        entity.CallService("media_pause");
    }

    public static void MediaPlay(this MediaPlayerEntity entity)
    {
        entity.CallService("media_play");
    }

    public static void MediaPlayPause(this MediaPlayerEntity entity)
    {
        entity.CallService("media_play_pause");
    }

    public static void MediaPreviousTrack(this MediaPlayerEntity entity)
    {
        entity.CallService("media_previous_track");
    }

    public static void MediaSeek(this MediaPlayerEntity entity, MediaPlayerMediaSeekParameters data)
    {
        entity.CallService("media_seek", data);
    }

    public static void MediaSeek(this MediaPlayerEntity entity, double @seekPosition)
    {
        entity.CallService("media_seek", new
            {
                @seek_position = @seekPosition
            }
        );
    }

    public static void MediaStop(this MediaPlayerEntity entity)
    {
        entity.CallService("media_stop");
    }

    public static void PlayMedia(this MediaPlayerEntity entity, MediaPlayerPlayMediaParameters data)
    {
        entity.CallService("play_media", data);
    }

    public static void PlayMedia(this MediaPlayerEntity entity, string @mediaContentId, string @mediaContentType)
    {
        entity.CallService("play_media", new
            {
                @media_content_id = @mediaContentId, @media_content_type = @mediaContentType
            }
        );
    }

    public static void RepeatSet(this MediaPlayerEntity entity, MediaPlayerRepeatSetParameters data)
    {
        entity.CallService("repeat_set", data);
    }

    public static void RepeatSet(this MediaPlayerEntity entity, string @repeat)
    {
        entity.CallService("repeat_set", new
            {
                @repeat
            }
        );
    }

    public static void SelectSoundMode(this MediaPlayerEntity entity, MediaPlayerSelectSoundModeParameters data)
    {
        entity.CallService("select_sound_mode", data);
    }

    public static void SelectSoundMode(this MediaPlayerEntity entity, string? @soundMode = null)
    {
        entity.CallService("select_sound_mode", new
            {
                @sound_mode = @soundMode
            }
        );
    }

    public static void SelectSource(this MediaPlayerEntity entity, MediaPlayerSelectSourceParameters data)
    {
        entity.CallService("select_source", data);
    }

    public static void SelectSource(this MediaPlayerEntity entity, string @source)
    {
        entity.CallService("select_source", new
            {
                @source
            }
        );
    }

    public static void ShuffleSet(this MediaPlayerEntity entity, MediaPlayerShuffleSetParameters data)
    {
        entity.CallService("shuffle_set", data);
    }

    public static void ShuffleSet(this MediaPlayerEntity entity, bool @shuffle)
    {
        entity.CallService("shuffle_set", new
            {
                @shuffle
            }
        );
    }

    public static void Toggle(this MediaPlayerEntity entity)
    {
        entity.CallService("toggle");
    }

    public static void TurnOff(this MediaPlayerEntity entity)
    {
        entity.CallService("turn_off");
    }

    public static void TurnOn(this MediaPlayerEntity entity)
    {
        entity.CallService("turn_on");
    }

    public static void Unjoin(this MediaPlayerEntity entity)
    {
        entity.CallService("unjoin");
    }

    public static void VolumeDown(this MediaPlayerEntity entity)
    {
        entity.CallService("volume_down");
    }

    public static void VolumeMute(this MediaPlayerEntity entity, MediaPlayerVolumeMuteParameters data)
    {
        entity.CallService("volume_mute", data);
    }

    public static void VolumeMute(this MediaPlayerEntity entity, bool @isVolumeMuted)
    {
        entity.CallService("volume_mute", new
            {
                @is_volume_muted = @isVolumeMuted
            }
        );
    }

    public static void VolumeSet(this MediaPlayerEntity entity, MediaPlayerVolumeSetParameters data)
    {
        entity.CallService("volume_set", data);
    }

    public static void VolumeSet(this MediaPlayerEntity entity, double @volumeLevel)
    {
        entity.CallService("volume_set", new
            {
                @volume_level = @volumeLevel
            }
        );
    }

    public static void VolumeUp(this MediaPlayerEntity entity)
    {
        entity.CallService("volume_up");
    }
}

public static class MelcloudEntityExtensionMethods
{
    public static void SetVaneHorizontal(this ClimateEntity entity, MelcloudSetVaneHorizontalParameters data)
    {
        entity.CallService("set_vane_horizontal", data);
    }

    public static void SetVaneHorizontal(this ClimateEntity entity, string @position)
    {
        entity.CallService("set_vane_horizontal", new
            {
                @position
            }
        );
    }

    public static void SetVaneVertical(this ClimateEntity entity, MelcloudSetVaneVerticalParameters data)
    {
        entity.CallService("set_vane_vertical", data);
    }

    public static void SetVaneVertical(this ClimateEntity entity, string @position)
    {
        entity.CallService("set_vane_vertical", new
            {
                @position
            }
        );
    }
}

public static class NumberEntityExtensionMethods
{
    public static void SetValue(this NumberEntity entity, NumberSetValueParameters data)
    {
        entity.CallService("set_value", data);
    }

    public static void SetValue(this NumberEntity entity, string? @value = null)
    {
        entity.CallService("set_value", new
            {
                @value
            }
        );
    }
}

public static class PiHoleEntityExtensionMethods
{
    public static void Disable(this SwitchEntity entity, PiHoleDisableParameters data)
    {
        entity.CallService("disable", data);
    }

    public static void Disable(this SwitchEntity entity, string @duration)
    {
        entity.CallService("disable", new
            {
                @duration
            }
        );
    }
}

public static class RemoteEntityExtensionMethods
{
    public static void DeleteCommand(this RemoteEntity entity, RemoteDeleteCommandParameters data)
    {
        entity.CallService("delete_command", data);
    }

    public static void DeleteCommand(this RemoteEntity entity, object @command, string? @device = null)
    {
        entity.CallService("delete_command", new
            {
                @device, @command
            }
        );
    }

    public static void LearnCommand(this RemoteEntity entity, RemoteLearnCommandParameters data)
    {
        entity.CallService("learn_command", data);
    }

    public static void LearnCommand(this RemoteEntity entity, string? @device = null, object? @command = null, string? @commandType = null, bool? @alternative = null, long? @timeout = null)
    {
        entity.CallService("learn_command", new
            {
                @device, @command, @command_type = @commandType, @alternative, @timeout
            }
        );
    }

    public static void SendCommand(this RemoteEntity entity, RemoteSendCommandParameters data)
    {
        entity.CallService("send_command", data);
    }

    public static void SendCommand(this RemoteEntity entity, string @command, string? @device = null, long? @numRepeats = null, double? @delaySecs = null, double? @holdSecs = null)
    {
        entity.CallService("send_command", new
            {
                @device, @command, @num_repeats = @numRepeats, @delay_secs = @delaySecs, @hold_secs = @holdSecs
            }
        );
    }

    public static void Toggle(this RemoteEntity entity)
    {
        entity.CallService("toggle");
    }

    public static void TurnOff(this RemoteEntity entity)
    {
        entity.CallService("turn_off");
    }

    public static void TurnOn(this RemoteEntity entity, RemoteTurnOnParameters data)
    {
        entity.CallService("turn_on", data);
    }

    public static void TurnOn(this RemoteEntity entity, string? @activity = null)
    {
        entity.CallService("turn_on", new
            {
                @activity
            }
        );
    }
}

public static class ScriptEntityExtensionMethods
{
    public static void Toggle(this ScriptEntity entity)
    {
        entity.CallService("toggle");
    }

    public static void TurnOff(this ScriptEntity entity)
    {
        entity.CallService("turn_off");
    }

    public static void TurnOn(this ScriptEntity entity)
    {
        entity.CallService("turn_on");
    }
}

public static class SelectEntityExtensionMethods
{
    public static void SelectOption(this SelectEntity entity, SelectSelectOptionParameters data)
    {
        entity.CallService("select_option", data);
    }

    public static void SelectOption(this SelectEntity entity, string @option)
    {
        entity.CallService("select_option", new
            {
                @option
            }
        );
    }
}

public static class SonosEntityExtensionMethods
{
    public static void Unjoin(this MediaPlayerEntity entity)
    {
        entity.CallService("unjoin");
    }
}

public static class SwitchEntityExtensionMethods
{
    public static void Toggle(this SwitchEntity entity)
    {
        entity.CallService("toggle");
    }

    public static void TurnOff(this SwitchEntity entity)
    {
        entity.CallService("turn_off");
    }

    public static void TurnOn(this SwitchEntity entity)
    {
        entity.CallService("turn_on");
    }
}

public static class TimerEntityExtensionMethods
{
    public static void Cancel(this TimerEntity entity)
    {
        entity.CallService("cancel");
    }

    public static void Finish(this TimerEntity entity)
    {
        entity.CallService("finish");
    }

    public static void Pause(this TimerEntity entity)
    {
        entity.CallService("pause");
    }

    public static void Start(this TimerEntity entity, TimerStartParameters data)
    {
        entity.CallService("start", data);
    }

    public static void Start(this TimerEntity entity, string? @duration = null)
    {
        entity.CallService("start", new
            {
                @duration
            }
        );
    }
}

public static class UtilityMeterEntityExtensionMethods
{
    public static void Calibrate(this SensorEntity entity, UtilityMeterCalibrateParameters data)
    {
        entity.CallService("calibrate", data);
    }

    public static void Calibrate(this SensorEntity entity, string @value)
    {
        entity.CallService("calibrate", new
            {
                @value
            }
        );
    }
}

public static class ZhaEntityExtensionMethods
{
    public static void ClearLockUserCode(this LockEntity entity, ZhaClearLockUserCodeParameters data)
    {
        entity.CallService("clear_lock_user_code", data);
    }

    public static void ClearLockUserCode(this LockEntity entity, string @codeSlot)
    {
        entity.CallService("clear_lock_user_code", new
            {
                @code_slot = @codeSlot
            }
        );
    }

    public static void DisableLockUserCode(this LockEntity entity, ZhaDisableLockUserCodeParameters data)
    {
        entity.CallService("disable_lock_user_code", data);
    }

    public static void DisableLockUserCode(this LockEntity entity, string @codeSlot)
    {
        entity.CallService("disable_lock_user_code", new
            {
                @code_slot = @codeSlot
            }
        );
    }

    public static void EnableLockUserCode(this LockEntity entity, ZhaEnableLockUserCodeParameters data)
    {
        entity.CallService("enable_lock_user_code", data);
    }

    public static void EnableLockUserCode(this LockEntity entity, string @codeSlot)
    {
        entity.CallService("enable_lock_user_code", new
            {
                @code_slot = @codeSlot
            }
        );
    }

    public static void SetLockUserCode(this LockEntity entity, ZhaSetLockUserCodeParameters data)
    {
        entity.CallService("set_lock_user_code", data);
    }

    public static void SetLockUserCode(this LockEntity entity, string @codeSlot, string @userCode)
    {
        entity.CallService("set_lock_user_code", new
            {
                @code_slot = @codeSlot, @user_code = @userCode
            }
        );
    }
}