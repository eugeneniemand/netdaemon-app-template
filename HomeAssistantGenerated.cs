using System;
using System.Collections.Generic;
using NetDaemon.HassModel;
using NetDaemon.HassModel.Entities;
using System.Text.Json.Serialization;

namespace HomeAssistantGenerated
{
	public interface IEntities
	{
		AlarmControlPanelEntities AlarmControlPanel { get; }

		AutomationEntities Automation { get; }

		BinarySensorEntities BinarySensor { get; }

		ButtonEntities Button { get; }

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
		private readonly IHaContext _haContext;
		public Entities(IHaContext haContext)
		{
			_haContext = haContext;
		}

		public AlarmControlPanelEntities AlarmControlPanel => new(_haContext);
		public AutomationEntities Automation => new(_haContext);
		public BinarySensorEntities BinarySensor => new(_haContext);
		public ButtonEntities Button => new(_haContext);
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
		private readonly IHaContext _haContext;
		public AlarmControlPanelEntities(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Alarmo</summary>
		public AlarmControlPanelEntity Alarmo => new(_haContext, "alarm_control_panel.alarmo");
	}

	public class AutomationEntities
	{
		private readonly IHaContext _haContext;
		public AutomationEntities(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Alaram Arriving Home</summary>
		public AutomationEntity AlaramArrivingHome => new(_haContext, "automation.alaram_arriving_home");
		///<summary>Alaram Leaving Home</summary>
		public AutomationEntity AlaramLeavingHome => new(_haContext, "automation.alaram_leaving_home");
		///<summary>Alarm Triggered</summary>
		public AutomationEntity AlarmTriggered => new(_haContext, "automation.alarm_triggered");
		///<summary>Arrive Home</summary>
		public AutomationEntity ArriveHome => new(_haContext, "automation.arrive_home");
		///<summary>Controller - IKEA E1524/E1810 5-Button Remote</summary>
		public AutomationEntity ControllerIkeaE1524E18105ButtonRemote => new(_haContext, "automation.controller_ikea_e1524_e1810_5_button_remote");
		///<summary>Ding Blinks Office</summary>
		public AutomationEntity DingBlinksOffice => new(_haContext, "automation.ding_blinks_office");
		///<summary>Door Chime</summary>
		public AutomationEntity DoorChime => new(_haContext, "automation.door_chime");
		///<summary>Door Lock Checks</summary>
		public AutomationEntity DoorLockChecks => new(_haContext, "automation.door_lock_checks");
		///<summary>Door Lock Checks</summary>
		public AutomationEntity DoorLockChecks2 => new(_haContext, "automation.door_lock_checks_2");
		///<summary>Door Notify</summary>
		public AutomationEntity DoorNotify => new(_haContext, "automation.door_notify");
		///<summary>Doorbell</summary>
		public AutomationEntity Doorbell => new(_haContext, "automation.doorbell");
		///<summary>Dryer Done Response</summary>
		public AutomationEntity DryerDoneResponse => new(_haContext, "automation.dryer_done_response");
		///<summary>Dryer Notification</summary>
		public AutomationEntity DryerNotification => new(_haContext, "automation.dryer_notification");
		///<summary>Dryer Reset</summary>
		public AutomationEntity DryerReset => new(_haContext, "automation.dryer_reset");
		///<summary>Front Door Motion</summary>
		public AutomationEntity FrontDoorMotion => new(_haContext, "automation.front_door_motion");
		///<summary>Granny Arrives</summary>
		public AutomationEntity GrannyArrives => new(_haContext, "automation.granny_arrives");
		///<summary>Hass Started</summary>
		public AutomationEntity HassStarted => new(_haContext, "automation.hass_started");
		///<summary>House Mode Request</summary>
		public AutomationEntity HouseModeRequest => new(_haContext, "automation.house_mode_request");
		///<summary>House Mode Response</summary>
		public AutomationEntity HouseModeResponse => new(_haContext, "automation.house_mode_response");
		///<summary>IKEA Open/Close Remote</summary>
		public AutomationEntity IkeaOpenCloseRemote => new(_haContext, "automation.ikea_open_close_remote");
		///<summary>In Bed</summary>
		public AutomationEntity InBed => new(_haContext, "automation.in_bed");
		///<summary>Jayden Alarm Trigger</summary>
		public AutomationEntity JaydenAlarmTrigger => new(_haContext, "automation.jayden_alarm_trigger");
		///<summary>Jayden Keep in bed</summary>
		public AutomationEntity KeepJaydenInBed => new(_haContext, "automation.keep_jayden_in_bed");
		///<summary>Laundry Done Request</summary>
		public AutomationEntity LaundryDoneRequest => new(_haContext, "automation.laundry_done_request");
		///<summary>Laundry Done Response</summary>
		public AutomationEntity LaundryDoneResponse => new(_haContext, "automation.laundry_done_response");
		///<summary>Leave Home</summary>
		public AutomationEntity LeaveHome => new(_haContext, "automation.leave_home");
		///<summary>Night Mode Request</summary>
		public AutomationEntity NightModeRequest => new(_haContext, "automation.night_mode_request");
		///<summary>Night Mode Response</summary>
		public AutomationEntity NightModeResponse => new(_haContext, "automation.night_mode_response");
		///<summary>Jayden Notify out of bed</summary>
		public AutomationEntity NotifyOutOfBed => new(_haContext, "automation.notify_out_of_bed");
		///<summary>Office Door Left Open Request</summary>
		public AutomationEntity OfficeDoorLeftOpenRequest => new(_haContext, "automation.office_door_left_open_request");
		///<summary>Office Door Left Open Response</summary>
		public AutomationEntity OfficeDoorLeftOpenResponse => new(_haContext, "automation.office_door_left_open_response");
		///<summary>Re Enable Pi Hole </summary>
		public AutomationEntity ReEnablePiHole => new(_haContext, "automation.re_enable_pi_hole");
		///<summary>Refresh Audi Data</summary>
		public AutomationEntity RefreshAudiData => new(_haContext, "automation.refresh_audi_data");
		///<summary>Remote Trial</summary>
		public AutomationEntity RemoteTrial => new(_haContext, "automation.remote_trial");
		///<summary>reset radiator mode</summary>
		public AutomationEntity ResetRadiatorMode => new(_haContext, "automation.reset_radiator_mode");
		///<summary>Set Ring Snapshot Interval on Startup</summary>
		public AutomationEntity SetRingSnapshotIntervalOnStartup => new(_haContext, "automation.set_ring_snapshot_interval_on_startup");
		///<summary>Snooze Ring Chime</summary>
		public AutomationEntity SnoozeRingChime => new(_haContext, "automation.snooze_ring_chime");
		///<summary>Snow machine</summary>
		public AutomationEntity SnowMachine => new(_haContext, "automation.snow_machine");
		///<summary>Turn off Lights when leaving home</summary>
		public AutomationEntity TurnOffLightsWhenLeavingHome => new(_haContext, "automation.turn_off_lights_when_leaving_home");
		///<summary>Update Netdaemon and restart container</summary>
		public AutomationEntity UpdateNetdaemonAndRestartContainer => new(_haContext, "automation.update_netdaemon_and_restart_container");
		///<summary>Utilities Acknowledged</summary>
		public AutomationEntity UtilitiesAcknowledged => new(_haContext, "automation.utilities_acknowledged");
		///<summary>Wakeup</summary>
		public AutomationEntity Wakeup => new(_haContext, "automation.wakeup");
		///<summary>Washing Done Response</summary>
		public AutomationEntity WashingDoneResponse => new(_haContext, "automation.washing_done_response");
		///<summary>Washing Notification</summary>
		public AutomationEntity WashingNotification => new(_haContext, "automation.washing_notification");
		///<summary>Washing Reset</summary>
		public AutomationEntity WashingReset => new(_haContext, "automation.washing_reset");
	}

	public class BinarySensorEntities
	{
		private readonly IHaContext _haContext;
		public BinarySensorEntities(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Aaron Motion</summary>
		public BinarySensorEntity AaronMotion => new(_haContext, "binary_sensor.aaron_motion");
		///<summary>Aaron Motion Occupancy</summary>
		public BinarySensorEntity AaronMotionOccupancy => new(_haContext, "binary_sensor.aaron_motion_occupancy");
		///<summary>Aubrecia Drive Motion</summary>
		public BinarySensorEntity AubreciaDriveMotion => new(_haContext, "binary_sensor.aubrecia_drive_motion");
		///<summary>Aubrecia Front Door Ding</summary>
		public BinarySensorEntity AubreciaFrontDoorDing => new(_haContext, "binary_sensor.aubrecia_front_door_ding");
		///<summary>Aubrecia Front Door Motion</summary>
		public BinarySensorEntity AubreciaFrontDoorMotion => new(_haContext, "binary_sensor.aubrecia_front_door_motion");
		///<summary>Audi Q7 Hood</summary>
		public BinarySensorEntity AudiQ7Bonnet => new(_haContext, "binary_sensor.audi_q7_bonnet");
		///<summary>Audi Q7 Trunk</summary>
		public BinarySensorEntity AudiQ7Boot => new(_haContext, "binary_sensor.audi_q7_boot");
		///<summary>Audi Q7 Trunk lock</summary>
		public BinarySensorEntity AudiQ7BootLock => new(_haContext, "binary_sensor.audi_q7_boot_lock");
		///<summary>Audi Q7 Doors</summary>
		public BinarySensorEntity AudiQ7Doors => new(_haContext, "binary_sensor.audi_q7_doors");
		///<summary>Audi Q7 Doors lock</summary>
		public BinarySensorEntity AudiQ7DoorsLock => new(_haContext, "binary_sensor.audi_q7_doors_lock");
		///<summary>Audi Q7 Parking light</summary>
		public BinarySensorEntity AudiQ7ParkingLight => new(_haContext, "binary_sensor.audi_q7_parking_light");
		///<summary>Audi Q7 Windows</summary>
		public BinarySensorEntity AudiQ7Windows => new(_haContext, "binary_sensor.audi_q7_windows");
		///<summary>Back Door</summary>
		public BinarySensorEntity BackDoor => new(_haContext, "binary_sensor.back_door");
		///<summary>Bathroom Motion</summary>
		public BinarySensorEntity BathroomMotion => new(_haContext, "binary_sensor.bathroom_motion");
		///<summary>Bathroom Motion Occupancy</summary>
		public BinarySensorEntity BathroomMotionOccupancy => new(_haContext, "binary_sensor.bathroom_motion_occupancy");
		///<summary>Blind Lounge Overheating</summary>
		public BinarySensorEntity BlindLoungeOverheating => new(_haContext, "binary_sensor.blind_lounge_overheating");
		///<summary>Dining</summary>
		public BinarySensorEntity Dining => new(_haContext, "binary_sensor.dining");
		///<summary>Dining Door</summary>
		public BinarySensorEntity DiningDoor => new(_haContext, "binary_sensor.dining_door");
		///<summary>Dining Motion</summary>
		public BinarySensorEntity DiningMotion => new(_haContext, "binary_sensor.dining_motion");
		///<summary>Dining Occupancy</summary>
		public BinarySensorEntity DiningOccupancy => new(_haContext, "binary_sensor.dining_occupancy");
		///<summary>Entrance Motion</summary>
		public BinarySensorEntity EntranceMotion => new(_haContext, "binary_sensor.entrance_motion");
		///<summary>Entrance Motion Occupancy</summary>
		public BinarySensorEntity EntranceMotionOccupancy => new(_haContext, "binary_sensor.entrance_motion_occupancy");
		///<summary>EUGENE-DESKTOP Battery Status</summary>
		public BinarySensorEntity EugeneDesktopBatteryStatus => new(_haContext, "binary_sensor.eugene_desktop_battery_status");
		///<summary>EUGENE-DESKTOP Network #0 - Wired</summary>
		public BinarySensorEntity EugeneDesktopNetwork0Wired => new(_haContext, "binary_sensor.eugene_desktop_network_0_wired");
		///<summary>EUGENE-DESKTOP Network #1 - Wired</summary>
		public BinarySensorEntity EugeneDesktopNetwork1Wired => new(_haContext, "binary_sensor.eugene_desktop_network_1_wired");
		///<summary>EUGENE-DESKTOP Power Status</summary>
		public BinarySensorEntity EugeneDesktopPowerStatus => new(_haContext, "binary_sensor.eugene_desktop_power_status");
		///<summary>Eugene Laptop In Use</summary>
		public BinarySensorEntity EugeneLaptopInUse => new(_haContext, "binary_sensor.eugene_laptop_in_use");
		///<summary>Eugene’s iPhone Focus</summary>
		public BinarySensorEntity EugenesIphoneFocus => new(_haContext, "binary_sensor.eugenes_iphone_focus");
		///<summary>Front Door</summary>
		public BinarySensorEntity FrontDoor => new(_haContext, "binary_sensor.front_door");
		///<summary>Garage Door</summary>
		public BinarySensorEntity GarageBackDoor => new(_haContext, "binary_sensor.garage_back_door");
		///<summary>Garden Motion</summary>
		public BinarySensorEntity GardenMotion2 => new(_haContext, "binary_sensor.garden_motion_2");
		///<summary>Hailey's iPhone Focus</summary>
		public BinarySensorEntity HaileySIphoneFocus => new(_haContext, "binary_sensor.hailey_s_iphone_focus");
		///<summary>Hailey’s MacBook Air Active</summary>
		public BinarySensorEntity HaileysMacbookAirActive => new(_haContext, "binary_sensor.haileys_macbook_air_active");
		///<summary>Hailey’s MacBook Air Audio Output In Use</summary>
		public BinarySensorEntity HaileysMacbookAirAudioOutputInUse => new(_haContext, "binary_sensor.haileys_macbook_air_audio_output_in_use");
		///<summary>Hailey’s MacBook Air Camera In Use</summary>
		public BinarySensorEntity HaileysMacbookAirCameraInUse => new(_haContext, "binary_sensor.haileys_macbook_air_camera_in_use");
		///<summary>Hailey’s MacBook Air FaceTime HD Camera (Built-in)</summary>
		public BinarySensorEntity HaileysMacbookAirFacetimeHdCameraBuiltIn => new(_haContext, "binary_sensor.haileys_macbook_air_facetime_hd_camera_built_in");
		///<summary>Hailey’s MacBook Air krisp microphone</summary>
		public BinarySensorEntity HaileysMacbookAirKrispMicrophone => new(_haContext, "binary_sensor.haileys_macbook_air_krisp_microphone");
		///<summary>Hailey’s MacBook Air MacBook Air Microphone</summary>
		public BinarySensorEntity HaileysMacbookAirMacbookAirMicrophone => new(_haContext, "binary_sensor.haileys_macbook_air_macbook_air_microphone");
		///<summary>Hailey’s MacBook Air Microphone In Use</summary>
		public BinarySensorEntity HaileysMacbookAirMicrophoneInUse => new(_haContext, "binary_sensor.haileys_macbook_air_microphone_in_use");
		///<summary>Hailey’s MacBook Air PLT_Legend</summary>
		public BinarySensorEntity HaileysMacbookAirPltLegend => new(_haContext, "binary_sensor.haileys_macbook_air_plt_legend");
		///<summary>Hailey’s MacBook Air USB PnP Audio Device</summary>
		public BinarySensorEntity HaileysMacbookAirUsbPnpAudioDevice => new(_haContext, "binary_sensor.haileys_macbook_air_usb_pnp_audio_device");
		///<summary>Hallway</summary>
		public BinarySensorEntity Hallway => new(_haContext, "binary_sensor.hallway");
		///<summary>Home Occupied</summary>
		public BinarySensorEntity HomeOccupied => new(_haContext, "binary_sensor.home_occupied");
		///<summary>Jayden Motion</summary>
		public BinarySensorEntity JaydenMotion => new(_haContext, "binary_sensor.jayden_motion");
		///<summary>Jayden Occupancy</summary>
		public BinarySensorEntity JaydenMotionOccupancy => new(_haContext, "binary_sensor.jayden_motion_occupancy");
		///<summary>Johan Front Door Ding</summary>
		public BinarySensorEntity JohanFrontDoorDing => new(_haContext, "binary_sensor.johan_front_door_ding");
		///<summary>Johan Front Door Motion</summary>
		public BinarySensorEntity JohanFrontDoorMotion => new(_haContext, "binary_sensor.johan_front_door_motion");
		///<summary>IKEA of Sweden TRADFRI open/close remote 3dcb2efe on_off</summary>
		public BinarySensorEntity KeTradfriOpenCloseRemote3dcb2efeOnOff => new(_haContext, "binary_sensor.ke_tradfri_open_close_remote_3dcb2efe_on_off");
		///<summary>Kitchen</summary>
		public BinarySensorEntity Kitchen => new(_haContext, "binary_sensor.kitchen");
		///<summary>Kitchen Motion</summary>
		public BinarySensorEntity KitchenMotion => new(_haContext, "binary_sensor.kitchen_motion");
		///<summary>Kitchen Occupancy</summary>
		public BinarySensorEntity KitchenMotionOccupancy => new(_haContext, "binary_sensor.kitchen_motion_occupancy");
		///<summary>Landing</summary>
		public BinarySensorEntity Landing => new(_haContext, "binary_sensor.landing");
		///<summary>Landing Motion</summary>
		public BinarySensorEntity LandingMotion => new(_haContext, "binary_sensor.landing_motion");
		///<summary>landing motion occupancy</summary>
		public BinarySensorEntity LandingMotionOccupancy => new(_haContext, "binary_sensor.landing_motion_occupancy");
		///<summary>Landing Smoke</summary>
		public BinarySensorEntity LandingSmoke => new(_haContext, "binary_sensor.landing_smoke");
		///<summary>Lounge TV</summary>
		public BinarySensorEntity LgTvInUse => new(_haContext, "binary_sensor.lg_tv_in_use");
		///<summary>Lounge</summary>
		public BinarySensorEntity Lounge => new(_haContext, "binary_sensor.lounge");
		///<summary>Lounge Door</summary>
		public BinarySensorEntity LoungeDoor => new(_haContext, "binary_sensor.lounge_door");
		///<summary>Lounge Left Window</summary>
		public BinarySensorEntity LoungeLeftWindow => new(_haContext, "binary_sensor.lounge_left_window");
		///<summary>Lounge Microphone</summary>
		public BinarySensorEntity LoungeMicrophone => new(_haContext, "binary_sensor.lounge_microphone");
		///<summary>Lounge Motion</summary>
		public BinarySensorEntity LoungeMotion => new(_haContext, "binary_sensor.lounge_motion");
		///<summary>LUMI lumi.sensor_motion.aq2 7dce1303 occupancy</summary>
		public BinarySensorEntity LoungeMotionOccupancy => new(_haContext, "binary_sensor.lounge_motion_occupancy");
		///<summary>Lounge Right Window</summary>
		public BinarySensorEntity LoungeRightWindow => new(_haContext, "binary_sensor.lounge_right_window");
		///<summary>LUMI lumi.sensor_magnet.aq2 56141203 on_off</summary>
		public BinarySensorEntity LumiLumiSensorMagnetAq256141203OnOff => new(_haContext, "binary_sensor.lumi_lumi_sensor_magnet_aq2_56141203_on_off");
		///<summary>LUMI lumi.sensor_magnet.aq2 83903a03 on_off</summary>
		public BinarySensorEntity LumiLumiSensorMagnetAq283903a03OnOff => new(_haContext, "binary_sensor.lumi_lumi_sensor_magnet_aq2_83903a03_on_off");
		///<summary>LUMI lumi.sensor_magnet.aq2 8c913a03 on_off</summary>
		public BinarySensorEntity LumiLumiSensorMagnetAq28c913a03OnOff => new(_haContext, "binary_sensor.lumi_lumi_sensor_magnet_aq2_8c913a03_on_off");
		///<summary>Officer Contact  on_off</summary>
		public BinarySensorEntity LumiLumiSensorMagnetAq2OnOff => new(_haContext, "binary_sensor.lumi_lumi_sensor_magnet_aq2_on_off");
		///<summary>Master Motion</summary>
		public BinarySensorEntity MasterMotion => new(_haContext, "binary_sensor.master_motion");
		///<summary>Master Motion Occupancy</summary>
		public BinarySensorEntity MasterMotionOccupancy => new(_haContext, "binary_sensor.master_motion_occupancy");
		///<summary>My Wall Panel AC Plugged</summary>
		public BinarySensorEntity MyWallPanelAcPlugged => new(_haContext, "binary_sensor.my_wall_panel_ac_plugged");
		///<summary>My Wall Panel Charging</summary>
		public BinarySensorEntity MyWallPanelCharging => new(_haContext, "binary_sensor.my_wall_panel_charging");
		///<summary>My Wall Panel Motion Detected</summary>
		public BinarySensorEntity MyWallPanelMotionDetected => new(_haContext, "binary_sensor.my_wall_panel_motion_detected");
		///<summary>My Wall Panel USB Plugged</summary>
		public BinarySensorEntity MyWallPanelUsbPlugged => new(_haContext, "binary_sensor.my_wall_panel_usb_plugged");
		///<summary>Drive</summary>
		public BinarySensorEntity NiemandDriveMotion => new(_haContext, "binary_sensor.niemand_drive_motion");
		///<summary>Niemand Drive Motion</summary>
		public BinarySensorEntity NiemandDriveMotion2 => new(_haContext, "binary_sensor.niemand_drive_motion_2");
		///<summary>Niemand Front Door Ding</summary>
		public BinarySensorEntity NiemandFrontDoorDing => new(_haContext, "binary_sensor.niemand_front_door_ding");
		///<summary>Niemand Front Door Ding</summary>
		public BinarySensorEntity NiemandFrontDoorDing2 => new(_haContext, "binary_sensor.niemand_front_door_ding_2");
		///<summary>Front Door</summary>
		public BinarySensorEntity NiemandFrontDoorMotion => new(_haContext, "binary_sensor.niemand_front_door_motion");
		///<summary>Niemand Front Door Motion</summary>
		public BinarySensorEntity NiemandFrontDoorMotion2 => new(_haContext, "binary_sensor.niemand_front_door_motion_2");
		///<summary>Garage</summary>
		public BinarySensorEntity NiemandGarageMotion => new(_haContext, "binary_sensor.niemand_garage_motion");
		///<summary>Niemand Garage Motion</summary>
		public BinarySensorEntity NiemandGarageMotion2 => new(_haContext, "binary_sensor.niemand_garage_motion_2");
		///<summary>Garden</summary>
		public BinarySensorEntity NiemandGardenMotion => new(_haContext, "binary_sensor.niemand_garden_motion");
		///<summary>Niemand Garden Motion</summary>
		public BinarySensorEntity NiemandGardenMotion2 => new(_haContext, "binary_sensor.niemand_garden_motion_2");
		///<summary>Side</summary>
		public BinarySensorEntity NiemandSideMotion => new(_haContext, "binary_sensor.niemand_side_motion");
		///<summary>Niemand Side Motion</summary>
		public BinarySensorEntity NiemandSideMotion2 => new(_haContext, "binary_sensor.niemand_side_motion_2");
		///<summary>Office Door</summary>
		public BinarySensorEntity OfficeDoor => new(_haContext, "binary_sensor.office_door");
		///<summary>Office Motion</summary>
		public BinarySensorEntity OfficeMotion => new(_haContext, "binary_sensor.office_motion");
		///<summary>Office Motion Occupancy</summary>
		public BinarySensorEntity OfficeMotionOccupancy => new(_haContext, "binary_sensor.office_motion_occupancy");
		///<summary>Pi-Hole Core Update Available</summary>
		public BinarySensorEntity PiHoleCoreUpdateAvailable => new(_haContext, "binary_sensor.pi_hole_core_update_available");
		///<summary>Pi-Hole FTL Update Available</summary>
		public BinarySensorEntity PiHoleFtlUpdateAvailable => new(_haContext, "binary_sensor.pi_hole_ftl_update_available");
		///<summary>Pi-Hole Web Update Available</summary>
		public BinarySensorEntity PiHoleWebUpdateAvailable => new(_haContext, "binary_sensor.pi_hole_web_update_available");
		///<summary>Ping Google</summary>
		public BinarySensorEntity PingGoogle => new(_haContext, "binary_sensor.ping_google");
		///<summary>Ping Konnected Add On</summary>
		public BinarySensorEntity PingKonnectedAddOn => new(_haContext, "binary_sensor.ping_konnected_add_on");
		///<summary>Ping Konnected Main</summary>
		public BinarySensorEntity PingKonnectedMain => new(_haContext, "binary_sensor.ping_konnected_main");
		///<summary>Playroom Motion</summary>
		public BinarySensorEntity PlayroomMotion => new(_haContext, "binary_sensor.playroom_motion");
		///<summary>Playroom Occupancy</summary>
		public BinarySensorEntity PlayroomMotionOccupancy => new(_haContext, "binary_sensor.playroom_motion_occupancy");
		///<summary>Remote UI</summary>
		public BinarySensorEntity RemoteUi => new(_haContext, "binary_sensor.remote_ui");
		///<summary>Study</summary>
		public BinarySensorEntity Study => new(_haContext, "binary_sensor.study");
		///<summary>Toilet Motion</summary>
		public BinarySensorEntity ToiletMotion => new(_haContext, "binary_sensor.toilet_motion");
		///<summary>Toilet Motion Occupancy</summary>
		public BinarySensorEntity ToiletMotionOccupancy => new(_haContext, "binary_sensor.toilet_motion_occupancy");
		///<summary>Toilet Window</summary>
		public BinarySensorEntity ToiletWindow => new(_haContext, "binary_sensor.toilet_window");
		///<summary>Updater</summary>
		public BinarySensorEntity Updater => new(_haContext, "binary_sensor.updater");
		///<summary>Utility Motion</summary>
		public BinarySensorEntity UtilityMotion => new(_haContext, "binary_sensor.utility_motion");
		///<summary>Utility Motion Occupancy</summary>
		public BinarySensorEntity UtilityMotionOccupancy => new(_haContext, "binary_sensor.utility_motion_occupancy");
	}

	public class ButtonEntities
	{
		private readonly IHaContext _haContext;
		public ButtonEntities(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>IKEA of Sweden FYRTUR block-out roller blind 29529dfe identify</summary>
		public ButtonEntity IkeaOfSwedenFyrturBlockOutRollerBlind29529dfeIdentify => new(_haContext, "button.ikea_of_sweden_fyrtur_block_out_roller_blind_29529dfe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb E27 W opal 1000lm 9919fcfe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbE27WOpal1000lm9919fcfeIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_e27_w_opal_1000lm_9919fcfe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb E27 W opal 1000lm b4b1f3fe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbE27WOpal1000lmB4b1f3feIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_e27_w_opal_1000lm_b4b1f3fe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb E27 WS opal 1000lm 7aba12fe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbE27WsOpal1000lm7aba12feIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_e27_ws_opal_1000lm_7aba12fe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb E27 WS opal 1000lm 9f6c01fe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbE27WsOpal1000lm9f6c01feIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_e27_ws_opal_1000lm_9f6c01fe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb E27 WS opal 1000lm b87713fe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbE27WsOpal1000lmB87713feIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_e27_ws_opal_1000lm_b87713fe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb E27 WS opal 1000lm c8a903fe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbE27WsOpal1000lmC8a903feIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_e27_ws_opal_1000lm_c8a903fe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb GU10 WS 400lm 062b4bfe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbGu10Ws400lm062b4bfeIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_gu10_ws_400lm_062b4bfe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb GU10 WS 400lm 145c4bfe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbGu10Ws400lm145c4bfeIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_gu10_ws_400lm_145c4bfe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb GU10 WS 400lm 1e5368fe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbGu10Ws400lm1e5368feIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_gu10_ws_400lm_1e5368fe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb GU10 WS 400lm 21daf6fe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbGu10Ws400lm21daf6feIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_gu10_ws_400lm_21daf6fe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb GU10 WS 400lm 235a5dfe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbGu10Ws400lm235a5dfeIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_gu10_ws_400lm_235a5dfe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb GU10 WS 400lm 26f462fe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbGu10Ws400lm26f462feIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_gu10_ws_400lm_26f462fe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb GU10 WS 400lm 31244bfe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbGu10Ws400lm31244bfeIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_gu10_ws_400lm_31244bfe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb GU10 WS 400lm 341922fe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbGu10Ws400lm341922feIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_gu10_ws_400lm_341922fe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb GU10 WS 400lm 502e4cfe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbGu10Ws400lm502e4cfeIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_gu10_ws_400lm_502e4cfe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb GU10 WS 400lm 5c78f6fe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbGu10Ws400lm5c78f6feIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_gu10_ws_400lm_5c78f6fe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb GU10 WS 400lm 5ec17bfe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbGu10Ws400lm5ec17bfeIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_gu10_ws_400lm_5ec17bfe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb GU10 WS 400lm 8bb43dfe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbGu10Ws400lm8bb43dfeIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_gu10_ws_400lm_8bb43dfe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb GU10 WS 400lm 937af8fe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbGu10Ws400lm937af8feIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_gu10_ws_400lm_937af8fe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb GU10 WS 400lm bb0a4cfe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbGu10Ws400lmBb0a4cfeIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_gu10_ws_400lm_bb0a4cfe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb GU10 WS 400lm cdee3efe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbGu10Ws400lmCdee3efeIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_gu10_ws_400lm_cdee3efe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb GU10 WS 400lm f8ea62fe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbGu10Ws400lmF8ea62feIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_gu10_ws_400lm_f8ea62fe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb GU10 WW 400lm 00caeefe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbGu10Ww400lm00caeefeIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_gu10_ww_400lm_00caeefe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb GU10 WW 400lm 0fc075fe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbGu10Ww400lm0fc075feIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_gu10_ww_400lm_0fc075fe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb GU10 WW 400lm 105c5dfe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbGu10Ww400lm105c5dfeIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_gu10_ww_400lm_105c5dfe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb GU10 WW 400lm 22331afe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbGu10Ww400lm22331afeIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_gu10_ww_400lm_22331afe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb GU10 WW 400lm 2d9a33fe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbGu10Ww400lm2d9a33feIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_gu10_ww_400lm_2d9a33fe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb GU10 WW 400lm 32d2bdfe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbGu10Ww400lm32d2bdfeIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_gu10_ww_400lm_32d2bdfe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb GU10 WW 400lm 436af0fe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbGu10Ww400lm436af0feIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_gu10_ww_400lm_436af0fe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb GU10 WW 400lm 50c275fe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbGu10Ww400lm50c275feIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_gu10_ww_400lm_50c275fe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb GU10 WW 400lm 680f1cfe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbGu10Ww400lm680f1cfeIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_gu10_ww_400lm_680f1cfe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb GU10 WW 400lm 7d61cbfe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbGu10Ww400lm7d61cbfeIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_gu10_ww_400lm_7d61cbfe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb GU10 WW 400lm 85b375fe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbGu10Ww400lm85b375feIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_gu10_ww_400lm_85b375fe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb GU10 WW 400lm 8ba6b5fe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbGu10Ww400lm8ba6b5feIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_gu10_ww_400lm_8ba6b5fe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb GU10 WW 400lm b0683bfe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbGu10Ww400lmB0683bfeIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_gu10_ww_400lm_b0683bfe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb GU10 WW 400lm b86ff0fe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbGu10Ww400lmB86ff0feIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_gu10_ww_400lm_b86ff0fe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb GU10 WW 400lm cf4febfe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbGu10Ww400lmCf4febfeIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_gu10_ww_400lm_cf4febfe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb GU10 WW 400lm d4fdf0fe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbGu10Ww400lmD4fdf0feIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_gu10_ww_400lm_d4fdf0fe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb GU10 WW 400lm f58cf0fe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbGu10Ww400lmF58cf0feIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_gu10_ww_400lm_f58cf0fe_identify");
		///<summary>IKEA of Sweden TRADFRI bulb GU10 WW 400lm fd78f0fe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriBulbGu10Ww400lmFd78f0feIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_bulb_gu10_ww_400lm_fd78f0fe_identify");
		///<summary>IKEA of Sweden TRADFRI open/close remote 3dcb2efe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriOpenCloseRemote3dcb2efeIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_open_close_remote_3dcb2efe_identify");
		///<summary>IKEA of Sweden TRADFRI remote control 580e51fe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriRemoteControl580e51feIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_remote_control_580e51fe_identify");
		///<summary>IKEA of Sweden TRADFRI remote control d73648fe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfriRemoteControlD73648feIdentify => new(_haContext, "button.ikea_of_sweden_tradfri_remote_control_d73648fe_identify");
		///<summary>IKEA of Sweden TRADFRIbulbG125E27WSopal470lm 9b7e6bfe identify</summary>
		public ButtonEntity IkeaOfSwedenTradfribulbg125e27wsopal470lm9b7e6bfeIdentify => new(_haContext, "button.ikea_of_sweden_tradfribulbg125e27wsopal470lm_9b7e6bfe_identify");
		///<summary>LUMI lumi.sensor_magnet.aq2 38f0ec02 identify</summary>
		public ButtonEntity LumiLumiSensorMagnetAq238f0ec02Identify => new(_haContext, "button.lumi_lumi_sensor_magnet_aq2_38f0ec02_identify");
		///<summary>LUMI lumi.sensor_magnet.aq2 56141203 identify</summary>
		public ButtonEntity LumiLumiSensorMagnetAq256141203Identify => new(_haContext, "button.lumi_lumi_sensor_magnet_aq2_56141203_identify");
		///<summary>LUMI lumi.sensor_magnet.aq2 83903a03 identify</summary>
		public ButtonEntity LumiLumiSensorMagnetAq283903a03Identify => new(_haContext, "button.lumi_lumi_sensor_magnet_aq2_83903a03_identify");
		///<summary>LUMI lumi.sensor_magnet.aq2 9e0b1203 identify</summary>
		public ButtonEntity LumiLumiSensorMagnetAq29e0b1203Identify => new(_haContext, "button.lumi_lumi_sensor_magnet_aq2_9e0b1203_identify");
		///<summary>LUMI lumi.sensor_magnet.aq2 ac831303 identify</summary>
		public ButtonEntity LumiLumiSensorMagnetAq2Ac831303Identify => new(_haContext, "button.lumi_lumi_sensor_magnet_aq2_ac831303_identify");
		///<summary>LUMI lumi.sensor_magnet.aq2 e6b02103 identify</summary>
		public ButtonEntity LumiLumiSensorMagnetAq2E6b02103Identify => new(_haContext, "button.lumi_lumi_sensor_magnet_aq2_e6b02103_identify");
		///<summary>LUMI lumi.sensor_motion.aq2 34796603 identify</summary>
		public ButtonEntity LumiLumiSensorMotionAq234796603Identify => new(_haContext, "button.lumi_lumi_sensor_motion_aq2_34796603_identify");
		///<summary>LUMI lumi.sensor_motion.aq2 3ca2f202 identify</summary>
		public ButtonEntity LumiLumiSensorMotionAq23ca2f202Identify => new(_haContext, "button.lumi_lumi_sensor_motion_aq2_3ca2f202_identify");
		///<summary>LUMI lumi.sensor_motion.aq2 4123f403 identify</summary>
		public ButtonEntity LumiLumiSensorMotionAq24123f403Identify => new(_haContext, "button.lumi_lumi_sensor_motion_aq2_4123f403_identify");
		///<summary>LUMI lumi.sensor_motion.aq2 54c2f302 identify</summary>
		public ButtonEntity LumiLumiSensorMotionAq254c2f302Identify => new(_haContext, "button.lumi_lumi_sensor_motion_aq2_54c2f302_identify");
		///<summary>LUMI lumi.sensor_motion.aq2 591d1b03 identify</summary>
		public ButtonEntity LumiLumiSensorMotionAq2591d1b03Identify => new(_haContext, "button.lumi_lumi_sensor_motion_aq2_591d1b03_identify");
		///<summary>LUMI lumi.sensor_motion.aq2 5cf75702 identify</summary>
		public ButtonEntity LumiLumiSensorMotionAq25cf75702Identify => new(_haContext, "button.lumi_lumi_sensor_motion_aq2_5cf75702_identify");
		///<summary>LUMI lumi.sensor_motion.aq2 7dce1303 identify</summary>
		public ButtonEntity LumiLumiSensorMotionAq27dce1303Identify => new(_haContext, "button.lumi_lumi_sensor_motion_aq2_7dce1303_identify");
		///<summary>LUMI lumi.sensor_motion.aq2 97a7f202 identify</summary>
		public ButtonEntity LumiLumiSensorMotionAq297a7f202Identify => new(_haContext, "button.lumi_lumi_sensor_motion_aq2_97a7f202_identify");
		///<summary>LUMI lumi.sensor_motion.aq2 b4796603 identify</summary>
		public ButtonEntity LumiLumiSensorMotionAq2B4796603Identify => new(_haContext, "button.lumi_lumi_sensor_motion_aq2_b4796603_identify");
		///<summary>LUMI lumi.sensor_motion.aq2 c0a6f202 identify</summary>
		public ButtonEntity LumiLumiSensorMotionAq2C0a6f202Identify => new(_haContext, "button.lumi_lumi_sensor_motion_aq2_c0a6f202_identify");
		///<summary>LUMI lumi.sensor_motion.aq2 ea1a1404 identify</summary>
		public ButtonEntity LumiLumiSensorMotionAq2Ea1a1404Identify => new(_haContext, "button.lumi_lumi_sensor_motion_aq2_ea1a1404_identify");
		///<summary>LUMI lumi.sensor_motion.aq2 ef2f1404 identify</summary>
		public ButtonEntity LumiLumiSensorMotionAq2Ef2f1404Identify => new(_haContext, "button.lumi_lumi_sensor_motion_aq2_ef2f1404_identify");
		///<summary>LUMI lumi.sensor_motion.aq2 f33b1404 identify</summary>
		public ButtonEntity LumiLumiSensorMotionAq2F33b1404Identify => new(_haContext, "button.lumi_lumi_sensor_motion_aq2_f33b1404_identify");
		///<summary>shellyswitch25-E5A1D2 OTA Update</summary>
		public ButtonEntity Shellyswitch25E5a1d2OtaUpdate => new(_haContext, "button.shellyswitch25_e5a1d2_ota_update");
		///<summary>shellyswitch25-E5A1D2 Reboot</summary>
		public ButtonEntity Shellyswitch25E5a1d2Reboot => new(_haContext, "button.shellyswitch25_e5a1d2_reboot");
	}

	public class CalendarEntities
	{
		private readonly IHaContext _haContext;
		public CalendarEntities(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Home</summary>
		public CalendarEntity Home => new(_haContext, "calendar.home");
	}

	public class CameraEntities
	{
		private readonly IHaContext _haContext;
		public CameraEntities(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Aubrecia Drive</summary>
		public CameraEntity AubreciaDrive => new(_haContext, "camera.aubrecia_drive");
		///<summary>Aubrecia Front Door</summary>
		public CameraEntity AubreciaFrontDoor => new(_haContext, "camera.aubrecia_front_door");
		///<summary>EUGENE-DESKTOP Screen #0</summary>
		public CameraEntity EugeneDesktopScreen0 => new(_haContext, "camera.eugene_desktop_screen_0");
		///<summary>Garden</summary>
		public CameraEntity Garden2 => new(_haContext, "camera.garden_2");
		///<summary>Johan Front Door</summary>
		public CameraEntity JohanFrontDoor => new(_haContext, "camera.johan_front_door");
		///<summary>Niemand Drive</summary>
		public CameraEntity NiemandDrive => new(_haContext, "camera.niemand_drive");
		///<summary>Niemand Drive Snapshot</summary>
		public CameraEntity NiemandDriveSnapshot => new(_haContext, "camera.niemand_drive_snapshot");
		///<summary>Niemand Front Door</summary>
		public CameraEntity NiemandFrontDoor => new(_haContext, "camera.niemand_front_door");
		///<summary>Niemand Front Door Snapshot</summary>
		public CameraEntity NiemandFrontDoorSnapshot => new(_haContext, "camera.niemand_front_door_snapshot");
		///<summary>Niemand Garage</summary>
		public CameraEntity NiemandGarage => new(_haContext, "camera.niemand_garage");
		///<summary>Niemand Garage Snapshot</summary>
		public CameraEntity NiemandGarageSnapshot => new(_haContext, "camera.niemand_garage_snapshot");
		///<summary>Niemand Garden</summary>
		public CameraEntity NiemandGarden => new(_haContext, "camera.niemand_garden");
		///<summary>Niemand Garden Snapshot</summary>
		public CameraEntity NiemandGardenSnapshot => new(_haContext, "camera.niemand_garden_snapshot");
		///<summary>Niemand Side</summary>
		public CameraEntity NiemandSide => new(_haContext, "camera.niemand_side");
		///<summary>Niemand Side Snapshot</summary>
		public CameraEntity NiemandSideSnapshot => new(_haContext, "camera.niemand_side_snapshot");
		///<summary>Ring Drive Video</summary>
		public CameraEntity RingDriveVideo => new(_haContext, "camera.ring_drive_video");
		///<summary>Ring Front Door Video</summary>
		public CameraEntity RingFrontDoorVideo => new(_haContext, "camera.ring_front_door_video");
		///<summary>Ring Garage Video</summary>
		public CameraEntity RingGarageVideo => new(_haContext, "camera.ring_garage_video");
		///<summary>Ring Garden Video</summary>
		public CameraEntity RingGardenVideo => new(_haContext, "camera.ring_garden_video");
		///<summary>Ring Side Video</summary>
		public CameraEntity RingSideVideo => new(_haContext, "camera.ring_side_video");
	}

	public class ClimateEntities
	{
		private readonly IHaContext _haContext;
		public ClimateEntities(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Playroom AC</summary>
		public ClimateEntity Bedroom1 => new(_haContext, "climate.bedroom_1");
		///<summary>Aaron AC</summary>
		public ClimateEntity Bedroom2 => new(_haContext, "climate.bedroom_2");
		///<summary>Master AC</summary>
		public ClimateEntity Bedroom3 => new(_haContext, "climate.bedroom_3");
		///<summary>Jayden AC</summary>
		public ClimateEntity Bedroom4 => new(_haContext, "climate.bedroom_4");
		///<summary>Lounge AC</summary>
		public ClimateEntity Lounge => new(_haContext, "climate.lounge");
		///<summary>Office</summary>
		public ClimateEntity Office => new(_haContext, "climate.office");
		///<summary>Wiser Boys</summary>
		public ClimateEntity WiserBoys => new(_haContext, "climate.wiser_boys");
		///<summary>Wiser Dining</summary>
		public ClimateEntity WiserDining => new(_haContext, "climate.wiser_dining");
		///<summary>Wiser Entrance</summary>
		public ClimateEntity WiserEntrance => new(_haContext, "climate.wiser_entrance");
		///<summary>Wiser Guest Room</summary>
		public ClimateEntity WiserGuestRoom => new(_haContext, "climate.wiser_guest_room");
		///<summary>Wiser Landing</summary>
		public ClimateEntity WiserLanding => new(_haContext, "climate.wiser_landing");
		///<summary>Wiser Lounge </summary>
		public ClimateEntity WiserLounge => new(_haContext, "climate.wiser_lounge");
		///<summary>Wiser Lounge Bay</summary>
		public ClimateEntity WiserLoungeBay => new(_haContext, "climate.wiser_lounge_bay");
		///<summary>Wiser Master</summary>
		public ClimateEntity WiserMaster => new(_haContext, "climate.wiser_master");
		///<summary>Wiser Office</summary>
		public ClimateEntity WiserOffice => new(_haContext, "climate.wiser_office");
		///<summary>Wiser Playroom</summary>
		public ClimateEntity WiserPlayroom => new(_haContext, "climate.wiser_playroom");
		///<summary>Wiser Utility</summary>
		public ClimateEntity WiserUtility => new(_haContext, "climate.wiser_utility");
	}

	public class CoverEntities
	{
		private readonly IHaContext _haContext;
		public CoverEntities(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Blind Lounge</summary>
		public CoverEntity BlindLounge => new(_haContext, "cover.blind_lounge");
		///<summary>landing blind</summary>
		public CoverEntity LandingBlind => new(_haContext, "cover.landing_blind");
	}

	public class DeviceTrackerEntities
	{
		private readonly IHaContext _haContext;
		public DeviceTrackerEntities(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>247D4D7D6C90-mysimplelink</summary>
		public DeviceTrackerEntity E247d4d7d6c90Mysimplelink => new(_haContext, "device_tracker.247d4d7d6c90_mysimplelink");
		///<summary>Aaron Echo</summary>
		public DeviceTrackerEntity AaronEcho => new(_haContext, "device_tracker.aaron_echo");
		///<summary>Treadmill</summary>
		public DeviceTrackerEntity AndroidB8c33f1cb7c0d776 => new(_haContext, "device_tracker.android_b8c33f1cb7c0d776");
		///<summary>ASGLH-WL-19140</summary>
		public DeviceTrackerEntity AsglhWl19140 => new(_haContext, "device_tracker.asglh_wl_19140");
		///<summary>Aubrecia</summary>
		public DeviceTrackerEntity Aubrecia => new(_haContext, "device_tracker.aubrecia");
		///<summary>AubreciasiPhone</summary>
		public DeviceTrackerEntity Aubreciasiphone => new(_haContext, "device_tracker.aubreciasiphone");
		///<summary>Audi Q7 Position</summary>
		public DeviceTrackerEntity AudiQ7Position => new(_haContext, "device_tracker.audi_q7_position");
		///<summary>Bedroom 1 AC</summary>
		public DeviceTrackerEntity Bedroom1Ac => new(_haContext, "device_tracker.bedroom_1_ac");
		///<summary>Bedroom 2 AC</summary>
		public DeviceTrackerEntity Bedroom2Ac => new(_haContext, "device_tracker.bedroom_2_ac");
		///<summary>Bedroom 3 AC</summary>
		public DeviceTrackerEntity Bedroom3Ac => new(_haContext, "device_tracker.bedroom_3_ac");
		///<summary>Bedroom 4 AC</summary>
		public DeviceTrackerEntity Bedroom4Ac => new(_haContext, "device_tracker.bedroom_4_ac");
		///<summary>christmas_indoor-1558</summary>
		public DeviceTrackerEntity ChristmasIndoor1558 => new(_haContext, "device_tracker.christmas_indoor_1558");
		///<summary>ASAZ-5CG127498B</summary>
		public DeviceTrackerEntity DesktopIpurn8t => new(_haContext, "device_tracker.desktop_ipurn8t");
		///<summary>Dining</summary>
		public DeviceTrackerEntity Dining => new(_haContext, "device_tracker.dining");
		///<summary>Dining Echo</summary>
		public DeviceTrackerEntity DiningEcho => new(_haContext, "device_tracker.dining_echo");
		///<summary>dryer</summary>
		public DeviceTrackerEntity Dryer => new(_haContext, "device_tracker.dryer");
		///<summary>Entrance</summary>
		public DeviceTrackerEntity Entrance => new(_haContext, "device_tracker.entrance");
		///<summary>ESP_6B7081</summary>
		public DeviceTrackerEntity Esp6b7081 => new(_haContext, "device_tracker.esp_6b7081");
		///<summary>ESP_6B7A3A</summary>
		public DeviceTrackerEntity Esp6b7a3a => new(_haContext, "device_tracker.esp_6b7a3a");
		///<summary>eufy RoboVac</summary>
		public DeviceTrackerEntity EufyRobovac => new(_haContext, "device_tracker.eufy_robovac");
		///<summary>eufy RoboVac</summary>
		public DeviceTrackerEntity EufyRobovac2 => new(_haContext, "device_tracker.eufy_robovac_2");
		///<summary>EUGENE-DESKTOP</summary>
		public DeviceTrackerEntity EugeneDesktop => new(_haContext, "device_tracker.eugene_desktop");
		///<summary>eugene_iphone_ip</summary>
		public DeviceTrackerEntity EugeneIphoneIp => new(_haContext, "device_tracker.eugene_iphone_ip");
		///<summary>Eugene’s iPhone</summary>
		public DeviceTrackerEntity EugenesIphone => new(_haContext, "device_tracker.eugenes_iphone");
		///<summary>Eugenes-iPhone</summary>
		public DeviceTrackerEntity EugenesIphone2 => new(_haContext, "device_tracker.eugenes_iphone_2");
		///<summary>EugenespleWatch</summary>
		public DeviceTrackerEntity Eugenesplewatch => new(_haContext, "device_tracker.eugenesplewatch");
		///<summary>floor_light-2086</summary>
		public DeviceTrackerEntity FloorLight2086 => new(_haContext, "device_tracker.floor_light_2086");
		///<summary>Foscam</summary>
		public DeviceTrackerEntity Foscam => new(_haContext, "device_tracker.foscam");
		///<summary>Galaxy-S8</summary>
		public DeviceTrackerEntity GalaxyS8 => new(_haContext, "device_tracker.galaxy_s8");
		///<summary>Garage Echo</summary>
		public DeviceTrackerEntity GarageEcho => new(_haContext, "device_tracker.garage_echo");
		///<summary>Garden Floodlights</summary>
		public DeviceTrackerEntity GardenFloodlights => new(_haContext, "device_tracker.garden_floodlights");
		///<summary>hailey_iphone_ip</summary>
		public DeviceTrackerEntity HaileyIphoneIp => new(_haContext, "device_tracker.hailey_iphone_ip");
		///<summary>Hailey's iPhone</summary>
		public DeviceTrackerEntity HaileySIphone => new(_haContext, "device_tracker.hailey_s_iphone");
		///<summary>Haileys-Air</summary>
		public DeviceTrackerEntity HaileysAir => new(_haContext, "device_tracker.haileys_air");
		///<summary>Haileys-iPhone</summary>
		public DeviceTrackerEntity HaileysIphone => new(_haContext, "device_tracker.haileys_iphone");
		///<summary>Haileys-iPhone</summary>
		public DeviceTrackerEntity HaileysIphone2 => new(_haContext, "device_tracker.haileys_iphone_2");
		///<summary>Hailey’s MacBook Air</summary>
		public DeviceTrackerEntity HaileysMacbookAir => new(_haContext, "device_tracker.haileys_macbook_air");
		///<summary>host_two</summary>
		public DeviceTrackerEntity HostTwo => new(_haContext, "device_tracker.host_two");
		///<summary>HUAWEI_P_smart_2019-86203</summary>
		public DeviceTrackerEntity HuaweiPSmart201986203 => new(_haContext, "device_tracker.huawei_p_smart_2019_86203");
		///<summary>iPad</summary>
		public DeviceTrackerEntity Ipad => new(_haContext, "device_tracker.ipad");
		///<summary>Jayden</summary>
		public DeviceTrackerEntity Jayden => new(_haContext, "device_tracker.jayden");
		///<summary>Jayden AppleTv</summary>
		public DeviceTrackerEntity JaydenAppletv => new(_haContext, "device_tracker.jayden_appletv");
		///<summary>jayden_bedside-4734</summary>
		public DeviceTrackerEntity JaydenBedside4734 => new(_haContext, "device_tracker.jayden_bedside_4734");
		///<summary>Jayden Echo</summary>
		public DeviceTrackerEntity JaydenEcho => new(_haContext, "device_tracker.jayden_echo");
		///<summary>Kitchen</summary>
		public DeviceTrackerEntity Kitchen => new(_haContext, "device_tracker.kitchen");
		///<summary>Kitchen Echo</summary>
		public DeviceTrackerEntity KitchenEcho => new(_haContext, "device_tracker.kitchen_echo");
		///<summary>Konnected AddOn</summary>
		public DeviceTrackerEntity KonnectedAddon => new(_haContext, "device_tracker.konnected_addon");
		///<summary>Konnected Main</summary>
		public DeviceTrackerEntity KonnectedMain => new(_haContext, "device_tracker.konnected_main");
		///<summary>Landing</summary>
		public DeviceTrackerEntity Landing => new(_haContext, "device_tracker.landing");
		///<summary>LG Lounge</summary>
		public DeviceTrackerEntity LgLounge => new(_haContext, "device_tracker.lg_lounge");
		///<summary>Living-Room</summary>
		public DeviceTrackerEntity LivingRoom => new(_haContext, "device_tracker.living_room");
		///<summary>Lounge</summary>
		public DeviceTrackerEntity Lounge => new(_haContext, "device_tracker.lounge");
		///<summary>Lounge AC</summary>
		public DeviceTrackerEntity LoungeAc => new(_haContext, "device_tracker.lounge_ac");
		///<summary>Lounge Blind</summary>
		public DeviceTrackerEntity LoungeBlind => new(_haContext, "device_tracker.lounge_blind");
		///<summary>Lounge Echo</summary>
		public DeviceTrackerEntity LoungeEcho => new(_haContext, "device_tracker.lounge_echo");
		///<summary>Master Echo</summary>
		public DeviceTrackerEntity MasterEcho => new(_haContext, "device_tracker.master_echo");
		///<summary>Master Tele</summary>
		public DeviceTrackerEntity MasterTele => new(_haContext, "device_tracker.master_tele");
		///<summary>Office AC</summary>
		public DeviceTrackerEntity OfficeAc => new(_haContext, "device_tracker.office_ac");
		///<summary>Office Echo</summary>
		public DeviceTrackerEntity OfficeEcho => new(_haContext, "device_tracker.office_echo");
		///<summary>Outside Drive</summary>
		public DeviceTrackerEntity OutsideDrive => new(_haContext, "device_tracker.outside_drive");
		///<summary>Outside Garage</summary>
		public DeviceTrackerEntity OutsideGarage => new(_haContext, "device_tracker.outside_garage");
		///<summary>Playroom Echo</summary>
		public DeviceTrackerEntity PlayroomEcho => new(_haContext, "device_tracker.playroom_echo");
		///<summary>Porch</summary>
		public DeviceTrackerEntity Porch => new(_haContext, "device_tracker.porch");
		///<summary>raspberrypi</summary>
		public DeviceTrackerEntity Raspberrypi => new(_haContext, "device_tracker.raspberrypi");
		///<summary>RaspberryPi CUPS</summary>
		public DeviceTrackerEntity RaspberrypiCups => new(_haContext, "device_tracker.raspberrypi_cups");
		///<summary>RingHpCam-49</summary>
		public DeviceTrackerEntity Ringhpcam49 => new(_haContext, "device_tracker.ringhpcam_49");
		///<summary>RingHpCam-4c</summary>
		public DeviceTrackerEntity Ringhpcam4c => new(_haContext, "device_tracker.ringhpcam_4c");
		///<summary>RingPro-d6</summary>
		public DeviceTrackerEntity RingproD6 => new(_haContext, "device_tracker.ringpro_d6");
		///<summary>RingStickUpCam-94</summary>
		public DeviceTrackerEntity Ringstickupcam94 => new(_haContext, "device_tracker.ringstickupcam_94");
		///<summary>RingStickUpCam-9b</summary>
		public DeviceTrackerEntity Ringstickupcam9b => new(_haContext, "device_tracker.ringstickupcam_9b");
		///<summary>RMMINI-d9-2b-62</summary>
		public DeviceTrackerEntity RmminiD92b62 => new(_haContext, "device_tracker.rmmini_d9_2b_62");
		///<summary>Sammi-Leigh-s-A52</summary>
		public DeviceTrackerEntity SammiLeighSA52 => new(_haContext, "device_tracker.sammi_leigh_s_a52");
		///<summary>shelly1-55E8B5</summary>
		public DeviceTrackerEntity Shelly155e8b5 => new(_haContext, "device_tracker.shelly1_55e8b5");
		///<summary>shelly1-BA6C98</summary>
		public DeviceTrackerEntity Shelly1Ba6c98 => new(_haContext, "device_tracker.shelly1_ba6c98");
		///<summary>smart-plug-1</summary>
		public DeviceTrackerEntity SmartPlug1 => new(_haContext, "device_tracker.smart_plug_1");
		///<summary>smart-plug-2</summary>
		public DeviceTrackerEntity SmartPlug2 => new(_haContext, "device_tracker.smart_plug_2");
		///<summary>smart-plug-4</summary>
		public DeviceTrackerEntity SmartPlug4 => new(_haContext, "device_tracker.smart_plug_4");
		///<summary>SonosZP</summary>
		public DeviceTrackerEntity Sonoszp => new(_haContext, "device_tracker.sonoszp");
		///<summary>SonosZP</summary>
		public DeviceTrackerEntity Sonoszp2 => new(_haContext, "device_tracker.sonoszp_2");
		///<summary>Suspect Device</summary>
		public DeviceTrackerEntity SuspectDevice => new(_haContext, "device_tracker.suspect_device");
		///<summary>Suspect Huawei</summary>
		public DeviceTrackerEntity SuspectHuawei => new(_haContext, "device_tracker.suspect_huawei");
		public DeviceTrackerEntity Unifi0a1eCf173cBfDefault => new(_haContext, "device_tracker.unifi_0a_1e_cf_17_3c_bf_default");
		public DeviceTrackerEntity Unifi0aEa8bDe2e46Default => new(_haContext, "device_tracker.unifi_0a_ea_8b_de_2e_46_default");
		public DeviceTrackerEntity Unifi22666f3b995fDefault => new(_haContext, "device_tracker.unifi_22_66_6f_3b_99_5f_default");
		public DeviceTrackerEntity Unifi2668Db030cBeDefault => new(_haContext, "device_tracker.unifi_26_68_db_03_0c_be_default");
		public DeviceTrackerEntity Unifi26Fd74Eb3d99Default => new(_haContext, "device_tracker.unifi_26_fd_74_eb_3d_99_default");
		public DeviceTrackerEntity Unifi2aD7E18c21A6Default => new(_haContext, "device_tracker.unifi_2a_d7_e1_8c_21_a6_default");
		///<summary>SonosZP</summary>
		public DeviceTrackerEntity Unifi347e5cD68b20Default => new(_haContext, "device_tracker.unifi_34_7e_5c_d6_8b_20_default");
		public DeviceTrackerEntity Unifi3a8980817eCeDefault => new(_haContext, "device_tracker.unifi_3a_89_80_81_7e_ce_default");
		///<summary>Jayden iPad</summary>
		public DeviceTrackerEntity Unifi54Ae270e3732Default => new(_haContext, "device_tracker.unifi_54_ae_27_0e_37_32_default");
		public DeviceTrackerEntity Unifi5a0c5eBc5aE3Default => new(_haContext, "device_tracker.unifi_5a_0c_5e_bc_5a_e3_default");
		public DeviceTrackerEntity Unifi625c7c18D2BfDefault => new(_haContext, "device_tracker.unifi_62_5c_7c_18_d2_bf_default");
		public DeviceTrackerEntity Unifi6a7dC2219651Default => new(_haContext, "device_tracker.unifi_6a_7d_c2_21_96_51_default");
		public DeviceTrackerEntity Unifi7aB7F6160d9cDefault => new(_haContext, "device_tracker.unifi_7a_b7_f6_16_0d_9c_default");
		public DeviceTrackerEntity Unifi8603C83f3f3aDefault => new(_haContext, "device_tracker.unifi_86_03_c8_3f_3f_3a_default");
		public DeviceTrackerEntity Unifi8a513f0f9323Default => new(_haContext, "device_tracker.unifi_8a_51_3f_0f_93_23_default");
		public DeviceTrackerEntity Unifi92A5C740B6F0Default => new(_haContext, "device_tracker.unifi_92_a5_c7_40_b6_f0_default");
		public DeviceTrackerEntity UnifiA8E3EeDdD898Default => new(_haContext, "device_tracker.unifi_a8_e3_ee_dd_d8_98_default");
		public DeviceTrackerEntity UnifiBa1d1e7c94C8Default => new(_haContext, "device_tracker.unifi_ba_1d_1e_7c_94_c8_default");
		public DeviceTrackerEntity UnifiBa7aDe6312CbDefault => new(_haContext, "device_tracker.unifi_ba_7a_de_63_12_cb_default");
		public DeviceTrackerEntity UnifiBeA71b06283cDefault => new(_haContext, "device_tracker.unifi_be_a7_1b_06_28_3c_default");
		public DeviceTrackerEntity UnifiC24cDdA852FeDefault => new(_haContext, "device_tracker.unifi_c2_4c_dd_a8_52_fe_default");
		public DeviceTrackerEntity UnifiC25413325fF5Default => new(_haContext, "device_tracker.unifi_c2_54_13_32_5f_f5_default");
		///<summary>Jayden RaspberryPi</summary>
		public DeviceTrackerEntity UnifiDcA632Dc56AfDefault => new(_haContext, "device_tracker.unifi_dc_a6_32_dc_56_af_default");
		public DeviceTrackerEntity UnifiE223Bf9c8221Default => new(_haContext, "device_tracker.unifi_e2_23_bf_9c_82_21_default");
		public DeviceTrackerEntity UnifiEa7f17B856D1Default => new(_haContext, "device_tracker.unifi_ea_7f_17_b8_56_d1_default");
		public DeviceTrackerEntity UnifiFa0005393fDfDefault => new(_haContext, "device_tracker.unifi_fa_00_05_39_3f_df_default");
		public DeviceTrackerEntity UnifiFa00D75740FcDefault => new(_haContext, "device_tracker.unifi_fa_00_d7_57_40_fc_default");
		public DeviceTrackerEntity UnifiFe5a3957E388Default => new(_haContext, "device_tracker.unifi_fe_5a_39_57_e3_88_default");
		///<summary>Upstairs</summary>
		public DeviceTrackerEntity Upstairs => new(_haContext, "device_tracker.upstairs");
		///<summary>Wallpanel Fire HD8</summary>
		public DeviceTrackerEntity WallpanelFireHd8 => new(_haContext, "device_tracker.wallpanel_fire_hd8");
		///<summary>washer</summary>
		public DeviceTrackerEntity Washer => new(_haContext, "device_tracker.washer");
		///<summary>WiserHeat031C5E</summary>
		public DeviceTrackerEntity Wiserheat031c5e => new(_haContext, "device_tracker.wiserheat031c5e");
	}

	public class GroupEntities
	{
		private readonly IHaContext _haContext;
		public GroupEntities(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Christmas</summary>
		public GroupEntity Christmas => new(_haContext, "group.christmas");
	}

	public class InputBooleanEntities
	{
		private readonly IHaContext _haContext;
		public InputBooleanEntities(IHaContext haContext)
		{
			_haContext = haContext;
		}

		public InputBooleanEntity AlaramPanic => new(_haContext, "input_boolean.alaram_panic");
		public InputBooleanEntity BackDoorChecked => new(_haContext, "input_boolean.back_door_checked");
		///<summary>dev_netdaemon_light_manager_v2_lights_manager</summary>
		public InputBooleanEntity DevNetdaemonLightManagerV2LightsManager => new(_haContext, "input_boolean.dev_netdaemon_light_manager_v2_lights_manager");
		///<summary>dev_netdaemon_niemand_discipline_manager</summary>
		public InputBooleanEntity DevNetdaemonNiemandDisciplineManager => new(_haContext, "input_boolean.dev_netdaemon_niemand_discipline_manager");
		public InputBooleanEntity DiningDoorChecked => new(_haContext, "input_boolean.dining_door_checked");
		public InputBooleanEntity DryerAck => new(_haContext, "input_boolean.dryer_ack");
		public InputBooleanEntity FrontDoorChecked => new(_haContext, "input_boolean.front_door_checked");
		public InputBooleanEntity GarageBackDoorChecked => new(_haContext, "input_boolean.garage_back_door_checked");
		public InputBooleanEntity KidsInBed => new(_haContext, "input_boolean.kids_in_bed");
		public InputBooleanEntity LoungeDoorChecked => new(_haContext, "input_boolean.lounge_door_checked");
		public InputBooleanEntity LoungeMotionLightsDisabled => new(_haContext, "input_boolean.lounge_motion_lights_disabled");
		///<summary>netdaemon_light_manager_v2_lights_manager</summary>
		public InputBooleanEntity NetdaemonLightManagerV2LightsManager => new(_haContext, "input_boolean.netdaemon_light_manager_v2_lights_manager");
		///<summary>netdaemon_niemand_discipline_manager</summary>
		public InputBooleanEntity NetdaemonNiemandDisciplineManager => new(_haContext, "input_boolean.netdaemon_niemand_discipline_manager");
		public InputBooleanEntity NotifyEugeneTelegram => new(_haContext, "input_boolean.notify_eugene_telegram");
		public InputBooleanEntity WasherAck => new(_haContext, "input_boolean.washer_ack");
	}

	public class InputNumberEntities
	{
		private readonly IHaContext _haContext;
		public InputNumberEntities(IHaContext haContext)
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
		private readonly IHaContext _haContext;
		public InputSelectEntities(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>House Mode</summary>
		public InputSelectEntity HouseMode => new(_haContext, "input_select.house_mode");
		///<summary>Timer Speaker</summary>
		public InputSelectEntity TimerSpeaker => new(_haContext, "input_select.timer_speaker");
	}

	public class InputTextEntities
	{
		private readonly IHaContext _haContext;
		public InputTextEntities(IHaContext haContext)
		{
			_haContext = haContext;
		}

		public InputTextEntity AlexaActionableNotification => new(_haContext, "input_text.alexa_actionable_notification");
		public InputTextEntity ImText => new(_haContext, "input_text.im_text");
		public InputTextEntity TtsText => new(_haContext, "input_text.tts_text");
	}

	public class LightEntities
	{
		private readonly IHaContext _haContext;
		public LightEntities(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Aaron</summary>
		public LightEntity Aaron => new(_haContext, "light.aaron");
		///<summary>Aaron 1</summary>
		public LightEntity Aaron1 => new(_haContext, "light.aaron_1");
		///<summary>Aaron 2</summary>
		public LightEntity Aaron2 => new(_haContext, "light.aaron_2");
		///<summary>Aaron 3</summary>
		public LightEntity Aaron3 => new(_haContext, "light.aaron_3");
		///<summary>Aaron 4</summary>
		public LightEntity Aaron4 => new(_haContext, "light.aaron_4");
		///<summary>Aaron Main</summary>
		public LightEntity AaronMain => new(_haContext, "light.aaron_main");
		///<summary>Aubrecia Drive light</summary>
		public LightEntity AubreciaDriveLight => new(_haContext, "light.aubrecia_drive_light");
		///<summary>Dining</summary>
		public LightEntity Dining => new(_haContext, "light.dining");
		///<summary>dining 1</summary>
		public LightEntity Dining1 => new(_haContext, "light.dining_1");
		///<summary>dining 2</summary>
		public LightEntity Dining2 => new(_haContext, "light.dining_2");
		///<summary>dining 3</summary>
		public LightEntity Dining3 => new(_haContext, "light.dining_3");
		///<summary>dining 4</summary>
		public LightEntity Dining4 => new(_haContext, "light.dining_4");
		///<summary>dining 5</summary>
		public LightEntity Dining5 => new(_haContext, "light.dining_5");
		///<summary>Dining Wall</summary>
		public LightEntity DiningWall => new(_haContext, "light.dining_wall");
		///<summary>dining wall 1</summary>
		public LightEntity DiningWall1 => new(_haContext, "light.dining_wall_1");
		///<summary>dining wall 2</summary>
		public LightEntity DiningWall2 => new(_haContext, "light.dining_wall_2");
		///<summary>Downstairs</summary>
		public LightEntity Downstairs => new(_haContext, "light.downstairs");
		///<summary>Entrance</summary>
		public LightEntity Entrance => new(_haContext, "light.entrance");
		///<summary>Floor</summary>
		public LightEntity Floor => new(_haContext, "light.floor");
		///<summary>Garden light</summary>
		public LightEntity GardenLight2 => new(_haContext, "light.garden_light_2");
		///<summary>Hallway</summary>
		public LightEntity Hallway => new(_haContext, "light.hallway");
		///<summary>Jayden</summary>
		public LightEntity Jayden => new(_haContext, "light.jayden");
		///<summary>Jayden 1</summary>
		public LightEntity Jayden1 => new(_haContext, "light.jayden_1");
		///<summary>Jayden 2</summary>
		public LightEntity Jayden2 => new(_haContext, "light.jayden_2");
		///<summary>Jayden 3</summary>
		public LightEntity Jayden3 => new(_haContext, "light.jayden_3");
		///<summary>Jayden 4</summary>
		public LightEntity Jayden4 => new(_haContext, "light.jayden_4");
		///<summary>Jayden Alarm</summary>
		public LightEntity JaydenAlarm => new(_haContext, "light.jayden_alarm");
		///<summary>Kitchen</summary>
		public LightEntity Kitchen => new(_haContext, "light.kitchen");
		///<summary>kitchen 1</summary>
		public LightEntity Kitchen1 => new(_haContext, "light.kitchen_1");
		///<summary>kitchen 2</summary>
		public LightEntity Kitchen2 => new(_haContext, "light.kitchen_2");
		///<summary>kitchen 3</summary>
		public LightEntity Kitchen3 => new(_haContext, "light.kitchen_3");
		///<summary>kitchen 4</summary>
		public LightEntity Kitchen4 => new(_haContext, "light.kitchen_4");
		///<summary>kitchen 5</summary>
		public LightEntity Kitchen5 => new(_haContext, "light.kitchen_5");
		///<summary>kitchen 6</summary>
		public LightEntity Kitchen6 => new(_haContext, "light.kitchen_6");
		///<summary>Landing</summary>
		public LightEntity Landing => new(_haContext, "light.landing");
		///<summary>landing 1</summary>
		public LightEntity Landing1 => new(_haContext, "light.landing_1");
		///<summary>Landing Day</summary>
		public LightEntity LandingDay => new(_haContext, "light.landing_day");
		///<summary>landing night</summary>
		public LightEntity LandingNight => new(_haContext, "light.landing_night");
		///<summary>Lounge</summary>
		public LightEntity Lounge => new(_haContext, "light.lounge");
		///<summary>lounge back level, light_color, on_off</summary>
		public LightEntity LoungeBackLevelLightColorOnOff => new(_haContext, "light.lounge_back_level_light_color_on_off");
		///<summary>lounge front level, light_color, on_off</summary>
		public LightEntity LoungeFrontLevelLightColorOnOff => new(_haContext, "light.lounge_front_level_light_color_on_off");
		///<summary>Master</summary>
		public LightEntity Master => new(_haContext, "light.master");
		///<summary>master 1</summary>
		public LightEntity Master1 => new(_haContext, "light.master_1");
		///<summary>Master 2</summary>
		public LightEntity Master2 => new(_haContext, "light.master_2");
		///<summary>master 3</summary>
		public LightEntity Master3 => new(_haContext, "light.master_3");
		///<summary>master 4</summary>
		public LightEntity Master4 => new(_haContext, "light.master_4");
		///<summary>Master Nightlight</summary>
		public LightEntity Master5 => new(_haContext, "light.master_5");
		///<summary>Niemand Drive light</summary>
		public LightEntity NiemandDriveLight => new(_haContext, "light.niemand_drive_light");
		///<summary>Niemand Drive Light</summary>
		public LightEntity NiemandDriveLight2 => new(_haContext, "light.niemand_drive_light_2");
		///<summary>Niemand Garden light</summary>
		public LightEntity NiemandGardenLight => new(_haContext, "light.niemand_garden_light");
		///<summary>Niemand Garden Light</summary>
		public LightEntity NiemandGardenLight2 => new(_haContext, "light.niemand_garden_light_2");
		///<summary>Office</summary>
		public LightEntity Office => new(_haContext, "light.office");
		///<summary>office 1</summary>
		public LightEntity Office1 => new(_haContext, "light.office_1");
		///<summary>office 3</summary>
		public LightEntity Office3 => new(_haContext, "light.office_3");
		///<summary>Outside</summary>
		public LightEntity Outside => new(_haContext, "light.outside");
		///<summary>Outside Back</summary>
		public LightEntity OutsideBack => new(_haContext, "light.outside_back");
		///<summary>Outside Drive</summary>
		public LightEntity OutsideDrive => new(_haContext, "light.outside_drive");
		///<summary>Outside Front</summary>
		public LightEntity OutsideFront => new(_haContext, "light.outside_front");
		///<summary>Outside Garage</summary>
		public LightEntity OutsideGarage => new(_haContext, "light.outside_garage");
		///<summary>Outside Garden Flood</summary>
		public LightEntity OutsideGardenFlood => new(_haContext, "light.outside_garden_flood");
		///<summary>Playroom</summary>
		public LightEntity Playroom => new(_haContext, "light.playroom");
		///<summary>Porch</summary>
		public LightEntity Porch => new(_haContext, "light.porch");
		///<summary>Toilet</summary>
		public LightEntity Toilet => new(_haContext, "light.toilet");
		///<summary>Upstairs</summary>
		public LightEntity Upstairs => new(_haContext, "light.upstairs");
		///<summary>Utility</summary>
		public LightEntity Utility => new(_haContext, "light.utility");
		///<summary>utility 1</summary>
		public LightEntity Utility1 => new(_haContext, "light.utility_1");
		///<summary>utility 2</summary>
		public LightEntity Utility2 => new(_haContext, "light.utility_2");
		///<summary>utility 3</summary>
		public LightEntity Utility3 => new(_haContext, "light.utility_3");
		///<summary>UtilityCupboard</summary>
		public LightEntity Utilitycupboard => new(_haContext, "light.utilitycupboard");
	}

	public class LockEntities
	{
		private readonly IHaContext _haContext;
		public LockEntities(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Audi Q7 Door lock</summary>
		public LockEntity AudiQ7DoorLock => new(_haContext, "lock.audi_q7_door_lock");
	}

	public class MediaPlayerEntities
	{
		private readonly IHaContext _haContext;
		public MediaPlayerEntities(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Aaron</summary>
		public MediaPlayerEntity Aaron => new(_haContext, "media_player.aaron");
		///<summary>Bedroom Apple TV</summary>
		public MediaPlayerEntity BedroomAppleTv => new(_haContext, "media_player.bedroom_apple_tv");
		///<summary>Bedroom Apple TV</summary>
		public MediaPlayerEntity BedroomAppleTv2 => new(_haContext, "media_player.bedroom_apple_tv_2");
		///<summary>Dining</summary>
		public MediaPlayerEntity Dining => new(_haContext, "media_player.dining");
		///<summary>Downstairs</summary>
		public MediaPlayerEntity Downstairs => new(_haContext, "media_player.downstairs");
		///<summary>Garden</summary>
		public MediaPlayerEntity EugeneS2ndEchoDot => new(_haContext, "media_player.eugene_s_2nd_echo_dot");
		///<summary>Garage</summary>
		public MediaPlayerEntity EugeneS5thEchoDot => new(_haContext, "media_player.eugene_s_5th_echo_dot");
		///<summary>Wallpanel</summary>
		public MediaPlayerEntity EugeneSFire => new(_haContext, "media_player.eugene_s_fire");
		///<summary>Lounge LG Alexa</summary>
		public MediaPlayerEntity EugeneSLgOledWebos2021Tv => new(_haContext, "media_player.eugene_s_lg_oled_webos_2021_tv");
		///<summary>Lounge</summary>
		public MediaPlayerEntity EugeneSSonosArc => new(_haContext, "media_player.eugene_s_sonos_arc");
		///<summary>Everywhere</summary>
		public MediaPlayerEntity Everywhere2 => new(_haContext, "media_player.everywhere_2");
		///<summary>Jayden</summary>
		public MediaPlayerEntity Jayden => new(_haContext, "media_player.jayden");
		///<summary>Kitchen</summary>
		public MediaPlayerEntity Kitchen => new(_haContext, "media_player.kitchen");
		///<summary>Living Room</summary>
		public MediaPlayerEntity LivingRoom => new(_haContext, "media_player.living_room");
		///<summary>Living Room</summary>
		public MediaPlayerEntity LivingRoom3 => new(_haContext, "media_player.living_room_3");
		///<summary>Lounge</summary>
		public MediaPlayerEntity Lounge => new(_haContext, "media_player.lounge");
		///<summary>Lounge Sonos</summary>
		public MediaPlayerEntity LoungeSonos => new(_haContext, "media_player.lounge_sonos");
		///<summary>Lounge TV</summary>
		public MediaPlayerEntity LoungeTv => new(_haContext, "media_player.lounge_tv");
		///<summary>Master</summary>
		public MediaPlayerEntity Master => new(_haContext, "media_player.master");
		///<summary>Master TV</summary>
		public MediaPlayerEntity MasterTv2 => new(_haContext, "media_player.master_tv_2");
		///<summary>Master TV Alexa</summary>
		public MediaPlayerEntity MasterTvAlexa => new(_haContext, "media_player.master_tv_alexa");
		///<summary>Office</summary>
		public MediaPlayerEntity Office => new(_haContext, "media_player.office");
		///<summary>Playroom</summary>
		public MediaPlayerEntity Playroom => new(_haContext, "media_player.playroom");
		///<summary>Spare echo</summary>
		public MediaPlayerEntity SpareEcho => new(_haContext, "media_player.spare_echo");
		///<summary>This Device</summary>
		public MediaPlayerEntity ThisDevice => new(_haContext, "media_player.this_device");
		///<summary>This Device</summary>
		public MediaPlayerEntity ThisDevice2 => new(_haContext, "media_player.this_device_2");
		///<summary>Upstairs</summary>
		public MediaPlayerEntity Upstairs => new(_haContext, "media_player.upstairs");
	}

	public class NumberEntities
	{
		private readonly IHaContext _haContext;
		public NumberEntities(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Downstairs Snooze Minutes</summary>
		public NumberEntity DownstairsSnoozeMinutes => new(_haContext, "number.downstairs_snooze_minutes");
		///<summary>Downstairs Volume</summary>
		public NumberEntity DownstairsVolume => new(_haContext, "number.downstairs_volume");
		///<summary>Lounge Audio Delay</summary>
		public NumberEntity LoungeAudioDelay => new(_haContext, "number.lounge_audio_delay");
		///<summary>Lounge Bass</summary>
		public NumberEntity LoungeBass => new(_haContext, "number.lounge_bass");
		///<summary>Lounge Treble</summary>
		public NumberEntity LoungeTreble => new(_haContext, "number.lounge_treble");
		///<summary>Niemand Drive Snapshot Interval</summary>
		public NumberEntity NiemandDriveSnapshotInterval => new(_haContext, "number.niemand_drive_snapshot_interval");
		///<summary>Niemand Front Door Snapshot Interval</summary>
		public NumberEntity NiemandFrontDoorSnapshotInterval => new(_haContext, "number.niemand_front_door_snapshot_interval");
		///<summary>Niemand Garage Snapshot Interval</summary>
		public NumberEntity NiemandGarageSnapshotInterval => new(_haContext, "number.niemand_garage_snapshot_interval");
		///<summary>Niemand Garden Snapshot Interval</summary>
		public NumberEntity NiemandGardenSnapshotInterval => new(_haContext, "number.niemand_garden_snapshot_interval");
		///<summary>Niemand Side Snapshot Interval</summary>
		public NumberEntity NiemandSideSnapshotInterval => new(_haContext, "number.niemand_side_snapshot_interval");
		///<summary>Ring Chime Snooze Minutes</summary>
		public NumberEntity RingChimeSnoozeMinutes => new(_haContext, "number.ring_chime_snooze_minutes");
		///<summary>Ring Chime Volume</summary>
		public NumberEntity RingChimeVolume => new(_haContext, "number.ring_chime_volume");
	}

	public class OctopusagileEntities
	{
		private readonly IHaContext _haContext;
		public OctopusagileEntities(IHaContext haContext)
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
		private readonly IHaContext _haContext;
		public PersonEntities(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Aubrecia</summary>
		public PersonEntity Aubrecia => new(_haContext, "person.aubrecia");
		///<summary>Eugene</summary>
		public PersonEntity Eugene => new(_haContext, "person.eugene");
		///<summary>Hailey</summary>
		public PersonEntity Hailey => new(_haContext, "person.hailey");
		///<summary>NetDaemon</summary>
		public PersonEntity Netdaemon => new(_haContext, "person.netdaemon");
	}

	public class RemoteEntities
	{
		private readonly IHaContext _haContext;
		public RemoteEntities(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Bedroom Apple TV</summary>
		public RemoteEntity BedroomAppleTv => new(_haContext, "remote.bedroom_apple_tv");
		///<summary>Bedroom Apple TV</summary>
		public RemoteEntity BedroomAppleTv2 => new(_haContext, "remote.bedroom_apple_tv_2");
		///<summary>Living Room</summary>
		public RemoteEntity LivingRoom => new(_haContext, "remote.living_room");
		///<summary>Living Room</summary>
		public RemoteEntity LivingRoom2 => new(_haContext, "remote.living_room_2");
		///<summary>Living Room</summary>
		public RemoteEntity LivingRoom3 => new(_haContext, "remote.living_room_3");
	}

	public class ScriptEntities
	{
		private readonly IHaContext _haContext;
		public ScriptEntities(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>activate_alexa_actionable_notification</summary>
		public ScriptEntity ActivateAlexaActionableNotification => new(_haContext, "script.activate_alexa_actionable_notification");
		///<summary>arrive_home</summary>
		public ScriptEntity ArriveHome => new(_haContext, "script.arrive_home");
		///<summary>im_text</summary>
		public ScriptEntity ImText => new(_haContext, "script.im_text");
		///<summary>Set Ring Mqtt Snapshot Interval</summary>
		public ScriptEntity RingMqttInterval => new(_haContext, "script.ring_mqtt_interval");
		///<summary>tts</summary>
		public ScriptEntity Tts => new(_haContext, "script.tts");
		///<summary>tts_text</summary>
		public ScriptEntity TtsText => new(_haContext, "script.tts_text");
		///<summary>weather</summary>
		public ScriptEntity Weather => new(_haContext, "script.weather");
	}

	public class SelectEntities
	{
		private readonly IHaContext _haContext;
		public SelectEntities(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Niemand Drive Event Select</summary>
		public SelectEntity NiemandDriveEventSelect => new(_haContext, "select.niemand_drive_event_select");
		///<summary>Niemand Front Door Event Select</summary>
		public SelectEntity NiemandFrontDoorEventSelect => new(_haContext, "select.niemand_front_door_event_select");
		///<summary>Niemand Garage Event Select</summary>
		public SelectEntity NiemandGarageEventSelect => new(_haContext, "select.niemand_garage_event_select");
		///<summary>Niemand Garden Event Select</summary>
		public SelectEntity NiemandGardenEventSelect => new(_haContext, "select.niemand_garden_event_select");
		///<summary>Niemand Side Event Select</summary>
		public SelectEntity NiemandSideEventSelect => new(_haContext, "select.niemand_side_event_select");
	}

	public class SensorEntities
	{
		private readonly IHaContext _haContext;
		public SensorEntities(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>247D4D7D6C90-mysimplelink RX</summary>
		public NumericSensorEntity E247d4d7d6c90MysimplelinkRx => new(_haContext, "sensor.247d4d7d6c90_mysimplelink_rx");
		///<summary>247D4D7D6C90-mysimplelink TX</summary>
		public NumericSensorEntity E247d4d7d6c90MysimplelinkTx => new(_haContext, "sensor.247d4d7d6c90_mysimplelink_tx");
		///<summary>Aaron Echo RX</summary>
		public NumericSensorEntity AaronEchoRx => new(_haContext, "sensor.aaron_echo_rx");
		///<summary>Aaron Echo TX</summary>
		public NumericSensorEntity AaronEchoTx => new(_haContext, "sensor.aaron_echo_tx");
		///<summary>Aaron Lux</summary>
		public NumericSensorEntity AaronLux => new(_haContext, "sensor.aaron_lux");
		///<summary>Aaron Motion Battery</summary>
		public NumericSensorEntity AaronMotionBattery => new(_haContext, "sensor.aaron_motion_battery");
		///<summary>AccuWeather Home Cloud Ceiling</summary>
		public NumericSensorEntity AccuweatherHomeCloudCeiling => new(_haContext, "sensor.accuweather_home_cloud_ceiling");
		///<summary>AccuWeather Home Hours Of Sun 0d</summary>
		public NumericSensorEntity AccuweatherHomeHoursOfSun0d => new(_haContext, "sensor.accuweather_home_hours_of_sun_0d");
		///<summary>AccuWeather Home Hours Of Sun 1d</summary>
		public NumericSensorEntity AccuweatherHomeHoursOfSun1d => new(_haContext, "sensor.accuweather_home_hours_of_sun_1d");
		///<summary>AccuWeather Home Hours Of Sun 2d</summary>
		public NumericSensorEntity AccuweatherHomeHoursOfSun2d => new(_haContext, "sensor.accuweather_home_hours_of_sun_2d");
		///<summary>AccuWeather Home Hours Of Sun 3d</summary>
		public NumericSensorEntity AccuweatherHomeHoursOfSun3d => new(_haContext, "sensor.accuweather_home_hours_of_sun_3d");
		///<summary>AccuWeather Home Hours Of Sun 4d</summary>
		public NumericSensorEntity AccuweatherHomeHoursOfSun4d => new(_haContext, "sensor.accuweather_home_hours_of_sun_4d");
		///<summary>AccuWeather Home Precipitation</summary>
		public NumericSensorEntity AccuweatherHomePrecipitation => new(_haContext, "sensor.accuweather_home_precipitation");
		///<summary>AccuWeather Home RealFeel Temperature</summary>
		public NumericSensorEntity AccuweatherHomeRealfeelTemperature => new(_haContext, "sensor.accuweather_home_realfeel_temperature");
		///<summary>AccuWeather Home RealFeel Temperature Max 0d</summary>
		public NumericSensorEntity AccuweatherHomeRealfeelTemperatureMax0d => new(_haContext, "sensor.accuweather_home_realfeel_temperature_max_0d");
		///<summary>AccuWeather Home RealFeel Temperature Max 1d</summary>
		public NumericSensorEntity AccuweatherHomeRealfeelTemperatureMax1d => new(_haContext, "sensor.accuweather_home_realfeel_temperature_max_1d");
		///<summary>AccuWeather Home RealFeel Temperature Max 2d</summary>
		public NumericSensorEntity AccuweatherHomeRealfeelTemperatureMax2d => new(_haContext, "sensor.accuweather_home_realfeel_temperature_max_2d");
		///<summary>AccuWeather Home RealFeel Temperature Max 3d</summary>
		public NumericSensorEntity AccuweatherHomeRealfeelTemperatureMax3d => new(_haContext, "sensor.accuweather_home_realfeel_temperature_max_3d");
		///<summary>AccuWeather Home RealFeel Temperature Max 4d</summary>
		public NumericSensorEntity AccuweatherHomeRealfeelTemperatureMax4d => new(_haContext, "sensor.accuweather_home_realfeel_temperature_max_4d");
		///<summary>AccuWeather Home RealFeel Temperature Min 0d</summary>
		public NumericSensorEntity AccuweatherHomeRealfeelTemperatureMin0d => new(_haContext, "sensor.accuweather_home_realfeel_temperature_min_0d");
		///<summary>AccuWeather Home RealFeel Temperature Min 1d</summary>
		public NumericSensorEntity AccuweatherHomeRealfeelTemperatureMin1d => new(_haContext, "sensor.accuweather_home_realfeel_temperature_min_1d");
		///<summary>AccuWeather Home RealFeel Temperature Min 2d</summary>
		public NumericSensorEntity AccuweatherHomeRealfeelTemperatureMin2d => new(_haContext, "sensor.accuweather_home_realfeel_temperature_min_2d");
		///<summary>AccuWeather Home RealFeel Temperature Min 3d</summary>
		public NumericSensorEntity AccuweatherHomeRealfeelTemperatureMin3d => new(_haContext, "sensor.accuweather_home_realfeel_temperature_min_3d");
		///<summary>AccuWeather Home RealFeel Temperature Min 4d</summary>
		public NumericSensorEntity AccuweatherHomeRealfeelTemperatureMin4d => new(_haContext, "sensor.accuweather_home_realfeel_temperature_min_4d");
		///<summary>AccuWeather Home Thunderstorm Probability Day 0d</summary>
		public NumericSensorEntity AccuweatherHomeThunderstormProbabilityDay0d => new(_haContext, "sensor.accuweather_home_thunderstorm_probability_day_0d");
		///<summary>AccuWeather Home Thunderstorm Probability Day 1d</summary>
		public NumericSensorEntity AccuweatherHomeThunderstormProbabilityDay1d => new(_haContext, "sensor.accuweather_home_thunderstorm_probability_day_1d");
		///<summary>AccuWeather Home Thunderstorm Probability Day 2d</summary>
		public NumericSensorEntity AccuweatherHomeThunderstormProbabilityDay2d => new(_haContext, "sensor.accuweather_home_thunderstorm_probability_day_2d");
		///<summary>AccuWeather Home Thunderstorm Probability Day 3d</summary>
		public NumericSensorEntity AccuweatherHomeThunderstormProbabilityDay3d => new(_haContext, "sensor.accuweather_home_thunderstorm_probability_day_3d");
		///<summary>AccuWeather Home Thunderstorm Probability Day 4d</summary>
		public NumericSensorEntity AccuweatherHomeThunderstormProbabilityDay4d => new(_haContext, "sensor.accuweather_home_thunderstorm_probability_day_4d");
		///<summary>AccuWeather Home Thunderstorm Probability Night 0d</summary>
		public NumericSensorEntity AccuweatherHomeThunderstormProbabilityNight0d => new(_haContext, "sensor.accuweather_home_thunderstorm_probability_night_0d");
		///<summary>AccuWeather Home Thunderstorm Probability Night 1d</summary>
		public NumericSensorEntity AccuweatherHomeThunderstormProbabilityNight1d => new(_haContext, "sensor.accuweather_home_thunderstorm_probability_night_1d");
		///<summary>AccuWeather Home Thunderstorm Probability Night 2d</summary>
		public NumericSensorEntity AccuweatherHomeThunderstormProbabilityNight2d => new(_haContext, "sensor.accuweather_home_thunderstorm_probability_night_2d");
		///<summary>AccuWeather Home Thunderstorm Probability Night 3d</summary>
		public NumericSensorEntity AccuweatherHomeThunderstormProbabilityNight3d => new(_haContext, "sensor.accuweather_home_thunderstorm_probability_night_3d");
		///<summary>AccuWeather Home Thunderstorm Probability Night 4d</summary>
		public NumericSensorEntity AccuweatherHomeThunderstormProbabilityNight4d => new(_haContext, "sensor.accuweather_home_thunderstorm_probability_night_4d");
		///<summary>AccuWeather Home UV Index</summary>
		public NumericSensorEntity AccuweatherHomeUvIndex => new(_haContext, "sensor.accuweather_home_uv_index");
		///<summary>AccuWeather Home UV Index 0d</summary>
		public NumericSensorEntity AccuweatherHomeUvIndex0d => new(_haContext, "sensor.accuweather_home_uv_index_0d");
		///<summary>AccuWeather Home UV Index 1d</summary>
		public NumericSensorEntity AccuweatherHomeUvIndex1d => new(_haContext, "sensor.accuweather_home_uv_index_1d");
		///<summary>AccuWeather Home UV Index 2d</summary>
		public NumericSensorEntity AccuweatherHomeUvIndex2d => new(_haContext, "sensor.accuweather_home_uv_index_2d");
		///<summary>AccuWeather Home UV Index 3d</summary>
		public NumericSensorEntity AccuweatherHomeUvIndex3d => new(_haContext, "sensor.accuweather_home_uv_index_3d");
		///<summary>AccuWeather Home UV Index 4d</summary>
		public NumericSensorEntity AccuweatherHomeUvIndex4d => new(_haContext, "sensor.accuweather_home_uv_index_4d");
		///<summary>AccuWeather Home Wind</summary>
		public NumericSensorEntity AccuweatherHomeWind => new(_haContext, "sensor.accuweather_home_wind");
		///<summary>AccuWeather Home Wind Day 0d</summary>
		public NumericSensorEntity AccuweatherHomeWindDay0d => new(_haContext, "sensor.accuweather_home_wind_day_0d");
		///<summary>AccuWeather Home Wind Day 1d</summary>
		public NumericSensorEntity AccuweatherHomeWindDay1d => new(_haContext, "sensor.accuweather_home_wind_day_1d");
		///<summary>AccuWeather Home Wind Day 2d</summary>
		public NumericSensorEntity AccuweatherHomeWindDay2d => new(_haContext, "sensor.accuweather_home_wind_day_2d");
		///<summary>AccuWeather Home Wind Day 3d</summary>
		public NumericSensorEntity AccuweatherHomeWindDay3d => new(_haContext, "sensor.accuweather_home_wind_day_3d");
		///<summary>AccuWeather Home Wind Day 4d</summary>
		public NumericSensorEntity AccuweatherHomeWindDay4d => new(_haContext, "sensor.accuweather_home_wind_day_4d");
		///<summary>AccuWeather Home Wind Night 0d</summary>
		public NumericSensorEntity AccuweatherHomeWindNight0d => new(_haContext, "sensor.accuweather_home_wind_night_0d");
		///<summary>AccuWeather Home Wind Night 1d</summary>
		public NumericSensorEntity AccuweatherHomeWindNight1d => new(_haContext, "sensor.accuweather_home_wind_night_1d");
		///<summary>AccuWeather Home Wind Night 2d</summary>
		public NumericSensorEntity AccuweatherHomeWindNight2d => new(_haContext, "sensor.accuweather_home_wind_night_2d");
		///<summary>AccuWeather Home Wind Night 3d</summary>
		public NumericSensorEntity AccuweatherHomeWindNight3d => new(_haContext, "sensor.accuweather_home_wind_night_3d");
		///<summary>AccuWeather Home Wind Night 4d</summary>
		public NumericSensorEntity AccuweatherHomeWindNight4d => new(_haContext, "sensor.accuweather_home_wind_night_4d");
		///<summary>Treadmill RX</summary>
		public NumericSensorEntity AndroidB8c33f1cb7c0d776Rx => new(_haContext, "sensor.android_b8c33f1cb7c0d776_rx");
		///<summary>Treadmill TX</summary>
		public NumericSensorEntity AndroidB8c33f1cb7c0d776Tx => new(_haContext, "sensor.android_b8c33f1cb7c0d776_tx");
		///<summary>ASGLH-WL-19140 RX</summary>
		public NumericSensorEntity AsglhWl19140Rx => new(_haContext, "sensor.asglh_wl_19140_rx");
		///<summary>ASGLH-WL-19140 TX</summary>
		public NumericSensorEntity AsglhWl19140Tx => new(_haContext, "sensor.asglh_wl_19140_tx");
		///<summary>Aubrecia Average Active Pace</summary>
		public NumericSensorEntity AubreciaAverageActivePace => new(_haContext, "sensor.aubrecia_average_active_pace");
		///<summary>Aubrecia Battery Level</summary>
		public NumericSensorEntity AubreciaBatteryLevel => new(_haContext, "sensor.aubrecia_battery_level");
		///<summary>Aubrecia Distance</summary>
		public NumericSensorEntity AubreciaDistance => new(_haContext, "sensor.aubrecia_distance");
		///<summary>Aubrecia Drive Battery</summary>
		public NumericSensorEntity AubreciaDriveBattery => new(_haContext, "sensor.aubrecia_drive_battery");
		///<summary>Aubrecia Floors Ascended</summary>
		public NumericSensorEntity AubreciaFloorsAscended => new(_haContext, "sensor.aubrecia_floors_ascended");
		///<summary>Aubrecia Floors Descended</summary>
		public NumericSensorEntity AubreciaFloorsDescended => new(_haContext, "sensor.aubrecia_floors_descended");
		///<summary>Aubrecia Front Door Battery</summary>
		public NumericSensorEntity AubreciaFrontDoorBattery => new(_haContext, "sensor.aubrecia_front_door_battery");
		///<summary>Aubrecia Steps</summary>
		public NumericSensorEntity AubreciaSteps => new(_haContext, "sensor.aubrecia_steps");
		///<summary>Aubrecia Storage</summary>
		public NumericSensorEntity AubreciaStorage => new(_haContext, "sensor.aubrecia_storage");
		///<summary>AubreciasiPhone RX</summary>
		public NumericSensorEntity AubreciasiphoneRx => new(_haContext, "sensor.aubreciasiphone_rx");
		///<summary>AubreciasiPhone TX</summary>
		public NumericSensorEntity AubreciasiphoneTx => new(_haContext, "sensor.aubreciasiphone_tx");
		///<summary>Audi Q7 Mileage</summary>
		public NumericSensorEntity AudiQ7MileageKm => new(_haContext, "sensor.audi_q7_mileage_km");
		///<summary>Audi Q7 Mileage</summary>
		public NumericSensorEntity AudiQ7MileageMi => new(_haContext, "sensor.audi_q7_mileage_mi");
		///<summary>Audi Q7 Oil change distance</summary>
		public NumericSensorEntity AudiQ7OilChangeDistanceKm => new(_haContext, "sensor.audi_q7_oil_change_distance_km");
		///<summary>Audi Q7 Oil change distance</summary>
		public NumericSensorEntity AudiQ7OilChangeDistanceMi => new(_haContext, "sensor.audi_q7_oil_change_distance_mi");
		///<summary>Audi Q7 Oil change time</summary>
		public NumericSensorEntity AudiQ7OilChangeTime => new(_haContext, "sensor.audi_q7_oil_change_time");
		///<summary>Audi Q7 Oil level</summary>
		public NumericSensorEntity AudiQ7OilLevel => new(_haContext, "sensor.audi_q7_oil_level");
		///<summary>Audi Q7 Range</summary>
		public NumericSensorEntity AudiQ7RangeKm => new(_haContext, "sensor.audi_q7_range_km");
		///<summary>Audi Q7 Range</summary>
		public NumericSensorEntity AudiQ7RangeMi => new(_haContext, "sensor.audi_q7_range_mi");
		///<summary>Audi Q7 Service inspection distance</summary>
		public NumericSensorEntity AudiQ7ServiceInspectionDistanceKm => new(_haContext, "sensor.audi_q7_service_inspection_distance_km");
		///<summary>Audi Q7 Service inspection distance</summary>
		public NumericSensorEntity AudiQ7ServiceInspectionDistanceMi => new(_haContext, "sensor.audi_q7_service_inspection_distance_mi");
		///<summary>Audi Q7 Service inspection time</summary>
		public NumericSensorEntity AudiQ7ServiceInspectionTime => new(_haContext, "sensor.audi_q7_service_inspection_time");
		///<summary>Audi Q7 Tank level</summary>
		public NumericSensorEntity AudiQ7TankLevel => new(_haContext, "sensor.audi_q7_tank_level");
		///<summary>Average Lux Downstairs</summary>
		public NumericSensorEntity AverageLuxDownstairs => new(_haContext, "sensor.average_lux_downstairs");
		///<summary>Average Lux Upstairs</summary>
		public NumericSensorEntity AverageLuxUpstairs => new(_haContext, "sensor.average_lux_upstairs");
		///<summary>Average Ping Konnected AddOn Count</summary>
		public NumericSensorEntity AveragePingKonnectedAddonCount => new(_haContext, "sensor.average_ping_konnected_addon_count");
		///<summary>Average Ping Konnected AddOn Ratio</summary>
		public NumericSensorEntity AveragePingKonnectedAddonRatio => new(_haContext, "sensor.average_ping_konnected_addon_ratio");
		///<summary>Average Ping Konnected Google Count</summary>
		public NumericSensorEntity AveragePingKonnectedGoogleCount => new(_haContext, "sensor.average_ping_konnected_google_count");
		///<summary>Average Ping Konnected Google Ratio</summary>
		public NumericSensorEntity AveragePingKonnectedGoogleRatio => new(_haContext, "sensor.average_ping_konnected_google_ratio");
		///<summary>Average Ping Konnected Main Count</summary>
		public NumericSensorEntity AveragePingKonnectedMainCount => new(_haContext, "sensor.average_ping_konnected_main_count");
		///<summary>Average Ping Konnected Main Ratio</summary>
		public NumericSensorEntity AveragePingKonnectedMainRatio => new(_haContext, "sensor.average_ping_konnected_main_ratio");
		///<summary>Average Temp</summary>
		public NumericSensorEntity AverageTemp => new(_haContext, "sensor.average_temp");
		///<summary>Average Temp Downstairs</summary>
		public NumericSensorEntity AverageTempDownstairs => new(_haContext, "sensor.average_temp_downstairs");
		///<summary>Average Temp Upstairs</summary>
		public NumericSensorEntity AverageTempUpstairs => new(_haContext, "sensor.average_temp_upstairs");
		///<summary>Bathroom Lux</summary>
		public NumericSensorEntity BathroomLux => new(_haContext, "sensor.bathroom_lux");
		///<summary>Bathroom Motion Battery</summary>
		public NumericSensorEntity BathroomMotionBattery => new(_haContext, "sensor.bathroom_motion_battery");
		///<summary>Bedroom 1 AC RX</summary>
		public NumericSensorEntity Bedroom1AcRx => new(_haContext, "sensor.bedroom_1_ac_rx");
		///<summary>Bedroom 1 AC TX</summary>
		public NumericSensorEntity Bedroom1AcTx => new(_haContext, "sensor.bedroom_1_ac_tx");
		///<summary>Bedroom 1 Energy</summary>
		public NumericSensorEntity Bedroom1Energy => new(_haContext, "sensor.bedroom_1_energy");
		///<summary>Bedroom 1 Room Temperature</summary>
		public NumericSensorEntity Bedroom1RoomTemperature => new(_haContext, "sensor.bedroom_1_room_temperature");
		///<summary>Bedroom 2 AC RX</summary>
		public NumericSensorEntity Bedroom2AcRx => new(_haContext, "sensor.bedroom_2_ac_rx");
		///<summary>Bedroom 2 AC TX</summary>
		public NumericSensorEntity Bedroom2AcTx => new(_haContext, "sensor.bedroom_2_ac_tx");
		///<summary>Bedroom 2 Energy</summary>
		public NumericSensorEntity Bedroom2Energy => new(_haContext, "sensor.bedroom_2_energy");
		///<summary>Bedroom 2 Room Temperature</summary>
		public NumericSensorEntity Bedroom2RoomTemperature => new(_haContext, "sensor.bedroom_2_room_temperature");
		///<summary>Bedroom 3 AC RX</summary>
		public NumericSensorEntity Bedroom3AcRx => new(_haContext, "sensor.bedroom_3_ac_rx");
		///<summary>Bedroom 3 AC TX</summary>
		public NumericSensorEntity Bedroom3AcTx => new(_haContext, "sensor.bedroom_3_ac_tx");
		///<summary>Bedroom 3 Energy</summary>
		public NumericSensorEntity Bedroom3Energy => new(_haContext, "sensor.bedroom_3_energy");
		///<summary>Bedroom 3 Room Temperature</summary>
		public NumericSensorEntity Bedroom3RoomTemperature => new(_haContext, "sensor.bedroom_3_room_temperature");
		///<summary>Bedroom 4 AC RX</summary>
		public NumericSensorEntity Bedroom4AcRx => new(_haContext, "sensor.bedroom_4_ac_rx");
		///<summary>Bedroom 4 AC TX</summary>
		public NumericSensorEntity Bedroom4AcTx => new(_haContext, "sensor.bedroom_4_ac_tx");
		///<summary>Bedroom 4 Energy</summary>
		public NumericSensorEntity Bedroom4Energy => new(_haContext, "sensor.bedroom_4_energy");
		///<summary>Bedroom 4 Room Temperature</summary>
		public NumericSensorEntity Bedroom4RoomTemperature => new(_haContext, "sensor.bedroom_4_room_temperature");
		///<summary>Blind Lounge Energy</summary>
		public NumericSensorEntity BlindLoungeEnergy => new(_haContext, "sensor.blind_lounge_energy");
		///<summary>christmas_indoor-1558 RX</summary>
		public NumericSensorEntity ChristmasIndoor1558Rx => new(_haContext, "sensor.christmas_indoor_1558_rx");
		///<summary>christmas_indoor-1558 TX</summary>
		public NumericSensorEntity ChristmasIndoor1558Tx => new(_haContext, "sensor.christmas_indoor_1558_tx");
		///<summary>Circadian Values</summary>
		public NumericSensorEntity CircadianValues => new(_haContext, "sensor.circadian_values");
		///<summary>ASAZ-5CG127498B RX</summary>
		public NumericSensorEntity DesktopIpurn8tRx => new(_haContext, "sensor.desktop_ipurn8t_rx");
		///<summary>ASAZ-5CG127498B TX</summary>
		public NumericSensorEntity DesktopIpurn8tTx => new(_haContext, "sensor.desktop_ipurn8t_tx");
		///<summary>Dining Echo RX</summary>
		public NumericSensorEntity DiningEchoRx => new(_haContext, "sensor.dining_echo_rx");
		///<summary>Dining Echo TX</summary>
		public NumericSensorEntity DiningEchoTx => new(_haContext, "sensor.dining_echo_tx");
		///<summary>Dining Lux</summary>
		public NumericSensorEntity DiningLux => new(_haContext, "sensor.dining_lux");
		///<summary>Dining Motion Battery</summary>
		public NumericSensorEntity DiningMotionBattery => new(_haContext, "sensor.dining_motion_battery");
		///<summary>Dining RX</summary>
		public NumericSensorEntity DiningRx => new(_haContext, "sensor.dining_rx");
		///<summary>Dining TX</summary>
		public NumericSensorEntity DiningTx => new(_haContext, "sensor.dining_tx");
		///<summary>Dishwasher Energy</summary>
		public NumericSensorEntity DishwasherEnergy => new(_haContext, "sensor.dishwasher_energy");
		///<summary>Dishwasher Power</summary>
		public NumericSensorEntity DishwasherPower => new(_haContext, "sensor.dishwasher_power");
		///<summary>Downstairs Wireless</summary>
		public NumericSensorEntity DownstairsWireless => new(_haContext, "sensor.downstairs_wireless");
		///<summary>Drive Energy</summary>
		public NumericSensorEntity DriveEnergy => new(_haContext, "sensor.drive_energy");
		///<summary>Drive Energy Spent</summary>
		public NumericSensorEntity DriveEnergySpent => new(_haContext, "sensor.drive_energy_spent");
		///<summary>drive_meter</summary>
		public NumericSensorEntity DriveMeter => new(_haContext, "sensor.drive_meter");
		///<summary>Drive Power</summary>
		public NumericSensorEntity DrivePower => new(_haContext, "sensor.drive_power");
		///<summary>Drive Temp</summary>
		public NumericSensorEntity DriveTemp => new(_haContext, "sensor.drive_temp");
		///<summary>Dryer Energy</summary>
		public NumericSensorEntity DryerEnergy => new(_haContext, "sensor.dryer_energy");
		///<summary>Dryer Power</summary>
		public NumericSensorEntity DryerPower => new(_haContext, "sensor.dryer_power");
		///<summary>dryer RX</summary>
		public NumericSensorEntity DryerRx => new(_haContext, "sensor.dryer_rx");
		///<summary>dryer TX</summary>
		public NumericSensorEntity DryerTx => new(_haContext, "sensor.dryer_tx");
		///<summary>Electric Consumption (Today)</summary>
		public NumericSensorEntity ElectricConsumptionToday => new(_haContext, "sensor.electric_consumption_today");
		///<summary>Electric Consumption (Year)</summary>
		public NumericSensorEntity ElectricConsumptionYear => new(_haContext, "sensor.electric_consumption_year");
		///<summary>Electric Cost (Today)</summary>
		public NumericSensorEntity ElectricCostToday => new(_haContext, "sensor.electric_cost_today");
		///<summary>Electric Tariff Rate</summary>
		public NumericSensorEntity ElectricTariffRate => new(_haContext, "sensor.electric_tariff_rate");
		///<summary>Electric Tariff Standing</summary>
		public NumericSensorEntity ElectricTariffStanding => new(_haContext, "sensor.electric_tariff_standing");
		///<summary>Entrance Lux</summary>
		public NumericSensorEntity EntranceLux => new(_haContext, "sensor.entrance_lux");
		///<summary>Entrance Motion Battery</summary>
		public NumericSensorEntity EntranceMotionBattery => new(_haContext, "sensor.entrance_motion_battery");
		///<summary>Entrance RX</summary>
		public NumericSensorEntity EntranceRx => new(_haContext, "sensor.entrance_rx");
		///<summary>Entrance TX</summary>
		public NumericSensorEntity EntranceTx => new(_haContext, "sensor.entrance_tx");
		///<summary>ESP_6B7081 RX</summary>
		public NumericSensorEntity Esp6b7081Rx => new(_haContext, "sensor.esp_6b7081_rx");
		///<summary>ESP_6B7081 TX</summary>
		public NumericSensorEntity Esp6b7081Tx => new(_haContext, "sensor.esp_6b7081_tx");
		///<summary>ESP_6B7A3A RX</summary>
		public NumericSensorEntity Esp6b7a3aRx => new(_haContext, "sensor.esp_6b7a3a_rx");
		///<summary>ESP_6B7A3A TX</summary>
		public NumericSensorEntity Esp6b7a3aTx => new(_haContext, "sensor.esp_6b7a3a_tx");
		///<summary>eufy RoboVac RX</summary>
		public NumericSensorEntity EufyRobovacRx => new(_haContext, "sensor.eufy_robovac_rx");
		///<summary>eufy RoboVac RX</summary>
		public NumericSensorEntity EufyRobovacRx2 => new(_haContext, "sensor.eufy_robovac_rx_2");
		///<summary>eufy RoboVac TX</summary>
		public NumericSensorEntity EufyRobovacTx => new(_haContext, "sensor.eufy_robovac_tx");
		///<summary>eufy RoboVac TX</summary>
		public NumericSensorEntity EufyRobovacTx2 => new(_haContext, "sensor.eufy_robovac_tx_2");
		///<summary>EUGENE-DESKTOP Battery Full Lifetime</summary>
		public NumericSensorEntity EugeneDesktopBatteryFullLifetime => new(_haContext, "sensor.eugene_desktop_battery_full_lifetime");
		///<summary>EUGENE-DESKTOP Battery Remaining</summary>
		public NumericSensorEntity EugeneDesktopBatteryRemaining => new(_haContext, "sensor.eugene_desktop_battery_remaining");
		///<summary>EUGENE-DESKTOP Battery Remaining Time</summary>
		public NumericSensorEntity EugeneDesktopBatteryRemainingTime => new(_haContext, "sensor.eugene_desktop_battery_remaining_time");
		///<summary>EUGENE-DESKTOP CPU Usage</summary>
		public NumericSensorEntity EugeneDesktopCpuUsage => new(_haContext, "sensor.eugene_desktop_cpu_usage");
		///<summary>EUGENE-DESKTOP Memory Available</summary>
		public NumericSensorEntity EugeneDesktopMemoryAvailable => new(_haContext, "sensor.eugene_desktop_memory_available");
		///<summary>EUGENE-DESKTOP Memory Total</summary>
		public NumericSensorEntity EugeneDesktopMemoryTotal => new(_haContext, "sensor.eugene_desktop_memory_total");
		///<summary>EUGENE-DESKTOP Memory Usage</summary>
		public NumericSensorEntity EugeneDesktopMemoryUsage => new(_haContext, "sensor.eugene_desktop_memory_usage");
		///<summary>EUGENE-DESKTOP Memory Used</summary>
		public NumericSensorEntity EugeneDesktopMemoryUsed => new(_haContext, "sensor.eugene_desktop_memory_used");
		///<summary>EUGENE-DESKTOP Network #0 - BPS Received</summary>
		public NumericSensorEntity EugeneDesktopNetwork0BpsReceived => new(_haContext, "sensor.eugene_desktop_network_0_bps_received");
		///<summary>EUGENE-DESKTOP Network #0 - BPS Sent</summary>
		public NumericSensorEntity EugeneDesktopNetwork0BpsSent => new(_haContext, "sensor.eugene_desktop_network_0_bps_sent");
		///<summary>EUGENE-DESKTOP Network #0 - Bytes Received</summary>
		public NumericSensorEntity EugeneDesktopNetwork0BytesReceived => new(_haContext, "sensor.eugene_desktop_network_0_bytes_received");
		///<summary>EUGENE-DESKTOP Network #0 - Bytes Sent</summary>
		public NumericSensorEntity EugeneDesktopNetwork0BytesSent => new(_haContext, "sensor.eugene_desktop_network_0_bytes_sent");
		///<summary>EUGENE-DESKTOP Network #0 - Speed</summary>
		public NumericSensorEntity EugeneDesktopNetwork0Speed => new(_haContext, "sensor.eugene_desktop_network_0_speed");
		///<summary>EUGENE-DESKTOP Network #1 - BPS Received</summary>
		public NumericSensorEntity EugeneDesktopNetwork1BpsReceived => new(_haContext, "sensor.eugene_desktop_network_1_bps_received");
		///<summary>EUGENE-DESKTOP Network #1 - BPS Sent</summary>
		public NumericSensorEntity EugeneDesktopNetwork1BpsSent => new(_haContext, "sensor.eugene_desktop_network_1_bps_sent");
		///<summary>EUGENE-DESKTOP Network #1 - Bytes Received</summary>
		public NumericSensorEntity EugeneDesktopNetwork1BytesReceived => new(_haContext, "sensor.eugene_desktop_network_1_bytes_received");
		///<summary>EUGENE-DESKTOP Network #1 - Bytes Sent</summary>
		public NumericSensorEntity EugeneDesktopNetwork1BytesSent => new(_haContext, "sensor.eugene_desktop_network_1_bytes_sent");
		///<summary>EUGENE-DESKTOP Network #1 - Speed</summary>
		public NumericSensorEntity EugeneDesktopNetwork1Speed => new(_haContext, "sensor.eugene_desktop_network_1_speed");
		///<summary>EUGENE-DESKTOP RX</summary>
		public NumericSensorEntity EugeneDesktopRx => new(_haContext, "sensor.eugene_desktop_rx");
		///<summary>EUGENE-DESKTOP Storage C - Available Free Space</summary>
		public NumericSensorEntity EugeneDesktopStorageCAvailableFreeSpace => new(_haContext, "sensor.eugene_desktop_storage_c_available_free_space");
		///<summary>EUGENE-DESKTOP Storage C - Total Free Space</summary>
		public NumericSensorEntity EugeneDesktopStorageCTotalFreeSpace => new(_haContext, "sensor.eugene_desktop_storage_c_total_free_space");
		///<summary>EUGENE-DESKTOP Storage C - Total Storage</summary>
		public NumericSensorEntity EugeneDesktopStorageCTotalStorage => new(_haContext, "sensor.eugene_desktop_storage_c_total_storage");
		///<summary>EUGENE-DESKTOP Storage C - Usage</summary>
		public NumericSensorEntity EugeneDesktopStorageCUsage => new(_haContext, "sensor.eugene_desktop_storage_c_usage");
		///<summary>EUGENE-DESKTOP Storage C - Used Space</summary>
		public NumericSensorEntity EugeneDesktopStorageCUsedSpace => new(_haContext, "sensor.eugene_desktop_storage_c_used_space");
		///<summary>EUGENE-DESKTOP Storage D - Available Free Space</summary>
		public NumericSensorEntity EugeneDesktopStorageDAvailableFreeSpace => new(_haContext, "sensor.eugene_desktop_storage_d_available_free_space");
		///<summary>EUGENE-DESKTOP Storage D - Total Free Space</summary>
		public NumericSensorEntity EugeneDesktopStorageDTotalFreeSpace => new(_haContext, "sensor.eugene_desktop_storage_d_total_free_space");
		///<summary>EUGENE-DESKTOP Storage D - Total Storage</summary>
		public NumericSensorEntity EugeneDesktopStorageDTotalStorage => new(_haContext, "sensor.eugene_desktop_storage_d_total_storage");
		///<summary>EUGENE-DESKTOP Storage D - Usage</summary>
		public NumericSensorEntity EugeneDesktopStorageDUsage => new(_haContext, "sensor.eugene_desktop_storage_d_usage");
		///<summary>EUGENE-DESKTOP Storage D - Used Space</summary>
		public NumericSensorEntity EugeneDesktopStorageDUsedSpace => new(_haContext, "sensor.eugene_desktop_storage_d_used_space");
		///<summary>EUGENE-DESKTOP Storage E - Available Free Space</summary>
		public NumericSensorEntity EugeneDesktopStorageEAvailableFreeSpace => new(_haContext, "sensor.eugene_desktop_storage_e_available_free_space");
		///<summary>EUGENE-DESKTOP Storage E - Total Free Space</summary>
		public NumericSensorEntity EugeneDesktopStorageETotalFreeSpace => new(_haContext, "sensor.eugene_desktop_storage_e_total_free_space");
		///<summary>EUGENE-DESKTOP Storage E - Total Storage</summary>
		public NumericSensorEntity EugeneDesktopStorageETotalStorage => new(_haContext, "sensor.eugene_desktop_storage_e_total_storage");
		///<summary>EUGENE-DESKTOP Storage E - Usage</summary>
		public NumericSensorEntity EugeneDesktopStorageEUsage => new(_haContext, "sensor.eugene_desktop_storage_e_usage");
		///<summary>EUGENE-DESKTOP Storage E - Used Space</summary>
		public NumericSensorEntity EugeneDesktopStorageEUsedSpace => new(_haContext, "sensor.eugene_desktop_storage_e_used_space");
		///<summary>EUGENE-DESKTOP System Idle Time</summary>
		public NumericSensorEntity EugeneDesktopSystemIdleTime => new(_haContext, "sensor.eugene_desktop_system_idle_time");
		///<summary>EUGENE-DESKTOP TX</summary>
		public NumericSensorEntity EugeneDesktopTx => new(_haContext, "sensor.eugene_desktop_tx");
		///<summary>Eugene’s iPhone Average Active Pace</summary>
		public NumericSensorEntity EugenesIphoneAverageActivePace => new(_haContext, "sensor.eugenes_iphone_average_active_pace");
		///<summary>Eugene’s iPhone Battery Level</summary>
		public NumericSensorEntity EugenesIphoneBatteryLevel => new(_haContext, "sensor.eugenes_iphone_battery_level");
		///<summary>Eugene’s iPhone Distance</summary>
		public NumericSensorEntity EugenesIphoneDistance => new(_haContext, "sensor.eugenes_iphone_distance");
		///<summary>Eugene’s iPhone Floors Ascended</summary>
		public NumericSensorEntity EugenesIphoneFloorsAscended => new(_haContext, "sensor.eugenes_iphone_floors_ascended");
		///<summary>Eugene’s iPhone Floors Descended</summary>
		public NumericSensorEntity EugenesIphoneFloorsDescended => new(_haContext, "sensor.eugenes_iphone_floors_descended");
		///<summary>Eugenes-iPhone RX</summary>
		public NumericSensorEntity EugenesIphoneRx => new(_haContext, "sensor.eugenes_iphone_rx");
		///<summary>Eugene’s iPhone Steps</summary>
		public NumericSensorEntity EugenesIphoneSteps => new(_haContext, "sensor.eugenes_iphone_steps");
		///<summary>Eugene’s iPhone Storage</summary>
		public NumericSensorEntity EugenesIphoneStorage => new(_haContext, "sensor.eugenes_iphone_storage");
		///<summary>Eugenes-iPhone TX</summary>
		public NumericSensorEntity EugenesIphoneTx => new(_haContext, "sensor.eugenes_iphone_tx");
		///<summary>EugenespleWatch RX</summary>
		public NumericSensorEntity EugenesplewatchRx => new(_haContext, "sensor.eugenesplewatch_rx");
		///<summary>EugenespleWatch TX</summary>
		public NumericSensorEntity EugenesplewatchTx => new(_haContext, "sensor.eugenesplewatch_tx");
		///<summary>floor_light-2086 RX</summary>
		public NumericSensorEntity FloorLight2086Rx => new(_haContext, "sensor.floor_light_2086_rx");
		///<summary>floor_light-2086 TX</summary>
		public NumericSensorEntity FloorLight2086Tx => new(_haContext, "sensor.floor_light_2086_tx");
		///<summary>Foscam RX</summary>
		public NumericSensorEntity FoscamRx => new(_haContext, "sensor.foscam_rx");
		///<summary>Foscam TX</summary>
		public NumericSensorEntity FoscamTx => new(_haContext, "sensor.foscam_tx");
		///<summary>Galaxy-S8 RX</summary>
		public NumericSensorEntity GalaxyS8Rx => new(_haContext, "sensor.galaxy_s8_rx");
		///<summary>Galaxy-S8 TX</summary>
		public NumericSensorEntity GalaxyS8Tx => new(_haContext, "sensor.galaxy_s8_tx");
		///<summary>Garage Echo RX</summary>
		public NumericSensorEntity GarageEchoRx => new(_haContext, "sensor.garage_echo_rx");
		///<summary>Garage Echo TX</summary>
		public NumericSensorEntity GarageEchoTx => new(_haContext, "sensor.garage_echo_tx");
		///<summary>Garden Battery</summary>
		public NumericSensorEntity GardenBattery2 => new(_haContext, "sensor.garden_battery_2");
		///<summary>Garden Floodlights RX</summary>
		public NumericSensorEntity GardenFloodlightsRx => new(_haContext, "sensor.garden_floodlights_rx");
		///<summary>Garden Floodlights TX</summary>
		public NumericSensorEntity GardenFloodlightsTx => new(_haContext, "sensor.garden_floodlights_tx");
		///<summary>Gas Consumption (Today)</summary>
		public NumericSensorEntity GasConsumptionToday => new(_haContext, "sensor.gas_consumption_today");
		///<summary>Gas Consumption (Year)</summary>
		public NumericSensorEntity GasConsumptionYear => new(_haContext, "sensor.gas_consumption_year");
		///<summary>Gas Cost (Today)</summary>
		public NumericSensorEntity GasCostToday => new(_haContext, "sensor.gas_cost_today");
		///<summary>Gas Tariff Rate</summary>
		public NumericSensorEntity GasTariffRate => new(_haContext, "sensor.gas_tariff_rate");
		///<summary>Gas Tariff Standing</summary>
		public NumericSensorEntity GasTariffStanding => new(_haContext, "sensor.gas_tariff_standing");
		///<summary>hacs</summary>
		public NumericSensorEntity Hacs => new(_haContext, "sensor.hacs");
		///<summary>Hailey's iPhone Average Active Pace</summary>
		public NumericSensorEntity HaileySIphoneAverageActivePace => new(_haContext, "sensor.hailey_s_iphone_average_active_pace");
		///<summary>Hailey's iPhone Battery Level</summary>
		public NumericSensorEntity HaileySIphoneBatteryLevel => new(_haContext, "sensor.hailey_s_iphone_battery_level");
		///<summary>Hailey's iPhone Distance</summary>
		public NumericSensorEntity HaileySIphoneDistance => new(_haContext, "sensor.hailey_s_iphone_distance");
		///<summary>Hailey's iPhone Floors Ascended</summary>
		public NumericSensorEntity HaileySIphoneFloorsAscended => new(_haContext, "sensor.hailey_s_iphone_floors_ascended");
		///<summary>Hailey's iPhone Floors Descended</summary>
		public NumericSensorEntity HaileySIphoneFloorsDescended => new(_haContext, "sensor.hailey_s_iphone_floors_descended");
		///<summary>Hailey's iPhone Steps</summary>
		public NumericSensorEntity HaileySIphoneSteps => new(_haContext, "sensor.hailey_s_iphone_steps");
		///<summary>Hailey's iPhone Storage</summary>
		public NumericSensorEntity HaileySIphoneStorage => new(_haContext, "sensor.hailey_s_iphone_storage");
		///<summary>Haileys-Air RX</summary>
		public NumericSensorEntity HaileysAirRx => new(_haContext, "sensor.haileys_air_rx");
		///<summary>Haileys-Air TX</summary>
		public NumericSensorEntity HaileysAirTx => new(_haContext, "sensor.haileys_air_tx");
		///<summary>Haileys-iPhone RX</summary>
		public NumericSensorEntity HaileysIphoneRx => new(_haContext, "sensor.haileys_iphone_rx");
		///<summary>Haileys-iPhone RX</summary>
		public NumericSensorEntity HaileysIphoneRx2 => new(_haContext, "sensor.haileys_iphone_rx_2");
		///<summary>Haileys-iPhone TX</summary>
		public NumericSensorEntity HaileysIphoneTx => new(_haContext, "sensor.haileys_iphone_tx");
		///<summary>Haileys-iPhone TX</summary>
		public NumericSensorEntity HaileysIphoneTx2 => new(_haContext, "sensor.haileys_iphone_tx_2");
		///<summary>Hailey’s MacBook Air Internal Battery Level</summary>
		public NumericSensorEntity HaileysMacbookAirInternalBatteryLevel => new(_haContext, "sensor.haileys_macbook_air_internal_battery_level");
		///<summary>Hailey’s MacBook Air Storage</summary>
		public NumericSensorEntity HaileysMacbookAirStorage => new(_haContext, "sensor.haileys_macbook_air_storage");
		///<summary>HUAWEI_P_smart_2019-86203 RX</summary>
		public NumericSensorEntity HuaweiPSmart201986203Rx => new(_haContext, "sensor.huawei_p_smart_2019_86203_rx");
		///<summary>HUAWEI_P_smart_2019-86203 TX</summary>
		public NumericSensorEntity HuaweiPSmart201986203Tx => new(_haContext, "sensor.huawei_p_smart_2019_86203_tx");
		///<summary>IKEA of Sweden TRADFRI remote control 580e51fe power</summary>
		public NumericSensorEntity IkeaOfSwedenTradfriRemoteControl580e51fePower => new(_haContext, "sensor.ikea_of_sweden_tradfri_remote_control_580e51fe_power");
		///<summary>IKEA of Sweden TRADFRI remote control d73648fe power</summary>
		public NumericSensorEntity IkeaOfSwedenTradfriRemoteControlD73648fePower => new(_haContext, "sensor.ikea_of_sweden_tradfri_remote_control_d73648fe_power");
		///<summary>iPad RX</summary>
		public NumericSensorEntity IpadRx => new(_haContext, "sensor.ipad_rx");
		///<summary>iPad TX</summary>
		public NumericSensorEntity IpadTx => new(_haContext, "sensor.ipad_tx");
		///<summary>Jayden AppleTv RX</summary>
		public NumericSensorEntity JaydenAppletvRx => new(_haContext, "sensor.jayden_appletv_rx");
		///<summary>Jayden AppleTv TX</summary>
		public NumericSensorEntity JaydenAppletvTx => new(_haContext, "sensor.jayden_appletv_tx");
		///<summary>jayden_bedside-4734 RX</summary>
		public NumericSensorEntity JaydenBedside4734Rx => new(_haContext, "sensor.jayden_bedside_4734_rx");
		///<summary>jayden_bedside-4734 TX</summary>
		public NumericSensorEntity JaydenBedside4734Tx => new(_haContext, "sensor.jayden_bedside_4734_tx");
		///<summary>Jayden Echo RX</summary>
		public NumericSensorEntity JaydenEchoRx => new(_haContext, "sensor.jayden_echo_rx");
		///<summary>Jayden Echo TX</summary>
		public NumericSensorEntity JaydenEchoTx => new(_haContext, "sensor.jayden_echo_tx");
		///<summary>Jayden Lux</summary>
		public NumericSensorEntity JaydenLux => new(_haContext, "sensor.jayden_lux");
		///<summary>Jayden Motion Battery</summary>
		public NumericSensorEntity JaydenMotionBattery => new(_haContext, "sensor.jayden_motion_battery");
		///<summary>Jayden RX</summary>
		public NumericSensorEntity JaydenRx => new(_haContext, "sensor.jayden_rx");
		///<summary>Jayden TX</summary>
		public NumericSensorEntity JaydenTx => new(_haContext, "sensor.jayden_tx");
		///<summary>Johan Front Door Battery</summary>
		public NumericSensorEntity JohanFrontDoorBattery => new(_haContext, "sensor.johan_front_door_battery");
		///<summary>IKEA of Sweden TRADFRI open/close remote 3dcb2efe power</summary>
		public NumericSensorEntity KeTradfriOpenCloseRemote3dcb2efePower => new(_haContext, "sensor.ke_tradfri_open_close_remote_3dcb2efe_power");
		///<summary>Kitchen Echo RX</summary>
		public NumericSensorEntity KitchenEchoRx => new(_haContext, "sensor.kitchen_echo_rx");
		///<summary>Kitchen Echo TX</summary>
		public NumericSensorEntity KitchenEchoTx => new(_haContext, "sensor.kitchen_echo_tx");
		///<summary>Kitchen Lux</summary>
		public NumericSensorEntity KitchenLux => new(_haContext, "sensor.kitchen_lux");
		///<summary>Kitchen Motion Battery</summary>
		public NumericSensorEntity KitchenMotionBattery => new(_haContext, "sensor.kitchen_motion_battery");
		///<summary>Konnected AddOn RX</summary>
		public NumericSensorEntity KonnectedAddonRx => new(_haContext, "sensor.konnected_addon_rx");
		///<summary>Konnected AddOn TX</summary>
		public NumericSensorEntity KonnectedAddonTx => new(_haContext, "sensor.konnected_addon_tx");
		///<summary>Konnected Main RX</summary>
		public NumericSensorEntity KonnectedMainRx => new(_haContext, "sensor.konnected_main_rx");
		///<summary>Konnected Main TX</summary>
		public NumericSensorEntity KonnectedMainTx => new(_haContext, "sensor.konnected_main_tx");
		///<summary>Landing Blind Battery</summary>
		public NumericSensorEntity LandingBlindBattery => new(_haContext, "sensor.landing_blind_battery");
		///<summary>Landing Blind Link</summary>
		public NumericSensorEntity LandingBlindLink => new(_haContext, "sensor.landing_blind_link");
		///<summary>landing blind power</summary>
		public NumericSensorEntity LandingBlindPower => new(_haContext, "sensor.landing_blind_power");
		///<summary>landing motion illuminance</summary>
		public NumericSensorEntity LandingLux => new(_haContext, "sensor.landing_lux");
		///<summary>landing motion power</summary>
		public NumericSensorEntity LandingMotionBattery => new(_haContext, "sensor.landing_motion_battery");
		///<summary>Landing RX</summary>
		public NumericSensorEntity LandingRx => new(_haContext, "sensor.landing_rx");
		///<summary>Landing TX</summary>
		public NumericSensorEntity LandingTx => new(_haContext, "sensor.landing_tx");
		///<summary>LG Lounge RX</summary>
		public NumericSensorEntity LgLoungeRx => new(_haContext, "sensor.lg_lounge_rx");
		///<summary>LG Lounge TX</summary>
		public NumericSensorEntity LgLoungeTx => new(_haContext, "sensor.lg_lounge_tx");
		///<summary>Living-Room RX</summary>
		public NumericSensorEntity LivingRoomRx => new(_haContext, "sensor.living_room_rx");
		///<summary>Living-Room TX</summary>
		public NumericSensorEntity LivingRoomTx => new(_haContext, "sensor.living_room_tx");
		///<summary>Lounge AC RX</summary>
		public NumericSensorEntity LoungeAcRx => new(_haContext, "sensor.lounge_ac_rx");
		///<summary>Lounge AC TX</summary>
		public NumericSensorEntity LoungeAcTx => new(_haContext, "sensor.lounge_ac_tx");
		///<summary>Lounge Blind RX</summary>
		public NumericSensorEntity LoungeBlindRx => new(_haContext, "sensor.lounge_blind_rx");
		///<summary>Lounge Blind TX</summary>
		public NumericSensorEntity LoungeBlindTx => new(_haContext, "sensor.lounge_blind_tx");
		///<summary>Lounge Echo RX</summary>
		public NumericSensorEntity LoungeEchoRx => new(_haContext, "sensor.lounge_echo_rx");
		///<summary>Lounge Echo TX</summary>
		public NumericSensorEntity LoungeEchoTx => new(_haContext, "sensor.lounge_echo_tx");
		///<summary>Lounge Energy</summary>
		public NumericSensorEntity LoungeEnergy => new(_haContext, "sensor.lounge_energy");
		///<summary>Lounge Lux</summary>
		public NumericSensorEntity LoungeLux => new(_haContext, "sensor.lounge_lux");
		///<summary>LUMI lumi.sensor_motion.aq2 7dce1303 power</summary>
		public NumericSensorEntity LoungeMotionBattery => new(_haContext, "sensor.lounge_motion_battery");
		///<summary>Lounge Room Temperature</summary>
		public NumericSensorEntity LoungeRoomTemperature => new(_haContext, "sensor.lounge_room_temperature");
		///<summary>LUMI lumi.sensor_magnet.aq2 38f0ec02 device_temperature</summary>
		public NumericSensorEntity LumiLumiSensorMagnetAq238f0ec02DeviceTemperature => new(_haContext, "sensor.lumi_lumi_sensor_magnet_aq2_38f0ec02_device_temperature");
		///<summary>LUMI lumi.sensor_magnet.aq2 38f0ec02 power</summary>
		public NumericSensorEntity LumiLumiSensorMagnetAq238f0ec02Power => new(_haContext, "sensor.lumi_lumi_sensor_magnet_aq2_38f0ec02_power");
		///<summary>LUMI lumi.sensor_magnet.aq2 56141203 device_temperature</summary>
		public NumericSensorEntity LumiLumiSensorMagnetAq256141203DeviceTemperature => new(_haContext, "sensor.lumi_lumi_sensor_magnet_aq2_56141203_device_temperature");
		///<summary>LUMI lumi.sensor_magnet.aq2 56141203 power</summary>
		public NumericSensorEntity LumiLumiSensorMagnetAq256141203Power => new(_haContext, "sensor.lumi_lumi_sensor_magnet_aq2_56141203_power");
		///<summary>LUMI lumi.sensor_magnet.aq2 83903a03 device_temperature</summary>
		public NumericSensorEntity LumiLumiSensorMagnetAq283903a03DeviceTemperature => new(_haContext, "sensor.lumi_lumi_sensor_magnet_aq2_83903a03_device_temperature");
		///<summary>LUMI lumi.sensor_magnet.aq2 83903a03 power</summary>
		public NumericSensorEntity LumiLumiSensorMagnetAq283903a03Power => new(_haContext, "sensor.lumi_lumi_sensor_magnet_aq2_83903a03_power");
		///<summary>LUMI lumi.sensor_magnet.aq2 8c913a03 power</summary>
		public NumericSensorEntity LumiLumiSensorMagnetAq28c913a03Power => new(_haContext, "sensor.lumi_lumi_sensor_magnet_aq2_8c913a03_power");
		///<summary>LUMI lumi.sensor_magnet.aq2 9e0b1203 device_temperature</summary>
		public NumericSensorEntity LumiLumiSensorMagnetAq29e0b1203DeviceTemperature => new(_haContext, "sensor.lumi_lumi_sensor_magnet_aq2_9e0b1203_device_temperature");
		///<summary>LUMI lumi.sensor_magnet.aq2 9e0b1203 power</summary>
		public NumericSensorEntity LumiLumiSensorMagnetAq29e0b1203Power => new(_haContext, "sensor.lumi_lumi_sensor_magnet_aq2_9e0b1203_power");
		///<summary>LUMI lumi.sensor_magnet.aq2 ac831303 device_temperature</summary>
		public NumericSensorEntity LumiLumiSensorMagnetAq2Ac831303DeviceTemperature => new(_haContext, "sensor.lumi_lumi_sensor_magnet_aq2_ac831303_device_temperature");
		///<summary>LUMI lumi.sensor_magnet.aq2 e6b02103 device_temperature</summary>
		public NumericSensorEntity LumiLumiSensorMagnetAq2E6b02103DeviceTemperature => new(_haContext, "sensor.lumi_lumi_sensor_magnet_aq2_e6b02103_device_temperature");
		///<summary>LUMI lumi.sensor_magnet.aq2 e6b02103 power</summary>
		public NumericSensorEntity LumiLumiSensorMagnetAq2E6b02103Power => new(_haContext, "sensor.lumi_lumi_sensor_magnet_aq2_e6b02103_power");
		///<summary>Officer Contact  power</summary>
		public NumericSensorEntity LumiLumiSensorMagnetAq2Power => new(_haContext, "sensor.lumi_lumi_sensor_magnet_aq2_power");
		///<summary>LUMI lumi.sensor_motion.aq2 34796603 device_temperature</summary>
		public NumericSensorEntity LumiLumiSensorMotionAq234796603DeviceTemperature => new(_haContext, "sensor.lumi_lumi_sensor_motion_aq2_34796603_device_temperature");
		///<summary>LUMI lumi.sensor_motion.aq2 3ca2f202 device_temperature</summary>
		public NumericSensorEntity LumiLumiSensorMotionAq23ca2f202DeviceTemperature => new(_haContext, "sensor.lumi_lumi_sensor_motion_aq2_3ca2f202_device_temperature");
		///<summary>LUMI lumi.sensor_motion.aq2 4123f403 device_temperature</summary>
		public NumericSensorEntity LumiLumiSensorMotionAq24123f403DeviceTemperature => new(_haContext, "sensor.lumi_lumi_sensor_motion_aq2_4123f403_device_temperature");
		///<summary>LUMI lumi.sensor_motion.aq2 54c2f302 device_temperature</summary>
		public NumericSensorEntity LumiLumiSensorMotionAq254c2f302DeviceTemperature => new(_haContext, "sensor.lumi_lumi_sensor_motion_aq2_54c2f302_device_temperature");
		///<summary>LUMI lumi.sensor_motion.aq2 591d1b03 device_temperature</summary>
		public NumericSensorEntity LumiLumiSensorMotionAq2591d1b03DeviceTemperature => new(_haContext, "sensor.lumi_lumi_sensor_motion_aq2_591d1b03_device_temperature");
		///<summary>LUMI lumi.sensor_motion.aq2 5cf75702 device_temperature</summary>
		public NumericSensorEntity LumiLumiSensorMotionAq25cf75702DeviceTemperature => new(_haContext, "sensor.lumi_lumi_sensor_motion_aq2_5cf75702_device_temperature");
		///<summary>LUMI lumi.sensor_motion.aq2 7dce1303 device_temperature</summary>
		public NumericSensorEntity LumiLumiSensorMotionAq27dce1303DeviceTemperature => new(_haContext, "sensor.lumi_lumi_sensor_motion_aq2_7dce1303_device_temperature");
		///<summary>LUMI lumi.sensor_motion.aq2 97a7f202 device_temperature</summary>
		public NumericSensorEntity LumiLumiSensorMotionAq297a7f202DeviceTemperature => new(_haContext, "sensor.lumi_lumi_sensor_motion_aq2_97a7f202_device_temperature");
		///<summary>LUMI lumi.sensor_motion.aq2 b4796603 device_temperature</summary>
		public NumericSensorEntity LumiLumiSensorMotionAq2B4796603DeviceTemperature => new(_haContext, "sensor.lumi_lumi_sensor_motion_aq2_b4796603_device_temperature");
		///<summary>LUMI lumi.sensor_motion.aq2 c0a6f202 device_temperature</summary>
		public NumericSensorEntity LumiLumiSensorMotionAq2C0a6f202DeviceTemperature => new(_haContext, "sensor.lumi_lumi_sensor_motion_aq2_c0a6f202_device_temperature");
		///<summary>LUMI lumi.sensor_motion.aq2 ea1a1404 device_temperature</summary>
		public NumericSensorEntity LumiLumiSensorMotionAq2Ea1a1404DeviceTemperature => new(_haContext, "sensor.lumi_lumi_sensor_motion_aq2_ea1a1404_device_temperature");
		///<summary>LUMI lumi.sensor_motion.aq2 ef2f1404 device_temperature</summary>
		public NumericSensorEntity LumiLumiSensorMotionAq2Ef2f1404DeviceTemperature => new(_haContext, "sensor.lumi_lumi_sensor_motion_aq2_ef2f1404_device_temperature");
		///<summary>LUMI lumi.sensor_motion.aq2 f33b1404 device_temperature</summary>
		public NumericSensorEntity LumiLumiSensorMotionAq2F33b1404DeviceTemperature => new(_haContext, "sensor.lumi_lumi_sensor_motion_aq2_f33b1404_device_temperature");
		///<summary>Master Echo RX</summary>
		public NumericSensorEntity MasterEchoRx => new(_haContext, "sensor.master_echo_rx");
		///<summary>Master Echo TX</summary>
		public NumericSensorEntity MasterEchoTx => new(_haContext, "sensor.master_echo_tx");
		///<summary>Master Lux</summary>
		public NumericSensorEntity MasterLux => new(_haContext, "sensor.master_lux");
		///<summary>Master Motion Battery</summary>
		public NumericSensorEntity MasterMotionBattery => new(_haContext, "sensor.master_motion_battery");
		///<summary>Master Tele RX</summary>
		public NumericSensorEntity MasterTeleRx => new(_haContext, "sensor.master_tele_rx");
		///<summary>Master Tele TX</summary>
		public NumericSensorEntity MasterTeleTx => new(_haContext, "sensor.master_tele_tx");
		///<summary>My Wall Panel Battery Level</summary>
		public NumericSensorEntity MyWallPanelBatteryLevel => new(_haContext, "sensor.my_wall_panel_battery_level");
		///<summary>My Wall Panel Light</summary>
		public NumericSensorEntity MyWallPanelLight => new(_haContext, "sensor.my_wall_panel_light");
		///<summary>Niemand Drive Battery</summary>
		public NumericSensorEntity NiemandDriveBattery => new(_haContext, "sensor.niemand_drive_battery");
		///<summary>Niemand Drive Wireless</summary>
		public NumericSensorEntity NiemandDriveWireless => new(_haContext, "sensor.niemand_drive_wireless");
		///<summary>Niemand Front Door Battery</summary>
		public NumericSensorEntity NiemandFrontDoorBattery => new(_haContext, "sensor.niemand_front_door_battery");
		///<summary>Niemand Front Door Wireless</summary>
		public NumericSensorEntity NiemandFrontDoorWireless => new(_haContext, "sensor.niemand_front_door_wireless");
		///<summary>Niemand Garage Battery</summary>
		public NumericSensorEntity NiemandGarageBattery => new(_haContext, "sensor.niemand_garage_battery");
		///<summary>Niemand Garage Battery</summary>
		public NumericSensorEntity NiemandGarageBattery2 => new(_haContext, "sensor.niemand_garage_battery_2");
		///<summary>Niemand Garage Wireless</summary>
		public NumericSensorEntity NiemandGarageWireless => new(_haContext, "sensor.niemand_garage_wireless");
		///<summary>Niemand Garden Battery</summary>
		public NumericSensorEntity NiemandGardenBattery => new(_haContext, "sensor.niemand_garden_battery");
		///<summary>Niemand Garden Wireless</summary>
		public NumericSensorEntity NiemandGardenWireless => new(_haContext, "sensor.niemand_garden_wireless");
		///<summary>Niemand Side Battery</summary>
		public NumericSensorEntity NiemandSideBattery => new(_haContext, "sensor.niemand_side_battery");
		///<summary>Niemand Side Battery</summary>
		public NumericSensorEntity NiemandSideBattery2 => new(_haContext, "sensor.niemand_side_battery_2");
		///<summary>Niemand Side Wireless</summary>
		public NumericSensorEntity NiemandSideWireless => new(_haContext, "sensor.niemand_side_wireless");
		///<summary>Octopus Agile Current Rate</summary>
		public NumericSensorEntity OctopusAgileCurrentRate => new(_haContext, "sensor.octopus_agile_current_rate");
		///<summary>Octopus Agile Minimum Rate</summary>
		public NumericSensorEntity OctopusAgileMinRate => new(_haContext, "sensor.octopus_agile_min_rate");
		///<summary>Octopus Agile Next Rate</summary>
		public NumericSensorEntity OctopusAgileNextRate => new(_haContext, "sensor.octopus_agile_next_rate");
		///<summary>Octopus Agile Previous Rate</summary>
		public NumericSensorEntity OctopusAgilePreviousRate => new(_haContext, "sensor.octopus_agile_previous_rate");
		///<summary>Office AC RX</summary>
		public NumericSensorEntity OfficeAcRx => new(_haContext, "sensor.office_ac_rx");
		///<summary>Office AC TX</summary>
		public NumericSensorEntity OfficeAcTx => new(_haContext, "sensor.office_ac_tx");
		///<summary>Office Echo RX</summary>
		public NumericSensorEntity OfficeEchoRx => new(_haContext, "sensor.office_echo_rx");
		///<summary>Office Echo TX</summary>
		public NumericSensorEntity OfficeEchoTx => new(_haContext, "sensor.office_echo_tx");
		///<summary>Office Energy</summary>
		public NumericSensorEntity OfficeEnergy => new(_haContext, "sensor.office_energy");
		///<summary>Office Motion Lux</summary>
		public NumericSensorEntity OfficeLux => new(_haContext, "sensor.office_lux");
		///<summary>Office Motion Battery</summary>
		public NumericSensorEntity OfficeMotionBattery => new(_haContext, "sensor.office_motion_battery");
		///<summary>Office Room Temperature</summary>
		public NumericSensorEntity OfficeRoomTemperature => new(_haContext, "sensor.office_room_temperature");
		///<summary>Outside Drive RX</summary>
		public NumericSensorEntity OutsideDriveRx => new(_haContext, "sensor.outside_drive_rx");
		///<summary>Outside Drive TX</summary>
		public NumericSensorEntity OutsideDriveTx => new(_haContext, "sensor.outside_drive_tx");
		///<summary>Outside Garage RX</summary>
		public NumericSensorEntity OutsideGarageRx => new(_haContext, "sensor.outside_garage_rx");
		///<summary>Outside Garage TX</summary>
		public NumericSensorEntity OutsideGarageTx => new(_haContext, "sensor.outside_garage_tx");
		///<summary>Pi-Hole Ads Blocked Today</summary>
		public NumericSensorEntity PiHoleAdsBlockedToday => new(_haContext, "sensor.pi_hole_ads_blocked_today");
		///<summary>Pi-Hole Ads Percentage Blocked Today</summary>
		public NumericSensorEntity PiHoleAdsPercentageBlockedToday => new(_haContext, "sensor.pi_hole_ads_percentage_blocked_today");
		///<summary>Pi-Hole DNS Queries Cached</summary>
		public NumericSensorEntity PiHoleDnsQueriesCached => new(_haContext, "sensor.pi_hole_dns_queries_cached");
		///<summary>Pi-Hole DNS Queries Forwarded</summary>
		public NumericSensorEntity PiHoleDnsQueriesForwarded => new(_haContext, "sensor.pi_hole_dns_queries_forwarded");
		///<summary>Pi-Hole DNS Queries Today</summary>
		public NumericSensorEntity PiHoleDnsQueriesToday => new(_haContext, "sensor.pi_hole_dns_queries_today");
		///<summary>Pi-Hole DNS Unique Clients</summary>
		public NumericSensorEntity PiHoleDnsUniqueClients => new(_haContext, "sensor.pi_hole_dns_unique_clients");
		///<summary>Pi-Hole DNS Unique Domains</summary>
		public NumericSensorEntity PiHoleDnsUniqueDomains => new(_haContext, "sensor.pi_hole_dns_unique_domains");
		///<summary>Pi-Hole Domains Blocked</summary>
		public NumericSensorEntity PiHoleDomainsBlocked => new(_haContext, "sensor.pi_hole_domains_blocked");
		///<summary>Pi-Hole Seen Clients</summary>
		public NumericSensorEntity PiHoleSeenClients => new(_haContext, "sensor.pi_hole_seen_clients");
		///<summary>Playroom Echo RX</summary>
		public NumericSensorEntity PlayroomEchoRx => new(_haContext, "sensor.playroom_echo_rx");
		///<summary>Playroom Echo TX</summary>
		public NumericSensorEntity PlayroomEchoTx => new(_haContext, "sensor.playroom_echo_tx");
		///<summary>Playroom Lux</summary>
		public NumericSensorEntity PlayroomLux => new(_haContext, "sensor.playroom_lux");
		///<summary>Playroom Motion Battery</summary>
		public NumericSensorEntity PlayroomMotionBattery => new(_haContext, "sensor.playroom_motion_battery");
		///<summary>Plug 1 Current</summary>
		public NumericSensorEntity Plug1Current => new(_haContext, "sensor.plug_1_current");
		///<summary>Plug 1 Energy</summary>
		public NumericSensorEntity Plug1Energy => new(_haContext, "sensor.plug_1_energy");
		///<summary>Plug 1 Voltage</summary>
		public NumericSensorEntity Plug1Voltage => new(_haContext, "sensor.plug_1_voltage");
		///<summary>Plug 2 Current</summary>
		public NumericSensorEntity Plug2Current => new(_haContext, "sensor.plug_2_current");
		///<summary>Plug 2 Energy</summary>
		public NumericSensorEntity Plug2Energy => new(_haContext, "sensor.plug_2_energy");
		///<summary>Plug 2 Voltage</summary>
		public NumericSensorEntity Plug2Voltage => new(_haContext, "sensor.plug_2_voltage");
		///<summary>Plug 3 Current</summary>
		public NumericSensorEntity Plug3Current => new(_haContext, "sensor.plug_3_current");
		///<summary>Plug 3 Energy</summary>
		public NumericSensorEntity Plug3Energy => new(_haContext, "sensor.plug_3_energy");
		///<summary>Plug 3 Voltage</summary>
		public NumericSensorEntity Plug3Voltage => new(_haContext, "sensor.plug_3_voltage");
		///<summary>Plug 4 Current</summary>
		public NumericSensorEntity Plug4Current => new(_haContext, "sensor.plug_4_current");
		///<summary>Plug 4 Energy</summary>
		public NumericSensorEntity Plug4Energy => new(_haContext, "sensor.plug_4_energy");
		///<summary>plug_4_energy_stats</summary>
		public NumericSensorEntity Plug4EnergyStats => new(_haContext, "sensor.plug_4_energy_stats");
		///<summary>Plug 4 Voltage</summary>
		public NumericSensorEntity Plug4Voltage => new(_haContext, "sensor.plug_4_voltage");
		///<summary>Plug 5 Current</summary>
		public NumericSensorEntity Plug5Current => new(_haContext, "sensor.plug_5_current");
		///<summary>Plug 5 Energy</summary>
		public NumericSensorEntity Plug5Energy => new(_haContext, "sensor.plug_5_energy");
		///<summary>Plug 5 Voltage</summary>
		public NumericSensorEntity Plug5Voltage => new(_haContext, "sensor.plug_5_voltage");
		///<summary>Porch RX</summary>
		public NumericSensorEntity PorchRx => new(_haContext, "sensor.porch_rx");
		///<summary>Porch TX</summary>
		public NumericSensorEntity PorchTx => new(_haContext, "sensor.porch_tx");
		///<summary>RaspberryPi CUPS RX</summary>
		public NumericSensorEntity RaspberrypiCupsRx => new(_haContext, "sensor.raspberrypi_cups_rx");
		///<summary>RaspberryPi CUPS TX</summary>
		public NumericSensorEntity RaspberrypiCupsTx => new(_haContext, "sensor.raspberrypi_cups_tx");
		///<summary>raspberrypi RX</summary>
		public NumericSensorEntity RaspberrypiRx => new(_haContext, "sensor.raspberrypi_rx");
		///<summary>raspberrypi TX</summary>
		public NumericSensorEntity RaspberrypiTx => new(_haContext, "sensor.raspberrypi_tx");
		///<summary>Remote 1 Battery</summary>
		public NumericSensorEntity Remote1Battery => new(_haContext, "sensor.remote_1_battery");
		///<summary>Remote 1 Link</summary>
		public NumericSensorEntity Remote1Link => new(_haContext, "sensor.remote_1_link");
		///<summary>Remote 2 Battery</summary>
		public NumericSensorEntity Remote2Battery => new(_haContext, "sensor.remote_2_battery");
		///<summary>Remote 2 Link</summary>
		public NumericSensorEntity Remote2Link => new(_haContext, "sensor.remote_2_link");
		///<summary>Remote 3 Battery</summary>
		public NumericSensorEntity Remote3Battery => new(_haContext, "sensor.remote_3_battery");
		///<summary>Remote 3 Link</summary>
		public NumericSensorEntity Remote3Link => new(_haContext, "sensor.remote_3_link");
		///<summary>RingHpCam-49 RX</summary>
		public NumericSensorEntity Ringhpcam49Rx => new(_haContext, "sensor.ringhpcam_49_rx");
		///<summary>RingHpCam-49 TX</summary>
		public NumericSensorEntity Ringhpcam49Tx => new(_haContext, "sensor.ringhpcam_49_tx");
		///<summary>RingHpCam-4c RX</summary>
		public NumericSensorEntity Ringhpcam4cRx => new(_haContext, "sensor.ringhpcam_4c_rx");
		///<summary>RingHpCam-4c TX</summary>
		public NumericSensorEntity Ringhpcam4cTx => new(_haContext, "sensor.ringhpcam_4c_tx");
		///<summary>RingPro-d6 RX</summary>
		public NumericSensorEntity RingproD6Rx => new(_haContext, "sensor.ringpro_d6_rx");
		///<summary>RingPro-d6 TX</summary>
		public NumericSensorEntity RingproD6Tx => new(_haContext, "sensor.ringpro_d6_tx");
		///<summary>RingStickUpCam-94 RX</summary>
		public NumericSensorEntity Ringstickupcam94Rx => new(_haContext, "sensor.ringstickupcam_94_rx");
		///<summary>RingStickUpCam-94 TX</summary>
		public NumericSensorEntity Ringstickupcam94Tx => new(_haContext, "sensor.ringstickupcam_94_tx");
		///<summary>RingStickUpCam-9b RX</summary>
		public NumericSensorEntity Ringstickupcam9bRx => new(_haContext, "sensor.ringstickupcam_9b_rx");
		///<summary>RingStickUpCam-9b TX</summary>
		public NumericSensorEntity Ringstickupcam9bTx => new(_haContext, "sensor.ringstickupcam_9b_tx");
		///<summary>RMMINI-d9-2b-62 RX</summary>
		public NumericSensorEntity RmminiD92b62Rx => new(_haContext, "sensor.rmmini_d9_2b_62_rx");
		///<summary>RMMINI-d9-2b-62 TX</summary>
		public NumericSensorEntity RmminiD92b62Tx => new(_haContext, "sensor.rmmini_d9_2b_62_tx");
		///<summary> RX</summary>
		public NumericSensorEntity Rx => new(_haContext, "sensor.rx");
		///<summary> RX</summary>
		public NumericSensorEntity Rx10 => new(_haContext, "sensor.rx_10");
		///<summary> RX</summary>
		public NumericSensorEntity Rx11 => new(_haContext, "sensor.rx_11");
		///<summary> RX</summary>
		public NumericSensorEntity Rx12 => new(_haContext, "sensor.rx_12");
		///<summary> RX</summary>
		public NumericSensorEntity Rx13 => new(_haContext, "sensor.rx_13");
		///<summary> RX</summary>
		public NumericSensorEntity Rx14 => new(_haContext, "sensor.rx_14");
		///<summary> RX</summary>
		public NumericSensorEntity Rx15 => new(_haContext, "sensor.rx_15");
		///<summary>SonosZP RX</summary>
		public NumericSensorEntity Rx16 => new(_haContext, "sensor.rx_16");
		///<summary> RX</summary>
		public NumericSensorEntity Rx17 => new(_haContext, "sensor.rx_17");
		///<summary> RX</summary>
		public NumericSensorEntity Rx18 => new(_haContext, "sensor.rx_18");
		///<summary> RX</summary>
		public NumericSensorEntity Rx19 => new(_haContext, "sensor.rx_19");
		///<summary> RX</summary>
		public NumericSensorEntity Rx2 => new(_haContext, "sensor.rx_2");
		///<summary> RX</summary>
		public NumericSensorEntity Rx20 => new(_haContext, "sensor.rx_20");
		///<summary> RX</summary>
		public NumericSensorEntity Rx21 => new(_haContext, "sensor.rx_21");
		///<summary> RX</summary>
		public NumericSensorEntity Rx22 => new(_haContext, "sensor.rx_22");
		///<summary> RX</summary>
		public NumericSensorEntity Rx23 => new(_haContext, "sensor.rx_23");
		///<summary>Jayden iPad RX</summary>
		public NumericSensorEntity Rx24 => new(_haContext, "sensor.rx_24");
		///<summary> RX</summary>
		public NumericSensorEntity Rx25 => new(_haContext, "sensor.rx_25");
		///<summary> RX</summary>
		public NumericSensorEntity Rx26 => new(_haContext, "sensor.rx_26");
		///<summary> RX</summary>
		public NumericSensorEntity Rx27 => new(_haContext, "sensor.rx_27");
		///<summary> RX</summary>
		public NumericSensorEntity Rx28 => new(_haContext, "sensor.rx_28");
		///<summary> RX</summary>
		public NumericSensorEntity Rx3 => new(_haContext, "sensor.rx_3");
		///<summary> RX</summary>
		public NumericSensorEntity Rx4 => new(_haContext, "sensor.rx_4");
		///<summary> RX</summary>
		public NumericSensorEntity Rx5 => new(_haContext, "sensor.rx_5");
		///<summary> RX</summary>
		public NumericSensorEntity Rx6 => new(_haContext, "sensor.rx_6");
		///<summary> RX</summary>
		public NumericSensorEntity Rx7 => new(_haContext, "sensor.rx_7");
		///<summary> RX</summary>
		public NumericSensorEntity Rx8 => new(_haContext, "sensor.rx_8");
		///<summary>Jayden RaspberryPi RX</summary>
		public NumericSensorEntity Rx9 => new(_haContext, "sensor.rx_9");
		///<summary>Sammi-Leigh-s-A52 RX</summary>
		public NumericSensorEntity SammiLeighSA52Rx => new(_haContext, "sensor.sammi_leigh_s_a52_rx");
		///<summary>Sammi-Leigh-s-A52 TX</summary>
		public NumericSensorEntity SammiLeighSA52Tx => new(_haContext, "sensor.sammi_leigh_s_a52_tx");
		///<summary>shelly1-55E8B5 RX</summary>
		public NumericSensorEntity Shelly155e8b5Rx => new(_haContext, "sensor.shelly1_55e8b5_rx");
		///<summary>shelly1-55E8B5 TX</summary>
		public NumericSensorEntity Shelly155e8b5Tx => new(_haContext, "sensor.shelly1_55e8b5_tx");
		///<summary>shelly1-BA6C98 RX</summary>
		public NumericSensorEntity Shelly1Ba6c98Rx => new(_haContext, "sensor.shelly1_ba6c98_rx");
		///<summary>shelly1-BA6C98 TX</summary>
		public NumericSensorEntity Shelly1Ba6c98Tx => new(_haContext, "sensor.shelly1_ba6c98_tx");
		///<summary>shellyswitch25-E5A1D2 Power</summary>
		public NumericSensorEntity Shellyswitch25E5a1d2Power => new(_haContext, "sensor.shellyswitch25_e5a1d2_power");
		///<summary>smart-plug-1 RX</summary>
		public NumericSensorEntity SmartPlug1Rx => new(_haContext, "sensor.smart_plug_1_rx");
		///<summary>smart-plug-1 TX</summary>
		public NumericSensorEntity SmartPlug1Tx => new(_haContext, "sensor.smart_plug_1_tx");
		///<summary>smart-plug-2 RX</summary>
		public NumericSensorEntity SmartPlug2Rx => new(_haContext, "sensor.smart_plug_2_rx");
		///<summary>smart-plug-2 TX</summary>
		public NumericSensorEntity SmartPlug2Tx => new(_haContext, "sensor.smart_plug_2_tx");
		///<summary>smart-plug-4 RX</summary>
		public NumericSensorEntity SmartPlug4Rx => new(_haContext, "sensor.smart_plug_4_rx");
		///<summary>smart-plug-4 TX</summary>
		public NumericSensorEntity SmartPlug4Tx => new(_haContext, "sensor.smart_plug_4_tx");
		///<summary>SonosZP RX</summary>
		public NumericSensorEntity SonoszpRx => new(_haContext, "sensor.sonoszp_rx");
		///<summary>SonosZP RX</summary>
		public NumericSensorEntity SonoszpRx2 => new(_haContext, "sensor.sonoszp_rx_2");
		///<summary>SonosZP TX</summary>
		public NumericSensorEntity SonoszpTx => new(_haContext, "sensor.sonoszp_tx");
		///<summary>SonosZP TX</summary>
		public NumericSensorEntity SonoszpTx2 => new(_haContext, "sensor.sonoszp_tx_2");
		///<summary>SpeedTest Download</summary>
		public NumericSensorEntity SpeedtestDownload => new(_haContext, "sensor.speedtest_download");
		///<summary>SpeedTest Ping</summary>
		public NumericSensorEntity SpeedtestPing => new(_haContext, "sensor.speedtest_ping");
		///<summary>SpeedTest Upload</summary>
		public NumericSensorEntity SpeedtestUpload => new(_haContext, "sensor.speedtest_upload");
		///<summary>Sprinkler Left Energy</summary>
		public NumericSensorEntity SprinklerLeftEnergy => new(_haContext, "sensor.sprinkler_left_energy");
		///<summary>Sprinkler Left Power</summary>
		public NumericSensorEntity SprinklerLeftPower => new(_haContext, "sensor.sprinkler_left_power");
		///<summary>Sprinkler Right Energy</summary>
		public NumericSensorEntity SprinklerRightEnergy => new(_haContext, "sensor.sprinkler_right_energy");
		///<summary>Sprinkler Right Power</summary>
		public NumericSensorEntity SprinklerRightPower => new(_haContext, "sensor.sprinkler_right_power");
		///<summary>Suspect Device RX</summary>
		public NumericSensorEntity SuspectDeviceRx => new(_haContext, "sensor.suspect_device_rx");
		///<summary>Suspect Device TX</summary>
		public NumericSensorEntity SuspectDeviceTx => new(_haContext, "sensor.suspect_device_tx");
		///<summary>Suspect Huawei RX</summary>
		public NumericSensorEntity SuspectHuaweiRx => new(_haContext, "sensor.suspect_huawei_rx");
		///<summary>Suspect Huawei TX</summary>
		public NumericSensorEntity SuspectHuaweiTx => new(_haContext, "sensor.suspect_huawei_tx");
		///<summary>Toilet Lux</summary>
		public NumericSensorEntity ToiletLux => new(_haContext, "sensor.toilet_lux");
		///<summary>Toilet Motion Battery</summary>
		public NumericSensorEntity ToiletMotionBattery => new(_haContext, "sensor.toilet_motion_battery");
		///<summary>Tumble dryer deltaEnergy</summary>
		public NumericSensorEntity TumbleDryerDeltaenergy => new(_haContext, "sensor.tumble_dryer_deltaenergy");
		///<summary>Tumble dryer deltaEnergy</summary>
		public NumericSensorEntity TumbleDryerDeltaenergy2 => new(_haContext, "sensor.tumble_dryer_deltaenergy_2");
		///<summary>Tumble dryer Energy Meter</summary>
		public NumericSensorEntity TumbleDryerEnergy => new(_haContext, "sensor.tumble_dryer_energy");
		///<summary>Tumble dryer energy</summary>
		public NumericSensorEntity TumbleDryerEnergy2 => new(_haContext, "sensor.tumble_dryer_energy_2");
		///<summary>Tumble dryer energySaved</summary>
		public NumericSensorEntity TumbleDryerEnergysaved => new(_haContext, "sensor.tumble_dryer_energysaved");
		///<summary>Tumble dryer energySaved</summary>
		public NumericSensorEntity TumbleDryerEnergysaved2 => new(_haContext, "sensor.tumble_dryer_energysaved_2");
		///<summary>Tumble dryer Power Meter</summary>
		public NumericSensorEntity TumbleDryerPower => new(_haContext, "sensor.tumble_dryer_power");
		///<summary>Tumble dryer power</summary>
		public NumericSensorEntity TumbleDryerPower2 => new(_haContext, "sensor.tumble_dryer_power_2");
		///<summary>Tumble dryer powerEnergy</summary>
		public NumericSensorEntity TumbleDryerPowerenergy => new(_haContext, "sensor.tumble_dryer_powerenergy");
		///<summary>Tumble dryer powerEnergy</summary>
		public NumericSensorEntity TumbleDryerPowerenergy2 => new(_haContext, "sensor.tumble_dryer_powerenergy_2");
		///<summary> TX</summary>
		public NumericSensorEntity Tx => new(_haContext, "sensor.tx");
		///<summary> TX</summary>
		public NumericSensorEntity Tx10 => new(_haContext, "sensor.tx_10");
		///<summary> TX</summary>
		public NumericSensorEntity Tx11 => new(_haContext, "sensor.tx_11");
		///<summary> TX</summary>
		public NumericSensorEntity Tx12 => new(_haContext, "sensor.tx_12");
		///<summary> TX</summary>
		public NumericSensorEntity Tx13 => new(_haContext, "sensor.tx_13");
		///<summary> TX</summary>
		public NumericSensorEntity Tx14 => new(_haContext, "sensor.tx_14");
		///<summary> TX</summary>
		public NumericSensorEntity Tx15 => new(_haContext, "sensor.tx_15");
		///<summary>SonosZP TX</summary>
		public NumericSensorEntity Tx16 => new(_haContext, "sensor.tx_16");
		///<summary> TX</summary>
		public NumericSensorEntity Tx17 => new(_haContext, "sensor.tx_17");
		///<summary> TX</summary>
		public NumericSensorEntity Tx18 => new(_haContext, "sensor.tx_18");
		///<summary> TX</summary>
		public NumericSensorEntity Tx19 => new(_haContext, "sensor.tx_19");
		///<summary> TX</summary>
		public NumericSensorEntity Tx2 => new(_haContext, "sensor.tx_2");
		///<summary> TX</summary>
		public NumericSensorEntity Tx20 => new(_haContext, "sensor.tx_20");
		///<summary> TX</summary>
		public NumericSensorEntity Tx21 => new(_haContext, "sensor.tx_21");
		///<summary> TX</summary>
		public NumericSensorEntity Tx22 => new(_haContext, "sensor.tx_22");
		///<summary> TX</summary>
		public NumericSensorEntity Tx23 => new(_haContext, "sensor.tx_23");
		///<summary>Jayden iPad TX</summary>
		public NumericSensorEntity Tx24 => new(_haContext, "sensor.tx_24");
		///<summary> TX</summary>
		public NumericSensorEntity Tx25 => new(_haContext, "sensor.tx_25");
		///<summary> TX</summary>
		public NumericSensorEntity Tx26 => new(_haContext, "sensor.tx_26");
		///<summary> TX</summary>
		public NumericSensorEntity Tx27 => new(_haContext, "sensor.tx_27");
		///<summary> TX</summary>
		public NumericSensorEntity Tx28 => new(_haContext, "sensor.tx_28");
		///<summary> TX</summary>
		public NumericSensorEntity Tx3 => new(_haContext, "sensor.tx_3");
		///<summary> TX</summary>
		public NumericSensorEntity Tx4 => new(_haContext, "sensor.tx_4");
		///<summary> TX</summary>
		public NumericSensorEntity Tx5 => new(_haContext, "sensor.tx_5");
		///<summary> TX</summary>
		public NumericSensorEntity Tx6 => new(_haContext, "sensor.tx_6");
		///<summary> TX</summary>
		public NumericSensorEntity Tx7 => new(_haContext, "sensor.tx_7");
		///<summary> TX</summary>
		public NumericSensorEntity Tx8 => new(_haContext, "sensor.tx_8");
		///<summary>Jayden RaspberryPi TX</summary>
		public NumericSensorEntity Tx9 => new(_haContext, "sensor.tx_9");
		///<summary>Tuya Socket 2</summary>
		public NumericSensorEntity Ty012047432cf4326b7081 => new(_haContext, "sensor.ty012047432cf4326b7081");
		///<summary>Tuya Socket 2</summary>
		public NumericSensorEntity Ty012047432cf4326b70812 => new(_haContext, "sensor.ty012047432cf4326b7081_2");
		///<summary>Tuya Socket 2</summary>
		public NumericSensorEntity Ty012047432cf4326b70813 => new(_haContext, "sensor.ty012047432cf4326b7081_3");
		///<summary>Tuya Socket 3</summary>
		public NumericSensorEntity Ty012047432cf4326b7a3a => new(_haContext, "sensor.ty012047432cf4326b7a3a");
		///<summary>Tuya Socket 3</summary>
		public NumericSensorEntity Ty012047432cf4326b7a3a2 => new(_haContext, "sensor.ty012047432cf4326b7a3a_2");
		///<summary>Tuya Socket 3</summary>
		public NumericSensorEntity Ty012047432cf4326b7a3a3 => new(_haContext, "sensor.ty012047432cf4326b7a3a_3");
		///<summary>Tuya Socket 1</summary>
		public NumericSensorEntity Ty012047435002915e9eb5 => new(_haContext, "sensor.ty012047435002915e9eb5");
		///<summary>Tuya Socket 1</summary>
		public NumericSensorEntity Ty012047435002915e9eb52 => new(_haContext, "sensor.ty012047435002915e9eb5_2");
		///<summary>Tuya Socket 1</summary>
		public NumericSensorEntity Ty012047435002915e9eb53 => new(_haContext, "sensor.ty012047435002915e9eb5_3");
		///<summary>Utility Lux</summary>
		public NumericSensorEntity UtilityLux => new(_haContext, "sensor.utility_lux");
		///<summary>Utility Motion Battery</summary>
		public NumericSensorEntity UtilityMotionBattery => new(_haContext, "sensor.utility_motion_battery");
		///<summary>Wallpanel Fire HD8 RX</summary>
		public NumericSensorEntity WallpanelFireHd8Rx => new(_haContext, "sensor.wallpanel_fire_hd8_rx");
		///<summary>Wallpanel Fire HD8 TX</summary>
		public NumericSensorEntity WallpanelFireHd8Tx => new(_haContext, "sensor.wallpanel_fire_hd8_tx");
		///<summary>washer RX</summary>
		public NumericSensorEntity WasherRx => new(_haContext, "sensor.washer_rx");
		///<summary>washer TX</summary>
		public NumericSensorEntity WasherTx => new(_haContext, "sensor.washer_tx");
		///<summary>Washing machine deltaEnergy</summary>
		public NumericSensorEntity WashingMachineDeltaenergy => new(_haContext, "sensor.washing_machine_deltaenergy");
		///<summary>Washing machine deltaEnergy</summary>
		public NumericSensorEntity WashingMachineDeltaenergy2 => new(_haContext, "sensor.washing_machine_deltaenergy_2");
		///<summary>Washing machine Energy Meter</summary>
		public NumericSensorEntity WashingMachineEnergy => new(_haContext, "sensor.washing_machine_energy");
		///<summary>Washing machine energy</summary>
		public NumericSensorEntity WashingMachineEnergy2 => new(_haContext, "sensor.washing_machine_energy_2");
		///<summary>Washing machine energySaved</summary>
		public NumericSensorEntity WashingMachineEnergysaved => new(_haContext, "sensor.washing_machine_energysaved");
		///<summary>Washing machine energySaved</summary>
		public NumericSensorEntity WashingMachineEnergysaved2 => new(_haContext, "sensor.washing_machine_energysaved_2");
		///<summary>Washing machine Power Meter</summary>
		public NumericSensorEntity WashingMachinePower => new(_haContext, "sensor.washing_machine_power");
		///<summary>Washing machine power</summary>
		public NumericSensorEntity WashingMachinePower2 => new(_haContext, "sensor.washing_machine_power_2");
		///<summary>Washing machine powerEnergy</summary>
		public NumericSensorEntity WashingMachinePowerenergy => new(_haContext, "sensor.washing_machine_powerenergy");
		///<summary>Washing machine powerEnergy</summary>
		public NumericSensorEntity WashingMachinePowerenergy2 => new(_haContext, "sensor.washing_machine_powerenergy_2");
		///<summary>Wiser iTRV-Boys Battery Level</summary>
		public NumericSensorEntity WiserItrvBoysBatteryLevel => new(_haContext, "sensor.wiser_itrv_boys_battery_level");
		///<summary>Wiser iTRV-Dining Battery Level</summary>
		public NumericSensorEntity WiserItrvDiningBatteryLevel => new(_haContext, "sensor.wiser_itrv_dining_battery_level");
		///<summary>Wiser iTRV-Entrance Battery Level</summary>
		public NumericSensorEntity WiserItrvEntranceBatteryLevel => new(_haContext, "sensor.wiser_itrv_entrance_battery_level");
		///<summary>Wiser iTRV-Guest Room Battery Level</summary>
		public NumericSensorEntity WiserItrvGuestRoomBatteryLevel => new(_haContext, "sensor.wiser_itrv_guest_room_battery_level");
		///<summary>Wiser iTRV-Landing Battery Level</summary>
		public NumericSensorEntity WiserItrvLandingBatteryLevel => new(_haContext, "sensor.wiser_itrv_landing_battery_level");
		///<summary>Wiser iTRV-Lounge  Battery Level</summary>
		public NumericSensorEntity WiserItrvLoungeBatteryLevel => new(_haContext, "sensor.wiser_itrv_lounge_battery_level");
		///<summary>Wiser iTRV-Lounge Bay Battery Level</summary>
		public NumericSensorEntity WiserItrvLoungeBayBatteryLevel => new(_haContext, "sensor.wiser_itrv_lounge_bay_battery_level");
		///<summary>Wiser iTRV-Master Battery Level</summary>
		public NumericSensorEntity WiserItrvMasterBatteryLevel => new(_haContext, "sensor.wiser_itrv_master_battery_level");
		///<summary>Wiser iTRV-Office Battery Level</summary>
		public NumericSensorEntity WiserItrvOfficeBatteryLevel => new(_haContext, "sensor.wiser_itrv_office_battery_level");
		///<summary>Wiser iTRV-Playroom Battery Level</summary>
		public NumericSensorEntity WiserItrvPlayroomBatteryLevel => new(_haContext, "sensor.wiser_itrv_playroom_battery_level");
		///<summary>Wiser iTRV-Utility Battery Level</summary>
		public NumericSensorEntity WiserItrvUtilityBatteryLevel => new(_haContext, "sensor.wiser_itrv_utility_battery_level");
		///<summary>Wiser RoomStat-Utility Battery Level</summary>
		public NumericSensorEntity WiserRoomstatUtilityBatteryLevel => new(_haContext, "sensor.wiser_roomstat_utility_battery_level");
		///<summary>WiserHeat031C5E RX</summary>
		public NumericSensorEntity Wiserheat031c5eRx => new(_haContext, "sensor.wiserheat031c5e_rx");
		///<summary>WiserHeat031C5E TX</summary>
		public NumericSensorEntity Wiserheat031c5eTx => new(_haContext, "sensor.wiserheat031c5e_tx");
		///<summary>247D4D7D6C90-mysimplelink Uptime</summary>
		public SensorEntity E247d4d7d6c90MysimplelinkUptime => new(_haContext, "sensor.247d4d7d6c90_mysimplelink_uptime");
		///<summary>Aaron 1 Light Last Seen</summary>
		public SensorEntity Aaron1LightLastSeen => new(_haContext, "sensor.aaron_1_light_last_seen");
		///<summary>Aaron 2 Light Last Seen</summary>
		public SensorEntity Aaron2LightLastSeen => new(_haContext, "sensor.aaron_2_light_last_seen");
		///<summary>Aaron 3 Light Last Seen</summary>
		public SensorEntity Aaron3LightLastSeen => new(_haContext, "sensor.aaron_3_light_last_seen");
		///<summary>Aaron 4 Light Last Seen</summary>
		public SensorEntity Aaron4LightLastSeen => new(_haContext, "sensor.aaron_4_light_last_seen");
		///<summary>Aaron Echo Uptime</summary>
		public SensorEntity AaronEchoUptime => new(_haContext, "sensor.aaron_echo_uptime");
		///<summary>Aaron Motion Last Seen</summary>
		public SensorEntity AaronMotionLastSeen => new(_haContext, "sensor.aaron_motion_last_seen");
		///<summary>Playroom next Alarm</summary>
		public SensorEntity AaronNextAlarm => new(_haContext, "sensor.aaron_next_alarm");
		///<summary>Playroom next Reminder</summary>
		public SensorEntity AaronNextReminder => new(_haContext, "sensor.aaron_next_reminder");
		///<summary>Playroom next Timer</summary>
		public SensorEntity AaronNextTimer => new(_haContext, "sensor.aaron_next_timer");
		///<summary>AccuWeather Home Pressure Tendency</summary>
		public SensorEntity AccuweatherHomePressureTendency => new(_haContext, "sensor.accuweather_home_pressure_tendency");
		///<summary>Alexa Actionable Notification Prompt</summary>
		public SensorEntity AlexaActionableNotificationPrompt => new(_haContext, "sensor.alexa_actionable_notification_prompt");
		///<summary>Treadmill Uptime</summary>
		public SensorEntity AndroidB8c33f1cb7c0d776Uptime => new(_haContext, "sensor.android_b8c33f1cb7c0d776_uptime");
		///<summary>ASGLH-WL-19140 Uptime</summary>
		public SensorEntity AsglhWl19140Uptime => new(_haContext, "sensor.asglh_wl_19140_uptime");
		///<summary>Aubrecia Activity</summary>
		public SensorEntity AubreciaActivity => new(_haContext, "sensor.aubrecia_activity");
		///<summary>Aubrecia Battery State</summary>
		public SensorEntity AubreciaBatteryState => new(_haContext, "sensor.aubrecia_battery_state");
		///<summary>Aubrecia BSSID</summary>
		public SensorEntity AubreciaBssid => new(_haContext, "sensor.aubrecia_bssid");
		///<summary>Aubrecia Connection Type</summary>
		public SensorEntity AubreciaConnectionType => new(_haContext, "sensor.aubrecia_connection_type");
		///<summary>Aubrecia Drive Last Activity</summary>
		public SensorEntity AubreciaDriveLastActivity => new(_haContext, "sensor.aubrecia_drive_last_activity");
		///<summary>Aubrecia Drive Last Motion</summary>
		public SensorEntity AubreciaDriveLastMotion => new(_haContext, "sensor.aubrecia_drive_last_motion");
		///<summary>Aubrecia Drive Volume</summary>
		public SensorEntity AubreciaDriveVolume => new(_haContext, "sensor.aubrecia_drive_volume");
		///<summary>Aubrecia Front Door Last Activity</summary>
		public SensorEntity AubreciaFrontDoorLastActivity => new(_haContext, "sensor.aubrecia_front_door_last_activity");
		///<summary>Aubrecia Front Door Last Ding</summary>
		public SensorEntity AubreciaFrontDoorLastDing => new(_haContext, "sensor.aubrecia_front_door_last_ding");
		///<summary>Aubrecia Front Door Last Motion</summary>
		public SensorEntity AubreciaFrontDoorLastMotion => new(_haContext, "sensor.aubrecia_front_door_last_motion");
		///<summary>Aubrecia Front Door Volume</summary>
		public SensorEntity AubreciaFrontDoorVolume => new(_haContext, "sensor.aubrecia_front_door_volume");
		///<summary>Aubrecia Geocoded Location</summary>
		public SensorEntity AubreciaGeocodedLocation => new(_haContext, "sensor.aubrecia_geocoded_location");
		///<summary>Aubrecia Last Update Trigger</summary>
		public SensorEntity AubreciaLastUpdateTrigger => new(_haContext, "sensor.aubrecia_last_update_trigger");
		///<summary>Aubrecia SIM 1</summary>
		public SensorEntity AubreciaSim1 => new(_haContext, "sensor.aubrecia_sim_1");
		///<summary>Aubrecia SIM 2</summary>
		public SensorEntity AubreciaSim2 => new(_haContext, "sensor.aubrecia_sim_2");
		///<summary>Aubrecia SSID</summary>
		public SensorEntity AubreciaSsid => new(_haContext, "sensor.aubrecia_ssid");
		///<summary>AubreciasiPhone Uptime</summary>
		public SensorEntity AubreciasiphoneUptime => new(_haContext, "sensor.aubreciasiphone_uptime");
		///<summary>Audi Q7 Doors/Boot state</summary>
		public SensorEntity AudiQ7DoorsBootState => new(_haContext, "sensor.audi_q7_doors_boot_state");
		///<summary>Audi Q7 Last Update</summary>
		public SensorEntity AudiQ7LastUpdate => new(_haContext, "sensor.audi_q7_last_update");
		///<summary>Audi Q7 Model</summary>
		public SensorEntity AudiQ7Model => new(_haContext, "sensor.audi_q7_model");
		///<summary>Bathroom Motion Last Seen</summary>
		public SensorEntity BathroomMotionLastSeen => new(_haContext, "sensor.bathroom_motion_last_seen");
		///<summary>Bedroom 1 AC Uptime</summary>
		public SensorEntity Bedroom1AcUptime => new(_haContext, "sensor.bedroom_1_ac_uptime");
		///<summary>Bedroom 2 AC Uptime</summary>
		public SensorEntity Bedroom2AcUptime => new(_haContext, "sensor.bedroom_2_ac_uptime");
		///<summary>Bedroom 3 AC Uptime</summary>
		public SensorEntity Bedroom3AcUptime => new(_haContext, "sensor.bedroom_3_ac_uptime");
		///<summary>Bedroom 4 AC Uptime</summary>
		public SensorEntity Bedroom4AcUptime => new(_haContext, "sensor.bedroom_4_ac_uptime");
		///<summary>christmas_indoor-1558 Uptime</summary>
		public SensorEntity ChristmasIndoor1558Uptime => new(_haContext, "sensor.christmas_indoor_1558_uptime");
		///<summary>ASAZ-5CG127498B Uptime</summary>
		public SensorEntity DesktopIpurn8tUptime => new(_haContext, "sensor.desktop_ipurn8t_uptime");
		///<summary>Dining Door Last Seen</summary>
		public SensorEntity DiningDoorLastSeen => new(_haContext, "sensor.dining_door_last_seen");
		///<summary>Dining Echo Uptime</summary>
		public SensorEntity DiningEchoUptime => new(_haContext, "sensor.dining_echo_uptime");
		///<summary>Dining Motion Last Seen</summary>
		public SensorEntity DiningMotionLastSeen => new(_haContext, "sensor.dining_motion_last_seen");
		///<summary>Dining Uptime</summary>
		public SensorEntity DiningUptime => new(_haContext, "sensor.dining_uptime");
		///<summary>Dining Wall 1 Light Last Seen</summary>
		public SensorEntity DiningWall1LightLastSeen => new(_haContext, "sensor.dining_wall_1_light_last_seen");
		///<summary>Dining Wall 2 Light Last Seen</summary>
		public SensorEntity DiningWall2LightLastSeen => new(_haContext, "sensor.dining_wall_2_light_last_seen");
		///<summary>Dining1 Light Last Seen</summary>
		public SensorEntity Dining1LightLastSeen => new(_haContext, "sensor.dining1_light_last_seen");
		///<summary>Dining2 Light Last Seen</summary>
		public SensorEntity Dining2LightLastSeen => new(_haContext, "sensor.dining2_light_last_seen");
		///<summary>Dining3 Light Last Seen</summary>
		public SensorEntity Dining3LightLastSeen => new(_haContext, "sensor.dining3_light_last_seen");
		///<summary>Dining4 Light Last Seen</summary>
		public SensorEntity Dining4LightLastSeen => new(_haContext, "sensor.dining4_light_last_seen");
		///<summary>Dining5 Light Last Seen</summary>
		public SensorEntity Dining5LightLastSeen => new(_haContext, "sensor.dining5_light_last_seen");
		///<summary>Downstairs Info</summary>
		public SensorEntity DownstairsInfo => new(_haContext, "sensor.downstairs_info");
		///<summary>Downstairs Volume</summary>
		public SensorEntity DownstairsVolume => new(_haContext, "sensor.downstairs_volume");
		///<summary>Drive Snapshot Last Updated</summary>
		public SensorEntity DriveSnapshotLastUpdated => new(_haContext, "sensor.drive_snapshot_last_updated");
		///<summary>dryer Uptime</summary>
		public SensorEntity DryerUptime => new(_haContext, "sensor.dryer_uptime");
		///<summary>Entrance 1 Light Last Seen</summary>
		public SensorEntity Entrance1LightLastSeen => new(_haContext, "sensor.entrance_1_light_last_seen");
		///<summary>Entrance 2 Light Last Seen</summary>
		public SensorEntity Entrance2LightLastSeen => new(_haContext, "sensor.entrance_2_light_last_seen");
		///<summary>Entrance Motion Last Seen</summary>
		public SensorEntity EntranceMotionLastSeen => new(_haContext, "sensor.entrance_motion_last_seen");
		///<summary>Entrance Uptime</summary>
		public SensorEntity EntranceUptime => new(_haContext, "sensor.entrance_uptime");
		///<summary>ESP_6B7081 Uptime</summary>
		public SensorEntity Esp6b7081Uptime => new(_haContext, "sensor.esp_6b7081_uptime");
		///<summary>ESP_6B7A3A Uptime</summary>
		public SensorEntity Esp6b7a3aUptime => new(_haContext, "sensor.esp_6b7a3a_uptime");
		///<summary>eufy RoboVac Uptime</summary>
		public SensorEntity EufyRobovacUptime => new(_haContext, "sensor.eufy_robovac_uptime");
		///<summary>eufy RoboVac Uptime</summary>
		public SensorEntity EufyRobovacUptime2 => new(_haContext, "sensor.eufy_robovac_uptime_2");
		///<summary>EUGENE-DESKTOP Battery Level</summary>
		public SensorEntity EugeneDesktopBatteryLevel => new(_haContext, "sensor.eugene_desktop_battery_level");
		///<summary>EUGENE-DESKTOP Current Username</summary>
		public SensorEntity EugeneDesktopCurrentUsername => new(_haContext, "sensor.eugene_desktop_current_username");
		///<summary>EUGENE-DESKTOP Network #0 - IPv4</summary>
		public SensorEntity EugeneDesktopNetwork0Ipv4 => new(_haContext, "sensor.eugene_desktop_network_0_ipv4");
		///<summary>EUGENE-DESKTOP Network #0 - IPv6</summary>
		public SensorEntity EugeneDesktopNetwork0Ipv6 => new(_haContext, "sensor.eugene_desktop_network_0_ipv6");
		///<summary>EUGENE-DESKTOP Network #1 - IPv4</summary>
		public SensorEntity EugeneDesktopNetwork1Ipv4 => new(_haContext, "sensor.eugene_desktop_network_1_ipv4");
		///<summary>EUGENE-DESKTOP Network #1 - IPv6</summary>
		public SensorEntity EugeneDesktopNetwork1Ipv6 => new(_haContext, "sensor.eugene_desktop_network_1_ipv6");
		///<summary>EUGENE-DESKTOP Screen #0 - Height</summary>
		public SensorEntity EugeneDesktopScreen0Height => new(_haContext, "sensor.eugene_desktop_screen_0_height");
		///<summary>EUGENE-DESKTOP Screen #0 - Width</summary>
		public SensorEntity EugeneDesktopScreen0Width => new(_haContext, "sensor.eugene_desktop_screen_0_width");
		///<summary>EUGENE-DESKTOP Storage C - Format</summary>
		public SensorEntity EugeneDesktopStorageCFormat => new(_haContext, "sensor.eugene_desktop_storage_c_format");
		///<summary>EUGENE-DESKTOP Storage C - Label</summary>
		public SensorEntity EugeneDesktopStorageCLabel => new(_haContext, "sensor.eugene_desktop_storage_c_label");
		///<summary>EUGENE-DESKTOP Storage D - Format</summary>
		public SensorEntity EugeneDesktopStorageDFormat => new(_haContext, "sensor.eugene_desktop_storage_d_format");
		///<summary>EUGENE-DESKTOP Storage D - Label</summary>
		public SensorEntity EugeneDesktopStorageDLabel => new(_haContext, "sensor.eugene_desktop_storage_d_label");
		///<summary>EUGENE-DESKTOP Storage E - Format</summary>
		public SensorEntity EugeneDesktopStorageEFormat => new(_haContext, "sensor.eugene_desktop_storage_e_format");
		///<summary>EUGENE-DESKTOP Storage E - Label</summary>
		public SensorEntity EugeneDesktopStorageELabel => new(_haContext, "sensor.eugene_desktop_storage_e_label");
		///<summary>EUGENE-DESKTOP System Boot Time</summary>
		public SensorEntity EugeneDesktopSystemBootTime => new(_haContext, "sensor.eugene_desktop_system_boot_time");
		///<summary>EUGENE-DESKTOP System Uptime</summary>
		public SensorEntity EugeneDesktopSystemUptime => new(_haContext, "sensor.eugene_desktop_system_uptime");
		///<summary>EUGENE-DESKTOP Uptime</summary>
		public SensorEntity EugeneDesktopUptime => new(_haContext, "sensor.eugene_desktop_uptime");
		///<summary>Garden next Alarm</summary>
		public SensorEntity EugeneS2ndEchoDotNextAlarm => new(_haContext, "sensor.eugene_s_2nd_echo_dot_next_alarm");
		///<summary>Garden next Reminder</summary>
		public SensorEntity EugeneS2ndEchoDotNextReminder => new(_haContext, "sensor.eugene_s_2nd_echo_dot_next_reminder");
		///<summary>Garden next Timer</summary>
		public SensorEntity EugeneS2ndEchoDotNextTimer => new(_haContext, "sensor.eugene_s_2nd_echo_dot_next_timer");
		///<summary>Spare echo next Alarm</summary>
		public SensorEntity EugeneS3rdEchoDotNextAlarm => new(_haContext, "sensor.eugene_s_3rd_echo_dot_next_alarm");
		///<summary>Spare echo next Reminder</summary>
		public SensorEntity EugeneS3rdEchoDotNextReminder => new(_haContext, "sensor.eugene_s_3rd_echo_dot_next_reminder");
		///<summary>Spare echo next Timer</summary>
		public SensorEntity EugeneS3rdEchoDotNextTimer => new(_haContext, "sensor.eugene_s_3rd_echo_dot_next_timer");
		///<summary>Lounge LG Alexa next Alarm</summary>
		public SensorEntity EugeneSLgOledWebos2021TvNextAlarm => new(_haContext, "sensor.eugene_s_lg_oled_webos_2021_tv_next_alarm");
		///<summary>Lounge LG Alexa next Reminder</summary>
		public SensorEntity EugeneSLgOledWebos2021TvNextReminder => new(_haContext, "sensor.eugene_s_lg_oled_webos_2021_tv_next_reminder");
		///<summary>Lounge LG Alexa next Timer</summary>
		public SensorEntity EugeneSLgOledWebos2021TvNextTimer => new(_haContext, "sensor.eugene_s_lg_oled_webos_2021_tv_next_timer");
		///<summary>Master LG Alexa next Alarm</summary>
		public SensorEntity EugeneSLgWebos2020TvNextAlarm => new(_haContext, "sensor.eugene_s_lg_webos_2020_tv_next_alarm");
		///<summary>Master LG Alexa next Reminder</summary>
		public SensorEntity EugeneSLgWebos2020TvNextReminder => new(_haContext, "sensor.eugene_s_lg_webos_2020_tv_next_reminder");
		///<summary>Master LG Alexa next Timer</summary>
		public SensorEntity EugeneSLgWebos2020TvNextTimer => new(_haContext, "sensor.eugene_s_lg_webos_2020_tv_next_timer");
		///<summary>Lounge next Alarm</summary>
		public SensorEntity EugeneSSonosArcNextAlarm => new(_haContext, "sensor.eugene_s_sonos_arc_next_alarm");
		///<summary>Lounge next Reminder</summary>
		public SensorEntity EugeneSSonosArcNextReminder => new(_haContext, "sensor.eugene_s_sonos_arc_next_reminder");
		///<summary>Lounge next Timer</summary>
		public SensorEntity EugeneSSonosArcNextTimer => new(_haContext, "sensor.eugene_s_sonos_arc_next_timer");
		///<summary>Eugene’s iPhone Activity</summary>
		public SensorEntity EugenesIphoneActivity => new(_haContext, "sensor.eugenes_iphone_activity");
		///<summary>Eugene’s iPhone Battery State</summary>
		public SensorEntity EugenesIphoneBatteryState => new(_haContext, "sensor.eugenes_iphone_battery_state");
		///<summary>Eugene’s iPhone BSSID</summary>
		public SensorEntity EugenesIphoneBssid => new(_haContext, "sensor.eugenes_iphone_bssid");
		///<summary>Eugene’s iPhone Connection Type</summary>
		public SensorEntity EugenesIphoneConnectionType => new(_haContext, "sensor.eugenes_iphone_connection_type");
		///<summary>Eugene’s iPhone Geocoded Location</summary>
		public SensorEntity EugenesIphoneGeocodedLocation => new(_haContext, "sensor.eugenes_iphone_geocoded_location");
		///<summary>Eugene’s iPhone Last Update Trigger</summary>
		public SensorEntity EugenesIphoneLastUpdateTrigger => new(_haContext, "sensor.eugenes_iphone_last_update_trigger");
		///<summary>Eugene’s iPhone SIM 1</summary>
		public SensorEntity EugenesIphoneSim1 => new(_haContext, "sensor.eugenes_iphone_sim_1");
		///<summary>Eugene’s iPhone SIM 2</summary>
		public SensorEntity EugenesIphoneSim2 => new(_haContext, "sensor.eugenes_iphone_sim_2");
		///<summary>Eugene’s iPhone SSID</summary>
		public SensorEntity EugenesIphoneSsid => new(_haContext, "sensor.eugenes_iphone_ssid");
		///<summary>Eugenes-iPhone Uptime</summary>
		public SensorEntity EugenesIphoneUptime => new(_haContext, "sensor.eugenes_iphone_uptime");
		///<summary>EugenespleWatch Uptime</summary>
		public SensorEntity EugenesplewatchUptime => new(_haContext, "sensor.eugenesplewatch_uptime");
		///<summary>fail2ban sshd</summary>
		public SensorEntity Fail2banSshd => new(_haContext, "sensor.fail2ban_sshd");
		///<summary>floor_light-2086 Uptime</summary>
		public SensorEntity FloorLight2086Uptime => new(_haContext, "sensor.floor_light_2086_uptime");
		///<summary>Floor Light Last Seen</summary>
		public SensorEntity FloorLightLastSeen => new(_haContext, "sensor.floor_light_last_seen");
		///<summary>Foscam Uptime</summary>
		public SensorEntity FoscamUptime => new(_haContext, "sensor.foscam_uptime");
		///<summary>Front Door Snapshot Last Updated</summary>
		public SensorEntity FrontDoorSnapshotLastUpdated => new(_haContext, "sensor.front_door_snapshot_last_updated");
		///<summary>Galaxy-S8 Uptime</summary>
		public SensorEntity GalaxyS8Uptime => new(_haContext, "sensor.galaxy_s8_uptime");
		///<summary>Garage Back Door Last Seen</summary>
		public SensorEntity GarageBackDoorLastSeen => new(_haContext, "sensor.garage_back_door_last_seen");
		///<summary>Garage Echo Uptime</summary>
		public SensorEntity GarageEchoUptime => new(_haContext, "sensor.garage_echo_uptime");
		///<summary>Garage next Alarm</summary>
		public SensorEntity GarageNextAlarm => new(_haContext, "sensor.garage_next_alarm");
		///<summary>Garage next Reminder</summary>
		public SensorEntity GarageNextReminder => new(_haContext, "sensor.garage_next_reminder");
		///<summary>Garage next Timer</summary>
		public SensorEntity GarageNextTimer => new(_haContext, "sensor.garage_next_timer");
		///<summary>Garage Snapshot Last Updated</summary>
		public SensorEntity GarageSnapshotLastUpdated => new(_haContext, "sensor.garage_snapshot_last_updated");
		///<summary>Garden Floodlights Uptime</summary>
		public SensorEntity GardenFloodlightsUptime => new(_haContext, "sensor.garden_floodlights_uptime");
		///<summary>Garden Last Activity</summary>
		public SensorEntity GardenLastActivity2 => new(_haContext, "sensor.garden_last_activity_2");
		///<summary>Garden Last Motion</summary>
		public SensorEntity GardenLastMotion2 => new(_haContext, "sensor.garden_last_motion_2");
		///<summary>Garden Snapshot Last Updated</summary>
		public SensorEntity GardenSnapshotLastUpdated => new(_haContext, "sensor.garden_snapshot_last_updated");
		///<summary>Garden Volume</summary>
		public SensorEntity GardenVolume2 => new(_haContext, "sensor.garden_volume_2");
		///<summary>Hailey's iPhone Activity</summary>
		public SensorEntity HaileySIphoneActivity => new(_haContext, "sensor.hailey_s_iphone_activity");
		///<summary>Hailey's iPhone Battery State</summary>
		public SensorEntity HaileySIphoneBatteryState => new(_haContext, "sensor.hailey_s_iphone_battery_state");
		///<summary>Hailey's iPhone BSSID</summary>
		public SensorEntity HaileySIphoneBssid => new(_haContext, "sensor.hailey_s_iphone_bssid");
		///<summary>Hailey's iPhone Connection Type</summary>
		public SensorEntity HaileySIphoneConnectionType => new(_haContext, "sensor.hailey_s_iphone_connection_type");
		///<summary>Hailey's iPhone Geocoded Location</summary>
		public SensorEntity HaileySIphoneGeocodedLocation => new(_haContext, "sensor.hailey_s_iphone_geocoded_location");
		///<summary>Hailey's iPhone Last Update Trigger</summary>
		public SensorEntity HaileySIphoneLastUpdateTrigger => new(_haContext, "sensor.hailey_s_iphone_last_update_trigger");
		///<summary>Hailey's iPhone SIM 1</summary>
		public SensorEntity HaileySIphoneSim1 => new(_haContext, "sensor.hailey_s_iphone_sim_1");
		///<summary>Hailey's iPhone SIM 2</summary>
		public SensorEntity HaileySIphoneSim2 => new(_haContext, "sensor.hailey_s_iphone_sim_2");
		///<summary>Hailey's iPhone SSID</summary>
		public SensorEntity HaileySIphoneSsid => new(_haContext, "sensor.hailey_s_iphone_ssid");
		///<summary>Haileys-Air Uptime</summary>
		public SensorEntity HaileysAirUptime => new(_haContext, "sensor.haileys_air_uptime");
		///<summary>Haileys-iPhone Uptime</summary>
		public SensorEntity HaileysIphoneUptime => new(_haContext, "sensor.haileys_iphone_uptime");
		///<summary>Haileys-iPhone Uptime</summary>
		public SensorEntity HaileysIphoneUptime2 => new(_haContext, "sensor.haileys_iphone_uptime_2");
		///<summary>Hailey’s MacBook Air Active Audio Output</summary>
		public SensorEntity HaileysMacbookAirActiveAudioOutput => new(_haContext, "sensor.haileys_macbook_air_active_audio_output");
		///<summary>Hailey’s MacBook Air Active Camera</summary>
		public SensorEntity HaileysMacbookAirActiveCamera => new(_haContext, "sensor.haileys_macbook_air_active_camera");
		///<summary>Hailey’s MacBook Air Active Microphone</summary>
		public SensorEntity HaileysMacbookAirActiveMicrophone => new(_haContext, "sensor.haileys_macbook_air_active_microphone");
		///<summary>Hailey’s MacBook Air BSSID</summary>
		public SensorEntity HaileysMacbookAirBssid => new(_haContext, "sensor.haileys_macbook_air_bssid");
		///<summary>Hailey’s MacBook Air Connection Type</summary>
		public SensorEntity HaileysMacbookAirConnectionType => new(_haContext, "sensor.haileys_macbook_air_connection_type");
		///<summary>Hailey’s MacBook Air Displays</summary>
		public SensorEntity HaileysMacbookAirDisplays => new(_haContext, "sensor.haileys_macbook_air_displays");
		///<summary>Hailey’s MacBook Air Frontmost App</summary>
		public SensorEntity HaileysMacbookAirFrontmostApp => new(_haContext, "sensor.haileys_macbook_air_frontmost_app");
		///<summary>Hailey’s MacBook Air Geocoded Location</summary>
		public SensorEntity HaileysMacbookAirGeocodedLocation => new(_haContext, "sensor.haileys_macbook_air_geocoded_location");
		///<summary>Hailey’s MacBook Air Internal Battery State</summary>
		public SensorEntity HaileysMacbookAirInternalBatteryState => new(_haContext, "sensor.haileys_macbook_air_internal_battery_state");
		///<summary>Hailey’s MacBook Air Last Update Trigger</summary>
		public SensorEntity HaileysMacbookAirLastUpdateTrigger => new(_haContext, "sensor.haileys_macbook_air_last_update_trigger");
		///<summary>Hailey’s MacBook Air Primary Display ID</summary>
		public SensorEntity HaileysMacbookAirPrimaryDisplayId => new(_haContext, "sensor.haileys_macbook_air_primary_display_id");
		///<summary>Hailey’s MacBook Air Primary Display Name</summary>
		public SensorEntity HaileysMacbookAirPrimaryDisplayName => new(_haContext, "sensor.haileys_macbook_air_primary_display_name");
		///<summary>Hailey’s MacBook Air SSID</summary>
		public SensorEntity HaileysMacbookAirSsid => new(_haContext, "sensor.haileys_macbook_air_ssid");
		///<summary>HP Color LaserJet 4500 hpijs pcl3, 3.18.12</summary>
		public SensorEntity HpColorLaserjet4500HpijsPcl331812 => new(_haContext, "sensor.hp_color_laserjet_4500_hpijs_pcl3_3_18_12");
		///<summary>HUAWEI_P_smart_2019-86203 Uptime</summary>
		public SensorEntity HuaweiPSmart201986203Uptime => new(_haContext, "sensor.huawei_p_smart_2019_86203_uptime");
		///<summary>iPad Uptime</summary>
		public SensorEntity IpadUptime => new(_haContext, "sensor.ipad_uptime");
		///<summary>Jayden AppleTv Uptime</summary>
		public SensorEntity JaydenAppletvUptime => new(_haContext, "sensor.jayden_appletv_uptime");
		///<summary>jayden_bedside-4734 Uptime</summary>
		public SensorEntity JaydenBedside4734Uptime => new(_haContext, "sensor.jayden_bedside_4734_uptime");
		///<summary>Jayden Echo Uptime</summary>
		public SensorEntity JaydenEchoUptime => new(_haContext, "sensor.jayden_echo_uptime");
		///<summary>Jayden Motion Last Seen</summary>
		public SensorEntity JaydenMotionLastSeen => new(_haContext, "sensor.jayden_motion_last_seen");
		///<summary>Aaron next Alarm</summary>
		public SensorEntity JaydenNextAlarm => new(_haContext, "sensor.jayden_next_alarm");
		///<summary>Jayden next Alarm</summary>
		public SensorEntity JaydenNextAlarm2 => new(_haContext, "sensor.jayden_next_alarm_2");
		///<summary>Aaron next Reminder</summary>
		public SensorEntity JaydenNextReminder => new(_haContext, "sensor.jayden_next_reminder");
		///<summary>Jayden next Reminder</summary>
		public SensorEntity JaydenNextReminder2 => new(_haContext, "sensor.jayden_next_reminder_2");
		///<summary>Aaron next Timer</summary>
		public SensorEntity JaydenNextTimer => new(_haContext, "sensor.jayden_next_timer");
		///<summary>Jayden next Timer</summary>
		public SensorEntity JaydenNextTimer2 => new(_haContext, "sensor.jayden_next_timer_2");
		///<summary>Jayden Uptime</summary>
		public SensorEntity JaydenUptime => new(_haContext, "sensor.jayden_uptime");
		///<summary>Johan Front Door Last Activity</summary>
		public SensorEntity JohanFrontDoorLastActivity => new(_haContext, "sensor.johan_front_door_last_activity");
		///<summary>Johan Front Door Last Ding</summary>
		public SensorEntity JohanFrontDoorLastDing => new(_haContext, "sensor.johan_front_door_last_ding");
		///<summary>Johan Front Door Last Motion</summary>
		public SensorEntity JohanFrontDoorLastMotion => new(_haContext, "sensor.johan_front_door_last_motion");
		///<summary>Johan Front Door Volume</summary>
		public SensorEntity JohanFrontDoorVolume => new(_haContext, "sensor.johan_front_door_volume");
		///<summary>Kitchen 1 Light Last Seen</summary>
		public SensorEntity Kitchen1LightLastSeen => new(_haContext, "sensor.kitchen_1_light_last_seen");
		///<summary>Kitchen 2 Light Last Seen</summary>
		public SensorEntity Kitchen2LightLastSeen => new(_haContext, "sensor.kitchen_2_light_last_seen");
		///<summary>Kitchen 3 Light Last Seen</summary>
		public SensorEntity Kitchen3LightLastSeen => new(_haContext, "sensor.kitchen_3_light_last_seen");
		///<summary>Kitchen 4 Light Last Seen</summary>
		public SensorEntity Kitchen4LightLastSeen => new(_haContext, "sensor.kitchen_4_light_last_seen");
		///<summary>Kitchen 5 Light Last Seen</summary>
		public SensorEntity Kitchen5LightLastSeen => new(_haContext, "sensor.kitchen_5_light_last_seen");
		///<summary>Kitchen 6 Light Last Seen</summary>
		public SensorEntity Kitchen6LightLastSeen => new(_haContext, "sensor.kitchen_6_light_last_seen");
		///<summary>Kitchen Echo Uptime</summary>
		public SensorEntity KitchenEchoUptime => new(_haContext, "sensor.kitchen_echo_uptime");
		///<summary>Kitchen next Alarm</summary>
		public SensorEntity KitchenNextAlarm => new(_haContext, "sensor.kitchen_next_alarm");
		///<summary>Kitchen next Reminder</summary>
		public SensorEntity KitchenNextReminder => new(_haContext, "sensor.kitchen_next_reminder");
		///<summary>Kitchen next Timer</summary>
		public SensorEntity KitchenNextTimer => new(_haContext, "sensor.kitchen_next_timer");
		///<summary>Konnected AddOn Uptime</summary>
		public SensorEntity KonnectedAddonUptime => new(_haContext, "sensor.konnected_addon_uptime");
		///<summary>Konnected Main Uptime</summary>
		public SensorEntity KonnectedMainUptime => new(_haContext, "sensor.konnected_main_uptime");
		///<summary>Landing Motion Last Seen</summary>
		public SensorEntity LandingMotionLastSeen => new(_haContext, "sensor.landing_motion_last_seen");
		///<summary>Landing Uptime</summary>
		public SensorEntity LandingUptime => new(_haContext, "sensor.landing_uptime");
		///<summary>LG Lounge Uptime</summary>
		public SensorEntity LgLoungeUptime => new(_haContext, "sensor.lg_lounge_uptime");
		///<summary>Living-Room Uptime</summary>
		public SensorEntity LivingRoomUptime => new(_haContext, "sensor.living_room_uptime");
		///<summary>Lounge AC Uptime</summary>
		public SensorEntity LoungeAcUptime => new(_haContext, "sensor.lounge_ac_uptime");
		///<summary>Lounge Audio Input Format</summary>
		public SensorEntity LoungeAudioInputFormat => new(_haContext, "sensor.lounge_audio_input_format");
		///<summary>Lounge Back Light Last Seen</summary>
		public SensorEntity LoungeBackLightLastSeen => new(_haContext, "sensor.lounge_back_light_last_seen");
		///<summary>Lounge Blind Uptime</summary>
		public SensorEntity LoungeBlindUptime => new(_haContext, "sensor.lounge_blind_uptime");
		///<summary>Lounge Door Last Seen</summary>
		public SensorEntity LoungeDoorLastSeen => new(_haContext, "sensor.lounge_door_last_seen");
		///<summary>Lounge Echo Uptime</summary>
		public SensorEntity LoungeEchoUptime => new(_haContext, "sensor.lounge_echo_uptime");
		///<summary>Lounge Front Light Last Seen</summary>
		public SensorEntity LoungeFrontLightLastSeen => new(_haContext, "sensor.lounge_front_light_last_seen");
		///<summary>Dining next Alarm</summary>
		public SensorEntity LoungeGroupNextAlarm => new(_haContext, "sensor.lounge_group_next_alarm");
		///<summary>Dining next Reminder</summary>
		public SensorEntity LoungeGroupNextReminder => new(_haContext, "sensor.lounge_group_next_reminder");
		///<summary>Dining next Timer</summary>
		public SensorEntity LoungeGroupNextTimer => new(_haContext, "sensor.lounge_group_next_timer");
		///<summary>Lounge Left Window Last Seen</summary>
		public SensorEntity LoungeLeftWindowLastSeen => new(_haContext, "sensor.lounge_left_window_last_seen");
		///<summary>Lounge Right Window Last Seen</summary>
		public SensorEntity LoungeRightWindowLastSeen => new(_haContext, "sensor.lounge_right_window_last_seen");
		///<summary>Lounge Sonos next Reminder</summary>
		public SensorEntity LoungeSonosNextReminder => new(_haContext, "sensor.lounge_sonos_next_reminder");
		///<summary>Master 1 Light Last Seen</summary>
		public SensorEntity Master1LightLastSeen => new(_haContext, "sensor.master_1_light_last_seen");
		///<summary>Master 2 Light Last Seen</summary>
		public SensorEntity Master2LightLastSeen => new(_haContext, "sensor.master_2_light_last_seen");
		///<summary>Master 3 Light Last Seen</summary>
		public SensorEntity Master3LightLastSeen => new(_haContext, "sensor.master_3_light_last_seen");
		///<summary>Master 4 Light Last Seen</summary>
		public SensorEntity Master4LightLastSeen => new(_haContext, "sensor.master_4_light_last_seen");
		///<summary>Master Echo Uptime</summary>
		public SensorEntity MasterEchoUptime => new(_haContext, "sensor.master_echo_uptime");
		///<summary>Master Motion Last Seen</summary>
		public SensorEntity MasterMotionLastSeen => new(_haContext, "sensor.master_motion_last_seen");
		///<summary>Master next Alarm</summary>
		public SensorEntity MasterNextAlarm => new(_haContext, "sensor.master_next_alarm");
		///<summary>Master next Reminder</summary>
		public SensorEntity MasterNextReminder => new(_haContext, "sensor.master_next_reminder");
		///<summary>Master next Timer</summary>
		public SensorEntity MasterNextTimer => new(_haContext, "sensor.master_next_timer");
		///<summary>Master Tele Uptime</summary>
		public SensorEntity MasterTeleUptime => new(_haContext, "sensor.master_tele_uptime");
		///<summary>neerslag_buienalarm_regen_data</summary>
		public SensorEntity NeerslagBuienalarmRegenData => new(_haContext, "sensor.neerslag_buienalarm_regen_data");
		///<summary>neerslag_buienradar_regen_data</summary>
		public SensorEntity NeerslagBuienradarRegenData => new(_haContext, "sensor.neerslag_buienradar_regen_data");
		///<summary>netdaemon_status</summary>
		public SensorEntity NetdaemonStatus => new(_haContext, "sensor.netdaemon_status");
		///<summary>Niemand Drive Info</summary>
		public SensorEntity NiemandDriveInfo => new(_haContext, "sensor.niemand_drive_info");
		///<summary>Niemand Drive Last Activity</summary>
		public SensorEntity NiemandDriveLastActivity => new(_haContext, "sensor.niemand_drive_last_activity");
		///<summary>Niemand Drive Last Motion</summary>
		public SensorEntity NiemandDriveLastMotion => new(_haContext, "sensor.niemand_drive_last_motion");
		///<summary>Niemand Drive Volume</summary>
		public SensorEntity NiemandDriveVolume => new(_haContext, "sensor.niemand_drive_volume");
		///<summary>Niemand Front Door Info</summary>
		public SensorEntity NiemandFrontDoorInfo => new(_haContext, "sensor.niemand_front_door_info");
		///<summary>Niemand Front Door Last Activity</summary>
		public SensorEntity NiemandFrontDoorLastActivity => new(_haContext, "sensor.niemand_front_door_last_activity");
		///<summary>Niemand Front Door Last Ding</summary>
		public SensorEntity NiemandFrontDoorLastDing => new(_haContext, "sensor.niemand_front_door_last_ding");
		///<summary>Niemand Front Door Last Motion</summary>
		public SensorEntity NiemandFrontDoorLastMotion => new(_haContext, "sensor.niemand_front_door_last_motion");
		///<summary>Niemand Front Door Volume</summary>
		public SensorEntity NiemandFrontDoorVolume => new(_haContext, "sensor.niemand_front_door_volume");
		///<summary>Niemand Garage Info</summary>
		public SensorEntity NiemandGarageInfo => new(_haContext, "sensor.niemand_garage_info");
		///<summary>Niemand Garage Last Activity</summary>
		public SensorEntity NiemandGarageLastActivity => new(_haContext, "sensor.niemand_garage_last_activity");
		///<summary>Niemand Garage Last Motion</summary>
		public SensorEntity NiemandGarageLastMotion => new(_haContext, "sensor.niemand_garage_last_motion");
		///<summary>Niemand Garage Volume</summary>
		public SensorEntity NiemandGarageVolume => new(_haContext, "sensor.niemand_garage_volume");
		///<summary>Niemand Garden Info</summary>
		public SensorEntity NiemandGardenInfo => new(_haContext, "sensor.niemand_garden_info");
		///<summary>Niemand Garden Last Activity</summary>
		public SensorEntity NiemandGardenLastActivity => new(_haContext, "sensor.niemand_garden_last_activity");
		///<summary>Niemand Garden Last Motion</summary>
		public SensorEntity NiemandGardenLastMotion => new(_haContext, "sensor.niemand_garden_last_motion");
		///<summary>Niemand Garden Volume</summary>
		public SensorEntity NiemandGardenVolume => new(_haContext, "sensor.niemand_garden_volume");
		///<summary>Niemand Side Info</summary>
		public SensorEntity NiemandSideInfo => new(_haContext, "sensor.niemand_side_info");
		///<summary>Niemand Side Last Activity</summary>
		public SensorEntity NiemandSideLastActivity => new(_haContext, "sensor.niemand_side_last_activity");
		///<summary>Niemand Side Last Motion</summary>
		public SensorEntity NiemandSideLastMotion => new(_haContext, "sensor.niemand_side_last_motion");
		///<summary>Niemand Side Volume</summary>
		public SensorEntity NiemandSideVolume => new(_haContext, "sensor.niemand_side_volume");
		///<summary>Office AC Uptime</summary>
		public SensorEntity OfficeAcUptime => new(_haContext, "sensor.office_ac_uptime");
		///<summary>Office Door Last Seen</summary>
		public SensorEntity OfficeDoorLastSeen => new(_haContext, "sensor.office_door_last_seen");
		///<summary>Office Echo Uptime</summary>
		public SensorEntity OfficeEchoUptime => new(_haContext, "sensor.office_echo_uptime");
		///<summary>Office Motion Last Seen</summary>
		public SensorEntity OfficeMotionLastSeen => new(_haContext, "sensor.office_motion_last_seen");
		///<summary>Office next Alarm</summary>
		public SensorEntity OfficeNextAlarm => new(_haContext, "sensor.office_next_alarm");
		///<summary>Office next Reminder</summary>
		public SensorEntity OfficeNextReminder => new(_haContext, "sensor.office_next_reminder");
		///<summary>Office next Timer</summary>
		public SensorEntity OfficeNextTimer => new(_haContext, "sensor.office_next_timer");
		///<summary>Outside Drive Uptime</summary>
		public SensorEntity OutsideDriveUptime => new(_haContext, "sensor.outside_drive_uptime");
		///<summary>Outside Garage Uptime</summary>
		public SensorEntity OutsideGarageUptime => new(_haContext, "sensor.outside_garage_uptime");
		///<summary>Playroom Echo Uptime</summary>
		public SensorEntity PlayroomEchoUptime => new(_haContext, "sensor.playroom_echo_uptime");
		///<summary>Playroom Light Last Seen</summary>
		public SensorEntity PlayroomLightLastSeen => new(_haContext, "sensor.playroom_light_last_seen");
		///<summary>Playroom Motion Last Seen</summary>
		public SensorEntity PlayroomMotionLastSeen => new(_haContext, "sensor.playroom_motion_last_seen");
		///<summary>plug_1_energy_stats</summary>
		public SensorEntity Plug1EnergyStats => new(_haContext, "sensor.plug_1_energy_stats");
		///<summary>Porch Uptime</summary>
		public SensorEntity PorchUptime => new(_haContext, "sensor.porch_uptime");
		///<summary>RaspberryPi CUPS Uptime</summary>
		public SensorEntity RaspberrypiCupsUptime => new(_haContext, "sensor.raspberrypi_cups_uptime");
		///<summary>raspberrypi Uptime</summary>
		public SensorEntity RaspberrypiUptime => new(_haContext, "sensor.raspberrypi_uptime");
		///<summary>Remote 1 Action</summary>
		public SensorEntity Remote1Action => new(_haContext, "sensor.remote_1_action");
		///<summary>Remote 2 Action</summary>
		public SensorEntity Remote2Action => new(_haContext, "sensor.remote_2_action");
		///<summary>Remote 3 Action</summary>
		public SensorEntity Remote3Action => new(_haContext, "sensor.remote_3_action");
		///<summary>Remote 3 Click</summary>
		public SensorEntity Remote3Click => new(_haContext, "sensor.remote_3_click");
		///<summary>RingHpCam-49 Uptime</summary>
		public SensorEntity Ringhpcam49Uptime => new(_haContext, "sensor.ringhpcam_49_uptime");
		///<summary>RingHpCam-4c Uptime</summary>
		public SensorEntity Ringhpcam4cUptime => new(_haContext, "sensor.ringhpcam_4c_uptime");
		///<summary>RingPro-d6 Uptime</summary>
		public SensorEntity RingproD6Uptime => new(_haContext, "sensor.ringpro_d6_uptime");
		///<summary>RingStickUpCam-94 Uptime</summary>
		public SensorEntity Ringstickupcam94Uptime => new(_haContext, "sensor.ringstickupcam_94_uptime");
		///<summary>RingStickUpCam-9b Uptime</summary>
		public SensorEntity Ringstickupcam9bUptime => new(_haContext, "sensor.ringstickupcam_9b_uptime");
		///<summary>RMMINI-d9-2b-62 Uptime</summary>
		public SensorEntity RmminiD92b62Uptime => new(_haContext, "sensor.rmmini_d9_2b_62_uptime");
		///<summary>Sammi-Leigh-s-A52 Uptime</summary>
		public SensorEntity SammiLeighSA52Uptime => new(_haContext, "sensor.sammi_leigh_s_a52_uptime");
		///<summary>shelly1-55E8B5 Uptime</summary>
		public SensorEntity Shelly155e8b5Uptime => new(_haContext, "sensor.shelly1_55e8b5_uptime");
		///<summary>shelly1-BA6C98 Uptime</summary>
		public SensorEntity Shelly1Ba6c98Uptime => new(_haContext, "sensor.shelly1_ba6c98_uptime");
		///<summary>Side Snapshot Last Updated</summary>
		public SensorEntity SideSnapshotLastUpdated => new(_haContext, "sensor.side_snapshot_last_updated");
		///<summary>smart-plug-1 Uptime</summary>
		public SensorEntity SmartPlug1Uptime => new(_haContext, "sensor.smart_plug_1_uptime");
		///<summary>smart-plug-2 Uptime</summary>
		public SensorEntity SmartPlug2Uptime => new(_haContext, "sensor.smart_plug_2_uptime");
		///<summary>smart-plug-4 Uptime</summary>
		public SensorEntity SmartPlug4Uptime => new(_haContext, "sensor.smart_plug_4_uptime");
		///<summary>SonosZP Uptime</summary>
		public SensorEntity SonoszpUptime => new(_haContext, "sensor.sonoszp_uptime");
		///<summary>SonosZP Uptime</summary>
		public SensorEntity SonoszpUptime2 => new(_haContext, "sensor.sonoszp_uptime_2");
		///<summary>Study1 Light Last Seen</summary>
		public SensorEntity Study1LightLastSeen => new(_haContext, "sensor.study1_light_last_seen");
		///<summary>Study2 Light Last Seen</summary>
		public SensorEntity Study2LightLastSeen => new(_haContext, "sensor.study2_light_last_seen");
		///<summary>Study3 Light Last Seen</summary>
		public SensorEntity Study3LightLastSeen => new(_haContext, "sensor.study3_light_last_seen");
		///<summary>Suspect Device Uptime</summary>
		public SensorEntity SuspectDeviceUptime => new(_haContext, "sensor.suspect_device_uptime");
		///<summary>Suspect Huawei Uptime</summary>
		public SensorEntity SuspectHuaweiUptime => new(_haContext, "sensor.suspect_huawei_uptime");
		///<summary>Last Motion</summary>
		public SensorEntity TemplateLastMotion => new(_haContext, "sensor.template_last_motion");
		///<summary>Last Motion Downstairs</summary>
		public SensorEntity TemplateLastMotionDownstairs => new(_haContext, "sensor.template_last_motion_downstairs");
		///<summary>Last Motion Upstairs</summary>
		public SensorEntity TemplateLastMotionUpstairs => new(_haContext, "sensor.template_last_motion_upstairs");
		///<summary>This Device next Alarm</summary>
		public SensorEntity ThisDeviceNextAlarm => new(_haContext, "sensor.this_device_next_alarm");
		///<summary>This Device next Alarm</summary>
		public SensorEntity ThisDeviceNextAlarm2 => new(_haContext, "sensor.this_device_next_alarm_2");
		///<summary>This Device next Reminder</summary>
		public SensorEntity ThisDeviceNextReminder => new(_haContext, "sensor.this_device_next_reminder");
		///<summary>This Device next Reminder</summary>
		public SensorEntity ThisDeviceNextReminder2 => new(_haContext, "sensor.this_device_next_reminder_2");
		///<summary>This Device next Timer</summary>
		public SensorEntity ThisDeviceNextTimer => new(_haContext, "sensor.this_device_next_timer");
		///<summary>This Device next Timer</summary>
		public SensorEntity ThisDeviceNextTimer2 => new(_haContext, "sensor.this_device_next_timer_2");
		///<summary>Toilet Light Last Seen</summary>
		public SensorEntity ToiletLightLastSeen => new(_haContext, "sensor.toilet_light_last_seen");
		///<summary>Toilet Motion Last Seen</summary>
		public SensorEntity ToiletMotionLastSeen => new(_haContext, "sensor.toilet_motion_last_seen");
		///<summary>Toilet Window Last Seen</summary>
		public SensorEntity ToiletWindowLastSeen => new(_haContext, "sensor.toilet_window_last_seen");
		///<summary>Drying Time</summary>
		public SensorEntity TumbleDryerDryerCompletionTime => new(_haContext, "sensor.tumble_dryer_dryer_completion_time");
		///<summary>Dryer Cycle</summary>
		public SensorEntity TumbleDryerDryerJobState => new(_haContext, "sensor.tumble_dryer_dryer_job_state");
		///<summary>Dryer State</summary>
		public SensorEntity TumbleDryerDryerMachineState => new(_haContext, "sensor.tumble_dryer_dryer_machine_state");
		///<summary> Uptime</summary>
		public SensorEntity Uptime => new(_haContext, "sensor.uptime");
		///<summary> Uptime</summary>
		public SensorEntity Uptime10 => new(_haContext, "sensor.uptime_10");
		///<summary> Uptime</summary>
		public SensorEntity Uptime11 => new(_haContext, "sensor.uptime_11");
		///<summary> Uptime</summary>
		public SensorEntity Uptime12 => new(_haContext, "sensor.uptime_12");
		///<summary> Uptime</summary>
		public SensorEntity Uptime13 => new(_haContext, "sensor.uptime_13");
		///<summary> Uptime</summary>
		public SensorEntity Uptime14 => new(_haContext, "sensor.uptime_14");
		///<summary> Uptime</summary>
		public SensorEntity Uptime15 => new(_haContext, "sensor.uptime_15");
		///<summary>SonosZP Uptime</summary>
		public SensorEntity Uptime16 => new(_haContext, "sensor.uptime_16");
		///<summary> Uptime</summary>
		public SensorEntity Uptime17 => new(_haContext, "sensor.uptime_17");
		///<summary> Uptime</summary>
		public SensorEntity Uptime18 => new(_haContext, "sensor.uptime_18");
		///<summary> Uptime</summary>
		public SensorEntity Uptime19 => new(_haContext, "sensor.uptime_19");
		///<summary> Uptime</summary>
		public SensorEntity Uptime2 => new(_haContext, "sensor.uptime_2");
		///<summary> Uptime</summary>
		public SensorEntity Uptime20 => new(_haContext, "sensor.uptime_20");
		///<summary> Uptime</summary>
		public SensorEntity Uptime21 => new(_haContext, "sensor.uptime_21");
		///<summary> Uptime</summary>
		public SensorEntity Uptime22 => new(_haContext, "sensor.uptime_22");
		///<summary> Uptime</summary>
		public SensorEntity Uptime23 => new(_haContext, "sensor.uptime_23");
		///<summary>Jayden iPad Uptime</summary>
		public SensorEntity Uptime24 => new(_haContext, "sensor.uptime_24");
		///<summary> Uptime</summary>
		public SensorEntity Uptime25 => new(_haContext, "sensor.uptime_25");
		///<summary> Uptime</summary>
		public SensorEntity Uptime26 => new(_haContext, "sensor.uptime_26");
		///<summary> Uptime</summary>
		public SensorEntity Uptime27 => new(_haContext, "sensor.uptime_27");
		///<summary> Uptime</summary>
		public SensorEntity Uptime28 => new(_haContext, "sensor.uptime_28");
		///<summary> Uptime</summary>
		public SensorEntity Uptime3 => new(_haContext, "sensor.uptime_3");
		///<summary> Uptime</summary>
		public SensorEntity Uptime4 => new(_haContext, "sensor.uptime_4");
		///<summary> Uptime</summary>
		public SensorEntity Uptime5 => new(_haContext, "sensor.uptime_5");
		///<summary> Uptime</summary>
		public SensorEntity Uptime6 => new(_haContext, "sensor.uptime_6");
		///<summary> Uptime</summary>
		public SensorEntity Uptime7 => new(_haContext, "sensor.uptime_7");
		///<summary> Uptime</summary>
		public SensorEntity Uptime8 => new(_haContext, "sensor.uptime_8");
		///<summary>Jayden RaspberryPi Uptime</summary>
		public SensorEntity Uptime9 => new(_haContext, "sensor.uptime_9");
		///<summary>Utility 1 Light Last Seen</summary>
		public SensorEntity Utility1LightLastSeen => new(_haContext, "sensor.utility_1_light_last_seen");
		///<summary>Utility 2 Light Last Seen</summary>
		public SensorEntity Utility2LightLastSeen => new(_haContext, "sensor.utility_2_light_last_seen");
		///<summary>Utility 3 Light Last Seen</summary>
		public SensorEntity Utility3LightLastSeen => new(_haContext, "sensor.utility_3_light_last_seen");
		///<summary>Utility Motion Last Seen</summary>
		public SensorEntity UtilityMotionLastSeen => new(_haContext, "sensor.utility_motion_last_seen");
		///<summary>Wallpanel Fire HD8 Uptime</summary>
		public SensorEntity WallpanelFireHd8Uptime => new(_haContext, "sensor.wallpanel_fire_hd8_uptime");
		///<summary>Wallpanel next Alarm</summary>
		public SensorEntity WallpanelNextAlarm => new(_haContext, "sensor.wallpanel_next_alarm");
		///<summary>Wallpanel next Reminder</summary>
		public SensorEntity WallpanelNextReminder => new(_haContext, "sensor.wallpanel_next_reminder");
		///<summary>Wallpanel next Timer</summary>
		public SensorEntity WallpanelNextTimer => new(_haContext, "sensor.wallpanel_next_timer");
		///<summary>washer Uptime</summary>
		public SensorEntity WasherUptime => new(_haContext, "sensor.washer_uptime");
		///<summary>Washing Time</summary>
		public SensorEntity WashingMachineWasherCompletionTime => new(_haContext, "sensor.washing_machine_washer_completion_time");
		///<summary>Washing Cycle</summary>
		public SensorEntity WashingMachineWasherJobState => new(_haContext, "sensor.washing_machine_washer_job_state");
		///<summary>Washing State</summary>
		public SensorEntity WashingMachineWasherMachineState => new(_haContext, "sensor.washing_machine_washer_machine_state");
		///<summary>Wiser Cloud Status</summary>
		public SensorEntity WiserCloudStatus => new(_haContext, "sensor.wiser_cloud_status");
		///<summary>Wiser Heathub</summary>
		public SensorEntity WiserHeathub => new(_haContext, "sensor.wiser_heathub");
		///<summary>Wiser Heating</summary>
		public SensorEntity WiserHeating => new(_haContext, "sensor.wiser_heating");
		///<summary>Wiser Hot Water</summary>
		public SensorEntity WiserHotWater => new(_haContext, "sensor.wiser_hot_water");
		///<summary>Wiser iTRV-Boys</summary>
		public SensorEntity WiserItrvBoys => new(_haContext, "sensor.wiser_itrv_boys");
		///<summary>Wiser iTRV-Dining</summary>
		public SensorEntity WiserItrvDining => new(_haContext, "sensor.wiser_itrv_dining");
		///<summary>Wiser iTRV-Entrance</summary>
		public SensorEntity WiserItrvEntrance => new(_haContext, "sensor.wiser_itrv_entrance");
		///<summary>Wiser iTRV-Guest Room</summary>
		public SensorEntity WiserItrvGuestRoom => new(_haContext, "sensor.wiser_itrv_guest_room");
		///<summary>Wiser iTRV-Landing</summary>
		public SensorEntity WiserItrvLanding => new(_haContext, "sensor.wiser_itrv_landing");
		///<summary>Wiser iTRV-Lounge </summary>
		public SensorEntity WiserItrvLounge => new(_haContext, "sensor.wiser_itrv_lounge");
		///<summary>Wiser iTRV-Lounge Bay</summary>
		public SensorEntity WiserItrvLoungeBay => new(_haContext, "sensor.wiser_itrv_lounge_bay");
		///<summary>Wiser iTRV-Master</summary>
		public SensorEntity WiserItrvMaster => new(_haContext, "sensor.wiser_itrv_master");
		///<summary>Wiser iTRV-Office</summary>
		public SensorEntity WiserItrvOffice => new(_haContext, "sensor.wiser_itrv_office");
		///<summary>Wiser iTRV-Playroom</summary>
		public SensorEntity WiserItrvPlayroom => new(_haContext, "sensor.wiser_itrv_playroom");
		///<summary>Wiser iTRV-Utility</summary>
		public SensorEntity WiserItrvUtility => new(_haContext, "sensor.wiser_itrv_utility");
		///<summary>Wiser Operation Mode</summary>
		public SensorEntity WiserOperationMode => new(_haContext, "sensor.wiser_operation_mode");
		///<summary>Wiser RoomStat-Utility</summary>
		public SensorEntity WiserRoomstatUtility => new(_haContext, "sensor.wiser_roomstat_utility");
		///<summary>WiserHeat031C5E Uptime</summary>
		public SensorEntity Wiserheat031c5eUptime => new(_haContext, "sensor.wiserheat031c5e_uptime");
	}

	public class SunEntities
	{
		private readonly IHaContext _haContext;
		public SunEntities(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Sun</summary>
		public SunEntity Sun => new(_haContext, "sun.sun");
	}

	public class SwitchEntities
	{
		private readonly IHaContext _haContext;
		public SwitchEntities(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Playroom do not disturb switch</summary>
		public SwitchEntity AaronDoNotDisturbSwitch => new(_haContext, "switch.aaron_do_not_disturb_switch");
		///<summary>Playroom repeat switch</summary>
		public SwitchEntity AaronRepeatSwitch => new(_haContext, "switch.aaron_repeat_switch");
		///<summary>Playroom shuffle switch</summary>
		public SwitchEntity AaronShuffleSwitch => new(_haContext, "switch.aaron_shuffle_switch");
		///<summary>Adaptive Lighting Adapt Brightness: Entrance</summary>
		public SwitchEntity AdaptiveLightingAdaptBrightnessEntrance => new(_haContext, "switch.adaptive_lighting_adapt_brightness_entrance");
		///<summary>Adaptive Lighting Adapt Brightness: Floor </summary>
		public SwitchEntity AdaptiveLightingAdaptBrightnessFloor => new(_haContext, "switch.adaptive_lighting_adapt_brightness_floor");
		///<summary>Adaptive Lighting Adapt Brightness: Hallway</summary>
		public SwitchEntity AdaptiveLightingAdaptBrightnessHallway => new(_haContext, "switch.adaptive_lighting_adapt_brightness_hallway");
		///<summary>Adaptive Lighting Adapt Brightness: Jayden</summary>
		public SwitchEntity AdaptiveLightingAdaptBrightnessJayden => new(_haContext, "switch.adaptive_lighting_adapt_brightness_jayden");
		///<summary>Adaptive Lighting Adapt Brightness: Kitchen</summary>
		public SwitchEntity AdaptiveLightingAdaptBrightnessKitchen => new(_haContext, "switch.adaptive_lighting_adapt_brightness_kitchen");
		///<summary>Adaptive Lighting Adapt Brightness: Landing</summary>
		public SwitchEntity AdaptiveLightingAdaptBrightnessLanding => new(_haContext, "switch.adaptive_lighting_adapt_brightness_landing");
		///<summary>Adaptive Lighting Adapt Brightness: Lounge</summary>
		public SwitchEntity AdaptiveLightingAdaptBrightnessLounge => new(_haContext, "switch.adaptive_lighting_adapt_brightness_lounge");
		///<summary>Adaptive Lighting Adapt Brightness: Master nightlight</summary>
		public SwitchEntity AdaptiveLightingAdaptBrightnessMasterNightlight => new(_haContext, "switch.adaptive_lighting_adapt_brightness_master_nightlight");
		///<summary>Adaptive Lighting Adapt Brightness: Study</summary>
		public SwitchEntity AdaptiveLightingAdaptBrightnessStudy => new(_haContext, "switch.adaptive_lighting_adapt_brightness_study");
		///<summary>Adaptive Lighting Adapt Brightness: Toilet</summary>
		public SwitchEntity AdaptiveLightingAdaptBrightnessToilet => new(_haContext, "switch.adaptive_lighting_adapt_brightness_toilet");
		///<summary>Adaptive Lighting Adapt Brightness: Utility</summary>
		public SwitchEntity AdaptiveLightingAdaptBrightnessUtility => new(_haContext, "switch.adaptive_lighting_adapt_brightness_utility");
		///<summary>Adaptive Lighting Adapt Color: Entrance</summary>
		public SwitchEntity AdaptiveLightingAdaptColorEntrance => new(_haContext, "switch.adaptive_lighting_adapt_color_entrance");
		///<summary>Adaptive Lighting Adapt Color: Floor </summary>
		public SwitchEntity AdaptiveLightingAdaptColorFloor => new(_haContext, "switch.adaptive_lighting_adapt_color_floor");
		///<summary>Adaptive Lighting Adapt Color: Hallway</summary>
		public SwitchEntity AdaptiveLightingAdaptColorHallway => new(_haContext, "switch.adaptive_lighting_adapt_color_hallway");
		///<summary>Adaptive Lighting Adapt Color: Jayden</summary>
		public SwitchEntity AdaptiveLightingAdaptColorJayden => new(_haContext, "switch.adaptive_lighting_adapt_color_jayden");
		///<summary>Adaptive Lighting Adapt Color: Kitchen</summary>
		public SwitchEntity AdaptiveLightingAdaptColorKitchen => new(_haContext, "switch.adaptive_lighting_adapt_color_kitchen");
		///<summary>Adaptive Lighting Adapt Color: Landing</summary>
		public SwitchEntity AdaptiveLightingAdaptColorLanding => new(_haContext, "switch.adaptive_lighting_adapt_color_landing");
		///<summary>Adaptive Lighting Adapt Color: Lounge</summary>
		public SwitchEntity AdaptiveLightingAdaptColorLounge => new(_haContext, "switch.adaptive_lighting_adapt_color_lounge");
		///<summary>Adaptive Lighting Adapt Color: Master nightlight</summary>
		public SwitchEntity AdaptiveLightingAdaptColorMasterNightlight => new(_haContext, "switch.adaptive_lighting_adapt_color_master_nightlight");
		///<summary>Adaptive Lighting Adapt Color: Study</summary>
		public SwitchEntity AdaptiveLightingAdaptColorStudy => new(_haContext, "switch.adaptive_lighting_adapt_color_study");
		///<summary>Adaptive Lighting Adapt Color: Toilet</summary>
		public SwitchEntity AdaptiveLightingAdaptColorToilet => new(_haContext, "switch.adaptive_lighting_adapt_color_toilet");
		///<summary>Adaptive Lighting Adapt Color: Utility</summary>
		public SwitchEntity AdaptiveLightingAdaptColorUtility => new(_haContext, "switch.adaptive_lighting_adapt_color_utility");
		///<summary>Adaptive Lighting: Entrance</summary>
		public SwitchEntity AdaptiveLightingEntrance => new(_haContext, "switch.adaptive_lighting_entrance");
		///<summary>Adaptive Lighting: Floor </summary>
		public SwitchEntity AdaptiveLightingFloor => new(_haContext, "switch.adaptive_lighting_floor");
		///<summary>Adaptive Lighting: Hallway</summary>
		public SwitchEntity AdaptiveLightingHallway => new(_haContext, "switch.adaptive_lighting_hallway");
		///<summary>Adaptive Lighting: Jayden</summary>
		public SwitchEntity AdaptiveLightingJayden => new(_haContext, "switch.adaptive_lighting_jayden");
		///<summary>Adaptive Lighting: Kitchen</summary>
		public SwitchEntity AdaptiveLightingKitchen => new(_haContext, "switch.adaptive_lighting_kitchen");
		///<summary>Adaptive Lighting: Landing</summary>
		public SwitchEntity AdaptiveLightingLanding => new(_haContext, "switch.adaptive_lighting_landing");
		///<summary>Adaptive Lighting: Lounge</summary>
		public SwitchEntity AdaptiveLightingLounge => new(_haContext, "switch.adaptive_lighting_lounge");
		///<summary>Adaptive Lighting: Master nightlight</summary>
		public SwitchEntity AdaptiveLightingMasterNightlight => new(_haContext, "switch.adaptive_lighting_master_nightlight");
		///<summary>Adaptive Lighting Sleep Mode: Entrance</summary>
		public SwitchEntity AdaptiveLightingSleepModeEntrance => new(_haContext, "switch.adaptive_lighting_sleep_mode_entrance");
		///<summary>Adaptive Lighting Sleep Mode: Floor </summary>
		public SwitchEntity AdaptiveLightingSleepModeFloor => new(_haContext, "switch.adaptive_lighting_sleep_mode_floor");
		///<summary>Adaptive Lighting Sleep Mode: Hallway</summary>
		public SwitchEntity AdaptiveLightingSleepModeHallway => new(_haContext, "switch.adaptive_lighting_sleep_mode_hallway");
		///<summary>Adaptive Lighting Sleep Mode: Jayden</summary>
		public SwitchEntity AdaptiveLightingSleepModeJayden => new(_haContext, "switch.adaptive_lighting_sleep_mode_jayden");
		///<summary>Adaptive Lighting Sleep Mode: Kitchen</summary>
		public SwitchEntity AdaptiveLightingSleepModeKitchen => new(_haContext, "switch.adaptive_lighting_sleep_mode_kitchen");
		///<summary>Adaptive Lighting Sleep Mode: Landing</summary>
		public SwitchEntity AdaptiveLightingSleepModeLanding => new(_haContext, "switch.adaptive_lighting_sleep_mode_landing");
		///<summary>Adaptive Lighting Sleep Mode: Lounge</summary>
		public SwitchEntity AdaptiveLightingSleepModeLounge => new(_haContext, "switch.adaptive_lighting_sleep_mode_lounge");
		///<summary>Adaptive Lighting Sleep Mode: Master nightlight</summary>
		public SwitchEntity AdaptiveLightingSleepModeMasterNightlight => new(_haContext, "switch.adaptive_lighting_sleep_mode_master_nightlight");
		///<summary>Adaptive Lighting Sleep Mode: Study</summary>
		public SwitchEntity AdaptiveLightingSleepModeStudy => new(_haContext, "switch.adaptive_lighting_sleep_mode_study");
		///<summary>Adaptive Lighting Sleep Mode: Toilet</summary>
		public SwitchEntity AdaptiveLightingSleepModeToilet => new(_haContext, "switch.adaptive_lighting_sleep_mode_toilet");
		///<summary>Adaptive Lighting Sleep Mode: Utility</summary>
		public SwitchEntity AdaptiveLightingSleepModeUtility => new(_haContext, "switch.adaptive_lighting_sleep_mode_utility");
		///<summary>Adaptive Lighting: Study</summary>
		public SwitchEntity AdaptiveLightingStudy => new(_haContext, "switch.adaptive_lighting_study");
		///<summary>Adaptive Lighting: Toilet</summary>
		public SwitchEntity AdaptiveLightingToilet => new(_haContext, "switch.adaptive_lighting_toilet");
		///<summary>Adaptive Lighting: Utility</summary>
		public SwitchEntity AdaptiveLightingUtility => new(_haContext, "switch.adaptive_lighting_utility");
		///<summary>Alarm Beep Infinate</summary>
		public SwitchEntity AlarmBeepInfinate => new(_haContext, "switch.alarm_beep_infinate");
		///<summary>Alarm Single Beep</summary>
		public SwitchEntity AlarmBeepOne => new(_haContext, "switch.alarm_beep_one");
		///<summary>Alarm Triple Beep</summary>
		public SwitchEntity AlarmBeepThree => new(_haContext, "switch.alarm_beep_three");
		///<summary>Alarm Double Beep</summary>
		public SwitchEntity AlarmBeepTwo => new(_haContext, "switch.alarm_beep_two");
		///<summary>Alarm Siren Beep Two</summary>
		public SwitchEntity AlarmSirenBeepTwo2 => new(_haContext, "switch.alarm_siren_beep_two_2");
		///<summary>Aubrecia Drive siren</summary>
		public SwitchEntity AubreciaDriveSiren => new(_haContext, "switch.aubrecia_drive_siren");
		///<summary>Chime Play Ding</summary>
		public SwitchEntity ChimePlayDing => new(_haContext, "switch.chime_play_ding");
		///<summary>Chime Play Motion</summary>
		public SwitchEntity ChimePlayMotion => new(_haContext, "switch.chime_play_motion");
		///<summary>Chime Snooze</summary>
		public SwitchEntity ChimeSnooze => new(_haContext, "switch.chime_snooze");
		///<summary>Christmas Indoor Sonoff</summary>
		public SwitchEntity ChristmasIndoorSonoff => new(_haContext, "switch.christmas_indoor_sonoff");
		///<summary>Circadian Aaron</summary>
		public SwitchEntity CircadianLightingCircadianAaron => new(_haContext, "switch.circadian_lighting_circadian_aaron");
		///<summary>Circadian Arron</summary>
		public SwitchEntity CircadianLightingCircadianArron => new(_haContext, "switch.circadian_lighting_circadian_arron");
		///<summary>Circadian Dining</summary>
		public SwitchEntity CircadianLightingCircadianDining => new(_haContext, "switch.circadian_lighting_circadian_dining");
		///<summary>Circadian Master</summary>
		public SwitchEntity CircadianLightingCircadianMaster => new(_haContext, "switch.circadian_lighting_circadian_master");
		///<summary>Discipline Manager</summary>
		public SwitchEntity DisciplineManagerEnabled => new(_haContext, "switch.discipline_manager_enabled");
		///<summary>Downstairs do not disturb switch</summary>
		public SwitchEntity DownstairsDoNotDisturbSwitch => new(_haContext, "switch.downstairs_do_not_disturb_switch");
		///<summary>Downstairs Play Ding Sound</summary>
		public SwitchEntity DownstairsPlayDingSound => new(_haContext, "switch.downstairs_play_ding_sound");
		///<summary>Downstairs Play Motion Sound</summary>
		public SwitchEntity DownstairsPlayMotionSound => new(_haContext, "switch.downstairs_play_motion_sound");
		///<summary>Downstairs repeat switch</summary>
		public SwitchEntity DownstairsRepeatSwitch => new(_haContext, "switch.downstairs_repeat_switch");
		///<summary>Downstairs shuffle switch</summary>
		public SwitchEntity DownstairsShuffleSwitch => new(_haContext, "switch.downstairs_shuffle_switch");
		///<summary>Downstairs Snooze</summary>
		public SwitchEntity DownstairsSnooze => new(_haContext, "switch.downstairs_snooze");
		///<summary>Entrance</summary>
		public SwitchEntity Entrance => new(_haContext, "switch.entrance");
		///<summary>Garden do not disturb switch</summary>
		public SwitchEntity EugeneS2ndEchoDotDoNotDisturbSwitch => new(_haContext, "switch.eugene_s_2nd_echo_dot_do_not_disturb_switch");
		///<summary>Garden repeat switch</summary>
		public SwitchEntity EugeneS2ndEchoDotRepeatSwitch => new(_haContext, "switch.eugene_s_2nd_echo_dot_repeat_switch");
		///<summary>Garden shuffle switch</summary>
		public SwitchEntity EugeneS2ndEchoDotShuffleSwitch => new(_haContext, "switch.eugene_s_2nd_echo_dot_shuffle_switch");
		///<summary>Spare echo do not disturb switch</summary>
		public SwitchEntity EugeneS3rdEchoDotDoNotDisturbSwitch => new(_haContext, "switch.eugene_s_3rd_echo_dot_do_not_disturb_switch");
		///<summary>Spare echo repeat switch</summary>
		public SwitchEntity EugeneS3rdEchoDotRepeatSwitch => new(_haContext, "switch.eugene_s_3rd_echo_dot_repeat_switch");
		///<summary>Spare echo shuffle switch</summary>
		public SwitchEntity EugeneS3rdEchoDotShuffleSwitch => new(_haContext, "switch.eugene_s_3rd_echo_dot_shuffle_switch");
		///<summary>Lounge LG Alexa do not disturb switch</summary>
		public SwitchEntity EugeneSLgOledWebos2021TvDoNotDisturbSwitch => new(_haContext, "switch.eugene_s_lg_oled_webos_2021_tv_do_not_disturb_switch");
		///<summary>Lounge LG Alexa repeat switch</summary>
		public SwitchEntity EugeneSLgOledWebos2021TvRepeatSwitch => new(_haContext, "switch.eugene_s_lg_oled_webos_2021_tv_repeat_switch");
		///<summary>Lounge LG Alexa shuffle switch</summary>
		public SwitchEntity EugeneSLgOledWebos2021TvShuffleSwitch => new(_haContext, "switch.eugene_s_lg_oled_webos_2021_tv_shuffle_switch");
		///<summary>Master LG Alexa do not disturb switch</summary>
		public SwitchEntity EugeneSLgWebos2020TvDoNotDisturbSwitch => new(_haContext, "switch.eugene_s_lg_webos_2020_tv_do_not_disturb_switch");
		///<summary>Master LG Alexa repeat switch</summary>
		public SwitchEntity EugeneSLgWebos2020TvRepeatSwitch => new(_haContext, "switch.eugene_s_lg_webos_2020_tv_repeat_switch");
		///<summary>Master LG Alexa shuffle switch</summary>
		public SwitchEntity EugeneSLgWebos2020TvShuffleSwitch => new(_haContext, "switch.eugene_s_lg_webos_2020_tv_shuffle_switch");
		///<summary>Lounge do not disturb switch</summary>
		public SwitchEntity EugeneSSonosArcDoNotDisturbSwitch => new(_haContext, "switch.eugene_s_sonos_arc_do_not_disturb_switch");
		///<summary>Lounge repeat switch</summary>
		public SwitchEntity EugeneSSonosArcRepeatSwitch => new(_haContext, "switch.eugene_s_sonos_arc_repeat_switch");
		///<summary>Lounge shuffle switch</summary>
		public SwitchEntity EugeneSSonosArcShuffleSwitch => new(_haContext, "switch.eugene_s_sonos_arc_shuffle_switch");
		///<summary>Everywhere do not disturb switch</summary>
		public SwitchEntity EverywhereDoNotDisturbSwitch => new(_haContext, "switch.everywhere_do_not_disturb_switch");
		///<summary>Everywhere repeat switch</summary>
		public SwitchEntity EverywhereRepeatSwitch => new(_haContext, "switch.everywhere_repeat_switch");
		///<summary>Everywhere shuffle switch</summary>
		public SwitchEntity EverywhereShuffleSwitch => new(_haContext, "switch.everywhere_shuffle_switch");
		///<summary>Floor Sonoff</summary>
		public SwitchEntity FloorSonoff => new(_haContext, "switch.floor_sonoff");
		///<summary>Garage do not disturb switch</summary>
		public SwitchEntity GarageDoNotDisturbSwitch => new(_haContext, "switch.garage_do_not_disturb_switch");
		///<summary>Garage repeat switch</summary>
		public SwitchEntity GarageRepeatSwitch => new(_haContext, "switch.garage_repeat_switch");
		///<summary>Garage shuffle switch</summary>
		public SwitchEntity GarageShuffleSwitch => new(_haContext, "switch.garage_shuffle_switch");
		///<summary>Garden siren</summary>
		public SwitchEntity GardenSiren2 => new(_haContext, "switch.garden_siren_2");
		///<summary>Jayden</summary>
		public SwitchEntity Jayden => new(_haContext, "switch.jayden");
		///<summary>Jayden AppleTv</summary>
		public SwitchEntity JaydenAppletv => new(_haContext, "switch.jayden_appletv");
		///<summary>Jayden Bedside</summary>
		public SwitchEntity JaydenBedside => new(_haContext, "switch.jayden_bedside");
		///<summary>Aaron do not disturb switch</summary>
		public SwitchEntity JaydenDoNotDisturbSwitch => new(_haContext, "switch.jayden_do_not_disturb_switch");
		///<summary>Jayden do not disturb switch</summary>
		public SwitchEntity JaydenDoNotDisturbSwitch2 => new(_haContext, "switch.jayden_do_not_disturb_switch_2");
		///<summary>Jayden iPad</summary>
		public SwitchEntity JaydenIpad => new(_haContext, "switch.jayden_ipad");
		///<summary>Jayden RaspberryPi</summary>
		public SwitchEntity JaydenRaspberrypi => new(_haContext, "switch.jayden_raspberrypi");
		///<summary>Aaron repeat switch</summary>
		public SwitchEntity JaydenRepeatSwitch => new(_haContext, "switch.jayden_repeat_switch");
		///<summary>Jayden repeat switch</summary>
		public SwitchEntity JaydenRepeatSwitch2 => new(_haContext, "switch.jayden_repeat_switch_2");
		///<summary>Aaron shuffle switch</summary>
		public SwitchEntity JaydenShuffleSwitch => new(_haContext, "switch.jayden_shuffle_switch");
		///<summary>Jayden shuffle switch</summary>
		public SwitchEntity JaydenShuffleSwitch2 => new(_haContext, "switch.jayden_shuffle_switch_2");
		///<summary>Kitchen do not disturb switch</summary>
		public SwitchEntity KitchenDoNotDisturbSwitch => new(_haContext, "switch.kitchen_do_not_disturb_switch");
		///<summary>Kitchen repeat switch</summary>
		public SwitchEntity KitchenRepeatSwitch => new(_haContext, "switch.kitchen_repeat_switch");
		///<summary>Kitchen shuffle switch</summary>
		public SwitchEntity KitchenShuffleSwitch => new(_haContext, "switch.kitchen_shuffle_switch");
		///<summary>Landing Night</summary>
		public SwitchEntity LandingNight => new(_haContext, "switch.landing_night");
		///<summary>LG TV</summary>
		public SwitchEntity LgTv => new(_haContext, "switch.lg_tv");
		///<summary>Light Manager Aaron</summary>
		public SwitchEntity LightManagerAaron => new(_haContext, "switch.light_manager_aaron");
		///<summary>Light Manager Dining</summary>
		public SwitchEntity LightManagerDining => new(_haContext, "switch.light_manager_dining");
		///<summary>Light Manager Drive</summary>
		public SwitchEntity LightManagerDrive => new(_haContext, "switch.light_manager_drive");
		///<summary>Light Manager Entrance</summary>
		public SwitchEntity LightManagerEntrance => new(_haContext, "switch.light_manager_entrance");
		///<summary>Light Manager Fish</summary>
		public SwitchEntity LightManagerFish => new(_haContext, "switch.light_manager_fish");
		///<summary>Light Manager Garage</summary>
		public SwitchEntity LightManagerGarage => new(_haContext, "switch.light_manager_garage");
		///<summary>Light Manager Garden</summary>
		public SwitchEntity LightManagerGarden => new(_haContext, "switch.light_manager_garden");
		///<summary>Light Manager Hallway</summary>
		public SwitchEntity LightManagerHallway => new(_haContext, "switch.light_manager_hallway");
		///<summary>Light Manager Jayden</summary>
		public SwitchEntity LightManagerJayden => new(_haContext, "switch.light_manager_jayden");
		///<summary>Light Manager Kitchen</summary>
		public SwitchEntity LightManagerKitchen => new(_haContext, "switch.light_manager_kitchen");
		///<summary>Light Manager Landing</summary>
		public SwitchEntity LightManagerLanding => new(_haContext, "switch.light_manager_landing");
		///<summary>Light Manager Lounge</summary>
		public SwitchEntity LightManagerLounge => new(_haContext, "switch.light_manager_lounge");
		///<summary>Light Manager Master</summary>
		public SwitchEntity LightManagerMaster => new(_haContext, "switch.light_manager_master");
		///<summary>Light Manager Playroom</summary>
		public SwitchEntity LightManagerPlayroom => new(_haContext, "switch.light_manager_playroom");
		///<summary>Light Manager Porch</summary>
		public SwitchEntity LightManagerPorch => new(_haContext, "switch.light_manager_porch");
		///<summary>Light Manager Study</summary>
		public SwitchEntity LightManagerStudy => new(_haContext, "switch.light_manager_study");
		///<summary>Light Manager Toilet</summary>
		public SwitchEntity LightManagerToilet => new(_haContext, "switch.light_manager_toilet");
		///<summary>Light Manager Utility</summary>
		public SwitchEntity LightManagerUtility => new(_haContext, "switch.light_manager_utility");
		///<summary>Dining do not disturb switch</summary>
		public SwitchEntity LoungeGroupDoNotDisturbSwitch => new(_haContext, "switch.lounge_group_do_not_disturb_switch");
		///<summary>Dining repeat switch</summary>
		public SwitchEntity LoungeGroupRepeatSwitch => new(_haContext, "switch.lounge_group_repeat_switch");
		///<summary>Dining shuffle switch</summary>
		public SwitchEntity LoungeGroupShuffleSwitch => new(_haContext, "switch.lounge_group_shuffle_switch");
		///<summary>Lounge Sonos do not disturb switch</summary>
		public SwitchEntity LoungeSonosDoNotDisturbSwitch => new(_haContext, "switch.lounge_sonos_do_not_disturb_switch");
		///<summary>Lounge Sonos repeat switch</summary>
		public SwitchEntity LoungeSonosRepeatSwitch => new(_haContext, "switch.lounge_sonos_repeat_switch");
		///<summary>Lounge Sonos shuffle switch</summary>
		public SwitchEntity LoungeSonosShuffleSwitch => new(_haContext, "switch.lounge_sonos_shuffle_switch");
		///<summary>Master do not disturb switch</summary>
		public SwitchEntity MasterDoNotDisturbSwitch => new(_haContext, "switch.master_do_not_disturb_switch");
		///<summary>Master repeat switch</summary>
		public SwitchEntity MasterRepeatSwitch => new(_haContext, "switch.master_repeat_switch");
		///<summary>Master shuffle switch</summary>
		public SwitchEntity MasterShuffleSwitch => new(_haContext, "switch.master_shuffle_switch");
		///<summary>netdaemon_lightsmanager</summary>
		public SwitchEntity NetdaemonLightsmanager => new(_haContext, "switch.netdaemon_lightsmanager");
		///<summary>Niemand Drive Event Stream</summary>
		public SwitchEntity NiemandDriveEventStream => new(_haContext, "switch.niemand_drive_event_stream");
		///<summary>Niemand Drive Live Stream</summary>
		public SwitchEntity NiemandDriveLiveStream => new(_haContext, "switch.niemand_drive_live_stream");
		///<summary>Niemand Drive siren</summary>
		public SwitchEntity NiemandDriveSiren => new(_haContext, "switch.niemand_drive_siren");
		///<summary>Niemand Drive Siren</summary>
		public SwitchEntity NiemandDriveSiren2 => new(_haContext, "switch.niemand_drive_siren_2");
		///<summary>Niemand Front Door Event Stream</summary>
		public SwitchEntity NiemandFrontDoorEventStream => new(_haContext, "switch.niemand_front_door_event_stream");
		///<summary>Niemand Front Door Live Stream</summary>
		public SwitchEntity NiemandFrontDoorLiveStream => new(_haContext, "switch.niemand_front_door_live_stream");
		///<summary>Niemand Garage Event Stream</summary>
		public SwitchEntity NiemandGarageEventStream => new(_haContext, "switch.niemand_garage_event_stream");
		///<summary>Niemand Garage Live Stream</summary>
		public SwitchEntity NiemandGarageLiveStream => new(_haContext, "switch.niemand_garage_live_stream");
		///<summary>Niemand Garage siren</summary>
		public SwitchEntity NiemandGarageSiren => new(_haContext, "switch.niemand_garage_siren");
		///<summary>Niemand Garage Siren</summary>
		public SwitchEntity NiemandGarageSiren2 => new(_haContext, "switch.niemand_garage_siren_2");
		///<summary>Niemand Garden Event Stream</summary>
		public SwitchEntity NiemandGardenEventStream => new(_haContext, "switch.niemand_garden_event_stream");
		///<summary>Niemand Garden Live Stream</summary>
		public SwitchEntity NiemandGardenLiveStream => new(_haContext, "switch.niemand_garden_live_stream");
		///<summary>Niemand Garden siren</summary>
		public SwitchEntity NiemandGardenSiren => new(_haContext, "switch.niemand_garden_siren");
		///<summary>Niemand Garden Siren</summary>
		public SwitchEntity NiemandGardenSiren2 => new(_haContext, "switch.niemand_garden_siren_2");
		///<summary>Niemand Side Event Stream</summary>
		public SwitchEntity NiemandSideEventStream => new(_haContext, "switch.niemand_side_event_stream");
		///<summary>Niemand Side Live Stream</summary>
		public SwitchEntity NiemandSideLiveStream => new(_haContext, "switch.niemand_side_live_stream");
		///<summary>Niemand Side siren</summary>
		public SwitchEntity NiemandSideSiren => new(_haContext, "switch.niemand_side_siren");
		///<summary>Niemand Side Siren</summary>
		public SwitchEntity NiemandSideSiren2 => new(_haContext, "switch.niemand_side_siren_2");
		///<summary>Office do not disturb switch</summary>
		public SwitchEntity OfficeDoNotDisturbSwitch => new(_haContext, "switch.office_do_not_disturb_switch");
		///<summary>Office repeat switch</summary>
		public SwitchEntity OfficeRepeatSwitch => new(_haContext, "switch.office_repeat_switch");
		///<summary>Office shuffle switch</summary>
		public SwitchEntity OfficeShuffleSwitch => new(_haContext, "switch.office_shuffle_switch");
		///<summary>Office Skylight</summary>
		public SwitchEntity OfficeSkylight => new(_haContext, "switch.office_skylight");
		///<summary>Pi-Hole</summary>
		public SwitchEntity PiHole => new(_haContext, "switch.pi_hole");
		///<summary>Plug 1</summary>
		public SwitchEntity Plug1 => new(_haContext, "switch.plug_1");
		///<summary>Plug 2</summary>
		public SwitchEntity Plug2 => new(_haContext, "switch.plug_2");
		///<summary>Plug 3</summary>
		public SwitchEntity Plug3 => new(_haContext, "switch.plug_3");
		///<summary>Plug 4</summary>
		public SwitchEntity Plug4 => new(_haContext, "switch.plug_4");
		///<summary>Plug 5</summary>
		public SwitchEntity Plug5 => new(_haContext, "switch.plug_5");
		///<summary>Schedule #5a4aba</summary>
		public SwitchEntity Schedule5a4aba2 => new(_haContext, "switch.schedule_5a4aba_2");
		///<summary>Schedule #5ed2d5</summary>
		public SwitchEntity Schedule5ed2d5 => new(_haContext, "switch.schedule_5ed2d5");
		///<summary>Schedule #7b0cda</summary>
		public SwitchEntity Schedule7b0cda2 => new(_haContext, "switch.schedule_7b0cda_2");
		///<summary>Schedule #e5d71d</summary>
		public SwitchEntity ScheduleE5d71d => new(_haContext, "switch.schedule_e5d71d");
		///<summary>Turn TV off</summary>
		public SwitchEntity ScheduleTurnTvOff => new(_haContext, "switch.schedule_turn_tv_off");
		///<summary>siren</summary>
		public SwitchEntity Siren => new(_haContext, "switch.siren");
		///<summary>Lounge Crossfade</summary>
		public SwitchEntity SonosLoungeCrossfade => new(_haContext, "switch.sonos_lounge_crossfade");
		///<summary>Lounge Night Sound</summary>
		public SwitchEntity SonosLoungeNightSound => new(_haContext, "switch.sonos_lounge_night_sound");
		///<summary>Lounge Speech Enhancement</summary>
		public SwitchEntity SonosLoungeSpeechEnhancement => new(_haContext, "switch.sonos_lounge_speech_enhancement");
		///<summary>Lounge Surround Enabled</summary>
		public SwitchEntity SonosLoungeSurroundEnabled => new(_haContext, "switch.sonos_lounge_surround_enabled");
		///<summary>This Device do not disturb switch</summary>
		public SwitchEntity ThisDeviceDoNotDisturbSwitch => new(_haContext, "switch.this_device_do_not_disturb_switch");
		///<summary>This Device do not disturb switch</summary>
		public SwitchEntity ThisDeviceDoNotDisturbSwitch2 => new(_haContext, "switch.this_device_do_not_disturb_switch_2");
		///<summary>Tumble dryer</summary>
		public SwitchEntity TumbleDryer => new(_haContext, "switch.tumble_dryer");
		///<summary>Christmas Villiage</summary>
		public SwitchEntity TuyaSocket2 => new(_haContext, "switch.tuya_socket_2");
		///<summary>Tuya Socket 3</summary>
		public SwitchEntity Ty012047432cf4326b7a3a => new(_haContext, "switch.ty012047432cf4326b7a3a");
		///<summary>Tuya Socket 1</summary>
		public SwitchEntity Ty012047435002915e9eb5 => new(_haContext, "switch.ty012047435002915e9eb5");
		///<summary>Upstairs do not disturb switch</summary>
		public SwitchEntity UpstairsDoNotDisturbSwitch => new(_haContext, "switch.upstairs_do_not_disturb_switch");
		///<summary>Upstairs repeat switch</summary>
		public SwitchEntity UpstairsRepeatSwitch => new(_haContext, "switch.upstairs_repeat_switch");
		///<summary>Upstairs shuffle switch</summary>
		public SwitchEntity UpstairsShuffleSwitch => new(_haContext, "switch.upstairs_shuffle_switch");
		///<summary>Wallpanel do not disturb switch</summary>
		public SwitchEntity WallpanelDoNotDisturbSwitch => new(_haContext, "switch.wallpanel_do_not_disturb_switch");
		///<summary>Washing machine</summary>
		public SwitchEntity WashingMachine => new(_haContext, "switch.washing_machine");
		///<summary>Wiser Away Mode</summary>
		public SwitchEntity WiserAwayMode => new(_haContext, "switch.wiser_away_mode");
		///<summary>Wiser Away Mode Affects Hot Water</summary>
		public SwitchEntity WiserAwayModeAffectsHotWater => new(_haContext, "switch.wiser_away_mode_affects_hot_water");
		///<summary>Wiser Comfort Mode</summary>
		public SwitchEntity WiserComfortMode => new(_haContext, "switch.wiser_comfort_mode");
		///<summary>Wiser Eco Mode</summary>
		public SwitchEntity WiserEcoMode => new(_haContext, "switch.wiser_eco_mode");
		///<summary>Wiser Valve Protection</summary>
		public SwitchEntity WiserValveProtection => new(_haContext, "switch.wiser_valve_protection");
	}

	public class TimerEntities
	{
		private readonly IHaContext _haContext;
		public TimerEntities(IHaContext haContext)
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
		private readonly IHaContext _haContext;
		public WeatherEntities(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>AccuWeather Home</summary>
		public WeatherEntity AccuweatherHome => new(_haContext, "weather.accuweather_home");
	}

	public class ZoneEntities
	{
		private readonly IHaContext _haContext;
		public ZoneEntities(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Home</summary>
		public ZoneEntity Home => new(_haContext, "zone.home");
		///<summary>Mum Home</summary>
		public ZoneEntity MumHome => new(_haContext, "zone.mum_home");
	}

	public record AlarmControlPanelEntity : Entity<AlarmControlPanelEntity, EntityState<AlarmControlPanelAttributes>, AlarmControlPanelAttributes>
	{
		public AlarmControlPanelEntity(IHaContext haContext, string entityId) : base(haContext, entityId)
		{
		}

		public AlarmControlPanelEntity(Entity entity) : base(entity)
		{
		}
	}

	public record AutomationEntity : Entity<AutomationEntity, EntityState<AutomationAttributes>, AutomationAttributes>
	{
		public AutomationEntity(IHaContext haContext, string entityId) : base(haContext, entityId)
		{
		}

		public AutomationEntity(Entity entity) : base(entity)
		{
		}
	}

	public record BinarySensorEntity : Entity<BinarySensorEntity, EntityState<BinarySensorAttributes>, BinarySensorAttributes>
	{
		public BinarySensorEntity(IHaContext haContext, string entityId) : base(haContext, entityId)
		{
		}

		public BinarySensorEntity(Entity entity) : base(entity)
		{
		}
	}

	public record ButtonEntity : Entity<ButtonEntity, EntityState<ButtonAttributes>, ButtonAttributes>
	{
		public ButtonEntity(IHaContext haContext, string entityId) : base(haContext, entityId)
		{
		}

		public ButtonEntity(Entity entity) : base(entity)
		{
		}
	}

	public record CalendarEntity : Entity<CalendarEntity, EntityState<CalendarAttributes>, CalendarAttributes>
	{
		public CalendarEntity(IHaContext haContext, string entityId) : base(haContext, entityId)
		{
		}

		public CalendarEntity(Entity entity) : base(entity)
		{
		}
	}

	public record CameraEntity : Entity<CameraEntity, EntityState<CameraAttributes>, CameraAttributes>
	{
		public CameraEntity(IHaContext haContext, string entityId) : base(haContext, entityId)
		{
		}

		public CameraEntity(Entity entity) : base(entity)
		{
		}
	}

	public record ClimateEntity : Entity<ClimateEntity, EntityState<ClimateAttributes>, ClimateAttributes>
	{
		public ClimateEntity(IHaContext haContext, string entityId) : base(haContext, entityId)
		{
		}

		public ClimateEntity(Entity entity) : base(entity)
		{
		}
	}

	public record CoverEntity : Entity<CoverEntity, EntityState<CoverAttributes>, CoverAttributes>
	{
		public CoverEntity(IHaContext haContext, string entityId) : base(haContext, entityId)
		{
		}

		public CoverEntity(Entity entity) : base(entity)
		{
		}
	}

	public record DeviceTrackerEntity : Entity<DeviceTrackerEntity, EntityState<DeviceTrackerAttributes>, DeviceTrackerAttributes>
	{
		public DeviceTrackerEntity(IHaContext haContext, string entityId) : base(haContext, entityId)
		{
		}

		public DeviceTrackerEntity(Entity entity) : base(entity)
		{
		}
	}

	public record GroupEntity : Entity<GroupEntity, EntityState<GroupAttributes>, GroupAttributes>
	{
		public GroupEntity(IHaContext haContext, string entityId) : base(haContext, entityId)
		{
		}

		public GroupEntity(Entity entity) : base(entity)
		{
		}
	}

	public record InputBooleanEntity : Entity<InputBooleanEntity, EntityState<InputBooleanAttributes>, InputBooleanAttributes>
	{
		public InputBooleanEntity(IHaContext haContext, string entityId) : base(haContext, entityId)
		{
		}

		public InputBooleanEntity(Entity entity) : base(entity)
		{
		}
	}

	public record InputNumberEntity : NumericEntity<InputNumberEntity, NumericEntityState<InputNumberAttributes>, InputNumberAttributes>
	{
		public InputNumberEntity(IHaContext haContext, string entityId) : base(haContext, entityId)
		{
		}

		public InputNumberEntity(Entity entity) : base(entity)
		{
		}
	}

	public record InputSelectEntity : Entity<InputSelectEntity, EntityState<InputSelectAttributes>, InputSelectAttributes>
	{
		public InputSelectEntity(IHaContext haContext, string entityId) : base(haContext, entityId)
		{
		}

		public InputSelectEntity(Entity entity) : base(entity)
		{
		}
	}

	public record InputTextEntity : Entity<InputTextEntity, EntityState<InputTextAttributes>, InputTextAttributes>
	{
		public InputTextEntity(IHaContext haContext, string entityId) : base(haContext, entityId)
		{
		}

		public InputTextEntity(Entity entity) : base(entity)
		{
		}
	}

	public record LightEntity : Entity<LightEntity, EntityState<LightAttributes>, LightAttributes>
	{
		public LightEntity(IHaContext haContext, string entityId) : base(haContext, entityId)
		{
		}

		public LightEntity(Entity entity) : base(entity)
		{
		}
	}

	public record LockEntity : Entity<LockEntity, EntityState<LockAttributes>, LockAttributes>
	{
		public LockEntity(IHaContext haContext, string entityId) : base(haContext, entityId)
		{
		}

		public LockEntity(Entity entity) : base(entity)
		{
		}
	}

	public record MediaPlayerEntity : Entity<MediaPlayerEntity, EntityState<MediaPlayerAttributes>, MediaPlayerAttributes>
	{
		public MediaPlayerEntity(IHaContext haContext, string entityId) : base(haContext, entityId)
		{
		}

		public MediaPlayerEntity(Entity entity) : base(entity)
		{
		}
	}

	public record NumberEntity : NumericEntity<NumberEntity, NumericEntityState<NumberAttributes>, NumberAttributes>
	{
		public NumberEntity(IHaContext haContext, string entityId) : base(haContext, entityId)
		{
		}

		public NumberEntity(Entity entity) : base(entity)
		{
		}
	}

	public record OctopusagileEntity : Entity<OctopusagileEntity, EntityState<OctopusagileAttributes>, OctopusagileAttributes>
	{
		public OctopusagileEntity(IHaContext haContext, string entityId) : base(haContext, entityId)
		{
		}

		public OctopusagileEntity(Entity entity) : base(entity)
		{
		}
	}

	public record PersonEntity : Entity<PersonEntity, EntityState<PersonAttributes>, PersonAttributes>
	{
		public PersonEntity(IHaContext haContext, string entityId) : base(haContext, entityId)
		{
		}

		public PersonEntity(Entity entity) : base(entity)
		{
		}
	}

	public record RemoteEntity : Entity<RemoteEntity, EntityState<RemoteAttributes>, RemoteAttributes>
	{
		public RemoteEntity(IHaContext haContext, string entityId) : base(haContext, entityId)
		{
		}

		public RemoteEntity(Entity entity) : base(entity)
		{
		}
	}

	public record ScriptEntity : Entity<ScriptEntity, EntityState<ScriptAttributes>, ScriptAttributes>
	{
		public ScriptEntity(IHaContext haContext, string entityId) : base(haContext, entityId)
		{
		}

		public ScriptEntity(Entity entity) : base(entity)
		{
		}
	}

	public record SelectEntity : Entity<SelectEntity, EntityState<SelectAttributes>, SelectAttributes>
	{
		public SelectEntity(IHaContext haContext, string entityId) : base(haContext, entityId)
		{
		}

		public SelectEntity(Entity entity) : base(entity)
		{
		}
	}

	public record NumericSensorEntity : NumericEntity<NumericSensorEntity, NumericEntityState<NumericSensorAttributes>, NumericSensorAttributes>
	{
		public NumericSensorEntity(IHaContext haContext, string entityId) : base(haContext, entityId)
		{
		}

		public NumericSensorEntity(Entity entity) : base(entity)
		{
		}
	}

	public record SensorEntity : Entity<SensorEntity, EntityState<SensorAttributes>, SensorAttributes>
	{
		public SensorEntity(IHaContext haContext, string entityId) : base(haContext, entityId)
		{
		}

		public SensorEntity(Entity entity) : base(entity)
		{
		}
	}

	public record SunEntity : Entity<SunEntity, EntityState<SunAttributes>, SunAttributes>
	{
		public SunEntity(IHaContext haContext, string entityId) : base(haContext, entityId)
		{
		}

		public SunEntity(Entity entity) : base(entity)
		{
		}
	}

	public record SwitchEntity : Entity<SwitchEntity, EntityState<SwitchAttributes>, SwitchAttributes>
	{
		public SwitchEntity(IHaContext haContext, string entityId) : base(haContext, entityId)
		{
		}

		public SwitchEntity(Entity entity) : base(entity)
		{
		}
	}

	public record TimerEntity : Entity<TimerEntity, EntityState<TimerAttributes>, TimerAttributes>
	{
		public TimerEntity(IHaContext haContext, string entityId) : base(haContext, entityId)
		{
		}

		public TimerEntity(Entity entity) : base(entity)
		{
		}
	}

	public record WeatherEntity : Entity<WeatherEntity, EntityState<WeatherAttributes>, WeatherAttributes>
	{
		public WeatherEntity(IHaContext haContext, string entityId) : base(haContext, entityId)
		{
		}

		public WeatherEntity(Entity entity) : base(entity)
		{
		}
	}

	public record ZoneEntity : Entity<ZoneEntity, EntityState<ZoneAttributes>, ZoneAttributes>
	{
		public ZoneEntity(IHaContext haContext, string entityId) : base(haContext, entityId)
		{
		}

		public ZoneEntity(Entity entity) : base(entity)
		{
		}
	}

	public record AlarmControlPanelAttributes
	{
		[JsonPropertyName("arm_mode")]
		public object? ArmMode { get; init; }

		[JsonPropertyName("bypassed_sensors")]
		public object? BypassedSensors { get; init; }

		[JsonPropertyName("changed_by")]
		public object? ChangedBy { get; init; }

		[JsonPropertyName("code_arm_required")]
		public bool? CodeArmRequired { get; init; }

		[JsonPropertyName("code_disarm_required")]
		public bool? CodeDisarmRequired { get; init; }

		[JsonPropertyName("code_format")]
		public object? CodeFormat { get; init; }

		[JsonPropertyName("delay")]
		public object? Delay { get; init; }

		[JsonPropertyName("expiration")]
		public object? Expiration { get; init; }

		[JsonPropertyName("friendly_name")]
		public string? FriendlyName { get; init; }

		[JsonPropertyName("icon")]
		public string? Icon { get; init; }

		[JsonPropertyName("open_sensors")]
		public object? OpenSensors { get; init; }

		[JsonPropertyName("supported_features")]
		public double? SupportedFeatures { get; init; }
	}

	public record AutomationAttributes
	{
		[JsonPropertyName("current")]
		public double? Current { get; init; }

		[JsonPropertyName("friendly_name")]
		public string? FriendlyName { get; init; }

		[JsonPropertyName("id")]
		public string? Id { get; init; }

		[JsonPropertyName("last_triggered")]
		public string? LastTriggered { get; init; }

		[JsonPropertyName("mode")]
		public string? Mode { get; init; }

		[JsonPropertyName("restored")]
		public bool? Restored { get; init; }

		[JsonPropertyName("supported_features")]
		public double? SupportedFeatures { get; init; }
	}

	public record BinarySensorAttributes
	{
		[JsonPropertyName("attribution")]
		public string? Attribution { get; init; }

		[JsonPropertyName("current_version")]
		public string? CurrentVersion { get; init; }

		[JsonPropertyName("device_class")]
		public string? DeviceClass { get; init; }

		[JsonPropertyName("Fast User Switched")]
		public bool? FastUserSwitched { get; init; }

		[JsonPropertyName("friendly_name")]
		public string? FriendlyName { get; init; }

		[JsonPropertyName("icon")]
		public string? Icon { get; init; }

		[JsonPropertyName("Idle")]
		public bool? Idle { get; init; }

		[JsonPropertyName("lastDing")]
		public double? LastDing { get; init; }

		[JsonPropertyName("lastDingTime")]
		public string? LastDingTime { get; init; }

		[JsonPropertyName("lastMotion")]
		public double? LastMotion { get; init; }

		[JsonPropertyName("lastMotionTime")]
		public string? LastMotionTime { get; init; }

		[JsonPropertyName("last_updated")]
		public string? LastUpdated { get; init; }

		[JsonPropertyName("latest_version")]
		public string? LatestVersion { get; init; }

		[JsonPropertyName("Locked")]
		public bool? Locked { get; init; }

		[JsonPropertyName("Manufacturer")]
		public string? Manufacturer { get; init; }

		[JsonPropertyName("motionDetectionEnabled")]
		public bool? MotionDetectionEnabled { get; init; }

		[JsonPropertyName("newest_version")]
		public string? NewestVersion { get; init; }

		[JsonPropertyName("personDetected")]
		public bool? PersonDetected { get; init; }

		[JsonPropertyName("release_notes")]
		public string? ReleaseNotes { get; init; }

		[JsonPropertyName("restored")]
		public bool? Restored { get; init; }

		[JsonPropertyName("round_trip_time_avg")]
		public double? RoundTripTimeAvg { get; init; }

		[JsonPropertyName("round_trip_time_max")]
		public double? RoundTripTimeMax { get; init; }

		[JsonPropertyName("round_trip_time_mdev")]
		public string? RoundTripTimeMdev { get; init; }

		[JsonPropertyName("round_trip_time_min")]
		public double? RoundTripTimeMin { get; init; }

		[JsonPropertyName("Screen Off")]
		public bool? ScreenOff { get; init; }

		[JsonPropertyName("Screensaver")]
		public bool? Screensaver { get; init; }

		[JsonPropertyName("Sleeping")]
		public bool? Sleeping { get; init; }

		[JsonPropertyName("supported_features")]
		public double? SupportedFeatures { get; init; }

		[JsonPropertyName("Terminating")]
		public bool? Terminating { get; init; }
	}

	public record ButtonAttributes
	{
		[JsonPropertyName("device_class")]
		public string? DeviceClass { get; init; }

		[JsonPropertyName("friendly_name")]
		public string? FriendlyName { get; init; }
	}

	public record CalendarAttributes
	{
		[JsonPropertyName("friendly_name")]
		public string? FriendlyName { get; init; }

		[JsonPropertyName("offset_reached")]
		public bool? OffsetReached { get; init; }
	}

	public record CameraAttributes
	{
		[JsonPropertyName("access_token")]
		public string? AccessToken { get; init; }

		[JsonPropertyName("attribution")]
		public string? Attribution { get; init; }

		[JsonPropertyName("entity_picture")]
		public string? EntityPicture { get; init; }

		[JsonPropertyName("friendly_name")]
		public string? FriendlyName { get; init; }

		[JsonPropertyName("frontend_stream_type")]
		public string? FrontendStreamType { get; init; }

		[JsonPropertyName("last_video_id")]
		public double? LastVideoId { get; init; }

		[JsonPropertyName("supported_features")]
		public double? SupportedFeatures { get; init; }

		[JsonPropertyName("timestamp")]
		public double? Timestamp { get; init; }

		[JsonPropertyName("video_url")]
		public string? VideoUrl { get; init; }
	}

	public record ClimateAttributes
	{
		[JsonPropertyName("away_mode_supressed")]
		public bool? AwayModeSupressed { get; init; }

		[JsonPropertyName("boost_end")]
		public string? BoostEnd { get; init; }

		[JsonPropertyName("boost_remaining")]
		public double? BoostRemaining { get; init; }

		[JsonPropertyName("control_output_state")]
		public string? ControlOutputState { get; init; }

		[JsonPropertyName("current_temperature")]
		public double? CurrentTemperature { get; init; }

		[JsonPropertyName("fan_mode")]
		public string? FanMode { get; init; }

		[JsonPropertyName("fan_modes")]
		public object? FanModes { get; init; }

		[JsonPropertyName("friendly_name")]
		public string? FriendlyName { get; init; }

		[JsonPropertyName("heating_rate")]
		public double? HeatingRate { get; init; }

		[JsonPropertyName("hvac_action")]
		public string? HvacAction { get; init; }

		[JsonPropertyName("hvac_modes")]
		public object? HvacModes { get; init; }

		[JsonPropertyName("icon")]
		public string? Icon { get; init; }

		[JsonPropertyName("max_temp")]
		public double? MaxTemp { get; init; }

		[JsonPropertyName("min_temp")]
		public double? MinTemp { get; init; }

		[JsonPropertyName("percentage_demand")]
		public double? PercentageDemand { get; init; }

		[JsonPropertyName("preset_mode")]
		public object? PresetMode { get; init; }

		[JsonPropertyName("preset_modes")]
		public object? PresetModes { get; init; }

		[JsonPropertyName("supported_features")]
		public double? SupportedFeatures { get; init; }

		[JsonPropertyName("swing_mode")]
		public string? SwingMode { get; init; }

		[JsonPropertyName("swing_modes")]
		public object? SwingModes { get; init; }

		[JsonPropertyName("target_temp_step")]
		public double? TargetTempStep { get; init; }

		[JsonPropertyName("temperature")]
		public double? Temperature { get; init; }

		[JsonPropertyName("vane_horizontal")]
		public string? VaneHorizontal { get; init; }

		[JsonPropertyName("vane_horizontal_positions")]
		public object? VaneHorizontalPositions { get; init; }

		[JsonPropertyName("vane_vertical")]
		public string? VaneVertical { get; init; }

		[JsonPropertyName("vane_vertical_positions")]
		public object? VaneVerticalPositions { get; init; }

		[JsonPropertyName("window_detection_active")]
		public bool? WindowDetectionActive { get; init; }

		[JsonPropertyName("window_state")]
		public string? WindowState { get; init; }
	}

	public record CoverAttributes
	{
		[JsonPropertyName("current_position")]
		public double? CurrentPosition { get; init; }

		[JsonPropertyName("device_class")]
		public string? DeviceClass { get; init; }

		[JsonPropertyName("friendly_name")]
		public string? FriendlyName { get; init; }

		[JsonPropertyName("supported_features")]
		public double? SupportedFeatures { get; init; }
	}

	public record DeviceTrackerAttributes
	{
		[JsonPropertyName("altitude")]
		public double? Altitude { get; init; }

		[JsonPropertyName("ap_mac")]
		public string? ApMac { get; init; }

		[JsonPropertyName("authorized")]
		public bool? Authorized { get; init; }

		[JsonPropertyName("battery_level")]
		public double? BatteryLevel { get; init; }

		[JsonPropertyName("essid")]
		public string? Essid { get; init; }

		[JsonPropertyName("friendly_name")]
		public string? FriendlyName { get; init; }

		[JsonPropertyName("gps_accuracy")]
		public double? GpsAccuracy { get; init; }

		[JsonPropertyName("hostname")]
		public string? Hostname { get; init; }

		[JsonPropertyName("host_name")]
		public string? HostName { get; init; }

		[JsonPropertyName("icon")]
		public string? Icon { get; init; }

		[JsonPropertyName("ip")]
		public string? Ip { get; init; }

		[JsonPropertyName("is_11r")]
		public bool? Is11r { get; init; }

		[JsonPropertyName("is_guest")]
		public bool? IsGuest { get; init; }

		[JsonPropertyName("_is_guest_by_uap")]
		public bool? IsGuestByUap { get; init; }

		[JsonPropertyName("is_wired")]
		public bool? IsWired { get; init; }

		[JsonPropertyName("latitude")]
		public double? Latitude { get; init; }

		[JsonPropertyName("longitude")]
		public double? Longitude { get; init; }

		[JsonPropertyName("mac")]
		public string? Mac { get; init; }

		[JsonPropertyName("name")]
		public string? Name { get; init; }

		[JsonPropertyName("note")]
		public string? Note { get; init; }

		[JsonPropertyName("oui")]
		public string? Oui { get; init; }

		[JsonPropertyName("qos_policy_applied")]
		public bool? QosPolicyApplied { get; init; }

		[JsonPropertyName("radio")]
		public string? Radio { get; init; }

		[JsonPropertyName("radio_proto")]
		public string? RadioProto { get; init; }

		[JsonPropertyName("restored")]
		public bool? Restored { get; init; }

		[JsonPropertyName("source_type")]
		public string? SourceType { get; init; }

		[JsonPropertyName("supported_features")]
		public double? SupportedFeatures { get; init; }

		[JsonPropertyName("vertical_accuracy")]
		public double? VerticalAccuracy { get; init; }

		[JsonPropertyName("vlan")]
		public double? Vlan { get; init; }
	}

	public record GroupAttributes
	{
		[JsonPropertyName("entity_id")]
		public object? EntityId { get; init; }

		[JsonPropertyName("friendly_name")]
		public string? FriendlyName { get; init; }

		[JsonPropertyName("order")]
		public double? Order { get; init; }
	}

	public record InputBooleanAttributes
	{
		[JsonPropertyName("editable")]
		public bool? Editable { get; init; }

		[JsonPropertyName("friendly_name")]
		public string? FriendlyName { get; init; }

		[JsonPropertyName("icon")]
		public string? Icon { get; init; }
	}

	public record InputNumberAttributes
	{
		[JsonPropertyName("editable")]
		public bool? Editable { get; init; }

		[JsonPropertyName("initial")]
		public object? Initial { get; init; }

		[JsonPropertyName("max")]
		public double? Max { get; init; }

		[JsonPropertyName("min")]
		public double? Min { get; init; }

		[JsonPropertyName("mode")]
		public string? Mode { get; init; }

		[JsonPropertyName("restored")]
		public bool? Restored { get; init; }

		[JsonPropertyName("step")]
		public double? Step { get; init; }

		[JsonPropertyName("supported_features")]
		public double? SupportedFeatures { get; init; }
	}

	public record InputSelectAttributes
	{
		[JsonPropertyName("editable")]
		public bool? Editable { get; init; }

		[JsonPropertyName("friendly_name")]
		public string? FriendlyName { get; init; }

		[JsonPropertyName("options")]
		public object? Options { get; init; }
	}

	public record InputTextAttributes
	{
		[JsonPropertyName("editable")]
		public bool? Editable { get; init; }

		[JsonPropertyName("max")]
		public double? Max { get; init; }

		[JsonPropertyName("min")]
		public double? Min { get; init; }

		[JsonPropertyName("mode")]
		public string? Mode { get; init; }

		[JsonPropertyName("pattern")]
		public object? Pattern { get; init; }
	}

	public record LightAttributes
	{
		[JsonPropertyName("attribution")]
		public string? Attribution { get; init; }

		[JsonPropertyName("brightness")]
		public double? Brightness { get; init; }

		[JsonPropertyName("color_mode")]
		public string? ColorMode { get; init; }

		[JsonPropertyName("color_temp")]
		public double? ColorTemp { get; init; }

		[JsonPropertyName("entity_id")]
		public object? EntityId { get; init; }

		[JsonPropertyName("friendly_name")]
		public string? FriendlyName { get; init; }

		[JsonPropertyName("hs_color")]
		public object? HsColor { get; init; }

		[JsonPropertyName("icon")]
		public string? Icon { get; init; }

		[JsonPropertyName("max_mireds")]
		public double? MaxMireds { get; init; }

		[JsonPropertyName("min_mireds")]
		public double? MinMireds { get; init; }

		[JsonPropertyName("off_brightness")]
		public object? OffBrightness { get; init; }

		[JsonPropertyName("rgb_color")]
		public object? RgbColor { get; init; }

		[JsonPropertyName("supported_color_modes")]
		public object? SupportedColorModes { get; init; }

		[JsonPropertyName("supported_features")]
		public double? SupportedFeatures { get; init; }

		[JsonPropertyName("xy_color")]
		public object? XyColor { get; init; }
	}

	public record LockAttributes
	{
		[JsonPropertyName("friendly_name")]
		public string? FriendlyName { get; init; }

		[JsonPropertyName("restored")]
		public bool? Restored { get; init; }

		[JsonPropertyName("supported_features")]
		public double? SupportedFeatures { get; init; }
	}

	public record MediaPlayerAttributes
	{
		[JsonPropertyName("available")]
		public bool? Available { get; init; }

		[JsonPropertyName("bluetooth_list")]
		public object? BluetoothList { get; init; }

		[JsonPropertyName("connected_bluetooth")]
		public object? ConnectedBluetooth { get; init; }

		[JsonPropertyName("device_class")]
		public string? DeviceClass { get; init; }

		[JsonPropertyName("entity_picture")]
		public string? EntityPicture { get; init; }

		[JsonPropertyName("entity_picture_local")]
		public string? EntityPictureLocal { get; init; }

		[JsonPropertyName("friendly_name")]
		public string? FriendlyName { get; init; }

		[JsonPropertyName("group_members")]
		public object? GroupMembers { get; init; }

		[JsonPropertyName("icon")]
		public string? Icon { get; init; }

		[JsonPropertyName("is_volume_muted")]
		public bool? IsVolumeMuted { get; init; }

		[JsonPropertyName("last_called")]
		public bool? LastCalled { get; init; }

		[JsonPropertyName("last_called_summary")]
		public string? LastCalledSummary { get; init; }

		[JsonPropertyName("last_called_timestamp")]
		public double? LastCalledTimestamp { get; init; }

		[JsonPropertyName("media_album_name")]
		public string? MediaAlbumName { get; init; }

		[JsonPropertyName("media_artist")]
		public string? MediaArtist { get; init; }

		[JsonPropertyName("media_content_type")]
		public string? MediaContentType { get; init; }

		[JsonPropertyName("media_duration")]
		public double? MediaDuration { get; init; }

		[JsonPropertyName("media_position")]
		public double? MediaPosition { get; init; }

		[JsonPropertyName("media_position_updated_at")]
		public string? MediaPositionUpdatedAt { get; init; }

		[JsonPropertyName("media_title")]
		public string? MediaTitle { get; init; }

		[JsonPropertyName("repeat")]
		public string? Repeat { get; init; }

		[JsonPropertyName("restored")]
		public bool? Restored { get; init; }

		[JsonPropertyName("shuffle")]
		public bool? Shuffle { get; init; }

		[JsonPropertyName("sonos_group")]
		public object? SonosGroup { get; init; }

		[JsonPropertyName("source")]
		public string? Source { get; init; }

		[JsonPropertyName("source_list")]
		public object? SourceList { get; init; }

		[JsonPropertyName("supported_features")]
		public double? SupportedFeatures { get; init; }

		[JsonPropertyName("volume_level")]
		public double? VolumeLevel { get; init; }
	}

	public record NumberAttributes
	{
		[JsonPropertyName("friendly_name")]
		public string? FriendlyName { get; init; }

		[JsonPropertyName("icon")]
		public string? Icon { get; init; }

		[JsonPropertyName("max")]
		public double? Max { get; init; }

		[JsonPropertyName("min")]
		public double? Min { get; init; }

		[JsonPropertyName("mode")]
		public string? Mode { get; init; }

		[JsonPropertyName("step")]
		public double? Step { get; init; }
	}

	public record OctopusagileAttributes
	{
		[JsonPropertyName("2022-03-12T19:00:00Z")]
		public double? HA20220312T190000Z { get; init; }

		[JsonPropertyName("2022-03-12T19:30:00Z")]
		public double? HA20220312T193000Z { get; init; }

		[JsonPropertyName("2022-03-12T20:00:00Z")]
		public double? HA20220312T200000Z { get; init; }

		[JsonPropertyName("2022-03-12T20:30:00Z")]
		public double? HA20220312T203000Z { get; init; }

		[JsonPropertyName("2022-03-12T21:00:00Z")]
		public double? HA20220312T210000Z { get; init; }

		[JsonPropertyName("2022-03-12T21:30:00Z")]
		public double? HA20220312T213000Z { get; init; }

		[JsonPropertyName("2022-03-12T22:00:00Z")]
		public double? HA20220312T220000Z { get; init; }

		[JsonPropertyName("2022-03-12T22:30:00Z")]
		public double? HA20220312T223000Z { get; init; }

		[JsonPropertyName("2022-03-12T23:00:00Z")]
		public double? HA20220312T230000Z { get; init; }

		[JsonPropertyName("2022-03-12T23:30:00Z")]
		public double? HA20220312T233000Z { get; init; }

		[JsonPropertyName("2022-03-13T00:00:00Z")]
		public double? HA20220313T000000Z { get; init; }

		[JsonPropertyName("2022-03-13T00:30:00Z")]
		public double? HA20220313T003000Z { get; init; }

		[JsonPropertyName("2022-03-13T01:00:00Z")]
		public double? HA20220313T010000Z { get; init; }

		[JsonPropertyName("2022-03-13T01:30:00Z")]
		public double? HA20220313T013000Z { get; init; }

		[JsonPropertyName("2022-03-13T02:00:00Z")]
		public double? HA20220313T020000Z { get; init; }

		[JsonPropertyName("2022-03-13T02:30:00Z")]
		public double? HA20220313T023000Z { get; init; }

		[JsonPropertyName("2022-03-13T03:00:00Z")]
		public double? HA20220313T030000Z { get; init; }

		[JsonPropertyName("2022-03-13T03:30:00Z")]
		public double? HA20220313T033000Z { get; init; }

		[JsonPropertyName("2022-03-13T04:00:00Z")]
		public double? HA20220313T040000Z { get; init; }

		[JsonPropertyName("2022-03-13T04:30:00Z")]
		public double? HA20220313T043000Z { get; init; }

		[JsonPropertyName("2022-03-13T05:00:00Z")]
		public double? HA20220313T050000Z { get; init; }

		[JsonPropertyName("2022-03-13T05:30:00Z")]
		public double? HA20220313T053000Z { get; init; }

		[JsonPropertyName("2022-03-13T06:00:00Z")]
		public double? HA20220313T060000Z { get; init; }

		[JsonPropertyName("2022-03-13T06:30:00Z")]
		public double? HA20220313T063000Z { get; init; }

		[JsonPropertyName("2022-03-13T07:00:00Z")]
		public double? HA20220313T070000Z { get; init; }

		[JsonPropertyName("2022-03-13T07:30:00Z")]
		public double? HA20220313T073000Z { get; init; }

		[JsonPropertyName("2022-03-13T08:00:00Z")]
		public double? HA20220313T080000Z { get; init; }

		[JsonPropertyName("2022-03-13T08:30:00Z")]
		public double? HA20220313T083000Z { get; init; }

		[JsonPropertyName("2022-03-13T09:00:00Z")]
		public double? HA20220313T090000Z { get; init; }

		[JsonPropertyName("2022-03-13T09:30:00Z")]
		public double? HA20220313T093000Z { get; init; }

		[JsonPropertyName("2022-03-13T10:00:00Z")]
		public double? HA20220313T100000Z { get; init; }

		[JsonPropertyName("2022-03-13T10:30:00Z")]
		public double? HA20220313T103000Z { get; init; }

		[JsonPropertyName("2022-03-13T11:00:00Z")]
		public double? HA20220313T110000Z { get; init; }

		[JsonPropertyName("2022-03-13T11:30:00Z")]
		public double? HA20220313T113000Z { get; init; }

		[JsonPropertyName("2022-03-13T12:00:00Z")]
		public double? HA20220313T120000Z { get; init; }

		[JsonPropertyName("2022-03-13T12:30:00Z")]
		public double? HA20220313T123000Z { get; init; }

		[JsonPropertyName("2022-03-13T13:00:00Z")]
		public double? HA20220313T130000Z { get; init; }

		[JsonPropertyName("2022-03-13T13:30:00Z")]
		public double? HA20220313T133000Z { get; init; }

		[JsonPropertyName("2022-03-13T14:00:00Z")]
		public double? HA20220313T140000Z { get; init; }

		[JsonPropertyName("2022-03-13T14:30:00Z")]
		public double? HA20220313T143000Z { get; init; }

		[JsonPropertyName("2022-03-13T15:00:00Z")]
		public double? HA20220313T150000Z { get; init; }

		[JsonPropertyName("2022-03-13T15:30:00Z")]
		public double? HA20220313T153000Z { get; init; }

		[JsonPropertyName("2022-03-13T16:00:00Z")]
		public double? HA20220313T160000Z { get; init; }

		[JsonPropertyName("2022-03-13T16:30:00Z")]
		public double? HA20220313T163000Z { get; init; }

		[JsonPropertyName("2022-03-13T17:00:00Z")]
		public double? HA20220313T170000Z { get; init; }

		[JsonPropertyName("2022-03-13T17:30:00Z")]
		public double? HA20220313T173000Z { get; init; }

		[JsonPropertyName("2022-03-13T18:00:00Z")]
		public double? HA20220313T180000Z { get; init; }

		[JsonPropertyName("2022-03-13T18:30:00Z")]
		public double? HA20220313T183000Z { get; init; }

		[JsonPropertyName("2022-03-13T19:00:00Z")]
		public double? HA20220313T190000Z { get; init; }

		[JsonPropertyName("2022-03-13T19:30:00Z")]
		public double? HA20220313T193000Z { get; init; }

		[JsonPropertyName("2022-03-13T20:00:00Z")]
		public double? HA20220313T200000Z { get; init; }

		[JsonPropertyName("2022-03-13T20:30:00Z")]
		public double? HA20220313T203000Z { get; init; }

		[JsonPropertyName("2022-03-13T21:00:00Z")]
		public double? HA20220313T210000Z { get; init; }

		[JsonPropertyName("2022-03-13T21:30:00Z")]
		public double? HA20220313T213000Z { get; init; }

		[JsonPropertyName("2022-03-13T22:00:00Z")]
		public double? HA20220313T220000Z { get; init; }

		[JsonPropertyName("2022-03-13T22:30:00Z")]
		public double? HA20220313T223000Z { get; init; }

		[JsonPropertyName("icon")]
		public string? Icon { get; init; }

		[JsonPropertyName("timers")]
		public object? Timers { get; init; }

		[JsonPropertyName("unit_of_measurement")]
		public string? UnitOfMeasurement { get; init; }
	}

	public record PersonAttributes
	{
		[JsonPropertyName("editable")]
		public bool? Editable { get; init; }

		[JsonPropertyName("entity_picture")]
		public string? EntityPicture { get; init; }

		[JsonPropertyName("friendly_name")]
		public string? FriendlyName { get; init; }

		[JsonPropertyName("gps_accuracy")]
		public double? GpsAccuracy { get; init; }

		[JsonPropertyName("id")]
		public string? Id { get; init; }

		[JsonPropertyName("latitude")]
		public double? Latitude { get; init; }

		[JsonPropertyName("longitude")]
		public double? Longitude { get; init; }

		[JsonPropertyName("source")]
		public string? Source { get; init; }

		[JsonPropertyName("user_id")]
		public string? UserId { get; init; }
	}

	public record RemoteAttributes
	{
		[JsonPropertyName("friendly_name")]
		public string? FriendlyName { get; init; }

		[JsonPropertyName("restored")]
		public bool? Restored { get; init; }

		[JsonPropertyName("supported_features")]
		public double? SupportedFeatures { get; init; }
	}

	public record ScriptAttributes
	{
		[JsonPropertyName("current")]
		public double? Current { get; init; }

		[JsonPropertyName("friendly_name")]
		public string? FriendlyName { get; init; }

		[JsonPropertyName("last_triggered")]
		public string? LastTriggered { get; init; }

		[JsonPropertyName("max")]
		public double? Max { get; init; }

		[JsonPropertyName("mode")]
		public string? Mode { get; init; }
	}

	public record SelectAttributes
	{
		[JsonPropertyName("eventId")]
		public string? EventId { get; init; }

		[JsonPropertyName("friendly_name")]
		public string? FriendlyName { get; init; }

		[JsonPropertyName("options")]
		public object? Options { get; init; }

		[JsonPropertyName("recordingUrl")]
		public string? RecordingUrl { get; init; }
	}

	public record NumericSensorAttributes
	{
		[JsonPropertyName("age_coverage_ratio")]
		public double? AgeCoverageRatio { get; init; }

		[JsonPropertyName("attribution")]
		public string? Attribution { get; init; }

		[JsonPropertyName("Available")]
		public string? Available { get; init; }

		[JsonPropertyName("Available (Important)")]
		public string? AvailableImportant { get; init; }

		[JsonPropertyName("Available (Opportunistic)")]
		public string? AvailableOpportunistic { get; init; }

		[JsonPropertyName("BatteryHealth")]
		public string? BatteryHealth { get; init; }

		[JsonPropertyName("BatteryHealthCondition")]
		public string? BatteryHealthCondition { get; init; }

		[JsonPropertyName("battery_level")]
		public string? BatteryLevel_0 { get; init; }

		[JsonPropertyName("batteryLevel")]
		public string? BatteryLevel_1 { get; init; }

		[JsonPropertyName("Battery Provides Time Remaining")]
		public bool? BatteryProvidesTimeRemaining { get; init; }

		[JsonPropertyName("battery_voltage")]
		public object? BatteryVoltage { get; init; }

		[JsonPropertyName("buffer_usage_ratio")]
		public double? BufferUsageRatio { get; init; }

		[JsonPropertyName("bytes_received")]
		public double? BytesReceived { get; init; }

		[JsonPropertyName("bytes_sent")]
		public double? BytesSent { get; init; }

		[JsonPropertyName("colortemp")]
		public double? Colortemp { get; init; }

		[JsonPropertyName("count_sensors")]
		public double? CountSensors { get; init; }

		[JsonPropertyName("cron pattern")]
		public string? Cronpattern { get; init; }

		[JsonPropertyName("Current")]
		public double? Current { get; init; }

		[JsonPropertyName("Current Capacity")]
		public double? CurrentCapacity { get; init; }

		[JsonPropertyName("DesignCycleCount")]
		public double? DesignCycleCount { get; init; }

		[JsonPropertyName("device_class")]
		public string? DeviceClass { get; init; }

		[JsonPropertyName("direction")]
		public string? Direction { get; init; }

		[JsonPropertyName("domains_blocked")]
		public double? DomainsBlocked { get; init; }

		[JsonPropertyName("friendly_name")]
		public string? FriendlyName { get; init; }

		[JsonPropertyName("Hardware Serial Number")]
		public string? HardwareSerialNumber { get; init; }

		[JsonPropertyName("icon")]
		public string? Icon { get; init; }

		[JsonPropertyName("Is Charged")]
		public bool? IsCharged { get; init; }

		[JsonPropertyName("Is Charging")]
		public bool? IsCharging { get; init; }

		[JsonPropertyName("Is Present")]
		public bool? IsPresent { get; init; }

		[JsonPropertyName("last")]
		public double? Last { get; init; }

		[JsonPropertyName("last_entity_id")]
		public string? LastEntityId { get; init; }

		[JsonPropertyName("last_period")]
		public string? LastPeriod { get; init; }

		[JsonPropertyName("last_reset")]
		public string? LastReset { get; init; }

		[JsonPropertyName("level")]
		public string? Level { get; init; }

		[JsonPropertyName("Max Capacity")]
		public double? MaxCapacity { get; init; }

		[JsonPropertyName("max_entity_id")]
		public string? MaxEntityId { get; init; }

		[JsonPropertyName("max_value")]
		public double? MaxValue { get; init; }

		[JsonPropertyName("mean")]
		public double? Mean { get; init; }

		[JsonPropertyName("median")]
		public double? Median { get; init; }

		[JsonPropertyName("meter_period")]
		public string? MeterPeriod { get; init; }

		[JsonPropertyName("min_entity_id")]
		public string? MinEntityId { get; init; }

		[JsonPropertyName("min_value")]
		public double? MinValue { get; init; }

		[JsonPropertyName("Name")]
		public string? Name { get; init; }

		[JsonPropertyName("Optimized Battery Charging Engaged")]
		public bool? OptimizedBatteryChargingEngaged { get; init; }

		[JsonPropertyName("Power Source ID")]
		public double? PowerSourceID { get; init; }

		[JsonPropertyName("Power Source State")]
		public string? PowerSourceState { get; init; }

		[JsonPropertyName("repositories")]
		public object? Repositories { get; init; }

		[JsonPropertyName("restored")]
		public bool? Restored { get; init; }

		[JsonPropertyName("rgb_color")]
		public object? RgbColor { get; init; }

		[JsonPropertyName("server_country")]
		public string? ServerCountry { get; init; }

		[JsonPropertyName("server_id")]
		public string? ServerId { get; init; }

		[JsonPropertyName("server_name")]
		public string? ServerName { get; init; }

		[JsonPropertyName("source")]
		public string? Source { get; init; }

		[JsonPropertyName("source_value_valid")]
		public bool? SourceValueValid { get; init; }

		[JsonPropertyName("state_class")]
		public string? StateClass { get; init; }

		[JsonPropertyName("status")]
		public string? Status { get; init; }

		[JsonPropertyName("supported_features")]
		public double? SupportedFeatures { get; init; }

		[JsonPropertyName("Time to Empty")]
		public double? TimetoEmpty { get; init; }

		[JsonPropertyName("Time to Full Charge")]
		public double? TimetoFullCharge { get; init; }

		[JsonPropertyName("Total")]
		public string? Total { get; init; }

		[JsonPropertyName("Transport Type")]
		public string? TransportType { get; init; }

		[JsonPropertyName("type")]
		public object? Type_0 { get; init; }

		[JsonPropertyName("Type")]
		public string? Type_1 { get; init; }

		[JsonPropertyName("unit_of_measurement")]
		public string? UnitOfMeasurement { get; init; }

		[JsonPropertyName("value")]
		public string? Value { get; init; }

		[JsonPropertyName("wirelessNetwork")]
		public string? WirelessNetwork { get; init; }

		[JsonPropertyName("wirelessSignal")]
		public double? WirelessSignal { get; init; }

		[JsonPropertyName("xy_color")]
		public object? XyColor { get; init; }
	}

	public record SensorAttributes
	{
		[JsonPropertyName("Active Audio Input")]
		public object? ActiveAudioInput { get; init; }

		[JsonPropertyName("Active Audio Output")]
		public object? ActiveAudioOutput { get; init; }

		[JsonPropertyName("Active Camera")]
		public object? ActiveCamera { get; init; }

		[JsonPropertyName("Administrative Area")]
		public string? AdministrativeArea { get; init; }

		[JsonPropertyName("All Audio Input")]
		public object? AllAudioInput { get; init; }

		[JsonPropertyName("All Audio Output")]
		public object? AllAudioOutput { get; init; }

		[JsonPropertyName("All Camera")]
		public object? AllCamera { get; init; }

		[JsonPropertyName("Allows VoIP")]
		public bool? AllowsVoIP { get; init; }

		[JsonPropertyName("answered")]
		public bool? Answered { get; init; }

		[JsonPropertyName("Areas Of Interest")]
		public string? AreasOfInterest { get; init; }

		[JsonPropertyName("attribution")]
		public string? Attribution { get; init; }

		[JsonPropertyName("AwayModeTemperature")]
		public double? AwayModeTemperature { get; init; }

		[JsonPropertyName("BatteryHealth")]
		public string? BatteryHealth { get; init; }

		[JsonPropertyName("BatteryHealthCondition")]
		public string? BatteryHealthCondition { get; init; }

		[JsonPropertyName("battery_level")]
		public string? BatteryLevel_0 { get; init; }

		[JsonPropertyName("batteryLevel")]
		public string? BatteryLevel_1 { get; init; }

		[JsonPropertyName("battery_percent")]
		public double? BatteryPercent { get; init; }

		[JsonPropertyName("Battery Provides Time Remaining")]
		public bool? BatteryProvidesTimeRemaining { get; init; }

		[JsonPropertyName("battery_voltage")]
		public string? BatteryVoltage { get; init; }

		[JsonPropertyName("buffer_usage_ratio")]
		public double? BufferUsageRatio { get; init; }

		[JsonPropertyName("Bundle Identifier")]
		public string? BundleIdentifier { get; init; }

		[JsonPropertyName("Carrier ID")]
		public string? CarrierID { get; init; }

		[JsonPropertyName("Carrier Name")]
		public string? CarrierName { get; init; }

		[JsonPropertyName("category")]
		public string? Category { get; init; }

		[JsonPropertyName("Confidence")]
		public string? Confidence { get; init; }

		[JsonPropertyName("controller_reception_RSSI")]
		public double? ControllerReceptionRSSI { get; init; }

		[JsonPropertyName("Country")]
		public string? Country { get; init; }

		[JsonPropertyName("created_at")]
		public string? CreatedAt { get; init; }

		[JsonPropertyName("Current")]
		public double? Current { get; init; }

		[JsonPropertyName("current_bans")]
		public object? CurrentBans { get; init; }

		[JsonPropertyName("Current Capacity")]
		public double? CurrentCapacity { get; init; }

		[JsonPropertyName("Current Radio Technology")]
		public string? CurrentRadioTechnology { get; init; }

		[JsonPropertyName("data")]
		public object? Data { get; init; }

		[JsonPropertyName("DesignCycleCount")]
		public double? DesignCycleCount { get; init; }

		[JsonPropertyName("device_class")]
		public string? DeviceClass { get; init; }

		[JsonPropertyName("device_lock_enabled")]
		public bool? DeviceLockEnabled { get; init; }

		[JsonPropertyName("device_reception_LQI")]
		public double? DeviceReceptionLQI { get; init; }

		[JsonPropertyName("device_reception_RSSI")]
		public double? DeviceReceptionRSSI { get; init; }

		[JsonPropertyName("dismissed")]
		public string? Dismissed { get; init; }

		[JsonPropertyName("displayed_signal_strength")]
		public string? DisplayedSignalStrength { get; init; }

		[JsonPropertyName("Display IDs")]
		public object? DisplayIDs { get; init; }

		[JsonPropertyName("Display Names")]
		public object? DisplayNames { get; init; }

		[JsonPropertyName("event")]
		public string? Event { get; init; }

		[JsonPropertyName("firmware")]
		public string? Firmware { get; init; }

		[JsonPropertyName("firmwareStatus")]
		public string? FirmwareStatus { get; init; }

		[JsonPropertyName("friendly_name")]
		public string? FriendlyName { get; init; }

		[JsonPropertyName("Hardware Address")]
		public string? HardwareAddress { get; init; }

		[JsonPropertyName("Hardware Serial Number")]
		public string? HardwareSerialNumber { get; init; }

		[JsonPropertyName("hub_route")]
		public string? HubRoute { get; init; }

		[JsonPropertyName("humidity")]
		public double? Humidity { get; init; }

		[JsonPropertyName("icon")]
		public string? Icon { get; init; }

		[JsonPropertyName("Inland Water")]
		public string? InlandWater { get; init; }

		[JsonPropertyName("integration")]
		public string? Integration { get; init; }

		[JsonPropertyName("Is Charged")]
		public bool? IsCharged { get; init; }

		[JsonPropertyName("Is Charging")]
		public bool? IsCharging { get; init; }

		[JsonPropertyName("Is Hidden")]
		public bool? IsHidden { get; init; }

		[JsonPropertyName("ISO Country Code")]
		public string? ISOCountryCode { get; init; }

		[JsonPropertyName("Is Present")]
		public bool? IsPresent { get; init; }

		[JsonPropertyName("last_changed")]
		public string? LastChanged { get; init; }

		[JsonPropertyName("lastUpdate")]
		public string? LastUpdate { get; init; }

		[JsonPropertyName("last_updated")]
		public string? LastUpdated { get; init; }

		[JsonPropertyName("Launch Date")]
		public string? LaunchDate { get; init; }

		[JsonPropertyName("Locality")]
		public string? Locality { get; init; }

		[JsonPropertyName("Location")]
		public object? Location { get; init; }

		[JsonPropertyName("Low Power Mode")]
		public bool? LowPowerMode { get; init; }

		[JsonPropertyName("Max Capacity")]
		public double? MaxCapacity { get; init; }

		[JsonPropertyName("Mobile Country Code")]
		public string? MobileCountryCode { get; init; }

		[JsonPropertyName("Mobile Network Code")]
		public string? MobileNetworkCode { get; init; }

		[JsonPropertyName("model_identifier")]
		public string? ModelIdentifier { get; init; }

		[JsonPropertyName("Name")]
		public string? Name { get; init; }

		[JsonPropertyName("node_id")]
		public double? NodeId { get; init; }

		[JsonPropertyName("number_of_loaded_apps")]
		public double? NumberOfLoadedApps { get; init; }

		[JsonPropertyName("number_of_running_apps")]
		public double? NumberOfRunningApps { get; init; }

		[JsonPropertyName("Ocean")]
		public string? Ocean { get; init; }

		[JsonPropertyName("Optimized Battery Charging Engaged")]
		public bool? OptimizedBatteryChargingEngaged { get; init; }

		[JsonPropertyName("Owns Menu Bar")]
		public bool? OwnsMenuBar { get; init; }

		[JsonPropertyName("parent_node_id")]
		public double? ParentNodeId { get; init; }

		[JsonPropertyName("percentage_demand_Channel-1")]
		public double? PercentageDemandChannel1 { get; init; }

		[JsonPropertyName("Postal Code")]
		public string? PostalCode { get; init; }

		[JsonPropertyName("Power Source ID")]
		public double? PowerSourceID { get; init; }

		[JsonPropertyName("Power Source State")]
		public string? PowerSourceState { get; init; }

		[JsonPropertyName("prior_value")]
		public string? PriorValue { get; init; }

		[JsonPropertyName("process_timestamp")]
		public string? ProcessTimestamp { get; init; }

		[JsonPropertyName("product_type")]
		public string? ProductType { get; init; }

		[JsonPropertyName("recording_status")]
		public string? RecordingStatus { get; init; }

		[JsonPropertyName("recurrence")]
		public object? Recurrence { get; init; }

		[JsonPropertyName("reminder")]
		public object? Reminder { get; init; }

		[JsonPropertyName("restored")]
		public bool? Restored { get; init; }

		[JsonPropertyName("room_ids_Channel-1")]
		public object? RoomIdsChannel1 { get; init; }

		[JsonPropertyName("serial_number")]
		public string? SerialNumber { get; init; }

		[JsonPropertyName("sorted_active")]
		public string? SortedActive { get; init; }

		[JsonPropertyName("sorted_all")]
		public string? SortedAll { get; init; }

		[JsonPropertyName("source_value_valid")]
		public bool? SourceValueValid { get; init; }

		[JsonPropertyName("state_class")]
		public string? StateClass { get; init; }

		[JsonPropertyName("status")]
		public string? Status { get; init; }

		[JsonPropertyName("still_Image_URL")]
		public string? StillImageURL { get; init; }

		[JsonPropertyName("stream_Source")]
		public string? StreamSource { get; init; }

		[JsonPropertyName("Sub Administrative Area")]
		public string? SubAdministrativeArea { get; init; }

		[JsonPropertyName("Sub Locality")]
		public string? SubLocality { get; init; }

		[JsonPropertyName("Sub Thoroughfare")]
		public string? SubThoroughfare { get; init; }

		[JsonPropertyName("supported_features")]
		public double? SupportedFeatures { get; init; }

		[JsonPropertyName("temperature")]
		public double? Temperature { get; init; }

		[JsonPropertyName("text")]
		public string? Text { get; init; }

		[JsonPropertyName("Thoroughfare")]
		public string? Thoroughfare { get; init; }

		[JsonPropertyName("Time to Empty")]
		public double? TimetoEmpty { get; init; }

		[JsonPropertyName("Time to Full Charge")]
		public double? TimetoFullCharge { get; init; }

		[JsonPropertyName("Time Zone")]
		public string? TimeZone { get; init; }

		[JsonPropertyName("total_active")]
		public double? TotalActive { get; init; }

		[JsonPropertyName("total_all")]
		public double? TotalAll { get; init; }

		[JsonPropertyName("total_bans")]
		public object? TotalBans { get; init; }

		[JsonPropertyName("Transport Type")]
		public string? TransportType { get; init; }

		[JsonPropertyName("Type")]
		public string? Type { get; init; }

		[JsonPropertyName("Types")]
		public object? Types { get; init; }

		[JsonPropertyName("vendor")]
		public string? Vendor { get; init; }

		[JsonPropertyName("version")]
		public string? Version { get; init; }

		[JsonPropertyName("wirelessNetwork")]
		public string? WirelessNetwork { get; init; }

		[JsonPropertyName("wirelessSignal")]
		public double? WirelessSignal { get; init; }

		[JsonPropertyName("zigbee_channel")]
		public double? ZigbeeChannel { get; init; }

		[JsonPropertyName("Zones")]
		public object? Zones { get; init; }
	}

	public record SunAttributes
	{
		[JsonPropertyName("azimuth")]
		public double? Azimuth { get; init; }

		[JsonPropertyName("elevation")]
		public double? Elevation { get; init; }

		[JsonPropertyName("friendly_name")]
		public string? FriendlyName { get; init; }

		[JsonPropertyName("next_dawn")]
		public string? NextDawn { get; init; }

		[JsonPropertyName("next_dusk")]
		public string? NextDusk { get; init; }

		[JsonPropertyName("next_midnight")]
		public string? NextMidnight { get; init; }

		[JsonPropertyName("next_noon")]
		public string? NextNoon { get; init; }

		[JsonPropertyName("next_rising")]
		public string? NextRising { get; init; }

		[JsonPropertyName("next_setting")]
		public string? NextSetting { get; init; }

		[JsonPropertyName("rising")]
		public bool? Rising { get; init; }
	}

	public record SwitchAttributes
	{
		[JsonPropertyName("actions")]
		public object? Actions { get; init; }

		[JsonPropertyName("attribution")]
		public string? Attribution { get; init; }

		[JsonPropertyName("AwayModeTemperature")]
		public double? AwayModeTemperature { get; init; }

		[JsonPropertyName("brightness")]
		public double? Brightness { get; init; }

		[JsonPropertyName("brightness_pct")]
		public double? BrightnessPct { get; init; }

		[JsonPropertyName("colortemp")]
		public double? Colortemp { get; init; }

		[JsonPropertyName("color_temp_kelvin")]
		public double? ColorTempKelvin { get; init; }

		[JsonPropertyName("color_temp_mired")]
		public double? ColorTempMired { get; init; }

		[JsonPropertyName("current_slot")]
		public object? CurrentSlot { get; init; }

		[JsonPropertyName("device_class")]
		public string? DeviceClass { get; init; }

		[JsonPropertyName("entities")]
		public object? Entities { get; init; }

		[JsonPropertyName("friendly_name")]
		public string? FriendlyName { get; init; }

		[JsonPropertyName("hs_color")]
		public object? HsColor { get; init; }

		[JsonPropertyName("icon")]
		public string? Icon { get; init; }

		[JsonPropertyName("integration")]
		public string? Integration { get; init; }

		[JsonPropertyName("manual_control")]
		public object? ManualControl { get; init; }

		[JsonPropertyName("minutes_remaining")]
		public double? MinutesRemaining { get; init; }

		[JsonPropertyName("next_slot")]
		public double? NextSlot { get; init; }

		[JsonPropertyName("next_trigger")]
		public string? NextTrigger { get; init; }

		[JsonPropertyName("rgb_color")]
		public object? RgbColor { get; init; }

		[JsonPropertyName("status")]
		public string? Status { get; init; }

		[JsonPropertyName("sun_position")]
		public double? SunPosition { get; init; }

		[JsonPropertyName("tags")]
		public object? Tags { get; init; }

		[JsonPropertyName("timeslots")]
		public object? Timeslots { get; init; }

		[JsonPropertyName("weekdays")]
		public object? Weekdays { get; init; }

		[JsonPropertyName("xy_color")]
		public object? XyColor { get; init; }
	}

	public record TimerAttributes
	{
		[JsonPropertyName("icon")]
		public string? Icon { get; init; }

		[JsonPropertyName("restored")]
		public bool? Restored { get; init; }

		[JsonPropertyName("supported_features")]
		public double? SupportedFeatures { get; init; }
	}

	public record WeatherAttributes
	{
		[JsonPropertyName("attribution")]
		public string? Attribution { get; init; }

		[JsonPropertyName("forecast")]
		public object? Forecast { get; init; }

		[JsonPropertyName("friendly_name")]
		public string? FriendlyName { get; init; }

		[JsonPropertyName("humidity")]
		public double? Humidity { get; init; }

		[JsonPropertyName("ozone")]
		public double? Ozone { get; init; }

		[JsonPropertyName("pressure")]
		public double? Pressure { get; init; }

		[JsonPropertyName("temperature")]
		public double? Temperature { get; init; }

		[JsonPropertyName("visibility")]
		public double? Visibility { get; init; }

		[JsonPropertyName("wind_bearing")]
		public double? WindBearing { get; init; }

		[JsonPropertyName("wind_speed")]
		public double? WindSpeed { get; init; }
	}

	public record ZoneAttributes
	{
		[JsonPropertyName("editable")]
		public bool? Editable { get; init; }

		[JsonPropertyName("friendly_name")]
		public string? FriendlyName { get; init; }

		[JsonPropertyName("icon")]
		public string? Icon { get; init; }

		[JsonPropertyName("latitude")]
		public double? Latitude { get; init; }

		[JsonPropertyName("longitude")]
		public double? Longitude { get; init; }

		[JsonPropertyName("passive")]
		public bool? Passive { get; init; }

		[JsonPropertyName("radius")]
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

		ButtonServices Button { get; }

		CameraServices Camera { get; }

		CircadianLightingServices CircadianLighting { get; }

		ClimateServices Climate { get; }

		CloudServices Cloud { get; }

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

		InputButtonServices InputButton { get; }

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

		SirenServices Siren { get; }

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
		private readonly IHaContext _haContext;
		public Services(IHaContext haContext)
		{
			_haContext = haContext;
		}

		public AdaptiveLightingServices AdaptiveLighting => new(_haContext);
		public AlarmControlPanelServices AlarmControlPanel => new(_haContext);
		public AlarmoServices Alarmo => new(_haContext);
		public AlexaMediaServices AlexaMedia => new(_haContext);
		public AudiconnectServices Audiconnect => new(_haContext);
		public AutomationServices Automation => new(_haContext);
		public ButtonServices Button => new(_haContext);
		public CameraServices Camera => new(_haContext);
		public CircadianLightingServices CircadianLighting => new(_haContext);
		public ClimateServices Climate => new(_haContext);
		public CloudServices Cloud => new(_haContext);
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
		public InputButtonServices InputButton => new(_haContext);
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
		public SirenServices Siren => new(_haContext);
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
		private readonly IHaContext _haContext;
		public AdaptiveLightingServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Applies the current Adaptive Lighting settings to lights.</summary>
		public void Apply(AdaptiveLightingApplyParameters data)
		{
			_haContext.CallService("adaptive_lighting", "apply", null, data);
		}

		///<summary>Applies the current Adaptive Lighting settings to lights.</summary>
		///<param name="entityId">entity_id of the Adaptive Lighting switch. eg: switch.adaptive_lighting_default</param>
		///<param name="lights">entity_id(s) of lights, default: lights of the switch eg: light.bedroom_ceiling</param>
		///<param name="transition">Transition of the lights. eg: 10</param>
		///<param name="adaptBrightness">Adapt the 'brightness', default: true eg: True</param>
		///<param name="adaptColor">Adapt the color_temp/color_rgb, default: true eg: True</param>
		///<param name="preferRgbColor">Prefer to use color_rgb over color_temp if possible, default: false eg: False</param>
		///<param name="turnOnLights">Turn on the lights that are off, default: false eg: False</param>
		public void Apply(object? @entityId = null, object? @lights = null, object? @transition = null, object? @adaptBrightness = null, object? @adaptColor = null, object? @preferRgbColor = null, object? @turnOnLights = null)
		{
			_haContext.CallService("adaptive_lighting", "apply", null, new AdaptiveLightingApplyParameters{EntityId = @entityId, Lights = @lights, Transition = @transition, AdaptBrightness = @adaptBrightness, AdaptColor = @adaptColor, PreferRgbColor = @preferRgbColor, TurnOnLights = @turnOnLights});
		}

		///<summary>Mark whether a light is 'manually controlled'.</summary>
		public void SetManualControl(AdaptiveLightingSetManualControlParameters data)
		{
			_haContext.CallService("adaptive_lighting", "set_manual_control", null, data);
		}

		///<summary>Mark whether a light is 'manually controlled'.</summary>
		///<param name="entityId">entity_id of the Adaptive Lighting switch. eg: switch.adaptive_lighting_default</param>
		///<param name="manualControl">Whether to add ('true') or remove ('false') the light from the 'manual_control' list, default: true eg: True</param>
		///<param name="lights">entity_id(s) of lights, if not specified, all lights in the switch are selected. eg: light.bedroom_ceiling</param>
		public void SetManualControl(object? @entityId = null, object? @manualControl = null, object? @lights = null)
		{
			_haContext.CallService("adaptive_lighting", "set_manual_control", null, new AdaptiveLightingSetManualControlParameters{EntityId = @entityId, ManualControl = @manualControl, Lights = @lights});
		}
	}

	public record AdaptiveLightingApplyParameters
	{
		///<summary>entity_id of the Adaptive Lighting switch. eg: switch.adaptive_lighting_default</summary>
		[JsonPropertyName("entity_id")]
		public object? EntityId { get; init; }

		///<summary>entity_id(s) of lights, default: lights of the switch eg: light.bedroom_ceiling</summary>
		[JsonPropertyName("lights")]
		public object? Lights { get; init; }

		///<summary>Transition of the lights. eg: 10</summary>
		[JsonPropertyName("transition")]
		public object? Transition { get; init; }

		///<summary>Adapt the 'brightness', default: true eg: True</summary>
		[JsonPropertyName("adapt_brightness")]
		public object? AdaptBrightness { get; init; }

		///<summary>Adapt the color_temp/color_rgb, default: true eg: True</summary>
		[JsonPropertyName("adapt_color")]
		public object? AdaptColor { get; init; }

		///<summary>Prefer to use color_rgb over color_temp if possible, default: false eg: False</summary>
		[JsonPropertyName("prefer_rgb_color")]
		public object? PreferRgbColor { get; init; }

		///<summary>Turn on the lights that are off, default: false eg: False</summary>
		[JsonPropertyName("turn_on_lights")]
		public object? TurnOnLights { get; init; }
	}

	public record AdaptiveLightingSetManualControlParameters
	{
		///<summary>entity_id of the Adaptive Lighting switch. eg: switch.adaptive_lighting_default</summary>
		[JsonPropertyName("entity_id")]
		public object? EntityId { get; init; }

		///<summary>Whether to add ('true') or remove ('false') the light from the 'manual_control' list, default: true eg: True</summary>
		[JsonPropertyName("manual_control")]
		public object? ManualControl { get; init; }

		///<summary>entity_id(s) of lights, if not specified, all lights in the switch are selected. eg: light.bedroom_ceiling</summary>
		[JsonPropertyName("lights")]
		public object? Lights { get; init; }
	}

	public class AlarmControlPanelServices
	{
		private readonly IHaContext _haContext;
		public AlarmControlPanelServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Send the alarm the command for arm away.</summary>
		///<param name="target">The target for this service call</param>
		public void AlarmArmAway(ServiceTarget target, AlarmControlPanelAlarmArmAwayParameters data)
		{
			_haContext.CallService("alarm_control_panel", "alarm_arm_away", target, data);
		}

		///<summary>Send the alarm the command for arm away.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="code">An optional code to arm away the alarm control panel with. eg: 1234</param>
		public void AlarmArmAway(ServiceTarget target, string? @code = null)
		{
			_haContext.CallService("alarm_control_panel", "alarm_arm_away", target, new AlarmControlPanelAlarmArmAwayParameters{Code = @code});
		}

		///<summary>Send arm custom bypass command.</summary>
		///<param name="target">The target for this service call</param>
		public void AlarmArmCustomBypass(ServiceTarget target, AlarmControlPanelAlarmArmCustomBypassParameters data)
		{
			_haContext.CallService("alarm_control_panel", "alarm_arm_custom_bypass", target, data);
		}

		///<summary>Send arm custom bypass command.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="code">An optional code to arm custom bypass the alarm control panel with. eg: 1234</param>
		public void AlarmArmCustomBypass(ServiceTarget target, string? @code = null)
		{
			_haContext.CallService("alarm_control_panel", "alarm_arm_custom_bypass", target, new AlarmControlPanelAlarmArmCustomBypassParameters{Code = @code});
		}

		///<summary>Send the alarm the command for arm home.</summary>
		///<param name="target">The target for this service call</param>
		public void AlarmArmHome(ServiceTarget target, AlarmControlPanelAlarmArmHomeParameters data)
		{
			_haContext.CallService("alarm_control_panel", "alarm_arm_home", target, data);
		}

		///<summary>Send the alarm the command for arm home.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="code">An optional code to arm home the alarm control panel with. eg: 1234</param>
		public void AlarmArmHome(ServiceTarget target, string? @code = null)
		{
			_haContext.CallService("alarm_control_panel", "alarm_arm_home", target, new AlarmControlPanelAlarmArmHomeParameters{Code = @code});
		}

		///<summary>Send the alarm the command for arm night.</summary>
		///<param name="target">The target for this service call</param>
		public void AlarmArmNight(ServiceTarget target, AlarmControlPanelAlarmArmNightParameters data)
		{
			_haContext.CallService("alarm_control_panel", "alarm_arm_night", target, data);
		}

		///<summary>Send the alarm the command for arm night.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="code">An optional code to arm night the alarm control panel with. eg: 1234</param>
		public void AlarmArmNight(ServiceTarget target, string? @code = null)
		{
			_haContext.CallService("alarm_control_panel", "alarm_arm_night", target, new AlarmControlPanelAlarmArmNightParameters{Code = @code});
		}

		///<summary>Send the alarm the command for arm vacation.</summary>
		///<param name="target">The target for this service call</param>
		public void AlarmArmVacation(ServiceTarget target, AlarmControlPanelAlarmArmVacationParameters data)
		{
			_haContext.CallService("alarm_control_panel", "alarm_arm_vacation", target, data);
		}

		///<summary>Send the alarm the command for arm vacation.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="code">An optional code to arm vacation the alarm control panel with. eg: 1234</param>
		public void AlarmArmVacation(ServiceTarget target, string? @code = null)
		{
			_haContext.CallService("alarm_control_panel", "alarm_arm_vacation", target, new AlarmControlPanelAlarmArmVacationParameters{Code = @code});
		}

		///<summary>Send the alarm the command for disarm.</summary>
		///<param name="target">The target for this service call</param>
		public void AlarmDisarm(ServiceTarget target, AlarmControlPanelAlarmDisarmParameters data)
		{
			_haContext.CallService("alarm_control_panel", "alarm_disarm", target, data);
		}

		///<summary>Send the alarm the command for disarm.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="code">An optional code to disarm the alarm control panel with. eg: 1234</param>
		public void AlarmDisarm(ServiceTarget target, string? @code = null)
		{
			_haContext.CallService("alarm_control_panel", "alarm_disarm", target, new AlarmControlPanelAlarmDisarmParameters{Code = @code});
		}

		///<summary>Send the alarm the command for trigger.</summary>
		///<param name="target">The target for this service call</param>
		public void AlarmTrigger(ServiceTarget target, AlarmControlPanelAlarmTriggerParameters data)
		{
			_haContext.CallService("alarm_control_panel", "alarm_trigger", target, data);
		}

		///<summary>Send the alarm the command for trigger.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="code">An optional code to trigger the alarm control panel with. eg: 1234</param>
		public void AlarmTrigger(ServiceTarget target, string? @code = null)
		{
			_haContext.CallService("alarm_control_panel", "alarm_trigger", target, new AlarmControlPanelAlarmTriggerParameters{Code = @code});
		}
	}

	public record AlarmControlPanelAlarmArmAwayParameters
	{
		///<summary>An optional code to arm away the alarm control panel with. eg: 1234</summary>
		[JsonPropertyName("code")]
		public string? Code { get; init; }
	}

	public record AlarmControlPanelAlarmArmCustomBypassParameters
	{
		///<summary>An optional code to arm custom bypass the alarm control panel with. eg: 1234</summary>
		[JsonPropertyName("code")]
		public string? Code { get; init; }
	}

	public record AlarmControlPanelAlarmArmHomeParameters
	{
		///<summary>An optional code to arm home the alarm control panel with. eg: 1234</summary>
		[JsonPropertyName("code")]
		public string? Code { get; init; }
	}

	public record AlarmControlPanelAlarmArmNightParameters
	{
		///<summary>An optional code to arm night the alarm control panel with. eg: 1234</summary>
		[JsonPropertyName("code")]
		public string? Code { get; init; }
	}

	public record AlarmControlPanelAlarmArmVacationParameters
	{
		///<summary>An optional code to arm vacation the alarm control panel with. eg: 1234</summary>
		[JsonPropertyName("code")]
		public string? Code { get; init; }
	}

	public record AlarmControlPanelAlarmDisarmParameters
	{
		///<summary>An optional code to disarm the alarm control panel with. eg: 1234</summary>
		[JsonPropertyName("code")]
		public string? Code { get; init; }
	}

	public record AlarmControlPanelAlarmTriggerParameters
	{
		///<summary>An optional code to trigger the alarm control panel with. eg: 1234</summary>
		[JsonPropertyName("code")]
		public string? Code { get; init; }
	}

	public class AlarmoServices
	{
		private readonly IHaContext _haContext;
		public AlarmoServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Arm an Alarmo entity with custom settings.</summary>
		public void Arm(AlarmoArmParameters data)
		{
			_haContext.CallService("alarmo", "arm", null, data);
		}

		///<summary>Arm an Alarmo entity with custom settings.</summary>
		///<param name="entityId">Name of entity that should be armed. eg: alarm_control_panel.alarm</param>
		///<param name="code">Code to arm the alarm with. eg: 1234</param>
		///<param name="mode">Mode to arm the alarm in. eg: away</param>
		///<param name="skipDelay">Skip the exit delay. eg: True</param>
		///<param name="force">Automatically bypass all sensors that prevent the arming operation. eg: True</param>
		public void Arm(string @entityId, string? @code = null, string? @mode = null, bool? @skipDelay = null, bool? @force = null)
		{
			_haContext.CallService("alarmo", "arm", null, new AlarmoArmParameters{EntityId = @entityId, Code = @code, Mode = @mode, SkipDelay = @skipDelay, Force = @force});
		}

		///<summary>Disarm an Alarmo entity.</summary>
		public void Disarm(AlarmoDisarmParameters data)
		{
			_haContext.CallService("alarmo", "disarm", null, data);
		}

		///<summary>Disarm an Alarmo entity.</summary>
		///<param name="entityId">Name of entity that should be disarmed. eg: alarm_control_panel.alarm</param>
		///<param name="code">Code to disarm the alarm with. eg: 1234</param>
		public void Disarm(string @entityId, string? @code = null)
		{
			_haContext.CallService("alarmo", "disarm", null, new AlarmoDisarmParameters{EntityId = @entityId, Code = @code});
		}
	}

	public record AlarmoArmParameters
	{
		///<summary>Name of entity that should be armed. eg: alarm_control_panel.alarm</summary>
		[JsonPropertyName("entity_id")]
		public string? EntityId { get; init; }

		///<summary>Code to arm the alarm with. eg: 1234</summary>
		[JsonPropertyName("code")]
		public string? Code { get; init; }

		///<summary>Mode to arm the alarm in. eg: away</summary>
		[JsonPropertyName("mode")]
		public string? Mode { get; init; }

		///<summary>Skip the exit delay. eg: True</summary>
		[JsonPropertyName("skip_delay")]
		public bool? SkipDelay { get; init; }

		///<summary>Automatically bypass all sensors that prevent the arming operation. eg: True</summary>
		[JsonPropertyName("force")]
		public bool? Force { get; init; }
	}

	public record AlarmoDisarmParameters
	{
		///<summary>Name of entity that should be disarmed. eg: alarm_control_panel.alarm</summary>
		[JsonPropertyName("entity_id")]
		public string? EntityId { get; init; }

		///<summary>Code to disarm the alarm with. eg: 1234</summary>
		[JsonPropertyName("code")]
		public string? Code { get; init; }
	}

	public class AlexaMediaServices
	{
		private readonly IHaContext _haContext;
		public AlexaMediaServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Clear last entries from Alexa history for each Alexa account.</summary>
		public void ClearHistory(AlexaMediaClearHistoryParameters data)
		{
			_haContext.CallService("alexa_media", "clear_history", null, data);
		}

		///<summary>Clear last entries from Alexa history for each Alexa account.</summary>
		///<param name="email">List of Alexa accounts to update. If empty, will delete from all known accounts. eg: my_email@alexa.com</param>
		///<param name="entries">Number of entries to clear from 1 to 50. If empty, clear 50. eg: 50</param>
		public void ClearHistory(object? @email = null, object? @entries = null)
		{
			_haContext.CallService("alexa_media", "clear_history", null, new AlexaMediaClearHistoryParameters{Email = @email, Entries = @entries});
		}

		///<summary>Force logout of Alexa Login account and deletion of .pickle. Intended for debugging use.</summary>
		public void ForceLogout(AlexaMediaForceLogoutParameters data)
		{
			_haContext.CallService("alexa_media", "force_logout", null, data);
		}

		///<summary>Force logout of Alexa Login account and deletion of .pickle. Intended for debugging use.</summary>
		///<param name="email">List of Alexa accounts to log out. If empty, will log out from all known accounts. eg: my_email@alexa.com</param>
		public void ForceLogout(object? @email = null)
		{
			_haContext.CallService("alexa_media", "force_logout", null, new AlexaMediaForceLogoutParameters{Email = @email});
		}

		///<summary>Forces update of last_called echo device for each Alexa account.</summary>
		public void UpdateLastCalled(AlexaMediaUpdateLastCalledParameters data)
		{
			_haContext.CallService("alexa_media", "update_last_called", null, data);
		}

		///<summary>Forces update of last_called echo device for each Alexa account.</summary>
		///<param name="email">List of Alexa accounts to update. If empty, will update all known accounts. eg: my_email@alexa.com</param>
		public void UpdateLastCalled(object? @email = null)
		{
			_haContext.CallService("alexa_media", "update_last_called", null, new AlexaMediaUpdateLastCalledParameters{Email = @email});
		}
	}

	public record AlexaMediaClearHistoryParameters
	{
		///<summary>List of Alexa accounts to update. If empty, will delete from all known accounts. eg: my_email@alexa.com</summary>
		[JsonPropertyName("email")]
		public object? Email { get; init; }

		///<summary>Number of entries to clear from 1 to 50. If empty, clear 50. eg: 50</summary>
		[JsonPropertyName("entries")]
		public object? Entries { get; init; }
	}

	public record AlexaMediaForceLogoutParameters
	{
		///<summary>List of Alexa accounts to log out. If empty, will log out from all known accounts. eg: my_email@alexa.com</summary>
		[JsonPropertyName("email")]
		public object? Email { get; init; }
	}

	public record AlexaMediaUpdateLastCalledParameters
	{
		///<summary>List of Alexa accounts to update. If empty, will update all known accounts. eg: my_email@alexa.com</summary>
		[JsonPropertyName("email")]
		public object? Email { get; init; }
	}

	public class AudiconnectServices
	{
		private readonly IHaContext _haContext;
		public AudiconnectServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Request an update of the state from the vehicle, as opposed to the normal update  mechanism, which only retrieves data from the servers. </summary>
		public void ExecuteVehicleAction(AudiconnectExecuteVehicleActionParameters data)
		{
			_haContext.CallService("audiconnect", "execute_vehicle_action", null, data);
		}

		///<summary>Request an update of the state from the vehicle, as opposed to the normal update  mechanism, which only retrieves data from the servers. </summary>
		///<param name="vin">The vehicle identification number (VIN) of the vehicle, 17 characters  eg: WBANXXXXXX1234567</param>
		///<param name="action">The action to be executed. Possible choices, depending on subscription options, include 'lock', 'unlock',  'start_climatisation', 'stop_climatisation, 'start_preheater', 'stop_preheater, 'start_window_heating', 'stop_window_heating'  eg: lock</param>
		public void ExecuteVehicleAction(object? @vin = null, object? @action = null)
		{
			_haContext.CallService("audiconnect", "execute_vehicle_action", null, new AudiconnectExecuteVehicleActionParameters{Vin = @vin, Action = @action});
		}

		public void RefreshData()
		{
			_haContext.CallService("audiconnect", "refresh_data", null);
		}
	}

	public record AudiconnectExecuteVehicleActionParameters
	{
		///<summary>The vehicle identification number (VIN) of the vehicle, 17 characters  eg: WBANXXXXXX1234567</summary>
		[JsonPropertyName("vin")]
		public object? Vin { get; init; }

		///<summary>The action to be executed. Possible choices, depending on subscription options, include 'lock', 'unlock',  'start_climatisation', 'stop_climatisation, 'start_preheater', 'stop_preheater, 'start_window_heating', 'stop_window_heating'  eg: lock</summary>
		[JsonPropertyName("action")]
		public object? Action { get; init; }
	}

	public class AutomationServices
	{
		private readonly IHaContext _haContext;
		public AutomationServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Reload the automation configuration.</summary>
		public void Reload()
		{
			_haContext.CallService("automation", "reload", null);
		}

		///<summary>Toggle (enable / disable) an automation.</summary>
		///<param name="target">The target for this service call</param>
		public void Toggle(ServiceTarget target)
		{
			_haContext.CallService("automation", "toggle", target);
		}

		///<summary>Trigger the actions of an automation.</summary>
		///<param name="target">The target for this service call</param>
		public void Trigger(ServiceTarget target, AutomationTriggerParameters data)
		{
			_haContext.CallService("automation", "trigger", target, data);
		}

		///<summary>Trigger the actions of an automation.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="skipCondition">Whether or not the conditions will be skipped.</param>
		public void Trigger(ServiceTarget target, bool? @skipCondition = null)
		{
			_haContext.CallService("automation", "trigger", target, new AutomationTriggerParameters{SkipCondition = @skipCondition});
		}

		///<summary>Disable an automation.</summary>
		///<param name="target">The target for this service call</param>
		public void TurnOff(ServiceTarget target, AutomationTurnOffParameters data)
		{
			_haContext.CallService("automation", "turn_off", target, data);
		}

		///<summary>Disable an automation.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="stopActions">Stop currently running actions.</param>
		public void TurnOff(ServiceTarget target, bool? @stopActions = null)
		{
			_haContext.CallService("automation", "turn_off", target, new AutomationTurnOffParameters{StopActions = @stopActions});
		}

		///<summary>Enable an automation.</summary>
		///<param name="target">The target for this service call</param>
		public void TurnOn(ServiceTarget target)
		{
			_haContext.CallService("automation", "turn_on", target);
		}
	}

	public record AutomationTriggerParameters
	{
		///<summary>Whether or not the conditions will be skipped.</summary>
		[JsonPropertyName("skip_condition")]
		public bool? SkipCondition { get; init; }
	}

	public record AutomationTurnOffParameters
	{
		///<summary>Stop currently running actions.</summary>
		[JsonPropertyName("stop_actions")]
		public bool? StopActions { get; init; }
	}

	public class ButtonServices
	{
		private readonly IHaContext _haContext;
		public ButtonServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Press the button entity.</summary>
		///<param name="target">The target for this service call</param>
		public void Press(ServiceTarget target)
		{
			_haContext.CallService("button", "press", target);
		}
	}

	public class CameraServices
	{
		private readonly IHaContext _haContext;
		public CameraServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Disable the motion detection in a camera.</summary>
		///<param name="target">The target for this service call</param>
		public void DisableMotionDetection(ServiceTarget target)
		{
			_haContext.CallService("camera", "disable_motion_detection", target);
		}

		///<summary>Enable the motion detection in a camera.</summary>
		///<param name="target">The target for this service call</param>
		public void EnableMotionDetection(ServiceTarget target)
		{
			_haContext.CallService("camera", "enable_motion_detection", target);
		}

		///<summary>Play camera stream on supported media player.</summary>
		///<param name="target">The target for this service call</param>
		public void PlayStream(ServiceTarget target, CameraPlayStreamParameters data)
		{
			_haContext.CallService("camera", "play_stream", target, data);
		}

		///<summary>Play camera stream on supported media player.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="mediaPlayer">Name(s) of media player to stream to.</param>
		///<param name="format">Stream format supported by media player.</param>
		public void PlayStream(ServiceTarget target, string @mediaPlayer, string? @format = null)
		{
			_haContext.CallService("camera", "play_stream", target, new CameraPlayStreamParameters{MediaPlayer = @mediaPlayer, Format = @format});
		}

		///<summary>Record live camera feed.</summary>
		///<param name="target">The target for this service call</param>
		public void Record(ServiceTarget target, CameraRecordParameters data)
		{
			_haContext.CallService("camera", "record", target, data);
		}

		///<summary>Record live camera feed.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="filename">Template of a Filename. Variable is entity_id. Must be mp4. eg: /tmp/snapshot_{{ entity_id.name }}.mp4</param>
		///<param name="duration">Target recording length.</param>
		///<param name="lookback">Target lookback period to include in addition to duration. Only available if there is currently an active HLS stream.</param>
		public void Record(ServiceTarget target, string @filename, long? @duration = null, long? @lookback = null)
		{
			_haContext.CallService("camera", "record", target, new CameraRecordParameters{Filename = @filename, Duration = @duration, Lookback = @lookback});
		}

		///<summary>Take a snapshot from a camera.</summary>
		///<param name="target">The target for this service call</param>
		public void Snapshot(ServiceTarget target, CameraSnapshotParameters data)
		{
			_haContext.CallService("camera", "snapshot", target, data);
		}

		///<summary>Take a snapshot from a camera.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="filename">Template of a Filename. Variable is entity_id. eg: /tmp/snapshot_{{ entity_id.name }}.jpg</param>
		public void Snapshot(ServiceTarget target, string @filename)
		{
			_haContext.CallService("camera", "snapshot", target, new CameraSnapshotParameters{Filename = @filename});
		}

		///<summary>Turn off camera.</summary>
		///<param name="target">The target for this service call</param>
		public void TurnOff(ServiceTarget target)
		{
			_haContext.CallService("camera", "turn_off", target);
		}

		///<summary>Turn on camera.</summary>
		///<param name="target">The target for this service call</param>
		public void TurnOn(ServiceTarget target)
		{
			_haContext.CallService("camera", "turn_on", target);
		}
	}

	public record CameraPlayStreamParameters
	{
		///<summary>Name(s) of media player to stream to.</summary>
		[JsonPropertyName("media_player")]
		public string? MediaPlayer { get; init; }

		///<summary>Stream format supported by media player.</summary>
		[JsonPropertyName("format")]
		public string? Format { get; init; }
	}

	public record CameraRecordParameters
	{
		///<summary>Template of a Filename. Variable is entity_id. Must be mp4. eg: /tmp/snapshot_{{ entity_id.name }}.mp4</summary>
		[JsonPropertyName("filename")]
		public string? Filename { get; init; }

		///<summary>Target recording length.</summary>
		[JsonPropertyName("duration")]
		public long? Duration { get; init; }

		///<summary>Target lookback period to include in addition to duration. Only available if there is currently an active HLS stream.</summary>
		[JsonPropertyName("lookback")]
		public long? Lookback { get; init; }
	}

	public record CameraSnapshotParameters
	{
		///<summary>Template of a Filename. Variable is entity_id. eg: /tmp/snapshot_{{ entity_id.name }}.jpg</summary>
		[JsonPropertyName("filename")]
		public string? Filename { get; init; }
	}

	public class CircadianLightingServices
	{
		private readonly IHaContext _haContext;
		public CircadianLightingServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Updates values for Circadian Lighting.</summary>
		public void ValuesUpdate()
		{
			_haContext.CallService("circadian_lighting", "values_update", null);
		}
	}

	public class ClimateServices
	{
		private readonly IHaContext _haContext;
		public ClimateServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Turn auxiliary heater on/off for climate device.</summary>
		///<param name="target">The target for this service call</param>
		public void SetAuxHeat(ServiceTarget target, ClimateSetAuxHeatParameters data)
		{
			_haContext.CallService("climate", "set_aux_heat", target, data);
		}

		///<summary>Turn auxiliary heater on/off for climate device.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="auxHeat">New value of auxiliary heater.</param>
		public void SetAuxHeat(ServiceTarget target, bool @auxHeat)
		{
			_haContext.CallService("climate", "set_aux_heat", target, new ClimateSetAuxHeatParameters{AuxHeat = @auxHeat});
		}

		///<summary>Set fan operation for climate device.</summary>
		///<param name="target">The target for this service call</param>
		public void SetFanMode(ServiceTarget target, ClimateSetFanModeParameters data)
		{
			_haContext.CallService("climate", "set_fan_mode", target, data);
		}

		///<summary>Set fan operation for climate device.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="fanMode">New value of fan mode. eg: low</param>
		public void SetFanMode(ServiceTarget target, string @fanMode)
		{
			_haContext.CallService("climate", "set_fan_mode", target, new ClimateSetFanModeParameters{FanMode = @fanMode});
		}

		///<summary>Set target humidity of climate device.</summary>
		///<param name="target">The target for this service call</param>
		public void SetHumidity(ServiceTarget target, ClimateSetHumidityParameters data)
		{
			_haContext.CallService("climate", "set_humidity", target, data);
		}

		///<summary>Set target humidity of climate device.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="humidity">New target humidity for climate device.</param>
		public void SetHumidity(ServiceTarget target, long @humidity)
		{
			_haContext.CallService("climate", "set_humidity", target, new ClimateSetHumidityParameters{Humidity = @humidity});
		}

		///<summary>Set HVAC operation mode for climate device.</summary>
		///<param name="target">The target for this service call</param>
		public void SetHvacMode(ServiceTarget target, ClimateSetHvacModeParameters data)
		{
			_haContext.CallService("climate", "set_hvac_mode", target, data);
		}

		///<summary>Set HVAC operation mode for climate device.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="hvacMode">New value of operation mode.</param>
		public void SetHvacMode(ServiceTarget target, string? @hvacMode = null)
		{
			_haContext.CallService("climate", "set_hvac_mode", target, new ClimateSetHvacModeParameters{HvacMode = @hvacMode});
		}

		///<summary>Set preset mode for climate device.</summary>
		///<param name="target">The target for this service call</param>
		public void SetPresetMode(ServiceTarget target, ClimateSetPresetModeParameters data)
		{
			_haContext.CallService("climate", "set_preset_mode", target, data);
		}

		///<summary>Set preset mode for climate device.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="presetMode">New value of preset mode. eg: away</param>
		public void SetPresetMode(ServiceTarget target, string @presetMode)
		{
			_haContext.CallService("climate", "set_preset_mode", target, new ClimateSetPresetModeParameters{PresetMode = @presetMode});
		}

		///<summary>Set swing operation for climate device.</summary>
		///<param name="target">The target for this service call</param>
		public void SetSwingMode(ServiceTarget target, ClimateSetSwingModeParameters data)
		{
			_haContext.CallService("climate", "set_swing_mode", target, data);
		}

		///<summary>Set swing operation for climate device.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="swingMode">New value of swing mode. eg: horizontal</param>
		public void SetSwingMode(ServiceTarget target, string @swingMode)
		{
			_haContext.CallService("climate", "set_swing_mode", target, new ClimateSetSwingModeParameters{SwingMode = @swingMode});
		}

		///<summary>Set target temperature of climate device.</summary>
		///<param name="target">The target for this service call</param>
		public void SetTemperature(ServiceTarget target, ClimateSetTemperatureParameters data)
		{
			_haContext.CallService("climate", "set_temperature", target, data);
		}

		///<summary>Set target temperature of climate device.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="temperature">New target temperature for HVAC.</param>
		///<param name="targetTempHigh">New target high temperature for HVAC.</param>
		///<param name="targetTempLow">New target low temperature for HVAC.</param>
		///<param name="hvacMode">HVAC operation mode to set temperature to.</param>
		public void SetTemperature(ServiceTarget target, double? @temperature = null, double? @targetTempHigh = null, double? @targetTempLow = null, string? @hvacMode = null)
		{
			_haContext.CallService("climate", "set_temperature", target, new ClimateSetTemperatureParameters{Temperature = @temperature, TargetTempHigh = @targetTempHigh, TargetTempLow = @targetTempLow, HvacMode = @hvacMode});
		}

		///<summary>Turn climate device off.</summary>
		///<param name="target">The target for this service call</param>
		public void TurnOff(ServiceTarget target)
		{
			_haContext.CallService("climate", "turn_off", target);
		}

		///<summary>Turn climate device on.</summary>
		///<param name="target">The target for this service call</param>
		public void TurnOn(ServiceTarget target)
		{
			_haContext.CallService("climate", "turn_on", target);
		}
	}

	public record ClimateSetAuxHeatParameters
	{
		///<summary>New value of auxiliary heater.</summary>
		[JsonPropertyName("aux_heat")]
		public bool? AuxHeat { get; init; }
	}

	public record ClimateSetFanModeParameters
	{
		///<summary>New value of fan mode. eg: low</summary>
		[JsonPropertyName("fan_mode")]
		public string? FanMode { get; init; }
	}

	public record ClimateSetHumidityParameters
	{
		///<summary>New target humidity for climate device.</summary>
		[JsonPropertyName("humidity")]
		public long? Humidity { get; init; }
	}

	public record ClimateSetHvacModeParameters
	{
		///<summary>New value of operation mode.</summary>
		[JsonPropertyName("hvac_mode")]
		public string? HvacMode { get; init; }
	}

	public record ClimateSetPresetModeParameters
	{
		///<summary>New value of preset mode. eg: away</summary>
		[JsonPropertyName("preset_mode")]
		public string? PresetMode { get; init; }
	}

	public record ClimateSetSwingModeParameters
	{
		///<summary>New value of swing mode. eg: horizontal</summary>
		[JsonPropertyName("swing_mode")]
		public string? SwingMode { get; init; }
	}

	public record ClimateSetTemperatureParameters
	{
		///<summary>New target temperature for HVAC.</summary>
		[JsonPropertyName("temperature")]
		public double? Temperature { get; init; }

		///<summary>New target high temperature for HVAC.</summary>
		[JsonPropertyName("target_temp_high")]
		public double? TargetTempHigh { get; init; }

		///<summary>New target low temperature for HVAC.</summary>
		[JsonPropertyName("target_temp_low")]
		public double? TargetTempLow { get; init; }

		///<summary>HVAC operation mode to set temperature to.</summary>
		[JsonPropertyName("hvac_mode")]
		public string? HvacMode { get; init; }
	}

	public class CloudServices
	{
		private readonly IHaContext _haContext;
		public CloudServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Make instance UI available outside over NabuCasa cloud</summary>
		public void RemoteConnect()
		{
			_haContext.CallService("cloud", "remote_connect", null);
		}

		///<summary>Disconnect UI from NabuCasa cloud</summary>
		public void RemoteDisconnect()
		{
			_haContext.CallService("cloud", "remote_disconnect", null);
		}
	}

	public class CounterServices
	{
		private readonly IHaContext _haContext;
		public CounterServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Change counter parameters.</summary>
		///<param name="target">The target for this service call</param>
		public void Configure(ServiceTarget target, CounterConfigureParameters data)
		{
			_haContext.CallService("counter", "configure", target, data);
		}

		///<summary>Change counter parameters.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="minimum">New minimum value for the counter or None to remove minimum.</param>
		///<param name="maximum">New maximum value for the counter or None to remove maximum.</param>
		///<param name="step">New value for step.</param>
		///<param name="initial">New value for initial.</param>
		///<param name="value">New state value.</param>
		public void Configure(ServiceTarget target, long? @minimum = null, long? @maximum = null, long? @step = null, long? @initial = null, long? @value = null)
		{
			_haContext.CallService("counter", "configure", target, new CounterConfigureParameters{Minimum = @minimum, Maximum = @maximum, Step = @step, Initial = @initial, Value = @value});
		}

		///<summary>Decrement a counter.</summary>
		///<param name="target">The target for this service call</param>
		public void Decrement(ServiceTarget target)
		{
			_haContext.CallService("counter", "decrement", target);
		}

		///<summary>Increment a counter.</summary>
		///<param name="target">The target for this service call</param>
		public void Increment(ServiceTarget target)
		{
			_haContext.CallService("counter", "increment", target);
		}

		///<summary>Reset a counter.</summary>
		///<param name="target">The target for this service call</param>
		public void Reset(ServiceTarget target)
		{
			_haContext.CallService("counter", "reset", target);
		}
	}

	public record CounterConfigureParameters
	{
		///<summary>New minimum value for the counter or None to remove minimum.</summary>
		[JsonPropertyName("minimum")]
		public long? Minimum { get; init; }

		///<summary>New maximum value for the counter or None to remove maximum.</summary>
		[JsonPropertyName("maximum")]
		public long? Maximum { get; init; }

		///<summary>New value for step.</summary>
		[JsonPropertyName("step")]
		public long? Step { get; init; }

		///<summary>New value for initial.</summary>
		[JsonPropertyName("initial")]
		public long? Initial { get; init; }

		///<summary>New state value.</summary>
		[JsonPropertyName("value")]
		public long? Value { get; init; }
	}

	public class CoverServices
	{
		private readonly IHaContext _haContext;
		public CoverServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Close all or specified cover.</summary>
		///<param name="target">The target for this service call</param>
		public void CloseCover(ServiceTarget target)
		{
			_haContext.CallService("cover", "close_cover", target);
		}

		///<summary>Close all or specified cover tilt.</summary>
		///<param name="target">The target for this service call</param>
		public void CloseCoverTilt(ServiceTarget target)
		{
			_haContext.CallService("cover", "close_cover_tilt", target);
		}

		///<summary>Open all or specified cover.</summary>
		///<param name="target">The target for this service call</param>
		public void OpenCover(ServiceTarget target)
		{
			_haContext.CallService("cover", "open_cover", target);
		}

		///<summary>Open all or specified cover tilt.</summary>
		///<param name="target">The target for this service call</param>
		public void OpenCoverTilt(ServiceTarget target)
		{
			_haContext.CallService("cover", "open_cover_tilt", target);
		}

		///<summary>Move to specific position all or specified cover.</summary>
		///<param name="target">The target for this service call</param>
		public void SetCoverPosition(ServiceTarget target, CoverSetCoverPositionParameters data)
		{
			_haContext.CallService("cover", "set_cover_position", target, data);
		}

		///<summary>Move to specific position all or specified cover.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="position">Position of the cover</param>
		public void SetCoverPosition(ServiceTarget target, long @position)
		{
			_haContext.CallService("cover", "set_cover_position", target, new CoverSetCoverPositionParameters{Position = @position});
		}

		///<summary>Move to specific position all or specified cover tilt.</summary>
		///<param name="target">The target for this service call</param>
		public void SetCoverTiltPosition(ServiceTarget target, CoverSetCoverTiltPositionParameters data)
		{
			_haContext.CallService("cover", "set_cover_tilt_position", target, data);
		}

		///<summary>Move to specific position all or specified cover tilt.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="tiltPosition">Tilt position of the cover.</param>
		public void SetCoverTiltPosition(ServiceTarget target, long @tiltPosition)
		{
			_haContext.CallService("cover", "set_cover_tilt_position", target, new CoverSetCoverTiltPositionParameters{TiltPosition = @tiltPosition});
		}

		///<summary>Stop all or specified cover.</summary>
		///<param name="target">The target for this service call</param>
		public void StopCover(ServiceTarget target)
		{
			_haContext.CallService("cover", "stop_cover", target);
		}

		///<summary>Stop all or specified cover.</summary>
		///<param name="target">The target for this service call</param>
		public void StopCoverTilt(ServiceTarget target)
		{
			_haContext.CallService("cover", "stop_cover_tilt", target);
		}

		///<summary>Toggle a cover open/closed.</summary>
		///<param name="target">The target for this service call</param>
		public void Toggle(ServiceTarget target)
		{
			_haContext.CallService("cover", "toggle", target);
		}

		///<summary>Toggle a cover tilt open/closed.</summary>
		///<param name="target">The target for this service call</param>
		public void ToggleCoverTilt(ServiceTarget target)
		{
			_haContext.CallService("cover", "toggle_cover_tilt", target);
		}
	}

	public record CoverSetCoverPositionParameters
	{
		///<summary>Position of the cover</summary>
		[JsonPropertyName("position")]
		public long? Position { get; init; }
	}

	public record CoverSetCoverTiltPositionParameters
	{
		///<summary>Tilt position of the cover.</summary>
		[JsonPropertyName("tilt_position")]
		public long? TiltPosition { get; init; }
	}

	public class DeviceTrackerServices
	{
		private readonly IHaContext _haContext;
		public DeviceTrackerServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Control tracked device.</summary>
		public void See(DeviceTrackerSeeParameters data)
		{
			_haContext.CallService("device_tracker", "see", null, data);
		}

		///<summary>Control tracked device.</summary>
		///<param name="mac">MAC address of device eg: FF:FF:FF:FF:FF:FF</param>
		///<param name="devId">Id of device (find id in known_devices.yaml). eg: phonedave</param>
		///<param name="hostName">Hostname of device eg: Dave</param>
		///<param name="locationName">Name of location where device is located (not_home is away). eg: home</param>
		///<param name="gps">GPS coordinates where device is located (latitude, longitude). eg: [51.509802, -0.086692]</param>
		///<param name="gpsAccuracy">Accuracy of GPS coordinates.</param>
		///<param name="battery">Battery level of device.</param>
		public void See(string? @mac = null, string? @devId = null, string? @hostName = null, string? @locationName = null, object? @gps = null, long? @gpsAccuracy = null, long? @battery = null)
		{
			_haContext.CallService("device_tracker", "see", null, new DeviceTrackerSeeParameters{Mac = @mac, DevId = @devId, HostName = @hostName, LocationName = @locationName, Gps = @gps, GpsAccuracy = @gpsAccuracy, Battery = @battery});
		}
	}

	public record DeviceTrackerSeeParameters
	{
		///<summary>MAC address of device eg: FF:FF:FF:FF:FF:FF</summary>
		[JsonPropertyName("mac")]
		public string? Mac { get; init; }

		///<summary>Id of device (find id in known_devices.yaml). eg: phonedave</summary>
		[JsonPropertyName("dev_id")]
		public string? DevId { get; init; }

		///<summary>Hostname of device eg: Dave</summary>
		[JsonPropertyName("host_name")]
		public string? HostName { get; init; }

		///<summary>Name of location where device is located (not_home is away). eg: home</summary>
		[JsonPropertyName("location_name")]
		public string? LocationName { get; init; }

		///<summary>GPS coordinates where device is located (latitude, longitude). eg: [51.509802, -0.086692]</summary>
		[JsonPropertyName("gps")]
		public object? Gps { get; init; }

		///<summary>Accuracy of GPS coordinates.</summary>
		[JsonPropertyName("gps_accuracy")]
		public long? GpsAccuracy { get; init; }

		///<summary>Battery level of device.</summary>
		[JsonPropertyName("battery")]
		public long? Battery { get; init; }
	}

	public class FanServices
	{
		private readonly IHaContext _haContext;
		public FanServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Decrease the speed of the fan by one speed or a percentage_step.</summary>
		///<param name="target">The target for this service call</param>
		public void DecreaseSpeed(ServiceTarget target, FanDecreaseSpeedParameters data)
		{
			_haContext.CallService("fan", "decrease_speed", target, data);
		}

		///<summary>Decrease the speed of the fan by one speed or a percentage_step.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="percentageStep">Decrease speed by a percentage.</param>
		public void DecreaseSpeed(ServiceTarget target, long? @percentageStep = null)
		{
			_haContext.CallService("fan", "decrease_speed", target, new FanDecreaseSpeedParameters{PercentageStep = @percentageStep});
		}

		///<summary>Increase the speed of the fan by one speed or a percentage_step.</summary>
		///<param name="target">The target for this service call</param>
		public void IncreaseSpeed(ServiceTarget target, FanIncreaseSpeedParameters data)
		{
			_haContext.CallService("fan", "increase_speed", target, data);
		}

		///<summary>Increase the speed of the fan by one speed or a percentage_step.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="percentageStep">Increase speed by a percentage.</param>
		public void IncreaseSpeed(ServiceTarget target, long? @percentageStep = null)
		{
			_haContext.CallService("fan", "increase_speed", target, new FanIncreaseSpeedParameters{PercentageStep = @percentageStep});
		}

		///<summary>Oscillate the fan.</summary>
		///<param name="target">The target for this service call</param>
		public void Oscillate(ServiceTarget target, FanOscillateParameters data)
		{
			_haContext.CallService("fan", "oscillate", target, data);
		}

		///<summary>Oscillate the fan.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="oscillating">Flag to turn on/off oscillation.</param>
		public void Oscillate(ServiceTarget target, bool @oscillating)
		{
			_haContext.CallService("fan", "oscillate", target, new FanOscillateParameters{Oscillating = @oscillating});
		}

		///<summary>Set the fan rotation.</summary>
		///<param name="target">The target for this service call</param>
		public void SetDirection(ServiceTarget target, FanSetDirectionParameters data)
		{
			_haContext.CallService("fan", "set_direction", target, data);
		}

		///<summary>Set the fan rotation.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="direction">The direction to rotate.</param>
		public void SetDirection(ServiceTarget target, string @direction)
		{
			_haContext.CallService("fan", "set_direction", target, new FanSetDirectionParameters{Direction = @direction});
		}

		///<summary>Set fan speed percentage.</summary>
		///<param name="target">The target for this service call</param>
		public void SetPercentage(ServiceTarget target, FanSetPercentageParameters data)
		{
			_haContext.CallService("fan", "set_percentage", target, data);
		}

		///<summary>Set fan speed percentage.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="percentage">Percentage speed setting.</param>
		public void SetPercentage(ServiceTarget target, long @percentage)
		{
			_haContext.CallService("fan", "set_percentage", target, new FanSetPercentageParameters{Percentage = @percentage});
		}

		///<summary>Set preset mode for a fan device.</summary>
		///<param name="target">The target for this service call</param>
		public void SetPresetMode(ServiceTarget target, FanSetPresetModeParameters data)
		{
			_haContext.CallService("fan", "set_preset_mode", target, data);
		}

		///<summary>Set preset mode for a fan device.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="presetMode">New value of preset mode. eg: auto</param>
		public void SetPresetMode(ServiceTarget target, string @presetMode)
		{
			_haContext.CallService("fan", "set_preset_mode", target, new FanSetPresetModeParameters{PresetMode = @presetMode});
		}

		///<summary>Set fan speed.</summary>
		///<param name="target">The target for this service call</param>
		public void SetSpeed(ServiceTarget target, FanSetSpeedParameters data)
		{
			_haContext.CallService("fan", "set_speed", target, data);
		}

		///<summary>Set fan speed.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="speed">Speed setting. eg: low</param>
		public void SetSpeed(ServiceTarget target, string @speed)
		{
			_haContext.CallService("fan", "set_speed", target, new FanSetSpeedParameters{Speed = @speed});
		}

		///<summary>Toggle the fan on/off.</summary>
		///<param name="target">The target for this service call</param>
		public void Toggle(ServiceTarget target)
		{
			_haContext.CallService("fan", "toggle", target);
		}

		///<summary>Turn fan off.</summary>
		///<param name="target">The target for this service call</param>
		public void TurnOff(ServiceTarget target)
		{
			_haContext.CallService("fan", "turn_off", target);
		}

		///<summary>Turn fan on.</summary>
		///<param name="target">The target for this service call</param>
		public void TurnOn(ServiceTarget target, FanTurnOnParameters data)
		{
			_haContext.CallService("fan", "turn_on", target, data);
		}

		///<summary>Turn fan on.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="speed">Speed setting. eg: high</param>
		///<param name="percentage">Percentage speed setting.</param>
		///<param name="presetMode">Preset mode setting. eg: auto</param>
		public void TurnOn(ServiceTarget target, string? @speed = null, long? @percentage = null, string? @presetMode = null)
		{
			_haContext.CallService("fan", "turn_on", target, new FanTurnOnParameters{Speed = @speed, Percentage = @percentage, PresetMode = @presetMode});
		}
	}

	public record FanDecreaseSpeedParameters
	{
		///<summary>Decrease speed by a percentage.</summary>
		[JsonPropertyName("percentage_step")]
		public long? PercentageStep { get; init; }
	}

	public record FanIncreaseSpeedParameters
	{
		///<summary>Increase speed by a percentage.</summary>
		[JsonPropertyName("percentage_step")]
		public long? PercentageStep { get; init; }
	}

	public record FanOscillateParameters
	{
		///<summary>Flag to turn on/off oscillation.</summary>
		[JsonPropertyName("oscillating")]
		public bool? Oscillating { get; init; }
	}

	public record FanSetDirectionParameters
	{
		///<summary>The direction to rotate.</summary>
		[JsonPropertyName("direction")]
		public string? Direction { get; init; }
	}

	public record FanSetPercentageParameters
	{
		///<summary>Percentage speed setting.</summary>
		[JsonPropertyName("percentage")]
		public long? Percentage { get; init; }
	}

	public record FanSetPresetModeParameters
	{
		///<summary>New value of preset mode. eg: auto</summary>
		[JsonPropertyName("preset_mode")]
		public string? PresetMode { get; init; }
	}

	public record FanSetSpeedParameters
	{
		///<summary>Speed setting. eg: low</summary>
		[JsonPropertyName("speed")]
		public string? Speed { get; init; }
	}

	public record FanTurnOnParameters
	{
		///<summary>Speed setting. eg: high</summary>
		[JsonPropertyName("speed")]
		public string? Speed { get; init; }

		///<summary>Percentage speed setting.</summary>
		[JsonPropertyName("percentage")]
		public long? Percentage { get; init; }

		///<summary>Preset mode setting. eg: auto</summary>
		[JsonPropertyName("preset_mode")]
		public string? PresetMode { get; init; }
	}

	public class FfmpegServices
	{
		private readonly IHaContext _haContext;
		public FfmpegServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Send a restart command to a ffmpeg based sensor.</summary>
		public void Restart(FfmpegRestartParameters data)
		{
			_haContext.CallService("ffmpeg", "restart", null, data);
		}

		///<summary>Send a restart command to a ffmpeg based sensor.</summary>
		///<param name="entityId">Name of entity that will restart. Platform dependent.</param>
		public void Restart(string? @entityId = null)
		{
			_haContext.CallService("ffmpeg", "restart", null, new FfmpegRestartParameters{EntityId = @entityId});
		}

		///<summary>Send a start command to a ffmpeg based sensor.</summary>
		public void Start(FfmpegStartParameters data)
		{
			_haContext.CallService("ffmpeg", "start", null, data);
		}

		///<summary>Send a start command to a ffmpeg based sensor.</summary>
		///<param name="entityId">Name of entity that will start. Platform dependent.</param>
		public void Start(string? @entityId = null)
		{
			_haContext.CallService("ffmpeg", "start", null, new FfmpegStartParameters{EntityId = @entityId});
		}

		///<summary>Send a stop command to a ffmpeg based sensor.</summary>
		public void Stop(FfmpegStopParameters data)
		{
			_haContext.CallService("ffmpeg", "stop", null, data);
		}

		///<summary>Send a stop command to a ffmpeg based sensor.</summary>
		///<param name="entityId">Name of entity that will stop. Platform dependent.</param>
		public void Stop(string? @entityId = null)
		{
			_haContext.CallService("ffmpeg", "stop", null, new FfmpegStopParameters{EntityId = @entityId});
		}
	}

	public record FfmpegRestartParameters
	{
		///<summary>Name of entity that will restart. Platform dependent.</summary>
		[JsonPropertyName("entity_id")]
		public string? EntityId { get; init; }
	}

	public record FfmpegStartParameters
	{
		///<summary>Name of entity that will start. Platform dependent.</summary>
		[JsonPropertyName("entity_id")]
		public string? EntityId { get; init; }
	}

	public record FfmpegStopParameters
	{
		///<summary>Name of entity that will stop. Platform dependent.</summary>
		[JsonPropertyName("entity_id")]
		public string? EntityId { get; init; }
	}

	public class FrontendServices
	{
		private readonly IHaContext _haContext;
		public FrontendServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Reload themes from YAML configuration.</summary>
		public void ReloadThemes()
		{
			_haContext.CallService("frontend", "reload_themes", null);
		}

		///<summary>Set a theme unless the client selected per-device theme.</summary>
		public void SetTheme(FrontendSetThemeParameters data)
		{
			_haContext.CallService("frontend", "set_theme", null, data);
		}

		///<summary>Set a theme unless the client selected per-device theme.</summary>
		///<param name="name">Name of a predefined theme, 'default' or 'none'. eg: default</param>
		///<param name="mode">The mode the theme is for.</param>
		public void SetTheme(string @name, string? @mode = null)
		{
			_haContext.CallService("frontend", "set_theme", null, new FrontendSetThemeParameters{Name = @name, Mode = @mode});
		}
	}

	public record FrontendSetThemeParameters
	{
		///<summary>Name of a predefined theme, 'default' or 'none'. eg: default</summary>
		[JsonPropertyName("name")]
		public string? Name { get; init; }

		///<summary>The mode the theme is for.</summary>
		[JsonPropertyName("mode")]
		public string? Mode { get; init; }
	}

	public class GenericServices
	{
		private readonly IHaContext _haContext;
		public GenericServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Reload all generic entities.</summary>
		public void Reload()
		{
			_haContext.CallService("generic", "reload", null);
		}
	}

	public class GroupServices
	{
		private readonly IHaContext _haContext;
		public GroupServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Reload group configuration, entities, and notify services.</summary>
		public void Reload()
		{
			_haContext.CallService("group", "reload", null);
		}

		///<summary>Remove a user group.</summary>
		public void Remove(GroupRemoveParameters data)
		{
			_haContext.CallService("group", "remove", null, data);
		}

		///<summary>Remove a user group.</summary>
		///<param name="objectId">Group id and part of entity id. eg: test_group</param>
		public void Remove(object @objectId)
		{
			_haContext.CallService("group", "remove", null, new GroupRemoveParameters{ObjectId = @objectId});
		}

		///<summary>Create/Update a user group.</summary>
		public void Set(GroupSetParameters data)
		{
			_haContext.CallService("group", "set", null, data);
		}

		///<summary>Create/Update a user group.</summary>
		///<param name="objectId">Group id and part of entity id. eg: test_group</param>
		///<param name="name">Name of group eg: My test group</param>
		///<param name="icon">Name of icon for the group. eg: mdi:camera</param>
		///<param name="entities">List of all members in the group. Not compatible with 'delta'. eg: domain.entity_id1, domain.entity_id2</param>
		///<param name="addEntities">List of members that will change on group listening. eg: domain.entity_id1, domain.entity_id2</param>
		///<param name="all">Enable this option if the group should only turn on when all entities are on.</param>
		public void Set(string @objectId, string? @name = null, string? @icon = null, object? @entities = null, object? @addEntities = null, bool? @all = null)
		{
			_haContext.CallService("group", "set", null, new GroupSetParameters{ObjectId = @objectId, Name = @name, Icon = @icon, Entities = @entities, AddEntities = @addEntities, All = @all});
		}
	}

	public record GroupRemoveParameters
	{
		///<summary>Group id and part of entity id. eg: test_group</summary>
		[JsonPropertyName("object_id")]
		public object? ObjectId { get; init; }
	}

	public record GroupSetParameters
	{
		///<summary>Group id and part of entity id. eg: test_group</summary>
		[JsonPropertyName("object_id")]
		public string? ObjectId { get; init; }

		///<summary>Name of group eg: My test group</summary>
		[JsonPropertyName("name")]
		public string? Name { get; init; }

		///<summary>Name of icon for the group. eg: mdi:camera</summary>
		[JsonPropertyName("icon")]
		public string? Icon { get; init; }

		///<summary>List of all members in the group. Not compatible with 'delta'. eg: domain.entity_id1, domain.entity_id2</summary>
		[JsonPropertyName("entities")]
		public object? Entities { get; init; }

		///<summary>List of members that will change on group listening. eg: domain.entity_id1, domain.entity_id2</summary>
		[JsonPropertyName("add_entities")]
		public object? AddEntities { get; init; }

		///<summary>Enable this option if the group should only turn on when all entities are on.</summary>
		[JsonPropertyName("all")]
		public bool? All { get; init; }
	}

	public class HistoryStatsServices
	{
		private readonly IHaContext _haContext;
		public HistoryStatsServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Reload all history_stats entities.</summary>
		public void Reload()
		{
			_haContext.CallService("history_stats", "reload", null);
		}
	}

	public class HomeassistantServices
	{
		private readonly IHaContext _haContext;
		public HomeassistantServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Check the Home Assistant configuration files for errors. Errors will be displayed in the Home Assistant log.</summary>
		public void CheckConfig()
		{
			_haContext.CallService("homeassistant", "check_config", null);
		}

		///<summary>Reload a config entry that matches a target.</summary>
		///<param name="target">The target for this service call</param>
		public void ReloadConfigEntry(ServiceTarget target, HomeassistantReloadConfigEntryParameters data)
		{
			_haContext.CallService("homeassistant", "reload_config_entry", target, data);
		}

		///<summary>Reload a config entry that matches a target.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="entryId">A configuration entry id eg: 8955375327824e14ba89e4b29cc3ec9a</param>
		public void ReloadConfigEntry(ServiceTarget target, string? @entryId = null)
		{
			_haContext.CallService("homeassistant", "reload_config_entry", target, new HomeassistantReloadConfigEntryParameters{EntryId = @entryId});
		}

		///<summary>Reload the core configuration.</summary>
		public void ReloadCoreConfig()
		{
			_haContext.CallService("homeassistant", "reload_core_config", null);
		}

		///<summary>Restart the Home Assistant service.</summary>
		public void Restart()
		{
			_haContext.CallService("homeassistant", "restart", null);
		}

		///<summary>Save the persistent states (for entities derived from RestoreEntity) immediately. Maintain the normal periodic saving interval.</summary>
		public void SavePersistentStates()
		{
			_haContext.CallService("homeassistant", "save_persistent_states", null);
		}

		///<summary>Update the Home Assistant location.</summary>
		public void SetLocation(HomeassistantSetLocationParameters data)
		{
			_haContext.CallService("homeassistant", "set_location", null, data);
		}

		///<summary>Update the Home Assistant location.</summary>
		///<param name="latitude">Latitude of your location. eg: 32.87336</param>
		///<param name="longitude">Longitude of your location. eg: 117.22743</param>
		public void SetLocation(string @latitude, string @longitude)
		{
			_haContext.CallService("homeassistant", "set_location", null, new HomeassistantSetLocationParameters{Latitude = @latitude, Longitude = @longitude});
		}

		///<summary>Stop the Home Assistant service.</summary>
		public void Stop()
		{
			_haContext.CallService("homeassistant", "stop", null);
		}

		///<summary>Generic service to toggle devices on/off under any domain</summary>
		///<param name="target">The target for this service call</param>
		public void Toggle(ServiceTarget target)
		{
			_haContext.CallService("homeassistant", "toggle", target);
		}

		///<summary>Generic service to turn devices off under any domain.</summary>
		///<param name="target">The target for this service call</param>
		public void TurnOff(ServiceTarget target)
		{
			_haContext.CallService("homeassistant", "turn_off", target);
		}

		///<summary>Generic service to turn devices on under any domain.</summary>
		///<param name="target">The target for this service call</param>
		public void TurnOn(ServiceTarget target)
		{
			_haContext.CallService("homeassistant", "turn_on", target);
		}

		///<summary>Force one or more entities to update its data</summary>
		///<param name="target">The target for this service call</param>
		public void UpdateEntity(ServiceTarget target)
		{
			_haContext.CallService("homeassistant", "update_entity", target);
		}
	}

	public record HomeassistantReloadConfigEntryParameters
	{
		///<summary>A configuration entry id eg: 8955375327824e14ba89e4b29cc3ec9a</summary>
		[JsonPropertyName("entry_id")]
		public string? EntryId { get; init; }
	}

	public record HomeassistantSetLocationParameters
	{
		///<summary>Latitude of your location. eg: 32.87336</summary>
		[JsonPropertyName("latitude")]
		public string? Latitude { get; init; }

		///<summary>Longitude of your location. eg: 117.22743</summary>
		[JsonPropertyName("longitude")]
		public string? Longitude { get; init; }
	}

	public class HumidifierServices
	{
		private readonly IHaContext _haContext;
		public HumidifierServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Set target humidity of humidifier device.</summary>
		///<param name="target">The target for this service call</param>
		public void SetHumidity(ServiceTarget target, HumidifierSetHumidityParameters data)
		{
			_haContext.CallService("humidifier", "set_humidity", target, data);
		}

		///<summary>Set target humidity of humidifier device.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="humidity">New target humidity for humidifier device.</param>
		public void SetHumidity(ServiceTarget target, long @humidity)
		{
			_haContext.CallService("humidifier", "set_humidity", target, new HumidifierSetHumidityParameters{Humidity = @humidity});
		}

		///<summary>Set mode for humidifier device.</summary>
		///<param name="target">The target for this service call</param>
		public void SetMode(ServiceTarget target, HumidifierSetModeParameters data)
		{
			_haContext.CallService("humidifier", "set_mode", target, data);
		}

		///<summary>Set mode for humidifier device.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="mode">New mode eg: away</param>
		public void SetMode(ServiceTarget target, string @mode)
		{
			_haContext.CallService("humidifier", "set_mode", target, new HumidifierSetModeParameters{Mode = @mode});
		}

		///<summary>Toggles a humidifier device.</summary>
		///<param name="target">The target for this service call</param>
		public void Toggle(ServiceTarget target)
		{
			_haContext.CallService("humidifier", "toggle", target);
		}

		///<summary>Turn humidifier device off.</summary>
		///<param name="target">The target for this service call</param>
		public void TurnOff(ServiceTarget target)
		{
			_haContext.CallService("humidifier", "turn_off", target);
		}

		///<summary>Turn humidifier device on.</summary>
		///<param name="target">The target for this service call</param>
		public void TurnOn(ServiceTarget target)
		{
			_haContext.CallService("humidifier", "turn_on", target);
		}
	}

	public record HumidifierSetHumidityParameters
	{
		///<summary>New target humidity for humidifier device.</summary>
		[JsonPropertyName("humidity")]
		public long? Humidity { get; init; }
	}

	public record HumidifierSetModeParameters
	{
		///<summary>New mode eg: away</summary>
		[JsonPropertyName("mode")]
		public string? Mode { get; init; }
	}

	public class InputBooleanServices
	{
		private readonly IHaContext _haContext;
		public InputBooleanServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Reload the input_boolean configuration</summary>
		public void Reload()
		{
			_haContext.CallService("input_boolean", "reload", null);
		}

		///<summary>Toggle an input boolean</summary>
		///<param name="target">The target for this service call</param>
		public void Toggle(ServiceTarget target)
		{
			_haContext.CallService("input_boolean", "toggle", target);
		}

		///<summary>Turn off an input boolean</summary>
		///<param name="target">The target for this service call</param>
		public void TurnOff(ServiceTarget target)
		{
			_haContext.CallService("input_boolean", "turn_off", target);
		}

		///<summary>Turn on an input boolean</summary>
		///<param name="target">The target for this service call</param>
		public void TurnOn(ServiceTarget target)
		{
			_haContext.CallService("input_boolean", "turn_on", target);
		}
	}

	public class InputButtonServices
	{
		private readonly IHaContext _haContext;
		public InputButtonServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Press the input button entity.</summary>
		///<param name="target">The target for this service call</param>
		public void Press(ServiceTarget target)
		{
			_haContext.CallService("input_button", "press", target);
		}

		public void Reload()
		{
			_haContext.CallService("input_button", "reload", null);
		}
	}

	public class InputDatetimeServices
	{
		private readonly IHaContext _haContext;
		public InputDatetimeServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Reload the input_datetime configuration.</summary>
		public void Reload()
		{
			_haContext.CallService("input_datetime", "reload", null);
		}

		///<summary>This can be used to dynamically set the date and/or time.</summary>
		///<param name="target">The target for this service call</param>
		public void SetDatetime(ServiceTarget target, InputDatetimeSetDatetimeParameters data)
		{
			_haContext.CallService("input_datetime", "set_datetime", target, data);
		}

		///<summary>This can be used to dynamically set the date and/or time.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="date">The target date the entity should be set to. eg: "2019-04-20"</param>
		///<param name="time">The target time the entity should be set to. eg: "05:04:20"</param>
		///<param name="datetime">The target date & time the entity should be set to. eg: "2019-04-20 05:04:20"</param>
		///<param name="timestamp">The target date & time the entity should be set to as expressed by a UNIX timestamp.</param>
		public void SetDatetime(ServiceTarget target, string? @date = null, DateTime? @time = null, string? @datetime = null, long? @timestamp = null)
		{
			_haContext.CallService("input_datetime", "set_datetime", target, new InputDatetimeSetDatetimeParameters{Date = @date, Time = @time, Datetime = @datetime, Timestamp = @timestamp});
		}
	}

	public record InputDatetimeSetDatetimeParameters
	{
		///<summary>The target date the entity should be set to. eg: "2019-04-20"</summary>
		[JsonPropertyName("date")]
		public string? Date { get; init; }

		///<summary>The target time the entity should be set to. eg: "05:04:20"</summary>
		[JsonPropertyName("time")]
		public DateTime? Time { get; init; }

		///<summary>The target date & time the entity should be set to. eg: "2019-04-20 05:04:20"</summary>
		[JsonPropertyName("datetime")]
		public string? Datetime { get; init; }

		///<summary>The target date & time the entity should be set to as expressed by a UNIX timestamp.</summary>
		[JsonPropertyName("timestamp")]
		public long? Timestamp { get; init; }
	}

	public class InputNumberServices
	{
		private readonly IHaContext _haContext;
		public InputNumberServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Decrement the value of an input number entity by its stepping.</summary>
		///<param name="target">The target for this service call</param>
		public void Decrement(ServiceTarget target)
		{
			_haContext.CallService("input_number", "decrement", target);
		}

		///<summary>Increment the value of an input number entity by its stepping.</summary>
		///<param name="target">The target for this service call</param>
		public void Increment(ServiceTarget target)
		{
			_haContext.CallService("input_number", "increment", target);
		}

		///<summary>Reload the input_number configuration.</summary>
		public void Reload()
		{
			_haContext.CallService("input_number", "reload", null);
		}

		///<summary>Set the value of an input number entity.</summary>
		///<param name="target">The target for this service call</param>
		public void SetValue(ServiceTarget target, InputNumberSetValueParameters data)
		{
			_haContext.CallService("input_number", "set_value", target, data);
		}

		///<summary>Set the value of an input number entity.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="value">The target value the entity should be set to.</param>
		public void SetValue(ServiceTarget target, double @value)
		{
			_haContext.CallService("input_number", "set_value", target, new InputNumberSetValueParameters{Value = @value});
		}
	}

	public record InputNumberSetValueParameters
	{
		///<summary>The target value the entity should be set to.</summary>
		[JsonPropertyName("value")]
		public double? Value { get; init; }
	}

	public class InputSelectServices
	{
		private readonly IHaContext _haContext;
		public InputSelectServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Reload the input_select configuration.</summary>
		public void Reload()
		{
			_haContext.CallService("input_select", "reload", null);
		}

		///<summary>Select the first option of an input select entity.</summary>
		///<param name="target">The target for this service call</param>
		public void SelectFirst(ServiceTarget target)
		{
			_haContext.CallService("input_select", "select_first", target);
		}

		///<summary>Select the last option of an input select entity.</summary>
		///<param name="target">The target for this service call</param>
		public void SelectLast(ServiceTarget target)
		{
			_haContext.CallService("input_select", "select_last", target);
		}

		///<summary>Select the next options of an input select entity.</summary>
		///<param name="target">The target for this service call</param>
		public void SelectNext(ServiceTarget target, InputSelectSelectNextParameters data)
		{
			_haContext.CallService("input_select", "select_next", target, data);
		}

		///<summary>Select the next options of an input select entity.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="cycle">If the option should cycle from the last to the first.</param>
		public void SelectNext(ServiceTarget target, bool? @cycle = null)
		{
			_haContext.CallService("input_select", "select_next", target, new InputSelectSelectNextParameters{Cycle = @cycle});
		}

		///<summary>Select an option of an input select entity.</summary>
		///<param name="target">The target for this service call</param>
		public void SelectOption(ServiceTarget target, InputSelectSelectOptionParameters data)
		{
			_haContext.CallService("input_select", "select_option", target, data);
		}

		///<summary>Select an option of an input select entity.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="option">Option to be selected. eg: "Item A"</param>
		public void SelectOption(ServiceTarget target, string @option)
		{
			_haContext.CallService("input_select", "select_option", target, new InputSelectSelectOptionParameters{Option = @option});
		}

		///<summary>Select the previous options of an input select entity.</summary>
		///<param name="target">The target for this service call</param>
		public void SelectPrevious(ServiceTarget target, InputSelectSelectPreviousParameters data)
		{
			_haContext.CallService("input_select", "select_previous", target, data);
		}

		///<summary>Select the previous options of an input select entity.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="cycle">If the option should cycle from the first to the last.</param>
		public void SelectPrevious(ServiceTarget target, bool? @cycle = null)
		{
			_haContext.CallService("input_select", "select_previous", target, new InputSelectSelectPreviousParameters{Cycle = @cycle});
		}

		///<summary>Set the options of an input select entity.</summary>
		///<param name="target">The target for this service call</param>
		public void SetOptions(ServiceTarget target, InputSelectSetOptionsParameters data)
		{
			_haContext.CallService("input_select", "set_options", target, data);
		}

		///<summary>Set the options of an input select entity.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="options">Options for the input select entity. eg: ["Item A", "Item B", "Item C"]</param>
		public void SetOptions(ServiceTarget target, object @options)
		{
			_haContext.CallService("input_select", "set_options", target, new InputSelectSetOptionsParameters{Options = @options});
		}
	}

	public record InputSelectSelectNextParameters
	{
		///<summary>If the option should cycle from the last to the first.</summary>
		[JsonPropertyName("cycle")]
		public bool? Cycle { get; init; }
	}

	public record InputSelectSelectOptionParameters
	{
		///<summary>Option to be selected. eg: "Item A"</summary>
		[JsonPropertyName("option")]
		public string? Option { get; init; }
	}

	public record InputSelectSelectPreviousParameters
	{
		///<summary>If the option should cycle from the first to the last.</summary>
		[JsonPropertyName("cycle")]
		public bool? Cycle { get; init; }
	}

	public record InputSelectSetOptionsParameters
	{
		///<summary>Options for the input select entity. eg: ["Item A", "Item B", "Item C"]</summary>
		[JsonPropertyName("options")]
		public object? Options { get; init; }
	}

	public class InputTextServices
	{
		private readonly IHaContext _haContext;
		public InputTextServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Reload the input_text configuration.</summary>
		public void Reload()
		{
			_haContext.CallService("input_text", "reload", null);
		}

		///<summary>Set the value of an input text entity.</summary>
		///<param name="target">The target for this service call</param>
		public void SetValue(ServiceTarget target, InputTextSetValueParameters data)
		{
			_haContext.CallService("input_text", "set_value", target, data);
		}

		///<summary>Set the value of an input text entity.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="value">The target value the entity should be set to. eg: This is an example text</param>
		public void SetValue(ServiceTarget target, string @value)
		{
			_haContext.CallService("input_text", "set_value", target, new InputTextSetValueParameters{Value = @value});
		}
	}

	public record InputTextSetValueParameters
	{
		///<summary>The target value the entity should be set to. eg: This is an example text</summary>
		[JsonPropertyName("value")]
		public string? Value { get; init; }
	}

	public class LightServices
	{
		private readonly IHaContext _haContext;
		public LightServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Toggles one or more lights, from on to off, or, off to on, based on their current state. </summary>
		///<param name="target">The target for this service call</param>
		public void Toggle(ServiceTarget target, LightToggleParameters data)
		{
			_haContext.CallService("light", "toggle", target, data);
		}

		///<summary>Toggles one or more lights, from on to off, or, off to on, based on their current state. </summary>
		///<param name="target">The target for this service call</param>
		///<param name="transition">Duration it takes to get to next state.</param>
		///<param name="rgbColor">Color for the light in RGB-format. eg: [255, 100, 100]</param>
		///<param name="colorName">A human readable color name.</param>
		///<param name="hsColor">Color for the light in hue/sat format. Hue is 0-360 and Sat is 0-100. eg: [300, 70]</param>
		///<param name="xyColor">Color for the light in XY-format. eg: [0.52, 0.43]</param>
		///<param name="colorTemp">Color temperature for the light in mireds.</param>
		///<param name="kelvin">Color temperature for the light in Kelvin.</param>
		///<param name="whiteValue">Number indicating level of white.</param>
		///<param name="brightness">Number indicating brightness, where 0 turns the light off, 1 is the minimum brightness and 255 is the maximum brightness supported by the light.</param>
		///<param name="brightnessPct">Number indicating percentage of full brightness, where 0 turns the light off, 1 is the minimum brightness and 100 is the maximum brightness supported by the light.</param>
		///<param name="profile">Name of a light profile to use. eg: relax</param>
		///<param name="flash">If the light should flash.</param>
		///<param name="effect">Light effect.</param>
		public void Toggle(ServiceTarget target, long? @transition = null, object? @rgbColor = null, string? @colorName = null, object? @hsColor = null, object? @xyColor = null, long? @colorTemp = null, long? @kelvin = null, long? @whiteValue = null, long? @brightness = null, long? @brightnessPct = null, string? @profile = null, string? @flash = null, string? @effect = null)
		{
			_haContext.CallService("light", "toggle", target, new LightToggleParameters{Transition = @transition, RgbColor = @rgbColor, ColorName = @colorName, HsColor = @hsColor, XyColor = @xyColor, ColorTemp = @colorTemp, Kelvin = @kelvin, WhiteValue = @whiteValue, Brightness = @brightness, BrightnessPct = @brightnessPct, Profile = @profile, Flash = @flash, Effect = @effect});
		}

		///<summary>Turns off one or more lights.</summary>
		///<param name="target">The target for this service call</param>
		public void TurnOff(ServiceTarget target, LightTurnOffParameters data)
		{
			_haContext.CallService("light", "turn_off", target, data);
		}

		///<summary>Turns off one or more lights.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="transition">Duration it takes to get to next state.</param>
		///<param name="flash">If the light should flash.</param>
		public void TurnOff(ServiceTarget target, long? @transition = null, string? @flash = null)
		{
			_haContext.CallService("light", "turn_off", target, new LightTurnOffParameters{Transition = @transition, Flash = @flash});
		}

		///<summary>Turn on one or more lights and adjust properties of the light, even when they are turned on already. </summary>
		///<param name="target">The target for this service call</param>
		public void TurnOn(ServiceTarget target, LightTurnOnParameters data)
		{
			_haContext.CallService("light", "turn_on", target, data);
		}

		///<summary>Turn on one or more lights and adjust properties of the light, even when they are turned on already. </summary>
		///<param name="target">The target for this service call</param>
		///<param name="transition">Duration it takes to get to next state.</param>
		///<param name="rgbColor">A list containing three integers between 0 and 255 representing the RGB (red, green, blue) color for the light. eg: [255, 100, 100]</param>
		///<param name="rgbwColor">A list containing four integers between 0 and 255 representing the RGBW (red, green, blue, white) color for the light. eg: [255, 100, 100, 50]</param>
		///<param name="rgbwwColor">A list containing five integers between 0 and 255 representing the RGBWW (red, green, blue, cold white, warm white) color for the light. eg: [255, 100, 100, 50, 70]</param>
		///<param name="colorName">A human readable color name.</param>
		///<param name="hsColor">Color for the light in hue/sat format. Hue is 0-360 and Sat is 0-100. eg: [300, 70]</param>
		///<param name="xyColor">Color for the light in XY-format. eg: [0.52, 0.43]</param>
		///<param name="colorTemp">Color temperature for the light in mireds.</param>
		///<param name="kelvin">Color temperature for the light in Kelvin.</param>
		///<param name="brightness">Number indicating brightness, where 0 turns the light off, 1 is the minimum brightness and 255 is the maximum brightness supported by the light.</param>
		///<param name="brightnessPct">Number indicating percentage of full brightness, where 0 turns the light off, 1 is the minimum brightness and 100 is the maximum brightness supported by the light.</param>
		///<param name="brightnessStep">Change brightness by an amount.</param>
		///<param name="brightnessStepPct">Change brightness by a percentage.</param>
		///<param name="white">Set the light to white mode and change its brightness, where 0 turns the light off, 1 is the minimum brightness and 255 is the maximum brightness supported by the light.</param>
		///<param name="profile">Name of a light profile to use. eg: relax</param>
		///<param name="flash">If the light should flash.</param>
		///<param name="effect">Light effect.</param>
		public void TurnOn(ServiceTarget target, long? @transition = null, object? @rgbColor = null, object? @rgbwColor = null, object? @rgbwwColor = null, string? @colorName = null, object? @hsColor = null, object? @xyColor = null, long? @colorTemp = null, long? @kelvin = null, long? @brightness = null, long? @brightnessPct = null, long? @brightnessStep = null, long? @brightnessStepPct = null, long? @white = null, string? @profile = null, string? @flash = null, string? @effect = null)
		{
			_haContext.CallService("light", "turn_on", target, new LightTurnOnParameters{Transition = @transition, RgbColor = @rgbColor, RgbwColor = @rgbwColor, RgbwwColor = @rgbwwColor, ColorName = @colorName, HsColor = @hsColor, XyColor = @xyColor, ColorTemp = @colorTemp, Kelvin = @kelvin, Brightness = @brightness, BrightnessPct = @brightnessPct, BrightnessStep = @brightnessStep, BrightnessStepPct = @brightnessStepPct, White = @white, Profile = @profile, Flash = @flash, Effect = @effect});
		}
	}

	public record LightToggleParameters
	{
		///<summary>Duration it takes to get to next state.</summary>
		[JsonPropertyName("transition")]
		public long? Transition { get; init; }

		///<summary>Color for the light in RGB-format. eg: [255, 100, 100]</summary>
		[JsonPropertyName("rgb_color")]
		public object? RgbColor { get; init; }

		///<summary>A human readable color name.</summary>
		[JsonPropertyName("color_name")]
		public string? ColorName { get; init; }

		///<summary>Color for the light in hue/sat format. Hue is 0-360 and Sat is 0-100. eg: [300, 70]</summary>
		[JsonPropertyName("hs_color")]
		public object? HsColor { get; init; }

		///<summary>Color for the light in XY-format. eg: [0.52, 0.43]</summary>
		[JsonPropertyName("xy_color")]
		public object? XyColor { get; init; }

		///<summary>Color temperature for the light in mireds.</summary>
		[JsonPropertyName("color_temp")]
		public long? ColorTemp { get; init; }

		///<summary>Color temperature for the light in Kelvin.</summary>
		[JsonPropertyName("kelvin")]
		public long? Kelvin { get; init; }

		///<summary>Number indicating level of white.</summary>
		[JsonPropertyName("white_value")]
		public long? WhiteValue { get; init; }

		///<summary>Number indicating brightness, where 0 turns the light off, 1 is the minimum brightness and 255 is the maximum brightness supported by the light.</summary>
		[JsonPropertyName("brightness")]
		public long? Brightness { get; init; }

		///<summary>Number indicating percentage of full brightness, where 0 turns the light off, 1 is the minimum brightness and 100 is the maximum brightness supported by the light.</summary>
		[JsonPropertyName("brightness_pct")]
		public long? BrightnessPct { get; init; }

		///<summary>Name of a light profile to use. eg: relax</summary>
		[JsonPropertyName("profile")]
		public string? Profile { get; init; }

		///<summary>If the light should flash.</summary>
		[JsonPropertyName("flash")]
		public string? Flash { get; init; }

		///<summary>Light effect.</summary>
		[JsonPropertyName("effect")]
		public string? Effect { get; init; }
	}

	public record LightTurnOffParameters
	{
		///<summary>Duration it takes to get to next state.</summary>
		[JsonPropertyName("transition")]
		public long? Transition { get; init; }

		///<summary>If the light should flash.</summary>
		[JsonPropertyName("flash")]
		public string? Flash { get; init; }
	}

	public record LightTurnOnParameters
	{
		///<summary>Duration it takes to get to next state.</summary>
		[JsonPropertyName("transition")]
		public long? Transition { get; init; }

		///<summary>A list containing three integers between 0 and 255 representing the RGB (red, green, blue) color for the light. eg: [255, 100, 100]</summary>
		[JsonPropertyName("rgb_color")]
		public object? RgbColor { get; init; }

		///<summary>A list containing four integers between 0 and 255 representing the RGBW (red, green, blue, white) color for the light. eg: [255, 100, 100, 50]</summary>
		[JsonPropertyName("rgbw_color")]
		public object? RgbwColor { get; init; }

		///<summary>A list containing five integers between 0 and 255 representing the RGBWW (red, green, blue, cold white, warm white) color for the light. eg: [255, 100, 100, 50, 70]</summary>
		[JsonPropertyName("rgbww_color")]
		public object? RgbwwColor { get; init; }

		///<summary>A human readable color name.</summary>
		[JsonPropertyName("color_name")]
		public string? ColorName { get; init; }

		///<summary>Color for the light in hue/sat format. Hue is 0-360 and Sat is 0-100. eg: [300, 70]</summary>
		[JsonPropertyName("hs_color")]
		public object? HsColor { get; init; }

		///<summary>Color for the light in XY-format. eg: [0.52, 0.43]</summary>
		[JsonPropertyName("xy_color")]
		public object? XyColor { get; init; }

		///<summary>Color temperature for the light in mireds.</summary>
		[JsonPropertyName("color_temp")]
		public long? ColorTemp { get; init; }

		///<summary>Color temperature for the light in Kelvin.</summary>
		[JsonPropertyName("kelvin")]
		public long? Kelvin { get; init; }

		///<summary>Number indicating brightness, where 0 turns the light off, 1 is the minimum brightness and 255 is the maximum brightness supported by the light.</summary>
		[JsonPropertyName("brightness")]
		public long? Brightness { get; init; }

		///<summary>Number indicating percentage of full brightness, where 0 turns the light off, 1 is the minimum brightness and 100 is the maximum brightness supported by the light.</summary>
		[JsonPropertyName("brightness_pct")]
		public long? BrightnessPct { get; init; }

		///<summary>Change brightness by an amount.</summary>
		[JsonPropertyName("brightness_step")]
		public long? BrightnessStep { get; init; }

		///<summary>Change brightness by a percentage.</summary>
		[JsonPropertyName("brightness_step_pct")]
		public long? BrightnessStepPct { get; init; }

		///<summary>Set the light to white mode and change its brightness, where 0 turns the light off, 1 is the minimum brightness and 255 is the maximum brightness supported by the light.</summary>
		[JsonPropertyName("white")]
		public long? White { get; init; }

		///<summary>Name of a light profile to use. eg: relax</summary>
		[JsonPropertyName("profile")]
		public string? Profile { get; init; }

		///<summary>If the light should flash.</summary>
		[JsonPropertyName("flash")]
		public string? Flash { get; init; }

		///<summary>Light effect.</summary>
		[JsonPropertyName("effect")]
		public string? Effect { get; init; }
	}

	public class LockServices
	{
		private readonly IHaContext _haContext;
		public LockServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Lock all or specified locks.</summary>
		///<param name="target">The target for this service call</param>
		public void Lock(ServiceTarget target, LockLockParameters data)
		{
			_haContext.CallService("lock", "lock", target, data);
		}

		///<summary>Lock all or specified locks.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="code">An optional code to lock the lock with. eg: 1234</param>
		public void Lock(ServiceTarget target, string? @code = null)
		{
			_haContext.CallService("lock", "lock", target, new LockLockParameters{Code = @code});
		}

		///<summary>Open all or specified locks.</summary>
		///<param name="target">The target for this service call</param>
		public void Open(ServiceTarget target, LockOpenParameters data)
		{
			_haContext.CallService("lock", "open", target, data);
		}

		///<summary>Open all or specified locks.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="code">An optional code to open the lock with. eg: 1234</param>
		public void Open(ServiceTarget target, string? @code = null)
		{
			_haContext.CallService("lock", "open", target, new LockOpenParameters{Code = @code});
		}

		///<summary>Unlock all or specified locks.</summary>
		///<param name="target">The target for this service call</param>
		public void Unlock(ServiceTarget target, LockUnlockParameters data)
		{
			_haContext.CallService("lock", "unlock", target, data);
		}

		///<summary>Unlock all or specified locks.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="code">An optional code to unlock the lock with. eg: 1234</param>
		public void Unlock(ServiceTarget target, string? @code = null)
		{
			_haContext.CallService("lock", "unlock", target, new LockUnlockParameters{Code = @code});
		}
	}

	public record LockLockParameters
	{
		///<summary>An optional code to lock the lock with. eg: 1234</summary>
		[JsonPropertyName("code")]
		public string? Code { get; init; }
	}

	public record LockOpenParameters
	{
		///<summary>An optional code to open the lock with. eg: 1234</summary>
		[JsonPropertyName("code")]
		public string? Code { get; init; }
	}

	public record LockUnlockParameters
	{
		///<summary>An optional code to unlock the lock with. eg: 1234</summary>
		[JsonPropertyName("code")]
		public string? Code { get; init; }
	}

	public class LogbookServices
	{
		private readonly IHaContext _haContext;
		public LogbookServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Create a custom entry in your logbook.</summary>
		public void Log(LogbookLogParameters data)
		{
			_haContext.CallService("logbook", "log", null, data);
		}

		///<summary>Create a custom entry in your logbook.</summary>
		///<param name="name">Custom name for an entity, can be referenced with entity_id. eg: Kitchen</param>
		///<param name="message">Message of the custom logbook entry. eg: is being used</param>
		///<param name="entityId">Entity to reference in custom logbook entry.</param>
		///<param name="domain">Icon of domain to display in custom logbook entry. eg: light</param>
		public void Log(string @name, string @message, string? @entityId = null, string? @domain = null)
		{
			_haContext.CallService("logbook", "log", null, new LogbookLogParameters{Name = @name, Message = @message, EntityId = @entityId, Domain = @domain});
		}
	}

	public record LogbookLogParameters
	{
		///<summary>Custom name for an entity, can be referenced with entity_id. eg: Kitchen</summary>
		[JsonPropertyName("name")]
		public string? Name { get; init; }

		///<summary>Message of the custom logbook entry. eg: is being used</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Entity to reference in custom logbook entry.</summary>
		[JsonPropertyName("entity_id")]
		public string? EntityId { get; init; }

		///<summary>Icon of domain to display in custom logbook entry. eg: light</summary>
		[JsonPropertyName("domain")]
		public string? Domain { get; init; }
	}

	public class LoggerServices
	{
		private readonly IHaContext _haContext;
		public LoggerServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Set the default log level for integrations.</summary>
		public void SetDefaultLevel(LoggerSetDefaultLevelParameters data)
		{
			_haContext.CallService("logger", "set_default_level", null, data);
		}

		///<summary>Set the default log level for integrations.</summary>
		///<param name="level">Default severity level for all integrations.</param>
		public void SetDefaultLevel(string? @level = null)
		{
			_haContext.CallService("logger", "set_default_level", null, new LoggerSetDefaultLevelParameters{Level = @level});
		}

		///<summary>Set log level for integrations.</summary>
		public void SetLevel()
		{
			_haContext.CallService("logger", "set_level", null);
		}
	}

	public record LoggerSetDefaultLevelParameters
	{
		///<summary>Default severity level for all integrations.</summary>
		[JsonPropertyName("level")]
		public string? Level { get; init; }
	}

	public class MediaPlayerServices
	{
		private readonly IHaContext _haContext;
		public MediaPlayerServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Send the media player the command to clear players playlist.</summary>
		///<param name="target">The target for this service call</param>
		public void ClearPlaylist(ServiceTarget target)
		{
			_haContext.CallService("media_player", "clear_playlist", target);
		}

		///<summary>Group players together. Only works on platforms with support for player groups.</summary>
		///<param name="target">The target for this service call</param>
		public void Join(ServiceTarget target, MediaPlayerJoinParameters data)
		{
			_haContext.CallService("media_player", "join", target, data);
		}

		///<summary>Group players together. Only works on platforms with support for player groups.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="groupMembers">The players which will be synced with the target player. eg: ["media_player.multiroom_player2", "media_player.multiroom_player3"]</param>
		public void Join(ServiceTarget target, object? @groupMembers = null)
		{
			_haContext.CallService("media_player", "join", target, new MediaPlayerJoinParameters{GroupMembers = @groupMembers});
		}

		///<summary>Send the media player the command for next track.</summary>
		///<param name="target">The target for this service call</param>
		public void MediaNextTrack(ServiceTarget target)
		{
			_haContext.CallService("media_player", "media_next_track", target);
		}

		///<summary>Send the media player the command for pause.</summary>
		///<param name="target">The target for this service call</param>
		public void MediaPause(ServiceTarget target)
		{
			_haContext.CallService("media_player", "media_pause", target);
		}

		///<summary>Send the media player the command for play.</summary>
		///<param name="target">The target for this service call</param>
		public void MediaPlay(ServiceTarget target)
		{
			_haContext.CallService("media_player", "media_play", target);
		}

		///<summary>Toggle media player play/pause state.</summary>
		///<param name="target">The target for this service call</param>
		public void MediaPlayPause(ServiceTarget target)
		{
			_haContext.CallService("media_player", "media_play_pause", target);
		}

		///<summary>Send the media player the command for previous track.</summary>
		///<param name="target">The target for this service call</param>
		public void MediaPreviousTrack(ServiceTarget target)
		{
			_haContext.CallService("media_player", "media_previous_track", target);
		}

		///<summary>Send the media player the command to seek in current playing media.</summary>
		///<param name="target">The target for this service call</param>
		public void MediaSeek(ServiceTarget target, MediaPlayerMediaSeekParameters data)
		{
			_haContext.CallService("media_player", "media_seek", target, data);
		}

		///<summary>Send the media player the command to seek in current playing media.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="seekPosition">Position to seek to. The format is platform dependent.</param>
		public void MediaSeek(ServiceTarget target, double @seekPosition)
		{
			_haContext.CallService("media_player", "media_seek", target, new MediaPlayerMediaSeekParameters{SeekPosition = @seekPosition});
		}

		///<summary>Send the media player the stop command.</summary>
		///<param name="target">The target for this service call</param>
		public void MediaStop(ServiceTarget target)
		{
			_haContext.CallService("media_player", "media_stop", target);
		}

		///<summary>Send the media player the command for playing media.</summary>
		///<param name="target">The target for this service call</param>
		public void PlayMedia(ServiceTarget target, MediaPlayerPlayMediaParameters data)
		{
			_haContext.CallService("media_player", "play_media", target, data);
		}

		///<summary>Send the media player the command for playing media.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="mediaContentId">The ID of the content to play. Platform dependent. eg: https://home-assistant.io/images/cast/splash.png</param>
		///<param name="mediaContentType">The type of the content to play. Like image, music, tvshow, video, episode, channel or playlist. eg: music</param>
		public void PlayMedia(ServiceTarget target, string @mediaContentId, string @mediaContentType)
		{
			_haContext.CallService("media_player", "play_media", target, new MediaPlayerPlayMediaParameters{MediaContentId = @mediaContentId, MediaContentType = @mediaContentType});
		}

		///<summary>Set repeat mode</summary>
		///<param name="target">The target for this service call</param>
		public void RepeatSet(ServiceTarget target, MediaPlayerRepeatSetParameters data)
		{
			_haContext.CallService("media_player", "repeat_set", target, data);
		}

		///<summary>Set repeat mode</summary>
		///<param name="target">The target for this service call</param>
		///<param name="repeat">Repeat mode to set.</param>
		public void RepeatSet(ServiceTarget target, string @repeat)
		{
			_haContext.CallService("media_player", "repeat_set", target, new MediaPlayerRepeatSetParameters{Repeat = @repeat});
		}

		///<summary>Send the media player the command to change sound mode.</summary>
		///<param name="target">The target for this service call</param>
		public void SelectSoundMode(ServiceTarget target, MediaPlayerSelectSoundModeParameters data)
		{
			_haContext.CallService("media_player", "select_sound_mode", target, data);
		}

		///<summary>Send the media player the command to change sound mode.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="soundMode">Name of the sound mode to switch to. eg: Music</param>
		public void SelectSoundMode(ServiceTarget target, string? @soundMode = null)
		{
			_haContext.CallService("media_player", "select_sound_mode", target, new MediaPlayerSelectSoundModeParameters{SoundMode = @soundMode});
		}

		///<summary>Send the media player the command to change input source.</summary>
		///<param name="target">The target for this service call</param>
		public void SelectSource(ServiceTarget target, MediaPlayerSelectSourceParameters data)
		{
			_haContext.CallService("media_player", "select_source", target, data);
		}

		///<summary>Send the media player the command to change input source.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="source">Name of the source to switch to. Platform dependent. eg: video1</param>
		public void SelectSource(ServiceTarget target, string @source)
		{
			_haContext.CallService("media_player", "select_source", target, new MediaPlayerSelectSourceParameters{Source = @source});
		}

		///<summary>Set shuffling state.</summary>
		///<param name="target">The target for this service call</param>
		public void ShuffleSet(ServiceTarget target, MediaPlayerShuffleSetParameters data)
		{
			_haContext.CallService("media_player", "shuffle_set", target, data);
		}

		///<summary>Set shuffling state.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="shuffle">True/false for enabling/disabling shuffle.</param>
		public void ShuffleSet(ServiceTarget target, bool @shuffle)
		{
			_haContext.CallService("media_player", "shuffle_set", target, new MediaPlayerShuffleSetParameters{Shuffle = @shuffle});
		}

		///<summary>Toggles a media player power state.</summary>
		///<param name="target">The target for this service call</param>
		public void Toggle(ServiceTarget target)
		{
			_haContext.CallService("media_player", "toggle", target);
		}

		///<summary>Turn a media player power off.</summary>
		///<param name="target">The target for this service call</param>
		public void TurnOff(ServiceTarget target)
		{
			_haContext.CallService("media_player", "turn_off", target);
		}

		///<summary>Turn a media player power on.</summary>
		///<param name="target">The target for this service call</param>
		public void TurnOn(ServiceTarget target)
		{
			_haContext.CallService("media_player", "turn_on", target);
		}

		///<summary>Unjoin the player from a group. Only works on platforms with support for player groups.</summary>
		///<param name="target">The target for this service call</param>
		public void Unjoin(ServiceTarget target)
		{
			_haContext.CallService("media_player", "unjoin", target);
		}

		///<summary>Turn a media player volume down.</summary>
		///<param name="target">The target for this service call</param>
		public void VolumeDown(ServiceTarget target)
		{
			_haContext.CallService("media_player", "volume_down", target);
		}

		///<summary>Mute a media player's volume.</summary>
		///<param name="target">The target for this service call</param>
		public void VolumeMute(ServiceTarget target, MediaPlayerVolumeMuteParameters data)
		{
			_haContext.CallService("media_player", "volume_mute", target, data);
		}

		///<summary>Mute a media player's volume.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="isVolumeMuted">True/false for mute/unmute.</param>
		public void VolumeMute(ServiceTarget target, bool @isVolumeMuted)
		{
			_haContext.CallService("media_player", "volume_mute", target, new MediaPlayerVolumeMuteParameters{IsVolumeMuted = @isVolumeMuted});
		}

		///<summary>Set a media player's volume level.</summary>
		///<param name="target">The target for this service call</param>
		public void VolumeSet(ServiceTarget target, MediaPlayerVolumeSetParameters data)
		{
			_haContext.CallService("media_player", "volume_set", target, data);
		}

		///<summary>Set a media player's volume level.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="volumeLevel">Volume level to set as float.</param>
		public void VolumeSet(ServiceTarget target, double @volumeLevel)
		{
			_haContext.CallService("media_player", "volume_set", target, new MediaPlayerVolumeSetParameters{VolumeLevel = @volumeLevel});
		}

		///<summary>Turn a media player volume up.</summary>
		///<param name="target">The target for this service call</param>
		public void VolumeUp(ServiceTarget target)
		{
			_haContext.CallService("media_player", "volume_up", target);
		}
	}

	public record MediaPlayerJoinParameters
	{
		///<summary>The players which will be synced with the target player. eg: ["media_player.multiroom_player2", "media_player.multiroom_player3"]</summary>
		[JsonPropertyName("group_members")]
		public object? GroupMembers { get; init; }
	}

	public record MediaPlayerMediaSeekParameters
	{
		///<summary>Position to seek to. The format is platform dependent.</summary>
		[JsonPropertyName("seek_position")]
		public double? SeekPosition { get; init; }
	}

	public record MediaPlayerPlayMediaParameters
	{
		///<summary>The ID of the content to play. Platform dependent. eg: https://home-assistant.io/images/cast/splash.png</summary>
		[JsonPropertyName("media_content_id")]
		public string? MediaContentId { get; init; }

		///<summary>The type of the content to play. Like image, music, tvshow, video, episode, channel or playlist. eg: music</summary>
		[JsonPropertyName("media_content_type")]
		public string? MediaContentType { get; init; }
	}

	public record MediaPlayerRepeatSetParameters
	{
		///<summary>Repeat mode to set.</summary>
		[JsonPropertyName("repeat")]
		public string? Repeat { get; init; }
	}

	public record MediaPlayerSelectSoundModeParameters
	{
		///<summary>Name of the sound mode to switch to. eg: Music</summary>
		[JsonPropertyName("sound_mode")]
		public string? SoundMode { get; init; }
	}

	public record MediaPlayerSelectSourceParameters
	{
		///<summary>Name of the source to switch to. Platform dependent. eg: video1</summary>
		[JsonPropertyName("source")]
		public string? Source { get; init; }
	}

	public record MediaPlayerShuffleSetParameters
	{
		///<summary>True/false for enabling/disabling shuffle.</summary>
		[JsonPropertyName("shuffle")]
		public bool? Shuffle { get; init; }
	}

	public record MediaPlayerVolumeMuteParameters
	{
		///<summary>True/false for mute/unmute.</summary>
		[JsonPropertyName("is_volume_muted")]
		public bool? IsVolumeMuted { get; init; }
	}

	public record MediaPlayerVolumeSetParameters
	{
		///<summary>Volume level to set as float.</summary>
		[JsonPropertyName("volume_level")]
		public double? VolumeLevel { get; init; }
	}

	public class MelcloudServices
	{
		private readonly IHaContext _haContext;
		public MelcloudServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Sets horizontal vane position.</summary>
		///<param name="target">The target for this service call</param>
		public void SetVaneHorizontal(ServiceTarget target, MelcloudSetVaneHorizontalParameters data)
		{
			_haContext.CallService("melcloud", "set_vane_horizontal", target, data);
		}

		///<summary>Sets horizontal vane position.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="position">Horizontal vane position. Possible options can be found in the vane_horizontal_positions state attribute.  eg: auto</param>
		public void SetVaneHorizontal(ServiceTarget target, string @position)
		{
			_haContext.CallService("melcloud", "set_vane_horizontal", target, new MelcloudSetVaneHorizontalParameters{Position = @position});
		}

		///<summary>Sets vertical vane position.</summary>
		///<param name="target">The target for this service call</param>
		public void SetVaneVertical(ServiceTarget target, MelcloudSetVaneVerticalParameters data)
		{
			_haContext.CallService("melcloud", "set_vane_vertical", target, data);
		}

		///<summary>Sets vertical vane position.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="position">Vertical vane position. Possible options can be found in the vane_vertical_positions state attribute.  eg: auto</param>
		public void SetVaneVertical(ServiceTarget target, string @position)
		{
			_haContext.CallService("melcloud", "set_vane_vertical", target, new MelcloudSetVaneVerticalParameters{Position = @position});
		}
	}

	public record MelcloudSetVaneHorizontalParameters
	{
		///<summary>Horizontal vane position. Possible options can be found in the vane_horizontal_positions state attribute.  eg: auto</summary>
		[JsonPropertyName("position")]
		public string? Position { get; init; }
	}

	public record MelcloudSetVaneVerticalParameters
	{
		///<summary>Vertical vane position. Possible options can be found in the vane_vertical_positions state attribute.  eg: auto</summary>
		[JsonPropertyName("position")]
		public string? Position { get; init; }
	}

	public class MinMaxServices
	{
		private readonly IHaContext _haContext;
		public MinMaxServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Reload all min_max entities.</summary>
		public void Reload()
		{
			_haContext.CallService("min_max", "reload", null);
		}
	}

	public class MqttServices
	{
		private readonly IHaContext _haContext;
		public MqttServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Dump messages on a topic selector to the 'mqtt_dump.txt' file in your configuration folder.</summary>
		public void Dump(MqttDumpParameters data)
		{
			_haContext.CallService("mqtt", "dump", null, data);
		}

		///<summary>Dump messages on a topic selector to the 'mqtt_dump.txt' file in your configuration folder.</summary>
		///<param name="topic">topic to listen to eg: OpenZWave/#</param>
		///<param name="duration">how long we should listen for messages in seconds</param>
		public void Dump(string? @topic = null, long? @duration = null)
		{
			_haContext.CallService("mqtt", "dump", null, new MqttDumpParameters{Topic = @topic, Duration = @duration});
		}

		///<summary>Publish a message to an MQTT topic.</summary>
		public void Publish(MqttPublishParameters data)
		{
			_haContext.CallService("mqtt", "publish", null, data);
		}

		///<summary>Publish a message to an MQTT topic.</summary>
		///<param name="topic">Topic to publish payload. eg: /homeassistant/hello</param>
		///<param name="payload">Payload to publish. eg: This is great</param>
		///<param name="payloadTemplate">Template to render as payload value. Ignored if payload given. eg: {{ states('sensor.temperature') }}</param>
		///<param name="qos">Quality of Service to use.</param>
		///<param name="retain">If message should have the retain flag set.</param>
		public void Publish(string @topic, string? @payload = null, object? @payloadTemplate = null, string? @qos = null, bool? @retain = null)
		{
			_haContext.CallService("mqtt", "publish", null, new MqttPublishParameters{Topic = @topic, Payload = @payload, PayloadTemplate = @payloadTemplate, Qos = @qos, Retain = @retain});
		}

		///<summary>Reload all MQTT entities from YAML.</summary>
		public void Reload()
		{
			_haContext.CallService("mqtt", "reload", null);
		}
	}

	public record MqttDumpParameters
	{
		///<summary>topic to listen to eg: OpenZWave/#</summary>
		[JsonPropertyName("topic")]
		public string? Topic { get; init; }

		///<summary>how long we should listen for messages in seconds</summary>
		[JsonPropertyName("duration")]
		public long? Duration { get; init; }
	}

	public record MqttPublishParameters
	{
		///<summary>Topic to publish payload. eg: /homeassistant/hello</summary>
		[JsonPropertyName("topic")]
		public string? Topic { get; init; }

		///<summary>Payload to publish. eg: This is great</summary>
		[JsonPropertyName("payload")]
		public string? Payload { get; init; }

		///<summary>Template to render as payload value. Ignored if payload given. eg: {{ states('sensor.temperature') }}</summary>
		[JsonPropertyName("payload_template")]
		public object? PayloadTemplate { get; init; }

		///<summary>Quality of Service to use.</summary>
		[JsonPropertyName("qos")]
		public string? Qos { get; init; }

		///<summary>If message should have the retain flag set.</summary>
		[JsonPropertyName("retain")]
		public bool? Retain { get; init; }
	}

	public class NetdaemonServices
	{
		private readonly IHaContext _haContext;
		public NetdaemonServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Create an entity</summary>
		public void EntityCreate(NetdaemonEntityCreateParameters data)
		{
			_haContext.CallService("netdaemon", "entity_create", null, data);
		}

		///<summary>Create an entity</summary>
		///<param name="entityId">The entity ID of the entity eg: sensor.awesome</param>
		///<param name="state">The state of the entity eg: Lorem ipsum</param>
		///<param name="icon">The icon for the entity eg: mdi:rocket-launch-outline</param>
		///<param name="unit">The unit of measurement for the entity</param>
		///<param name="options">List of options for a select entity</param>
		///<param name="attributes">The attributes of the entity</param>
		public void EntityCreate(object? @entityId = null, object? @state = null, object? @icon = null, object? @unit = null, object? @options = null, object? @attributes = null)
		{
			_haContext.CallService("netdaemon", "entity_create", null, new NetdaemonEntityCreateParameters{EntityId = @entityId, State = @state, Icon = @icon, Unit = @unit, Options = @options, Attributes = @attributes});
		}

		///<summary>Remove an entity</summary>
		public void EntityRemove(NetdaemonEntityRemoveParameters data)
		{
			_haContext.CallService("netdaemon", "entity_remove", null, data);
		}

		///<summary>Remove an entity</summary>
		///<param name="entityId">The entity ID of the entity eg: sensor.awesome</param>
		public void EntityRemove(object? @entityId = null)
		{
			_haContext.CallService("netdaemon", "entity_remove", null, new NetdaemonEntityRemoveParameters{EntityId = @entityId});
		}

		///<summary>Update an entity</summary>
		public void EntityUpdate(NetdaemonEntityUpdateParameters data)
		{
			_haContext.CallService("netdaemon", "entity_update", null, data);
		}

		///<summary>Update an entity</summary>
		///<param name="entityId">The entity ID of the entity eg: sensor.awesome</param>
		///<param name="state">The state of the entity eg: Lorem ipsum</param>
		///<param name="icon">The icon for the entity eg: mdi:rocket-launch-outline</param>
		///<param name="unit">The unit of measurement for the entity</param>
		///<param name="options">List of options for a select entity</param>
		///<param name="attributes">The attributes of the entity</param>
		public void EntityUpdate(object? @entityId = null, object? @state = null, object? @icon = null, object? @unit = null, object? @options = null, object? @attributes = null)
		{
			_haContext.CallService("netdaemon", "entity_update", null, new NetdaemonEntityUpdateParameters{EntityId = @entityId, State = @state, Icon = @icon, Unit = @unit, Options = @options, Attributes = @attributes});
		}

		///<summary>Register a new service for netdaemon, used by the daemon and not to be used by users</summary>
		public void RegisterService(NetdaemonRegisterServiceParameters data)
		{
			_haContext.CallService("netdaemon", "register_service", null, data);
		}

		///<summary>Register a new service for netdaemon, used by the daemon and not to be used by users</summary>
		///<param name="service">The name of the service to register</param>
		///<param name="class">The class that implements the service call</param>
		///<param name="method">The method to call</param>
		public void RegisterService(object? @service = null, object? @class = null, object? @method = null)
		{
			_haContext.CallService("netdaemon", "register_service", null, new NetdaemonRegisterServiceParameters{Service = @service, Class = @class, Method = @method});
		}

		public void ReloadApps()
		{
			_haContext.CallService("netdaemon", "reload_apps", null);
		}
	}

	public record NetdaemonEntityCreateParameters
	{
		///<summary>The entity ID of the entity eg: sensor.awesome</summary>
		[JsonPropertyName("entity_id")]
		public object? EntityId { get; init; }

		///<summary>The state of the entity eg: Lorem ipsum</summary>
		[JsonPropertyName("state")]
		public object? State { get; init; }

		///<summary>The icon for the entity eg: mdi:rocket-launch-outline</summary>
		[JsonPropertyName("icon")]
		public object? Icon { get; init; }

		///<summary>The unit of measurement for the entity</summary>
		[JsonPropertyName("unit")]
		public object? Unit { get; init; }

		///<summary>List of options for a select entity</summary>
		[JsonPropertyName("options")]
		public object? Options { get; init; }

		///<summary>The attributes of the entity</summary>
		[JsonPropertyName("attributes")]
		public object? Attributes { get; init; }
	}

	public record NetdaemonEntityRemoveParameters
	{
		///<summary>The entity ID of the entity eg: sensor.awesome</summary>
		[JsonPropertyName("entity_id")]
		public object? EntityId { get; init; }
	}

	public record NetdaemonEntityUpdateParameters
	{
		///<summary>The entity ID of the entity eg: sensor.awesome</summary>
		[JsonPropertyName("entity_id")]
		public object? EntityId { get; init; }

		///<summary>The state of the entity eg: Lorem ipsum</summary>
		[JsonPropertyName("state")]
		public object? State { get; init; }

		///<summary>The icon for the entity eg: mdi:rocket-launch-outline</summary>
		[JsonPropertyName("icon")]
		public object? Icon { get; init; }

		///<summary>The unit of measurement for the entity</summary>
		[JsonPropertyName("unit")]
		public object? Unit { get; init; }

		///<summary>List of options for a select entity</summary>
		[JsonPropertyName("options")]
		public object? Options { get; init; }

		///<summary>The attributes of the entity</summary>
		[JsonPropertyName("attributes")]
		public object? Attributes { get; init; }
	}

	public record NetdaemonRegisterServiceParameters
	{
		///<summary>The name of the service to register</summary>
		[JsonPropertyName("service")]
		public object? Service { get; init; }

		///<summary>The class that implements the service call</summary>
		[JsonPropertyName("class")]
		public object? Class { get; init; }

		///<summary>The method to call</summary>
		[JsonPropertyName("method")]
		public object? Method { get; init; }
	}

	public class NotifyServices
	{
		private readonly IHaContext _haContext;
		public NotifyServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Sends a notification message using the alexa_media service.</summary>
		public void AlexaMedia(NotifyAlexaMediaParameters data)
		{
			_haContext.CallService("notify", "alexa_media", null, data);
		}

		///<summary>Sends a notification message using the alexa_media service.</summary>
		///<param name="message">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="title">Title for your notification. eg: Your Garage Door Friend</param>
		///<param name="target">An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</param>
		///<param name="data">Extended information for notification. Optional depending on the platform. eg: platform specific</param>
		public void AlexaMedia(string @message, string? @title = null, object? @target = null, object? @data = null)
		{
			_haContext.CallService("notify", "alexa_media", null, new NotifyAlexaMediaParameters{Message = @message, Title = @title, Target = @target, Data = @data});
		}

		///<summary>Sends a notification message using the alexa_media_aaron integration.</summary>
		public void AlexaMediaAaron(NotifyAlexaMediaAaronParameters data)
		{
			_haContext.CallService("notify", "alexa_media_aaron", null, data);
		}

		///<summary>Sends a notification message using the alexa_media_aaron integration.</summary>
		///<param name="message">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="title">Title for your notification. eg: Your Garage Door Friend</param>
		///<param name="target">An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</param>
		///<param name="data">Extended information for notification. Optional depending on the platform. eg: platform specific</param>
		public void AlexaMediaAaron(string @message, string? @title = null, object? @target = null, object? @data = null)
		{
			_haContext.CallService("notify", "alexa_media_aaron", null, new NotifyAlexaMediaAaronParameters{Message = @message, Title = @title, Target = @target, Data = @data});
		}

		///<summary>Sends a notification message using the alexa_media_bose_qc35_ii integration.</summary>
		public void AlexaMediaBoseQc35Ii(NotifyAlexaMediaBoseQc35IiParameters data)
		{
			_haContext.CallService("notify", "alexa_media_bose_qc35_ii", null, data);
		}

		///<summary>Sends a notification message using the alexa_media_bose_qc35_ii integration.</summary>
		///<param name="message">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="title">Title for your notification. eg: Your Garage Door Friend</param>
		///<param name="target">An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</param>
		///<param name="data">Extended information for notification. Optional depending on the platform. eg: platform specific</param>
		public void AlexaMediaBoseQc35Ii(string @message, string? @title = null, object? @target = null, object? @data = null)
		{
			_haContext.CallService("notify", "alexa_media_bose_qc35_ii", null, new NotifyAlexaMediaBoseQc35IiParameters{Message = @message, Title = @title, Target = @target, Data = @data});
		}

		///<summary>Sends a notification message using the alexa_media_dining integration.</summary>
		public void AlexaMediaDining(NotifyAlexaMediaDiningParameters data)
		{
			_haContext.CallService("notify", "alexa_media_dining", null, data);
		}

		///<summary>Sends a notification message using the alexa_media_dining integration.</summary>
		///<param name="message">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="title">Title for your notification. eg: Your Garage Door Friend</param>
		///<param name="target">An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</param>
		///<param name="data">Extended information for notification. Optional depending on the platform. eg: platform specific</param>
		public void AlexaMediaDining(string @message, string? @title = null, object? @target = null, object? @data = null)
		{
			_haContext.CallService("notify", "alexa_media_dining", null, new NotifyAlexaMediaDiningParameters{Message = @message, Title = @title, Target = @target, Data = @data});
		}

		///<summary>Sends a notification message using the alexa_media_downstairs integration.</summary>
		public void AlexaMediaDownstairs(NotifyAlexaMediaDownstairsParameters data)
		{
			_haContext.CallService("notify", "alexa_media_downstairs", null, data);
		}

		///<summary>Sends a notification message using the alexa_media_downstairs integration.</summary>
		///<param name="message">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="title">Title for your notification. eg: Your Garage Door Friend</param>
		///<param name="target">An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</param>
		///<param name="data">Extended information for notification. Optional depending on the platform. eg: platform specific</param>
		public void AlexaMediaDownstairs(string @message, string? @title = null, object? @target = null, object? @data = null)
		{
			_haContext.CallService("notify", "alexa_media_downstairs", null, new NotifyAlexaMediaDownstairsParameters{Message = @message, Title = @title, Target = @target, Data = @data});
		}

		///<summary>Sends a notification message using the alexa_media_eugene_s_2nd_echo_dot integration.</summary>
		public void AlexaMediaEugeneS2ndEchoDot(NotifyAlexaMediaEugeneS2ndEchoDotParameters data)
		{
			_haContext.CallService("notify", "alexa_media_eugene_s_2nd_echo_dot", null, data);
		}

		///<summary>Sends a notification message using the alexa_media_eugene_s_2nd_echo_dot integration.</summary>
		///<param name="message">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="title">Title for your notification. eg: Your Garage Door Friend</param>
		///<param name="target">An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</param>
		///<param name="data">Extended information for notification. Optional depending on the platform. eg: platform specific</param>
		public void AlexaMediaEugeneS2ndEchoDot(string @message, string? @title = null, object? @target = null, object? @data = null)
		{
			_haContext.CallService("notify", "alexa_media_eugene_s_2nd_echo_dot", null, new NotifyAlexaMediaEugeneS2ndEchoDotParameters{Message = @message, Title = @title, Target = @target, Data = @data});
		}

		///<summary>Sends a notification message using the alexa_media_eugene_s_5th_echo_dot integration.</summary>
		public void AlexaMediaEugeneS5thEchoDot(NotifyAlexaMediaEugeneS5thEchoDotParameters data)
		{
			_haContext.CallService("notify", "alexa_media_eugene_s_5th_echo_dot", null, data);
		}

		///<summary>Sends a notification message using the alexa_media_eugene_s_5th_echo_dot integration.</summary>
		///<param name="message">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="title">Title for your notification. eg: Your Garage Door Friend</param>
		///<param name="target">An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</param>
		///<param name="data">Extended information for notification. Optional depending on the platform. eg: platform specific</param>
		public void AlexaMediaEugeneS5thEchoDot(string @message, string? @title = null, object? @target = null, object? @data = null)
		{
			_haContext.CallService("notify", "alexa_media_eugene_s_5th_echo_dot", null, new NotifyAlexaMediaEugeneS5thEchoDotParameters{Message = @message, Title = @title, Target = @target, Data = @data});
		}

		///<summary>Sends a notification message using the alexa_media_eugene_s_fire integration.</summary>
		public void AlexaMediaEugeneSFire(NotifyAlexaMediaEugeneSFireParameters data)
		{
			_haContext.CallService("notify", "alexa_media_eugene_s_fire", null, data);
		}

		///<summary>Sends a notification message using the alexa_media_eugene_s_fire integration.</summary>
		///<param name="message">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="title">Title for your notification. eg: Your Garage Door Friend</param>
		///<param name="target">An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</param>
		///<param name="data">Extended information for notification. Optional depending on the platform. eg: platform specific</param>
		public void AlexaMediaEugeneSFire(string @message, string? @title = null, object? @target = null, object? @data = null)
		{
			_haContext.CallService("notify", "alexa_media_eugene_s_fire", null, new NotifyAlexaMediaEugeneSFireParameters{Message = @message, Title = @title, Target = @target, Data = @data});
		}

		///<summary>Sends a notification message using the alexa_media_eugene_s_lg_oled_webos_2021_tv integration.</summary>
		public void AlexaMediaEugeneSLgOledWebos2021Tv(NotifyAlexaMediaEugeneSLgOledWebos2021TvParameters data)
		{
			_haContext.CallService("notify", "alexa_media_eugene_s_lg_oled_webos_2021_tv", null, data);
		}

		///<summary>Sends a notification message using the alexa_media_eugene_s_lg_oled_webos_2021_tv integration.</summary>
		///<param name="message">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="title">Title for your notification. eg: Your Garage Door Friend</param>
		///<param name="target">An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</param>
		///<param name="data">Extended information for notification. Optional depending on the platform. eg: platform specific</param>
		public void AlexaMediaEugeneSLgOledWebos2021Tv(string @message, string? @title = null, object? @target = null, object? @data = null)
		{
			_haContext.CallService("notify", "alexa_media_eugene_s_lg_oled_webos_2021_tv", null, new NotifyAlexaMediaEugeneSLgOledWebos2021TvParameters{Message = @message, Title = @title, Target = @target, Data = @data});
		}

		///<summary>Sends a notification message using the alexa_media_eugene_s_sonos_arc integration.</summary>
		public void AlexaMediaEugeneSSonosArc(NotifyAlexaMediaEugeneSSonosArcParameters data)
		{
			_haContext.CallService("notify", "alexa_media_eugene_s_sonos_arc", null, data);
		}

		///<summary>Sends a notification message using the alexa_media_eugene_s_sonos_arc integration.</summary>
		///<param name="message">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="title">Title for your notification. eg: Your Garage Door Friend</param>
		///<param name="target">An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</param>
		///<param name="data">Extended information for notification. Optional depending on the platform. eg: platform specific</param>
		public void AlexaMediaEugeneSSonosArc(string @message, string? @title = null, object? @target = null, object? @data = null)
		{
			_haContext.CallService("notify", "alexa_media_eugene_s_sonos_arc", null, new NotifyAlexaMediaEugeneSSonosArcParameters{Message = @message, Title = @title, Target = @target, Data = @data});
		}

		///<summary>Sends a notification message using the alexa_media_everywhere_2 integration.</summary>
		public void AlexaMediaEverywhere2(NotifyAlexaMediaEverywhere2Parameters data)
		{
			_haContext.CallService("notify", "alexa_media_everywhere_2", null, data);
		}

		///<summary>Sends a notification message using the alexa_media_everywhere_2 integration.</summary>
		///<param name="message">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="title">Title for your notification. eg: Your Garage Door Friend</param>
		///<param name="target">An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</param>
		///<param name="data">Extended information for notification. Optional depending on the platform. eg: platform specific</param>
		public void AlexaMediaEverywhere2(string @message, string? @title = null, object? @target = null, object? @data = null)
		{
			_haContext.CallService("notify", "alexa_media_everywhere_2", null, new NotifyAlexaMediaEverywhere2Parameters{Message = @message, Title = @title, Target = @target, Data = @data});
		}

		///<summary>Sends a notification message using the alexa_media_jayden integration.</summary>
		public void AlexaMediaJayden(NotifyAlexaMediaJaydenParameters data)
		{
			_haContext.CallService("notify", "alexa_media_jayden", null, data);
		}

		///<summary>Sends a notification message using the alexa_media_jayden integration.</summary>
		///<param name="message">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="title">Title for your notification. eg: Your Garage Door Friend</param>
		///<param name="target">An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</param>
		///<param name="data">Extended information for notification. Optional depending on the platform. eg: platform specific</param>
		public void AlexaMediaJayden(string @message, string? @title = null, object? @target = null, object? @data = null)
		{
			_haContext.CallService("notify", "alexa_media_jayden", null, new NotifyAlexaMediaJaydenParameters{Message = @message, Title = @title, Target = @target, Data = @data});
		}

		///<summary>Sends a notification message using the alexa_media_kitchen integration.</summary>
		public void AlexaMediaKitchen(NotifyAlexaMediaKitchenParameters data)
		{
			_haContext.CallService("notify", "alexa_media_kitchen", null, data);
		}

		///<summary>Sends a notification message using the alexa_media_kitchen integration.</summary>
		///<param name="message">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="title">Title for your notification. eg: Your Garage Door Friend</param>
		///<param name="target">An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</param>
		///<param name="data">Extended information for notification. Optional depending on the platform. eg: platform specific</param>
		public void AlexaMediaKitchen(string @message, string? @title = null, object? @target = null, object? @data = null)
		{
			_haContext.CallService("notify", "alexa_media_kitchen", null, new NotifyAlexaMediaKitchenParameters{Message = @message, Title = @title, Target = @target, Data = @data});
		}

		///<summary>Sends a notification message using the alexa_media_last_called integration.</summary>
		public void AlexaMediaLastCalled(NotifyAlexaMediaLastCalledParameters data)
		{
			_haContext.CallService("notify", "alexa_media_last_called", null, data);
		}

		///<summary>Sends a notification message using the alexa_media_last_called integration.</summary>
		///<param name="message">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="title">Title for your notification. eg: Your Garage Door Friend</param>
		///<param name="target">An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</param>
		///<param name="data">Extended information for notification. Optional depending on the platform. eg: platform specific</param>
		public void AlexaMediaLastCalled(string @message, string? @title = null, object? @target = null, object? @data = null)
		{
			_haContext.CallService("notify", "alexa_media_last_called", null, new NotifyAlexaMediaLastCalledParameters{Message = @message, Title = @title, Target = @target, Data = @data});
		}

		///<summary>Sends a notification message using the alexa_media_lounge_sonos integration.</summary>
		public void AlexaMediaLoungeSonos(NotifyAlexaMediaLoungeSonosParameters data)
		{
			_haContext.CallService("notify", "alexa_media_lounge_sonos", null, data);
		}

		///<summary>Sends a notification message using the alexa_media_lounge_sonos integration.</summary>
		///<param name="message">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="title">Title for your notification. eg: Your Garage Door Friend</param>
		///<param name="target">An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</param>
		///<param name="data">Extended information for notification. Optional depending on the platform. eg: platform specific</param>
		public void AlexaMediaLoungeSonos(string @message, string? @title = null, object? @target = null, object? @data = null)
		{
			_haContext.CallService("notify", "alexa_media_lounge_sonos", null, new NotifyAlexaMediaLoungeSonosParameters{Message = @message, Title = @title, Target = @target, Data = @data});
		}

		///<summary>Sends a notification message using the alexa_media_master integration.</summary>
		public void AlexaMediaMaster(NotifyAlexaMediaMasterParameters data)
		{
			_haContext.CallService("notify", "alexa_media_master", null, data);
		}

		///<summary>Sends a notification message using the alexa_media_master integration.</summary>
		///<param name="message">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="title">Title for your notification. eg: Your Garage Door Friend</param>
		///<param name="target">An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</param>
		///<param name="data">Extended information for notification. Optional depending on the platform. eg: platform specific</param>
		public void AlexaMediaMaster(string @message, string? @title = null, object? @target = null, object? @data = null)
		{
			_haContext.CallService("notify", "alexa_media_master", null, new NotifyAlexaMediaMasterParameters{Message = @message, Title = @title, Target = @target, Data = @data});
		}

		///<summary>Sends a notification message using the alexa_media_master_tv_alexa integration.</summary>
		public void AlexaMediaMasterTvAlexa(NotifyAlexaMediaMasterTvAlexaParameters data)
		{
			_haContext.CallService("notify", "alexa_media_master_tv_alexa", null, data);
		}

		///<summary>Sends a notification message using the alexa_media_master_tv_alexa integration.</summary>
		///<param name="message">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="title">Title for your notification. eg: Your Garage Door Friend</param>
		///<param name="target">An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</param>
		///<param name="data">Extended information for notification. Optional depending on the platform. eg: platform specific</param>
		public void AlexaMediaMasterTvAlexa(string @message, string? @title = null, object? @target = null, object? @data = null)
		{
			_haContext.CallService("notify", "alexa_media_master_tv_alexa", null, new NotifyAlexaMediaMasterTvAlexaParameters{Message = @message, Title = @title, Target = @target, Data = @data});
		}

		///<summary>Sends a notification message using the alexa_media_office integration.</summary>
		public void AlexaMediaOffice(NotifyAlexaMediaOfficeParameters data)
		{
			_haContext.CallService("notify", "alexa_media_office", null, data);
		}

		///<summary>Sends a notification message using the alexa_media_office integration.</summary>
		///<param name="message">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="title">Title for your notification. eg: Your Garage Door Friend</param>
		///<param name="target">An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</param>
		///<param name="data">Extended information for notification. Optional depending on the platform. eg: platform specific</param>
		public void AlexaMediaOffice(string @message, string? @title = null, object? @target = null, object? @data = null)
		{
			_haContext.CallService("notify", "alexa_media_office", null, new NotifyAlexaMediaOfficeParameters{Message = @message, Title = @title, Target = @target, Data = @data});
		}

		///<summary>Sends a notification message using the alexa_media_playroom integration.</summary>
		public void AlexaMediaPlayroom(NotifyAlexaMediaPlayroomParameters data)
		{
			_haContext.CallService("notify", "alexa_media_playroom", null, data);
		}

		///<summary>Sends a notification message using the alexa_media_playroom integration.</summary>
		///<param name="message">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="title">Title for your notification. eg: Your Garage Door Friend</param>
		///<param name="target">An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</param>
		///<param name="data">Extended information for notification. Optional depending on the platform. eg: platform specific</param>
		public void AlexaMediaPlayroom(string @message, string? @title = null, object? @target = null, object? @data = null)
		{
			_haContext.CallService("notify", "alexa_media_playroom", null, new NotifyAlexaMediaPlayroomParameters{Message = @message, Title = @title, Target = @target, Data = @data});
		}

		///<summary>Sends a notification message using the alexa_media_spare_echo integration.</summary>
		public void AlexaMediaSpareEcho(NotifyAlexaMediaSpareEchoParameters data)
		{
			_haContext.CallService("notify", "alexa_media_spare_echo", null, data);
		}

		///<summary>Sends a notification message using the alexa_media_spare_echo integration.</summary>
		///<param name="message">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="title">Title for your notification. eg: Your Garage Door Friend</param>
		///<param name="target">An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</param>
		///<param name="data">Extended information for notification. Optional depending on the platform. eg: platform specific</param>
		public void AlexaMediaSpareEcho(string @message, string? @title = null, object? @target = null, object? @data = null)
		{
			_haContext.CallService("notify", "alexa_media_spare_echo", null, new NotifyAlexaMediaSpareEchoParameters{Message = @message, Title = @title, Target = @target, Data = @data});
		}

		///<summary>Sends a notification message using the alexa_media_this_device integration.</summary>
		public void AlexaMediaThisDevice(NotifyAlexaMediaThisDeviceParameters data)
		{
			_haContext.CallService("notify", "alexa_media_this_device", null, data);
		}

		///<summary>Sends a notification message using the alexa_media_this_device integration.</summary>
		///<param name="message">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="title">Title for your notification. eg: Your Garage Door Friend</param>
		///<param name="target">An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</param>
		///<param name="data">Extended information for notification. Optional depending on the platform. eg: platform specific</param>
		public void AlexaMediaThisDevice(string @message, string? @title = null, object? @target = null, object? @data = null)
		{
			_haContext.CallService("notify", "alexa_media_this_device", null, new NotifyAlexaMediaThisDeviceParameters{Message = @message, Title = @title, Target = @target, Data = @data});
		}

		///<summary>Sends a notification message using the alexa_media_this_device_2 integration.</summary>
		public void AlexaMediaThisDevice2(NotifyAlexaMediaThisDevice2Parameters data)
		{
			_haContext.CallService("notify", "alexa_media_this_device_2", null, data);
		}

		///<summary>Sends a notification message using the alexa_media_this_device_2 integration.</summary>
		///<param name="message">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="title">Title for your notification. eg: Your Garage Door Friend</param>
		///<param name="target">An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</param>
		///<param name="data">Extended information for notification. Optional depending on the platform. eg: platform specific</param>
		public void AlexaMediaThisDevice2(string @message, string? @title = null, object? @target = null, object? @data = null)
		{
			_haContext.CallService("notify", "alexa_media_this_device_2", null, new NotifyAlexaMediaThisDevice2Parameters{Message = @message, Title = @title, Target = @target, Data = @data});
		}

		///<summary>Sends a notification message using the alexa_media_upstairs integration.</summary>
		public void AlexaMediaUpstairs(NotifyAlexaMediaUpstairsParameters data)
		{
			_haContext.CallService("notify", "alexa_media_upstairs", null, data);
		}

		///<summary>Sends a notification message using the alexa_media_upstairs integration.</summary>
		///<param name="message">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="title">Title for your notification. eg: Your Garage Door Friend</param>
		///<param name="target">An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</param>
		///<param name="data">Extended information for notification. Optional depending on the platform. eg: platform specific</param>
		public void AlexaMediaUpstairs(string @message, string? @title = null, object? @target = null, object? @data = null)
		{
			_haContext.CallService("notify", "alexa_media_upstairs", null, new NotifyAlexaMediaUpstairsParameters{Message = @message, Title = @title, Target = @target, Data = @data});
		}

		///<summary>Sends a notification message using the eugene service.</summary>
		public void Eugene(NotifyEugeneParameters data)
		{
			_haContext.CallService("notify", "eugene", null, data);
		}

		///<summary>Sends a notification message using the eugene service.</summary>
		///<param name="message">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="title">Title for your notification. eg: Your Garage Door Friend</param>
		///<param name="target">An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</param>
		///<param name="data">Extended information for notification. Optional depending on the platform. eg: platform specific</param>
		public void Eugene(string @message, string? @title = null, object? @target = null, object? @data = null)
		{
			_haContext.CallService("notify", "eugene", null, new NotifyEugeneParameters{Message = @message, Title = @title, Target = @target, Data = @data});
		}

		///<summary>Sends a notification message using the hailey service.</summary>
		public void Hailey(NotifyHaileyParameters data)
		{
			_haContext.CallService("notify", "hailey", null, data);
		}

		///<summary>Sends a notification message using the hailey service.</summary>
		///<param name="message">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="title">Title for your notification. eg: Your Garage Door Friend</param>
		///<param name="target">An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</param>
		///<param name="data">Extended information for notification. Optional depending on the platform. eg: platform specific</param>
		public void Hailey(string @message, string? @title = null, object? @target = null, object? @data = null)
		{
			_haContext.CallService("notify", "hailey", null, new NotifyHaileyParameters{Message = @message, Title = @title, Target = @target, Data = @data});
		}

		///<summary>Sends a notification message using the lounge_tv service.</summary>
		public void LoungeTv(NotifyLoungeTvParameters data)
		{
			_haContext.CallService("notify", "lounge_tv", null, data);
		}

		///<summary>Sends a notification message using the lounge_tv service.</summary>
		///<param name="message">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="title">Title for your notification. eg: Your Garage Door Friend</param>
		///<param name="target">An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</param>
		///<param name="data">Extended information for notification. Optional depending on the platform. eg: platform specific</param>
		public void LoungeTv(string @message, string? @title = null, object? @target = null, object? @data = null)
		{
			_haContext.CallService("notify", "lounge_tv", null, new NotifyLoungeTvParameters{Message = @message, Title = @title, Target = @target, Data = @data});
		}

		///<summary>Sends a notification message using the master_tv service.</summary>
		public void MasterTv(NotifyMasterTvParameters data)
		{
			_haContext.CallService("notify", "master_tv", null, data);
		}

		///<summary>Sends a notification message using the master_tv service.</summary>
		///<param name="message">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="title">Title for your notification. eg: Your Garage Door Friend</param>
		///<param name="target">An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</param>
		///<param name="data">Extended information for notification. Optional depending on the platform. eg: platform specific</param>
		public void MasterTv(string @message, string? @title = null, object? @target = null, object? @data = null)
		{
			_haContext.CallService("notify", "master_tv", null, new NotifyMasterTvParameters{Message = @message, Title = @title, Target = @target, Data = @data});
		}

		///<summary>Sends a notification message using the mobile_app_eugenes_iphone integration.</summary>
		public void MobileAppEugenesIphone(NotifyMobileAppEugenesIphoneParameters data)
		{
			_haContext.CallService("notify", "mobile_app_eugenes_iphone", null, data);
		}

		///<summary>Sends a notification message using the mobile_app_eugenes_iphone integration.</summary>
		///<param name="message">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="title">Title for your notification. eg: Your Garage Door Friend</param>
		///<param name="target">An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</param>
		///<param name="data">Extended information for notification. Optional depending on the platform. eg: platform specific</param>
		public void MobileAppEugenesIphone(string @message, string? @title = null, object? @target = null, object? @data = null)
		{
			_haContext.CallService("notify", "mobile_app_eugenes_iphone", null, new NotifyMobileAppEugenesIphoneParameters{Message = @message, Title = @title, Target = @target, Data = @data});
		}

		///<summary>Sends a notification message using the mobile_app_hailey_s_iphone integration.</summary>
		public void MobileAppHaileySIphone(NotifyMobileAppHaileySIphoneParameters data)
		{
			_haContext.CallService("notify", "mobile_app_hailey_s_iphone", null, data);
		}

		///<summary>Sends a notification message using the mobile_app_hailey_s_iphone integration.</summary>
		///<param name="message">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="title">Title for your notification. eg: Your Garage Door Friend</param>
		///<param name="target">An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</param>
		///<param name="data">Extended information for notification. Optional depending on the platform. eg: platform specific</param>
		public void MobileAppHaileySIphone(string @message, string? @title = null, object? @target = null, object? @data = null)
		{
			_haContext.CallService("notify", "mobile_app_hailey_s_iphone", null, new NotifyMobileAppHaileySIphoneParameters{Message = @message, Title = @title, Target = @target, Data = @data});
		}

		///<summary>Sends a notification message using the mobile_app_haileys_macbook_air integration.</summary>
		public void MobileAppHaileysMacbookAir(NotifyMobileAppHaileysMacbookAirParameters data)
		{
			_haContext.CallService("notify", "mobile_app_haileys_macbook_air", null, data);
		}

		///<summary>Sends a notification message using the mobile_app_haileys_macbook_air integration.</summary>
		///<param name="message">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="title">Title for your notification. eg: Your Garage Door Friend</param>
		///<param name="target">An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</param>
		///<param name="data">Extended information for notification. Optional depending on the platform. eg: platform specific</param>
		public void MobileAppHaileysMacbookAir(string @message, string? @title = null, object? @target = null, object? @data = null)
		{
			_haContext.CallService("notify", "mobile_app_haileys_macbook_air", null, new NotifyMobileAppHaileysMacbookAirParameters{Message = @message, Title = @title, Target = @target, Data = @data});
		}

		///<summary>Sends a notification message using the mobile_app_iphone integration.</summary>
		public void MobileAppIphone(NotifyMobileAppIphoneParameters data)
		{
			_haContext.CallService("notify", "mobile_app_iphone", null, data);
		}

		///<summary>Sends a notification message using the mobile_app_iphone integration.</summary>
		///<param name="message">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="title">Title for your notification. eg: Your Garage Door Friend</param>
		///<param name="target">An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</param>
		///<param name="data">Extended information for notification. Optional depending on the platform. eg: platform specific</param>
		public void MobileAppIphone(string @message, string? @title = null, object? @target = null, object? @data = null)
		{
			_haContext.CallService("notify", "mobile_app_iphone", null, new NotifyMobileAppIphoneParameters{Message = @message, Title = @title, Target = @target, Data = @data});
		}

		///<summary>Sends a notification message using the notify service.</summary>
		public void Notify(NotifyNotifyParameters data)
		{
			_haContext.CallService("notify", "notify", null, data);
		}

		///<summary>Sends a notification message using the notify service.</summary>
		///<param name="message">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="title">Title for your notification. eg: Your Garage Door Friend</param>
		///<param name="target">An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</param>
		///<param name="data">Extended information for notification. Optional depending on the platform. eg: platform specific</param>
		public void Notify(string @message, string? @title = null, object? @target = null, object? @data = null)
		{
			_haContext.CallService("notify", "notify", null, new NotifyNotifyParameters{Message = @message, Title = @title, Target = @target, Data = @data});
		}

		///<summary>Sends a notification to the visible in the front-end.</summary>
		public void PersistentNotification(NotifyPersistentNotificationParameters data)
		{
			_haContext.CallService("notify", "persistent_notification", null, data);
		}

		///<summary>Sends a notification to the visible in the front-end.</summary>
		///<param name="message">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="title">Title for your notification. eg: Your Garage Door Friend</param>
		public void PersistentNotification(string @message, string? @title = null)
		{
			_haContext.CallService("notify", "persistent_notification", null, new NotifyPersistentNotificationParameters{Message = @message, Title = @title});
		}

		///<summary>Sends a notification message using the twinstead service.</summary>
		public void Twinstead(NotifyTwinsteadParameters data)
		{
			_haContext.CallService("notify", "twinstead", null, data);
		}

		///<summary>Sends a notification message using the twinstead service.</summary>
		///<param name="message">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="title">Title for your notification. eg: Your Garage Door Friend</param>
		///<param name="target">An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</param>
		///<param name="data">Extended information for notification. Optional depending on the platform. eg: platform specific</param>
		public void Twinstead(string @message, string? @title = null, object? @target = null, object? @data = null)
		{
			_haContext.CallService("notify", "twinstead", null, new NotifyTwinsteadParameters{Message = @message, Title = @title, Target = @target, Data = @data});
		}
	}

	public record NotifyAlexaMediaParameters
	{
		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Title for your notification. eg: Your Garage Door Friend</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }

		///<summary>An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Extended information for notification. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("data")]
		public object? Data { get; init; }
	}

	public record NotifyAlexaMediaAaronParameters
	{
		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Title for your notification. eg: Your Garage Door Friend</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }

		///<summary>An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Extended information for notification. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("data")]
		public object? Data { get; init; }
	}

	public record NotifyAlexaMediaBoseQc35IiParameters
	{
		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Title for your notification. eg: Your Garage Door Friend</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }

		///<summary>An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Extended information for notification. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("data")]
		public object? Data { get; init; }
	}

	public record NotifyAlexaMediaDiningParameters
	{
		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Title for your notification. eg: Your Garage Door Friend</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }

		///<summary>An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Extended information for notification. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("data")]
		public object? Data { get; init; }
	}

	public record NotifyAlexaMediaDownstairsParameters
	{
		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Title for your notification. eg: Your Garage Door Friend</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }

		///<summary>An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Extended information for notification. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("data")]
		public object? Data { get; init; }
	}

	public record NotifyAlexaMediaEugeneS2ndEchoDotParameters
	{
		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Title for your notification. eg: Your Garage Door Friend</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }

		///<summary>An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Extended information for notification. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("data")]
		public object? Data { get; init; }
	}

	public record NotifyAlexaMediaEugeneS5thEchoDotParameters
	{
		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Title for your notification. eg: Your Garage Door Friend</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }

		///<summary>An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Extended information for notification. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("data")]
		public object? Data { get; init; }
	}

	public record NotifyAlexaMediaEugeneSFireParameters
	{
		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Title for your notification. eg: Your Garage Door Friend</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }

		///<summary>An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Extended information for notification. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("data")]
		public object? Data { get; init; }
	}

	public record NotifyAlexaMediaEugeneSLgOledWebos2021TvParameters
	{
		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Title for your notification. eg: Your Garage Door Friend</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }

		///<summary>An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Extended information for notification. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("data")]
		public object? Data { get; init; }
	}

	public record NotifyAlexaMediaEugeneSSonosArcParameters
	{
		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Title for your notification. eg: Your Garage Door Friend</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }

		///<summary>An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Extended information for notification. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("data")]
		public object? Data { get; init; }
	}

	public record NotifyAlexaMediaEverywhere2Parameters
	{
		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Title for your notification. eg: Your Garage Door Friend</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }

		///<summary>An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Extended information for notification. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("data")]
		public object? Data { get; init; }
	}

	public record NotifyAlexaMediaJaydenParameters
	{
		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Title for your notification. eg: Your Garage Door Friend</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }

		///<summary>An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Extended information for notification. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("data")]
		public object? Data { get; init; }
	}

	public record NotifyAlexaMediaKitchenParameters
	{
		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Title for your notification. eg: Your Garage Door Friend</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }

		///<summary>An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Extended information for notification. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("data")]
		public object? Data { get; init; }
	}

	public record NotifyAlexaMediaLastCalledParameters
	{
		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Title for your notification. eg: Your Garage Door Friend</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }

		///<summary>An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Extended information for notification. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("data")]
		public object? Data { get; init; }
	}

	public record NotifyAlexaMediaLoungeSonosParameters
	{
		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Title for your notification. eg: Your Garage Door Friend</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }

		///<summary>An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Extended information for notification. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("data")]
		public object? Data { get; init; }
	}

	public record NotifyAlexaMediaMasterParameters
	{
		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Title for your notification. eg: Your Garage Door Friend</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }

		///<summary>An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Extended information for notification. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("data")]
		public object? Data { get; init; }
	}

	public record NotifyAlexaMediaMasterTvAlexaParameters
	{
		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Title for your notification. eg: Your Garage Door Friend</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }

		///<summary>An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Extended information for notification. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("data")]
		public object? Data { get; init; }
	}

	public record NotifyAlexaMediaOfficeParameters
	{
		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Title for your notification. eg: Your Garage Door Friend</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }

		///<summary>An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Extended information for notification. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("data")]
		public object? Data { get; init; }
	}

	public record NotifyAlexaMediaPlayroomParameters
	{
		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Title for your notification. eg: Your Garage Door Friend</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }

		///<summary>An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Extended information for notification. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("data")]
		public object? Data { get; init; }
	}

	public record NotifyAlexaMediaSpareEchoParameters
	{
		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Title for your notification. eg: Your Garage Door Friend</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }

		///<summary>An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Extended information for notification. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("data")]
		public object? Data { get; init; }
	}

	public record NotifyAlexaMediaThisDeviceParameters
	{
		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Title for your notification. eg: Your Garage Door Friend</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }

		///<summary>An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Extended information for notification. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("data")]
		public object? Data { get; init; }
	}

	public record NotifyAlexaMediaThisDevice2Parameters
	{
		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Title for your notification. eg: Your Garage Door Friend</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }

		///<summary>An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Extended information for notification. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("data")]
		public object? Data { get; init; }
	}

	public record NotifyAlexaMediaUpstairsParameters
	{
		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Title for your notification. eg: Your Garage Door Friend</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }

		///<summary>An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Extended information for notification. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("data")]
		public object? Data { get; init; }
	}

	public record NotifyEugeneParameters
	{
		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Title for your notification. eg: Your Garage Door Friend</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }

		///<summary>An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Extended information for notification. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("data")]
		public object? Data { get; init; }
	}

	public record NotifyHaileyParameters
	{
		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Title for your notification. eg: Your Garage Door Friend</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }

		///<summary>An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Extended information for notification. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("data")]
		public object? Data { get; init; }
	}

	public record NotifyLoungeTvParameters
	{
		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Title for your notification. eg: Your Garage Door Friend</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }

		///<summary>An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Extended information for notification. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("data")]
		public object? Data { get; init; }
	}

	public record NotifyMasterTvParameters
	{
		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Title for your notification. eg: Your Garage Door Friend</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }

		///<summary>An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Extended information for notification. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("data")]
		public object? Data { get; init; }
	}

	public record NotifyMobileAppEugenesIphoneParameters
	{
		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Title for your notification. eg: Your Garage Door Friend</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }

		///<summary>An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Extended information for notification. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("data")]
		public object? Data { get; init; }
	}

	public record NotifyMobileAppHaileySIphoneParameters
	{
		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Title for your notification. eg: Your Garage Door Friend</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }

		///<summary>An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Extended information for notification. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("data")]
		public object? Data { get; init; }
	}

	public record NotifyMobileAppHaileysMacbookAirParameters
	{
		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Title for your notification. eg: Your Garage Door Friend</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }

		///<summary>An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Extended information for notification. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("data")]
		public object? Data { get; init; }
	}

	public record NotifyMobileAppIphoneParameters
	{
		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Title for your notification. eg: Your Garage Door Friend</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }

		///<summary>An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Extended information for notification. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("data")]
		public object? Data { get; init; }
	}

	public record NotifyNotifyParameters
	{
		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Title for your notification. eg: Your Garage Door Friend</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }

		///<summary>An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Extended information for notification. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("data")]
		public object? Data { get; init; }
	}

	public record NotifyPersistentNotificationParameters
	{
		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Title for your notification. eg: Your Garage Door Friend</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }
	}

	public record NotifyTwinsteadParameters
	{
		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Title for your notification. eg: Your Garage Door Friend</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }

		///<summary>An array of targets to send the notification to. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Extended information for notification. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("data")]
		public object? Data { get; init; }
	}

	public class NumberServices
	{
		private readonly IHaContext _haContext;
		public NumberServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Set the value of a Number entity.</summary>
		///<param name="target">The target for this service call</param>
		public void SetValue(ServiceTarget target, NumberSetValueParameters data)
		{
			_haContext.CallService("number", "set_value", target, data);
		}

		///<summary>Set the value of a Number entity.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="value">The target value the entity should be set to. eg: 42</param>
		public void SetValue(ServiceTarget target, string? @value = null)
		{
			_haContext.CallService("number", "set_value", target, new NumberSetValueParameters{Value = @value});
		}
	}

	public record NumberSetValueParameters
	{
		///<summary>The target value the entity should be set to. eg: 42</summary>
		[JsonPropertyName("value")]
		public string? Value { get; init; }
	}

	public class OctopusagileServices
	{
		private readonly IHaContext _haContext;
		public OctopusagileServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Update rates entity, switch entities on/off based on the timers entity.</summary>
		public void HalfHour()
		{
			_haContext.CallService("octopusagile", "half_hour", null);
		}

		///<summary>Get the most recent consumption data from the Octopus API</summary>
		public void UpdateConsumption()
		{
			_haContext.CallService("octopusagile", "update_consumption", null);
		}

		///<summary>Manually populate timers and moneymakers.</summary>
		public void UpdateTimers()
		{
			_haContext.CallService("octopusagile", "update_timers", null);
		}
	}

	public class PersistentNotificationServices
	{
		private readonly IHaContext _haContext;
		public PersistentNotificationServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Show a notification in the frontend.</summary>
		public void Create(PersistentNotificationCreateParameters data)
		{
			_haContext.CallService("persistent_notification", "create", null, data);
		}

		///<summary>Show a notification in the frontend.</summary>
		///<param name="message">Message body of the notification. [Templates accepted] eg: Please check your configuration.yaml.</param>
		///<param name="title">Optional title for your notification. [Templates accepted] eg: Test notification</param>
		///<param name="notificationId">Target ID of the notification, will replace a notification with the same ID. eg: 1234</param>
		public void Create(string @message, string? @title = null, string? @notificationId = null)
		{
			_haContext.CallService("persistent_notification", "create", null, new PersistentNotificationCreateParameters{Message = @message, Title = @title, NotificationId = @notificationId});
		}

		///<summary>Remove a notification from the frontend.</summary>
		public void Dismiss(PersistentNotificationDismissParameters data)
		{
			_haContext.CallService("persistent_notification", "dismiss", null, data);
		}

		///<summary>Remove a notification from the frontend.</summary>
		///<param name="notificationId">Target ID of the notification, which should be removed. eg: 1234</param>
		public void Dismiss(string @notificationId)
		{
			_haContext.CallService("persistent_notification", "dismiss", null, new PersistentNotificationDismissParameters{NotificationId = @notificationId});
		}

		///<summary>Mark a notification read.</summary>
		public void MarkRead(PersistentNotificationMarkReadParameters data)
		{
			_haContext.CallService("persistent_notification", "mark_read", null, data);
		}

		///<summary>Mark a notification read.</summary>
		///<param name="notificationId">Target ID of the notification, which should be mark read. eg: 1234</param>
		public void MarkRead(string @notificationId)
		{
			_haContext.CallService("persistent_notification", "mark_read", null, new PersistentNotificationMarkReadParameters{NotificationId = @notificationId});
		}
	}

	public record PersistentNotificationCreateParameters
	{
		///<summary>Message body of the notification. [Templates accepted] eg: Please check your configuration.yaml.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Optional title for your notification. [Templates accepted] eg: Test notification</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }

		///<summary>Target ID of the notification, will replace a notification with the same ID. eg: 1234</summary>
		[JsonPropertyName("notification_id")]
		public string? NotificationId { get; init; }
	}

	public record PersistentNotificationDismissParameters
	{
		///<summary>Target ID of the notification, which should be removed. eg: 1234</summary>
		[JsonPropertyName("notification_id")]
		public string? NotificationId { get; init; }
	}

	public record PersistentNotificationMarkReadParameters
	{
		///<summary>Target ID of the notification, which should be mark read. eg: 1234</summary>
		[JsonPropertyName("notification_id")]
		public string? NotificationId { get; init; }
	}

	public class PersonServices
	{
		private readonly IHaContext _haContext;
		public PersonServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Reload the person configuration.</summary>
		public void Reload()
		{
			_haContext.CallService("person", "reload", null);
		}
	}

	public class PiHoleServices
	{
		private readonly IHaContext _haContext;
		public PiHoleServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Disable configured Pi-hole(s) for an amount of time</summary>
		///<param name="target">The target for this service call</param>
		public void Disable(ServiceTarget target, PiHoleDisableParameters data)
		{
			_haContext.CallService("pi_hole", "disable", target, data);
		}

		///<summary>Disable configured Pi-hole(s) for an amount of time</summary>
		///<param name="target">The target for this service call</param>
		///<param name="duration">Time that the Pi-hole should be disabled for eg: 00:00:15</param>
		public void Disable(ServiceTarget target, string @duration)
		{
			_haContext.CallService("pi_hole", "disable", target, new PiHoleDisableParameters{Duration = @duration});
		}
	}

	public record PiHoleDisableParameters
	{
		///<summary>Time that the Pi-hole should be disabled for eg: 00:00:15</summary>
		[JsonPropertyName("duration")]
		public string? Duration { get; init; }
	}

	public class PingServices
	{
		private readonly IHaContext _haContext;
		public PingServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Reload all ping entities.</summary>
		public void Reload()
		{
			_haContext.CallService("ping", "reload", null);
		}
	}

	public class RecorderServices
	{
		private readonly IHaContext _haContext;
		public RecorderServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Stop the recording of events and state changes</summary>
		public void Disable()
		{
			_haContext.CallService("recorder", "disable", null);
		}

		///<summary>Start the recording of events and state changes</summary>
		public void Enable()
		{
			_haContext.CallService("recorder", "enable", null);
		}

		///<summary>Start purge task - to clean up old data from your database.</summary>
		public void Purge(RecorderPurgeParameters data)
		{
			_haContext.CallService("recorder", "purge", null, data);
		}

		///<summary>Start purge task - to clean up old data from your database.</summary>
		///<param name="keepDays">Number of history days to keep in database after purge.</param>
		///<param name="repack">Attempt to save disk space by rewriting the entire database file.</param>
		///<param name="applyFilter">Apply entity_id and event_type filter in addition to time based purge.</param>
		public void Purge(long? @keepDays = null, bool? @repack = null, bool? @applyFilter = null)
		{
			_haContext.CallService("recorder", "purge", null, new RecorderPurgeParameters{KeepDays = @keepDays, Repack = @repack, ApplyFilter = @applyFilter});
		}

		///<summary>Start purge task to remove specific entities from your database.</summary>
		///<param name="target">The target for this service call</param>
		public void PurgeEntities(ServiceTarget target, RecorderPurgeEntitiesParameters data)
		{
			_haContext.CallService("recorder", "purge_entities", target, data);
		}

		///<summary>Start purge task to remove specific entities from your database.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="domains">List the domains that need to be removed from the recorder database. eg: sun</param>
		///<param name="entityGlobs">List the regular expressions to select entities for removal from the recorder database. eg: domain*.object_id*</param>
		public void PurgeEntities(ServiceTarget target, object? @domains = null, object? @entityGlobs = null)
		{
			_haContext.CallService("recorder", "purge_entities", target, new RecorderPurgeEntitiesParameters{Domains = @domains, EntityGlobs = @entityGlobs});
		}
	}

	public record RecorderPurgeParameters
	{
		///<summary>Number of history days to keep in database after purge.</summary>
		[JsonPropertyName("keep_days")]
		public long? KeepDays { get; init; }

		///<summary>Attempt to save disk space by rewriting the entire database file.</summary>
		[JsonPropertyName("repack")]
		public bool? Repack { get; init; }

		///<summary>Apply entity_id and event_type filter in addition to time based purge.</summary>
		[JsonPropertyName("apply_filter")]
		public bool? ApplyFilter { get; init; }
	}

	public record RecorderPurgeEntitiesParameters
	{
		///<summary>List the domains that need to be removed from the recorder database. eg: sun</summary>
		[JsonPropertyName("domains")]
		public object? Domains { get; init; }

		///<summary>List the regular expressions to select entities for removal from the recorder database. eg: domain*.object_id*</summary>
		[JsonPropertyName("entity_globs")]
		public object? EntityGlobs { get; init; }
	}

	public class RemoteServices
	{
		private readonly IHaContext _haContext;
		public RemoteServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Deletes a command or a list of commands from the database.</summary>
		///<param name="target">The target for this service call</param>
		public void DeleteCommand(ServiceTarget target, RemoteDeleteCommandParameters data)
		{
			_haContext.CallService("remote", "delete_command", target, data);
		}

		///<summary>Deletes a command or a list of commands from the database.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="device">Name of the device from which commands will be deleted. eg: television</param>
		///<param name="command">A single command or a list of commands to delete. eg: Mute</param>
		public void DeleteCommand(ServiceTarget target, object @command, string? @device = null)
		{
			_haContext.CallService("remote", "delete_command", target, new RemoteDeleteCommandParameters{Device = @device, Command = @command});
		}

		///<summary>Learns a command or a list of commands from a device.</summary>
		///<param name="target">The target for this service call</param>
		public void LearnCommand(ServiceTarget target, RemoteLearnCommandParameters data)
		{
			_haContext.CallService("remote", "learn_command", target, data);
		}

		///<summary>Learns a command or a list of commands from a device.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="device">Device ID to learn command from. eg: television</param>
		///<param name="command">A single command or a list of commands to learn. eg: Turn on</param>
		///<param name="commandType">The type of command to be learned.</param>
		///<param name="alternative">If code must be stored as alternative (useful for discrete remotes).</param>
		///<param name="timeout">Timeout for the command to be learned.</param>
		public void LearnCommand(ServiceTarget target, string? @device = null, object? @command = null, string? @commandType = null, bool? @alternative = null, long? @timeout = null)
		{
			_haContext.CallService("remote", "learn_command", target, new RemoteLearnCommandParameters{Device = @device, Command = @command, CommandType = @commandType, Alternative = @alternative, Timeout = @timeout});
		}

		///<summary>Sends a command or a list of commands to a device.</summary>
		///<param name="target">The target for this service call</param>
		public void SendCommand(ServiceTarget target, RemoteSendCommandParameters data)
		{
			_haContext.CallService("remote", "send_command", target, data);
		}

		///<summary>Sends a command or a list of commands to a device.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="device">Device ID to send command to. eg: 32756745</param>
		///<param name="command">A single command or a list of commands to send. eg: Play</param>
		///<param name="numRepeats">The number of times you want to repeat the command(s).</param>
		///<param name="delaySecs">The time you want to wait in between repeated commands.</param>
		///<param name="holdSecs">The time you want to have it held before the release is send.</param>
		public void SendCommand(ServiceTarget target, string @command, string? @device = null, long? @numRepeats = null, double? @delaySecs = null, double? @holdSecs = null)
		{
			_haContext.CallService("remote", "send_command", target, new RemoteSendCommandParameters{Device = @device, Command = @command, NumRepeats = @numRepeats, DelaySecs = @delaySecs, HoldSecs = @holdSecs});
		}

		///<summary>Toggles a device.</summary>
		///<param name="target">The target for this service call</param>
		public void Toggle(ServiceTarget target)
		{
			_haContext.CallService("remote", "toggle", target);
		}

		///<summary>Sends the Power Off Command.</summary>
		///<param name="target">The target for this service call</param>
		public void TurnOff(ServiceTarget target)
		{
			_haContext.CallService("remote", "turn_off", target);
		}

		///<summary>Sends the Power On Command.</summary>
		///<param name="target">The target for this service call</param>
		public void TurnOn(ServiceTarget target, RemoteTurnOnParameters data)
		{
			_haContext.CallService("remote", "turn_on", target, data);
		}

		///<summary>Sends the Power On Command.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="activity">Activity ID or Activity Name to start. eg: BedroomTV</param>
		public void TurnOn(ServiceTarget target, string? @activity = null)
		{
			_haContext.CallService("remote", "turn_on", target, new RemoteTurnOnParameters{Activity = @activity});
		}
	}

	public record RemoteDeleteCommandParameters
	{
		///<summary>Name of the device from which commands will be deleted. eg: television</summary>
		[JsonPropertyName("device")]
		public string? Device { get; init; }

		///<summary>A single command or a list of commands to delete. eg: Mute</summary>
		[JsonPropertyName("command")]
		public object? Command { get; init; }
	}

	public record RemoteLearnCommandParameters
	{
		///<summary>Device ID to learn command from. eg: television</summary>
		[JsonPropertyName("device")]
		public string? Device { get; init; }

		///<summary>A single command or a list of commands to learn. eg: Turn on</summary>
		[JsonPropertyName("command")]
		public object? Command { get; init; }

		///<summary>The type of command to be learned.</summary>
		[JsonPropertyName("command_type")]
		public string? CommandType { get; init; }

		///<summary>If code must be stored as alternative (useful for discrete remotes).</summary>
		[JsonPropertyName("alternative")]
		public bool? Alternative { get; init; }

		///<summary>Timeout for the command to be learned.</summary>
		[JsonPropertyName("timeout")]
		public long? Timeout { get; init; }
	}

	public record RemoteSendCommandParameters
	{
		///<summary>Device ID to send command to. eg: 32756745</summary>
		[JsonPropertyName("device")]
		public string? Device { get; init; }

		///<summary>A single command or a list of commands to send. eg: Play</summary>
		[JsonPropertyName("command")]
		public string? Command { get; init; }

		///<summary>The number of times you want to repeat the command(s).</summary>
		[JsonPropertyName("num_repeats")]
		public long? NumRepeats { get; init; }

		///<summary>The time you want to wait in between repeated commands.</summary>
		[JsonPropertyName("delay_secs")]
		public double? DelaySecs { get; init; }

		///<summary>The time you want to have it held before the release is send.</summary>
		[JsonPropertyName("hold_secs")]
		public double? HoldSecs { get; init; }
	}

	public record RemoteTurnOnParameters
	{
		///<summary>Activity ID or Activity Name to start. eg: BedroomTV</summary>
		[JsonPropertyName("activity")]
		public string? Activity { get; init; }
	}

	public class RingServices
	{
		private readonly IHaContext _haContext;
		public RingServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Updates the data we have for all your ring devices</summary>
		public void Update()
		{
			_haContext.CallService("ring", "update", null);
		}
	}

	public class SceneServices
	{
		private readonly IHaContext _haContext;
		public SceneServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Activate a scene with configuration.</summary>
		public void Apply(SceneApplyParameters data)
		{
			_haContext.CallService("scene", "apply", null, data);
		}

		///<summary>Activate a scene with configuration.</summary>
		///<param name="entities">The entities and the state that they need to be. eg: {"light.kitchen": "on", "light.ceiling": {"state": "on", "brightness": 80}}</param>
		///<param name="transition">Transition duration it takes to bring devices to the state defined in the scene.</param>
		public void Apply(object @entities, long? @transition = null)
		{
			_haContext.CallService("scene", "apply", null, new SceneApplyParameters{Entities = @entities, Transition = @transition});
		}

		///<summary>Creates a new scene.</summary>
		public void Create(SceneCreateParameters data)
		{
			_haContext.CallService("scene", "create", null, data);
		}

		///<summary>Creates a new scene.</summary>
		///<param name="sceneId">The entity_id of the new scene. eg: all_lights</param>
		///<param name="entities">The entities to control with the scene. eg: {"light.tv_back_light": "on", "light.ceiling": {"state": "on", "brightness": 200}}</param>
		///<param name="snapshotEntities">The entities of which a snapshot is to be taken eg: ["light.ceiling", "light.kitchen"]</param>
		public void Create(string @sceneId, object? @entities = null, object? @snapshotEntities = null)
		{
			_haContext.CallService("scene", "create", null, new SceneCreateParameters{SceneId = @sceneId, Entities = @entities, SnapshotEntities = @snapshotEntities});
		}

		///<summary>Reload the scene configuration.</summary>
		public void Reload()
		{
			_haContext.CallService("scene", "reload", null);
		}

		///<summary>Activate a scene.</summary>
		///<param name="target">The target for this service call</param>
		public void TurnOn(ServiceTarget target, SceneTurnOnParameters data)
		{
			_haContext.CallService("scene", "turn_on", target, data);
		}

		///<summary>Activate a scene.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="transition">Transition duration it takes to bring devices to the state defined in the scene.</param>
		public void TurnOn(ServiceTarget target, long? @transition = null)
		{
			_haContext.CallService("scene", "turn_on", target, new SceneTurnOnParameters{Transition = @transition});
		}
	}

	public record SceneApplyParameters
	{
		///<summary>The entities and the state that they need to be. eg: {"light.kitchen": "on", "light.ceiling": {"state": "on", "brightness": 80}}</summary>
		[JsonPropertyName("entities")]
		public object? Entities { get; init; }

		///<summary>Transition duration it takes to bring devices to the state defined in the scene.</summary>
		[JsonPropertyName("transition")]
		public long? Transition { get; init; }
	}

	public record SceneCreateParameters
	{
		///<summary>The entity_id of the new scene. eg: all_lights</summary>
		[JsonPropertyName("scene_id")]
		public string? SceneId { get; init; }

		///<summary>The entities to control with the scene. eg: {"light.tv_back_light": "on", "light.ceiling": {"state": "on", "brightness": 200}}</summary>
		[JsonPropertyName("entities")]
		public object? Entities { get; init; }

		///<summary>The entities of which a snapshot is to be taken eg: ["light.ceiling", "light.kitchen"]</summary>
		[JsonPropertyName("snapshot_entities")]
		public object? SnapshotEntities { get; init; }
	}

	public record SceneTurnOnParameters
	{
		///<summary>Transition duration it takes to bring devices to the state defined in the scene.</summary>
		[JsonPropertyName("transition")]
		public long? Transition { get; init; }
	}

	public class SchedulerServices
	{
		private readonly IHaContext _haContext;
		public SchedulerServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Create a new schedule entity</summary>
		public void Add(SchedulerAddParameters data)
		{
			_haContext.CallService("scheduler", "add", null, data);
		}

		///<summary>Create a new schedule entity</summary>
		///<param name="weekdays">Days of the week for which the schedule should be repeated eg: ["daily"]</param>
		///<param name="startDate">Date from which schedule should be executed eg: ["2021-01-01"]</param>
		///<param name="endDate">Date until which schedule should be executed eg: ["2021-12-31"]</param>
		///<param name="timeslots">list of timeslots with their actions and optionally conditions (should be kept the same for all timeslots) eg: [{start: "12:00", stop: "13:00", actions: [{service: "light.turn_on", entity_id: "light.my_lamp", service_data: {brightness: 200}}]}]</param>
		///<param name="repeatType">Control what happens after the schedule is triggered eg: "repeat"</param>
		///<param name="name">Friendly name for the schedule eg: My schedule</param>
		public void Add(object @timeslots, string @repeatType, object? @weekdays = null, object? @startDate = null, object? @endDate = null, string? @name = null)
		{
			_haContext.CallService("scheduler", "add", null, new SchedulerAddParameters{Weekdays = @weekdays, StartDate = @startDate, EndDate = @endDate, Timeslots = @timeslots, RepeatType = @repeatType, Name = @name});
		}

		///<summary>Duplicate a schedule entity</summary>
		public void Copy(SchedulerCopyParameters data)
		{
			_haContext.CallService("scheduler", "copy", null, data);
		}

		///<summary>Duplicate a schedule entity</summary>
		///<param name="entityId">Identifier of the scheduler entity. eg: switch.schedule_abcdef</param>
		///<param name="name">Friendly name for the copied schedule eg: My schedule</param>
		public void Copy(string @entityId, string? @name = null)
		{
			_haContext.CallService("scheduler", "copy", null, new SchedulerCopyParameters{EntityId = @entityId, Name = @name});
		}

		///<summary>Edit a schedule entity</summary>
		public void Edit(SchedulerEditParameters data)
		{
			_haContext.CallService("scheduler", "edit", null, data);
		}

		///<summary>Edit a schedule entity</summary>
		///<param name="entityId">Identifier of the scheduler entity. eg: switch.schedule_abcdef</param>
		///<param name="weekdays">Days of the week for which the schedule should be repeated eg: ["daily"]</param>
		///<param name="startDate">Date from which schedule should be executed eg: ["2021-01-01"]</param>
		///<param name="endDate">Date until which schedule should be executed eg: ["2021-12-31"]</param>
		///<param name="timeslots">list of timeslots with their actions and optionally conditions (should be kept the same for all timeslots) eg: [{start: "12:00", stop: "13:00", actions: [{service: "light.turn_on", entity_id: "light.my_lamp", service_data: {brightness: 200}}]}]</param>
		///<param name="repeatType">Control what happens after the schedule is triggered eg: "repeat"</param>
		///<param name="name">Friendly name for the schedule eg: My schedule</param>
		public void Edit(string @entityId, object? @weekdays = null, object? @startDate = null, object? @endDate = null, object? @timeslots = null, string? @repeatType = null, string? @name = null)
		{
			_haContext.CallService("scheduler", "edit", null, new SchedulerEditParameters{EntityId = @entityId, Weekdays = @weekdays, StartDate = @startDate, EndDate = @endDate, Timeslots = @timeslots, RepeatType = @repeatType, Name = @name});
		}

		///<summary>Remove a schedule entity</summary>
		public void Remove(SchedulerRemoveParameters data)
		{
			_haContext.CallService("scheduler", "remove", null, data);
		}

		///<summary>Remove a schedule entity</summary>
		///<param name="entityId">Identifier of the scheduler entity. eg: switch.schedule_abcdef</param>
		public void Remove(string @entityId)
		{
			_haContext.CallService("scheduler", "remove", null, new SchedulerRemoveParameters{EntityId = @entityId});
		}

		///<summary>Execute the action of a schedule, optionally at a given time.</summary>
		public void RunAction(SchedulerRunActionParameters data)
		{
			_haContext.CallService("scheduler", "run_action", null, data);
		}

		///<summary>Execute the action of a schedule, optionally at a given time.</summary>
		///<param name="entityId">Identifier of the scheduler entity. eg: switch.schedule_abcdef</param>
		///<param name="time">Time for which to evaluate the action (only useful for schedules with multiple timeslot) eg: "12:00"</param>
		public void RunAction(string @entityId, DateTime? @time = null)
		{
			_haContext.CallService("scheduler", "run_action", null, new SchedulerRunActionParameters{EntityId = @entityId, Time = @time});
		}
	}

	public record SchedulerAddParameters
	{
		///<summary>Days of the week for which the schedule should be repeated eg: ["daily"]</summary>
		[JsonPropertyName("weekdays")]
		public object? Weekdays { get; init; }

		///<summary>Date from which schedule should be executed eg: ["2021-01-01"]</summary>
		[JsonPropertyName("start_date")]
		public object? StartDate { get; init; }

		///<summary>Date until which schedule should be executed eg: ["2021-12-31"]</summary>
		[JsonPropertyName("end_date")]
		public object? EndDate { get; init; }

		///<summary>list of timeslots with their actions and optionally conditions (should be kept the same for all timeslots) eg: [{start: "12:00", stop: "13:00", actions: [{service: "light.turn_on", entity_id: "light.my_lamp", service_data: {brightness: 200}}]}]</summary>
		[JsonPropertyName("timeslots")]
		public object? Timeslots { get; init; }

		///<summary>Control what happens after the schedule is triggered eg: "repeat"</summary>
		[JsonPropertyName("repeat_type")]
		public string? RepeatType { get; init; }

		///<summary>Friendly name for the schedule eg: My schedule</summary>
		[JsonPropertyName("name")]
		public string? Name { get; init; }
	}

	public record SchedulerCopyParameters
	{
		///<summary>Identifier of the scheduler entity. eg: switch.schedule_abcdef</summary>
		[JsonPropertyName("entity_id")]
		public string? EntityId { get; init; }

		///<summary>Friendly name for the copied schedule eg: My schedule</summary>
		[JsonPropertyName("name")]
		public string? Name { get; init; }
	}

	public record SchedulerEditParameters
	{
		///<summary>Identifier of the scheduler entity. eg: switch.schedule_abcdef</summary>
		[JsonPropertyName("entity_id")]
		public string? EntityId { get; init; }

		///<summary>Days of the week for which the schedule should be repeated eg: ["daily"]</summary>
		[JsonPropertyName("weekdays")]
		public object? Weekdays { get; init; }

		///<summary>Date from which schedule should be executed eg: ["2021-01-01"]</summary>
		[JsonPropertyName("start_date")]
		public object? StartDate { get; init; }

		///<summary>Date until which schedule should be executed eg: ["2021-12-31"]</summary>
		[JsonPropertyName("end_date")]
		public object? EndDate { get; init; }

		///<summary>list of timeslots with their actions and optionally conditions (should be kept the same for all timeslots) eg: [{start: "12:00", stop: "13:00", actions: [{service: "light.turn_on", entity_id: "light.my_lamp", service_data: {brightness: 200}}]}]</summary>
		[JsonPropertyName("timeslots")]
		public object? Timeslots { get; init; }

		///<summary>Control what happens after the schedule is triggered eg: "repeat"</summary>
		[JsonPropertyName("repeat_type")]
		public string? RepeatType { get; init; }

		///<summary>Friendly name for the schedule eg: My schedule</summary>
		[JsonPropertyName("name")]
		public string? Name { get; init; }
	}

	public record SchedulerRemoveParameters
	{
		///<summary>Identifier of the scheduler entity. eg: switch.schedule_abcdef</summary>
		[JsonPropertyName("entity_id")]
		public string? EntityId { get; init; }
	}

	public record SchedulerRunActionParameters
	{
		///<summary>Identifier of the scheduler entity. eg: switch.schedule_abcdef</summary>
		[JsonPropertyName("entity_id")]
		public string? EntityId { get; init; }

		///<summary>Time for which to evaluate the action (only useful for schedules with multiple timeslot) eg: "12:00"</summary>
		[JsonPropertyName("time")]
		public DateTime? Time { get; init; }
	}

	public class ScriptServices
	{
		private readonly IHaContext _haContext;
		public ScriptServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Activates an actionable notification on a specific echo device</summary>
		public void ActivateAlexaActionableNotification(ScriptActivateAlexaActionableNotificationParameters data)
		{
			_haContext.CallService("script", "activate_alexa_actionable_notification", null, data);
		}

		///<summary>Activates an actionable notification on a specific echo device</summary>
		///<param name="text">The text you would like alexa to speak. eg: What would you like the thermostat set to?</param>
		///<param name="eventId">Correlation ID for event responses eg: ask_for_temperature</param>
		///<param name="alexaDevice">Alexa device you want to trigger eg: media_player.bedroom_echo</param>
		public void ActivateAlexaActionableNotification(object? @text = null, object? @eventId = null, object? @alexaDevice = null)
		{
			_haContext.CallService("script", "activate_alexa_actionable_notification", null, new ScriptActivateAlexaActionableNotificationParameters{Text = @text, EventId = @eventId, AlexaDevice = @alexaDevice});
		}

		public void ArriveHome()
		{
			_haContext.CallService("script", "arrive_home", null);
		}

		public void ImText()
		{
			_haContext.CallService("script", "im_text", null);
		}

		///<summary>Reload all the available scripts</summary>
		public void Reload()
		{
			_haContext.CallService("script", "reload", null);
		}

		public void RingMqttInterval()
		{
			_haContext.CallService("script", "ring_mqtt_interval", null);
		}

		///<summary>Toggle script</summary>
		///<param name="target">The target for this service call</param>
		public void Toggle(ServiceTarget target)
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

		///<summary>Turn off script</summary>
		///<param name="target">The target for this service call</param>
		public void TurnOff(ServiceTarget target)
		{
			_haContext.CallService("script", "turn_off", target);
		}

		///<summary>Turn on script</summary>
		///<param name="target">The target for this service call</param>
		public void TurnOn(ServiceTarget target)
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
		///<summary>The text you would like alexa to speak. eg: What would you like the thermostat set to?</summary>
		[JsonPropertyName("text")]
		public object? Text { get; init; }

		///<summary>Correlation ID for event responses eg: ask_for_temperature</summary>
		[JsonPropertyName("event_id")]
		public object? EventId { get; init; }

		///<summary>Alexa device you want to trigger eg: media_player.bedroom_echo</summary>
		[JsonPropertyName("alexa_device")]
		public object? AlexaDevice { get; init; }
	}

	public class SelectServices
	{
		private readonly IHaContext _haContext;
		public SelectServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Select an option of an select entity.</summary>
		///<param name="target">The target for this service call</param>
		public void SelectOption(ServiceTarget target, SelectSelectOptionParameters data)
		{
			_haContext.CallService("select", "select_option", target, data);
		}

		///<summary>Select an option of an select entity.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="option">Option to be selected. eg: "Item A"</param>
		public void SelectOption(ServiceTarget target, string @option)
		{
			_haContext.CallService("select", "select_option", target, new SelectSelectOptionParameters{Option = @option});
		}
	}

	public record SelectSelectOptionParameters
	{
		///<summary>Option to be selected. eg: "Item A"</summary>
		[JsonPropertyName("option")]
		public string? Option { get; init; }
	}

	public class SirenServices
	{
		private readonly IHaContext _haContext;
		public SirenServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Toggles a siren.</summary>
		///<param name="target">The target for this service call</param>
		public void Toggle(ServiceTarget target)
		{
			_haContext.CallService("siren", "toggle", target);
		}

		///<summary>Turn siren off.</summary>
		///<param name="target">The target for this service call</param>
		public void TurnOff(ServiceTarget target)
		{
			_haContext.CallService("siren", "turn_off", target);
		}

		///<summary>Turn siren on.</summary>
		///<param name="target">The target for this service call</param>
		public void TurnOn(ServiceTarget target, SirenTurnOnParameters data)
		{
			_haContext.CallService("siren", "turn_on", target, data);
		}

		///<summary>Turn siren on.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="tone">The tone to emit when turning the siren on. When `available_tones` property is a map, either the key or the value can be used. Must be supported by the integration. eg: fire</param>
		///<param name="volumeLevel">The volume level of the noise to emit when turning the siren on. Must be supported by the integration. eg: 0.5</param>
		///<param name="duration">The duration in seconds of the noise to emit when turning the siren on. Must be supported by the integration. eg: 15</param>
		public void TurnOn(ServiceTarget target, string? @tone = null, double? @volumeLevel = null, string? @duration = null)
		{
			_haContext.CallService("siren", "turn_on", target, new SirenTurnOnParameters{Tone = @tone, VolumeLevel = @volumeLevel, Duration = @duration});
		}
	}

	public record SirenTurnOnParameters
	{
		///<summary>The tone to emit when turning the siren on. When `available_tones` property is a map, either the key or the value can be used. Must be supported by the integration. eg: fire</summary>
		[JsonPropertyName("tone")]
		public string? Tone { get; init; }

		///<summary>The volume level of the noise to emit when turning the siren on. Must be supported by the integration. eg: 0.5</summary>
		[JsonPropertyName("volume_level")]
		public double? VolumeLevel { get; init; }

		///<summary>The duration in seconds of the noise to emit when turning the siren on. Must be supported by the integration. eg: 15</summary>
		[JsonPropertyName("duration")]
		public string? Duration { get; init; }
	}

	public class SonosServices
	{
		private readonly IHaContext _haContext;
		public SonosServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Clear a Sonos timer.</summary>
		///<param name="target">The target for this service call</param>
		public void ClearSleepTimer(ServiceTarget target)
		{
			_haContext.CallService("sonos", "clear_sleep_timer", target);
		}

		///<summary>Group player together.</summary>
		public void Join(SonosJoinParameters data)
		{
			_haContext.CallService("sonos", "join", null, data);
		}

		///<summary>Group player together.</summary>
		///<param name="master">Entity ID of the player that should become the coordinator of the group.</param>
		///<param name="entityId">Name of entity that will join the master.</param>
		public void Join(string @master, string @entityId)
		{
			_haContext.CallService("sonos", "join", null, new SonosJoinParameters{Master = @master, EntityId = @entityId});
		}

		///<summary>Start playing the queue from the first item.</summary>
		///<param name="target">The target for this service call</param>
		public void PlayQueue(ServiceTarget target, SonosPlayQueueParameters data)
		{
			_haContext.CallService("sonos", "play_queue", target, data);
		}

		///<summary>Start playing the queue from the first item.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="queuePosition">Position of the song in the queue to start playing from.</param>
		public void PlayQueue(ServiceTarget target, long? @queuePosition = null)
		{
			_haContext.CallService("sonos", "play_queue", target, new SonosPlayQueueParameters{QueuePosition = @queuePosition});
		}

		///<summary>Removes an item from the queue.</summary>
		///<param name="target">The target for this service call</param>
		public void RemoveFromQueue(ServiceTarget target, SonosRemoveFromQueueParameters data)
		{
			_haContext.CallService("sonos", "remove_from_queue", target, data);
		}

		///<summary>Removes an item from the queue.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="queuePosition">Position in the queue to remove.</param>
		public void RemoveFromQueue(ServiceTarget target, long? @queuePosition = null)
		{
			_haContext.CallService("sonos", "remove_from_queue", target, new SonosRemoveFromQueueParameters{QueuePosition = @queuePosition});
		}

		///<summary>Restore a snapshot of the media player.</summary>
		public void Restore(SonosRestoreParameters data)
		{
			_haContext.CallService("sonos", "restore", null, data);
		}

		///<summary>Restore a snapshot of the media player.</summary>
		///<param name="entityId">Name of entity that will be restored.</param>
		///<param name="withGroup">True or False. Also restore the group layout.</param>
		public void Restore(string? @entityId = null, bool? @withGroup = null)
		{
			_haContext.CallService("sonos", "restore", null, new SonosRestoreParameters{EntityId = @entityId, WithGroup = @withGroup});
		}

		///<summary>Set a Sonos timer.</summary>
		///<param name="target">The target for this service call</param>
		public void SetSleepTimer(ServiceTarget target, SonosSetSleepTimerParameters data)
		{
			_haContext.CallService("sonos", "set_sleep_timer", target, data);
		}

		///<summary>Set a Sonos timer.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="sleepTime">Number of seconds to set the timer.</param>
		public void SetSleepTimer(ServiceTarget target, long? @sleepTime = null)
		{
			_haContext.CallService("sonos", "set_sleep_timer", target, new SonosSetSleepTimerParameters{SleepTime = @sleepTime});
		}

		///<summary>Take a snapshot of the media player.</summary>
		public void Snapshot(SonosSnapshotParameters data)
		{
			_haContext.CallService("sonos", "snapshot", null, data);
		}

		///<summary>Take a snapshot of the media player.</summary>
		///<param name="entityId">Name of entity that will be snapshot.</param>
		///<param name="withGroup">True or False. Also snapshot the group layout.</param>
		public void Snapshot(string? @entityId = null, bool? @withGroup = null)
		{
			_haContext.CallService("sonos", "snapshot", null, new SonosSnapshotParameters{EntityId = @entityId, WithGroup = @withGroup});
		}

		///<summary>Unjoin the player from a group.</summary>
		///<param name="target">The target for this service call</param>
		public void Unjoin(ServiceTarget target)
		{
			_haContext.CallService("sonos", "unjoin", target);
		}

		///<summary>Updates an alarm with new time and volume settings.</summary>
		///<param name="target">The target for this service call</param>
		public void UpdateAlarm(ServiceTarget target, SonosUpdateAlarmParameters data)
		{
			_haContext.CallService("sonos", "update_alarm", target, data);
		}

		///<summary>Updates an alarm with new time and volume settings.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="alarmId">ID for the alarm to be updated.</param>
		///<param name="time">Set time for the alarm. eg: 07:00</param>
		///<param name="volume">Set alarm volume level.</param>
		///<param name="enabled">Enable or disable the alarm.</param>
		///<param name="includeLinkedZones">Enable or disable including grouped rooms.</param>
		public void UpdateAlarm(ServiceTarget target, long @alarmId, DateTime? @time = null, double? @volume = null, bool? @enabled = null, bool? @includeLinkedZones = null)
		{
			_haContext.CallService("sonos", "update_alarm", target, new SonosUpdateAlarmParameters{AlarmId = @alarmId, Time = @time, Volume = @volume, Enabled = @enabled, IncludeLinkedZones = @includeLinkedZones});
		}
	}

	public record SonosJoinParameters
	{
		///<summary>Entity ID of the player that should become the coordinator of the group.</summary>
		[JsonPropertyName("master")]
		public string? Master { get; init; }

		///<summary>Name of entity that will join the master.</summary>
		[JsonPropertyName("entity_id")]
		public string? EntityId { get; init; }
	}

	public record SonosPlayQueueParameters
	{
		///<summary>Position of the song in the queue to start playing from.</summary>
		[JsonPropertyName("queue_position")]
		public long? QueuePosition { get; init; }
	}

	public record SonosRemoveFromQueueParameters
	{
		///<summary>Position in the queue to remove.</summary>
		[JsonPropertyName("queue_position")]
		public long? QueuePosition { get; init; }
	}

	public record SonosRestoreParameters
	{
		///<summary>Name of entity that will be restored.</summary>
		[JsonPropertyName("entity_id")]
		public string? EntityId { get; init; }

		///<summary>True or False. Also restore the group layout.</summary>
		[JsonPropertyName("with_group")]
		public bool? WithGroup { get; init; }
	}

	public record SonosSetSleepTimerParameters
	{
		///<summary>Number of seconds to set the timer.</summary>
		[JsonPropertyName("sleep_time")]
		public long? SleepTime { get; init; }
	}

	public record SonosSnapshotParameters
	{
		///<summary>Name of entity that will be snapshot.</summary>
		[JsonPropertyName("entity_id")]
		public string? EntityId { get; init; }

		///<summary>True or False. Also snapshot the group layout.</summary>
		[JsonPropertyName("with_group")]
		public bool? WithGroup { get; init; }
	}

	public record SonosUpdateAlarmParameters
	{
		///<summary>ID for the alarm to be updated.</summary>
		[JsonPropertyName("alarm_id")]
		public long? AlarmId { get; init; }

		///<summary>Set time for the alarm. eg: 07:00</summary>
		[JsonPropertyName("time")]
		public DateTime? Time { get; init; }

		///<summary>Set alarm volume level.</summary>
		[JsonPropertyName("volume")]
		public double? Volume { get; init; }

		///<summary>Enable or disable the alarm.</summary>
		[JsonPropertyName("enabled")]
		public bool? Enabled { get; init; }

		///<summary>Enable or disable including grouped rooms.</summary>
		[JsonPropertyName("include_linked_zones")]
		public bool? IncludeLinkedZones { get; init; }
	}

	public class SpeedtestdotnetServices
	{
		private readonly IHaContext _haContext;
		public SpeedtestdotnetServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Immediately execute a speed test with Speedtest.net</summary>
		public void Speedtest()
		{
			_haContext.CallService("speedtestdotnet", "speedtest", null);
		}
	}

	public class StatisticsServices
	{
		private readonly IHaContext _haContext;
		public StatisticsServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Reload all statistics entities.</summary>
		public void Reload()
		{
			_haContext.CallService("statistics", "reload", null);
		}
	}

	public class SwitchServices
	{
		private readonly IHaContext _haContext;
		public SwitchServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Toggles a switch state</summary>
		///<param name="target">The target for this service call</param>
		public void Toggle(ServiceTarget target)
		{
			_haContext.CallService("switch", "toggle", target);
		}

		///<summary>Turn a switch off</summary>
		///<param name="target">The target for this service call</param>
		public void TurnOff(ServiceTarget target)
		{
			_haContext.CallService("switch", "turn_off", target);
		}

		///<summary>Turn a switch on</summary>
		///<param name="target">The target for this service call</param>
		public void TurnOn(ServiceTarget target)
		{
			_haContext.CallService("switch", "turn_on", target);
		}
	}

	public class SystemLogServices
	{
		private readonly IHaContext _haContext;
		public SystemLogServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Clear all log entries.</summary>
		public void Clear()
		{
			_haContext.CallService("system_log", "clear", null);
		}

		///<summary>Write log entry.</summary>
		public void Write(SystemLogWriteParameters data)
		{
			_haContext.CallService("system_log", "write", null, data);
		}

		///<summary>Write log entry.</summary>
		///<param name="message">Message to log. eg: Something went wrong</param>
		///<param name="level">Log level.</param>
		///<param name="logger">Logger name under which to log the message. Defaults to 'system_log.external'. eg: mycomponent.myplatform</param>
		public void Write(string @message, string? @level = null, string? @logger = null)
		{
			_haContext.CallService("system_log", "write", null, new SystemLogWriteParameters{Message = @message, Level = @level, Logger = @logger});
		}
	}

	public record SystemLogWriteParameters
	{
		///<summary>Message to log. eg: Something went wrong</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Log level.</summary>
		[JsonPropertyName("level")]
		public string? Level { get; init; }

		///<summary>Logger name under which to log the message. Defaults to 'system_log.external'. eg: mycomponent.myplatform</summary>
		[JsonPropertyName("logger")]
		public string? Logger { get; init; }
	}

	public class TelegramServices
	{
		private readonly IHaContext _haContext;
		public TelegramServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Reload telegram notify services.</summary>
		public void Reload()
		{
			_haContext.CallService("telegram", "reload", null);
		}
	}

	public class TelegramBotServices
	{
		private readonly IHaContext _haContext;
		public TelegramBotServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Respond to a callback query originated by clicking on an online keyboard button. The answer will be displayed to the user as a notification at the top of the chat screen or as an alert.</summary>
		public void AnswerCallbackQuery(TelegramBotAnswerCallbackQueryParameters data)
		{
			_haContext.CallService("telegram_bot", "answer_callback_query", null, data);
		}

		///<summary>Respond to a callback query originated by clicking on an online keyboard button. The answer will be displayed to the user as a notification at the top of the chat screen or as an alert.</summary>
		///<param name="message">Unformatted text message body of the notification. eg: OK, I'm listening</param>
		///<param name="callbackQueryId">Unique id of the callback response. eg: {{ trigger.event.data.id }}</param>
		///<param name="showAlert">Show a permanent notification.</param>
		///<param name="timeout">Timeout for sending the answer. Will help with timeout errors (poor internet connection, etc)</param>
		public void AnswerCallbackQuery(string @message, string @callbackQueryId, bool @showAlert, long? @timeout = null)
		{
			_haContext.CallService("telegram_bot", "answer_callback_query", null, new TelegramBotAnswerCallbackQueryParameters{Message = @message, CallbackQueryId = @callbackQueryId, ShowAlert = @showAlert, Timeout = @timeout});
		}

		///<summary>Delete a previously sent message.</summary>
		public void DeleteMessage(TelegramBotDeleteMessageParameters data)
		{
			_haContext.CallService("telegram_bot", "delete_message", null, data);
		}

		///<summary>Delete a previously sent message.</summary>
		///<param name="messageId">id of the message to delete. eg: {{ trigger.event.data.message.message_id }}</param>
		///<param name="chatId">The chat_id where to delete the message. eg: 12345</param>
		public void DeleteMessage(string @messageId, string @chatId)
		{
			_haContext.CallService("telegram_bot", "delete_message", null, new TelegramBotDeleteMessageParameters{MessageId = @messageId, ChatId = @chatId});
		}

		///<summary>Edit the caption of a previously sent message.</summary>
		public void EditCaption(TelegramBotEditCaptionParameters data)
		{
			_haContext.CallService("telegram_bot", "edit_caption", null, data);
		}

		///<summary>Edit the caption of a previously sent message.</summary>
		///<param name="messageId">id of the message to edit. eg: {{ trigger.event.data.message.message_id }}</param>
		///<param name="chatId">The chat_id where to edit the caption. eg: 12345</param>
		///<param name="caption">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="inlineKeyboard">List of rows of commands, comma-separated, to make a custom inline keyboard with buttons with associated callback data. eg: ["/button1, /button2", "/button3"] or [[["Text button1", "/button1"], ["Text button2", "/button2"]], [["Text button3", "/button3"]]]</param>
		public void EditCaption(string @messageId, string @chatId, string @caption, object? @inlineKeyboard = null)
		{
			_haContext.CallService("telegram_bot", "edit_caption", null, new TelegramBotEditCaptionParameters{MessageId = @messageId, ChatId = @chatId, Caption = @caption, InlineKeyboard = @inlineKeyboard});
		}

		///<summary>Edit a previously sent message.</summary>
		public void EditMessage(TelegramBotEditMessageParameters data)
		{
			_haContext.CallService("telegram_bot", "edit_message", null, data);
		}

		///<summary>Edit a previously sent message.</summary>
		///<param name="messageId">id of the message to edit. eg: {{ trigger.event.data.message.message_id }}</param>
		///<param name="chatId">The chat_id where to edit the message. eg: 12345</param>
		///<param name="message">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="title">Optional title for your notification. Will be composed as '%title\n%message' eg: Your Garage Door Friend</param>
		///<param name="parseMode">Parser for the message text.</param>
		///<param name="disableWebPagePreview">Disables link previews for links in the message.</param>
		///<param name="inlineKeyboard">List of rows of commands, comma-separated, to make a custom inline keyboard with buttons with associated callback data. eg: ["/button1, /button2", "/button3"] or [[["Text button1", "/button1"], ["Text button2", "/button2"]], [["Text button3", "/button3"]]]</param>
		public void EditMessage(string @messageId, string @chatId, string? @message = null, string? @title = null, string? @parseMode = null, bool? @disableWebPagePreview = null, object? @inlineKeyboard = null)
		{
			_haContext.CallService("telegram_bot", "edit_message", null, new TelegramBotEditMessageParameters{MessageId = @messageId, ChatId = @chatId, Message = @message, Title = @title, ParseMode = @parseMode, DisableWebPagePreview = @disableWebPagePreview, InlineKeyboard = @inlineKeyboard});
		}

		///<summary>Edit the inline keyboard of a previously sent message.</summary>
		public void EditReplymarkup(TelegramBotEditReplymarkupParameters data)
		{
			_haContext.CallService("telegram_bot", "edit_replymarkup", null, data);
		}

		///<summary>Edit the inline keyboard of a previously sent message.</summary>
		///<param name="messageId">id of the message to edit. eg: {{ trigger.event.data.message.message_id }}</param>
		///<param name="chatId">The chat_id where to edit the reply_markup. eg: 12345</param>
		///<param name="inlineKeyboard">List of rows of commands, comma-separated, to make a custom inline keyboard with buttons with associated callback data. eg: ["/button1, /button2", "/button3"] or [[["Text button1", "/button1"], ["Text button2", "/button2"]], [["Text button3", "/button3"]]]</param>
		public void EditReplymarkup(string @messageId, string @chatId, object @inlineKeyboard)
		{
			_haContext.CallService("telegram_bot", "edit_replymarkup", null, new TelegramBotEditReplymarkupParameters{MessageId = @messageId, ChatId = @chatId, InlineKeyboard = @inlineKeyboard});
		}

		public void LeaveChat()
		{
			_haContext.CallService("telegram_bot", "leave_chat", null);
		}

		///<summary>Send an anmiation.</summary>
		public void SendAnimation(TelegramBotSendAnimationParameters data)
		{
			_haContext.CallService("telegram_bot", "send_animation", null, data);
		}

		///<summary>Send an anmiation.</summary>
		///<param name="url">Remote path to a GIF or H.264/MPEG-4 AVC video without sound. eg: http://example.org/path/to/the/animation.gif</param>
		///<param name="file">Local path to a GIF or H.264/MPEG-4 AVC video without sound. eg: /path/to/the/animation.gif</param>
		///<param name="caption">The title of the animation. eg: My animation</param>
		///<param name="username">Username for a URL which require HTTP authentication. eg: myuser</param>
		///<param name="password">Password (or bearer token) for a URL which require HTTP authentication. eg: myuser_pwd</param>
		///<param name="authentication">Define which authentication method to use. Set to `digest` to use HTTP digest authentication, or `bearer_token` for OAuth 2.0 bearer token authentication. Defaults to `basic`.</param>
		///<param name="target">An array of pre-authorized chat_ids to send the document to. If not present, first allowed chat_id is the default. eg: [12345, 67890] or 12345</param>
		///<param name="parseMode">Parser for the message text.</param>
		///<param name="disableNotification">Sends the message silently. iOS users and Web users will not receive a notification, Android users will receive a notification with no sound.</param>
		///<param name="verifySsl">Enable or disable SSL certificate verification. Set to false if you're downloading the file from a URL and you don't want to validate the SSL certificate of the server.</param>
		///<param name="timeout">Timeout for send sticker. Will help with timeout errors (poor internet connection, etc)</param>
		///<param name="keyboard">List of rows of commands, comma-separated, to make a custom keyboard. eg: ["/command1, /command2", "/command3"]</param>
		///<param name="inlineKeyboard">List of rows of commands, comma-separated, to make a custom inline keyboard with buttons with associated callback data. eg: ["/button1, /button2", "/button3"] or [[["Text button1", "/button1"], ["Text button2", "/button2"]], [["Text button3", "/button3"]]]</param>
		public void SendAnimation(string? @url = null, string? @file = null, string? @caption = null, string? @username = null, string? @password = null, string? @authentication = null, object? @target = null, string? @parseMode = null, bool? @disableNotification = null, bool? @verifySsl = null, long? @timeout = null, object? @keyboard = null, object? @inlineKeyboard = null)
		{
			_haContext.CallService("telegram_bot", "send_animation", null, new TelegramBotSendAnimationParameters{Url = @url, File = @file, Caption = @caption, Username = @username, Password = @password, Authentication = @authentication, Target = @target, ParseMode = @parseMode, DisableNotification = @disableNotification, VerifySsl = @verifySsl, Timeout = @timeout, Keyboard = @keyboard, InlineKeyboard = @inlineKeyboard});
		}

		///<summary>Send a document.</summary>
		public void SendDocument(TelegramBotSendDocumentParameters data)
		{
			_haContext.CallService("telegram_bot", "send_document", null, data);
		}

		///<summary>Send a document.</summary>
		///<param name="url">Remote path to a document. eg: http://example.org/path/to/the/document.odf</param>
		///<param name="file">Local path to a document. eg: /tmp/whatever.odf</param>
		///<param name="caption">The title of the document. eg: Document Title xy</param>
		///<param name="username">Username for a URL which require HTTP authentication. eg: myuser</param>
		///<param name="password">Password (or bearer token) for a URL which require HTTP authentication. eg: myuser_pwd</param>
		///<param name="authentication">Define which authentication method to use. Set to `digest` to use HTTP digest authentication, or `bearer_token` for OAuth 2.0 bearer token authentication. Defaults to `basic`.</param>
		///<param name="target">An array of pre-authorized chat_ids to send the document to. If not present, first allowed chat_id is the default. eg: [12345, 67890] or 12345</param>
		///<param name="parseMode">Parser for the message text.</param>
		///<param name="disableNotification">Sends the message silently. iOS users and Web users will not receive a notification, Android users will receive a notification with no sound.</param>
		///<param name="verifySsl">Enable or disable SSL certificate verification. Set to false if you're downloading the file from a URL and you don't want to validate the SSL certificate of the server.</param>
		///<param name="timeout">Timeout for send document. Will help with timeout errors (poor internet connection, etc)</param>
		///<param name="keyboard">List of rows of commands, comma-separated, to make a custom keyboard. eg: ["/command1, /command2", "/command3"]</param>
		///<param name="inlineKeyboard">List of rows of commands, comma-separated, to make a custom inline keyboard with buttons with associated callback data. eg: ["/button1, /button2", "/button3"] or [[["Text button1", "/button1"], ["Text button2", "/button2"]], [["Text button3", "/button3"]]]</param>
		///<param name="messageTag">Tag for sent message. In telegram_sent event data: {{trigger.event.data.message_tag}} eg: msg_to_edit</param>
		public void SendDocument(string? @url = null, string? @file = null, string? @caption = null, string? @username = null, string? @password = null, string? @authentication = null, object? @target = null, string? @parseMode = null, bool? @disableNotification = null, bool? @verifySsl = null, long? @timeout = null, object? @keyboard = null, object? @inlineKeyboard = null, string? @messageTag = null)
		{
			_haContext.CallService("telegram_bot", "send_document", null, new TelegramBotSendDocumentParameters{Url = @url, File = @file, Caption = @caption, Username = @username, Password = @password, Authentication = @authentication, Target = @target, ParseMode = @parseMode, DisableNotification = @disableNotification, VerifySsl = @verifySsl, Timeout = @timeout, Keyboard = @keyboard, InlineKeyboard = @inlineKeyboard, MessageTag = @messageTag});
		}

		///<summary>Send a location.</summary>
		public void SendLocation(TelegramBotSendLocationParameters data)
		{
			_haContext.CallService("telegram_bot", "send_location", null, data);
		}

		///<summary>Send a location.</summary>
		///<param name="latitude">The latitude to send.</param>
		///<param name="longitude">The longitude to send.</param>
		///<param name="target">An array of pre-authorized chat_ids to send the location to. If not present, first allowed chat_id is the default. eg: [12345, 67890] or 12345</param>
		///<param name="disableNotification">Sends the message silently. iOS users and Web users will not receive a notification, Android users will receive a notification with no sound.</param>
		///<param name="timeout">Timeout for send photo. Will help with timeout errors (poor internet connection, etc)</param>
		///<param name="keyboard">List of rows of commands, comma-separated, to make a custom keyboard. eg: ["/command1, /command2", "/command3"]</param>
		///<param name="inlineKeyboard">List of rows of commands, comma-separated, to make a custom inline keyboard with buttons with associated callback data. eg: ["/button1, /button2", "/button3"] or [[["Text button1", "/button1"], ["Text button2", "/button2"]], [["Text button3", "/button3"]]]</param>
		///<param name="messageTag">Tag for sent message. In telegram_sent event data: {{trigger.event.data.message_tag}} eg: msg_to_edit</param>
		public void SendLocation(double @latitude, double @longitude, object? @target = null, bool? @disableNotification = null, long? @timeout = null, object? @keyboard = null, object? @inlineKeyboard = null, string? @messageTag = null)
		{
			_haContext.CallService("telegram_bot", "send_location", null, new TelegramBotSendLocationParameters{Latitude = @latitude, Longitude = @longitude, Target = @target, DisableNotification = @disableNotification, Timeout = @timeout, Keyboard = @keyboard, InlineKeyboard = @inlineKeyboard, MessageTag = @messageTag});
		}

		///<summary>Send a notification.</summary>
		public void SendMessage(TelegramBotSendMessageParameters data)
		{
			_haContext.CallService("telegram_bot", "send_message", null, data);
		}

		///<summary>Send a notification.</summary>
		///<param name="message">Message body of the notification. eg: The garage door has been open for 10 minutes.</param>
		///<param name="title">Optional title for your notification. Will be composed as '%title\n%message' eg: Your Garage Door Friend</param>
		///<param name="target">An array of pre-authorized chat_ids to send the notification to. If not present, first allowed chat_id is the default. eg: [12345, 67890] or 12345</param>
		///<param name="parseMode">Parser for the message text.</param>
		///<param name="disableNotification">Sends the message silently. iOS users and Web users will not receive a notification, Android users will receive a notification with no sound.</param>
		///<param name="disableWebPagePreview">Disables link previews for links in the message.</param>
		///<param name="timeout">Timeout for send message. Will help with timeout errors (poor internet connection, etc)s</param>
		///<param name="keyboard">List of rows of commands, comma-separated, to make a custom keyboard. Empty list clears a previously set keyboard. eg: ["/command1, /command2", "/command3"]</param>
		///<param name="inlineKeyboard">List of rows of commands, comma-separated, to make a custom inline keyboard with buttons with associated callback data. eg: ["/button1, /button2", "/button3"] or ["Text button1:/button1, Text button2:/button2", "Text button3:/button3"] or [[["Text button1", "/button1"], ["Text button2", "/button2"]], [["Text button3", "/button3"]]]</param>
		///<param name="messageTag">Tag for sent message. In telegram_sent event data: {{trigger.event.data.message_tag}} eg: msg_to_edit</param>
		public void SendMessage(string @message, string? @title = null, object? @target = null, string? @parseMode = null, bool? @disableNotification = null, bool? @disableWebPagePreview = null, long? @timeout = null, object? @keyboard = null, object? @inlineKeyboard = null, string? @messageTag = null)
		{
			_haContext.CallService("telegram_bot", "send_message", null, new TelegramBotSendMessageParameters{Message = @message, Title = @title, Target = @target, ParseMode = @parseMode, DisableNotification = @disableNotification, DisableWebPagePreview = @disableWebPagePreview, Timeout = @timeout, Keyboard = @keyboard, InlineKeyboard = @inlineKeyboard, MessageTag = @messageTag});
		}

		///<summary>Send a photo.</summary>
		public void SendPhoto(TelegramBotSendPhotoParameters data)
		{
			_haContext.CallService("telegram_bot", "send_photo", null, data);
		}

		///<summary>Send a photo.</summary>
		///<param name="url">Remote path to an image. eg: http://example.org/path/to/the/image.png</param>
		///<param name="file">Local path to an image. eg: /path/to/the/image.png</param>
		///<param name="caption">The title of the image. eg: My image</param>
		///<param name="username">Username for a URL which require HTTP authentication. eg: myuser</param>
		///<param name="password">Password (or bearer token) for a URL which require HTTP authentication. eg: myuser_pwd</param>
		///<param name="authentication">Define which authentication method to use. Set to `digest` to use HTTP digest authentication, or `bearer_token` for OAuth 2.0 bearer token authentication. Defaults to `basic`.</param>
		///<param name="target">An array of pre-authorized chat_ids to send the document to. If not present, first allowed chat_id is the default. eg: [12345, 67890] or 12345</param>
		///<param name="parseMode">Parser for the message text.</param>
		///<param name="disableNotification">Sends the message silently. iOS users and Web users will not receive a notification, Android users will receive a notification with no sound.</param>
		///<param name="verifySsl">Enable or disable SSL certificate verification. Set to false if you're downloading the file from a URL and you don't want to validate the SSL certificate of the server.</param>
		///<param name="timeout">Timeout for send photo. Will help with timeout errors (poor internet connection, etc)</param>
		///<param name="keyboard">List of rows of commands, comma-separated, to make a custom keyboard. eg: ["/command1, /command2", "/command3"]</param>
		///<param name="inlineKeyboard">List of rows of commands, comma-separated, to make a custom inline keyboard with buttons with associated callback data. eg: ["/button1, /button2", "/button3"] or [[["Text button1", "/button1"], ["Text button2", "/button2"]], [["Text button3", "/button3"]]]</param>
		///<param name="messageTag">Tag for sent message. In telegram_sent event data: {{trigger.event.data.message_tag}} eg: msg_to_edit</param>
		public void SendPhoto(string? @url = null, string? @file = null, string? @caption = null, string? @username = null, string? @password = null, string? @authentication = null, object? @target = null, string? @parseMode = null, bool? @disableNotification = null, bool? @verifySsl = null, long? @timeout = null, object? @keyboard = null, object? @inlineKeyboard = null, string? @messageTag = null)
		{
			_haContext.CallService("telegram_bot", "send_photo", null, new TelegramBotSendPhotoParameters{Url = @url, File = @file, Caption = @caption, Username = @username, Password = @password, Authentication = @authentication, Target = @target, ParseMode = @parseMode, DisableNotification = @disableNotification, VerifySsl = @verifySsl, Timeout = @timeout, Keyboard = @keyboard, InlineKeyboard = @inlineKeyboard, MessageTag = @messageTag});
		}

		///<summary>Send a sticker.</summary>
		public void SendSticker(TelegramBotSendStickerParameters data)
		{
			_haContext.CallService("telegram_bot", "send_sticker", null, data);
		}

		///<summary>Send a sticker.</summary>
		///<param name="url">Remote path to a static .webp or animated .tgs sticker. eg: http://example.org/path/to/the/sticker.webp</param>
		///<param name="file">Local path to a static .webp or animated .tgs sticker. eg: /path/to/the/sticker.webp</param>
		///<param name="username">Username for a URL which require HTTP authentication. eg: myuser</param>
		///<param name="password">Password (or bearer token) for a URL which require HTTP authentication. eg: myuser_pwd</param>
		///<param name="authentication">Define which authentication method to use. Set to `digest` to use HTTP digest authentication, or `bearer_token` for OAuth 2.0 bearer token authentication. Defaults to `basic`.</param>
		///<param name="target">An array of pre-authorized chat_ids to send the document to. If not present, first allowed chat_id is the default. eg: [12345, 67890] or 12345</param>
		///<param name="disableNotification">Sends the message silently. iOS users and Web users will not receive a notification, Android users will receive a notification with no sound.</param>
		///<param name="verifySsl">Enable or disable SSL certificate verification. Set to false if you're downloading the file from a URL and you don't want to validate the SSL certificate of the server.</param>
		///<param name="timeout">Timeout for send sticker. Will help with timeout errors (poor internet connection, etc)</param>
		///<param name="keyboard">List of rows of commands, comma-separated, to make a custom keyboard. eg: ["/command1, /command2", "/command3"]</param>
		///<param name="inlineKeyboard">List of rows of commands, comma-separated, to make a custom inline keyboard with buttons with associated callback data. eg: ["/button1, /button2", "/button3"] or [[["Text button1", "/button1"], ["Text button2", "/button2"]], [["Text button3", "/button3"]]]</param>
		///<param name="messageTag">Tag for sent message. In telegram_sent event data: {{trigger.event.data.message_tag}} eg: msg_to_edit</param>
		public void SendSticker(string? @url = null, string? @file = null, string? @username = null, string? @password = null, string? @authentication = null, object? @target = null, bool? @disableNotification = null, bool? @verifySsl = null, long? @timeout = null, object? @keyboard = null, object? @inlineKeyboard = null, string? @messageTag = null)
		{
			_haContext.CallService("telegram_bot", "send_sticker", null, new TelegramBotSendStickerParameters{Url = @url, File = @file, Username = @username, Password = @password, Authentication = @authentication, Target = @target, DisableNotification = @disableNotification, VerifySsl = @verifySsl, Timeout = @timeout, Keyboard = @keyboard, InlineKeyboard = @inlineKeyboard, MessageTag = @messageTag});
		}

		///<summary>Send a video.</summary>
		public void SendVideo(TelegramBotSendVideoParameters data)
		{
			_haContext.CallService("telegram_bot", "send_video", null, data);
		}

		///<summary>Send a video.</summary>
		///<param name="url">Remote path to a video. eg: http://example.org/path/to/the/video.mp4</param>
		///<param name="file">Local path to a video. eg: /path/to/the/video.mp4</param>
		///<param name="caption">The title of the video. eg: My video</param>
		///<param name="username">Username for a URL which require HTTP authentication. eg: myuser</param>
		///<param name="password">Password (or bearer token) for a URL which require HTTP authentication. eg: myuser_pwd</param>
		///<param name="authentication">Define which authentication method to use. Set to `digest` to use HTTP digest authentication, or `bearer_token` for OAuth 2.0 bearer token authentication. Defaults to `basic`.</param>
		///<param name="target">An array of pre-authorized chat_ids to send the document to. If not present, first allowed chat_id is the default. eg: [12345, 67890] or 12345</param>
		///<param name="parseMode">Parser for the message text.</param>
		///<param name="disableNotification">Sends the message silently. iOS users and Web users will not receive a notification, Android users will receive a notification with no sound.</param>
		///<param name="verifySsl">Enable or disable SSL certificate verification. Set to false if you're downloading the file from a URL and you don't want to validate the SSL certificate of the server.</param>
		///<param name="timeout">Timeout for send video. Will help with timeout errors (poor internet connection, etc)</param>
		///<param name="keyboard">List of rows of commands, comma-separated, to make a custom keyboard. eg: ["/command1, /command2", "/command3"]</param>
		///<param name="inlineKeyboard">List of rows of commands, comma-separated, to make a custom inline keyboard with buttons with associated callback data. eg: ["/button1, /button2", "/button3"] or [[["Text button1", "/button1"], ["Text button2", "/button2"]], [["Text button3", "/button3"]]]</param>
		///<param name="messageTag">Tag for sent message. In telegram_sent event data: {{trigger.event.data.message_tag}} eg: msg_to_edit</param>
		public void SendVideo(string? @url = null, string? @file = null, string? @caption = null, string? @username = null, string? @password = null, string? @authentication = null, object? @target = null, string? @parseMode = null, bool? @disableNotification = null, bool? @verifySsl = null, long? @timeout = null, object? @keyboard = null, object? @inlineKeyboard = null, string? @messageTag = null)
		{
			_haContext.CallService("telegram_bot", "send_video", null, new TelegramBotSendVideoParameters{Url = @url, File = @file, Caption = @caption, Username = @username, Password = @password, Authentication = @authentication, Target = @target, ParseMode = @parseMode, DisableNotification = @disableNotification, VerifySsl = @verifySsl, Timeout = @timeout, Keyboard = @keyboard, InlineKeyboard = @inlineKeyboard, MessageTag = @messageTag});
		}

		///<summary>Send a voice message.</summary>
		public void SendVoice(TelegramBotSendVoiceParameters data)
		{
			_haContext.CallService("telegram_bot", "send_voice", null, data);
		}

		///<summary>Send a voice message.</summary>
		///<param name="url">Remote path to a voice message. eg: http://example.org/path/to/the/voice.opus</param>
		///<param name="file">Local path to a voice message. eg: /path/to/the/voice.opus</param>
		///<param name="caption">The title of the voice message. eg: My microphone recording</param>
		///<param name="username">Username for a URL which require HTTP authentication. eg: myuser</param>
		///<param name="password">Password (or bearer token) for a URL which require HTTP authentication. eg: myuser_pwd</param>
		///<param name="authentication">Define which authentication method to use. Set to `digest` to use HTTP digest authentication, or `bearer_token` for OAuth 2.0 bearer token authentication. Defaults to `basic`.</param>
		///<param name="target">An array of pre-authorized chat_ids to send the document to. If not present, first allowed chat_id is the default. eg: [12345, 67890] or 12345</param>
		///<param name="disableNotification">Sends the message silently. iOS users and Web users will not receive a notification, Android users will receive a notification with no sound.</param>
		///<param name="verifySsl">Enable or disable SSL certificate verification. Set to false if you're downloading the file from a URL and you don't want to validate the SSL certificate of the server.</param>
		///<param name="timeout">Timeout for send voice. Will help with timeout errors (poor internet connection, etc)</param>
		///<param name="keyboard">List of rows of commands, comma-separated, to make a custom keyboard. eg: ["/command1, /command2", "/command3"]</param>
		///<param name="inlineKeyboard">List of rows of commands, comma-separated, to make a custom inline keyboard with buttons with associated callback data. eg: ["/button1, /button2", "/button3"] or [[["Text button1", "/button1"], ["Text button2", "/button2"]], [["Text button3", "/button3"]]]</param>
		///<param name="messageTag">Tag for sent message. In telegram_sent event data: {{trigger.event.data.message_tag}} eg: msg_to_edit</param>
		public void SendVoice(string? @url = null, string? @file = null, string? @caption = null, string? @username = null, string? @password = null, string? @authentication = null, object? @target = null, bool? @disableNotification = null, bool? @verifySsl = null, long? @timeout = null, object? @keyboard = null, object? @inlineKeyboard = null, string? @messageTag = null)
		{
			_haContext.CallService("telegram_bot", "send_voice", null, new TelegramBotSendVoiceParameters{Url = @url, File = @file, Caption = @caption, Username = @username, Password = @password, Authentication = @authentication, Target = @target, DisableNotification = @disableNotification, VerifySsl = @verifySsl, Timeout = @timeout, Keyboard = @keyboard, InlineKeyboard = @inlineKeyboard, MessageTag = @messageTag});
		}
	}

	public record TelegramBotAnswerCallbackQueryParameters
	{
		///<summary>Unformatted text message body of the notification. eg: OK, I'm listening</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Unique id of the callback response. eg: {{ trigger.event.data.id }}</summary>
		[JsonPropertyName("callback_query_id")]
		public string? CallbackQueryId { get; init; }

		///<summary>Show a permanent notification.</summary>
		[JsonPropertyName("show_alert")]
		public bool? ShowAlert { get; init; }

		///<summary>Timeout for sending the answer. Will help with timeout errors (poor internet connection, etc)</summary>
		[JsonPropertyName("timeout")]
		public long? Timeout { get; init; }
	}

	public record TelegramBotDeleteMessageParameters
	{
		///<summary>id of the message to delete. eg: {{ trigger.event.data.message.message_id }}</summary>
		[JsonPropertyName("message_id")]
		public string? MessageId { get; init; }

		///<summary>The chat_id where to delete the message. eg: 12345</summary>
		[JsonPropertyName("chat_id")]
		public string? ChatId { get; init; }
	}

	public record TelegramBotEditCaptionParameters
	{
		///<summary>id of the message to edit. eg: {{ trigger.event.data.message.message_id }}</summary>
		[JsonPropertyName("message_id")]
		public string? MessageId { get; init; }

		///<summary>The chat_id where to edit the caption. eg: 12345</summary>
		[JsonPropertyName("chat_id")]
		public string? ChatId { get; init; }

		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("caption")]
		public string? Caption { get; init; }

		///<summary>List of rows of commands, comma-separated, to make a custom inline keyboard with buttons with associated callback data. eg: ["/button1, /button2", "/button3"] or [[["Text button1", "/button1"], ["Text button2", "/button2"]], [["Text button3", "/button3"]]]</summary>
		[JsonPropertyName("inline_keyboard")]
		public object? InlineKeyboard { get; init; }
	}

	public record TelegramBotEditMessageParameters
	{
		///<summary>id of the message to edit. eg: {{ trigger.event.data.message.message_id }}</summary>
		[JsonPropertyName("message_id")]
		public string? MessageId { get; init; }

		///<summary>The chat_id where to edit the message. eg: 12345</summary>
		[JsonPropertyName("chat_id")]
		public string? ChatId { get; init; }

		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Optional title for your notification. Will be composed as '%title\n%message' eg: Your Garage Door Friend</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }

		///<summary>Parser for the message text.</summary>
		[JsonPropertyName("parse_mode")]
		public string? ParseMode { get; init; }

		///<summary>Disables link previews for links in the message.</summary>
		[JsonPropertyName("disable_web_page_preview")]
		public bool? DisableWebPagePreview { get; init; }

		///<summary>List of rows of commands, comma-separated, to make a custom inline keyboard with buttons with associated callback data. eg: ["/button1, /button2", "/button3"] or [[["Text button1", "/button1"], ["Text button2", "/button2"]], [["Text button3", "/button3"]]]</summary>
		[JsonPropertyName("inline_keyboard")]
		public object? InlineKeyboard { get; init; }
	}

	public record TelegramBotEditReplymarkupParameters
	{
		///<summary>id of the message to edit. eg: {{ trigger.event.data.message.message_id }}</summary>
		[JsonPropertyName("message_id")]
		public string? MessageId { get; init; }

		///<summary>The chat_id where to edit the reply_markup. eg: 12345</summary>
		[JsonPropertyName("chat_id")]
		public string? ChatId { get; init; }

		///<summary>List of rows of commands, comma-separated, to make a custom inline keyboard with buttons with associated callback data. eg: ["/button1, /button2", "/button3"] or [[["Text button1", "/button1"], ["Text button2", "/button2"]], [["Text button3", "/button3"]]]</summary>
		[JsonPropertyName("inline_keyboard")]
		public object? InlineKeyboard { get; init; }
	}

	public record TelegramBotSendAnimationParameters
	{
		///<summary>Remote path to a GIF or H.264/MPEG-4 AVC video without sound. eg: http://example.org/path/to/the/animation.gif</summary>
		[JsonPropertyName("url")]
		public string? Url { get; init; }

		///<summary>Local path to a GIF or H.264/MPEG-4 AVC video without sound. eg: /path/to/the/animation.gif</summary>
		[JsonPropertyName("file")]
		public string? File { get; init; }

		///<summary>The title of the animation. eg: My animation</summary>
		[JsonPropertyName("caption")]
		public string? Caption { get; init; }

		///<summary>Username for a URL which require HTTP authentication. eg: myuser</summary>
		[JsonPropertyName("username")]
		public string? Username { get; init; }

		///<summary>Password (or bearer token) for a URL which require HTTP authentication. eg: myuser_pwd</summary>
		[JsonPropertyName("password")]
		public string? Password { get; init; }

		///<summary>Define which authentication method to use. Set to `digest` to use HTTP digest authentication, or `bearer_token` for OAuth 2.0 bearer token authentication. Defaults to `basic`.</summary>
		[JsonPropertyName("authentication")]
		public string? Authentication { get; init; }

		///<summary>An array of pre-authorized chat_ids to send the document to. If not present, first allowed chat_id is the default. eg: [12345, 67890] or 12345</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Parser for the message text.</summary>
		[JsonPropertyName("parse_mode")]
		public string? ParseMode { get; init; }

		///<summary>Sends the message silently. iOS users and Web users will not receive a notification, Android users will receive a notification with no sound.</summary>
		[JsonPropertyName("disable_notification")]
		public bool? DisableNotification { get; init; }

		///<summary>Enable or disable SSL certificate verification. Set to false if you're downloading the file from a URL and you don't want to validate the SSL certificate of the server.</summary>
		[JsonPropertyName("verify_ssl")]
		public bool? VerifySsl { get; init; }

		///<summary>Timeout for send sticker. Will help with timeout errors (poor internet connection, etc)</summary>
		[JsonPropertyName("timeout")]
		public long? Timeout { get; init; }

		///<summary>List of rows of commands, comma-separated, to make a custom keyboard. eg: ["/command1, /command2", "/command3"]</summary>
		[JsonPropertyName("keyboard")]
		public object? Keyboard { get; init; }

		///<summary>List of rows of commands, comma-separated, to make a custom inline keyboard with buttons with associated callback data. eg: ["/button1, /button2", "/button3"] or [[["Text button1", "/button1"], ["Text button2", "/button2"]], [["Text button3", "/button3"]]]</summary>
		[JsonPropertyName("inline_keyboard")]
		public object? InlineKeyboard { get; init; }
	}

	public record TelegramBotSendDocumentParameters
	{
		///<summary>Remote path to a document. eg: http://example.org/path/to/the/document.odf</summary>
		[JsonPropertyName("url")]
		public string? Url { get; init; }

		///<summary>Local path to a document. eg: /tmp/whatever.odf</summary>
		[JsonPropertyName("file")]
		public string? File { get; init; }

		///<summary>The title of the document. eg: Document Title xy</summary>
		[JsonPropertyName("caption")]
		public string? Caption { get; init; }

		///<summary>Username for a URL which require HTTP authentication. eg: myuser</summary>
		[JsonPropertyName("username")]
		public string? Username { get; init; }

		///<summary>Password (or bearer token) for a URL which require HTTP authentication. eg: myuser_pwd</summary>
		[JsonPropertyName("password")]
		public string? Password { get; init; }

		///<summary>Define which authentication method to use. Set to `digest` to use HTTP digest authentication, or `bearer_token` for OAuth 2.0 bearer token authentication. Defaults to `basic`.</summary>
		[JsonPropertyName("authentication")]
		public string? Authentication { get; init; }

		///<summary>An array of pre-authorized chat_ids to send the document to. If not present, first allowed chat_id is the default. eg: [12345, 67890] or 12345</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Parser for the message text.</summary>
		[JsonPropertyName("parse_mode")]
		public string? ParseMode { get; init; }

		///<summary>Sends the message silently. iOS users and Web users will not receive a notification, Android users will receive a notification with no sound.</summary>
		[JsonPropertyName("disable_notification")]
		public bool? DisableNotification { get; init; }

		///<summary>Enable or disable SSL certificate verification. Set to false if you're downloading the file from a URL and you don't want to validate the SSL certificate of the server.</summary>
		[JsonPropertyName("verify_ssl")]
		public bool? VerifySsl { get; init; }

		///<summary>Timeout for send document. Will help with timeout errors (poor internet connection, etc)</summary>
		[JsonPropertyName("timeout")]
		public long? Timeout { get; init; }

		///<summary>List of rows of commands, comma-separated, to make a custom keyboard. eg: ["/command1, /command2", "/command3"]</summary>
		[JsonPropertyName("keyboard")]
		public object? Keyboard { get; init; }

		///<summary>List of rows of commands, comma-separated, to make a custom inline keyboard with buttons with associated callback data. eg: ["/button1, /button2", "/button3"] or [[["Text button1", "/button1"], ["Text button2", "/button2"]], [["Text button3", "/button3"]]]</summary>
		[JsonPropertyName("inline_keyboard")]
		public object? InlineKeyboard { get; init; }

		///<summary>Tag for sent message. In telegram_sent event data: {{trigger.event.data.message_tag}} eg: msg_to_edit</summary>
		[JsonPropertyName("message_tag")]
		public string? MessageTag { get; init; }
	}

	public record TelegramBotSendLocationParameters
	{
		///<summary>The latitude to send.</summary>
		[JsonPropertyName("latitude")]
		public double? Latitude { get; init; }

		///<summary>The longitude to send.</summary>
		[JsonPropertyName("longitude")]
		public double? Longitude { get; init; }

		///<summary>An array of pre-authorized chat_ids to send the location to. If not present, first allowed chat_id is the default. eg: [12345, 67890] or 12345</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Sends the message silently. iOS users and Web users will not receive a notification, Android users will receive a notification with no sound.</summary>
		[JsonPropertyName("disable_notification")]
		public bool? DisableNotification { get; init; }

		///<summary>Timeout for send photo. Will help with timeout errors (poor internet connection, etc)</summary>
		[JsonPropertyName("timeout")]
		public long? Timeout { get; init; }

		///<summary>List of rows of commands, comma-separated, to make a custom keyboard. eg: ["/command1, /command2", "/command3"]</summary>
		[JsonPropertyName("keyboard")]
		public object? Keyboard { get; init; }

		///<summary>List of rows of commands, comma-separated, to make a custom inline keyboard with buttons with associated callback data. eg: ["/button1, /button2", "/button3"] or [[["Text button1", "/button1"], ["Text button2", "/button2"]], [["Text button3", "/button3"]]]</summary>
		[JsonPropertyName("inline_keyboard")]
		public object? InlineKeyboard { get; init; }

		///<summary>Tag for sent message. In telegram_sent event data: {{trigger.event.data.message_tag}} eg: msg_to_edit</summary>
		[JsonPropertyName("message_tag")]
		public string? MessageTag { get; init; }
	}

	public record TelegramBotSendMessageParameters
	{
		///<summary>Message body of the notification. eg: The garage door has been open for 10 minutes.</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Optional title for your notification. Will be composed as '%title\n%message' eg: Your Garage Door Friend</summary>
		[JsonPropertyName("title")]
		public string? Title { get; init; }

		///<summary>An array of pre-authorized chat_ids to send the notification to. If not present, first allowed chat_id is the default. eg: [12345, 67890] or 12345</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Parser for the message text.</summary>
		[JsonPropertyName("parse_mode")]
		public string? ParseMode { get; init; }

		///<summary>Sends the message silently. iOS users and Web users will not receive a notification, Android users will receive a notification with no sound.</summary>
		[JsonPropertyName("disable_notification")]
		public bool? DisableNotification { get; init; }

		///<summary>Disables link previews for links in the message.</summary>
		[JsonPropertyName("disable_web_page_preview")]
		public bool? DisableWebPagePreview { get; init; }

		///<summary>Timeout for send message. Will help with timeout errors (poor internet connection, etc)s</summary>
		[JsonPropertyName("timeout")]
		public long? Timeout { get; init; }

		///<summary>List of rows of commands, comma-separated, to make a custom keyboard. Empty list clears a previously set keyboard. eg: ["/command1, /command2", "/command3"]</summary>
		[JsonPropertyName("keyboard")]
		public object? Keyboard { get; init; }

		///<summary>List of rows of commands, comma-separated, to make a custom inline keyboard with buttons with associated callback data. eg: ["/button1, /button2", "/button3"] or ["Text button1:/button1, Text button2:/button2", "Text button3:/button3"] or [[["Text button1", "/button1"], ["Text button2", "/button2"]], [["Text button3", "/button3"]]]</summary>
		[JsonPropertyName("inline_keyboard")]
		public object? InlineKeyboard { get; init; }

		///<summary>Tag for sent message. In telegram_sent event data: {{trigger.event.data.message_tag}} eg: msg_to_edit</summary>
		[JsonPropertyName("message_tag")]
		public string? MessageTag { get; init; }
	}

	public record TelegramBotSendPhotoParameters
	{
		///<summary>Remote path to an image. eg: http://example.org/path/to/the/image.png</summary>
		[JsonPropertyName("url")]
		public string? Url { get; init; }

		///<summary>Local path to an image. eg: /path/to/the/image.png</summary>
		[JsonPropertyName("file")]
		public string? File { get; init; }

		///<summary>The title of the image. eg: My image</summary>
		[JsonPropertyName("caption")]
		public string? Caption { get; init; }

		///<summary>Username for a URL which require HTTP authentication. eg: myuser</summary>
		[JsonPropertyName("username")]
		public string? Username { get; init; }

		///<summary>Password (or bearer token) for a URL which require HTTP authentication. eg: myuser_pwd</summary>
		[JsonPropertyName("password")]
		public string? Password { get; init; }

		///<summary>Define which authentication method to use. Set to `digest` to use HTTP digest authentication, or `bearer_token` for OAuth 2.0 bearer token authentication. Defaults to `basic`.</summary>
		[JsonPropertyName("authentication")]
		public string? Authentication { get; init; }

		///<summary>An array of pre-authorized chat_ids to send the document to. If not present, first allowed chat_id is the default. eg: [12345, 67890] or 12345</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Parser for the message text.</summary>
		[JsonPropertyName("parse_mode")]
		public string? ParseMode { get; init; }

		///<summary>Sends the message silently. iOS users and Web users will not receive a notification, Android users will receive a notification with no sound.</summary>
		[JsonPropertyName("disable_notification")]
		public bool? DisableNotification { get; init; }

		///<summary>Enable or disable SSL certificate verification. Set to false if you're downloading the file from a URL and you don't want to validate the SSL certificate of the server.</summary>
		[JsonPropertyName("verify_ssl")]
		public bool? VerifySsl { get; init; }

		///<summary>Timeout for send photo. Will help with timeout errors (poor internet connection, etc)</summary>
		[JsonPropertyName("timeout")]
		public long? Timeout { get; init; }

		///<summary>List of rows of commands, comma-separated, to make a custom keyboard. eg: ["/command1, /command2", "/command3"]</summary>
		[JsonPropertyName("keyboard")]
		public object? Keyboard { get; init; }

		///<summary>List of rows of commands, comma-separated, to make a custom inline keyboard with buttons with associated callback data. eg: ["/button1, /button2", "/button3"] or [[["Text button1", "/button1"], ["Text button2", "/button2"]], [["Text button3", "/button3"]]]</summary>
		[JsonPropertyName("inline_keyboard")]
		public object? InlineKeyboard { get; init; }

		///<summary>Tag for sent message. In telegram_sent event data: {{trigger.event.data.message_tag}} eg: msg_to_edit</summary>
		[JsonPropertyName("message_tag")]
		public string? MessageTag { get; init; }
	}

	public record TelegramBotSendStickerParameters
	{
		///<summary>Remote path to a static .webp or animated .tgs sticker. eg: http://example.org/path/to/the/sticker.webp</summary>
		[JsonPropertyName("url")]
		public string? Url { get; init; }

		///<summary>Local path to a static .webp or animated .tgs sticker. eg: /path/to/the/sticker.webp</summary>
		[JsonPropertyName("file")]
		public string? File { get; init; }

		///<summary>Username for a URL which require HTTP authentication. eg: myuser</summary>
		[JsonPropertyName("username")]
		public string? Username { get; init; }

		///<summary>Password (or bearer token) for a URL which require HTTP authentication. eg: myuser_pwd</summary>
		[JsonPropertyName("password")]
		public string? Password { get; init; }

		///<summary>Define which authentication method to use. Set to `digest` to use HTTP digest authentication, or `bearer_token` for OAuth 2.0 bearer token authentication. Defaults to `basic`.</summary>
		[JsonPropertyName("authentication")]
		public string? Authentication { get; init; }

		///<summary>An array of pre-authorized chat_ids to send the document to. If not present, first allowed chat_id is the default. eg: [12345, 67890] or 12345</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Sends the message silently. iOS users and Web users will not receive a notification, Android users will receive a notification with no sound.</summary>
		[JsonPropertyName("disable_notification")]
		public bool? DisableNotification { get; init; }

		///<summary>Enable or disable SSL certificate verification. Set to false if you're downloading the file from a URL and you don't want to validate the SSL certificate of the server.</summary>
		[JsonPropertyName("verify_ssl")]
		public bool? VerifySsl { get; init; }

		///<summary>Timeout for send sticker. Will help with timeout errors (poor internet connection, etc)</summary>
		[JsonPropertyName("timeout")]
		public long? Timeout { get; init; }

		///<summary>List of rows of commands, comma-separated, to make a custom keyboard. eg: ["/command1, /command2", "/command3"]</summary>
		[JsonPropertyName("keyboard")]
		public object? Keyboard { get; init; }

		///<summary>List of rows of commands, comma-separated, to make a custom inline keyboard with buttons with associated callback data. eg: ["/button1, /button2", "/button3"] or [[["Text button1", "/button1"], ["Text button2", "/button2"]], [["Text button3", "/button3"]]]</summary>
		[JsonPropertyName("inline_keyboard")]
		public object? InlineKeyboard { get; init; }

		///<summary>Tag for sent message. In telegram_sent event data: {{trigger.event.data.message_tag}} eg: msg_to_edit</summary>
		[JsonPropertyName("message_tag")]
		public string? MessageTag { get; init; }
	}

	public record TelegramBotSendVideoParameters
	{
		///<summary>Remote path to a video. eg: http://example.org/path/to/the/video.mp4</summary>
		[JsonPropertyName("url")]
		public string? Url { get; init; }

		///<summary>Local path to a video. eg: /path/to/the/video.mp4</summary>
		[JsonPropertyName("file")]
		public string? File { get; init; }

		///<summary>The title of the video. eg: My video</summary>
		[JsonPropertyName("caption")]
		public string? Caption { get; init; }

		///<summary>Username for a URL which require HTTP authentication. eg: myuser</summary>
		[JsonPropertyName("username")]
		public string? Username { get; init; }

		///<summary>Password (or bearer token) for a URL which require HTTP authentication. eg: myuser_pwd</summary>
		[JsonPropertyName("password")]
		public string? Password { get; init; }

		///<summary>Define which authentication method to use. Set to `digest` to use HTTP digest authentication, or `bearer_token` for OAuth 2.0 bearer token authentication. Defaults to `basic`.</summary>
		[JsonPropertyName("authentication")]
		public string? Authentication { get; init; }

		///<summary>An array of pre-authorized chat_ids to send the document to. If not present, first allowed chat_id is the default. eg: [12345, 67890] or 12345</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Parser for the message text.</summary>
		[JsonPropertyName("parse_mode")]
		public string? ParseMode { get; init; }

		///<summary>Sends the message silently. iOS users and Web users will not receive a notification, Android users will receive a notification with no sound.</summary>
		[JsonPropertyName("disable_notification")]
		public bool? DisableNotification { get; init; }

		///<summary>Enable or disable SSL certificate verification. Set to false if you're downloading the file from a URL and you don't want to validate the SSL certificate of the server.</summary>
		[JsonPropertyName("verify_ssl")]
		public bool? VerifySsl { get; init; }

		///<summary>Timeout for send video. Will help with timeout errors (poor internet connection, etc)</summary>
		[JsonPropertyName("timeout")]
		public long? Timeout { get; init; }

		///<summary>List of rows of commands, comma-separated, to make a custom keyboard. eg: ["/command1, /command2", "/command3"]</summary>
		[JsonPropertyName("keyboard")]
		public object? Keyboard { get; init; }

		///<summary>List of rows of commands, comma-separated, to make a custom inline keyboard with buttons with associated callback data. eg: ["/button1, /button2", "/button3"] or [[["Text button1", "/button1"], ["Text button2", "/button2"]], [["Text button3", "/button3"]]]</summary>
		[JsonPropertyName("inline_keyboard")]
		public object? InlineKeyboard { get; init; }

		///<summary>Tag for sent message. In telegram_sent event data: {{trigger.event.data.message_tag}} eg: msg_to_edit</summary>
		[JsonPropertyName("message_tag")]
		public string? MessageTag { get; init; }
	}

	public record TelegramBotSendVoiceParameters
	{
		///<summary>Remote path to a voice message. eg: http://example.org/path/to/the/voice.opus</summary>
		[JsonPropertyName("url")]
		public string? Url { get; init; }

		///<summary>Local path to a voice message. eg: /path/to/the/voice.opus</summary>
		[JsonPropertyName("file")]
		public string? File { get; init; }

		///<summary>The title of the voice message. eg: My microphone recording</summary>
		[JsonPropertyName("caption")]
		public string? Caption { get; init; }

		///<summary>Username for a URL which require HTTP authentication. eg: myuser</summary>
		[JsonPropertyName("username")]
		public string? Username { get; init; }

		///<summary>Password (or bearer token) for a URL which require HTTP authentication. eg: myuser_pwd</summary>
		[JsonPropertyName("password")]
		public string? Password { get; init; }

		///<summary>Define which authentication method to use. Set to `digest` to use HTTP digest authentication, or `bearer_token` for OAuth 2.0 bearer token authentication. Defaults to `basic`.</summary>
		[JsonPropertyName("authentication")]
		public string? Authentication { get; init; }

		///<summary>An array of pre-authorized chat_ids to send the document to. If not present, first allowed chat_id is the default. eg: [12345, 67890] or 12345</summary>
		[JsonPropertyName("target")]
		public object? Target { get; init; }

		///<summary>Sends the message silently. iOS users and Web users will not receive a notification, Android users will receive a notification with no sound.</summary>
		[JsonPropertyName("disable_notification")]
		public bool? DisableNotification { get; init; }

		///<summary>Enable or disable SSL certificate verification. Set to false if you're downloading the file from a URL and you don't want to validate the SSL certificate of the server.</summary>
		[JsonPropertyName("verify_ssl")]
		public bool? VerifySsl { get; init; }

		///<summary>Timeout for send voice. Will help with timeout errors (poor internet connection, etc)</summary>
		[JsonPropertyName("timeout")]
		public long? Timeout { get; init; }

		///<summary>List of rows of commands, comma-separated, to make a custom keyboard. eg: ["/command1, /command2", "/command3"]</summary>
		[JsonPropertyName("keyboard")]
		public object? Keyboard { get; init; }

		///<summary>List of rows of commands, comma-separated, to make a custom inline keyboard with buttons with associated callback data. eg: ["/button1, /button2", "/button3"] or [[["Text button1", "/button1"], ["Text button2", "/button2"]], [["Text button3", "/button3"]]]</summary>
		[JsonPropertyName("inline_keyboard")]
		public object? InlineKeyboard { get; init; }

		///<summary>Tag for sent message. In telegram_sent event data: {{trigger.event.data.message_tag}} eg: msg_to_edit</summary>
		[JsonPropertyName("message_tag")]
		public string? MessageTag { get; init; }
	}

	public class TemplateServices
	{
		private readonly IHaContext _haContext;
		public TemplateServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Reload all template entities.</summary>
		public void Reload()
		{
			_haContext.CallService("template", "reload", null);
		}
	}

	public class TimerServices
	{
		private readonly IHaContext _haContext;
		public TimerServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Cancel a timer.</summary>
		///<param name="target">The target for this service call</param>
		public void Cancel(ServiceTarget target)
		{
			_haContext.CallService("timer", "cancel", target);
		}

		///<summary>Finish a timer.</summary>
		///<param name="target">The target for this service call</param>
		public void Finish(ServiceTarget target)
		{
			_haContext.CallService("timer", "finish", target);
		}

		///<summary>Pause a timer.</summary>
		///<param name="target">The target for this service call</param>
		public void Pause(ServiceTarget target)
		{
			_haContext.CallService("timer", "pause", target);
		}

		public void Reload()
		{
			_haContext.CallService("timer", "reload", null);
		}

		///<summary>Start a timer</summary>
		///<param name="target">The target for this service call</param>
		public void Start(ServiceTarget target, TimerStartParameters data)
		{
			_haContext.CallService("timer", "start", target, data);
		}

		///<summary>Start a timer</summary>
		///<param name="target">The target for this service call</param>
		///<param name="duration">Duration the timer requires to finish. [optional] eg: 00:01:00 or 60</param>
		public void Start(ServiceTarget target, string? @duration = null)
		{
			_haContext.CallService("timer", "start", target, new TimerStartParameters{Duration = @duration});
		}
	}

	public record TimerStartParameters
	{
		///<summary>Duration the timer requires to finish. [optional] eg: 00:01:00 or 60</summary>
		[JsonPropertyName("duration")]
		public string? Duration { get; init; }
	}

	public class TtsServices
	{
		private readonly IHaContext _haContext;
		public TtsServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Remove all text-to-speech cache files and RAM cache.</summary>
		public void ClearCache()
		{
			_haContext.CallService("tts", "clear_cache", null);
		}

		///<summary>Say something using text-to-speech on a media player with cloud.</summary>
		public void CloudSay(TtsCloudSayParameters data)
		{
			_haContext.CallService("tts", "cloud_say", null, data);
		}

		///<summary>Say something using text-to-speech on a media player with cloud.</summary>
		///<param name="entityId">Name(s) of media player entities.</param>
		///<param name="message">Text to speak on devices. eg: My name is hanna</param>
		///<param name="cache">Control file cache of this message.</param>
		///<param name="language">Language to use for speech generation. eg: ru</param>
		///<param name="options">A dictionary containing platform-specific options. Optional depending on the platform. eg: platform specific</param>
		public void CloudSay(string @entityId, string @message, bool? @cache = null, string? @language = null, object? @options = null)
		{
			_haContext.CallService("tts", "cloud_say", null, new TtsCloudSayParameters{EntityId = @entityId, Message = @message, Cache = @cache, Language = @language, Options = @options});
		}

		///<summary>Say something using text-to-speech on a media player with google_translate.</summary>
		public void GoogleTranslateSay(TtsGoogleTranslateSayParameters data)
		{
			_haContext.CallService("tts", "google_translate_say", null, data);
		}

		///<summary>Say something using text-to-speech on a media player with google_translate.</summary>
		///<param name="entityId">Name(s) of media player entities.</param>
		///<param name="message">Text to speak on devices. eg: My name is hanna</param>
		///<param name="cache">Control file cache of this message.</param>
		///<param name="language">Language to use for speech generation. eg: ru</param>
		///<param name="options">A dictionary containing platform-specific options. Optional depending on the platform. eg: platform specific</param>
		public void GoogleTranslateSay(string @entityId, string @message, bool? @cache = null, string? @language = null, object? @options = null)
		{
			_haContext.CallService("tts", "google_translate_say", null, new TtsGoogleTranslateSayParameters{EntityId = @entityId, Message = @message, Cache = @cache, Language = @language, Options = @options});
		}
	}

	public record TtsCloudSayParameters
	{
		///<summary>Name(s) of media player entities.</summary>
		[JsonPropertyName("entity_id")]
		public string? EntityId { get; init; }

		///<summary>Text to speak on devices. eg: My name is hanna</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Control file cache of this message.</summary>
		[JsonPropertyName("cache")]
		public bool? Cache { get; init; }

		///<summary>Language to use for speech generation. eg: ru</summary>
		[JsonPropertyName("language")]
		public string? Language { get; init; }

		///<summary>A dictionary containing platform-specific options. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("options")]
		public object? Options { get; init; }
	}

	public record TtsGoogleTranslateSayParameters
	{
		///<summary>Name(s) of media player entities.</summary>
		[JsonPropertyName("entity_id")]
		public string? EntityId { get; init; }

		///<summary>Text to speak on devices. eg: My name is hanna</summary>
		[JsonPropertyName("message")]
		public string? Message { get; init; }

		///<summary>Control file cache of this message.</summary>
		[JsonPropertyName("cache")]
		public bool? Cache { get; init; }

		///<summary>Language to use for speech generation. eg: ru</summary>
		[JsonPropertyName("language")]
		public string? Language { get; init; }

		///<summary>A dictionary containing platform-specific options. Optional depending on the platform. eg: platform specific</summary>
		[JsonPropertyName("options")]
		public object? Options { get; init; }
	}

	public class UnifiServices
	{
		private readonly IHaContext _haContext;
		public UnifiServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Try to get wireless client to reconnect to UniFi Network</summary>
		public void ReconnectClient(UnifiReconnectClientParameters data)
		{
			_haContext.CallService("unifi", "reconnect_client", null, data);
		}

		///<summary>Try to get wireless client to reconnect to UniFi Network</summary>
		///<param name="deviceId">Try reconnect client to wireless network</param>
		public void ReconnectClient(string @deviceId)
		{
			_haContext.CallService("unifi", "reconnect_client", null, new UnifiReconnectClientParameters{DeviceId = @deviceId});
		}

		///<summary>Clean up clients that has only been associated with the controller for a short period of time.</summary>
		public void RemoveClients()
		{
			_haContext.CallService("unifi", "remove_clients", null);
		}
	}

	public record UnifiReconnectClientParameters
	{
		///<summary>Try reconnect client to wireless network</summary>
		[JsonPropertyName("device_id")]
		public string? DeviceId { get; init; }
	}

	public class UtilityMeterServices
	{
		private readonly IHaContext _haContext;
		public UtilityMeterServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Calibrates a utility meter sensor.</summary>
		///<param name="target">The target for this service call</param>
		public void Calibrate(ServiceTarget target, UtilityMeterCalibrateParameters data)
		{
			_haContext.CallService("utility_meter", "calibrate", target, data);
		}

		///<summary>Calibrates a utility meter sensor.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="value">Value to which set the meter eg: 100</param>
		public void Calibrate(ServiceTarget target, string @value)
		{
			_haContext.CallService("utility_meter", "calibrate", target, new UtilityMeterCalibrateParameters{Value = @value});
		}
	}

	public record UtilityMeterCalibrateParameters
	{
		///<summary>Value to which set the meter eg: 100</summary>
		[JsonPropertyName("value")]
		public string? Value { get; init; }
	}

	public class VacuumServices
	{
		private readonly IHaContext _haContext;
		public VacuumServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Tell the vacuum cleaner to do a spot clean-up.</summary>
		///<param name="target">The target for this service call</param>
		public void CleanSpot(ServiceTarget target)
		{
			_haContext.CallService("vacuum", "clean_spot", target);
		}

		///<summary>Locate the vacuum cleaner robot.</summary>
		///<param name="target">The target for this service call</param>
		public void Locate(ServiceTarget target)
		{
			_haContext.CallService("vacuum", "locate", target);
		}

		///<summary>Pause the cleaning task.</summary>
		///<param name="target">The target for this service call</param>
		public void Pause(ServiceTarget target)
		{
			_haContext.CallService("vacuum", "pause", target);
		}

		///<summary>Tell the vacuum cleaner to return to its dock.</summary>
		///<param name="target">The target for this service call</param>
		public void ReturnToBase(ServiceTarget target)
		{
			_haContext.CallService("vacuum", "return_to_base", target);
		}

		///<summary>Send a raw command to the vacuum cleaner.</summary>
		///<param name="target">The target for this service call</param>
		public void SendCommand(ServiceTarget target, VacuumSendCommandParameters data)
		{
			_haContext.CallService("vacuum", "send_command", target, data);
		}

		///<summary>Send a raw command to the vacuum cleaner.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="command">Command to execute. eg: set_dnd_timer</param>
		///<param name="params">Parameters for the command. eg: { "key": "value" }</param>
		public void SendCommand(ServiceTarget target, string @command, object? @params = null)
		{
			_haContext.CallService("vacuum", "send_command", target, new VacuumSendCommandParameters{Command = @command, Params = @params});
		}

		///<summary>Set the fan speed of the vacuum cleaner.</summary>
		///<param name="target">The target for this service call</param>
		public void SetFanSpeed(ServiceTarget target, VacuumSetFanSpeedParameters data)
		{
			_haContext.CallService("vacuum", "set_fan_speed", target, data);
		}

		///<summary>Set the fan speed of the vacuum cleaner.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="fanSpeed">Platform dependent vacuum cleaner fan speed, with speed steps, like 'medium' or by percentage, between 0 and 100. eg: low</param>
		public void SetFanSpeed(ServiceTarget target, string @fanSpeed)
		{
			_haContext.CallService("vacuum", "set_fan_speed", target, new VacuumSetFanSpeedParameters{FanSpeed = @fanSpeed});
		}

		///<summary>Start or resume the cleaning task.</summary>
		///<param name="target">The target for this service call</param>
		public void Start(ServiceTarget target)
		{
			_haContext.CallService("vacuum", "start", target);
		}

		///<summary>Start, pause, or resume the cleaning task.</summary>
		///<param name="target">The target for this service call</param>
		public void StartPause(ServiceTarget target)
		{
			_haContext.CallService("vacuum", "start_pause", target);
		}

		///<summary>Stop the current cleaning task.</summary>
		///<param name="target">The target for this service call</param>
		public void Stop(ServiceTarget target)
		{
			_haContext.CallService("vacuum", "stop", target);
		}

		public void Toggle()
		{
			_haContext.CallService("vacuum", "toggle", null);
		}

		///<summary>Stop the current cleaning task and return to home.</summary>
		///<param name="target">The target for this service call</param>
		public void TurnOff(ServiceTarget target)
		{
			_haContext.CallService("vacuum", "turn_off", target);
		}

		///<summary>Start a new cleaning task.</summary>
		///<param name="target">The target for this service call</param>
		public void TurnOn(ServiceTarget target)
		{
			_haContext.CallService("vacuum", "turn_on", target);
		}
	}

	public record VacuumSendCommandParameters
	{
		///<summary>Command to execute. eg: set_dnd_timer</summary>
		[JsonPropertyName("command")]
		public string? Command { get; init; }

		///<summary>Parameters for the command. eg: { "key": "value" }</summary>
		[JsonPropertyName("params")]
		public object? Params { get; init; }
	}

	public record VacuumSetFanSpeedParameters
	{
		///<summary>Platform dependent vacuum cleaner fan speed, with speed steps, like 'medium' or by percentage, between 0 and 100. eg: low</summary>
		[JsonPropertyName("fan_speed")]
		public string? FanSpeed { get; init; }
	}

	public class WakeOnLanServices
	{
		private readonly IHaContext _haContext;
		public WakeOnLanServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Send a 'magic packet' to wake up a device with 'Wake-On-LAN' capabilities.</summary>
		public void SendMagicPacket(WakeOnLanSendMagicPacketParameters data)
		{
			_haContext.CallService("wake_on_lan", "send_magic_packet", null, data);
		}

		///<summary>Send a 'magic packet' to wake up a device with 'Wake-On-LAN' capabilities.</summary>
		///<param name="mac">MAC address of the device to wake up. eg: aa:bb:cc:dd:ee:ff</param>
		///<param name="broadcastAddress">Broadcast IP where to send the magic packet. eg: 192.168.255.255</param>
		///<param name="broadcastPort">Port where to send the magic packet.</param>
		public void SendMagicPacket(string @mac, string? @broadcastAddress = null, long? @broadcastPort = null)
		{
			_haContext.CallService("wake_on_lan", "send_magic_packet", null, new WakeOnLanSendMagicPacketParameters{Mac = @mac, BroadcastAddress = @broadcastAddress, BroadcastPort = @broadcastPort});
		}
	}

	public record WakeOnLanSendMagicPacketParameters
	{
		///<summary>MAC address of the device to wake up. eg: aa:bb:cc:dd:ee:ff</summary>
		[JsonPropertyName("mac")]
		public string? Mac { get; init; }

		///<summary>Broadcast IP where to send the magic packet. eg: 192.168.255.255</summary>
		[JsonPropertyName("broadcast_address")]
		public string? BroadcastAddress { get; init; }

		///<summary>Port where to send the magic packet.</summary>
		[JsonPropertyName("broadcast_port")]
		public long? BroadcastPort { get; init; }
	}

	public class WaterHeaterServices
	{
		private readonly IHaContext _haContext;
		public WaterHeaterServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Turn away mode on/off for water_heater device.</summary>
		///<param name="target">The target for this service call</param>
		public void SetAwayMode(ServiceTarget target, WaterHeaterSetAwayModeParameters data)
		{
			_haContext.CallService("water_heater", "set_away_mode", target, data);
		}

		///<summary>Turn away mode on/off for water_heater device.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="awayMode">New value of away mode.</param>
		public void SetAwayMode(ServiceTarget target, bool @awayMode)
		{
			_haContext.CallService("water_heater", "set_away_mode", target, new WaterHeaterSetAwayModeParameters{AwayMode = @awayMode});
		}

		///<summary>Set operation mode for water_heater device.</summary>
		///<param name="target">The target for this service call</param>
		public void SetOperationMode(ServiceTarget target, WaterHeaterSetOperationModeParameters data)
		{
			_haContext.CallService("water_heater", "set_operation_mode", target, data);
		}

		///<summary>Set operation mode for water_heater device.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="operationMode">New value of operation mode. eg: eco</param>
		public void SetOperationMode(ServiceTarget target, string @operationMode)
		{
			_haContext.CallService("water_heater", "set_operation_mode", target, new WaterHeaterSetOperationModeParameters{OperationMode = @operationMode});
		}

		///<summary>Set target temperature of water_heater device.</summary>
		///<param name="target">The target for this service call</param>
		public void SetTemperature(ServiceTarget target, WaterHeaterSetTemperatureParameters data)
		{
			_haContext.CallService("water_heater", "set_temperature", target, data);
		}

		///<summary>Set target temperature of water_heater device.</summary>
		///<param name="target">The target for this service call</param>
		///<param name="temperature">New target temperature for water heater.</param>
		///<param name="operationMode">New value of operation mode. eg: eco</param>
		public void SetTemperature(ServiceTarget target, double @temperature, string? @operationMode = null)
		{
			_haContext.CallService("water_heater", "set_temperature", target, new WaterHeaterSetTemperatureParameters{Temperature = @temperature, OperationMode = @operationMode});
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
		///<summary>New value of away mode.</summary>
		[JsonPropertyName("away_mode")]
		public bool? AwayMode { get; init; }
	}

	public record WaterHeaterSetOperationModeParameters
	{
		///<summary>New value of operation mode. eg: eco</summary>
		[JsonPropertyName("operation_mode")]
		public string? OperationMode { get; init; }
	}

	public record WaterHeaterSetTemperatureParameters
	{
		///<summary>New target temperature for water heater.</summary>
		[JsonPropertyName("temperature")]
		public double? Temperature { get; init; }

		///<summary>New value of operation mode. eg: eco</summary>
		[JsonPropertyName("operation_mode")]
		public string? OperationMode { get; init; }
	}

	public class WebostvServices
	{
		private readonly IHaContext _haContext;
		public WebostvServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Send a button press command.</summary>
		public void Button(WebostvButtonParameters data)
		{
			_haContext.CallService("webostv", "button", null, data);
		}

		///<summary>Send a button press command.</summary>
		///<param name="entityId">Name(s) of the webostv entities where to run the API method.</param>
		///<param name="button">Name of the button to press.  Known possible values are LEFT, RIGHT, DOWN, UP, HOME, MENU, BACK, ENTER, DASH, INFO, ASTERISK, CC, EXIT, MUTE, RED, GREEN, BLUE, VOLUMEUP, VOLUMEDOWN, CHANNELUP, CHANNELDOWN, PLAY, PAUSE, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 eg: LEFT</param>
		public void Button(string @entityId, string @button)
		{
			_haContext.CallService("webostv", "button", null, new WebostvButtonParameters{EntityId = @entityId, Button = @button});
		}

		///<summary>Send a command.</summary>
		public void Command(WebostvCommandParameters data)
		{
			_haContext.CallService("webostv", "command", null, data);
		}

		///<summary>Send a command.</summary>
		///<param name="entityId">Name(s) of the webostv entities where to run the API method.</param>
		///<param name="command">Endpoint of the command. eg: system.launcher/open</param>
		///<param name="payload">An optional payload to provide to the endpoint in the format of key value pair(s). eg: target: https://www.google.com</param>
		public void Command(string @entityId, string @command, object? @payload = null)
		{
			_haContext.CallService("webostv", "command", null, new WebostvCommandParameters{EntityId = @entityId, Command = @command, Payload = @payload});
		}

		///<summary>Send the TV the command to change sound output.</summary>
		public void SelectSoundOutput(WebostvSelectSoundOutputParameters data)
		{
			_haContext.CallService("webostv", "select_sound_output", null, data);
		}

		///<summary>Send the TV the command to change sound output.</summary>
		///<param name="entityId">Name(s) of the webostv entities to change sound output on.</param>
		///<param name="soundOutput">Name of the sound output to switch to. eg: external_speaker</param>
		public void SelectSoundOutput(string @entityId, string @soundOutput)
		{
			_haContext.CallService("webostv", "select_sound_output", null, new WebostvSelectSoundOutputParameters{EntityId = @entityId, SoundOutput = @soundOutput});
		}
	}

	public record WebostvButtonParameters
	{
		///<summary>Name(s) of the webostv entities where to run the API method.</summary>
		[JsonPropertyName("entity_id")]
		public string? EntityId { get; init; }

		///<summary>Name of the button to press.  Known possible values are LEFT, RIGHT, DOWN, UP, HOME, MENU, BACK, ENTER, DASH, INFO, ASTERISK, CC, EXIT, MUTE, RED, GREEN, BLUE, VOLUMEUP, VOLUMEDOWN, CHANNELUP, CHANNELDOWN, PLAY, PAUSE, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 eg: LEFT</summary>
		[JsonPropertyName("button")]
		public string? Button { get; init; }
	}

	public record WebostvCommandParameters
	{
		///<summary>Name(s) of the webostv entities where to run the API method.</summary>
		[JsonPropertyName("entity_id")]
		public string? EntityId { get; init; }

		///<summary>Endpoint of the command. eg: system.launcher/open</summary>
		[JsonPropertyName("command")]
		public string? Command { get; init; }

		///<summary>An optional payload to provide to the endpoint in the format of key value pair(s). eg: target: https://www.google.com</summary>
		[JsonPropertyName("payload")]
		public object? Payload { get; init; }
	}

	public record WebostvSelectSoundOutputParameters
	{
		///<summary>Name(s) of the webostv entities to change sound output on.</summary>
		[JsonPropertyName("entity_id")]
		public string? EntityId { get; init; }

		///<summary>Name of the sound output to switch to. eg: external_speaker</summary>
		[JsonPropertyName("sound_output")]
		public string? SoundOutput { get; init; }
	}

	public class WiserServices
	{
		private readonly IHaContext _haContext;
		public WiserServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Set the boost mode ON defining the period of time and the desired target temperature or temperature increase delta for the boost.  Setting the temperature delta overrides any target temperature. </summary>
		public void BoostHeating(WiserBoostHeatingParameters data)
		{
			_haContext.CallService("wiser", "boost_heating", null, data);
		}

		///<summary>Set the boost mode ON defining the period of time and the desired target temperature or temperature increase delta for the boost.  Setting the temperature delta overrides any target temperature. </summary>
		///<param name="entityId">Enter the entity_id for the room required to set the boost mode.  eg: climate.wiser_lounge</param>
		///<param name="timePeriod">Set the time period for the boost in minutes. eg: 60</param>
		///<param name="temperature">Set the target temperature for the boost period. eg: 20.5</param>
		///<param name="temperatureDelta">Set the temperature increase for the boost period. eg: 2</param>
		public void BoostHeating(object? @entityId = null, object? @timePeriod = null, object? @temperature = null, object? @temperatureDelta = null)
		{
			_haContext.CallService("wiser", "boost_heating", null, new WiserBoostHeatingParameters{EntityId = @entityId, TimePeriod = @timePeriod, Temperature = @temperature, TemperatureDelta = @temperatureDelta});
		}

		///<summary>Copy the schedule in a room to another room</summary>
		public void CopySchedule(WiserCopyScheduleParameters data)
		{
			_haContext.CallService("wiser", "copy_schedule", null, data);
		}

		///<summary>Copy the schedule in a room to another room</summary>
		///<param name="entityId">Enter the entity_id for the room to copy the schedule from. eg: climate.wiser_lounge</param>
		///<param name="toEntityId">Enter the entity_id for the room to copy the schedule to. eg: climate.wiser_kitchen</param>
		public void CopySchedule(object? @entityId = null, object? @toEntityId = null)
		{
			_haContext.CallService("wiser", "copy_schedule", null, new WiserCopyScheduleParameters{EntityId = @entityId, ToEntityId = @toEntityId});
		}

		///<summary>Read the schedule from a roomId and write to an output file in yaml </summary>
		public void GetSchedule(WiserGetScheduleParameters data)
		{
			_haContext.CallService("wiser", "get_schedule", null, data);
		}

		///<summary>Read the schedule from a roomId and write to an output file in yaml </summary>
		///<param name="entityId">Enter the entity_id for the room to read the schedule. eg: climate.wiser_lounge</param>
		///<param name="filename">The filename to write out the yaml. See README.MD if running within a docker container.  eg: schedule1.yaml</param>
		public void GetSchedule(object? @entityId = null, object? @filename = null)
		{
			_haContext.CallService("wiser", "get_schedule", null, new WiserGetScheduleParameters{EntityId = @entityId, Filename = @filename});
		}

		///<summary>Sets the hot water mode</summary>
		public void SetHotwaterMode(WiserSetHotwaterModeParameters data)
		{
			_haContext.CallService("wiser", "set_hotwater_mode", null, data);
		}

		///<summary>Sets the hot water mode</summary>
		///<param name="hotwaterMode">Enter the mode can be on, off or auto. eg: auto</param>
		public void SetHotwaterMode(object? @hotwaterMode = null)
		{
			_haContext.CallService("wiser", "set_hotwater_mode", null, new WiserSetHotwaterModeParameters{HotwaterMode = @hotwaterMode});
		}

		///<summary>Read in the yaml schedule file and set the schedule in a room</summary>
		public void SetSchedule(WiserSetScheduleParameters data)
		{
			_haContext.CallService("wiser", "set_schedule", null, data);
		}

		///<summary>Read in the yaml schedule file and set the schedule in a room</summary>
		///<param name="entityId">Enter the entity_id for the room to set the schedule. eg: climate.wiser_lounge</param>
		///<param name="filename">The filename to read the yaml schedule from. eg: schedules/schedule1.yaml</param>
		public void SetSchedule(object? @entityId = null, object? @filename = null)
		{
			_haContext.CallService("wiser", "set_schedule", null, new WiserSetScheduleParameters{EntityId = @entityId, Filename = @filename});
		}

		///<summary>Sets the mode of a smart plug</summary>
		public void SetSmartplugMode(WiserSetSmartplugModeParameters data)
		{
			_haContext.CallService("wiser", "set_smartplug_mode", null, data);
		}

		///<summary>Sets the mode of a smart plug</summary>
		///<param name="entityId">Enter the entity_id for the smartplug. eg: switch.smartplug</param>
		///<param name="plugMode">Enter the switch mode. Can be auto or manual. eg: manual</param>
		public void SetSmartplugMode(object? @entityId = null, object? @plugMode = null)
		{
			_haContext.CallService("wiser", "set_smartplug_mode", null, new WiserSetSmartplugModeParameters{EntityId = @entityId, PlugMode = @plugMode});
		}
	}

	public record WiserBoostHeatingParameters
	{
		///<summary>Enter the entity_id for the room required to set the boost mode.  eg: climate.wiser_lounge</summary>
		[JsonPropertyName("entity_id")]
		public object? EntityId { get; init; }

		///<summary>Set the time period for the boost in minutes. eg: 60</summary>
		[JsonPropertyName("time_period")]
		public object? TimePeriod { get; init; }

		///<summary>Set the target temperature for the boost period. eg: 20.5</summary>
		[JsonPropertyName("temperature")]
		public object? Temperature { get; init; }

		///<summary>Set the temperature increase for the boost period. eg: 2</summary>
		[JsonPropertyName("temperature_delta")]
		public object? TemperatureDelta { get; init; }
	}

	public record WiserCopyScheduleParameters
	{
		///<summary>Enter the entity_id for the room to copy the schedule from. eg: climate.wiser_lounge</summary>
		[JsonPropertyName("entity_id")]
		public object? EntityId { get; init; }

		///<summary>Enter the entity_id for the room to copy the schedule to. eg: climate.wiser_kitchen</summary>
		[JsonPropertyName("to_entity_id")]
		public object? ToEntityId { get; init; }
	}

	public record WiserGetScheduleParameters
	{
		///<summary>Enter the entity_id for the room to read the schedule. eg: climate.wiser_lounge</summary>
		[JsonPropertyName("entity_id")]
		public object? EntityId { get; init; }

		///<summary>The filename to write out the yaml. See README.MD if running within a docker container.  eg: schedule1.yaml</summary>
		[JsonPropertyName("filename")]
		public object? Filename { get; init; }
	}

	public record WiserSetHotwaterModeParameters
	{
		///<summary>Enter the mode can be on, off or auto. eg: auto</summary>
		[JsonPropertyName("hotwater_mode")]
		public object? HotwaterMode { get; init; }
	}

	public record WiserSetScheduleParameters
	{
		///<summary>Enter the entity_id for the room to set the schedule. eg: climate.wiser_lounge</summary>
		[JsonPropertyName("entity_id")]
		public object? EntityId { get; init; }

		///<summary>The filename to read the yaml schedule from. eg: schedules/schedule1.yaml</summary>
		[JsonPropertyName("filename")]
		public object? Filename { get; init; }
	}

	public record WiserSetSmartplugModeParameters
	{
		///<summary>Enter the entity_id for the smartplug. eg: switch.smartplug</summary>
		[JsonPropertyName("entity_id")]
		public object? EntityId { get; init; }

		///<summary>Enter the switch mode. Can be auto or manual. eg: manual</summary>
		[JsonPropertyName("plug_mode")]
		public object? PlugMode { get; init; }
	}

	public class ZhaServices
	{
		private readonly IHaContext _haContext;
		public ZhaServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Clear a user code from a lock</summary>
		///<param name="target">The target for this service call</param>
		public void ClearLockUserCode(ServiceTarget target, ZhaClearLockUserCodeParameters data)
		{
			_haContext.CallService("zha", "clear_lock_user_code", target, data);
		}

		///<summary>Clear a user code from a lock</summary>
		///<param name="target">The target for this service call</param>
		///<param name="codeSlot">Code slot to clear code from eg: 1</param>
		public void ClearLockUserCode(ServiceTarget target, string @codeSlot)
		{
			_haContext.CallService("zha", "clear_lock_user_code", target, new ZhaClearLockUserCodeParameters{CodeSlot = @codeSlot});
		}

		///<summary>Disable a user code on a lock</summary>
		///<param name="target">The target for this service call</param>
		public void DisableLockUserCode(ServiceTarget target, ZhaDisableLockUserCodeParameters data)
		{
			_haContext.CallService("zha", "disable_lock_user_code", target, data);
		}

		///<summary>Disable a user code on a lock</summary>
		///<param name="target">The target for this service call</param>
		///<param name="codeSlot">Code slot to disable eg: 1</param>
		public void DisableLockUserCode(ServiceTarget target, string @codeSlot)
		{
			_haContext.CallService("zha", "disable_lock_user_code", target, new ZhaDisableLockUserCodeParameters{CodeSlot = @codeSlot});
		}

		///<summary>Enable a user code on a lock</summary>
		///<param name="target">The target for this service call</param>
		public void EnableLockUserCode(ServiceTarget target, ZhaEnableLockUserCodeParameters data)
		{
			_haContext.CallService("zha", "enable_lock_user_code", target, data);
		}

		///<summary>Enable a user code on a lock</summary>
		///<param name="target">The target for this service call</param>
		///<param name="codeSlot">Code slot to enable eg: 1</param>
		public void EnableLockUserCode(ServiceTarget target, string @codeSlot)
		{
			_haContext.CallService("zha", "enable_lock_user_code", target, new ZhaEnableLockUserCodeParameters{CodeSlot = @codeSlot});
		}

		///<summary>Issue command on the specified cluster on the specified entity.</summary>
		public void IssueZigbeeClusterCommand(ZhaIssueZigbeeClusterCommandParameters data)
		{
			_haContext.CallService("zha", "issue_zigbee_cluster_command", null, data);
		}

		///<summary>Issue command on the specified cluster on the specified entity.</summary>
		///<param name="ieee">IEEE address for the device eg: 00:0d:6f:00:05:7d:2d:34</param>
		///<param name="endpointId">Endpoint id for the cluster</param>
		///<param name="clusterId">ZCL cluster to retrieve attributes for</param>
		///<param name="clusterType">type of the cluster</param>
		///<param name="command">id of the command to execute</param>
		///<param name="commandType">type of the command to execute</param>
		///<param name="args">args to pass to the command eg: [arg1, arg2, argN]</param>
		///<param name="manufacturer">manufacturer code eg: 252</param>
		public void IssueZigbeeClusterCommand(string @ieee, long @endpointId, long @clusterId, long @command, string @commandType, string? @clusterType = null, object? @args = null, string? @manufacturer = null)
		{
			_haContext.CallService("zha", "issue_zigbee_cluster_command", null, new ZhaIssueZigbeeClusterCommandParameters{Ieee = @ieee, EndpointId = @endpointId, ClusterId = @clusterId, ClusterType = @clusterType, Command = @command, CommandType = @commandType, Args = @args, Manufacturer = @manufacturer});
		}

		///<summary>Issue command on the specified cluster on the specified group.</summary>
		public void IssueZigbeeGroupCommand(ZhaIssueZigbeeGroupCommandParameters data)
		{
			_haContext.CallService("zha", "issue_zigbee_group_command", null, data);
		}

		///<summary>Issue command on the specified cluster on the specified group.</summary>
		///<param name="group">Hexadecimal address of the group eg: 546</param>
		///<param name="clusterId">ZCL cluster to send command to</param>
		///<param name="clusterType">type of the cluster</param>
		///<param name="command">id of the command to execute</param>
		///<param name="args">args to pass to the command eg: [arg1, arg2, argN]</param>
		///<param name="manufacturer">manufacturer code eg: 252</param>
		public void IssueZigbeeGroupCommand(string @group, long @clusterId, long @command, string? @clusterType = null, object? @args = null, string? @manufacturer = null)
		{
			_haContext.CallService("zha", "issue_zigbee_group_command", null, new ZhaIssueZigbeeGroupCommandParameters{Group = @group, ClusterId = @clusterId, ClusterType = @clusterType, Command = @command, Args = @args, Manufacturer = @manufacturer});
		}

		///<summary>Allow nodes to join the Zigbee network.</summary>
		public void Permit(ZhaPermitParameters data)
		{
			_haContext.CallService("zha", "permit", null, data);
		}

		///<summary>Allow nodes to join the Zigbee network.</summary>
		///<param name="duration">Time to permit joins, in seconds</param>
		///<param name="ieee">IEEE address of the node permitting new joins eg: 00:0d:6f:00:05:7d:2d:34</param>
		///<param name="sourceIeee">IEEE address of the joining device (must be used with install code) eg: 00:0a:bf:00:01:10:23:35</param>
		///<param name="installCode">Install code of the joining device (must be used with source_ieee) eg: 1234-5678-1234-5678-AABB-CCDD-AABB-CCDD-EEFF</param>
		///<param name="qrCode">value of the QR install code (different between vendors) eg: Z:000D6FFFFED4163B$I:52797BF4A5084DAA8E1712B61741CA024051</param>
		public void Permit(long? @duration = null, string? @ieee = null, string? @sourceIeee = null, string? @installCode = null, string? @qrCode = null)
		{
			_haContext.CallService("zha", "permit", null, new ZhaPermitParameters{Duration = @duration, Ieee = @ieee, SourceIeee = @sourceIeee, InstallCode = @installCode, QrCode = @qrCode});
		}

		///<summary>Remove a node from the Zigbee network.</summary>
		public void Remove(ZhaRemoveParameters data)
		{
			_haContext.CallService("zha", "remove", null, data);
		}

		///<summary>Remove a node from the Zigbee network.</summary>
		///<param name="ieee">IEEE address of the node to remove eg: 00:0d:6f:00:05:7d:2d:34</param>
		public void Remove(string @ieee)
		{
			_haContext.CallService("zha", "remove", null, new ZhaRemoveParameters{Ieee = @ieee});
		}

		///<summary>Set a user code on a lock</summary>
		///<param name="target">The target for this service call</param>
		public void SetLockUserCode(ServiceTarget target, ZhaSetLockUserCodeParameters data)
		{
			_haContext.CallService("zha", "set_lock_user_code", target, data);
		}

		///<summary>Set a user code on a lock</summary>
		///<param name="target">The target for this service call</param>
		///<param name="codeSlot">Code slot to set the code in eg: 1</param>
		///<param name="userCode">Code to set eg: 1234</param>
		public void SetLockUserCode(ServiceTarget target, string @codeSlot, string @userCode)
		{
			_haContext.CallService("zha", "set_lock_user_code", target, new ZhaSetLockUserCodeParameters{CodeSlot = @codeSlot, UserCode = @userCode});
		}

		///<summary>Set attribute value for the specified cluster on the specified entity.</summary>
		public void SetZigbeeClusterAttribute(ZhaSetZigbeeClusterAttributeParameters data)
		{
			_haContext.CallService("zha", "set_zigbee_cluster_attribute", null, data);
		}

		///<summary>Set attribute value for the specified cluster on the specified entity.</summary>
		///<param name="ieee">IEEE address for the device eg: 00:0d:6f:00:05:7d:2d:34</param>
		///<param name="endpointId">Endpoint id for the cluster</param>
		///<param name="clusterId">ZCL cluster to retrieve attributes for</param>
		///<param name="clusterType">type of the cluster</param>
		///<param name="attribute">id of the attribute to set eg: 0</param>
		///<param name="value">value to write to the attribute eg: 1</param>
		///<param name="manufacturer">manufacturer code eg: 252</param>
		public void SetZigbeeClusterAttribute(string @ieee, long @endpointId, long @clusterId, long @attribute, string @value, string? @clusterType = null, string? @manufacturer = null)
		{
			_haContext.CallService("zha", "set_zigbee_cluster_attribute", null, new ZhaSetZigbeeClusterAttributeParameters{Ieee = @ieee, EndpointId = @endpointId, ClusterId = @clusterId, ClusterType = @clusterType, Attribute = @attribute, Value = @value, Manufacturer = @manufacturer});
		}

		///<summary>This service uses the WD capabilities to emit a quick audible/visible pulse called a "squawk". The squawk command has no effect if the WD is currently active (warning in progress).</summary>
		public void WarningDeviceSquawk(ZhaWarningDeviceSquawkParameters data)
		{
			_haContext.CallService("zha", "warning_device_squawk", null, data);
		}

		///<summary>This service uses the WD capabilities to emit a quick audible/visible pulse called a "squawk". The squawk command has no effect if the WD is currently active (warning in progress).</summary>
		///<param name="ieee">IEEE address for the device eg: 00:0d:6f:00:05:7d:2d:34</param>
		///<param name="mode">The Squawk Mode field is used as a 4-bit enumeration, and can have one of the values shown in Table 8-24 of the ZCL spec - Squawk Mode Field. The exact operation of each mode (how the WD “squawks”) is implementation specific.</param>
		///<param name="strobe">The strobe field is used as a Boolean, and determines if the visual indication is also required in addition to the audible squawk, as shown in Table 8-25 of the ZCL spec - Strobe Bit.</param>
		///<param name="level">The squawk level field is used as a 2-bit enumeration, and determines the intensity of audible squawk sound as shown in Table 8-26 of the ZCL spec - Squawk Level Field Values.</param>
		public void WarningDeviceSquawk(string @ieee, long? @mode = null, long? @strobe = null, long? @level = null)
		{
			_haContext.CallService("zha", "warning_device_squawk", null, new ZhaWarningDeviceSquawkParameters{Ieee = @ieee, Mode = @mode, Strobe = @strobe, Level = @level});
		}

		///<summary>This service starts the WD operation. The WD alerts the surrounding area by audible (siren) and visual (strobe) signals.</summary>
		public void WarningDeviceWarn(ZhaWarningDeviceWarnParameters data)
		{
			_haContext.CallService("zha", "warning_device_warn", null, data);
		}

		///<summary>This service starts the WD operation. The WD alerts the surrounding area by audible (siren) and visual (strobe) signals.</summary>
		///<param name="ieee">IEEE address for the device eg: 00:0d:6f:00:05:7d:2d:34</param>
		///<param name="mode">The Warning Mode field is used as an 4-bit enumeration, can have one of the values 0-6 defined below in table 8-20 of the ZCL spec. The exact behavior of the WD device in each mode is according to the relevant security standards.</param>
		///<param name="strobe">The Strobe field is used as a 2-bit enumeration, and determines if the visual indication is required in addition to the audible siren, as indicated in Table 8-21 of the ZCL spec. "0" means no strobe, "1" means strobe. If the strobe field is “1” and the Warning Mode is “0” (“Stop”) then only the strobe is activated.</param>
		///<param name="level">The Siren Level field is used as a 2-bit enumeration, and indicates the intensity of audible squawk sound as shown in Table 8-22 of the ZCL spec.</param>
		///<param name="duration">Requested duration of warning, in seconds (16 bit). If both Strobe and Warning Mode are "0" this field SHALL be ignored.</param>
		///<param name="dutyCycle">Indicates the length of the flash cycle. This provides a means of varying the flash duration for different alarm types (e.g., fire, police, burglar). Valid range is 0-100 in increments of 10. All other values SHALL be rounded to the nearest valid value. Strobe SHALL calculate duty cycle over a duration of one second. The ON state SHALL precede the OFF state. For example, if Strobe Duty Cycle Field specifies “40,” then the strobe SHALL flash ON for 4/10ths of a second and then turn OFF for 6/10ths of a second.</param>
		///<param name="intensity">Indicates the intensity of the strobe as shown in Table 8-23 of the ZCL spec. This attribute is designed to vary the output of the strobe (i.e., brightness) and not its frequency, which is detailed in section 8.4.2.3.1.6 of the ZCL spec.</param>
		public void WarningDeviceWarn(string @ieee, long? @mode = null, long? @strobe = null, long? @level = null, long? @duration = null, long? @dutyCycle = null, long? @intensity = null)
		{
			_haContext.CallService("zha", "warning_device_warn", null, new ZhaWarningDeviceWarnParameters{Ieee = @ieee, Mode = @mode, Strobe = @strobe, Level = @level, Duration = @duration, DutyCycle = @dutyCycle, Intensity = @intensity});
		}
	}

	public record ZhaClearLockUserCodeParameters
	{
		///<summary>Code slot to clear code from eg: 1</summary>
		[JsonPropertyName("code_slot")]
		public string? CodeSlot { get; init; }
	}

	public record ZhaDisableLockUserCodeParameters
	{
		///<summary>Code slot to disable eg: 1</summary>
		[JsonPropertyName("code_slot")]
		public string? CodeSlot { get; init; }
	}

	public record ZhaEnableLockUserCodeParameters
	{
		///<summary>Code slot to enable eg: 1</summary>
		[JsonPropertyName("code_slot")]
		public string? CodeSlot { get; init; }
	}

	public record ZhaIssueZigbeeClusterCommandParameters
	{
		///<summary>IEEE address for the device eg: 00:0d:6f:00:05:7d:2d:34</summary>
		[JsonPropertyName("ieee")]
		public string? Ieee { get; init; }

		///<summary>Endpoint id for the cluster</summary>
		[JsonPropertyName("endpoint_id")]
		public long? EndpointId { get; init; }

		///<summary>ZCL cluster to retrieve attributes for</summary>
		[JsonPropertyName("cluster_id")]
		public long? ClusterId { get; init; }

		///<summary>type of the cluster</summary>
		[JsonPropertyName("cluster_type")]
		public string? ClusterType { get; init; }

		///<summary>id of the command to execute</summary>
		[JsonPropertyName("command")]
		public long? Command { get; init; }

		///<summary>type of the command to execute</summary>
		[JsonPropertyName("command_type")]
		public string? CommandType { get; init; }

		///<summary>args to pass to the command eg: [arg1, arg2, argN]</summary>
		[JsonPropertyName("args")]
		public object? Args { get; init; }

		///<summary>manufacturer code eg: 252</summary>
		[JsonPropertyName("manufacturer")]
		public string? Manufacturer { get; init; }
	}

	public record ZhaIssueZigbeeGroupCommandParameters
	{
		///<summary>Hexadecimal address of the group eg: 546</summary>
		[JsonPropertyName("group")]
		public string? Group { get; init; }

		///<summary>ZCL cluster to send command to</summary>
		[JsonPropertyName("cluster_id")]
		public long? ClusterId { get; init; }

		///<summary>type of the cluster</summary>
		[JsonPropertyName("cluster_type")]
		public string? ClusterType { get; init; }

		///<summary>id of the command to execute</summary>
		[JsonPropertyName("command")]
		public long? Command { get; init; }

		///<summary>args to pass to the command eg: [arg1, arg2, argN]</summary>
		[JsonPropertyName("args")]
		public object? Args { get; init; }

		///<summary>manufacturer code eg: 252</summary>
		[JsonPropertyName("manufacturer")]
		public string? Manufacturer { get; init; }
	}

	public record ZhaPermitParameters
	{
		///<summary>Time to permit joins, in seconds</summary>
		[JsonPropertyName("duration")]
		public long? Duration { get; init; }

		///<summary>IEEE address of the node permitting new joins eg: 00:0d:6f:00:05:7d:2d:34</summary>
		[JsonPropertyName("ieee")]
		public string? Ieee { get; init; }

		///<summary>IEEE address of the joining device (must be used with install code) eg: 00:0a:bf:00:01:10:23:35</summary>
		[JsonPropertyName("source_ieee")]
		public string? SourceIeee { get; init; }

		///<summary>Install code of the joining device (must be used with source_ieee) eg: 1234-5678-1234-5678-AABB-CCDD-AABB-CCDD-EEFF</summary>
		[JsonPropertyName("install_code")]
		public string? InstallCode { get; init; }

		///<summary>value of the QR install code (different between vendors) eg: Z:000D6FFFFED4163B$I:52797BF4A5084DAA8E1712B61741CA024051</summary>
		[JsonPropertyName("qr_code")]
		public string? QrCode { get; init; }
	}

	public record ZhaRemoveParameters
	{
		///<summary>IEEE address of the node to remove eg: 00:0d:6f:00:05:7d:2d:34</summary>
		[JsonPropertyName("ieee")]
		public string? Ieee { get; init; }
	}

	public record ZhaSetLockUserCodeParameters
	{
		///<summary>Code slot to set the code in eg: 1</summary>
		[JsonPropertyName("code_slot")]
		public string? CodeSlot { get; init; }

		///<summary>Code to set eg: 1234</summary>
		[JsonPropertyName("user_code")]
		public string? UserCode { get; init; }
	}

	public record ZhaSetZigbeeClusterAttributeParameters
	{
		///<summary>IEEE address for the device eg: 00:0d:6f:00:05:7d:2d:34</summary>
		[JsonPropertyName("ieee")]
		public string? Ieee { get; init; }

		///<summary>Endpoint id for the cluster</summary>
		[JsonPropertyName("endpoint_id")]
		public long? EndpointId { get; init; }

		///<summary>ZCL cluster to retrieve attributes for</summary>
		[JsonPropertyName("cluster_id")]
		public long? ClusterId { get; init; }

		///<summary>type of the cluster</summary>
		[JsonPropertyName("cluster_type")]
		public string? ClusterType { get; init; }

		///<summary>id of the attribute to set eg: 0</summary>
		[JsonPropertyName("attribute")]
		public long? Attribute { get; init; }

		///<summary>value to write to the attribute eg: 1</summary>
		[JsonPropertyName("value")]
		public string? Value { get; init; }

		///<summary>manufacturer code eg: 252</summary>
		[JsonPropertyName("manufacturer")]
		public string? Manufacturer { get; init; }
	}

	public record ZhaWarningDeviceSquawkParameters
	{
		///<summary>IEEE address for the device eg: 00:0d:6f:00:05:7d:2d:34</summary>
		[JsonPropertyName("ieee")]
		public string? Ieee { get; init; }

		///<summary>The Squawk Mode field is used as a 4-bit enumeration, and can have one of the values shown in Table 8-24 of the ZCL spec - Squawk Mode Field. The exact operation of each mode (how the WD “squawks”) is implementation specific.</summary>
		[JsonPropertyName("mode")]
		public long? Mode { get; init; }

		///<summary>The strobe field is used as a Boolean, and determines if the visual indication is also required in addition to the audible squawk, as shown in Table 8-25 of the ZCL spec - Strobe Bit.</summary>
		[JsonPropertyName("strobe")]
		public long? Strobe { get; init; }

		///<summary>The squawk level field is used as a 2-bit enumeration, and determines the intensity of audible squawk sound as shown in Table 8-26 of the ZCL spec - Squawk Level Field Values.</summary>
		[JsonPropertyName("level")]
		public long? Level { get; init; }
	}

	public record ZhaWarningDeviceWarnParameters
	{
		///<summary>IEEE address for the device eg: 00:0d:6f:00:05:7d:2d:34</summary>
		[JsonPropertyName("ieee")]
		public string? Ieee { get; init; }

		///<summary>The Warning Mode field is used as an 4-bit enumeration, can have one of the values 0-6 defined below in table 8-20 of the ZCL spec. The exact behavior of the WD device in each mode is according to the relevant security standards.</summary>
		[JsonPropertyName("mode")]
		public long? Mode { get; init; }

		///<summary>The Strobe field is used as a 2-bit enumeration, and determines if the visual indication is required in addition to the audible siren, as indicated in Table 8-21 of the ZCL spec. "0" means no strobe, "1" means strobe. If the strobe field is “1” and the Warning Mode is “0” (“Stop”) then only the strobe is activated.</summary>
		[JsonPropertyName("strobe")]
		public long? Strobe { get; init; }

		///<summary>The Siren Level field is used as a 2-bit enumeration, and indicates the intensity of audible squawk sound as shown in Table 8-22 of the ZCL spec.</summary>
		[JsonPropertyName("level")]
		public long? Level { get; init; }

		///<summary>Requested duration of warning, in seconds (16 bit). If both Strobe and Warning Mode are "0" this field SHALL be ignored.</summary>
		[JsonPropertyName("duration")]
		public long? Duration { get; init; }

		///<summary>Indicates the length of the flash cycle. This provides a means of varying the flash duration for different alarm types (e.g., fire, police, burglar). Valid range is 0-100 in increments of 10. All other values SHALL be rounded to the nearest valid value. Strobe SHALL calculate duty cycle over a duration of one second. The ON state SHALL precede the OFF state. For example, if Strobe Duty Cycle Field specifies “40,” then the strobe SHALL flash ON for 4/10ths of a second and then turn OFF for 6/10ths of a second.</summary>
		[JsonPropertyName("duty_cycle")]
		public long? DutyCycle { get; init; }

		///<summary>Indicates the intensity of the strobe as shown in Table 8-23 of the ZCL spec. This attribute is designed to vary the output of the strobe (i.e., brightness) and not its frequency, which is detailed in section 8.4.2.3.1.6 of the ZCL spec.</summary>
		[JsonPropertyName("intensity")]
		public long? Intensity { get; init; }
	}

	public class ZoneServices
	{
		private readonly IHaContext _haContext;
		public ZoneServices(IHaContext haContext)
		{
			_haContext = haContext;
		}

		///<summary>Reload the YAML-based zone configuration.</summary>
		public void Reload()
		{
			_haContext.CallService("zone", "reload", null);
		}
	}

	public static class AlarmControlPanelEntityExtensionMethods
	{
		///<summary>Send the alarm the command for arm away.</summary>
		public static void AlarmArmAway(this AlarmControlPanelEntity target, AlarmControlPanelAlarmArmAwayParameters data)
		{
			target.CallService("alarm_arm_away", data);
		}

		///<summary>Send the alarm the command for arm away.</summary>
		public static void AlarmArmAway(this IEnumerable<AlarmControlPanelEntity> target, AlarmControlPanelAlarmArmAwayParameters data)
		{
			target.CallService("alarm_arm_away", data);
		}

		///<summary>Send the alarm the command for arm away.</summary>
		///<param name="target">The AlarmControlPanelEntity to call this service for</param>
		///<param name="code">An optional code to arm away the alarm control panel with. eg: 1234</param>
		public static void AlarmArmAway(this AlarmControlPanelEntity target, string? @code = null)
		{
			target.CallService("alarm_arm_away", new AlarmControlPanelAlarmArmAwayParameters{Code = @code});
		}

		///<summary>Send the alarm the command for arm away.</summary>
		///<param name="target">The IEnumerable<AlarmControlPanelEntity> to call this service for</param>
		///<param name="code">An optional code to arm away the alarm control panel with. eg: 1234</param>
		public static void AlarmArmAway(this IEnumerable<AlarmControlPanelEntity> target, string? @code = null)
		{
			target.CallService("alarm_arm_away", new AlarmControlPanelAlarmArmAwayParameters{Code = @code});
		}

		///<summary>Send arm custom bypass command.</summary>
		public static void AlarmArmCustomBypass(this AlarmControlPanelEntity target, AlarmControlPanelAlarmArmCustomBypassParameters data)
		{
			target.CallService("alarm_arm_custom_bypass", data);
		}

		///<summary>Send arm custom bypass command.</summary>
		public static void AlarmArmCustomBypass(this IEnumerable<AlarmControlPanelEntity> target, AlarmControlPanelAlarmArmCustomBypassParameters data)
		{
			target.CallService("alarm_arm_custom_bypass", data);
		}

		///<summary>Send arm custom bypass command.</summary>
		///<param name="target">The AlarmControlPanelEntity to call this service for</param>
		///<param name="code">An optional code to arm custom bypass the alarm control panel with. eg: 1234</param>
		public static void AlarmArmCustomBypass(this AlarmControlPanelEntity target, string? @code = null)
		{
			target.CallService("alarm_arm_custom_bypass", new AlarmControlPanelAlarmArmCustomBypassParameters{Code = @code});
		}

		///<summary>Send arm custom bypass command.</summary>
		///<param name="target">The IEnumerable<AlarmControlPanelEntity> to call this service for</param>
		///<param name="code">An optional code to arm custom bypass the alarm control panel with. eg: 1234</param>
		public static void AlarmArmCustomBypass(this IEnumerable<AlarmControlPanelEntity> target, string? @code = null)
		{
			target.CallService("alarm_arm_custom_bypass", new AlarmControlPanelAlarmArmCustomBypassParameters{Code = @code});
		}

		///<summary>Send the alarm the command for arm home.</summary>
		public static void AlarmArmHome(this AlarmControlPanelEntity target, AlarmControlPanelAlarmArmHomeParameters data)
		{
			target.CallService("alarm_arm_home", data);
		}

		///<summary>Send the alarm the command for arm home.</summary>
		public static void AlarmArmHome(this IEnumerable<AlarmControlPanelEntity> target, AlarmControlPanelAlarmArmHomeParameters data)
		{
			target.CallService("alarm_arm_home", data);
		}

		///<summary>Send the alarm the command for arm home.</summary>
		///<param name="target">The AlarmControlPanelEntity to call this service for</param>
		///<param name="code">An optional code to arm home the alarm control panel with. eg: 1234</param>
		public static void AlarmArmHome(this AlarmControlPanelEntity target, string? @code = null)
		{
			target.CallService("alarm_arm_home", new AlarmControlPanelAlarmArmHomeParameters{Code = @code});
		}

		///<summary>Send the alarm the command for arm home.</summary>
		///<param name="target">The IEnumerable<AlarmControlPanelEntity> to call this service for</param>
		///<param name="code">An optional code to arm home the alarm control panel with. eg: 1234</param>
		public static void AlarmArmHome(this IEnumerable<AlarmControlPanelEntity> target, string? @code = null)
		{
			target.CallService("alarm_arm_home", new AlarmControlPanelAlarmArmHomeParameters{Code = @code});
		}

		///<summary>Send the alarm the command for arm night.</summary>
		public static void AlarmArmNight(this AlarmControlPanelEntity target, AlarmControlPanelAlarmArmNightParameters data)
		{
			target.CallService("alarm_arm_night", data);
		}

		///<summary>Send the alarm the command for arm night.</summary>
		public static void AlarmArmNight(this IEnumerable<AlarmControlPanelEntity> target, AlarmControlPanelAlarmArmNightParameters data)
		{
			target.CallService("alarm_arm_night", data);
		}

		///<summary>Send the alarm the command for arm night.</summary>
		///<param name="target">The AlarmControlPanelEntity to call this service for</param>
		///<param name="code">An optional code to arm night the alarm control panel with. eg: 1234</param>
		public static void AlarmArmNight(this AlarmControlPanelEntity target, string? @code = null)
		{
			target.CallService("alarm_arm_night", new AlarmControlPanelAlarmArmNightParameters{Code = @code});
		}

		///<summary>Send the alarm the command for arm night.</summary>
		///<param name="target">The IEnumerable<AlarmControlPanelEntity> to call this service for</param>
		///<param name="code">An optional code to arm night the alarm control panel with. eg: 1234</param>
		public static void AlarmArmNight(this IEnumerable<AlarmControlPanelEntity> target, string? @code = null)
		{
			target.CallService("alarm_arm_night", new AlarmControlPanelAlarmArmNightParameters{Code = @code});
		}

		///<summary>Send the alarm the command for arm vacation.</summary>
		public static void AlarmArmVacation(this AlarmControlPanelEntity target, AlarmControlPanelAlarmArmVacationParameters data)
		{
			target.CallService("alarm_arm_vacation", data);
		}

		///<summary>Send the alarm the command for arm vacation.</summary>
		public static void AlarmArmVacation(this IEnumerable<AlarmControlPanelEntity> target, AlarmControlPanelAlarmArmVacationParameters data)
		{
			target.CallService("alarm_arm_vacation", data);
		}

		///<summary>Send the alarm the command for arm vacation.</summary>
		///<param name="target">The AlarmControlPanelEntity to call this service for</param>
		///<param name="code">An optional code to arm vacation the alarm control panel with. eg: 1234</param>
		public static void AlarmArmVacation(this AlarmControlPanelEntity target, string? @code = null)
		{
			target.CallService("alarm_arm_vacation", new AlarmControlPanelAlarmArmVacationParameters{Code = @code});
		}

		///<summary>Send the alarm the command for arm vacation.</summary>
		///<param name="target">The IEnumerable<AlarmControlPanelEntity> to call this service for</param>
		///<param name="code">An optional code to arm vacation the alarm control panel with. eg: 1234</param>
		public static void AlarmArmVacation(this IEnumerable<AlarmControlPanelEntity> target, string? @code = null)
		{
			target.CallService("alarm_arm_vacation", new AlarmControlPanelAlarmArmVacationParameters{Code = @code});
		}

		///<summary>Send the alarm the command for disarm.</summary>
		public static void AlarmDisarm(this AlarmControlPanelEntity target, AlarmControlPanelAlarmDisarmParameters data)
		{
			target.CallService("alarm_disarm", data);
		}

		///<summary>Send the alarm the command for disarm.</summary>
		public static void AlarmDisarm(this IEnumerable<AlarmControlPanelEntity> target, AlarmControlPanelAlarmDisarmParameters data)
		{
			target.CallService("alarm_disarm", data);
		}

		///<summary>Send the alarm the command for disarm.</summary>
		///<param name="target">The AlarmControlPanelEntity to call this service for</param>
		///<param name="code">An optional code to disarm the alarm control panel with. eg: 1234</param>
		public static void AlarmDisarm(this AlarmControlPanelEntity target, string? @code = null)
		{
			target.CallService("alarm_disarm", new AlarmControlPanelAlarmDisarmParameters{Code = @code});
		}

		///<summary>Send the alarm the command for disarm.</summary>
		///<param name="target">The IEnumerable<AlarmControlPanelEntity> to call this service for</param>
		///<param name="code">An optional code to disarm the alarm control panel with. eg: 1234</param>
		public static void AlarmDisarm(this IEnumerable<AlarmControlPanelEntity> target, string? @code = null)
		{
			target.CallService("alarm_disarm", new AlarmControlPanelAlarmDisarmParameters{Code = @code});
		}

		///<summary>Send the alarm the command for trigger.</summary>
		public static void AlarmTrigger(this AlarmControlPanelEntity target, AlarmControlPanelAlarmTriggerParameters data)
		{
			target.CallService("alarm_trigger", data);
		}

		///<summary>Send the alarm the command for trigger.</summary>
		public static void AlarmTrigger(this IEnumerable<AlarmControlPanelEntity> target, AlarmControlPanelAlarmTriggerParameters data)
		{
			target.CallService("alarm_trigger", data);
		}

		///<summary>Send the alarm the command for trigger.</summary>
		///<param name="target">The AlarmControlPanelEntity to call this service for</param>
		///<param name="code">An optional code to trigger the alarm control panel with. eg: 1234</param>
		public static void AlarmTrigger(this AlarmControlPanelEntity target, string? @code = null)
		{
			target.CallService("alarm_trigger", new AlarmControlPanelAlarmTriggerParameters{Code = @code});
		}

		///<summary>Send the alarm the command for trigger.</summary>
		///<param name="target">The IEnumerable<AlarmControlPanelEntity> to call this service for</param>
		///<param name="code">An optional code to trigger the alarm control panel with. eg: 1234</param>
		public static void AlarmTrigger(this IEnumerable<AlarmControlPanelEntity> target, string? @code = null)
		{
			target.CallService("alarm_trigger", new AlarmControlPanelAlarmTriggerParameters{Code = @code});
		}
	}

	public static class AutomationEntityExtensionMethods
	{
		///<summary>Toggle (enable / disable) an automation.</summary>
		public static void Toggle(this AutomationEntity target)
		{
			target.CallService("toggle");
		}

		///<summary>Toggle (enable / disable) an automation.</summary>
		public static void Toggle(this IEnumerable<AutomationEntity> target)
		{
			target.CallService("toggle");
		}

		///<summary>Trigger the actions of an automation.</summary>
		public static void Trigger(this AutomationEntity target, AutomationTriggerParameters data)
		{
			target.CallService("trigger", data);
		}

		///<summary>Trigger the actions of an automation.</summary>
		public static void Trigger(this IEnumerable<AutomationEntity> target, AutomationTriggerParameters data)
		{
			target.CallService("trigger", data);
		}

		///<summary>Trigger the actions of an automation.</summary>
		///<param name="target">The AutomationEntity to call this service for</param>
		///<param name="skipCondition">Whether or not the conditions will be skipped.</param>
		public static void Trigger(this AutomationEntity target, bool? @skipCondition = null)
		{
			target.CallService("trigger", new AutomationTriggerParameters{SkipCondition = @skipCondition});
		}

		///<summary>Trigger the actions of an automation.</summary>
		///<param name="target">The IEnumerable<AutomationEntity> to call this service for</param>
		///<param name="skipCondition">Whether or not the conditions will be skipped.</param>
		public static void Trigger(this IEnumerable<AutomationEntity> target, bool? @skipCondition = null)
		{
			target.CallService("trigger", new AutomationTriggerParameters{SkipCondition = @skipCondition});
		}

		///<summary>Disable an automation.</summary>
		public static void TurnOff(this AutomationEntity target, AutomationTurnOffParameters data)
		{
			target.CallService("turn_off", data);
		}

		///<summary>Disable an automation.</summary>
		public static void TurnOff(this IEnumerable<AutomationEntity> target, AutomationTurnOffParameters data)
		{
			target.CallService("turn_off", data);
		}

		///<summary>Disable an automation.</summary>
		///<param name="target">The AutomationEntity to call this service for</param>
		///<param name="stopActions">Stop currently running actions.</param>
		public static void TurnOff(this AutomationEntity target, bool? @stopActions = null)
		{
			target.CallService("turn_off", new AutomationTurnOffParameters{StopActions = @stopActions});
		}

		///<summary>Disable an automation.</summary>
		///<param name="target">The IEnumerable<AutomationEntity> to call this service for</param>
		///<param name="stopActions">Stop currently running actions.</param>
		public static void TurnOff(this IEnumerable<AutomationEntity> target, bool? @stopActions = null)
		{
			target.CallService("turn_off", new AutomationTurnOffParameters{StopActions = @stopActions});
		}

		///<summary>Enable an automation.</summary>
		public static void TurnOn(this AutomationEntity target)
		{
			target.CallService("turn_on");
		}

		///<summary>Enable an automation.</summary>
		public static void TurnOn(this IEnumerable<AutomationEntity> target)
		{
			target.CallService("turn_on");
		}
	}

	public static class ButtonEntityExtensionMethods
	{
		///<summary>Press the button entity.</summary>
		public static void Press(this ButtonEntity target)
		{
			target.CallService("press");
		}

		///<summary>Press the button entity.</summary>
		public static void Press(this IEnumerable<ButtonEntity> target)
		{
			target.CallService("press");
		}
	}

	public static class CameraEntityExtensionMethods
	{
		///<summary>Disable the motion detection in a camera.</summary>
		public static void DisableMotionDetection(this CameraEntity target)
		{
			target.CallService("disable_motion_detection");
		}

		///<summary>Disable the motion detection in a camera.</summary>
		public static void DisableMotionDetection(this IEnumerable<CameraEntity> target)
		{
			target.CallService("disable_motion_detection");
		}

		///<summary>Enable the motion detection in a camera.</summary>
		public static void EnableMotionDetection(this CameraEntity target)
		{
			target.CallService("enable_motion_detection");
		}

		///<summary>Enable the motion detection in a camera.</summary>
		public static void EnableMotionDetection(this IEnumerable<CameraEntity> target)
		{
			target.CallService("enable_motion_detection");
		}

		///<summary>Play camera stream on supported media player.</summary>
		public static void PlayStream(this CameraEntity target, CameraPlayStreamParameters data)
		{
			target.CallService("play_stream", data);
		}

		///<summary>Play camera stream on supported media player.</summary>
		public static void PlayStream(this IEnumerable<CameraEntity> target, CameraPlayStreamParameters data)
		{
			target.CallService("play_stream", data);
		}

		///<summary>Play camera stream on supported media player.</summary>
		///<param name="target">The CameraEntity to call this service for</param>
		///<param name="mediaPlayer">Name(s) of media player to stream to.</param>
		///<param name="format">Stream format supported by media player.</param>
		public static void PlayStream(this CameraEntity target, string @mediaPlayer, string? @format = null)
		{
			target.CallService("play_stream", new CameraPlayStreamParameters{MediaPlayer = @mediaPlayer, Format = @format});
		}

		///<summary>Play camera stream on supported media player.</summary>
		///<param name="target">The IEnumerable<CameraEntity> to call this service for</param>
		///<param name="mediaPlayer">Name(s) of media player to stream to.</param>
		///<param name="format">Stream format supported by media player.</param>
		public static void PlayStream(this IEnumerable<CameraEntity> target, string @mediaPlayer, string? @format = null)
		{
			target.CallService("play_stream", new CameraPlayStreamParameters{MediaPlayer = @mediaPlayer, Format = @format});
		}

		///<summary>Record live camera feed.</summary>
		public static void Record(this CameraEntity target, CameraRecordParameters data)
		{
			target.CallService("record", data);
		}

		///<summary>Record live camera feed.</summary>
		public static void Record(this IEnumerable<CameraEntity> target, CameraRecordParameters data)
		{
			target.CallService("record", data);
		}

		///<summary>Record live camera feed.</summary>
		///<param name="target">The CameraEntity to call this service for</param>
		///<param name="filename">Template of a Filename. Variable is entity_id. Must be mp4. eg: /tmp/snapshot_{{ entity_id.name }}.mp4</param>
		///<param name="duration">Target recording length.</param>
		///<param name="lookback">Target lookback period to include in addition to duration. Only available if there is currently an active HLS stream.</param>
		public static void Record(this CameraEntity target, string @filename, long? @duration = null, long? @lookback = null)
		{
			target.CallService("record", new CameraRecordParameters{Filename = @filename, Duration = @duration, Lookback = @lookback});
		}

		///<summary>Record live camera feed.</summary>
		///<param name="target">The IEnumerable<CameraEntity> to call this service for</param>
		///<param name="filename">Template of a Filename. Variable is entity_id. Must be mp4. eg: /tmp/snapshot_{{ entity_id.name }}.mp4</param>
		///<param name="duration">Target recording length.</param>
		///<param name="lookback">Target lookback period to include in addition to duration. Only available if there is currently an active HLS stream.</param>
		public static void Record(this IEnumerable<CameraEntity> target, string @filename, long? @duration = null, long? @lookback = null)
		{
			target.CallService("record", new CameraRecordParameters{Filename = @filename, Duration = @duration, Lookback = @lookback});
		}

		///<summary>Take a snapshot from a camera.</summary>
		public static void Snapshot(this CameraEntity target, CameraSnapshotParameters data)
		{
			target.CallService("snapshot", data);
		}

		///<summary>Take a snapshot from a camera.</summary>
		public static void Snapshot(this IEnumerable<CameraEntity> target, CameraSnapshotParameters data)
		{
			target.CallService("snapshot", data);
		}

		///<summary>Take a snapshot from a camera.</summary>
		///<param name="target">The CameraEntity to call this service for</param>
		///<param name="filename">Template of a Filename. Variable is entity_id. eg: /tmp/snapshot_{{ entity_id.name }}.jpg</param>
		public static void Snapshot(this CameraEntity target, string @filename)
		{
			target.CallService("snapshot", new CameraSnapshotParameters{Filename = @filename});
		}

		///<summary>Take a snapshot from a camera.</summary>
		///<param name="target">The IEnumerable<CameraEntity> to call this service for</param>
		///<param name="filename">Template of a Filename. Variable is entity_id. eg: /tmp/snapshot_{{ entity_id.name }}.jpg</param>
		public static void Snapshot(this IEnumerable<CameraEntity> target, string @filename)
		{
			target.CallService("snapshot", new CameraSnapshotParameters{Filename = @filename});
		}

		///<summary>Turn off camera.</summary>
		public static void TurnOff(this CameraEntity target)
		{
			target.CallService("turn_off");
		}

		///<summary>Turn off camera.</summary>
		public static void TurnOff(this IEnumerable<CameraEntity> target)
		{
			target.CallService("turn_off");
		}

		///<summary>Turn on camera.</summary>
		public static void TurnOn(this CameraEntity target)
		{
			target.CallService("turn_on");
		}

		///<summary>Turn on camera.</summary>
		public static void TurnOn(this IEnumerable<CameraEntity> target)
		{
			target.CallService("turn_on");
		}
	}

	public static class ClimateEntityExtensionMethods
	{
		///<summary>Turn auxiliary heater on/off for climate device.</summary>
		public static void SetAuxHeat(this ClimateEntity target, ClimateSetAuxHeatParameters data)
		{
			target.CallService("set_aux_heat", data);
		}

		///<summary>Turn auxiliary heater on/off for climate device.</summary>
		public static void SetAuxHeat(this IEnumerable<ClimateEntity> target, ClimateSetAuxHeatParameters data)
		{
			target.CallService("set_aux_heat", data);
		}

		///<summary>Turn auxiliary heater on/off for climate device.</summary>
		///<param name="target">The ClimateEntity to call this service for</param>
		///<param name="auxHeat">New value of auxiliary heater.</param>
		public static void SetAuxHeat(this ClimateEntity target, bool @auxHeat)
		{
			target.CallService("set_aux_heat", new ClimateSetAuxHeatParameters{AuxHeat = @auxHeat});
		}

		///<summary>Turn auxiliary heater on/off for climate device.</summary>
		///<param name="target">The IEnumerable<ClimateEntity> to call this service for</param>
		///<param name="auxHeat">New value of auxiliary heater.</param>
		public static void SetAuxHeat(this IEnumerable<ClimateEntity> target, bool @auxHeat)
		{
			target.CallService("set_aux_heat", new ClimateSetAuxHeatParameters{AuxHeat = @auxHeat});
		}

		///<summary>Set fan operation for climate device.</summary>
		public static void SetFanMode(this ClimateEntity target, ClimateSetFanModeParameters data)
		{
			target.CallService("set_fan_mode", data);
		}

		///<summary>Set fan operation for climate device.</summary>
		public static void SetFanMode(this IEnumerable<ClimateEntity> target, ClimateSetFanModeParameters data)
		{
			target.CallService("set_fan_mode", data);
		}

		///<summary>Set fan operation for climate device.</summary>
		///<param name="target">The ClimateEntity to call this service for</param>
		///<param name="fanMode">New value of fan mode. eg: low</param>
		public static void SetFanMode(this ClimateEntity target, string @fanMode)
		{
			target.CallService("set_fan_mode", new ClimateSetFanModeParameters{FanMode = @fanMode});
		}

		///<summary>Set fan operation for climate device.</summary>
		///<param name="target">The IEnumerable<ClimateEntity> to call this service for</param>
		///<param name="fanMode">New value of fan mode. eg: low</param>
		public static void SetFanMode(this IEnumerable<ClimateEntity> target, string @fanMode)
		{
			target.CallService("set_fan_mode", new ClimateSetFanModeParameters{FanMode = @fanMode});
		}

		///<summary>Set target humidity of climate device.</summary>
		public static void SetHumidity(this ClimateEntity target, ClimateSetHumidityParameters data)
		{
			target.CallService("set_humidity", data);
		}

		///<summary>Set target humidity of climate device.</summary>
		public static void SetHumidity(this IEnumerable<ClimateEntity> target, ClimateSetHumidityParameters data)
		{
			target.CallService("set_humidity", data);
		}

		///<summary>Set target humidity of climate device.</summary>
		///<param name="target">The ClimateEntity to call this service for</param>
		///<param name="humidity">New target humidity for climate device.</param>
		public static void SetHumidity(this ClimateEntity target, long @humidity)
		{
			target.CallService("set_humidity", new ClimateSetHumidityParameters{Humidity = @humidity});
		}

		///<summary>Set target humidity of climate device.</summary>
		///<param name="target">The IEnumerable<ClimateEntity> to call this service for</param>
		///<param name="humidity">New target humidity for climate device.</param>
		public static void SetHumidity(this IEnumerable<ClimateEntity> target, long @humidity)
		{
			target.CallService("set_humidity", new ClimateSetHumidityParameters{Humidity = @humidity});
		}

		///<summary>Set HVAC operation mode for climate device.</summary>
		public static void SetHvacMode(this ClimateEntity target, ClimateSetHvacModeParameters data)
		{
			target.CallService("set_hvac_mode", data);
		}

		///<summary>Set HVAC operation mode for climate device.</summary>
		public static void SetHvacMode(this IEnumerable<ClimateEntity> target, ClimateSetHvacModeParameters data)
		{
			target.CallService("set_hvac_mode", data);
		}

		///<summary>Set HVAC operation mode for climate device.</summary>
		///<param name="target">The ClimateEntity to call this service for</param>
		///<param name="hvacMode">New value of operation mode.</param>
		public static void SetHvacMode(this ClimateEntity target, string? @hvacMode = null)
		{
			target.CallService("set_hvac_mode", new ClimateSetHvacModeParameters{HvacMode = @hvacMode});
		}

		///<summary>Set HVAC operation mode for climate device.</summary>
		///<param name="target">The IEnumerable<ClimateEntity> to call this service for</param>
		///<param name="hvacMode">New value of operation mode.</param>
		public static void SetHvacMode(this IEnumerable<ClimateEntity> target, string? @hvacMode = null)
		{
			target.CallService("set_hvac_mode", new ClimateSetHvacModeParameters{HvacMode = @hvacMode});
		}

		///<summary>Set preset mode for climate device.</summary>
		public static void SetPresetMode(this ClimateEntity target, ClimateSetPresetModeParameters data)
		{
			target.CallService("set_preset_mode", data);
		}

		///<summary>Set preset mode for climate device.</summary>
		public static void SetPresetMode(this IEnumerable<ClimateEntity> target, ClimateSetPresetModeParameters data)
		{
			target.CallService("set_preset_mode", data);
		}

		///<summary>Set preset mode for climate device.</summary>
		///<param name="target">The ClimateEntity to call this service for</param>
		///<param name="presetMode">New value of preset mode. eg: away</param>
		public static void SetPresetMode(this ClimateEntity target, string @presetMode)
		{
			target.CallService("set_preset_mode", new ClimateSetPresetModeParameters{PresetMode = @presetMode});
		}

		///<summary>Set preset mode for climate device.</summary>
		///<param name="target">The IEnumerable<ClimateEntity> to call this service for</param>
		///<param name="presetMode">New value of preset mode. eg: away</param>
		public static void SetPresetMode(this IEnumerable<ClimateEntity> target, string @presetMode)
		{
			target.CallService("set_preset_mode", new ClimateSetPresetModeParameters{PresetMode = @presetMode});
		}

		///<summary>Set swing operation for climate device.</summary>
		public static void SetSwingMode(this ClimateEntity target, ClimateSetSwingModeParameters data)
		{
			target.CallService("set_swing_mode", data);
		}

		///<summary>Set swing operation for climate device.</summary>
		public static void SetSwingMode(this IEnumerable<ClimateEntity> target, ClimateSetSwingModeParameters data)
		{
			target.CallService("set_swing_mode", data);
		}

		///<summary>Set swing operation for climate device.</summary>
		///<param name="target">The ClimateEntity to call this service for</param>
		///<param name="swingMode">New value of swing mode. eg: horizontal</param>
		public static void SetSwingMode(this ClimateEntity target, string @swingMode)
		{
			target.CallService("set_swing_mode", new ClimateSetSwingModeParameters{SwingMode = @swingMode});
		}

		///<summary>Set swing operation for climate device.</summary>
		///<param name="target">The IEnumerable<ClimateEntity> to call this service for</param>
		///<param name="swingMode">New value of swing mode. eg: horizontal</param>
		public static void SetSwingMode(this IEnumerable<ClimateEntity> target, string @swingMode)
		{
			target.CallService("set_swing_mode", new ClimateSetSwingModeParameters{SwingMode = @swingMode});
		}

		///<summary>Set target temperature of climate device.</summary>
		public static void SetTemperature(this ClimateEntity target, ClimateSetTemperatureParameters data)
		{
			target.CallService("set_temperature", data);
		}

		///<summary>Set target temperature of climate device.</summary>
		public static void SetTemperature(this IEnumerable<ClimateEntity> target, ClimateSetTemperatureParameters data)
		{
			target.CallService("set_temperature", data);
		}

		///<summary>Set target temperature of climate device.</summary>
		///<param name="target">The ClimateEntity to call this service for</param>
		///<param name="temperature">New target temperature for HVAC.</param>
		///<param name="targetTempHigh">New target high temperature for HVAC.</param>
		///<param name="targetTempLow">New target low temperature for HVAC.</param>
		///<param name="hvacMode">HVAC operation mode to set temperature to.</param>
		public static void SetTemperature(this ClimateEntity target, double? @temperature = null, double? @targetTempHigh = null, double? @targetTempLow = null, string? @hvacMode = null)
		{
			target.CallService("set_temperature", new ClimateSetTemperatureParameters{Temperature = @temperature, TargetTempHigh = @targetTempHigh, TargetTempLow = @targetTempLow, HvacMode = @hvacMode});
		}

		///<summary>Set target temperature of climate device.</summary>
		///<param name="target">The IEnumerable<ClimateEntity> to call this service for</param>
		///<param name="temperature">New target temperature for HVAC.</param>
		///<param name="targetTempHigh">New target high temperature for HVAC.</param>
		///<param name="targetTempLow">New target low temperature for HVAC.</param>
		///<param name="hvacMode">HVAC operation mode to set temperature to.</param>
		public static void SetTemperature(this IEnumerable<ClimateEntity> target, double? @temperature = null, double? @targetTempHigh = null, double? @targetTempLow = null, string? @hvacMode = null)
		{
			target.CallService("set_temperature", new ClimateSetTemperatureParameters{Temperature = @temperature, TargetTempHigh = @targetTempHigh, TargetTempLow = @targetTempLow, HvacMode = @hvacMode});
		}

		///<summary>Turn climate device off.</summary>
		public static void TurnOff(this ClimateEntity target)
		{
			target.CallService("turn_off");
		}

		///<summary>Turn climate device off.</summary>
		public static void TurnOff(this IEnumerable<ClimateEntity> target)
		{
			target.CallService("turn_off");
		}

		///<summary>Turn climate device on.</summary>
		public static void TurnOn(this ClimateEntity target)
		{
			target.CallService("turn_on");
		}

		///<summary>Turn climate device on.</summary>
		public static void TurnOn(this IEnumerable<ClimateEntity> target)
		{
			target.CallService("turn_on");
		}
	}

	public static class CoverEntityExtensionMethods
	{
		///<summary>Close all or specified cover.</summary>
		public static void CloseCover(this CoverEntity target)
		{
			target.CallService("close_cover");
		}

		///<summary>Close all or specified cover.</summary>
		public static void CloseCover(this IEnumerable<CoverEntity> target)
		{
			target.CallService("close_cover");
		}

		///<summary>Close all or specified cover tilt.</summary>
		public static void CloseCoverTilt(this CoverEntity target)
		{
			target.CallService("close_cover_tilt");
		}

		///<summary>Close all or specified cover tilt.</summary>
		public static void CloseCoverTilt(this IEnumerable<CoverEntity> target)
		{
			target.CallService("close_cover_tilt");
		}

		///<summary>Open all or specified cover.</summary>
		public static void OpenCover(this CoverEntity target)
		{
			target.CallService("open_cover");
		}

		///<summary>Open all or specified cover.</summary>
		public static void OpenCover(this IEnumerable<CoverEntity> target)
		{
			target.CallService("open_cover");
		}

		///<summary>Open all or specified cover tilt.</summary>
		public static void OpenCoverTilt(this CoverEntity target)
		{
			target.CallService("open_cover_tilt");
		}

		///<summary>Open all or specified cover tilt.</summary>
		public static void OpenCoverTilt(this IEnumerable<CoverEntity> target)
		{
			target.CallService("open_cover_tilt");
		}

		///<summary>Move to specific position all or specified cover.</summary>
		public static void SetCoverPosition(this CoverEntity target, CoverSetCoverPositionParameters data)
		{
			target.CallService("set_cover_position", data);
		}

		///<summary>Move to specific position all or specified cover.</summary>
		public static void SetCoverPosition(this IEnumerable<CoverEntity> target, CoverSetCoverPositionParameters data)
		{
			target.CallService("set_cover_position", data);
		}

		///<summary>Move to specific position all or specified cover.</summary>
		///<param name="target">The CoverEntity to call this service for</param>
		///<param name="position">Position of the cover</param>
		public static void SetCoverPosition(this CoverEntity target, long @position)
		{
			target.CallService("set_cover_position", new CoverSetCoverPositionParameters{Position = @position});
		}

		///<summary>Move to specific position all or specified cover.</summary>
		///<param name="target">The IEnumerable<CoverEntity> to call this service for</param>
		///<param name="position">Position of the cover</param>
		public static void SetCoverPosition(this IEnumerable<CoverEntity> target, long @position)
		{
			target.CallService("set_cover_position", new CoverSetCoverPositionParameters{Position = @position});
		}

		///<summary>Move to specific position all or specified cover tilt.</summary>
		public static void SetCoverTiltPosition(this CoverEntity target, CoverSetCoverTiltPositionParameters data)
		{
			target.CallService("set_cover_tilt_position", data);
		}

		///<summary>Move to specific position all or specified cover tilt.</summary>
		public static void SetCoverTiltPosition(this IEnumerable<CoverEntity> target, CoverSetCoverTiltPositionParameters data)
		{
			target.CallService("set_cover_tilt_position", data);
		}

		///<summary>Move to specific position all or specified cover tilt.</summary>
		///<param name="target">The CoverEntity to call this service for</param>
		///<param name="tiltPosition">Tilt position of the cover.</param>
		public static void SetCoverTiltPosition(this CoverEntity target, long @tiltPosition)
		{
			target.CallService("set_cover_tilt_position", new CoverSetCoverTiltPositionParameters{TiltPosition = @tiltPosition});
		}

		///<summary>Move to specific position all or specified cover tilt.</summary>
		///<param name="target">The IEnumerable<CoverEntity> to call this service for</param>
		///<param name="tiltPosition">Tilt position of the cover.</param>
		public static void SetCoverTiltPosition(this IEnumerable<CoverEntity> target, long @tiltPosition)
		{
			target.CallService("set_cover_tilt_position", new CoverSetCoverTiltPositionParameters{TiltPosition = @tiltPosition});
		}

		///<summary>Stop all or specified cover.</summary>
		public static void StopCover(this CoverEntity target)
		{
			target.CallService("stop_cover");
		}

		///<summary>Stop all or specified cover.</summary>
		public static void StopCover(this IEnumerable<CoverEntity> target)
		{
			target.CallService("stop_cover");
		}

		///<summary>Stop all or specified cover.</summary>
		public static void StopCoverTilt(this CoverEntity target)
		{
			target.CallService("stop_cover_tilt");
		}

		///<summary>Stop all or specified cover.</summary>
		public static void StopCoverTilt(this IEnumerable<CoverEntity> target)
		{
			target.CallService("stop_cover_tilt");
		}

		///<summary>Toggle a cover open/closed.</summary>
		public static void Toggle(this CoverEntity target)
		{
			target.CallService("toggle");
		}

		///<summary>Toggle a cover open/closed.</summary>
		public static void Toggle(this IEnumerable<CoverEntity> target)
		{
			target.CallService("toggle");
		}

		///<summary>Toggle a cover tilt open/closed.</summary>
		public static void ToggleCoverTilt(this CoverEntity target)
		{
			target.CallService("toggle_cover_tilt");
		}

		///<summary>Toggle a cover tilt open/closed.</summary>
		public static void ToggleCoverTilt(this IEnumerable<CoverEntity> target)
		{
			target.CallService("toggle_cover_tilt");
		}
	}

	public static class InputBooleanEntityExtensionMethods
	{
		///<summary>Toggle an input boolean</summary>
		public static void Toggle(this InputBooleanEntity target)
		{
			target.CallService("toggle");
		}

		///<summary>Toggle an input boolean</summary>
		public static void Toggle(this IEnumerable<InputBooleanEntity> target)
		{
			target.CallService("toggle");
		}

		///<summary>Turn off an input boolean</summary>
		public static void TurnOff(this InputBooleanEntity target)
		{
			target.CallService("turn_off");
		}

		///<summary>Turn off an input boolean</summary>
		public static void TurnOff(this IEnumerable<InputBooleanEntity> target)
		{
			target.CallService("turn_off");
		}

		///<summary>Turn on an input boolean</summary>
		public static void TurnOn(this InputBooleanEntity target)
		{
			target.CallService("turn_on");
		}

		///<summary>Turn on an input boolean</summary>
		public static void TurnOn(this IEnumerable<InputBooleanEntity> target)
		{
			target.CallService("turn_on");
		}
	}

	public static class InputNumberEntityExtensionMethods
	{
		///<summary>Decrement the value of an input number entity by its stepping.</summary>
		public static void Decrement(this InputNumberEntity target)
		{
			target.CallService("decrement");
		}

		///<summary>Decrement the value of an input number entity by its stepping.</summary>
		public static void Decrement(this IEnumerable<InputNumberEntity> target)
		{
			target.CallService("decrement");
		}

		///<summary>Increment the value of an input number entity by its stepping.</summary>
		public static void Increment(this InputNumberEntity target)
		{
			target.CallService("increment");
		}

		///<summary>Increment the value of an input number entity by its stepping.</summary>
		public static void Increment(this IEnumerable<InputNumberEntity> target)
		{
			target.CallService("increment");
		}

		///<summary>Set the value of an input number entity.</summary>
		public static void SetValue(this InputNumberEntity target, InputNumberSetValueParameters data)
		{
			target.CallService("set_value", data);
		}

		///<summary>Set the value of an input number entity.</summary>
		public static void SetValue(this IEnumerable<InputNumberEntity> target, InputNumberSetValueParameters data)
		{
			target.CallService("set_value", data);
		}

		///<summary>Set the value of an input number entity.</summary>
		///<param name="target">The InputNumberEntity to call this service for</param>
		///<param name="value">The target value the entity should be set to.</param>
		public static void SetValue(this InputNumberEntity target, double @value)
		{
			target.CallService("set_value", new InputNumberSetValueParameters{Value = @value});
		}

		///<summary>Set the value of an input number entity.</summary>
		///<param name="target">The IEnumerable<InputNumberEntity> to call this service for</param>
		///<param name="value">The target value the entity should be set to.</param>
		public static void SetValue(this IEnumerable<InputNumberEntity> target, double @value)
		{
			target.CallService("set_value", new InputNumberSetValueParameters{Value = @value});
		}
	}

	public static class InputSelectEntityExtensionMethods
	{
		///<summary>Select the first option of an input select entity.</summary>
		public static void SelectFirst(this InputSelectEntity target)
		{
			target.CallService("select_first");
		}

		///<summary>Select the first option of an input select entity.</summary>
		public static void SelectFirst(this IEnumerable<InputSelectEntity> target)
		{
			target.CallService("select_first");
		}

		///<summary>Select the last option of an input select entity.</summary>
		public static void SelectLast(this InputSelectEntity target)
		{
			target.CallService("select_last");
		}

		///<summary>Select the last option of an input select entity.</summary>
		public static void SelectLast(this IEnumerable<InputSelectEntity> target)
		{
			target.CallService("select_last");
		}

		///<summary>Select the next options of an input select entity.</summary>
		public static void SelectNext(this InputSelectEntity target, InputSelectSelectNextParameters data)
		{
			target.CallService("select_next", data);
		}

		///<summary>Select the next options of an input select entity.</summary>
		public static void SelectNext(this IEnumerable<InputSelectEntity> target, InputSelectSelectNextParameters data)
		{
			target.CallService("select_next", data);
		}

		///<summary>Select the next options of an input select entity.</summary>
		///<param name="target">The InputSelectEntity to call this service for</param>
		///<param name="cycle">If the option should cycle from the last to the first.</param>
		public static void SelectNext(this InputSelectEntity target, bool? @cycle = null)
		{
			target.CallService("select_next", new InputSelectSelectNextParameters{Cycle = @cycle});
		}

		///<summary>Select the next options of an input select entity.</summary>
		///<param name="target">The IEnumerable<InputSelectEntity> to call this service for</param>
		///<param name="cycle">If the option should cycle from the last to the first.</param>
		public static void SelectNext(this IEnumerable<InputSelectEntity> target, bool? @cycle = null)
		{
			target.CallService("select_next", new InputSelectSelectNextParameters{Cycle = @cycle});
		}

		///<summary>Select an option of an input select entity.</summary>
		public static void SelectOption(this InputSelectEntity target, InputSelectSelectOptionParameters data)
		{
			target.CallService("select_option", data);
		}

		///<summary>Select an option of an input select entity.</summary>
		public static void SelectOption(this IEnumerable<InputSelectEntity> target, InputSelectSelectOptionParameters data)
		{
			target.CallService("select_option", data);
		}

		///<summary>Select an option of an input select entity.</summary>
		///<param name="target">The InputSelectEntity to call this service for</param>
		///<param name="option">Option to be selected. eg: "Item A"</param>
		public static void SelectOption(this InputSelectEntity target, string @option)
		{
			target.CallService("select_option", new InputSelectSelectOptionParameters{Option = @option});
		}

		///<summary>Select an option of an input select entity.</summary>
		///<param name="target">The IEnumerable<InputSelectEntity> to call this service for</param>
		///<param name="option">Option to be selected. eg: "Item A"</param>
		public static void SelectOption(this IEnumerable<InputSelectEntity> target, string @option)
		{
			target.CallService("select_option", new InputSelectSelectOptionParameters{Option = @option});
		}

		///<summary>Select the previous options of an input select entity.</summary>
		public static void SelectPrevious(this InputSelectEntity target, InputSelectSelectPreviousParameters data)
		{
			target.CallService("select_previous", data);
		}

		///<summary>Select the previous options of an input select entity.</summary>
		public static void SelectPrevious(this IEnumerable<InputSelectEntity> target, InputSelectSelectPreviousParameters data)
		{
			target.CallService("select_previous", data);
		}

		///<summary>Select the previous options of an input select entity.</summary>
		///<param name="target">The InputSelectEntity to call this service for</param>
		///<param name="cycle">If the option should cycle from the first to the last.</param>
		public static void SelectPrevious(this InputSelectEntity target, bool? @cycle = null)
		{
			target.CallService("select_previous", new InputSelectSelectPreviousParameters{Cycle = @cycle});
		}

		///<summary>Select the previous options of an input select entity.</summary>
		///<param name="target">The IEnumerable<InputSelectEntity> to call this service for</param>
		///<param name="cycle">If the option should cycle from the first to the last.</param>
		public static void SelectPrevious(this IEnumerable<InputSelectEntity> target, bool? @cycle = null)
		{
			target.CallService("select_previous", new InputSelectSelectPreviousParameters{Cycle = @cycle});
		}

		///<summary>Set the options of an input select entity.</summary>
		public static void SetOptions(this InputSelectEntity target, InputSelectSetOptionsParameters data)
		{
			target.CallService("set_options", data);
		}

		///<summary>Set the options of an input select entity.</summary>
		public static void SetOptions(this IEnumerable<InputSelectEntity> target, InputSelectSetOptionsParameters data)
		{
			target.CallService("set_options", data);
		}

		///<summary>Set the options of an input select entity.</summary>
		///<param name="target">The InputSelectEntity to call this service for</param>
		///<param name="options">Options for the input select entity. eg: ["Item A", "Item B", "Item C"]</param>
		public static void SetOptions(this InputSelectEntity target, object @options)
		{
			target.CallService("set_options", new InputSelectSetOptionsParameters{Options = @options});
		}

		///<summary>Set the options of an input select entity.</summary>
		///<param name="target">The IEnumerable<InputSelectEntity> to call this service for</param>
		///<param name="options">Options for the input select entity. eg: ["Item A", "Item B", "Item C"]</param>
		public static void SetOptions(this IEnumerable<InputSelectEntity> target, object @options)
		{
			target.CallService("set_options", new InputSelectSetOptionsParameters{Options = @options});
		}
	}

	public static class InputTextEntityExtensionMethods
	{
		///<summary>Set the value of an input text entity.</summary>
		public static void SetValue(this InputTextEntity target, InputTextSetValueParameters data)
		{
			target.CallService("set_value", data);
		}

		///<summary>Set the value of an input text entity.</summary>
		public static void SetValue(this IEnumerable<InputTextEntity> target, InputTextSetValueParameters data)
		{
			target.CallService("set_value", data);
		}

		///<summary>Set the value of an input text entity.</summary>
		///<param name="target">The InputTextEntity to call this service for</param>
		///<param name="value">The target value the entity should be set to. eg: This is an example text</param>
		public static void SetValue(this InputTextEntity target, string @value)
		{
			target.CallService("set_value", new InputTextSetValueParameters{Value = @value});
		}

		///<summary>Set the value of an input text entity.</summary>
		///<param name="target">The IEnumerable<InputTextEntity> to call this service for</param>
		///<param name="value">The target value the entity should be set to. eg: This is an example text</param>
		public static void SetValue(this IEnumerable<InputTextEntity> target, string @value)
		{
			target.CallService("set_value", new InputTextSetValueParameters{Value = @value});
		}
	}

	public static class LightEntityExtensionMethods
	{
		///<summary>Toggles one or more lights, from on to off, or, off to on, based on their current state. </summary>
		public static void Toggle(this LightEntity target, LightToggleParameters data)
		{
			target.CallService("toggle", data);
		}

		///<summary>Toggles one or more lights, from on to off, or, off to on, based on their current state. </summary>
		public static void Toggle(this IEnumerable<LightEntity> target, LightToggleParameters data)
		{
			target.CallService("toggle", data);
		}

		///<summary>Toggles one or more lights, from on to off, or, off to on, based on their current state. </summary>
		///<param name="target">The LightEntity to call this service for</param>
		///<param name="transition">Duration it takes to get to next state.</param>
		///<param name="rgbColor">Color for the light in RGB-format. eg: [255, 100, 100]</param>
		///<param name="colorName">A human readable color name.</param>
		///<param name="hsColor">Color for the light in hue/sat format. Hue is 0-360 and Sat is 0-100. eg: [300, 70]</param>
		///<param name="xyColor">Color for the light in XY-format. eg: [0.52, 0.43]</param>
		///<param name="colorTemp">Color temperature for the light in mireds.</param>
		///<param name="kelvin">Color temperature for the light in Kelvin.</param>
		///<param name="whiteValue">Number indicating level of white.</param>
		///<param name="brightness">Number indicating brightness, where 0 turns the light off, 1 is the minimum brightness and 255 is the maximum brightness supported by the light.</param>
		///<param name="brightnessPct">Number indicating percentage of full brightness, where 0 turns the light off, 1 is the minimum brightness and 100 is the maximum brightness supported by the light.</param>
		///<param name="profile">Name of a light profile to use. eg: relax</param>
		///<param name="flash">If the light should flash.</param>
		///<param name="effect">Light effect.</param>
		public static void Toggle(this LightEntity target, long? @transition = null, object? @rgbColor = null, string? @colorName = null, object? @hsColor = null, object? @xyColor = null, long? @colorTemp = null, long? @kelvin = null, long? @whiteValue = null, long? @brightness = null, long? @brightnessPct = null, string? @profile = null, string? @flash = null, string? @effect = null)
		{
			target.CallService("toggle", new LightToggleParameters{Transition = @transition, RgbColor = @rgbColor, ColorName = @colorName, HsColor = @hsColor, XyColor = @xyColor, ColorTemp = @colorTemp, Kelvin = @kelvin, WhiteValue = @whiteValue, Brightness = @brightness, BrightnessPct = @brightnessPct, Profile = @profile, Flash = @flash, Effect = @effect});
		}

		///<summary>Toggles one or more lights, from on to off, or, off to on, based on their current state. </summary>
		///<param name="target">The IEnumerable<LightEntity> to call this service for</param>
		///<param name="transition">Duration it takes to get to next state.</param>
		///<param name="rgbColor">Color for the light in RGB-format. eg: [255, 100, 100]</param>
		///<param name="colorName">A human readable color name.</param>
		///<param name="hsColor">Color for the light in hue/sat format. Hue is 0-360 and Sat is 0-100. eg: [300, 70]</param>
		///<param name="xyColor">Color for the light in XY-format. eg: [0.52, 0.43]</param>
		///<param name="colorTemp">Color temperature for the light in mireds.</param>
		///<param name="kelvin">Color temperature for the light in Kelvin.</param>
		///<param name="whiteValue">Number indicating level of white.</param>
		///<param name="brightness">Number indicating brightness, where 0 turns the light off, 1 is the minimum brightness and 255 is the maximum brightness supported by the light.</param>
		///<param name="brightnessPct">Number indicating percentage of full brightness, where 0 turns the light off, 1 is the minimum brightness and 100 is the maximum brightness supported by the light.</param>
		///<param name="profile">Name of a light profile to use. eg: relax</param>
		///<param name="flash">If the light should flash.</param>
		///<param name="effect">Light effect.</param>
		public static void Toggle(this IEnumerable<LightEntity> target, long? @transition = null, object? @rgbColor = null, string? @colorName = null, object? @hsColor = null, object? @xyColor = null, long? @colorTemp = null, long? @kelvin = null, long? @whiteValue = null, long? @brightness = null, long? @brightnessPct = null, string? @profile = null, string? @flash = null, string? @effect = null)
		{
			target.CallService("toggle", new LightToggleParameters{Transition = @transition, RgbColor = @rgbColor, ColorName = @colorName, HsColor = @hsColor, XyColor = @xyColor, ColorTemp = @colorTemp, Kelvin = @kelvin, WhiteValue = @whiteValue, Brightness = @brightness, BrightnessPct = @brightnessPct, Profile = @profile, Flash = @flash, Effect = @effect});
		}

		///<summary>Turns off one or more lights.</summary>
		public static void TurnOff(this LightEntity target, LightTurnOffParameters data)
		{
			target.CallService("turn_off", data);
		}

		///<summary>Turns off one or more lights.</summary>
		public static void TurnOff(this IEnumerable<LightEntity> target, LightTurnOffParameters data)
		{
			target.CallService("turn_off", data);
		}

		///<summary>Turns off one or more lights.</summary>
		///<param name="target">The LightEntity to call this service for</param>
		///<param name="transition">Duration it takes to get to next state.</param>
		///<param name="flash">If the light should flash.</param>
		public static void TurnOff(this LightEntity target, long? @transition = null, string? @flash = null)
		{
			target.CallService("turn_off", new LightTurnOffParameters{Transition = @transition, Flash = @flash});
		}

		///<summary>Turns off one or more lights.</summary>
		///<param name="target">The IEnumerable<LightEntity> to call this service for</param>
		///<param name="transition">Duration it takes to get to next state.</param>
		///<param name="flash">If the light should flash.</param>
		public static void TurnOff(this IEnumerable<LightEntity> target, long? @transition = null, string? @flash = null)
		{
			target.CallService("turn_off", new LightTurnOffParameters{Transition = @transition, Flash = @flash});
		}

		///<summary>Turn on one or more lights and adjust properties of the light, even when they are turned on already. </summary>
		public static void TurnOn(this LightEntity target, LightTurnOnParameters data)
		{
			target.CallService("turn_on", data);
		}

		///<summary>Turn on one or more lights and adjust properties of the light, even when they are turned on already. </summary>
		public static void TurnOn(this IEnumerable<LightEntity> target, LightTurnOnParameters data)
		{
			target.CallService("turn_on", data);
		}

		///<summary>Turn on one or more lights and adjust properties of the light, even when they are turned on already. </summary>
		///<param name="target">The LightEntity to call this service for</param>
		///<param name="transition">Duration it takes to get to next state.</param>
		///<param name="rgbColor">A list containing three integers between 0 and 255 representing the RGB (red, green, blue) color for the light. eg: [255, 100, 100]</param>
		///<param name="rgbwColor">A list containing four integers between 0 and 255 representing the RGBW (red, green, blue, white) color for the light. eg: [255, 100, 100, 50]</param>
		///<param name="rgbwwColor">A list containing five integers between 0 and 255 representing the RGBWW (red, green, blue, cold white, warm white) color for the light. eg: [255, 100, 100, 50, 70]</param>
		///<param name="colorName">A human readable color name.</param>
		///<param name="hsColor">Color for the light in hue/sat format. Hue is 0-360 and Sat is 0-100. eg: [300, 70]</param>
		///<param name="xyColor">Color for the light in XY-format. eg: [0.52, 0.43]</param>
		///<param name="colorTemp">Color temperature for the light in mireds.</param>
		///<param name="kelvin">Color temperature for the light in Kelvin.</param>
		///<param name="brightness">Number indicating brightness, where 0 turns the light off, 1 is the minimum brightness and 255 is the maximum brightness supported by the light.</param>
		///<param name="brightnessPct">Number indicating percentage of full brightness, where 0 turns the light off, 1 is the minimum brightness and 100 is the maximum brightness supported by the light.</param>
		///<param name="brightnessStep">Change brightness by an amount.</param>
		///<param name="brightnessStepPct">Change brightness by a percentage.</param>
		///<param name="white">Set the light to white mode and change its brightness, where 0 turns the light off, 1 is the minimum brightness and 255 is the maximum brightness supported by the light.</param>
		///<param name="profile">Name of a light profile to use. eg: relax</param>
		///<param name="flash">If the light should flash.</param>
		///<param name="effect">Light effect.</param>
		public static void TurnOn(this LightEntity target, long? @transition = null, object? @rgbColor = null, object? @rgbwColor = null, object? @rgbwwColor = null, string? @colorName = null, object? @hsColor = null, object? @xyColor = null, long? @colorTemp = null, long? @kelvin = null, long? @brightness = null, long? @brightnessPct = null, long? @brightnessStep = null, long? @brightnessStepPct = null, long? @white = null, string? @profile = null, string? @flash = null, string? @effect = null)
		{
			target.CallService("turn_on", new LightTurnOnParameters{Transition = @transition, RgbColor = @rgbColor, RgbwColor = @rgbwColor, RgbwwColor = @rgbwwColor, ColorName = @colorName, HsColor = @hsColor, XyColor = @xyColor, ColorTemp = @colorTemp, Kelvin = @kelvin, Brightness = @brightness, BrightnessPct = @brightnessPct, BrightnessStep = @brightnessStep, BrightnessStepPct = @brightnessStepPct, White = @white, Profile = @profile, Flash = @flash, Effect = @effect});
		}

		///<summary>Turn on one or more lights and adjust properties of the light, even when they are turned on already. </summary>
		///<param name="target">The IEnumerable<LightEntity> to call this service for</param>
		///<param name="transition">Duration it takes to get to next state.</param>
		///<param name="rgbColor">A list containing three integers between 0 and 255 representing the RGB (red, green, blue) color for the light. eg: [255, 100, 100]</param>
		///<param name="rgbwColor">A list containing four integers between 0 and 255 representing the RGBW (red, green, blue, white) color for the light. eg: [255, 100, 100, 50]</param>
		///<param name="rgbwwColor">A list containing five integers between 0 and 255 representing the RGBWW (red, green, blue, cold white, warm white) color for the light. eg: [255, 100, 100, 50, 70]</param>
		///<param name="colorName">A human readable color name.</param>
		///<param name="hsColor">Color for the light in hue/sat format. Hue is 0-360 and Sat is 0-100. eg: [300, 70]</param>
		///<param name="xyColor">Color for the light in XY-format. eg: [0.52, 0.43]</param>
		///<param name="colorTemp">Color temperature for the light in mireds.</param>
		///<param name="kelvin">Color temperature for the light in Kelvin.</param>
		///<param name="brightness">Number indicating brightness, where 0 turns the light off, 1 is the minimum brightness and 255 is the maximum brightness supported by the light.</param>
		///<param name="brightnessPct">Number indicating percentage of full brightness, where 0 turns the light off, 1 is the minimum brightness and 100 is the maximum brightness supported by the light.</param>
		///<param name="brightnessStep">Change brightness by an amount.</param>
		///<param name="brightnessStepPct">Change brightness by a percentage.</param>
		///<param name="white">Set the light to white mode and change its brightness, where 0 turns the light off, 1 is the minimum brightness and 255 is the maximum brightness supported by the light.</param>
		///<param name="profile">Name of a light profile to use. eg: relax</param>
		///<param name="flash">If the light should flash.</param>
		///<param name="effect">Light effect.</param>
		public static void TurnOn(this IEnumerable<LightEntity> target, long? @transition = null, object? @rgbColor = null, object? @rgbwColor = null, object? @rgbwwColor = null, string? @colorName = null, object? @hsColor = null, object? @xyColor = null, long? @colorTemp = null, long? @kelvin = null, long? @brightness = null, long? @brightnessPct = null, long? @brightnessStep = null, long? @brightnessStepPct = null, long? @white = null, string? @profile = null, string? @flash = null, string? @effect = null)
		{
			target.CallService("turn_on", new LightTurnOnParameters{Transition = @transition, RgbColor = @rgbColor, RgbwColor = @rgbwColor, RgbwwColor = @rgbwwColor, ColorName = @colorName, HsColor = @hsColor, XyColor = @xyColor, ColorTemp = @colorTemp, Kelvin = @kelvin, Brightness = @brightness, BrightnessPct = @brightnessPct, BrightnessStep = @brightnessStep, BrightnessStepPct = @brightnessStepPct, White = @white, Profile = @profile, Flash = @flash, Effect = @effect});
		}
	}

	public static class LockEntityExtensionMethods
	{
		///<summary>Lock all or specified locks.</summary>
		public static void Lock(this LockEntity target, LockLockParameters data)
		{
			target.CallService("lock", data);
		}

		///<summary>Lock all or specified locks.</summary>
		public static void Lock(this IEnumerable<LockEntity> target, LockLockParameters data)
		{
			target.CallService("lock", data);
		}

		///<summary>Lock all or specified locks.</summary>
		///<param name="target">The LockEntity to call this service for</param>
		///<param name="code">An optional code to lock the lock with. eg: 1234</param>
		public static void Lock(this LockEntity target, string? @code = null)
		{
			target.CallService("lock", new LockLockParameters{Code = @code});
		}

		///<summary>Lock all or specified locks.</summary>
		///<param name="target">The IEnumerable<LockEntity> to call this service for</param>
		///<param name="code">An optional code to lock the lock with. eg: 1234</param>
		public static void Lock(this IEnumerable<LockEntity> target, string? @code = null)
		{
			target.CallService("lock", new LockLockParameters{Code = @code});
		}

		///<summary>Open all or specified locks.</summary>
		public static void Open(this LockEntity target, LockOpenParameters data)
		{
			target.CallService("open", data);
		}

		///<summary>Open all or specified locks.</summary>
		public static void Open(this IEnumerable<LockEntity> target, LockOpenParameters data)
		{
			target.CallService("open", data);
		}

		///<summary>Open all or specified locks.</summary>
		///<param name="target">The LockEntity to call this service for</param>
		///<param name="code">An optional code to open the lock with. eg: 1234</param>
		public static void Open(this LockEntity target, string? @code = null)
		{
			target.CallService("open", new LockOpenParameters{Code = @code});
		}

		///<summary>Open all or specified locks.</summary>
		///<param name="target">The IEnumerable<LockEntity> to call this service for</param>
		///<param name="code">An optional code to open the lock with. eg: 1234</param>
		public static void Open(this IEnumerable<LockEntity> target, string? @code = null)
		{
			target.CallService("open", new LockOpenParameters{Code = @code});
		}

		///<summary>Unlock all or specified locks.</summary>
		public static void Unlock(this LockEntity target, LockUnlockParameters data)
		{
			target.CallService("unlock", data);
		}

		///<summary>Unlock all or specified locks.</summary>
		public static void Unlock(this IEnumerable<LockEntity> target, LockUnlockParameters data)
		{
			target.CallService("unlock", data);
		}

		///<summary>Unlock all or specified locks.</summary>
		///<param name="target">The LockEntity to call this service for</param>
		///<param name="code">An optional code to unlock the lock with. eg: 1234</param>
		public static void Unlock(this LockEntity target, string? @code = null)
		{
			target.CallService("unlock", new LockUnlockParameters{Code = @code});
		}

		///<summary>Unlock all or specified locks.</summary>
		///<param name="target">The IEnumerable<LockEntity> to call this service for</param>
		///<param name="code">An optional code to unlock the lock with. eg: 1234</param>
		public static void Unlock(this IEnumerable<LockEntity> target, string? @code = null)
		{
			target.CallService("unlock", new LockUnlockParameters{Code = @code});
		}
	}

	public static class MediaPlayerEntityExtensionMethods
	{
		///<summary>Send the media player the command to clear players playlist.</summary>
		public static void ClearPlaylist(this MediaPlayerEntity target)
		{
			target.CallService("clear_playlist");
		}

		///<summary>Send the media player the command to clear players playlist.</summary>
		public static void ClearPlaylist(this IEnumerable<MediaPlayerEntity> target)
		{
			target.CallService("clear_playlist");
		}

		///<summary>Group players together. Only works on platforms with support for player groups.</summary>
		public static void Join(this MediaPlayerEntity target, MediaPlayerJoinParameters data)
		{
			target.CallService("join", data);
		}

		///<summary>Group players together. Only works on platforms with support for player groups.</summary>
		public static void Join(this IEnumerable<MediaPlayerEntity> target, MediaPlayerJoinParameters data)
		{
			target.CallService("join", data);
		}

		///<summary>Group players together. Only works on platforms with support for player groups.</summary>
		///<param name="target">The MediaPlayerEntity to call this service for</param>
		///<param name="groupMembers">The players which will be synced with the target player. eg: ["media_player.multiroom_player2", "media_player.multiroom_player3"]</param>
		public static void Join(this MediaPlayerEntity target, object? @groupMembers = null)
		{
			target.CallService("join", new MediaPlayerJoinParameters{GroupMembers = @groupMembers});
		}

		///<summary>Group players together. Only works on platforms with support for player groups.</summary>
		///<param name="target">The IEnumerable<MediaPlayerEntity> to call this service for</param>
		///<param name="groupMembers">The players which will be synced with the target player. eg: ["media_player.multiroom_player2", "media_player.multiroom_player3"]</param>
		public static void Join(this IEnumerable<MediaPlayerEntity> target, object? @groupMembers = null)
		{
			target.CallService("join", new MediaPlayerJoinParameters{GroupMembers = @groupMembers});
		}

		///<summary>Send the media player the command for next track.</summary>
		public static void MediaNextTrack(this MediaPlayerEntity target)
		{
			target.CallService("media_next_track");
		}

		///<summary>Send the media player the command for next track.</summary>
		public static void MediaNextTrack(this IEnumerable<MediaPlayerEntity> target)
		{
			target.CallService("media_next_track");
		}

		///<summary>Send the media player the command for pause.</summary>
		public static void MediaPause(this MediaPlayerEntity target)
		{
			target.CallService("media_pause");
		}

		///<summary>Send the media player the command for pause.</summary>
		public static void MediaPause(this IEnumerable<MediaPlayerEntity> target)
		{
			target.CallService("media_pause");
		}

		///<summary>Send the media player the command for play.</summary>
		public static void MediaPlay(this MediaPlayerEntity target)
		{
			target.CallService("media_play");
		}

		///<summary>Send the media player the command for play.</summary>
		public static void MediaPlay(this IEnumerable<MediaPlayerEntity> target)
		{
			target.CallService("media_play");
		}

		///<summary>Toggle media player play/pause state.</summary>
		public static void MediaPlayPause(this MediaPlayerEntity target)
		{
			target.CallService("media_play_pause");
		}

		///<summary>Toggle media player play/pause state.</summary>
		public static void MediaPlayPause(this IEnumerable<MediaPlayerEntity> target)
		{
			target.CallService("media_play_pause");
		}

		///<summary>Send the media player the command for previous track.</summary>
		public static void MediaPreviousTrack(this MediaPlayerEntity target)
		{
			target.CallService("media_previous_track");
		}

		///<summary>Send the media player the command for previous track.</summary>
		public static void MediaPreviousTrack(this IEnumerable<MediaPlayerEntity> target)
		{
			target.CallService("media_previous_track");
		}

		///<summary>Send the media player the command to seek in current playing media.</summary>
		public static void MediaSeek(this MediaPlayerEntity target, MediaPlayerMediaSeekParameters data)
		{
			target.CallService("media_seek", data);
		}

		///<summary>Send the media player the command to seek in current playing media.</summary>
		public static void MediaSeek(this IEnumerable<MediaPlayerEntity> target, MediaPlayerMediaSeekParameters data)
		{
			target.CallService("media_seek", data);
		}

		///<summary>Send the media player the command to seek in current playing media.</summary>
		///<param name="target">The MediaPlayerEntity to call this service for</param>
		///<param name="seekPosition">Position to seek to. The format is platform dependent.</param>
		public static void MediaSeek(this MediaPlayerEntity target, double @seekPosition)
		{
			target.CallService("media_seek", new MediaPlayerMediaSeekParameters{SeekPosition = @seekPosition});
		}

		///<summary>Send the media player the command to seek in current playing media.</summary>
		///<param name="target">The IEnumerable<MediaPlayerEntity> to call this service for</param>
		///<param name="seekPosition">Position to seek to. The format is platform dependent.</param>
		public static void MediaSeek(this IEnumerable<MediaPlayerEntity> target, double @seekPosition)
		{
			target.CallService("media_seek", new MediaPlayerMediaSeekParameters{SeekPosition = @seekPosition});
		}

		///<summary>Send the media player the stop command.</summary>
		public static void MediaStop(this MediaPlayerEntity target)
		{
			target.CallService("media_stop");
		}

		///<summary>Send the media player the stop command.</summary>
		public static void MediaStop(this IEnumerable<MediaPlayerEntity> target)
		{
			target.CallService("media_stop");
		}

		///<summary>Send the media player the command for playing media.</summary>
		public static void PlayMedia(this MediaPlayerEntity target, MediaPlayerPlayMediaParameters data)
		{
			target.CallService("play_media", data);
		}

		///<summary>Send the media player the command for playing media.</summary>
		public static void PlayMedia(this IEnumerable<MediaPlayerEntity> target, MediaPlayerPlayMediaParameters data)
		{
			target.CallService("play_media", data);
		}

		///<summary>Send the media player the command for playing media.</summary>
		///<param name="target">The MediaPlayerEntity to call this service for</param>
		///<param name="mediaContentId">The ID of the content to play. Platform dependent. eg: https://home-assistant.io/images/cast/splash.png</param>
		///<param name="mediaContentType">The type of the content to play. Like image, music, tvshow, video, episode, channel or playlist. eg: music</param>
		public static void PlayMedia(this MediaPlayerEntity target, string @mediaContentId, string @mediaContentType)
		{
			target.CallService("play_media", new MediaPlayerPlayMediaParameters{MediaContentId = @mediaContentId, MediaContentType = @mediaContentType});
		}

		///<summary>Send the media player the command for playing media.</summary>
		///<param name="target">The IEnumerable<MediaPlayerEntity> to call this service for</param>
		///<param name="mediaContentId">The ID of the content to play. Platform dependent. eg: https://home-assistant.io/images/cast/splash.png</param>
		///<param name="mediaContentType">The type of the content to play. Like image, music, tvshow, video, episode, channel or playlist. eg: music</param>
		public static void PlayMedia(this IEnumerable<MediaPlayerEntity> target, string @mediaContentId, string @mediaContentType)
		{
			target.CallService("play_media", new MediaPlayerPlayMediaParameters{MediaContentId = @mediaContentId, MediaContentType = @mediaContentType});
		}

		///<summary>Set repeat mode</summary>
		public static void RepeatSet(this MediaPlayerEntity target, MediaPlayerRepeatSetParameters data)
		{
			target.CallService("repeat_set", data);
		}

		///<summary>Set repeat mode</summary>
		public static void RepeatSet(this IEnumerable<MediaPlayerEntity> target, MediaPlayerRepeatSetParameters data)
		{
			target.CallService("repeat_set", data);
		}

		///<summary>Set repeat mode</summary>
		///<param name="target">The MediaPlayerEntity to call this service for</param>
		///<param name="repeat">Repeat mode to set.</param>
		public static void RepeatSet(this MediaPlayerEntity target, string @repeat)
		{
			target.CallService("repeat_set", new MediaPlayerRepeatSetParameters{Repeat = @repeat});
		}

		///<summary>Set repeat mode</summary>
		///<param name="target">The IEnumerable<MediaPlayerEntity> to call this service for</param>
		///<param name="repeat">Repeat mode to set.</param>
		public static void RepeatSet(this IEnumerable<MediaPlayerEntity> target, string @repeat)
		{
			target.CallService("repeat_set", new MediaPlayerRepeatSetParameters{Repeat = @repeat});
		}

		///<summary>Send the media player the command to change sound mode.</summary>
		public static void SelectSoundMode(this MediaPlayerEntity target, MediaPlayerSelectSoundModeParameters data)
		{
			target.CallService("select_sound_mode", data);
		}

		///<summary>Send the media player the command to change sound mode.</summary>
		public static void SelectSoundMode(this IEnumerable<MediaPlayerEntity> target, MediaPlayerSelectSoundModeParameters data)
		{
			target.CallService("select_sound_mode", data);
		}

		///<summary>Send the media player the command to change sound mode.</summary>
		///<param name="target">The MediaPlayerEntity to call this service for</param>
		///<param name="soundMode">Name of the sound mode to switch to. eg: Music</param>
		public static void SelectSoundMode(this MediaPlayerEntity target, string? @soundMode = null)
		{
			target.CallService("select_sound_mode", new MediaPlayerSelectSoundModeParameters{SoundMode = @soundMode});
		}

		///<summary>Send the media player the command to change sound mode.</summary>
		///<param name="target">The IEnumerable<MediaPlayerEntity> to call this service for</param>
		///<param name="soundMode">Name of the sound mode to switch to. eg: Music</param>
		public static void SelectSoundMode(this IEnumerable<MediaPlayerEntity> target, string? @soundMode = null)
		{
			target.CallService("select_sound_mode", new MediaPlayerSelectSoundModeParameters{SoundMode = @soundMode});
		}

		///<summary>Send the media player the command to change input source.</summary>
		public static void SelectSource(this MediaPlayerEntity target, MediaPlayerSelectSourceParameters data)
		{
			target.CallService("select_source", data);
		}

		///<summary>Send the media player the command to change input source.</summary>
		public static void SelectSource(this IEnumerable<MediaPlayerEntity> target, MediaPlayerSelectSourceParameters data)
		{
			target.CallService("select_source", data);
		}

		///<summary>Send the media player the command to change input source.</summary>
		///<param name="target">The MediaPlayerEntity to call this service for</param>
		///<param name="source">Name of the source to switch to. Platform dependent. eg: video1</param>
		public static void SelectSource(this MediaPlayerEntity target, string @source)
		{
			target.CallService("select_source", new MediaPlayerSelectSourceParameters{Source = @source});
		}

		///<summary>Send the media player the command to change input source.</summary>
		///<param name="target">The IEnumerable<MediaPlayerEntity> to call this service for</param>
		///<param name="source">Name of the source to switch to. Platform dependent. eg: video1</param>
		public static void SelectSource(this IEnumerable<MediaPlayerEntity> target, string @source)
		{
			target.CallService("select_source", new MediaPlayerSelectSourceParameters{Source = @source});
		}

		///<summary>Set shuffling state.</summary>
		public static void ShuffleSet(this MediaPlayerEntity target, MediaPlayerShuffleSetParameters data)
		{
			target.CallService("shuffle_set", data);
		}

		///<summary>Set shuffling state.</summary>
		public static void ShuffleSet(this IEnumerable<MediaPlayerEntity> target, MediaPlayerShuffleSetParameters data)
		{
			target.CallService("shuffle_set", data);
		}

		///<summary>Set shuffling state.</summary>
		///<param name="target">The MediaPlayerEntity to call this service for</param>
		///<param name="shuffle">True/false for enabling/disabling shuffle.</param>
		public static void ShuffleSet(this MediaPlayerEntity target, bool @shuffle)
		{
			target.CallService("shuffle_set", new MediaPlayerShuffleSetParameters{Shuffle = @shuffle});
		}

		///<summary>Set shuffling state.</summary>
		///<param name="target">The IEnumerable<MediaPlayerEntity> to call this service for</param>
		///<param name="shuffle">True/false for enabling/disabling shuffle.</param>
		public static void ShuffleSet(this IEnumerable<MediaPlayerEntity> target, bool @shuffle)
		{
			target.CallService("shuffle_set", new MediaPlayerShuffleSetParameters{Shuffle = @shuffle});
		}

		///<summary>Toggles a media player power state.</summary>
		public static void Toggle(this MediaPlayerEntity target)
		{
			target.CallService("toggle");
		}

		///<summary>Toggles a media player power state.</summary>
		public static void Toggle(this IEnumerable<MediaPlayerEntity> target)
		{
			target.CallService("toggle");
		}

		///<summary>Turn a media player power off.</summary>
		public static void TurnOff(this MediaPlayerEntity target)
		{
			target.CallService("turn_off");
		}

		///<summary>Turn a media player power off.</summary>
		public static void TurnOff(this IEnumerable<MediaPlayerEntity> target)
		{
			target.CallService("turn_off");
		}

		///<summary>Turn a media player power on.</summary>
		public static void TurnOn(this MediaPlayerEntity target)
		{
			target.CallService("turn_on");
		}

		///<summary>Turn a media player power on.</summary>
		public static void TurnOn(this IEnumerable<MediaPlayerEntity> target)
		{
			target.CallService("turn_on");
		}

		///<summary>Unjoin the player from a group. Only works on platforms with support for player groups.</summary>
		public static void Unjoin(this MediaPlayerEntity target)
		{
			target.CallService("unjoin");
		}

		///<summary>Unjoin the player from a group. Only works on platforms with support for player groups.</summary>
		public static void Unjoin(this IEnumerable<MediaPlayerEntity> target)
		{
			target.CallService("unjoin");
		}

		///<summary>Turn a media player volume down.</summary>
		public static void VolumeDown(this MediaPlayerEntity target)
		{
			target.CallService("volume_down");
		}

		///<summary>Turn a media player volume down.</summary>
		public static void VolumeDown(this IEnumerable<MediaPlayerEntity> target)
		{
			target.CallService("volume_down");
		}

		///<summary>Mute a media player's volume.</summary>
		public static void VolumeMute(this MediaPlayerEntity target, MediaPlayerVolumeMuteParameters data)
		{
			target.CallService("volume_mute", data);
		}

		///<summary>Mute a media player's volume.</summary>
		public static void VolumeMute(this IEnumerable<MediaPlayerEntity> target, MediaPlayerVolumeMuteParameters data)
		{
			target.CallService("volume_mute", data);
		}

		///<summary>Mute a media player's volume.</summary>
		///<param name="target">The MediaPlayerEntity to call this service for</param>
		///<param name="isVolumeMuted">True/false for mute/unmute.</param>
		public static void VolumeMute(this MediaPlayerEntity target, bool @isVolumeMuted)
		{
			target.CallService("volume_mute", new MediaPlayerVolumeMuteParameters{IsVolumeMuted = @isVolumeMuted});
		}

		///<summary>Mute a media player's volume.</summary>
		///<param name="target">The IEnumerable<MediaPlayerEntity> to call this service for</param>
		///<param name="isVolumeMuted">True/false for mute/unmute.</param>
		public static void VolumeMute(this IEnumerable<MediaPlayerEntity> target, bool @isVolumeMuted)
		{
			target.CallService("volume_mute", new MediaPlayerVolumeMuteParameters{IsVolumeMuted = @isVolumeMuted});
		}

		///<summary>Set a media player's volume level.</summary>
		public static void VolumeSet(this MediaPlayerEntity target, MediaPlayerVolumeSetParameters data)
		{
			target.CallService("volume_set", data);
		}

		///<summary>Set a media player's volume level.</summary>
		public static void VolumeSet(this IEnumerable<MediaPlayerEntity> target, MediaPlayerVolumeSetParameters data)
		{
			target.CallService("volume_set", data);
		}

		///<summary>Set a media player's volume level.</summary>
		///<param name="target">The MediaPlayerEntity to call this service for</param>
		///<param name="volumeLevel">Volume level to set as float.</param>
		public static void VolumeSet(this MediaPlayerEntity target, double @volumeLevel)
		{
			target.CallService("volume_set", new MediaPlayerVolumeSetParameters{VolumeLevel = @volumeLevel});
		}

		///<summary>Set a media player's volume level.</summary>
		///<param name="target">The IEnumerable<MediaPlayerEntity> to call this service for</param>
		///<param name="volumeLevel">Volume level to set as float.</param>
		public static void VolumeSet(this IEnumerable<MediaPlayerEntity> target, double @volumeLevel)
		{
			target.CallService("volume_set", new MediaPlayerVolumeSetParameters{VolumeLevel = @volumeLevel});
		}

		///<summary>Turn a media player volume up.</summary>
		public static void VolumeUp(this MediaPlayerEntity target)
		{
			target.CallService("volume_up");
		}

		///<summary>Turn a media player volume up.</summary>
		public static void VolumeUp(this IEnumerable<MediaPlayerEntity> target)
		{
			target.CallService("volume_up");
		}
	}

	public static class MelcloudEntityExtensionMethods
	{
		///<summary>Sets horizontal vane position.</summary>
		public static void SetVaneHorizontal(this ClimateEntity target, MelcloudSetVaneHorizontalParameters data)
		{
			target.CallService("set_vane_horizontal", data);
		}

		///<summary>Sets horizontal vane position.</summary>
		public static void SetVaneHorizontal(this IEnumerable<ClimateEntity> target, MelcloudSetVaneHorizontalParameters data)
		{
			target.CallService("set_vane_horizontal", data);
		}

		///<summary>Sets horizontal vane position.</summary>
		///<param name="target">The ClimateEntity to call this service for</param>
		///<param name="position">Horizontal vane position. Possible options can be found in the vane_horizontal_positions state attribute.  eg: auto</param>
		public static void SetVaneHorizontal(this ClimateEntity target, string @position)
		{
			target.CallService("set_vane_horizontal", new MelcloudSetVaneHorizontalParameters{Position = @position});
		}

		///<summary>Sets horizontal vane position.</summary>
		///<param name="target">The IEnumerable<ClimateEntity> to call this service for</param>
		///<param name="position">Horizontal vane position. Possible options can be found in the vane_horizontal_positions state attribute.  eg: auto</param>
		public static void SetVaneHorizontal(this IEnumerable<ClimateEntity> target, string @position)
		{
			target.CallService("set_vane_horizontal", new MelcloudSetVaneHorizontalParameters{Position = @position});
		}

		///<summary>Sets vertical vane position.</summary>
		public static void SetVaneVertical(this ClimateEntity target, MelcloudSetVaneVerticalParameters data)
		{
			target.CallService("set_vane_vertical", data);
		}

		///<summary>Sets vertical vane position.</summary>
		public static void SetVaneVertical(this IEnumerable<ClimateEntity> target, MelcloudSetVaneVerticalParameters data)
		{
			target.CallService("set_vane_vertical", data);
		}

		///<summary>Sets vertical vane position.</summary>
		///<param name="target">The ClimateEntity to call this service for</param>
		///<param name="position">Vertical vane position. Possible options can be found in the vane_vertical_positions state attribute.  eg: auto</param>
		public static void SetVaneVertical(this ClimateEntity target, string @position)
		{
			target.CallService("set_vane_vertical", new MelcloudSetVaneVerticalParameters{Position = @position});
		}

		///<summary>Sets vertical vane position.</summary>
		///<param name="target">The IEnumerable<ClimateEntity> to call this service for</param>
		///<param name="position">Vertical vane position. Possible options can be found in the vane_vertical_positions state attribute.  eg: auto</param>
		public static void SetVaneVertical(this IEnumerable<ClimateEntity> target, string @position)
		{
			target.CallService("set_vane_vertical", new MelcloudSetVaneVerticalParameters{Position = @position});
		}
	}

	public static class NumberEntityExtensionMethods
	{
		///<summary>Set the value of a Number entity.</summary>
		public static void SetValue(this NumberEntity target, NumberSetValueParameters data)
		{
			target.CallService("set_value", data);
		}

		///<summary>Set the value of a Number entity.</summary>
		public static void SetValue(this IEnumerable<NumberEntity> target, NumberSetValueParameters data)
		{
			target.CallService("set_value", data);
		}

		///<summary>Set the value of a Number entity.</summary>
		///<param name="target">The NumberEntity to call this service for</param>
		///<param name="value">The target value the entity should be set to. eg: 42</param>
		public static void SetValue(this NumberEntity target, string? @value = null)
		{
			target.CallService("set_value", new NumberSetValueParameters{Value = @value});
		}

		///<summary>Set the value of a Number entity.</summary>
		///<param name="target">The IEnumerable<NumberEntity> to call this service for</param>
		///<param name="value">The target value the entity should be set to. eg: 42</param>
		public static void SetValue(this IEnumerable<NumberEntity> target, string? @value = null)
		{
			target.CallService("set_value", new NumberSetValueParameters{Value = @value});
		}
	}

	public static class PiHoleEntityExtensionMethods
	{
		///<summary>Disable configured Pi-hole(s) for an amount of time</summary>
		public static void Disable(this SwitchEntity target, PiHoleDisableParameters data)
		{
			target.CallService("disable", data);
		}

		///<summary>Disable configured Pi-hole(s) for an amount of time</summary>
		public static void Disable(this IEnumerable<SwitchEntity> target, PiHoleDisableParameters data)
		{
			target.CallService("disable", data);
		}

		///<summary>Disable configured Pi-hole(s) for an amount of time</summary>
		///<param name="target">The SwitchEntity to call this service for</param>
		///<param name="duration">Time that the Pi-hole should be disabled for eg: 00:00:15</param>
		public static void Disable(this SwitchEntity target, string @duration)
		{
			target.CallService("disable", new PiHoleDisableParameters{Duration = @duration});
		}

		///<summary>Disable configured Pi-hole(s) for an amount of time</summary>
		///<param name="target">The IEnumerable<SwitchEntity> to call this service for</param>
		///<param name="duration">Time that the Pi-hole should be disabled for eg: 00:00:15</param>
		public static void Disable(this IEnumerable<SwitchEntity> target, string @duration)
		{
			target.CallService("disable", new PiHoleDisableParameters{Duration = @duration});
		}
	}

	public static class RemoteEntityExtensionMethods
	{
		///<summary>Deletes a command or a list of commands from the database.</summary>
		public static void DeleteCommand(this RemoteEntity target, RemoteDeleteCommandParameters data)
		{
			target.CallService("delete_command", data);
		}

		///<summary>Deletes a command or a list of commands from the database.</summary>
		public static void DeleteCommand(this IEnumerable<RemoteEntity> target, RemoteDeleteCommandParameters data)
		{
			target.CallService("delete_command", data);
		}

		///<summary>Deletes a command or a list of commands from the database.</summary>
		///<param name="target">The RemoteEntity to call this service for</param>
		///<param name="device">Name of the device from which commands will be deleted. eg: television</param>
		///<param name="command">A single command or a list of commands to delete. eg: Mute</param>
		public static void DeleteCommand(this RemoteEntity target, object @command, string? @device = null)
		{
			target.CallService("delete_command", new RemoteDeleteCommandParameters{Device = @device, Command = @command});
		}

		///<summary>Deletes a command or a list of commands from the database.</summary>
		///<param name="target">The IEnumerable<RemoteEntity> to call this service for</param>
		///<param name="device">Name of the device from which commands will be deleted. eg: television</param>
		///<param name="command">A single command or a list of commands to delete. eg: Mute</param>
		public static void DeleteCommand(this IEnumerable<RemoteEntity> target, object @command, string? @device = null)
		{
			target.CallService("delete_command", new RemoteDeleteCommandParameters{Device = @device, Command = @command});
		}

		///<summary>Learns a command or a list of commands from a device.</summary>
		public static void LearnCommand(this RemoteEntity target, RemoteLearnCommandParameters data)
		{
			target.CallService("learn_command", data);
		}

		///<summary>Learns a command or a list of commands from a device.</summary>
		public static void LearnCommand(this IEnumerable<RemoteEntity> target, RemoteLearnCommandParameters data)
		{
			target.CallService("learn_command", data);
		}

		///<summary>Learns a command or a list of commands from a device.</summary>
		///<param name="target">The RemoteEntity to call this service for</param>
		///<param name="device">Device ID to learn command from. eg: television</param>
		///<param name="command">A single command or a list of commands to learn. eg: Turn on</param>
		///<param name="commandType">The type of command to be learned.</param>
		///<param name="alternative">If code must be stored as alternative (useful for discrete remotes).</param>
		///<param name="timeout">Timeout for the command to be learned.</param>
		public static void LearnCommand(this RemoteEntity target, string? @device = null, object? @command = null, string? @commandType = null, bool? @alternative = null, long? @timeout = null)
		{
			target.CallService("learn_command", new RemoteLearnCommandParameters{Device = @device, Command = @command, CommandType = @commandType, Alternative = @alternative, Timeout = @timeout});
		}

		///<summary>Learns a command or a list of commands from a device.</summary>
		///<param name="target">The IEnumerable<RemoteEntity> to call this service for</param>
		///<param name="device">Device ID to learn command from. eg: television</param>
		///<param name="command">A single command or a list of commands to learn. eg: Turn on</param>
		///<param name="commandType">The type of command to be learned.</param>
		///<param name="alternative">If code must be stored as alternative (useful for discrete remotes).</param>
		///<param name="timeout">Timeout for the command to be learned.</param>
		public static void LearnCommand(this IEnumerable<RemoteEntity> target, string? @device = null, object? @command = null, string? @commandType = null, bool? @alternative = null, long? @timeout = null)
		{
			target.CallService("learn_command", new RemoteLearnCommandParameters{Device = @device, Command = @command, CommandType = @commandType, Alternative = @alternative, Timeout = @timeout});
		}

		///<summary>Sends a command or a list of commands to a device.</summary>
		public static void SendCommand(this RemoteEntity target, RemoteSendCommandParameters data)
		{
			target.CallService("send_command", data);
		}

		///<summary>Sends a command or a list of commands to a device.</summary>
		public static void SendCommand(this IEnumerable<RemoteEntity> target, RemoteSendCommandParameters data)
		{
			target.CallService("send_command", data);
		}

		///<summary>Sends a command or a list of commands to a device.</summary>
		///<param name="target">The RemoteEntity to call this service for</param>
		///<param name="device">Device ID to send command to. eg: 32756745</param>
		///<param name="command">A single command or a list of commands to send. eg: Play</param>
		///<param name="numRepeats">The number of times you want to repeat the command(s).</param>
		///<param name="delaySecs">The time you want to wait in between repeated commands.</param>
		///<param name="holdSecs">The time you want to have it held before the release is send.</param>
		public static void SendCommand(this RemoteEntity target, string @command, string? @device = null, long? @numRepeats = null, double? @delaySecs = null, double? @holdSecs = null)
		{
			target.CallService("send_command", new RemoteSendCommandParameters{Device = @device, Command = @command, NumRepeats = @numRepeats, DelaySecs = @delaySecs, HoldSecs = @holdSecs});
		}

		///<summary>Sends a command or a list of commands to a device.</summary>
		///<param name="target">The IEnumerable<RemoteEntity> to call this service for</param>
		///<param name="device">Device ID to send command to. eg: 32756745</param>
		///<param name="command">A single command or a list of commands to send. eg: Play</param>
		///<param name="numRepeats">The number of times you want to repeat the command(s).</param>
		///<param name="delaySecs">The time you want to wait in between repeated commands.</param>
		///<param name="holdSecs">The time you want to have it held before the release is send.</param>
		public static void SendCommand(this IEnumerable<RemoteEntity> target, string @command, string? @device = null, long? @numRepeats = null, double? @delaySecs = null, double? @holdSecs = null)
		{
			target.CallService("send_command", new RemoteSendCommandParameters{Device = @device, Command = @command, NumRepeats = @numRepeats, DelaySecs = @delaySecs, HoldSecs = @holdSecs});
		}

		///<summary>Toggles a device.</summary>
		public static void Toggle(this RemoteEntity target)
		{
			target.CallService("toggle");
		}

		///<summary>Toggles a device.</summary>
		public static void Toggle(this IEnumerable<RemoteEntity> target)
		{
			target.CallService("toggle");
		}

		///<summary>Sends the Power Off Command.</summary>
		public static void TurnOff(this RemoteEntity target)
		{
			target.CallService("turn_off");
		}

		///<summary>Sends the Power Off Command.</summary>
		public static void TurnOff(this IEnumerable<RemoteEntity> target)
		{
			target.CallService("turn_off");
		}

		///<summary>Sends the Power On Command.</summary>
		public static void TurnOn(this RemoteEntity target, RemoteTurnOnParameters data)
		{
			target.CallService("turn_on", data);
		}

		///<summary>Sends the Power On Command.</summary>
		public static void TurnOn(this IEnumerable<RemoteEntity> target, RemoteTurnOnParameters data)
		{
			target.CallService("turn_on", data);
		}

		///<summary>Sends the Power On Command.</summary>
		///<param name="target">The RemoteEntity to call this service for</param>
		///<param name="activity">Activity ID or Activity Name to start. eg: BedroomTV</param>
		public static void TurnOn(this RemoteEntity target, string? @activity = null)
		{
			target.CallService("turn_on", new RemoteTurnOnParameters{Activity = @activity});
		}

		///<summary>Sends the Power On Command.</summary>
		///<param name="target">The IEnumerable<RemoteEntity> to call this service for</param>
		///<param name="activity">Activity ID or Activity Name to start. eg: BedroomTV</param>
		public static void TurnOn(this IEnumerable<RemoteEntity> target, string? @activity = null)
		{
			target.CallService("turn_on", new RemoteTurnOnParameters{Activity = @activity});
		}
	}

	public static class ScriptEntityExtensionMethods
	{
		///<summary>Toggle script</summary>
		public static void Toggle(this ScriptEntity target)
		{
			target.CallService("toggle");
		}

		///<summary>Toggle script</summary>
		public static void Toggle(this IEnumerable<ScriptEntity> target)
		{
			target.CallService("toggle");
		}

		///<summary>Turn off script</summary>
		public static void TurnOff(this ScriptEntity target)
		{
			target.CallService("turn_off");
		}

		///<summary>Turn off script</summary>
		public static void TurnOff(this IEnumerable<ScriptEntity> target)
		{
			target.CallService("turn_off");
		}

		///<summary>Turn on script</summary>
		public static void TurnOn(this ScriptEntity target)
		{
			target.CallService("turn_on");
		}

		///<summary>Turn on script</summary>
		public static void TurnOn(this IEnumerable<ScriptEntity> target)
		{
			target.CallService("turn_on");
		}
	}

	public static class SelectEntityExtensionMethods
	{
		///<summary>Select an option of an select entity.</summary>
		public static void SelectOption(this SelectEntity target, SelectSelectOptionParameters data)
		{
			target.CallService("select_option", data);
		}

		///<summary>Select an option of an select entity.</summary>
		public static void SelectOption(this IEnumerable<SelectEntity> target, SelectSelectOptionParameters data)
		{
			target.CallService("select_option", data);
		}

		///<summary>Select an option of an select entity.</summary>
		///<param name="target">The SelectEntity to call this service for</param>
		///<param name="option">Option to be selected. eg: "Item A"</param>
		public static void SelectOption(this SelectEntity target, string @option)
		{
			target.CallService("select_option", new SelectSelectOptionParameters{Option = @option});
		}

		///<summary>Select an option of an select entity.</summary>
		///<param name="target">The IEnumerable<SelectEntity> to call this service for</param>
		///<param name="option">Option to be selected. eg: "Item A"</param>
		public static void SelectOption(this IEnumerable<SelectEntity> target, string @option)
		{
			target.CallService("select_option", new SelectSelectOptionParameters{Option = @option});
		}
	}

	public static class SonosEntityExtensionMethods
	{
		///<summary>Unjoin the player from a group.</summary>
		public static void Unjoin(this MediaPlayerEntity target)
		{
			target.CallService("unjoin");
		}

		///<summary>Unjoin the player from a group.</summary>
		public static void Unjoin(this IEnumerable<MediaPlayerEntity> target)
		{
			target.CallService("unjoin");
		}
	}

	public static class SwitchEntityExtensionMethods
	{
		///<summary>Toggles a switch state</summary>
		public static void Toggle(this SwitchEntity target)
		{
			target.CallService("toggle");
		}

		///<summary>Toggles a switch state</summary>
		public static void Toggle(this IEnumerable<SwitchEntity> target)
		{
			target.CallService("toggle");
		}

		///<summary>Turn a switch off</summary>
		public static void TurnOff(this SwitchEntity target)
		{
			target.CallService("turn_off");
		}

		///<summary>Turn a switch off</summary>
		public static void TurnOff(this IEnumerable<SwitchEntity> target)
		{
			target.CallService("turn_off");
		}

		///<summary>Turn a switch on</summary>
		public static void TurnOn(this SwitchEntity target)
		{
			target.CallService("turn_on");
		}

		///<summary>Turn a switch on</summary>
		public static void TurnOn(this IEnumerable<SwitchEntity> target)
		{
			target.CallService("turn_on");
		}
	}

	public static class TimerEntityExtensionMethods
	{
		///<summary>Cancel a timer.</summary>
		public static void Cancel(this TimerEntity target)
		{
			target.CallService("cancel");
		}

		///<summary>Cancel a timer.</summary>
		public static void Cancel(this IEnumerable<TimerEntity> target)
		{
			target.CallService("cancel");
		}

		///<summary>Finish a timer.</summary>
		public static void Finish(this TimerEntity target)
		{
			target.CallService("finish");
		}

		///<summary>Finish a timer.</summary>
		public static void Finish(this IEnumerable<TimerEntity> target)
		{
			target.CallService("finish");
		}

		///<summary>Pause a timer.</summary>
		public static void Pause(this TimerEntity target)
		{
			target.CallService("pause");
		}

		///<summary>Pause a timer.</summary>
		public static void Pause(this IEnumerable<TimerEntity> target)
		{
			target.CallService("pause");
		}

		///<summary>Start a timer</summary>
		public static void Start(this TimerEntity target, TimerStartParameters data)
		{
			target.CallService("start", data);
		}

		///<summary>Start a timer</summary>
		public static void Start(this IEnumerable<TimerEntity> target, TimerStartParameters data)
		{
			target.CallService("start", data);
		}

		///<summary>Start a timer</summary>
		///<param name="target">The TimerEntity to call this service for</param>
		///<param name="duration">Duration the timer requires to finish. [optional] eg: 00:01:00 or 60</param>
		public static void Start(this TimerEntity target, string? @duration = null)
		{
			target.CallService("start", new TimerStartParameters{Duration = @duration});
		}

		///<summary>Start a timer</summary>
		///<param name="target">The IEnumerable<TimerEntity> to call this service for</param>
		///<param name="duration">Duration the timer requires to finish. [optional] eg: 00:01:00 or 60</param>
		public static void Start(this IEnumerable<TimerEntity> target, string? @duration = null)
		{
			target.CallService("start", new TimerStartParameters{Duration = @duration});
		}
	}

	public static class UtilityMeterEntityExtensionMethods
	{
		///<summary>Calibrates a utility meter sensor.</summary>
		public static void Calibrate(this SensorEntity target, UtilityMeterCalibrateParameters data)
		{
			target.CallService("calibrate", data);
		}

		///<summary>Calibrates a utility meter sensor.</summary>
		public static void Calibrate(this IEnumerable<SensorEntity> target, UtilityMeterCalibrateParameters data)
		{
			target.CallService("calibrate", data);
		}

		///<summary>Calibrates a utility meter sensor.</summary>
		///<param name="target">The SensorEntity to call this service for</param>
		///<param name="value">Value to which set the meter eg: 100</param>
		public static void Calibrate(this SensorEntity target, string @value)
		{
			target.CallService("calibrate", new UtilityMeterCalibrateParameters{Value = @value});
		}

		///<summary>Calibrates a utility meter sensor.</summary>
		///<param name="target">The IEnumerable<SensorEntity> to call this service for</param>
		///<param name="value">Value to which set the meter eg: 100</param>
		public static void Calibrate(this IEnumerable<SensorEntity> target, string @value)
		{
			target.CallService("calibrate", new UtilityMeterCalibrateParameters{Value = @value});
		}
	}

	public static class ZhaEntityExtensionMethods
	{
		///<summary>Clear a user code from a lock</summary>
		public static void ClearLockUserCode(this LockEntity target, ZhaClearLockUserCodeParameters data)
		{
			target.CallService("clear_lock_user_code", data);
		}

		///<summary>Clear a user code from a lock</summary>
		public static void ClearLockUserCode(this IEnumerable<LockEntity> target, ZhaClearLockUserCodeParameters data)
		{
			target.CallService("clear_lock_user_code", data);
		}

		///<summary>Clear a user code from a lock</summary>
		///<param name="target">The LockEntity to call this service for</param>
		///<param name="codeSlot">Code slot to clear code from eg: 1</param>
		public static void ClearLockUserCode(this LockEntity target, string @codeSlot)
		{
			target.CallService("clear_lock_user_code", new ZhaClearLockUserCodeParameters{CodeSlot = @codeSlot});
		}

		///<summary>Clear a user code from a lock</summary>
		///<param name="target">The IEnumerable<LockEntity> to call this service for</param>
		///<param name="codeSlot">Code slot to clear code from eg: 1</param>
		public static void ClearLockUserCode(this IEnumerable<LockEntity> target, string @codeSlot)
		{
			target.CallService("clear_lock_user_code", new ZhaClearLockUserCodeParameters{CodeSlot = @codeSlot});
		}

		///<summary>Disable a user code on a lock</summary>
		public static void DisableLockUserCode(this LockEntity target, ZhaDisableLockUserCodeParameters data)
		{
			target.CallService("disable_lock_user_code", data);
		}

		///<summary>Disable a user code on a lock</summary>
		public static void DisableLockUserCode(this IEnumerable<LockEntity> target, ZhaDisableLockUserCodeParameters data)
		{
			target.CallService("disable_lock_user_code", data);
		}

		///<summary>Disable a user code on a lock</summary>
		///<param name="target">The LockEntity to call this service for</param>
		///<param name="codeSlot">Code slot to disable eg: 1</param>
		public static void DisableLockUserCode(this LockEntity target, string @codeSlot)
		{
			target.CallService("disable_lock_user_code", new ZhaDisableLockUserCodeParameters{CodeSlot = @codeSlot});
		}

		///<summary>Disable a user code on a lock</summary>
		///<param name="target">The IEnumerable<LockEntity> to call this service for</param>
		///<param name="codeSlot">Code slot to disable eg: 1</param>
		public static void DisableLockUserCode(this IEnumerable<LockEntity> target, string @codeSlot)
		{
			target.CallService("disable_lock_user_code", new ZhaDisableLockUserCodeParameters{CodeSlot = @codeSlot});
		}

		///<summary>Enable a user code on a lock</summary>
		public static void EnableLockUserCode(this LockEntity target, ZhaEnableLockUserCodeParameters data)
		{
			target.CallService("enable_lock_user_code", data);
		}

		///<summary>Enable a user code on a lock</summary>
		public static void EnableLockUserCode(this IEnumerable<LockEntity> target, ZhaEnableLockUserCodeParameters data)
		{
			target.CallService("enable_lock_user_code", data);
		}

		///<summary>Enable a user code on a lock</summary>
		///<param name="target">The LockEntity to call this service for</param>
		///<param name="codeSlot">Code slot to enable eg: 1</param>
		public static void EnableLockUserCode(this LockEntity target, string @codeSlot)
		{
			target.CallService("enable_lock_user_code", new ZhaEnableLockUserCodeParameters{CodeSlot = @codeSlot});
		}

		///<summary>Enable a user code on a lock</summary>
		///<param name="target">The IEnumerable<LockEntity> to call this service for</param>
		///<param name="codeSlot">Code slot to enable eg: 1</param>
		public static void EnableLockUserCode(this IEnumerable<LockEntity> target, string @codeSlot)
		{
			target.CallService("enable_lock_user_code", new ZhaEnableLockUserCodeParameters{CodeSlot = @codeSlot});
		}

		///<summary>Set a user code on a lock</summary>
		public static void SetLockUserCode(this LockEntity target, ZhaSetLockUserCodeParameters data)
		{
			target.CallService("set_lock_user_code", data);
		}

		///<summary>Set a user code on a lock</summary>
		public static void SetLockUserCode(this IEnumerable<LockEntity> target, ZhaSetLockUserCodeParameters data)
		{
			target.CallService("set_lock_user_code", data);
		}

		///<summary>Set a user code on a lock</summary>
		///<param name="target">The LockEntity to call this service for</param>
		///<param name="codeSlot">Code slot to set the code in eg: 1</param>
		///<param name="userCode">Code to set eg: 1234</param>
		public static void SetLockUserCode(this LockEntity target, string @codeSlot, string @userCode)
		{
			target.CallService("set_lock_user_code", new ZhaSetLockUserCodeParameters{CodeSlot = @codeSlot, UserCode = @userCode});
		}

		///<summary>Set a user code on a lock</summary>
		///<param name="target">The IEnumerable<LockEntity> to call this service for</param>
		///<param name="codeSlot">Code slot to set the code in eg: 1</param>
		///<param name="userCode">Code to set eg: 1234</param>
		public static void SetLockUserCode(this IEnumerable<LockEntity> target, string @codeSlot, string @userCode)
		{
			target.CallService("set_lock_user_code", new ZhaSetLockUserCodeParameters{CodeSlot = @codeSlot, UserCode = @userCode});
		}
	}
}