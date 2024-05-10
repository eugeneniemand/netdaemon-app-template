//acquisitionSurveyReason,adsConfig,animationEnabled,betaStatus,blockedUserIds,blockerUserIds,canUseModerationTools,classroomLeaderboardsEnabled,courses,creationDate,currentCourseId,email,emailAnnouncement,emailAssignment,emailAssignmentComplete,emailClassroomJoin,emailClassroomLeave,emailEditSuggested,emailEventsDigest,emailFollow,emailPass,emailPromotion,emailResearch,emailWeeklyProgressReport,emailSchoolsAnnouncement,emailSchoolsNewsletter,emailSchoolsProductUpdate,emailSchoolsPromotion,emailStreamPost,emailVerified,emailWeeklyReport,enableMicrophone,enableSoundEffects,enableSpeaker,experiments{connect_enable_social_underage_v2,connect_friends_quests_gifting_2,connect_web_migrate_to_feed_service,designsys_web_redesign_settings_page,gweb_diamond_tournament_dogfooding,minfra_web_stripe_setup_intent,path__web_gpt_info_stories,path_web_course_complete_slides,path_web_duoradio_audio_controls_redesign_v2,path_web_example_sentences_with_transliterations,path_web_hover,path_web_persistent_headers_redesign,path_web_remove_about_course_page,path_web_sections_overview,retention_web_fix_lapsed_banner_shows,retention_web_streak_earnback_challenge_v2,spack_web_5xp_g_practice,spack_web_animation_checklist,spack_web_animation_longscroll,spack_web_copysolidate_hearts,spack_web_copysolidate_super_longscroll,spack_web_fp_upgrade_hook,spack_web_hearts_on_off,spack_web_super_promo_d12_ft_ineligible,spack_web_super_promo_d12_pf2_v2,spack_web_upgrade_flow,tsl_web_tournament_fetch_data,tsl_web_tournament_port,web_hintable_text_rewrite_v3,writing_web_pronunciation_bingo},facebookId,fromLanguage,gemsConfig,globalAmbassadorStatus,googleId,hasFacebookId,hasGoogleId,hasPlus,health,id,inviteURL,joinedClassroomIds,lastResurrectionTimestamp,learningLanguage,lingots,location,monthlyXp,name,observedClassroomIds,optionalFeatures,persistentNotifications,picture,plusDiscounts,practiceReminderSettings,privacySettings,referralInfo,rewardBundles,roles,sessionCount,streak,streakData{currentStreak,longestStreak,previousStreak},timezone,timezoneOffset,totalXp,trackingProperties,username,webNotificationIds,weeklyXp,xpGains,xpGoal,zhTw,currentCourse
using System.Text.Json.Serialization;

public class CompletedLevels
    {
        public int skill { get; set; }
        public int chest { get; set; }
        public int unit_review { get; set; }
        public int practice { get; set; }
    }

    public class ConnectEnableSocialUnderageV2
    {
        public string name { get; set; }
        public bool eligible { get; set; }
        public List<object> contexts { get; set; }
        public bool treated { get; set; }
        public string condition { get; set; }
        public string destiny { get; set; }
    }

    public class ConnectFriendsQuestsGifting2
    {
        public string name { get; set; }
        public bool eligible { get; set; }
        public List<object> contexts { get; set; }
        public bool treated { get; set; }
        public string condition { get; set; }
        public string destiny { get; set; }
    }

    public class ConnectWebMigrateToFeedService
    {
        public string name { get; set; }
        public bool eligible { get; set; }
        public List<object> contexts { get; set; }
        public bool treated { get; set; }
        public string condition { get; set; }
        public string destiny { get; set; }
    }

    public class Course
    {
        public bool preload { get; set; }
        public bool placementTestAvailable { get; set; }
        public string authorId { get; set; }
        public string title { get; set; }
        public string learningLanguage { get; set; }
        public int xp { get; set; }
        public bool healthEnabled { get; set; }
        public string fromLanguage { get; set; }
        public string id { get; set; }
        public int crowns { get; set; }
    }

    public class CurrentCourse
    {
        public List<object> assignments { get; set; }
        public int progressVersion { get; set; }
        public bool managedInHouse { get; set; }
        public string subject { get; set; }
        public List<PathSectioned> pathSectioned { get; set; }
        public string activePathSectionId { get; set; }
        public List<object> smartTips { get; set; }
        public string title { get; set; }
        public bool preload { get; set; }
        public TrackingProperties trackingProperties { get; set; }
        public object placementDepth { get; set; }
        public object ttsAccents { get; set; }
        public object alphabetsPathProgressKey { get; set; }
        public int numberOfWords { get; set; }
        public List<List<object>> skills { get; set; }
        public List<object> path { get; set; }
        public int numberOfSentences { get; set; }
        public string id { get; set; }
        public string fromLanguage { get; set; }
        public int wordsLearned { get; set; }
        public object fluency { get; set; }
        public string authorId { get; set; }
        public string finalCheckpointSession { get; set; }
        public PathDetails pathDetails { get; set; }
        public object storiesTabPromotionLocation { get; set; }
        public List<Section> sections { get; set; }
        public SideQuestProgress sideQuestProgress { get; set; }
        public List<object> inLessonAvatars { get; set; }
        public bool placementTestAvailable { get; set; }
        public string learningLanguage { get; set; }
        public int crowns { get; set; }
        public int extraCrowns { get; set; }
        public int xp { get; set; }
        public string topic { get; set; }
        public bool healthEnabled { get; set; }
        public List<object> pathExperiments { get; set; }
        public List<PathSectionsSummary> pathSectionsSummary { get; set; }
        public List<object> checkpointTests { get; set; }
        public string status { get; set; }
    }

    public class CurrentStreak
    {
        public string endDate { get; set; }
        public int length { get; set; }
        public string lastExtendedDate { get; set; }
        public string startDate { get; set; }
    }

    public class DailyRefreshInfo
    {
        public int nodeIndex { get; set; }
        public int expiresAt { get; set; }
    }

    public class De
    {
        public int timeInMinutes { get; set; }
        public bool pushEnabled { get; set; }
        public bool useSmartReminderTime { get; set; }
        public bool emailEnabled { get; set; }
    }

    public class DesignsysWebRedesignSettingsPage
    {
        public string name { get; set; }
        public bool eligible { get; set; }
        public List<object> contexts { get; set; }
        public bool treated { get; set; }
        public string condition { get; set; }
        public string destiny { get; set; }
    }

    public class Es
    {
        public int timeInMinutes { get; set; }
        public bool pushEnabled { get; set; }
        public bool useSmartReminderTime { get; set; }
        public bool emailEnabled { get; set; }
    }

    public class Experiments
    {
        public PathWebPersistentHeadersRedesign path_web_persistent_headers_redesign { get; set; }
        public PathWebSectionsOverview path_web_sections_overview { get; set; }
        public SpackWeb5xpGPractice spack_web_5xp_g_practice { get; set; }
        public SpackWebAnimationChecklist spack_web_animation_checklist { get; set; }
        public TslWebTournamentFetchData tsl_web_tournament_fetch_data { get; set; }
        public SpackWebCopysolidateHearts spack_web_copysolidate_hearts { get; set; }
        public DesignsysWebRedesignSettingsPage designsys_web_redesign_settings_page { get; set; }
        public ConnectWebMigrateToFeedService connect_web_migrate_to_feed_service { get; set; }
        public PathWebRemoveAboutCoursePage path_web_remove_about_course_page { get; set; }
        public RetentionWebStreakEarnbackChallengeV2 retention_web_streak_earnback_challenge_v2 { get; set; }
        public WebHintableTextRewriteV3 web_hintable_text_rewrite_v3 { get; set; }
        public MinfraWebStripeSetupIntent minfra_web_stripe_setup_intent { get; set; }
        public PathWebHover path_web_hover { get; set; }
        public SpackWebAnimationLongscroll spack_web_animation_longscroll { get; set; }
        public ConnectFriendsQuestsGifting2 connect_friends_quests_gifting_2 { get; set; }
        public PathWebDuoradioAudioControlsRedesignV2 path_web_duoradio_audio_controls_redesign_v2 { get; set; }
        public SpackWebSuperPromoD12Pf2V2 spack_web_super_promo_d12_pf2_v2 { get; set; }
        public GwebDiamondTournamentDogfooding gweb_diamond_tournament_dogfooding { get; set; }
        public PathWebCourseCompleteSlides path_web_course_complete_slides { get; set; }
        public SpackWebUpgradeFlow spack_web_upgrade_flow { get; set; }
        public ConnectEnableSocialUnderageV2 connect_enable_social_underage_v2 { get; set; }
        public WritingWebPronunciationBingo writing_web_pronunciation_bingo { get; set; }
        public SpackWebHeartsOnOff spack_web_hearts_on_off { get; set; }
        public SpackWebFpUpgradeHook spack_web_fp_upgrade_hook { get; set; }
        public PathWebGptInfoStories path__web_gpt_info_stories { get; set; }
        public TslWebTournamentPort tsl_web_tournament_port { get; set; }
        public PathWebExampleSentencesWithTransliterations path_web_example_sentences_with_transliterations { get; set; }
        public RetentionWebFixLapsedBannerShows retention_web_fix_lapsed_banner_shows { get; set; }
        public SpackWebCopysolidateSuperLongscroll spack_web_copysolidate_super_longscroll { get; set; }
        public SpackWebSuperPromoD12FtIneligible spack_web_super_promo_d12_ft_ineligible { get; set; }
    }

    public class GemsConfig
    {
        public int gems { get; set; }
        public int gemsPerSkill { get; set; }
        public bool useGems { get; set; }
    }

    public class GlobalAmbassadorStatus
    {
    }

    public class Guidebook
    {
        public string url { get; set; }
    }

    public class GwebDiamondTournamentDogfooding
    {
        public string name { get; set; }
        public bool eligible { get; set; }
        public List<object> contexts { get; set; }
        public bool treated { get; set; }
        public string condition { get; set; }
        public string destiny { get; set; }
    }

    public class Health
    {
        public int maxHearts { get; set; }
        public bool eligibleForFreeRefill { get; set; }
        public bool healthEnabled { get; set; }
        public int secondsPerHeartSegment { get; set; }
        public bool unlimitedHeartsAvailable { get; set; }
        public int hearts { get; set; }
        public object secondsUntilNextHeartSegment { get; set; }
        public bool useHealth { get; set; }
    }

    public class It
    {
        public int timeInMinutes { get; set; }
        public bool pushEnabled { get; set; }
        public bool useSmartReminderTime { get; set; }
        public bool emailEnabled { get; set; }
    }

    public class Item
    {
        public int count { get; set; }
        public string itemType { get; set; }
    }

    public class Level
    {
        public string id { get; set; }
        public string state { get; set; }
        public int finishedSessions { get; set; }
        public PathLevelMetadata pathLevelMetadata { get; set; }
        public PathLevelClientData pathLevelClientData { get; set; }
        public int totalSessions { get; set; }
        public string debugName { get; set; }
        public bool hasLevelReview { get; set; }
        public string type { get; set; }
        public string subtype { get; set; }
        public bool isInProgressSequence { get; set; }
        public DailyRefreshInfo dailyRefreshInfo { get; set; }
    }

    public class LongestStreak
    {
        public string endDate { get; set; }
        public int length { get; set; }
        public string achieveDate { get; set; }
        public string startDate { get; set; }
    }

    public class MinfraWebStripeSetupIntent
    {
        public string name { get; set; }
        public bool eligible { get; set; }
        public List<object> contexts { get; set; }
        public bool treated { get; set; }
        public string condition { get; set; }
        public string destiny { get; set; }
    }

    public class NlNL
    {
        public int timeInMinutes { get; set; }
        public bool pushEnabled { get; set; }
        public bool useSmartReminderTime { get; set; }
        public bool emailEnabled { get; set; }
    }

    public class OptionalFeature
    {
        public string status { get; set; }
        public string id { get; set; }
    }

    public class PathDetails
    {
        public List<object> notifications { get; set; }
        public List<object> clientNotifications { get; set; }
    }

    public class PathLevelClientData
    {
        public string skillId { get; set; }
        public int? crownLevelIndex { get; set; }
        public int hardModeLevelIndex { get; set; }
        public string teachingObjective { get; set; }
        public object assignmentInfo { get; set; }
        public List<string> skillIds { get; set; }
        public bool? isPathExtension { get; set; }
        public int? unitIndex { get; set; }
        public int? numberOfLegendarySessions { get; set; }
        public int? dailyRefreshIndex { get; set; }
        public int? expiresAt { get; set; }
    }

    public class PathLevelMetadata
    {
        public string skillId { get; set; }
        public int crownLevelIndex { get; set; }
        public string anchorSkillId { get; set; }
        public int? indexSinceAnchorSkill { get; set; }
        public string treeId { get; set; }
        public int? unitIndex { get; set; }
        public List<string> skillIds { get; set; }
    }

    public class PathSectioned
    {
        public int index { get; set; }
        public string debugName { get; set; }
        public string type { get; set; }
        public int completedUnits { get; set; }
        public int totalUnits { get; set; }
        public List<Unit> units { get; set; }
        public object cefr { get; set; }
        public Summary summary { get; set; }
        public object exampleSentence { get; set; }
    }

    public class PathSectionsSummary
    {
        public string id { get; set; }
        public int index { get; set; }
        public string debugName { get; set; }
        public string type { get; set; }
        public CompletedLevels completedLevels { get; set; }
        public TotalLevels totalLevels { get; set; }
        public int completedUnits { get; set; }
        public int totalUnits { get; set; }
        public List<int> totalLevelsPerUnit { get; set; }
        public List<int> completedLevelsPerUnit { get; set; }
        public List<string> firstUnitTestSkillIds { get; set; }
        public object cefr { get; set; }
        public Summary summary { get; set; }
        public object exampleSentence { get; set; }
    }

    public class PathWebCourseCompleteSlides
    {
        public string name { get; set; }
        public bool eligible { get; set; }
        public List<object> contexts { get; set; }
        public bool treated { get; set; }
        public string condition { get; set; }
        public string destiny { get; set; }
    }

    public class PathWebDuoradioAudioControlsRedesignV2
    {
        public string name { get; set; }
        public bool eligible { get; set; }
        public List<object> contexts { get; set; }
        public bool treated { get; set; }
        public string condition { get; set; }
        public string destiny { get; set; }
    }

    public class PathWebExampleSentencesWithTransliterations
    {
        public string name { get; set; }
        public bool eligible { get; set; }
        public List<object> contexts { get; set; }
        public bool treated { get; set; }
        public string condition { get; set; }
        public string destiny { get; set; }
    }

    public class PathWebGptInfoStories
    {
        public string name { get; set; }
        public bool eligible { get; set; }
        public List<object> contexts { get; set; }
        public bool treated { get; set; }
        public string condition { get; set; }
        public string destiny { get; set; }
    }

    public class PathWebHover
    {
        public string name { get; set; }
        public bool eligible { get; set; }
        public List<object> contexts { get; set; }
        public bool treated { get; set; }
        public string condition { get; set; }
        public string destiny { get; set; }
    }

    public class PathWebPersistentHeadersRedesign
    {
        public string name { get; set; }
        public bool eligible { get; set; }
        public List<object> contexts { get; set; }
        public bool treated { get; set; }
        public string condition { get; set; }
        public string destiny { get; set; }
    }

    public class PathWebRemoveAboutCoursePage
    {
        public string name { get; set; }
        public bool eligible { get; set; }
        public List<object> contexts { get; set; }
        public bool treated { get; set; }
        public string condition { get; set; }
        public string destiny { get; set; }
    }

    public class PathWebSectionsOverview
    {
        public string name { get; set; }
        public bool eligible { get; set; }
        public List<object> contexts { get; set; }
        public bool treated { get; set; }
        public string condition { get; set; }
        public string destiny { get; set; }
    }

    public class PracticeReminderSettings
    {
        [JsonPropertyName("nl-NL")]
        public NlNL nl { get; set; }
        public De de { get; set; }
        public It it { get; set; }
        public Es es { get; set; }
    }

    public class PreviousStreak
    {
        public string endDate { get; set; }
        public int length { get; set; }
        public string lastExtendedDate { get; set; }
        public string startDate { get; set; }
    }

    public class ReferralInfo
    {
        public object inviterName { get; set; }
        public bool isEligibleForOffer { get; set; }
        public object unconsumedInviteeName { get; set; }
        public List<object> unconsumedInviteeIds { get; set; }
        public int numBonusesReady { get; set; }
        public bool hasReachedCap { get; set; }
        public bool isEligibleForBonus { get; set; }
    }

    public class RetentionWebFixLapsedBannerShows
    {
        public string name { get; set; }
        public bool eligible { get; set; }
        public List<object> contexts { get; set; }
        public bool treated { get; set; }
        public string condition { get; set; }
        public string destiny { get; set; }
    }

    public class RetentionWebStreakEarnbackChallengeV2
    {
        public string name { get; set; }
        public bool eligible { get; set; }
        public List<object> contexts { get; set; }
        public bool treated { get; set; }
        public string condition { get; set; }
        public string destiny { get; set; }
    }

    public class Reward
    {
        public List<object> tags { get; set; }
        public bool consumed { get; set; }
        public string rewardType { get; set; }
        public string currency { get; set; }
        public int amount { get; set; }
        public string id { get; set; }
        public string itemId { get; set; }
        public List<Item> items { get; set; }
    }

    public class RewardBundle
    {
        public List<Reward> rewards { get; set; }
        public string id { get; set; }
        public bool empty { get; set; }
        public string rewardBundleType { get; set; }
    }

    public class Root
    {
        // public List<object> blockerUserIds { get; set; }
        // public bool emailSchoolsProductUpdate { get; set; }
        // public List<object> joinedClassroomIds { get; set; }
        // public bool hasFacebookId { get; set; }
        // public TrackingProperties trackingProperties { get; set; }
        // public bool emailSchoolsPromotion { get; set; }
        // public bool animationEnabled { get; set; }
        public int totalXp { get; set; }
        // public string timezoneOffset { get; set; }
        public int sessionCount { get; set; }
        // public string betaStatus { get; set; }
        // public string inviteURL { get; set; }
        // public int id { get; set; }
        // public GemsConfig gemsConfig { get; set; }
        // public List<object> webNotificationIds { get; set; }
        // public bool emailClassroomJoin { get; set; }
        // public int lastResurrectionTimestamp { get; set; }
        // public List<XpGain> xpGains { get; set; }
        // public List<object> blockedUserIds { get; set; }
        // public List<Course> courses { get; set; }
        // public bool emailClassroomLeave { get; set; }
        // public bool emailAnnouncement { get; set; }
        // public bool emailSchoolsNewsletter { get; set; }
        public int weeklyXp { get; set; }
        public int streak { get; set; }
        // public int creationDate { get; set; }
        // public bool emailPass { get; set; }
        // public bool emailSchoolsAnnouncement { get; set; }
        // public bool enableMicrophone { get; set; }
        // public string acquisitionSurveyReason { get; set; }
        // public bool emailEditSuggested { get; set; }
        // public object xpGoal { get; set; }
        // public bool emailResearch { get; set; }
        // public bool enableSoundEffects { get; set; }
        // public List<OptionalFeature> optionalFeatures { get; set; }
        public PracticeReminderSettings practiceReminderSettings { get; set; }
        // public bool emailFollow { get; set; }
        // public List<RewardBundle> rewardBundles { get; set; }
        // public string timezone { get; set; }
        // public GlobalAmbassadorStatus globalAmbassadorStatus { get; set; }
        // public List<string> roles { get; set; }
        // public Experiments experiments { get; set; }
        // public bool emailWeeklyProgressReport { get; set; }
        // public bool emailAssignmentComplete { get; set; }
        // public bool emailPromotion { get; set; }
        public CurrentCourse currentCourse { get; set; }
        // public List<object> plusDiscounts { get; set; }
        // public bool hasPlus { get; set; }
        // public bool emailEventsDigest { get; set; }
        // public string email { get; set; }
        // public bool emailWeeklyReport { get; set; }
        // public bool classroomLeaderboardsEnabled { get; set; }
        // public List<string> persistentNotifications { get; set; }
        // public string fromLanguage { get; set; }
        // public List<object> observedClassroomIds { get; set; }
        // public bool hasGoogleId { get; set; }
        public Health health { get; set; }
        // public bool emailStreamPost { get; set; }
        // public ReferralInfo referralInfo { get; set; }
        // public List<object> privacySettings { get; set; }
        public StreakData streakData { get; set; }
        // public string picture { get; set; }
        // public bool canUseModerationTools { get; set; }
        // public bool emailVerified { get; set; }
        // public string currentCourseId { get; set; }
        // public bool emailAssignment { get; set; }
        // public int lingots { get; set; }
        // public int monthlyXp { get; set; }
        // public string learningLanguage { get; set; }
        // public bool enableSpeaker { get; set; }
        public string username { get; set; }
    }

    public class Section
    {
        public bool checkpointAccessible { get; set; }
        public bool checkpointFinished { get; set; }
        public string checkpointSessionType { get; set; }
        public object masteryScore { get; set; }
        public string name { get; set; }
        public int numRows { get; set; }
        public object summary { get; set; }
        public object cefrLevel { get; set; }
    }

    public class SideQuestProgress
    {
    }

    public class SpackWeb5xpGPractice
    {
        public string name { get; set; }
        public bool eligible { get; set; }
        public List<object> contexts { get; set; }
        public bool treated { get; set; }
        public string condition { get; set; }
        public string destiny { get; set; }
    }

    public class SpackWebAnimationChecklist
    {
        public string name { get; set; }
        public bool eligible { get; set; }
        public List<object> contexts { get; set; }
        public bool treated { get; set; }
        public string condition { get; set; }
        public string destiny { get; set; }
    }

    public class SpackWebAnimationLongscroll
    {
        public string name { get; set; }
        public bool eligible { get; set; }
        public List<object> contexts { get; set; }
        public bool treated { get; set; }
        public string condition { get; set; }
        public string destiny { get; set; }
    }

    public class SpackWebCopysolidateHearts
    {
        public string name { get; set; }
        public bool eligible { get; set; }
        public List<object> contexts { get; set; }
        public bool treated { get; set; }
        public string condition { get; set; }
        public string destiny { get; set; }
    }

    public class SpackWebCopysolidateSuperLongscroll
    {
        public string name { get; set; }
        public bool eligible { get; set; }
        public List<object> contexts { get; set; }
        public bool treated { get; set; }
        public string condition { get; set; }
        public string destiny { get; set; }
    }

    public class SpackWebFpUpgradeHook
    {
        public string name { get; set; }
        public bool eligible { get; set; }
        public List<object> contexts { get; set; }
        public bool treated { get; set; }
        public string condition { get; set; }
        public string destiny { get; set; }
    }

    public class SpackWebHeartsOnOff
    {
        public string name { get; set; }
        public bool eligible { get; set; }
        public List<object> contexts { get; set; }
        public bool treated { get; set; }
        public string condition { get; set; }
        public string destiny { get; set; }
    }

    public class SpackWebSuperPromoD12FtIneligible
    {
        public string name { get; set; }
        public bool eligible { get; set; }
        public List<object> contexts { get; set; }
        public bool treated { get; set; }
        public string condition { get; set; }
        public string destiny { get; set; }
    }

    public class SpackWebSuperPromoD12Pf2V2
    {
        public string name { get; set; }
        public bool eligible { get; set; }
        public List<object> contexts { get; set; }
        public bool treated { get; set; }
        public string condition { get; set; }
        public string destiny { get; set; }
    }

    public class SpackWebUpgradeFlow
    {
        public string name { get; set; }
        public bool eligible { get; set; }
        public List<object> contexts { get; set; }
        public bool treated { get; set; }
        public string condition { get; set; }
        public string destiny { get; set; }
    }

    public class StreakData
    {
        public CurrentStreak currentStreak { get; set; }
        public PreviousStreak previousStreak { get; set; }
        public LongestStreak longestStreak { get; set; }
    }

    public class Summary
    {
        public string grammarConceptUrl { get; set; }
        public object cefrContentUrl { get; set; }
    }

    public class TotalLevels
    {
        public int skill { get; set; }
        public int chest { get; set; }
        public int unit_review { get; set; }
        public int practice { get; set; }
    }

    public class TrackingProperties
    {
        public bool disable_clubs { get; set; }
        public string skill_tree_id { get; set; }
        public bool disable_social { get; set; }
        public bool notification_sms_enabled { get; set; }
        public bool has_item_weekend_amulet { get; set; }
        public object beta_shake_to_report_enabled { get; set; }
        public long creation_age { get; set; }
        public bool has_item_gold_subscription { get; set; }
        public DateTime creation_date_new { get; set; }
        public string learning_language { get; set; }
        public bool has_item_streak_wager { get; set; }
        public bool disable_discussions { get; set; }
        public string beta_enrollment_status { get; set; }
        public object placement_depth { get; set; }
        public int num_sessions_completed { get; set; }
        public int level { get; set; }
        public bool disable_friends_quests { get; set; }
        public bool disable_leaderboards { get; set; }
        public int streak { get; set; }
        public string acquisition_survey_reason { get; set; }
        public bool notification_wechat_enabled { get; set; }
        public bool disable_third_party_tracking { get; set; }
        public bool notification_whatsapp_enabled { get; set; }
        public bool has_item_immersive_subscription { get; set; }
        public int gems { get; set; }
        public int user_id { get; set; }
        public int distinct_id { get; set; }
        public bool disable_personalized_ads { get; set; }
        public double utc_offset { get; set; }
        public string course_topic_id { get; set; }
        public bool has_picture { get; set; }
        public bool has_item_live_subscription { get; set; }
        public bool is_age_restricted { get; set; }
        public object placement_section_index { get; set; }
        public int num_followers { get; set; }
        public bool trial_account { get; set; }
        public object prior_proficiency_onboarding { get; set; }
        public bool disable_stream { get; set; }
        public string course_subject { get; set; }
        public bool has_item_premium_subscription { get; set; }
        public int num_following { get; set; }
        public bool disable_kudos { get; set; }
        public string direction { get; set; }
        public long creation_date_millis { get; set; }
        public bool disable_profile_country { get; set; }
        public string course_id { get; set; }
        public bool has_item_rupee_wager { get; set; }
        public int num_item_streak_freeze { get; set; }
        public bool has_item_streak_freeze { get; set; }
        public string learning_reason { get; set; }
        public bool disable_events { get; set; }
        public bool disable_mature_words { get; set; }
        public int lingots { get; set; }
        public int leaderboard_league { get; set; }
        public bool disable_immersion { get; set; }
        public string username { get; set; }
        public string ui_language { get; set; }
        public object max_cefr_level { get; set; }
        public int max_tree_level { get; set; }
        public int max_section_index { get; set; }
        public bool took_placementtest { get; set; }
        public int path_position_active_node_index { get; set; }
        public int path_position_active_unit_index { get; set; }
        public int path_position_active_section_index { get; set; }
        public object path_position_active_section_cefr { get; set; }
        public bool path_uses_unit_vision { get; set; }
    }

    public class TslWebTournamentFetchData
    {
        public string name { get; set; }
        public bool eligible { get; set; }
        public List<object> contexts { get; set; }
        public bool treated { get; set; }
        public string condition { get; set; }
        public string destiny { get; set; }
    }

    public class TslWebTournamentPort
    {
        public string name { get; set; }
        public bool eligible { get; set; }
        public List<object> contexts { get; set; }
        public bool treated { get; set; }
        public string condition { get; set; }
        public string destiny { get; set; }
    }

    public class Unit
    {
        public int unitIndex { get; set; }
        public List<Level> levels { get; set; }
        public Guidebook guidebook { get; set; }
        public string teachingObjective { get; set; }
        public object cefrLevel { get; set; }
    }

    public class WebHintableTextRewriteV3
    {
        public string name { get; set; }
        public bool eligible { get; set; }
        public List<object> contexts { get; set; }
        public bool treated { get; set; }
        public string condition { get; set; }
        public string destiny { get; set; }
    }

    public class WritingWebPronunciationBingo
    {
        public string name { get; set; }
        public bool eligible { get; set; }
        public List<object> contexts { get; set; }
        public bool treated { get; set; }
        public string condition { get; set; }
        public string destiny { get; set; }
    }

    public class XpGain
    {
        public string skillId { get; set; }
        public int xp { get; set; }
        public string eventType { get; set; }
        public int time { get; set; }
    }

