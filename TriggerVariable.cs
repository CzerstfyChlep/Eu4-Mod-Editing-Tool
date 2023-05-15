using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Eu4ModEditor.TriggerVariable;

namespace Eu4ModEditor
{
    public class TriggerVariable
    {
        public string Name = "";
        public Value ExpectedValue;
        public Scope AllowedScopes;
        public string Description;

        /*
        public static readonly TriggerVariable NamedAdvisor = new TriggerVariable()
        {
            Name = "<advisor>",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has hired an advisor of the specified type which has at least level X."
        };

        public static readonly TriggerVariable NamedBuilding = new TriggerVariable()
        {
            Name = "<building>",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X buildings from the specified building type."
        };

        public static readonly TriggerVariable NamedIdeaGroup = new TriggerVariable()
        {
            Name = "<idea group>",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X ideas from the specified idea group."
        };

        public static readonly TriggerVariable NamedInstitution = new TriggerVariable()
        {
            Name = "<institution>",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the support for the specified institution in the province is at least X."
        };

        public static readonly TriggerVariable NamedReligion = new TriggerVariable()
        {
            Name = "<religion>",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a tolerance of at least X of the specified religion.\r\nNote: No correct localisation."
        };

        public static readonly TriggerVariable NamedSubjectType = new TriggerVariable()
        {
            Name = "<subject_type>",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X subjects of the given type."
        };
        public static readonly TriggerVariable NamedTradeGood = new TriggerVariable()
        {
            Name = "<trade_good>",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = ""
        };


        public static readonly TriggerVariable absolutism = new TriggerVariable()
        {
            Name = "absolutism",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X absolutism."
        };

        public static readonly TriggerVariable accepted_culture = new TriggerVariable()
        {
            Name = "accepted_culture",
            ExpectedValue = Value.IdentifierORProvinceScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country accepts the specified culture."
        };

        public static readonly TriggerVariable active_major_mission = new TriggerVariable()
        {
            Name = "active_major_mission",
            ExpectedValue = Value.MissionIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if X is the current mission of the country."
        };

        public static readonly TriggerVariable adm = new TriggerVariable()
        {
            Name = "adm",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a ruler with an administrative skill of at least X."
        };

        public static readonly TriggerVariable adm_power = new TriggerVariable()
        {
            Name = "adm_power",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X administrative power."
        };

        public static readonly TriggerVariable adm_tech = new TriggerVariable()
        {
            Name = "adm_tech",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has an administrative technology of at least X."
        };

        public static readonly TriggerVariable advisor = new TriggerVariable()
        {
            Name = "advisor",
            ExpectedValue = Value.AdvisorID,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has an advisor of the specified type."
        };

        public static readonly TriggerVariable advisor_exists = new TriggerVariable()
        {
            Name = "advisor_exists",
            ExpectedValue = Value.AdvisorID,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the advisor X exists."
        };

        public static readonly TriggerVariable ai = new TriggerVariable()
        {
            Name = "ai",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is controlled by the AI."
        };

        public static readonly TriggerVariable alliance_with = new TriggerVariable()
        {
            Name = "alliance_with",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has an alliance with X."
        };

        public static readonly TriggerVariable allows_female_emperor = new TriggerVariable()
        {
            Name = "allows_female_emperor",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true if females can become the emperor."
        };

        public static readonly TriggerVariable always = new TriggerVariable()
        {
            Name = "always",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true under all circumstances if set to yes, returns false under all circumstances if set to no."
        };

        public static readonly TriggerVariable area = new TriggerVariable()
        {
            Name = "area",
            ExpectedValue = Value.AreaIdentifier,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province is part of the area X."
        };

        public static readonly TriggerVariable army_size = new TriggerVariable()
        {
            Name = "army_size",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has an army of at least X k soldiers."
        };

        public static readonly TriggerVariable army_size_percentage = new TriggerVariable()
        {
            Name = "army_size_percentage",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the total army size of the country is at least X% of its [[land force limit]]."
        };

        public static readonly TriggerVariable army_professionalism = new TriggerVariable()
        {
            Name = "army_professionalism",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country's army professionalism is of at least X."
        };

        public static readonly TriggerVariable army_tradition = new TriggerVariable()
        {
            Name = "army_tradition",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has an army tradition of at least X."
        };

        public static readonly TriggerVariable artillery_fraction = new TriggerVariable()
        {
            Name = "artillery_fraction",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the ratio of the artillery fraction to the army size of the country is at least X."
        };

        public static readonly TriggerVariable artillery_in_province = new TriggerVariable()
        {
            Name = "artillery_in_province",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Province,
            Description = "Returns true if there are at least X artillery units in the province."
        };

        public static readonly TriggerVariable at_war_with_religious_enemy = new TriggerVariable()
        {
            Name = "at_war_with_religious_enemy",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is at war with any country of a different religion."
        };

        public static readonly TriggerVariable authority = new TriggerVariable()
        {
            Name = "authority",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the Inti country has at least X authority.\n\tReturns true if the Inti country has at least as much authority as the specified country."
        };

        public static readonly TriggerVariable average_autonomy = new TriggerVariable()
        {
            Name = "average_autonomy",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has an average autonomy in its provinces of at least X."
        };

        public static readonly TriggerVariable average_autonomy_above_min = new TriggerVariable()
        {
            Name = "average_autonomy_above_min",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = ""
        };

        public static readonly TriggerVariable average_effective_unrest = new TriggerVariable()
        {
            Name = "average_effective_unrest",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = ""
        };

        public static readonly TriggerVariable average_home_autonomy = new TriggerVariable()
        {
            Name = "average_home_autonomy",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has an average autonomy in its provinces excluding overseas provinces is at least X."
        };

        public static readonly TriggerVariable average_unrest = new TriggerVariable()
        {
            Name = "average_unrest",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has an average unrest in its provinces of at least X."
        };

        public static readonly TriggerVariable base_manpower = new TriggerVariable()
        {
            Name = "base_manpower",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the base manpower of the province is at least X."
        };

        public static readonly TriggerVariable base_production = new TriggerVariable()
        {
            Name = "base_production",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the base production of the province is at least X."
        };

        public static readonly TriggerVariable base_tax = new TriggerVariable()
        {
            Name = "base_tax",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the base tax of the province is at least X."
        };

        public static readonly TriggerVariable blockade = new TriggerVariable()
        {
            Name = "blockade",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the blockade penalty of the country is at least X%."
        };

        public static readonly TriggerVariable can_be_overlord = new TriggerVariable()
        {
            Name = "can_be_overlord",
            ExpectedValue = Value.SubjectIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if country meets the conditions defined in the subject type's is_potential_overlord section."
        };

        public static readonly TriggerVariable can_build = new TriggerVariable()
        {
            Name = "can_build",
            ExpectedValue = Value.BuildingIdentifier,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the specified building can be built in the province."
        };

        public static readonly TriggerVariable can_create_vassals = new TriggerVariable()
        {
            Name = "can_create_vassals",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country can create a vassal.Warning: Interprets anything after ‘=’ as “yes”. Note: Works only for independent countries."
        };

        public static readonly TriggerVariable can_heir_be_child_of_consort = new TriggerVariable()
        {
            Name = "can_heir_be_child_of_consort",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country’s heir can potentially be the consort’s child."
        };

        public static readonly TriggerVariable can_justify_trade_conflict = new TriggerVariable()
        {
            Name = "can_justify_trade_conflict",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country can justify a trade conflict against country X."
        };

        public static readonly TriggerVariable can_migrate = new TriggerVariable()
        {
            Name = "can_migrate",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country can migrate to another province. Doesn't return true if the timer is still counting down."
        };

        public static readonly TriggerVariable can_spawn_rebels = new TriggerVariable()
        {
            Name = "can_spawn_rebels",
            ExpectedValue = Value.RebelIdentifier,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the specified rebel faction is a valid rebel faction of the province."
        };

        public static readonly TriggerVariable capital = new TriggerVariable()
        {
            Name = "capital",
            ExpectedValue = Value.ProvinceID,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country's capital is the province with the ID X."
        };

        public static readonly TriggerVariable cavalry_fraction = new TriggerVariable()
        {
            Name = "cavalry_fraction",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the ratio of the cavalry fraction to the army size of the country is at least X."
        };

        public static readonly TriggerVariable cavalry_in_province = new TriggerVariable()
        {
            Name = "cavalry_in_province",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Province,
            Description = "Returns true if there are at least X cavalry units in the province."
        };

        public static readonly TriggerVariable province_has_center_of_trade_of_level = new TriggerVariable()
        {
            Name = "province_has_center_of_trade_of_level",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province has a center of trade of at least this level."
        };

        public static readonly TriggerVariable church_power = new TriggerVariable()
        {
            Name = "church_power",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X church power."
        };

        public static readonly TriggerVariable coalition_target = new TriggerVariable()
        {
            Name = "coalition_target",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is the target of a coalition."
        };

        public static readonly TriggerVariable colonial_claim_by_anyone_of_religion = new TriggerVariable()
        {
            Name = "colonial_claim_by_anyone_of_religion",
            ExpectedValue = Value.ReligionIdentifier,
            AllowedScopes = Scope.Province,
            Description = "Returns true if ... ''has gotten a colonial grant for the scope's colonial region from any potential pope-like entities.''"
        };

        public static readonly TriggerVariable colonial_region = new TriggerVariable()
        {
            Name = "colonial_region",
            ExpectedValue = Value.ColonialRegionIdentifier,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province is part of the colonial region X."
        };

        public static readonly TriggerVariable colony = new TriggerVariable()
        {
            Name = "colony",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if country has at least that many colonial subjects."
        };

        public static readonly TriggerVariable colony_claim = new TriggerVariable()
        {
            Name = "colony_claim",
            ExpectedValue = Value.Tag,
            AllowedScopes = Scope.Country,
            Description = "Returns true if country has claim on colony"
        };

        public static readonly TriggerVariable colonysize = new TriggerVariable()
        {
            Name = "colonysize",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Province,
            Description = "Returns true if colony is at least size X."
        };

        public static readonly TriggerVariable consort_adm = new TriggerVariable()
        {
            Name = "consort_adm",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a consort with an administrative skill of at least X."
        };

        public static readonly TriggerVariable consort_age = new TriggerVariable()
        {
            Name = "consort_age",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country’s consort has an age of at least X.<br>Note: Always returns false if there is no consort."
        };

        public static readonly TriggerVariable consort_dip = new TriggerVariable()
        {
            Name = "consort_dip",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a consort with an diplomatic skill of at least X."
        };

        public static readonly TriggerVariable consort_culture = new TriggerVariable()
        {
            Name = "consort_culture",
            ExpectedValue = Value.CultureIdentifierORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country's consort has the specified culture. Can utilise Variables."
        };

        public static readonly TriggerVariable consort_has_personality = new TriggerVariable()
        {
            Name = "consort_has_personality",
            ExpectedValue = Value.PersonalityIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country’s consort has the specified personality."
        };

        public static readonly TriggerVariable consort_mil = new TriggerVariable()
        {
            Name = "consort_mil",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a consort with an military skill of at least X."
        };

        public static readonly TriggerVariable consort_religion = new TriggerVariable()
        {
            Name = "consort_religion",
            ExpectedValue = Value.ReligionIdentifierORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country's consort has the specified religion. Can utilise Variables."
        };

        public static readonly TriggerVariable construction_progress = new TriggerVariable()
        {
            Name = "construction_progress",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the construction progress is at least X%."
        };

        public static readonly TriggerVariable continent = new TriggerVariable()
        {
            Name = "continent",
            ExpectedValue = Value.ContinentIdetifierORTagORScopeORProvinceID,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province is located on continent X."
        };

        public static readonly TriggerVariable controlled_by = new TriggerVariable()
        {
            Name = "controlled_by",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province is controlled by X."
        };

        public static readonly TriggerVariable controls = new TriggerVariable()
        {
            Name = "controls",
            ExpectedValue = Value.ProvinceID,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the province with id X is controlled by the country."
        };

        public static readonly TriggerVariable core_claim = new TriggerVariable()
        {
            Name = "core_claim",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a core on any province owned by country X."
        };

        public static readonly TriggerVariable core_percentage = new TriggerVariable()
        {
            Name = "core_percentage",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has cored at least X% of its provinces."
        };

        public static readonly TriggerVariable corruption = new TriggerVariable()
        {
            Name = "corruption",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a [[corruption]] of at least X."
        };

        public static readonly TriggerVariable council_position = new TriggerVariable()
        {
            Name = "council_position",
            ExpectedValue = Value.CouncilPositionIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if X is the country's council position in the Council of Trent."
        };

        public static readonly TriggerVariable country_or_non_sovereign_subject_holds = new TriggerVariable()
        {
            Name = "country_or_non_sovereign_subject_holds",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province is part of the specified country or its non-tributary subjects."
        };

        public static readonly TriggerVariable country_or_subject_holds = new TriggerVariable()
        {
            Name = "country_or_subject_holds",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province is part of the specified country or its subjects."
        };

        public static readonly TriggerVariable crown_land_share = new TriggerVariable()
        {
            Name = "crown_land_share",
            ExpectedValue = Value.IntegerOREstateIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the % of the country's development is held by the Crown Land. If an estate is declared, returns true if % of Crown Land is higher than land owned by the estate."
        };

        public static readonly TriggerVariable culture = new TriggerVariable()
        {
            Name = "culture",
            ExpectedValue = Value.CultureIdentifierORScope,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province culture is X."
        };

        public static readonly TriggerVariable culture_group = new TriggerVariable()
        {
            Name = "culture_group",
            ExpectedValue = Value.CultureIdentifierORScope,
            AllowedScopes = Scope.CountryORProvince,
            Description = "Returns true if the country/province has a culture of the specified culture group."
        };

        public static readonly TriggerVariable culture_group_claim = new TriggerVariable()
        {
            Name = "culture_group_claim",
            ExpectedValue = Value.Tag,
            AllowedScopes = Scope.Country,
            Description = "Returns true if country's primary culture is the same culture group as any province owned by country X."
        };

        public static readonly TriggerVariable current_age = new TriggerVariable()
        {
            Name = "current_age",
            ExpectedValue = Value.AgeIdentifier,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true, if the current age is X."
        };

        public static readonly TriggerVariable current_bribe = new TriggerVariable()
        {
            Name = "current_bribe",
            ExpectedValue = Value.BribeIdentifier,
            AllowedScopes = Scope.Province,
            Description = "Returns true, if the seat in the province wants bribe X."
        };

        public static readonly TriggerVariable current_debate = new TriggerVariable()
        {
            Name = "current_debate",
            ExpectedValue = Value.DebateIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the parliament of the country debates about X."
        };

        public static readonly TriggerVariable current_icon = new TriggerVariable()
        {
            Name = "current_icon",
            ExpectedValue = Value.IconIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country has the specified (orthodox) icon."
        };

        public static readonly TriggerVariable current_income_balance = new TriggerVariable()
        {
            Name = "current_income_balance",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the current income balance is X."
        };

        public static readonly TriggerVariable current_institution = new TriggerVariable()
        {
            Name = "current_institution",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the current institution (technically the first unembraced meaning in case you have two unembraced, it would be the older one) progress is at least X"
        };

        public static readonly TriggerVariable current_institution_growth = new TriggerVariable()
        {
            Name = "current_institution_growth",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.CountryORProvince,
            Description = "Returns true, if the country/province has a annual (whereas monthly is printed in province screen) institution growth of at least X for the first not embraced institution."
        };

        public static readonly TriggerVariable current_size_of_parliament = new TriggerVariable()
        {
            Name = "current_size_of_parliament",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the parliament of the country has a least X seats."
        };

        public static readonly TriggerVariable defensive_war_with = new TriggerVariable()
        {
            Name = "defensive_war_with",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is in a defensive war with country X."
        };

        public static readonly TriggerVariable devastation = new TriggerVariable()
        {
            Name = "devastation",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the devastation of the province is at least X."
        };

        public static readonly TriggerVariable development = new TriggerVariable()
        {
            Name = "development",
            ExpectedValue = Value.IntegerORScope,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the development of the province is at least X. Does not accept base scopes as of 1.32."
        };
        public static readonly TriggerVariable development_of_overlord_fraction = new TriggerVariable()
        {
            Name = "development_of_overlord_fraction",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country has X percent of its overlord's development"
        };

        public static readonly TriggerVariable devotion = new TriggerVariable()
        {
            Name = "devotion",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X devotion."
        };

        public static readonly TriggerVariable dip = new TriggerVariable()
        {
            Name = "dip",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a ruler with a diplomatic skill of at least X."
        };

        public static readonly TriggerVariable diplomatic_reputation = new TriggerVariable()
        {
            Name = "diplomatic_reputation",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a diplomatic reputation of at least X."
        };

        public static readonly TriggerVariable dip_power = new TriggerVariable()
        {
            Name = "dip_power",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X diplomatic power."
        };
        public static readonly TriggerVariable dip_tech = new TriggerVariable()
        {
            Name = "dip_tech",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has an diplomatic technology of at least level X."
        };

        public static readonly TriggerVariable dominant_culture = new TriggerVariable()
        {
            Name = "dominant_culture",
            ExpectedValue = Value.CultureIdentifierORCapital,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the dominant culture in the country is X."
        };

        public static readonly TriggerVariable dominant_religion = new TriggerVariable()
        {
            Name = "dominant_religion",
            ExpectedValue = Value.ReligionIdentifierORCapital,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the dominant religion in the country is X."
        };

        public static readonly TriggerVariable doom = new TriggerVariable()
        {
            Name = "doom",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X doom."
        };

        public static readonly TriggerVariable dynasty = new TriggerVariable()
        {
            Name = "dynasty",
            ExpectedValue = Value.StringORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the ruling dynasty of the country is X."
        };

        public static readonly TriggerVariable empire_of_china_reform_passed = new TriggerVariable()
        {
            Name = "empire_of_china_reform_passed",
            ExpectedValue = Value.ChinaReformIdentifier,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true, if X Empire of China reform is enacted."
        };

        public static readonly TriggerVariable estate_led_regency_influence = new TriggerVariable()
        {
            Name = "estate_led_regency_influence",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the estate leading the regency in the country has at least X influence."
        };

        public static readonly TriggerVariable estate_led_regency_loyalty = new TriggerVariable()
        {
            Name = "estate_led_regency_loyalty",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the estate leading the regency in the country has at least X loyalty."
        };


        public static readonly TriggerVariable exiled_same_dynasty_as_current = new TriggerVariable()
        {
            Name = "exiled_same_dynasty_as_current",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if an exiled ruler has the same dynasty as the current one."
        };

        public static readonly TriggerVariable exists = new TriggerVariable()
        {
            Name = "exists",
            ExpectedValue = Value.TagORBoolean,
            AllowedScopes = Scope.CountryORAnywhere,
            Description = "Returns true if country X exists."
        };

        public static readonly TriggerVariable faction_in_power = new TriggerVariable()
        {
            Name = "faction_in_power",
            ExpectedValue = Value.FactionIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the faction in power of the country is X."
        };

        public static readonly TriggerVariable federation_size = new TriggerVariable()
        {
            Name = "federation_size",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the federation with the country has at least X members."
        };

        public static readonly TriggerVariable fervor = new TriggerVariable()
        {
            Name = "fervor",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has stored at least X fervor points."
        };

        public static readonly TriggerVariable fort_level = new TriggerVariable()
        {
            Name = "fort_level",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the fort in the province has at least level X."
        };


        public static readonly TriggerVariable full_idea_group = new TriggerVariable()
        {
            Name = "full_idea_group",
            ExpectedValue = Value.IdeaGroupIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has completed the X idea group."
        };

        public static readonly TriggerVariable galley_fraction = new TriggerVariable()
        {
            Name = "galley_fraction",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the ratio of the galley fraction to the navy size of the country is at least X."
        };

        public static readonly TriggerVariable galleys_in_province = new TriggerVariable()
        {
            Name = "galleys_in_province",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Province,
            Description = "Returns true if there are at least X galleys in the province."
        };
        public static readonly TriggerVariable garrison = new TriggerVariable()
        {
            Name = "garrison",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the garrison is at X men."
        };
        public static readonly TriggerVariable gives_military_access_to = new TriggerVariable()
        {
            Name = "gives_military_access_to",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the scoped country gives military access to the specified country."
        };
        public static readonly TriggerVariable gives_fleet_basing_rights_to = new TriggerVariable()
        {
            Name = "gives_fleet_basing_rights_to",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the scoped country gives fleet basing rights to the specified country."
        };

        public static readonly TriggerVariable gold_income = new TriggerVariable()
        {
            Name = "gold_income",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has an income from Trade_goods#Gold_mines|Gold of at least X."
        };

        public static readonly TriggerVariable gold_income_percentage = new TriggerVariable()
        {
            Name = "gold_income_percentage",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Country,
            Description = "Returns true if in the country the proportion of income from Trade_goods#Gold_mines|Gold is at least X."
        };

        public static readonly TriggerVariable government = new TriggerVariable()
        {
            Name = "government",
            ExpectedValue = Value.GovernmentIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has government type X, or government reforms that alter specific mechanics (Harem, Dictatorship).<br>Identifier: monarchy, has_harem, republic, dictatorship, theocracy, tribal, native."
        };

        public static readonly TriggerVariable government_rank = new TriggerVariable()
        {
            Name = "government_rank",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a government rank of X or higher."
        };

        public static readonly TriggerVariable grown_by_development = new TriggerVariable()
        {
            Name = "grown_by_development",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country's total development has grown by the specified amount"
        };


        public static readonly TriggerVariable grown_by_states = new TriggerVariable()
        {
            Name = "grown_by_states",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country's total number of states (not areas) has grown by the specified amount. The states don't have to be fully owned and the provinces don't need full cores."
        };

        public static readonly TriggerVariable great_power_rank = new TriggerVariable()
        {
            Name = "great_power_rank",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a great power rank of X or worse."
        };

        public static readonly TriggerVariable guaranteed_by = new TriggerVariable()
        {
            Name = "guaranteed_by",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is guaranteed by X."
        };

        public static readonly TriggerVariable had_recent_war = new TriggerVariable()
        {
            Name = "had_recent_war",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country fought a war within the last X years."
        };


        //TODO }</pre>
        public static readonly TriggerVariable harmonization_progress = new TriggerVariable()
        {
            Name = "harmonization_progress",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country's current harmonization progress is at least at X."
        };

        public static readonly TriggerVariable harmony = new TriggerVariable()
        {
            Name = "harmony",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country has at least X harmony."
        };

        public static readonly TriggerVariable has_active_debate = new TriggerVariable()
        {
            Name = "has_active_debate",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has ongoing debate in parliament."
        };

        public static readonly TriggerVariable has_active_fervor = new TriggerVariable()
        {
            Name = "has_active_fervor",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has activated a fervor effect."
        };

        public static readonly TriggerVariable has_active_policy = new TriggerVariable()
        {
            Name = "has_active_policy",
            ExpectedValue = Value.PolicyIdentfier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if country has the specified policy active."
        };

        public static readonly TriggerVariable has_active_triggered_province_modifier = new TriggerVariable()
        {
            Name = "has_active_triggered_province_modifier",
            ExpectedValue = Value.ModifierIdentifier,
            AllowedScopes = Scope.Province,
            Description = "Returns true if province has the specified triggered province modifier."
        };

        public static readonly TriggerVariable has_adopted_cult = new TriggerVariable()
        {
            Name = "has_adopted_cult",
            ExpectedValue = Value.CultIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has adopted the specified cult."
        };

        public static readonly TriggerVariable has_advisor = new TriggerVariable()
        {
            Name = "has_advisor",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has hired an advisor. Warning: Interprets every right side argument as <code>yes</code>."
        };

        public static readonly TriggerVariable has_age_ability = new TriggerVariable()
        {
            Name = "has_age_ability",
            ExpectedValue = Value.AgeAbilityIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country has the specified age ability."
        };

        public static readonly TriggerVariable has_any_disaster = new TriggerVariable()
        {
            Name = "has_any_disaster",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is currently in a disaster."
        };

        public static readonly TriggerVariable has_border_with_religious_enemy = new TriggerVariable()
        {
            Name = "has_border_with_religious_enemy",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country borders any country of a different religion. Warning: Interprets anything after ‘=’ as “yes”."
        };

        public static readonly TriggerVariable has_building = new TriggerVariable()
        {
            Name = "has_building",
            ExpectedValue = Value.BuildingIdentifier,
            AllowedScopes = Scope.Province,
            Description = "Returns true if there is the specified building in the province."
        };

        public static readonly TriggerVariable has_cardinal = new TriggerVariable()
        {
            Name = "has_cardinal",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province has a cardinal in the curia."
        };

        public static readonly TriggerVariable has_casus_belli_against = new TriggerVariable()
        {
            Name = "has_casus_belli_against",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a casus belli against country X."
        };

        public static readonly TriggerVariable has_center_of_trade_of_level = new TriggerVariable()
        {
            Name = "has_center_of_trade_of_level",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a center of trade of a least level X."
        };

        public static readonly TriggerVariable has_changed_nation = new TriggerVariable()
        {
            Name = "has_changed_nation",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if playing as a released vassal."
        };

        public static readonly TriggerVariable has_church_aspect = new TriggerVariable()
        {
            Name = "has_church_aspect",
            ExpectedValue = Value.ChurchAspectIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has enabled the church aspect X."
        };

        public static readonly TriggerVariable has_climate = new TriggerVariable()
        {
            Name = "has_climate",
            ExpectedValue = Value.ClimateIdentifier,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province has climate X."
        };

        public static readonly TriggerVariable has_colonial_parent = new TriggerVariable()
        {
            Name = "has_colonial_parent",
            ExpectedValue = Value.Tag,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the specified country is the colonial parent of the country."
        };

        public static readonly TriggerVariable has_colonist = new TriggerVariable()
        {
            Name = "has_colonist",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if scoped province has an active colonist"
        };

        public static readonly TriggerVariable has_commanding_three_star = new TriggerVariable()
        {
            Name = "has_commanding_three_star",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if country has a three star general."
        };

        public static readonly TriggerVariable has_consort = new TriggerVariable()
        {
            Name = "has_consort",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a consort."
        };

        public static readonly TriggerVariable has_consort_flag = new TriggerVariable()
        {
            Name = "has_consort_flag",
            ExpectedValue = Value.ConsortFlagIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the consort flag X is set."
        };

        public static readonly TriggerVariable has_consort_regency = new TriggerVariable()
        {
            Name = "has_consort_regency",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a consort | consort regency."
        };

        public static readonly TriggerVariable has_construction = new TriggerVariable()
        {
            Name = "has_construction",
            ExpectedValue = Value.Construction,
            AllowedScopes = Scope.Province,
            Description = "Returns true if there is the specified construction in progress in the province.<br>Possible values are core, culture, building, merchant, diplomat, missionary, army, navy, canal, great_project etc."
        };

        public static readonly TriggerVariable has_country_flag = new TriggerVariable()
        {
            Name = "has_country_flag",
            ExpectedValue = Value.CountryFlagIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the flag X is set for the country."
        };

        public static readonly TriggerVariable has_country_modifier = new TriggerVariable()
        {
            Name = "has_country_modifier",
            ExpectedValue = Value.ModifierIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has the modifier X."
        };

        public static readonly TriggerVariable has_custom_ideas = new TriggerVariable()
        {
            Name = "has_custom_ideas",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has custom ideas."
        };

        public static readonly TriggerVariable has_disaster = new TriggerVariable()
        {
            Name = "has_disaster",
            ExpectedValue = Value.DisasterIdentfier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is currently in the disaster X."
        };
        public static readonly TriggerVariable has_discovered = new TriggerVariable()
        {
            Name = "has_discovered",
            ExpectedValue = Value.ProvinceIDORTagORScope,
            AllowedScopes = Scope.CountryORProvince,
            Description = "Returns true if the country has discovered the province with the ID X."
        };

        public static readonly TriggerVariable has_dlc = new TriggerVariable()
        {
            Name = "has_dlc",
            ExpectedValue = Value.DLCIdentifier,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true if the DLC X is enabled."
        };

        public static readonly TriggerVariable has_divert_trade = new TriggerVariable()
        {
            Name = "has_divert_trade",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the protectorate has divert trade to its overlord."
        };

        public static readonly TriggerVariable has_embargo_rivals = new TriggerVariable()
        {
            Name = "has_embargo_rivals",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the subject nation is embargoing overlord's rivals."
        };

        public static readonly TriggerVariable has_empty_adjacent_province = new TriggerVariable()
        {
            Name = "has_empty_adjacent_province",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if an adjacent province is uncolonized. Warning: Works only with 'yes'."
        };

        public static readonly TriggerVariable has_estate = new TriggerVariable()
        {
            Name = "has_estate",
            ExpectedValue = Value.EstateIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has estate X."
        };

        public static readonly TriggerVariable has_estate_province = new TriggerVariable()
        {
            Name = "has_estate",
            ExpectedValue = Value.BooleanOREstateIdentifier,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province is controlled by an estate."
        };

        public static readonly TriggerVariable has_estate_loan = new TriggerVariable()
        {
            Name = "has_estate_loan",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has an estate loan."
        };

        public static readonly TriggerVariable has_estate_privilege = new TriggerVariable()
        {
            Name = "has_estate_privilege",
            ExpectedValue = Value.PrivilegeIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has the listed privilege."
        };

        public static readonly TriggerVariable has_faction = new TriggerVariable()
        {
            Name = "has_faction",
            ExpectedValue = Value.FactionIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has the specified faction."
        };

        public static readonly TriggerVariable has_factions = new TriggerVariable()
        {
            Name = "has_factions",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if country has factions."
        };

        public static readonly TriggerVariable has_female_consort = new TriggerVariable()
        {
            Name = "has_female_consort",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a female consort."
        };

        public static readonly TriggerVariable has_female_heir = new TriggerVariable()
        {
            Name = "has_female_heir",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a female heir."
        };

        public static readonly TriggerVariable has_first_revolution_started = new TriggerVariable()
        {
            Name = "has_first_revolution_started",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true if a revolution has happened in the world."
        };

        public static readonly TriggerVariable has_flagship = new TriggerVariable()
        {
            Name = "has_flagship",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a flagship."
        };

        public static readonly TriggerVariable has_foreign_consort = new TriggerVariable()
        {
            Name = "has_foreign_consort",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has foreign consort."
        };

        public static readonly TriggerVariable has_foreign_heir = new TriggerVariable()
        {
            Name = "has_foreign_heir",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has foreign heir."
        };

        public static readonly TriggerVariable has_friendly_reformation_center = new TriggerVariable()
        {
            Name = "has_friendly_reformation_center",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a friendly center of reformation."
        };

        public static readonly TriggerVariable has_game_started = new TriggerVariable()
        {
            Name = "has_game_started",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true if the game has started."
        };

        public static readonly TriggerVariable has_given_consort_to = new TriggerVariable()
        {
            Name = "has_given_consort_to",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the scoped country has given a consort to the specified country."
        };

        public static readonly TriggerVariable has_guaranteed = new TriggerVariable()
        {
            Name = "has_guaranteed",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has guaranteed country X."
        };

        public static readonly TriggerVariable has_global_flag = new TriggerVariable()
        {
            Name = "has_global_flag",
            ExpectedValue = Value.GlobalFlagIdentifier,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true if the specified global flag is set."
        };
        public static readonly TriggerVariable has_government_mechanic = new TriggerVariable()
        {
            Name = "has_government_mechanic",
            ExpectedValue = Value.GovernmentMechanicIdentfier,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country uses the specified government mechanic."
        };

        public static readonly TriggerVariable has_government_power = new TriggerVariable()
        {
            Name = "has_government_power",
            ExpectedValue = Value.GovernmentPowerIdentifier,
            AllowedScopes = Scope.Country,
            Description = "''Description needed''"
        };

        public static readonly TriggerVariable has_had_golden_age = new TriggerVariable()
        {
            Name = "has_had_golden_age",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country has a golden age."
        };

        public static readonly TriggerVariable has_harmonized_with = new TriggerVariable()
        {
            Name = "has_harmonized_with",
            ExpectedValue = Value.ReligionORReligionGroupORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country has harmonized with the specified religion or religion group."
        };

        public static readonly TriggerVariable has_harsh_treatment = new TriggerVariable()
        {
            Name = "has_harsh_treatment",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "''Description needed''"
        };

        public static readonly TriggerVariable has_heir = new TriggerVariable()
        {
            Name = "has_heir",
            ExpectedValue = Value.StringORBoolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has an heir (named X)."
        };

        public static readonly TriggerVariable has_heir_flag = new TriggerVariable()
        {
            Name = "has_heir_flag",
            ExpectedValue = Value.HeirFlagIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the heir flag X is set."
        };

        public static readonly TriggerVariable has_heir_leader_from = new TriggerVariable()
        {
            Name = "has_heir_leader_from",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Province,
            Description = "Returns true if an army in the province is led by an heir from X."
        };

        public static readonly TriggerVariable has_hostile_reformation_center = new TriggerVariable()
        {
            Name = "has_hostile_reformation_center",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a hostile center of reformation."
        };

        public static readonly TriggerVariable has_idea = new TriggerVariable()
        {
            Name = "has_idea",
            ExpectedValue = Value.IdeaIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has the X idea."
        };

        public static readonly TriggerVariable has_idea_group = new TriggerVariable()
        {
            Name = "has_idea_group",
            ExpectedValue = Value.IdeaGroupIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has chosen the X idea group."
        };

        public static readonly TriggerVariable has_influencing_fort = new TriggerVariable()
        {
            Name = "has_influencing_fort",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province in the Zone of control of a fort."
        };

        public static readonly TriggerVariable has_institution = new TriggerVariable()
        {
            Name = "has_institution",
            ExpectedValue = Value.InstitutionIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has the specified institution."
        };

        public static readonly TriggerVariable has_latent_trade_goods = new TriggerVariable()
        {
            Name = "has_latent_trade_goods",
            ExpectedValue = Value.TradeGoodIdentifier,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province has the specified latent trade good."
        };

        public static readonly TriggerVariable has_leader = new TriggerVariable()
        {
            Name = "has_leader",
            ExpectedValue = Value.String,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has the leader \"X\"."
        };

        public static readonly TriggerVariable has_matching_religion = new TriggerVariable()
        {
            Name = "has_matching_religion",
            ExpectedValue = Value.ReligionIdentifierORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has the specified religion or syncretic faith."
        };
        public static readonly TriggerVariable has_merchant = new TriggerVariable()
        {
            Name = "has_merchant",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the scoped country has an active merchant in the trade node."
        };

        public static readonly TriggerVariable has_mission = new TriggerVariable()
        {
            Name = "has_mission",
            ExpectedValue = Value.MissionIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has the specified mission."
        };

        public static readonly TriggerVariable has_missionary = new TriggerVariable()
        {
            Name = "has_missionary",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province has an active missionary."
        };

        public static readonly TriggerVariable has_monsoon = new TriggerVariable()
        {
            Name = "has_monsoon",
            ExpectedValue = Value.MonsoonIdentifier,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province has monsoon X."
        };

        public static readonly TriggerVariable has_most_province_trade_power = new TriggerVariable()
        {
            Name = "has_most_province_trade_power",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the country X has most amount of trade power in trade node."
        };

        public static readonly TriggerVariable has_new_dynasty = new TriggerVariable()
        {
            Name = "has_new_dynasty",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a new dynasty."
        };

        public static readonly TriggerVariable has_owner_accepted_culture = new TriggerVariable()
        {
            Name = "has_owner_accepted_culture",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the culture of the province is an accepted culture(NOT primary!) of its owner."
        };

        public static readonly TriggerVariable has_owner_culture = new TriggerVariable()
        {
            Name = "has_owner_culture",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province has the primary culture of its owner."
        };

        public static readonly TriggerVariable has_owner_religion = new TriggerVariable()
        {
            Name = "has_owner_religion",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province has the religion of its owner."
        };

        public static readonly TriggerVariable has_pasha = new TriggerVariable()
        {
            Name = "has_pasha",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province has a pasha."
        };

        public static readonly TriggerVariable has_parliament = new TriggerVariable()
        {
            Name = "has_parliament",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a parliament."
        };

        public static readonly TriggerVariable has_personal_deity = new TriggerVariable()
        {
            Name = "has_personal_deity",
            ExpectedValue = Value.PersonalDietyIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the ruler of the country has picked the specified personal deity."
        };

        public static readonly TriggerVariable has_pillaged_capital_against = new TriggerVariable()
        {
            Name = "has_pillaged_capital_against",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has used the Pillage Capital peace treaty against the specified country."
        };

        public static readonly TriggerVariable has_port = new TriggerVariable()
        {
            Name = "has_port",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if it is a coastal province."
        };

        public static readonly TriggerVariable has_privateers = new TriggerVariable()
        {
            Name = "has_privateers",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has privateers in any trade node."
        };

        public static readonly TriggerVariable has_promote_investments = new TriggerVariable()
        {
            Name = "has_promote_investments",
            ExpectedValue = Value.TradeCompanyIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is promoting investments in the specified trade company (region)."
        };

        public static readonly TriggerVariable has_province_flag = new TriggerVariable()
        {
            Name = "has_province_flag",
            ExpectedValue = Value.ProvinceFlagIdentifier,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province has the province flag X."
        };

        public static readonly TriggerVariable has_province_modifier = new TriggerVariable()
        {
            Name = "has_province_modifier",
            ExpectedValue = Value.ModifierIdentifier,
            AllowedScopes = Scope.Province,
            Description = "Returns true if province has the province modifier X.<br /> (Checks also for triggered province modifiers.)"
        };

        public static readonly TriggerVariable has_rebel_faction = new TriggerVariable()
        {
            Name = "has_rebel_faction",
            ExpectedValue = Value.RebelIdentifier,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province is controlled by (the specified) rebel faction."
        };


        public static readonly TriggerVariable has_regency = new TriggerVariable()
        {
            Name = "has_regency",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a regency."
        };

        public static readonly TriggerVariable has_reform = new TriggerVariable()
        {
            Name = "has_reform",
            ExpectedValue = Value.ReformIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has the specific government reform."
        };

        public static readonly TriggerVariable government_reform_progress = new TriggerVariable()
        {
            Name = "government_reform_progress",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has x or more reform progress saved up."
        };

        public static readonly TriggerVariable has_removed_fow = new TriggerVariable()
        {
            Name = "has_removed_fow",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has lifted the fog of war from the specified country."
        };

        public static readonly TriggerVariable has_revolution_in_province = new TriggerVariable()
        {
            Name = "has_revolution_in_province",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the revolution is present in the province."
        };

        public static readonly TriggerVariable has_ruler = new TriggerVariable()
        {
            Name = "has_ruler",
            ExpectedValue = Value.String,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a ruler named \"X\"."
        };

        public static readonly TriggerVariable has_ruler_flag = new TriggerVariable()
        {
            Name = "has_ruler_flag",
            ExpectedValue = Value.RulerFlagIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the ruler flag X is set for the country."
        };

        public static readonly TriggerVariable has_ruler_leader_from = new TriggerVariable()
        {
            Name = "has_ruler_leader_from",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Province,
            Description = "Returns true if an army in the province is led by a ruler from X."
        };

        public static readonly TriggerVariable has_ruler_modifier = new TriggerVariable()
        {
            Name = "has_ruler_modifier",
            ExpectedValue = Value.ModifierIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true the country has the modifier X until the ruler changes."
        };

        public static readonly TriggerVariable has_saved_event_target = new TriggerVariable()
        {
            Name = "has_saved_event_target",
            ExpectedValue = Value.EventTargetIdentifier,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true if the specified event target has been saved."
        };

        public static readonly TriggerVariable has_scutage = new TriggerVariable()
        {
            Name = "has_scutage",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the vassal pays scutage."
        };

        public static readonly TriggerVariable has_seat_in_parliament = new TriggerVariable()
        {
            Name = "has_seat_in_parliament",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province has a seat in parliament."
        };

        public static readonly TriggerVariable has_secondary_religion = new TriggerVariable()
        {
            Name = "has_secondary_religion",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the county has a secondary religion."
        };

        public static readonly TriggerVariable has_send_officers = new TriggerVariable()
        {
            Name = "has_send_officers",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the protectorate has received officers."
        };

        public static readonly TriggerVariable has_siege = new TriggerVariable()
        {
            Name = "has_siege",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province has siege."
        };

        public static readonly TriggerVariable has_spawned_rebels = new TriggerVariable()
        {
            Name = "has_spawned_rebels",
            ExpectedValue = Value.RebelIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if rebels of the specified type are active in the country."
        };

        public static readonly TriggerVariable has_spawned_supported_rebels = new TriggerVariable()
        {
            Name = "has_spawned_supported_rebels",
            ExpectedValue = Value.Scope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if rebels which were supported by the specified country are active in the country."
        };

        public static readonly TriggerVariable has_state_edict = new TriggerVariable()
        {
            Name = "has_state_edict",
            ExpectedValue = Value.StateEdictIdentifier,
            AllowedScopes = Scope.State,
            Description = "Returns true if the state has X edict enabled."
        };

        public static readonly TriggerVariable has_state_patriach = new TriggerVariable()
        {
            Name = "has_state_patriach",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true, if the province has any state patriarch."
        };

        public static readonly TriggerVariable has_subsidize_armies = new TriggerVariable()
        {
            Name = "has_subsidize_armies",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the march has subsidizes armies."
        };

        public static readonly TriggerVariable has_supply_depot = new TriggerVariable()
        {
            Name = "has_supply_depot",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province has a supply depot built by X"
        };

        public static readonly TriggerVariable has_support_loyalists = new TriggerVariable()
        {
            Name = "has_support_loyalists",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the subject nation receives support for loyalists."
        };

        public static readonly TriggerVariable has_subject_of_type = new TriggerVariable()
        {
            Name = "has_subject_of_type",
            ExpectedValue = Value.SubjectIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least one subject of the specified subject type."
        };

        public static readonly TriggerVariable has_switched_nation = new TriggerVariable()
        {
            Name = "has_switched_nation",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true if the player has changed nation by playing as a Vassal#Release_a_nation|released vassal."
        };

        public static readonly TriggerVariable has_terrain = new TriggerVariable()
        {
            Name = "has_terrain",
            ExpectedValue = Value.TerrainIdentifier,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province has terrain X. Terrains are listed in base game file terrains.txt"
        };

        public static readonly TriggerVariable has_trader = new TriggerVariable()
        {
            Name = "has_trader",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the specified country has a merchant in the trade node."
        };

        public static readonly TriggerVariable has_truce = new TriggerVariable()
        {
            Name = "has_truce",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a truce."
        };

        public static readonly TriggerVariable has_unembraced_institution = new TriggerVariable()
        {
            Name = "has_unembraced_institution",
            ExpectedValue = Value.InstitutionIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has not embraced the specified institution."
        };

        public static readonly TriggerVariable has_unified_culture_group = new TriggerVariable()
        {
            Name = "has_unified_culture_group",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country owns all provinces of its culture group."
        };

        public static readonly TriggerVariable has_unit_type = new TriggerVariable()
        {
            Name = "has_unit_type",
            ExpectedValue = Value.UnitTypeIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has selected unit type X as preferable unit."
        };

        public static readonly TriggerVariable has_unlocked_cult = new TriggerVariable()
        {
            Name = "has_unlocked_cult",
            ExpectedValue = Value.UnitTypeIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has unlocked the specified cult."
        };

        public static readonly TriggerVariable has_wartaxes = new TriggerVariable()
        {
            Name = "has_wartaxes",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has raised war taxes."
        };

        public static readonly TriggerVariable has_winter = new TriggerVariable()
        {
            Name = "has_winter",
            ExpectedValue = Value.WinterIdentifier,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province has winter X."
        };
        public static readonly TriggerVariable have_had_reform = new TriggerVariable()
        {
            Name = "have_had_reform",
            ExpectedValue = Value.ReformIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has previously had the specific government reform."
        };

        public static readonly TriggerVariable heavy_ship_fraction = new TriggerVariable()
        {
            Name = "heavy_ship_fraction",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the ratio of the heavy ship fraction to the navy size of the country is at least X."
        };

        public static readonly TriggerVariable heavy_ships_in_province = new TriggerVariable()
        {
            Name = "heavy_ships_in_province",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Province,
            Description = "Returns true if there are at least X heavy ships in the province."
        };
        public static readonly TriggerVariable heir_adm = new TriggerVariable()
        {
            Name = "heir_adm",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has an heir with an administrative skill of at least X."
        };

        public static readonly TriggerVariable heir_age = new TriggerVariable()
        {
            Name = "heir_age",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has an heir that is at least X years old.<br>Note: Always returns false if there is no heir."
        };

        public static readonly TriggerVariable heir_dip = new TriggerVariable()
        {
            Name = "heir_dip",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has an heir with a diplomatic skill of at least X."
        };

        public static readonly TriggerVariable heir_claim = new TriggerVariable()
        {
            Name = "heir_claim",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has an heir with a claim strength of at least X."
        };

        public static readonly TriggerVariable heir_culture = new TriggerVariable()
        {
            Name = "heir_culture",
            ExpectedValue = Value.CultureIdentifierORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country's heir has the specified culture. Can utilise Variables|Event Scope Values."
        };

        public static readonly TriggerVariable heir_has_consort_dynasty = new TriggerVariable()
        {
            Name = "heir_has_consort_dynasty",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country’s heir and consort have the same dynasty."
        };

        public static readonly TriggerVariable heir_has_personality = new TriggerVariable()
        {
            Name = "heir_has_personality",
            ExpectedValue = Value.PersonalityIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country’s heir has the specified personality."
        };

        public static readonly TriggerVariable heir_has_ruler_dynasty = new TriggerVariable()
        {
            Name = "heir_has_ruler_dynasty",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country’s heir and ruler have the same dynasty."
        };

        public static readonly TriggerVariable heir_mil = new TriggerVariable()
        {
            Name = "heir_mil",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has an heir with a military skill of at least X."
        };

        public static readonly TriggerVariable heir_nationality = new TriggerVariable()
        {
            Name = "heir_nationality",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has an heir with nationality X."
        };

        public static readonly TriggerVariable heir_religion = new TriggerVariable()
        {
            Name = "heir_religion",
            ExpectedValue = Value.ReligionIdentifierORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country's heir has the specified religion. Can utilise Variables|Event Scope Values."
        };

        public static readonly TriggerVariable higher_development_than = new TriggerVariable()
        {
            Name = "higher_development_than",
            ExpectedValue = Value.ProvinceIDORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the scope has a higher development than the province."
        };

        public static readonly TriggerVariable highest_value_trade_node = new TriggerVariable()
        {
            Name = "highest_value_trade_node",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the trade node is the highest valued trade node in the world. The value is calculated as total trade value minus outgoing trade value."
        };

        public static readonly TriggerVariable historical_friend_with = new TriggerVariable()
        {
            Name = "historical_friend_with",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the countries are historical friends."
        };

        public static readonly TriggerVariable historical_rival_with = new TriggerVariable()
        {
            Name = "historical_rival_with",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the countries are historical rivals"
        };

        public static readonly TriggerVariable holy_order = new TriggerVariable()
        {
            Name = "holy_order",
            ExpectedValue = Value.HolyOrderIdentifier,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province has the specific Holy Order active."
        };

        public static readonly TriggerVariable horde_unity = new TriggerVariable()
        {
            Name = "horde_unity",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a horde unity of at least X."
        };

        public static readonly TriggerVariable hre_heretic_religion = new TriggerVariable()
        {
            Name = "hre_heretic_religion",
            ExpectedValue = Value.ReligionIdentifierORScope,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true if the specified religion is the opposition religion of the HRE.<br>Note: No localisation for the negation."
        };

        public static readonly TriggerVariable hre_leagues_enabled = new TriggerVariable()
        {
            Name = "hre_leagues_enabled",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true religious leagues are enabled."
        };

        public static readonly TriggerVariable hre_reform_passed = new TriggerVariable()
        {
            Name = "hre_reform_passed",
            ExpectedValue = Value.HREReformIdentifier,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true if the specific imperial reform is enacted."
        };

        public static readonly TriggerVariable hre_religion = new TriggerVariable()
        {
            Name = "hre_religion",
            ExpectedValue = Value.ReligionIdentifierORScope,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true if the specified religion is the dominant faith of the HRE."
        };

        public static readonly TriggerVariable hre_religion_locked = new TriggerVariable()
        {
            Name = "hre_religion_locked",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true if an official faith of the HRE has been permanently established."
        };

        public static readonly TriggerVariable hre_religion_treaty = new TriggerVariable()
        {
            Name = "hre_religion_treaty",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true if the treaty of religious peace in the HRE has been signed."
        };

        public static readonly TriggerVariable hre_size = new TriggerVariable()
        {
            Name = "hre_size",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true if the HRE contains at least X members."
        };

        public static readonly TriggerVariable imperial_influence = new TriggerVariable()
        {
            Name = "imperial_influence",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true if the emperor of the HRE has an imperial authority of at least X."
        };

        public static readonly TriggerVariable imperial_mandate = new TriggerVariable()
        {
            Name = "imperial_mandate",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true, if the emperor of China has at least X mandate."
        };

        public static readonly TriggerVariable in_golden_age = new TriggerVariable()
        {
            Name = "in_golden_age",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country currently is in a golden age."
        };

        public static readonly TriggerVariable infantry_fraction = new TriggerVariable()
        {
            Name = "infantry_fraction",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the ratio of the infantry fraction to the army size of the country is at least X."
        };

        public static readonly TriggerVariable infantry_in_province = new TriggerVariable()
        {
            Name = "infantry_in_province",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Province,
            Description = "Returns true if there are at least X infantry units in the province."
        };

        public static readonly TriggerVariable inflation = new TriggerVariable()
        {
            Name = "inflation",
            ExpectedValue = Value.FloatORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has an inflation of at least X"
        };

        public static readonly TriggerVariable innovativeness = new TriggerVariable()
        {
            Name = "innovativeness",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the countries has at least X innovativeness."
        };

        public static readonly TriggerVariable invested_papal_influence = new TriggerVariable()
        {
            Name = "invested_papal_influence",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has invested at least X papal influence in the election of the next papal controller."
        };

        public static readonly TriggerVariable in_league = new TriggerVariable()
        {
            Name = "in_league",
            ExpectedValue = Value.LeagueIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the countries is in the X league."
        };

        public static readonly TriggerVariable ironman = new TriggerVariable()
        {
            Name = "ironman",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true if the ironman mode is enabled"
        };

        public static readonly TriggerVariable is_advisor_employed = new TriggerVariable()
        {
            Name = "is_advisor_employed",
            ExpectedValue = Value.AdvisorID,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true if the advisor with the ID X is employed."
        };

        public static readonly TriggerVariable is_all_concessions_in_council_taken = new TriggerVariable()
        {
            Name = "is_all_concessions_in_council_taken",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true if all concessions in the Council of Trent have been taken."
        };

        public static readonly TriggerVariable is_at_war = new TriggerVariable()
        {
            Name = "is_at_war",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is at war."
        };

        public static readonly TriggerVariable is_backing_current_issue = new TriggerVariable()
        {
            Name = "is_backing_current_issue",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province is backing the current issue in the parliament."
        };

        public static readonly TriggerVariable is_bankrupt = new TriggerVariable()
        {
            Name = "is_bankrupt",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is bankrupt."
        };

        public static readonly TriggerVariable is_blockaded = new TriggerVariable()
        {
            Name = "is_blockaded",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province is blockaded."
        };

        public static readonly TriggerVariable is_blockaded_by = new TriggerVariable()
        {
            Name = "is_blockaded_by",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province is blockaded by the specified country."
        };

        public static readonly TriggerVariable is_capital = new TriggerVariable()
        {
            Name = "is_capital",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province is a capital."
        };

        public static readonly TriggerVariable is_capital_of = new TriggerVariable()
        {
            Name = "is_capital_of",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province the capital of the specified country."
        };

        public static readonly TriggerVariable is_city = new TriggerVariable()
        {
            Name = "is_city",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province is a city, i.e. has a population of at least 1000."
        };

        public static readonly TriggerVariable is_claim = new TriggerVariable()
        {
            Name = "is_claim",
            ExpectedValue = Value.ProvinceIDORTagORScope,
            AllowedScopes = Scope.CountryORProvince,
            Description = "Returns true if the country has a claim on the province with the ID X. Returns true if the specified country has a claim on the province."
        };

        public static readonly TriggerVariable is_client_nation = new TriggerVariable()
        {
            Name = "is_client_nation",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is a client state."
        };

        public static readonly TriggerVariable is_client_nation_of = new TriggerVariable()
        {
            Name = "is_client_nation_of",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is a client state of X."
        };

        public static readonly TriggerVariable is_colonial_nation = new TriggerVariable()
        {
            Name = "is_colonial_nation",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is a non-independent colonial nation."
        };

        public static readonly TriggerVariable is_colonial_nation_of = new TriggerVariable()
        {
            Name = "is_colonial_nation_of",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is a colonial nation of X."
        };

        public static readonly TriggerVariable is_colony = new TriggerVariable()
        {
            Name = "is_colony",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province is a colony."
        };

        public static readonly TriggerVariable is_core = new TriggerVariable()
        {
            Name = "is_core",
            ExpectedValue = Value.ProvinceIDORTagORScope,
            AllowedScopes = Scope.CountryORProvince,
            Description = "Returns true if the country has a core on the province with the ID X. Returns true if the country X has a core on the province."
        };
        public static readonly TriggerVariable is_council_enabled = new TriggerVariable()
        {
            Name = "is_council_enabled",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true if the Council of Trent has started."
        };

        public static readonly TriggerVariable is_crusade_target = new TriggerVariable()
        {
            Name = "is_crusade_target",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is the target of a crusade."
        };

        public static readonly TriggerVariable is_defender_of_faith = new TriggerVariable()
        {
            Name = "is_defender_of_faith",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is the defender of the faith."
        };

        public static readonly TriggerVariable is_defender_of_faith_of_tier = new TriggerVariable()
        {
            Name = "is_defender_of_faith_of_tier",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is the defender of the faith of at least tier X. Works even without the Emperor DLC, even though the tier is not shown in game in that case."
        };

        public static readonly TriggerVariable is_dynamic_tag = new TriggerVariable()
        {
            Name = "is_dynamic_tag",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country is a dynamically created tag (e.g. client states)."
        };

        public static readonly TriggerVariable is_elector = new TriggerVariable()
        {
            Name = "is_elector",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is an elector of the HRE."
        };

        public static readonly TriggerVariable is_emperor = new TriggerVariable()
        {
            Name = "is_emperor",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is the Holy Roman Empire#Emperor|emperor of the HRE."
        };

        public static readonly TriggerVariable is_emperor_of_china = new TriggerVariable()
        {
            Name = "is_emperor_of_china",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is the emperor of China."
        };

        public static readonly TriggerVariable is_empty = new TriggerVariable()
        {
            Name = "is_empty",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if province is empty"
        };

        public static readonly TriggerVariable is_enemy = new TriggerVariable()
        {
            Name = "is_enemy",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if country X has rivalled the country."
        };

        public static readonly TriggerVariable is_excommunicated = new TriggerVariable()
        {
            Name = "is_excommunicated",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the ruler of the country is excommunicated."
        };

        public static readonly TriggerVariable is_federation_leader = new TriggerVariable()
        {
            Name = "is_federation_leader",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is a federation leader."
        };

        public static readonly TriggerVariable is_female = new TriggerVariable()
        {
            Name = "is_female",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if ruler of the country is female."
        };

        public static readonly TriggerVariable is_force_converted = new TriggerVariable()
        {
            Name = "is_force_converted",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if country has been force-converted via either religious rebels or an Enforce Religion peace treaty.<br>Note: The subject interaction Enforce Religion does not cause the subject to be considered as force-converted."
        };

        public static readonly TriggerVariable is_former_colonial_nation = new TriggerVariable()
        {
            Name = "is_former_colonial_nation",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is a colonial nation that has gained independence."
        };

        public static readonly TriggerVariable is_foreign_claim = new TriggerVariable()
        {
            Name = "is_foreign_claim",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province is a (permanent) claim of any country which is not the owner of the province"
        };


        public static readonly TriggerVariable is_great_power = new TriggerVariable()
        {
            Name = "is_great_power",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is a great power."
        };

        public static readonly TriggerVariable is_harmonizing_with = new TriggerVariable()
        {
            Name = "is_harmonizing_with",
            ExpectedValue = Value.ReligionORReligionGroupORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country is currently harmonizing with the specified religion or religion group."
        };

        public static readonly TriggerVariable is_heir_leader = new TriggerVariable()
        {
            Name = "is_heir_leader",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the heir of the country is a general.<br>Note: Proper localisation for the negation only with <code>NOT = …</code>."
        };

        public static readonly TriggerVariable is_hegemon = new TriggerVariable()
        {
            Name = "is_hegemon",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is considered one of the three Hegemon types."
        };

        public static readonly TriggerVariable is_hegemon_of_type = new TriggerVariable()
        {
            Name = "is_hegemon_of_type",
            ExpectedValue = Value.HegemonTypeIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country holds the specific Hegemony."
        };
        public static readonly TriggerVariable is_imperial_ban_allowed = new TriggerVariable()
        {
            Name = "is_imperial_ban_allowed",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true if the Holy Roman Empire#Emperor|emperor has a casus belli on occupiers of the Empire.<br>Note: Enabled/Disabled with Call for Reichsreform."
        };

        public static readonly TriggerVariable is_incident_active = new TriggerVariable()
        {
            Name = "is_incident_active",
            ExpectedValue = Value.IncidentIdentifierORBooleanORAnyORNone,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the specified incident is active."
        };

        public static readonly TriggerVariable is_incident_happened = new TriggerVariable()
        {
            Name = "is_incident_happened",
            ExpectedValue = Value.IncidentIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the specified incident already happened."
        };

        public static readonly TriggerVariable is_incident_possible = new TriggerVariable()
        {
            Name = "is_incident_possible",
            ExpectedValue = Value.IncidentIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the specified incident can possibly happen."
        };

        public static readonly TriggerVariable is_incident_potential = new TriggerVariable()
        {
            Name = "is_incident_potential",
            ExpectedValue = Value.IncidentIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the specified incident is visible and therefor can happen in the future."
        };

        public static readonly TriggerVariable is_institution_enabled = new TriggerVariable()
        {
            Name = "is_institution_enabled",
            ExpectedValue = Value.IncidentIdentifier,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true if the specified institution has been discovered."
        };

        public static readonly TriggerVariable is_institution_origin = new TriggerVariable()
        {
            Name = "is_institution_origin",
            ExpectedValue = Value.InstitutionIdentifier,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province is the origin of the specified institution."
        };

        public static readonly TriggerVariable is_in_capital_area = new TriggerVariable()
        {
            Name = "is_in_capital_area",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province is connected (i.e. has a land connection including straits) to the capital of its owner."
        };

        public static readonly TriggerVariable is_in_coalition = new TriggerVariable()
        {
            Name = "is_in_coalition",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is in a coalition."
        };

        public static readonly TriggerVariable is_in_coalition_war = new TriggerVariable()
        {
            Name = "is_in_coalition_war",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is fighting a coalition war."
        };

        public static readonly TriggerVariable is_in_deficit = new TriggerVariable()
        {
            Name = "is_in_deficit",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is running a deficit."
        };

        public static readonly TriggerVariable is_in_extended_regency = new TriggerVariable()
        {
            Name = "is_in_extended_regency",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is inside the period an extended regency."
        };
        public static readonly TriggerVariable is_in_league_war = new TriggerVariable()
        {
            Name = "is_in_league_war",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is in a religious league war."
        };

        public static readonly TriggerVariable is_in_trade_league = new TriggerVariable()
        {
            Name = "is_in_trade_league",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is a member of a trade league."
        };

        public static readonly TriggerVariable is_in_trade_league_with = new TriggerVariable()
        {
            Name = "is_in_trade_league_with",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is a member of the same trade league as country X."
        };

        public static readonly TriggerVariable is_island = new TriggerVariable()
        {
            Name = "is_island",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province is an Island#is_island|island, i.e. has no neighboring land province and no reachable province via a strait."
        };

        public static readonly TriggerVariable is_league_enemy = new TriggerVariable()
        {
            Name = "is_league_enemy",
            ExpectedValue = Value.Scope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if country X is league enemy of the country('s league)."
        };

        public static readonly TriggerVariable is_lacking_institutions = new TriggerVariable()
        {
            Name = "is_lacking_institutions",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is lacking any institution."
        };

        public static readonly TriggerVariable is_league_friend = new TriggerVariable()
        {
            Name = "is_league_friend",
            ExpectedValue = Value.Scope,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country is in the same religious league as the specified country."
        };

        public static readonly TriggerVariable is_league_leader = new TriggerVariable()
        {
            Name = "is_league_leader",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country leads a religious league."
        };

        public static readonly TriggerVariable is_lesser_in_union = new TriggerVariable()
        {
            Name = "is_lesser_in_union",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is the lesser partner in a personal union."
        };

        public static readonly TriggerVariable is_looted = new TriggerVariable()
        {
            Name = "is_looted",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province is looted."
        };

        public static readonly TriggerVariable is_monarch_leader = new TriggerVariable()
        {
            Name = "is_monarch_leader",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the rulerof the country is a general.<br>Note: Proper localisation for the negation only with <code>NOT = …</code>."
        };

        public static readonly TriggerVariable is_month = new TriggerVariable()
        {
            Name = "is_month",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true if the current month is at least X (zero based)."
        };

        public static readonly TriggerVariable is_march = new TriggerVariable()
        {
            Name = "is_march",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is a march."
        };

        public static readonly TriggerVariable is_neighbor_of = new TriggerVariable()
        {
            Name = "is_neighbor_of",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is neighbor of X."
        };

        public static readonly TriggerVariable is_node_in_trade_company_region = new TriggerVariable()
        {
            Name = "is_node_in_trade_company_region",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true, if the province's trade node is in a trade company region."
        };

        public static readonly TriggerVariable is_nomad = new TriggerVariable()
        {
            Name = "is_nomad",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if country's government is nomadic.<br>(Note: Only the Steppe Nomads government is flagged as nomadic.)"
        };

        public static readonly TriggerVariable is_orangists_in_power = new TriggerVariable()
        {
            Name = "is_orangists_in_power",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if in the country the orangists are in power."
        };

        public static readonly TriggerVariable is_origin_of_consort = new TriggerVariable()
        {
            Name = "is_origin_of_consort",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the specified country is the origin country of the scoped country’s consort."
        };

        public static readonly TriggerVariable is_overseas = new TriggerVariable()
        {
            Name = "is_overseas",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province is overseas"
        };

        public static readonly TriggerVariable is_overseas_subject = new TriggerVariable()
        {
            Name = "is_overseas_subject",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the subject is overseas."
        };

        public static readonly TriggerVariable is_owned_by_trade_company = new TriggerVariable()
        {
            Name = "is_owned_by_trade_company",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province belongs to a trade company."
        };

        public static readonly TriggerVariable is_papal_controller = new TriggerVariable()
        {
            Name = "is_papal_controller",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is the papal controller."
        };

        public static readonly TriggerVariable is_part_of_hre = new TriggerVariable()
        {
            Name = "is_part_of_hre",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.CountryORProvince,
            Description = "Returns true if the country/province is part of the HRE."
        };

        public static readonly TriggerVariable is_permanent_claim = new TriggerVariable()
        {
            Name = "is_permanent_claim",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province is a permanent claim of X."
        };

        public static readonly TriggerVariable is_playing_custom_nation = new TriggerVariable()
        {
            Name = "is_playing_custom_nation",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is a player-designed custom nation."
        };

        public static readonly TriggerVariable is_possible_march = new TriggerVariable()
        {
            Name = "is_possible_march",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if vassal X is a possible march of the country."
        };

        public static readonly TriggerVariable is_possible_vassal = new TriggerVariable()
        {
            Name = "is_possible_vassal",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if country X is releasable as vassal of the country."
        };

        public static readonly TriggerVariable is_previous_papal_controller = new TriggerVariable()
        {
            Name = "is_previous_papal_controller",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is the previous papal controller."
        };

        public static readonly TriggerVariable is_prosperous = new TriggerVariable()
        {
            Name = "is_prosperous",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province is prosperous. NOTE: Does not have a tooltip."
        };

        public static readonly TriggerVariable is_protectorate = new TriggerVariable()
        {
            Name = "is_protectorate",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is a protectorate"
        };

        public static readonly TriggerVariable is_random_new_world = new TriggerVariable()
        {
            Name = "is_random_new_world",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true if playing with a random New World."
        };

        public static readonly TriggerVariable is_reformation_center = new TriggerVariable()
        {
            Name = "is_reformation_center",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province is a reformation center."
        };

        public static readonly TriggerVariable is_religion_grant_colonial_claim = new TriggerVariable()
        {
            Name = "is_religion_grant_colonial_claim",
            ExpectedValue = Value.BooleanORTagORScope,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province has been granted to any country, to no country or to a specific country."
        };

        public static readonly TriggerVariable is_religion_enabled = new TriggerVariable()
        {
            Name = "is_religion_enabled",
            ExpectedValue = Value.ReligionIdentifier,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true if the specified religion is enabled."
        };

        public static readonly TriggerVariable is_religion_reformed = new TriggerVariable()
        {
            Name = "is_religion_reformed",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has Pagan denominations#Mesoamerican_and_South_American_religions|reformed their religion."
        };

        public static readonly TriggerVariable is_renting_condottieri_to = new TriggerVariable()
        {
            Name = "is_renting_condottieri_to",
            ExpectedValue = Value.Tag,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is renting condottieri to the specified country."
        };

        public static readonly TriggerVariable is_revolution_target = new TriggerVariable()
        {
            Name = "is_revolution_target",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is the revolution target."
        };

        public static readonly TriggerVariable is_rival = new TriggerVariable()
        {
            Name = "is_rival",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if country X is a rival of the country."
        };

        public static readonly TriggerVariable is_ruler_commanding_unit = new TriggerVariable()
        {
            Name = "is_ruler_commanding_unit",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Unit,
            Description = "Returns true if the unit is commanded by the owner's ruler, heir or consort."
        };

        public static readonly TriggerVariable is_sea = new TriggerVariable()
        {
            Name = "is_sea",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province is sea.<br>Mostly used for trade nodes."
        };

        public static readonly TriggerVariable is_state = new TriggerVariable()
        {
            Name = "is_state",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province is in a state."
        };

        public static readonly TriggerVariable is_state_core = new TriggerVariable()
        {
            Name = "is_state_core",
            ExpectedValue = Value.ProvinceIDORTagORScope,
            AllowedScopes = Scope.CountryORProvince,
            Description = "Returns true if the country has a state core on the province with the ID X."
        };

        public static readonly TriggerVariable is_statists_in_power = new TriggerVariable()
        {
            Name = "is_statists_in_power",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if in the country the statists are in power."
        };

        public static readonly TriggerVariable is_strongest_trade_power = new TriggerVariable()
        {
            Name = "is_strongest_trade_power",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the specified country has the most trade power in the area."
        };

        public static readonly TriggerVariable is_subject = new TriggerVariable()
        {
            Name = "is_subject",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is a subject."
        };

        public static readonly TriggerVariable is_subject_of = new TriggerVariable()
        {
            Name = "is_subject_of",
            ExpectedValue = Value.Tag,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is a subject of X."
        };

        public static readonly TriggerVariable is_subject_of_type = new TriggerVariable()
        {
            Name = "is_subject_of_type",
            ExpectedValue = Value.SubjectIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is a subject of subject type."
        };


        public static readonly TriggerVariable is_supporting_independence_of = new TriggerVariable()
        {
            Name = "is_supporting_independence_of",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is supporting the Diplomacy#Support_independence|independence of the specified country."
        };

        public static readonly TriggerVariable is_territorial_core = new TriggerVariable()
        {
            Name = "is_territorial_core",
            ExpectedValue = Value.ProvinceIDORTagORScope,
            AllowedScopes = Scope.CountryORProvince,
            Description = "Returns true if the country has a territorial core on the province with the ID X."
        };

        public static readonly TriggerVariable is_territory = new TriggerVariable()
        {
            Name = "is_territory",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province is in a territory."
        };

        public static readonly TriggerVariable is_threat = new TriggerVariable()
        {
            Name = "is_threat",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if country X is threatened by the country."
        };

        public static readonly TriggerVariable is_trade_league_leader = new TriggerVariable()
        {
            Name = "is_trade_league_leader",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is the leader of a trade league."
        };

        public static readonly TriggerVariable is_tribal = new TriggerVariable()
        {
            Name = "is_tribal",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a tribal government. Essentially the same as \"government = tribal\""
        };

        public static readonly TriggerVariable is_vassal = new TriggerVariable()
        {
            Name = "is_vassal",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is a vassal."
        };

        public static readonly TriggerVariable is_wasteland = new TriggerVariable()
        {
            Name = "is_wasteland",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province is wasteland."
        };

        public static readonly TriggerVariable is_year = new TriggerVariable()
        {
            Name = "is_year",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true if the current year is at least X."
        };

        public static readonly TriggerVariable island = new TriggerVariable()
        {
            Name = "island",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province is an Island#island|island, i.e. has no neighboring land provinces."
        };

        public static readonly TriggerVariable isolationism = new TriggerVariable()
        {
            Name = "isolationism",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country has an isolationism level of at least X."
        };

        public static readonly TriggerVariable janissary_percentage = new TriggerVariable()
        {
            Name = "janissary_percentage",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has an army of at least X k janissaries."
        };

        public static readonly TriggerVariable junior_union_with = new TriggerVariable()
        {
            Name = "junior_union_with",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is the junior partner in a personal union under country X."
        };

        public static readonly TriggerVariable karma = new TriggerVariable()
        {
            Name = "karma",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a karma of at least X."
        };

        public static readonly TriggerVariable knows_country = new TriggerVariable()
        {
            Name = "knows_country",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has knowledge of country X."
        };

        public static readonly TriggerVariable land_forcelimit = new TriggerVariable()
        {
            Name = "land_forcelimit",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a land force limit of at least X."
        };
        public static readonly TriggerVariable land_maintenance = new TriggerVariable()
        {
            Name = "land_maintenance",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has set its land maintenance to X."
        };

        public static readonly TriggerVariable land_morale = new TriggerVariable()
        {
            Name = "land_morale",
            ExpectedValue = Value.FloatORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a morale of armies of at least X."
        };
        public static readonly TriggerVariable last_mission = new TriggerVariable()
        {
            Name = "last_mission",
            ExpectedValue = Value.MissionIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the last mission of the country was the specified mission."
        };

        public static readonly TriggerVariable legitimacy = new TriggerVariable()
        {
            Name = "legitimacy",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X legitimacy."
        };

        public static readonly TriggerVariable legitimacy_equivalent = new TriggerVariable()
        {
            Name = "legitimacy_equivalent",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country's legitimacy equivalent (legitimacy, republican tradition, devotion, horde unity, meritocracy etc.) is at least X."
        };

        public static readonly TriggerVariable legitimacy_or_horde_unity = new TriggerVariable()
        {
            Name = "legitimacy_or_horde_unity",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X legitimacy or horde unity."
        };

        public static readonly TriggerVariable liberty_desire = new TriggerVariable()
        {
            Name = "liberty_desire",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Return true if the subject has a liberty desire of at least X."
        };

        public static readonly TriggerVariable light_ship_fraction = new TriggerVariable()
        {
            Name = "light_ship_fraction",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the ratio of the light ship fraction to the navy size of the country is at least X."
        };

        public static readonly TriggerVariable light_ships_in_province = new TriggerVariable()
        {
            Name = "light_ships_in_province",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Province,
            Description = "Returns true if there are at least X light ships in the province."
        };

        public static readonly TriggerVariable likely_rebels = new TriggerVariable()
        {
            Name = "likely_rebels",
            ExpectedValue = Value.RebelIdentifier,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province has the specified rebel faction as likely rebels."
        };

        public static readonly TriggerVariable local_autonomy = new TriggerVariable()
        {
            Name = "local_autonomy",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province has a local autonomy of at least X."
        };

        public static readonly TriggerVariable local_autonomy_above_min = new TriggerVariable()
        {
            Name = "local_autonomy_above_min",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Province,
            Description = "Returns true, if the province's local autonomy is at least X above the province's minimum local autonomy."
        };

        public static readonly TriggerVariable luck = new TriggerVariable()
        {
            Name = "luck",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is a lucky nation. (AI controlled counties only.)"
        };

        public static readonly TriggerVariable march_of = new TriggerVariable()
        {
            Name = "march_of",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is a march under country X."
        };

        public static readonly TriggerVariable manpower = new TriggerVariable()
        {
            Name = "manpower",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X*1000 available manpower."
        };

        public static readonly TriggerVariable manpower_percentage = new TriggerVariable()
        {
            Name = "manpower_percentage",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a manpower level of at least X%."
        };

        public static readonly TriggerVariable marriage_with = new TriggerVariable()
        {
            Name = "marriage_with",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a royal marriage with X."
        };

        public static readonly TriggerVariable max_manpower = new TriggerVariable()
        {
            Name = "max_manpower",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X*1000 maximum manpower."
        };

        public static readonly TriggerVariable mercantilism = new TriggerVariable()
        {
            Name = "mercantilism",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country's mercantilism is at least X."
        };

        public static readonly TriggerVariable meritocracy = new TriggerVariable()
        {
            Name = "meritocracy",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country has a meritocracy value of at least X."
        };

        public static readonly TriggerVariable mil = new TriggerVariable()
        {
            Name = "mil",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a ruler with a military skill of at least X."
        };
        public static readonly TriggerVariable militarised_society = new TriggerVariable()
        {
            Name = "militarised_society",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a militarization of country of at least X."
        };

        public static readonly TriggerVariable mil_power = new TriggerVariable()
        {
            Name = "mil_power",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X military power."
        };
        public static readonly TriggerVariable mil_tech = new TriggerVariable()
        {
            Name = "mil_tech",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has an military technology of at least level X."
        };

        public static readonly TriggerVariable mission_completed = new TriggerVariable()
        {
            Name = "mission_completed",
            ExpectedValue = Value.MissionIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has completed mission X."
        };

        public static readonly TriggerVariable monthly_income = new TriggerVariable()
        {
            Name = "monthly_income",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a monthly income of at least X."
        };

        public static readonly TriggerVariable monthly_adm = new TriggerVariable()
        {
            Name = "monthly_adm",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country gains at least X adm power every month."
        };

        public static readonly TriggerVariable monthly_dip = new TriggerVariable()
        {
            Name = "monthly_dip",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country gains at least X dip power every month."
        };

        public static readonly TriggerVariable monthly_mil = new TriggerVariable()
        {
            Name = "monthly_mil",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country gains at least X mil power every month."
        };

        public static readonly TriggerVariable months_of_ruling = new TriggerVariable()
        {
            Name = "months_of_ruling",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a ruler that has ruled for at least X months."
        };

        public static readonly TriggerVariable months_since_defection = new TriggerVariable()
        {
            Name = "months_since_defection",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province defected within the last X months."
        };

        public static readonly TriggerVariable nationalism = new TriggerVariable()
        {
            Name = "nationalism",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Province,
            Description = "Returns true if province has at least X years of separatism remaining."
        };

        public static readonly TriggerVariable national_focus = new TriggerVariable()
        {
            Name = "national_focus",
            ExpectedValue = Value.NationalFocusIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if country has set the national focus to X."
        };

        public static readonly TriggerVariable nation_designer_points = new TriggerVariable()
        {
            Name = "nation_designer_points",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if at least X points were used when creating the custom nation."
        };

        public static readonly TriggerVariable native_ferocity = new TriggerVariable()
        {
            Name = "native_ferocity",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Province,
            Description = "Returns true if ferocity of natives in the province is at least X."
        };

        public static readonly TriggerVariable native_hostileness = new TriggerVariable()
        {
            Name = "native_hostileness",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Province,
            Description = "Returns true if hostileness of natives in the province is at least X."
        };

        public static readonly TriggerVariable native_policy = new TriggerVariable()
        {
            Name = "native_policy",
            ExpectedValue = Value.NativePolicyIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if country has the specified native policy."
        };

        public static readonly TriggerVariable native_size = new TriggerVariable()
        {
            Name = "native_size",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Province,
            Description = "Returns true if size of natives in the province is at least X."
        };

        public static readonly TriggerVariable naval_forcelimit = new TriggerVariable()
        {
            Name = "naval_forcelimit",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a naval force limit of at least X."
        };

        public static readonly TriggerVariable naval_maintenance = new TriggerVariable()
        {
            Name = "naval_maintenance",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country's naval maintenance slider is at least X fraction of maximum."
        };

        public static readonly TriggerVariable naval_morale = new TriggerVariable()
        {
            Name = "naval_morale",
            ExpectedValue = Value.FloatORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a morale of navies of at least X."
        };

        public static readonly TriggerVariable navy_size = new TriggerVariable()
        {
            Name = "navy_size",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the current scope has a navy of at least X ships."
        };

        public static readonly TriggerVariable navy_size_percentage = new TriggerVariable()
        {
            Name = "navy_size_percentage",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the ratio of the total navy size of the country to its naval force limit is at least X."
        };

        public static readonly TriggerVariable navy_tradition = new TriggerVariable()
        {
            Name = "navy_tradition",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a navy tradition of at least X."
        };
        public static readonly TriggerVariable normal_or_historical_nations = new TriggerVariable()
        {
            Name = "normal_or_historical_nations",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true if game is set to use Options#Nations|normal or historical nations."
        };

        public static readonly TriggerVariable normal_province_values = new TriggerVariable()
        {
            Name = "normal_province_values",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true if game is set to use Options#Province_Tax_and_Manpower|normal province values."
        };

        public static readonly TriggerVariable num_accepted_cultures = new TriggerVariable()
        {
            Name = "num_accepted_cultures",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country has at least X accepted cultures."
        };

        public static readonly TriggerVariable num_free_building_slots = new TriggerVariable()
        {
            Name = "num_free_building_slots",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province has at least X building slots remaining."
        };

        public static readonly TriggerVariable num_of_active_blessings = new TriggerVariable()
        {
            Name = "num_of_active_blessings",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has enabled at least X blessings."
        };

        public static readonly TriggerVariable num_of_admirals = new TriggerVariable()
        {
            Name = "num_of_admirals",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X admirals."
        };

        public static readonly TriggerVariable num_of_admirals_with_traits = new TriggerVariable()
        {
            Name = "num_of_admirals_with_traits",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X admirals with traits."
        };

        public static readonly TriggerVariable num_of_allies = new TriggerVariable()
        {
            Name = "num_of_allies",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Return true if the country has at least X allies."
        };

        public static readonly TriggerVariable num_of_artillery = new TriggerVariable()
        {
            Name = "num_of_artillery",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X artillery regiments."
        };

        public static readonly TriggerVariable num_of_aspects = new TriggerVariable()
        {
            Name = "num_of_aspects",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X church aspects."
        };

        public static readonly TriggerVariable num_of_banners = new TriggerVariable()
        {
            Name = "num_of_banners",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country has at least X banner units."
        };
        public static readonly TriggerVariable num_of_buildings_in_province = new TriggerVariable()
        {
            Name = "num_of_buildings_in_province",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Province,
            Description = "Returns true, if the province has at least X buildings."
        };
        public static readonly TriggerVariable num_of_captured_ships_with_boarding_doctrine = new TriggerVariable()
        {
            Name = "num_of_captured_ships_with_boarding_doctrine",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has captured at least X ships while having the ‘Ship Boarding’ naval doctrine."
        };

        public static readonly TriggerVariable num_of_centers_of_trade = new TriggerVariable()
        {
            Name = "num_of_centers_of_trade",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X centers of trade."
        };

        public static readonly TriggerVariable num_of_cardinals = new TriggerVariable()
        {
            Name = "num_of_cardinals",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X cardinals in the Holy See."
        };

        public static readonly TriggerVariable num_of_cavalry = new TriggerVariable()
        {
            Name = "num_of_cavalry",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X cavalry regiments."
        };

        public static readonly TriggerVariable num_of_cawa = new TriggerVariable()
        {
            Name = "num_of_cawa",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "\tReturns true if the country has at least X cawa regiments."
        };

        public static readonly TriggerVariable num_of_cities = new TriggerVariable()
        {
            Name = "num_of_cities",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country owns at least X cities."
        };

        public static readonly TriggerVariable num_of_coalition_members = new TriggerVariable()
        {
            Name = "num_of_coalition_members",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is in a coalition of at least X members."
        };

        public static readonly TriggerVariable num_of_colonies = new TriggerVariable()
        {
            Name = "num_of_colonies",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X unfinished colonies."
        };

        public static readonly TriggerVariable num_of_colonists = new TriggerVariable()
        {
            Name = "num_of_colonists",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X colonists."
        };

        public static readonly TriggerVariable num_of_conquistadors = new TriggerVariable()
        {
            Name = "num_of_conquistadors",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X conquistadors."
        };

        public static readonly TriggerVariable num_of_consorts = new TriggerVariable()
        {
            Name = "num_of_consorts",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the ruler of the country had at least X separate consorts."
        };

        public static readonly TriggerVariable num_of_continents = new TriggerVariable()
        {
            Name = "num_of_continents",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country owns provinces on at least X continents. Only provinces owned by the country itself are taken into account, not provinces owned by subjects."
        };

        public static readonly TriggerVariable num_of_cossacks = new TriggerVariable()
        {
            Name = "num_of_cossacks",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country has at least X cossack units."
        };

        public static readonly TriggerVariable num_of_custom_nations = new TriggerVariable()
        {
            Name = "num_of_custom_nations",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if there are at least X custom nations in the game."
        };

        public static readonly TriggerVariable num_of_diplomatic_relations = new TriggerVariable()
        {
            Name = "num_of_diplomatic_relations",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X diplomatic relations."
        };

        public static readonly TriggerVariable num_of_diplomats = new TriggerVariable()
        {
            Name = "num_of_diplomats",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X diplomats."
        };

        public static readonly TriggerVariable num_of_electors = new TriggerVariable()
        {
            Name = "num_of_electors",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true if there are at least X electors of the HRE."
        };

        public static readonly TriggerVariable num_of_explorers = new TriggerVariable()
        {
            Name = "num_of_explorers",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X explorers."
        };

        public static readonly TriggerVariable num_of_foreign_hre_provinces = new TriggerVariable()
        {
            Name = "num_of_foreign_hre_provinces",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true if there are at least X provinces owned by non-member states or subjects of non-member states."
        };

        public static readonly TriggerVariable num_of_free_diplomatic_relations = new TriggerVariable()
        {
            Name = "num_of_free_diplomatic_relations",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X free diplomatic relations slots."
        };

        public static readonly TriggerVariable num_of_galley = new TriggerVariable()
        {
            Name = "num_of_galley",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X galleys."
        };

        public static readonly TriggerVariable num_of_generals = new TriggerVariable()
        {
            Name = "num_of_generals",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X generals."
        };

        public static readonly TriggerVariable num_of_generals_with_traits = new TriggerVariable()
        {
            Name = "num_of_generals_with_traits",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X generals with traits."
        };

        public static readonly TriggerVariable num_of_harmonized = new TriggerVariable()
        {
            Name = "num_of_harmonized",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country has harmonized with at least X religions or religion groups."
        };

        public static readonly TriggerVariable num_of_heavy_ship = new TriggerVariable()
        {
            Name = "num_of_heavy_ship",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X heavy ships."
        };

        public static readonly TriggerVariable num_of_infantry = new TriggerVariable()
        {
            Name = "num_of_infantry",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X infantry regiments."
        };

        public static readonly TriggerVariable num_of_light_ship = new TriggerVariable()
        {
            Name = "num_of_light_ship",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X light ships."
        };

        public static readonly TriggerVariable num_of_loans = new TriggerVariable()
        {
            Name = "num_of_loans",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X loans."
        };
        public static readonly TriggerVariable num_of_marches = new TriggerVariable()
        {
            Name = "num_of_marches",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X marches."
        };
        public static readonly TriggerVariable num_of_marines = new TriggerVariable()
        {
            Name = "num_of_marines",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has enabled at least X marines."
        };

        public static readonly TriggerVariable num_of_mercenaries = new TriggerVariable()
        {
            Name = "num_of_mercenaries",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X mercenaries."
        };

        public static readonly TriggerVariable num_of_merchants = new TriggerVariable()
        {
            Name = "num_of_merchants",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X merchants."
        };

        public static readonly TriggerVariable num_of_missionaries = new TriggerVariable()
        {
            Name = "num_of_missionaries",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X missionaries."
        };

        public static readonly TriggerVariable num_of_owned_and_controlled_institutions = new TriggerVariable()
        {
            Name = "num_of_owned_and_controlled_institutions",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country owns and controls at least X provinces that are institution origins."
        };

        public static readonly TriggerVariable num_of_ports = new TriggerVariable()
        {
            Name = "num_of_ports",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if country owns at least X home ports (in lands contiguously connected to the capital)."
        };

        public static readonly TriggerVariable num_of_ports_blockading = new TriggerVariable()
        {
            Name = "num_of_ports_blockading",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country blockades at least X ports."
        };

        public static readonly TriggerVariable num_of_powerful_estates = new TriggerVariable()
        {
            Name = "num_of_powerful_estates",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X estates with at least 70 influence."
        };

        public static readonly TriggerVariable num_of_protectorates = new TriggerVariable()
        {
            Name = "num_of_protectorates",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X protectorates."
        };
        public static readonly TriggerVariable num_of_provinces_in_states = new TriggerVariable()
        {
            Name = "num_of_provinces_in_states",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X provinces is states."
        };

        public static readonly TriggerVariable num_of_provinces_in_territories = new TriggerVariable()
        {
            Name = "num_of_provinces_in_territories",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X provinces is territories."
        };


        public static readonly TriggerVariable num_of_rajput = new TriggerVariable()
        {
            Name = "num_of_rajput",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X rajput regiments."
        };

        public static readonly TriggerVariable num_of_rebel_armies = new TriggerVariable()
        {
            Name = "num_of_rebel_armies",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the number of rebel armies in the country is at least X."
        };

        public static readonly TriggerVariable num_of_rebel_controlled_provinces = new TriggerVariable()
        {
            Name = "num_of_rebel_controlled_provinces",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the number of rebel controlled provinces in the country is at least X."
        };

        public static readonly TriggerVariable num_of_revolts = new TriggerVariable()
        {
            Name = "num_of_revolts",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the number of revolts in the country is at least X.<br>''The same as ‘num_of_rebel_controlled_provinces’.''"
        };

        public static readonly TriggerVariable num_of_royal_marriages = new TriggerVariable()
        {
            Name = "num_of_royal_marriages",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X royal marriages."
        };

        public static readonly TriggerVariable num_of_ruler_traits = new TriggerVariable()
        {
            Name = "num_of_ruler_traits",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the ruler has at least X personality traits."
        };

        public static readonly TriggerVariable num_of_states = new TriggerVariable()
        {
            Name = "num_of_states",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X states."
        };

        public static readonly TriggerVariable num_of_streltsy = new TriggerVariable()
        {
            Name = "num_of_streltsy",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country has at least X streltsy units."
        };

        public static readonly TriggerVariable num_of_strong_trade_companies = new TriggerVariable()
        {
            Name = "num_of_strong_trade_companies",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X strong trade companies."
        };

        public static readonly TriggerVariable num_of_subjects = new TriggerVariable()
        {
            Name = "num_of_subjects",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is the overlord of at least X subject countries of any type."
        };

        public static readonly TriggerVariable num_of_territories = new TriggerVariable()
        {
            Name = "num_of_territories",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country has at least X territories (areas that aren't states)."
        };

        public static readonly TriggerVariable num_of_times_improved = new TriggerVariable()
        {
            Name = "num_of_times_improved",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the development of the province was improved at least X times."
        };

        public static readonly TriggerVariable num_of_times_improved_by_owner = new TriggerVariable()
        {
            Name = "num_of_times_improved_by_owner",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the development of the province was improved at least X times by the current owner."
        };

        public static readonly TriggerVariable num_of_times_used_pillage_capital = new TriggerVariable()
        {
            Name = "num_of_times_used_pillage_capital",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has used the Pillage Capital peace treaty at least X times."
        };

        public static readonly TriggerVariable num_of_times_used_transfer_development = new TriggerVariable()
        {
            Name = "num_of_times_used_transfer_development",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has used the Concentrate Development action at least X times."
        };

        public static readonly TriggerVariable num_of_total_ports = new TriggerVariable()
        {
            Name = "num_of_total_ports",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country owns at least X total ports (anywhere in the world)."
        };

        public static readonly TriggerVariable num_of_trade_companies = new TriggerVariable()
        {
            Name = "num_of_trade_companies",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X trade companies."
        };

        public static readonly TriggerVariable num_of_trade_embargos = new TriggerVariable()
        {
            Name = "num_of_trade_embargos",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X trade embargos."
        };

        public static readonly TriggerVariable num_of_trading_bonuses = new TriggerVariable()
        {
            Name = "num_of_trading_bonuses",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country hasat least X ‘trading in’ bonuses."
        };

        public static readonly TriggerVariable num_of_transport = new TriggerVariable()
        {
            Name = "num_of_transport",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X transports."
        };

        public static readonly TriggerVariable num_of_trusted_allies = new TriggerVariable()
        {
            Name = "num_of_trusted_allies",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X allies with 100 trust."
        };

        public static readonly TriggerVariable num_of_unions = new TriggerVariable()
        {
            Name = "num_of_unions",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X personal unions."
        };

        public static readonly TriggerVariable num_of_unlocked_cults = new TriggerVariable()
        {
            Name = "num_of_unlocked_cults",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has unlocked at least X cults."
        };

        public static readonly TriggerVariable num_of_war_reparations = new TriggerVariable()
        {
            Name = "num_of_war_reparations",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country receives war reparations from at least X countries."
        };

        public static readonly TriggerVariable num_ships_privateering = new TriggerVariable()
        {
            Name = "num_ships_privateering",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if country has at least X ships privateering."
        };

        public static readonly TriggerVariable offensive_war_with = new TriggerVariable()
        {
            Name = "offensive_war_with",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is in an offensive war with country X."
        };

        public static readonly TriggerVariable overextension_percentage = new TriggerVariable()
        {
            Name = "overextension_percentage",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Country,
            Description = "Returns true if country has at least X% overextension."
        };

        public static readonly TriggerVariable overlord_of = new TriggerVariable()
        {
            Name = "overlord_of",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is the overlord of X."
        };

        public static readonly TriggerVariable overseas_provinces_percentage = new TriggerVariable()
        {
            Name = "overseas_provinces_percentage",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has X percentage of overseas provinces."
        };

        public static readonly TriggerVariable owned_by = new TriggerVariable()
        {
            Name = "owned_by",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province is owned by the country X."
        };

        public static readonly TriggerVariable owns = new TriggerVariable()
        {
            Name = "owns",
            ExpectedValue = Value.ProvinceID,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country owns the specified province."
        };

        public static readonly TriggerVariable owns_core_province = new TriggerVariable()
        {
            Name = "owns_core_province",
            ExpectedValue = Value.ProvinceID,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country owns and has a core on the specified province."
        };

        public static readonly TriggerVariable owns_or_non_sovereign_subject_of = new TriggerVariable()
        {
            Name = "owns_or_non_sovereign_subject_of",
            ExpectedValue = Value.ProvinceID,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country or a subject that is not categorized as \"sovereign\" (e.g. tributary states are excluded) owns the specified province."
        };

        public static readonly TriggerVariable owns_or_subject_of = new TriggerVariable()
        {
            Name = "owns_or_subject_of",
            ExpectedValue = Value.ProvinceIDORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country or a subject owns the specified province."
        };

        public static readonly TriggerVariable papacy_active = new TriggerVariable()
        {
            Name = "papacy_active",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true if the papacy is active."
        };

        public static readonly TriggerVariable papal_influence = new TriggerVariable()
        {
            Name = "papal_influence",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country's papal influence is at least X."
        };
        public static readonly TriggerVariable patriarch_authority = new TriggerVariable()
        {
            Name = "patriarch_authority",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country's patriarch authority is at least X."
        };

        public static readonly TriggerVariable percentage_backing_issue = new TriggerVariable()
        {
            Name = "percentage_backing_issue",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Country,
            Description = "Returns true if at least the Xth part of the seats in the parliament is backing for the current issue."
        };

        public static readonly TriggerVariable personality = new TriggerVariable()
        {
            Name = "personality",
            ExpectedValue = Value.AIPersonalityIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a ruler which personality is X. Is limited to ai_personalities listed in base game file 'common/ai_personalities/00_ai_personalities.txt'. For ruler personality, use ruler_has_personality"
        };

        public static readonly TriggerVariable piety = new TriggerVariable()
        {
            Name = "piety",
            ExpectedValue = Value.FloatORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country's piety is at least X."
        };

        public static readonly TriggerVariable preferred_emperor = new TriggerVariable()
        {
            Name = "preferred_emperor",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if an elector has the country X is the preferred emperor."
        };

        public static readonly TriggerVariable prestige = new TriggerVariable()
        {
            Name = "prestige",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Return true if the country has a prestige of at least X."
        };

        public static readonly TriggerVariable previous_owner = new TriggerVariable()
        {
            Name = "previous_owner",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the previous owner of the province was X."
        };

        public static readonly TriggerVariable power_projection = new TriggerVariable()
        {
            Name = "power_projection",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if country has a Power_projection of at least X. Appeared in 1.30"
        };

        public static readonly TriggerVariable primary_culture = new TriggerVariable()
        {
            Name = "primary_culture",
            ExpectedValue = Value.CultureIdentifierORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country's primary culture is X."
        };

        public static readonly TriggerVariable primitives = new TriggerVariable()
        {
            Name = "primitives",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is primitive."
        };

        public static readonly TriggerVariable production_efficiency = new TriggerVariable()
        {
            Name = "production_efficiency",
            ExpectedValue = Value.FloatORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a production efficiency of at least X."
        };

        public static readonly TriggerVariable production_income_percentage = new TriggerVariable()
        {
            Name = "production_income_percentage",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the ratio of production income to total income is at least X."
        };

        public static readonly TriggerVariable province_id = new TriggerVariable()
        {
            Name = "province_id",
            ExpectedValue = Value.IntegerORScope,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province has the ID X."
        };

        public static readonly TriggerVariable province_is_on_an_island = new TriggerVariable()
        {
            Name = "province_is_on_an_island",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province is Island#province_is_on_an_island|on an island."
        };

        public static readonly TriggerVariable province_getting_expelled_minority = new TriggerVariable()
        {
            Name = "province_getting_expelled_minority",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province is getting minorities."
        };

        public static readonly TriggerVariable province_group = new TriggerVariable()
        {
            Name = "province_group",
            ExpectedValue = Value.ProvinceGroupIdentifier,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province belongs to the specified province group."
        };

        public static readonly TriggerVariable province_size = new TriggerVariable()
        {
            Name = "province_size",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province, in '''provinces.bmp''', contains X pixels."
        };

        public static readonly TriggerVariable province_trade_power = new TriggerVariable()
        {
            Name = "province_trade_power",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Province,
            Description = "Returns true if trade power generated by the province is at least X."
        };

        public static readonly TriggerVariable provinces_on_capital_continent_of = new TriggerVariable()
        {
            Name = "provinces_on_capital_continent_of",
            ExpectedValue = Value.Scope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a province on the continent with the capital of the specified country."
        };

        public static readonly TriggerVariable pure_unrest = new TriggerVariable()
        {
            Name = "pure_unrest",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province has a base unrest of at least X."
        };

        public static readonly TriggerVariable range = new TriggerVariable()
        {
            Name = "range",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province is within the range of the specified country."
        };

        public static readonly TriggerVariable real_day_of_year = new TriggerVariable()
        {
            Name = "real_day_of_year",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true, if today is X. Refers to the actual real day (probably takes system time)."
        };

        public static readonly TriggerVariable real_month_of_year = new TriggerVariable()
        {
            Name = "real_month_of_year",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true if the month of the year in reality is at least X (January &#8793; 0)"
        };

        public static readonly TriggerVariable reform_desire = new TriggerVariable()
        {
            Name = "reform_desire",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true if the reform desire is at least X%."
        };

        public static readonly TriggerVariable receives_military_access_from = new TriggerVariable()
        {
            Name = "receives_military_access_from",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the scoped country receives military access from the specified country."
        };

        public static readonly TriggerVariable receives_fleet_basing_rights_from = new TriggerVariable()
        {
            Name = "receives_fleet_basing_rights_from",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the scoped country receives fleet basing rights from the specified country."
        };

        public static readonly TriggerVariable reform_level = new TriggerVariable()
        {
            Name = "receives_fleet_basing_rights_from",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Retrurn true if the country has reached at least X levels of government reforms."
        };


        public static readonly TriggerVariable region = new TriggerVariable()
        {
            Name = "region",
            ExpectedValue = Value.RegionIdentifier,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province is part of the region X."
        };

        public static readonly TriggerVariable religion = new TriggerVariable()
        {
            Name = "religion",
            ExpectedValue = Value.ReligionIdentifierORScope,
            AllowedScopes = Scope.CountryORProvince,
            Description = "Returns true if the country/province has religion X."
        };

        public static readonly TriggerVariable religion_group = new TriggerVariable()
        {
            Name = "religion_group",
            ExpectedValue = Value.ReligionGroupORScope,
            AllowedScopes = Scope.CountryORProvince,
            Description = "Returns true if the country/province has a religion of the specified religious group."
        };

        public static readonly TriggerVariable religious_unity = new TriggerVariable()
        {
            Name = "religious_unity",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country's religious unity is at least X."
        };

        public static readonly TriggerVariable republican_tradition = new TriggerVariable()
        {
            Name = "republican_tradition",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if country's republican tradition is at least X."
        };

        public static readonly TriggerVariable revanchism = new TriggerVariable()
        {
            Name = "revanchism",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if country's revanchism is at least X."
        };

        public static readonly TriggerVariable revolt_percentage = new TriggerVariable()
        {
            Name = "revolt_percentage",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Country,
            Description = "Returns true if at least the Xth part of the provinces of the country have revolts."
        };

        public static readonly TriggerVariable revolution_target_exists = new TriggerVariable()
        {
            Name = "revolution_target_exists",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true if there is a revolutionary target in the world."
        };

        public static readonly TriggerVariable ruler_age = new TriggerVariable()
        {
            Name = "ruler_age",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a ruler that is at least X years old."
        };

        public static readonly TriggerVariable ruler_consort_marriage_length = new TriggerVariable()
        {
            Name = "ruler_consort_marriage_length",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "''Needs description''"
        };

        public static readonly TriggerVariable ruler_culture = new TriggerVariable()
        {
            Name = "ruler_culture",
            ExpectedValue = Value.CultureIdentifierORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country's ruler has the specified culture. Can utilise Variables|Event Scope Values."
        };

        public static readonly TriggerVariable ruler_has_personality = new TriggerVariable()
        {
            Name = "ruler_has_personality",
            ExpectedValue = Value.PersonalityIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country’s ruler has the specified personality."
        };

        public static readonly TriggerVariable ruler_is_foreigner = new TriggerVariable()
        {
            Name = "ruler_is_foreigner",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has foreign ruler."
        };

        public static readonly TriggerVariable ruler_religion = new TriggerVariable()
        {
            Name = "ruler_religion",
            ExpectedValue = Value.ReligionIdentifierORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country's ruler has the specified religion. Can utilise Variables|Event Scope Values."
        };

        public static readonly TriggerVariable sailors = new TriggerVariable()
        {
            Name = "sailors",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X sailors."
        };

        public static readonly TriggerVariable sailors_percentage = new TriggerVariable()
        {
            Name = "sailors_percentage",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a sailor level of at least X%."
        };

        public static readonly TriggerVariable max_sailors = new TriggerVariable()
        {
            Name = "max_sailors",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has at least X maximum sailors."
        };

        public static readonly TriggerVariable same_continent = new TriggerVariable()
        {
            Name = "same_continent",
            ExpectedValue = Value.IntegerORScope,
            AllowedScopes = Scope.CountryORProvince,
            Description = "Returns true, if specified province is on the same continent as the province. If used in country scope, capitals are checked."
        };

        public static readonly TriggerVariable secondary_religion = new TriggerVariable()
        {
            Name = "secondary_religion",
            ExpectedValue = Value.ReligionIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true the secondary religion of the country is X."
        };

        public static readonly TriggerVariable senior_union_with = new TriggerVariable()
        {
            Name = "senior_union_with",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is the senior partner in a personal union over country X."
        };

        public static readonly TriggerVariable sieged_by = new TriggerVariable()
        {
            Name = "sieged_by",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province is being besieged by country X."
        };

        public static readonly TriggerVariable splendor = new TriggerVariable()
        {
            Name = "splendor",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country has at least X splendor."
        };

        public static readonly TriggerVariable stability = new TriggerVariable()
        {
            Name = "stability",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a stability of at least X."
        };

        public static readonly TriggerVariable start_date = new TriggerVariable()
        {
            Name = "start_date",
            ExpectedValue = Value.Date,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true if the start date of the campaign is X."
        };

        public static readonly TriggerVariable started_in = new TriggerVariable()
        {
            Name = "started_in",
            ExpectedValue = Value.Date,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true if the start date of the campaign is X or after."
        };

        public static readonly TriggerVariable statists_vs_orangists = new TriggerVariable()
        {
            Name = "statists_vs_orangists",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Country,
            Description = "Returns true if Statists vs Orangists is at least X."
        };

        public static readonly TriggerVariable subsidised_percent_amount = new TriggerVariable()
        {
            Name = "subsidised_percent_amount",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country receives subsidies of at least X% of its monthly income."
        };

        public static readonly TriggerVariable succession_claim = new TriggerVariable()
        {
            Name = "succession_claim",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has claim throne|claimed the throne of the country X."
        };

        public static readonly TriggerVariable superregion = new TriggerVariable()
        {
            Name = "superregion",
            ExpectedValue = Value.SuperregionIdentifier,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province belongs to the superregion X."
        };

        public static readonly TriggerVariable tag = new TriggerVariable()
        {
            Name = "tag",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is the specified country."
        };

        public static readonly TriggerVariable tariff_value = new TriggerVariable()
        {
            Name = "tariff_value",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the colonial nation pays at least X% tariffs."
        };

        public static readonly TriggerVariable tax_income_percentage = new TriggerVariable()
        {
            Name = "tax_income_percentage",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the ratio of tax income to total income is at least X."
        };

        public static readonly TriggerVariable tech_difference = new TriggerVariable()
        {
            Name = "tech_difference",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the scoped country is at least X technologies ahead (compared to the country)."
        };

        public static readonly TriggerVariable technology_group = new TriggerVariable()
        {
            Name = "technology_group",
            ExpectedValue = Value.TechnologyGroupIdentifierORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has the technology group X."
        };

        public static readonly TriggerVariable tolerance_to_this = new TriggerVariable()
        {
            Name = "tolerance_to_this",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.CountryORProvince,
            Description = "Returns true if the country has a tolerance of at least X towards the religion of the country or scoped province."
        };

        public static readonly TriggerVariable total_base_tax = new TriggerVariable()
        {
            Name = "total_base_tax",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the base tax of the country's provinces totals at least X."
        };

        public static readonly TriggerVariable total_development = new TriggerVariable()
        {
            Name = "total_development",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a total development of at least X."
        };

        public static readonly TriggerVariable total_number_of_cardinals = new TriggerVariable()
        {
            Name = "total_number_of_cardinals",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Anywhere,
            Description = "Returns true if the total number of cardinals is at least X."
        };

        public static readonly TriggerVariable trade_league_embargoed_by = new TriggerVariable()
        {
            Name = "trade_league_embargoed_by",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "''Description needed''"
        };

        public static readonly TriggerVariable total_own_and_non_tributary_subject_development = new TriggerVariable()
        {
            Name = "total_own_and_non_tributary_subject_development",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country and its non-tributary subjects have more Total development than the specified country and its non-tributary subjects."
        };

        public static readonly TriggerVariable transports_in_province = new TriggerVariable()
        {
            Name = "transports_in_province",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Province,
            Description = "Returns true if there are at least X transports in the province."
        };

        public static readonly TriggerVariable trade_company_region = new TriggerVariable()
        {
            Name = "trade_company_region",
            ExpectedValue = Value.TradeCompanyIdentifier,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province is in the trade company region."
        };

        public static readonly TriggerVariable trade_company_size = new TriggerVariable()
        {
            Name = "trade_company_size",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the trade company has at least X provinces."
        };

        public static readonly TriggerVariable trade_efficiency = new TriggerVariable()
        {
            Name = "trade_efficiency",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a trade efficiency over X."
        };

        public static readonly TriggerVariable trade_embargoing = new TriggerVariable()
        {
            Name = "trade_embargoing",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is embargoing country X."
        };

        public static readonly TriggerVariable trade_embargo_by = new TriggerVariable()
        {
            Name = "trade_embargo_by",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if country X is embargoing the country."
        };

        public static readonly TriggerVariable trade_goods = new TriggerVariable()
        {
            Name = "trade_goods",
            ExpectedValue = Value.TradeGoodIdentifier,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the province is producing the trade good X."
        };


        public static readonly TriggerVariable trade_income_percentage = new TriggerVariable()
        {
            Name = "trade_income_percentage",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the ratio of trade income to total income is at least X."
        };

        public static readonly TriggerVariable trade_node_value = new TriggerVariable()
        {
            Name = "trade_node_value",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Province,
            Description = "Returns true if total trade value in the node is at least X."
        };

        public static readonly TriggerVariable trade_range = new TriggerVariable()
        {
            Name = "trade_range",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Province,
            Description = "Returns true if the trade node is within the trade range of the specified country."
        };

        public static readonly TriggerVariable transport_fraction = new TriggerVariable()
        {
            Name = "transport_fraction",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the ratio of the transport fraction to the navy size of the country is at least X."
        };

        public static readonly TriggerVariable treasury = new TriggerVariable()
        {
            Name = "treasury",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if country's treasury contains at least X ducats."
        };

        public static readonly TriggerVariable tribal_allegiance = new TriggerVariable()
        {
            Name = "tribal_allegiance",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country has a tribal allegiance of at least X."
        };

        public static readonly TriggerVariable tribal_development = new TriggerVariable()
        {
            Name = "tribal_development",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.CountryORProvince,
            Description = "Returns true, if the country or province has at least X tribal development, or the scoped country has more development than the defined country."
        };


        public static readonly TriggerVariable truce_with = new TriggerVariable()
        {
            Name = "truce_with",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a truce with X."
        };

        public static readonly TriggerVariable trust = new TriggerVariable()
        {
            Name = "trust",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the current scope has a trust level of at least X with the specified tag."
        };

        public static readonly TriggerVariable unit_has_leader = new TriggerVariable()
        {
            Name = "unit_has_leader",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if any unit is in the province has a leader. Warning: Works only with 'yes'."
        };

        public static readonly TriggerVariable unit_in_battle = new TriggerVariable()
        {
            Name = "unit_in_battle",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if any unit in the province is in a battle."
        };

        public static readonly TriggerVariable unit_in_siege = new TriggerVariable()
        {
            Name = "unit_in_siege",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Province,
            Description = "Returns true if any unit in the province is in a siege."
        };

        public static readonly TriggerVariable units_in_province = new TriggerVariable()
        {
            Name = "units_in_province",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Province,
            Description = "Returns true if there are at least X units in the province."
        };

        public static readonly TriggerVariable unit_type = new TriggerVariable()
        {
            Name = "unit_type",
            ExpectedValue = Value.UnitTypeIdentifier,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has units of type X."
        };

        public static readonly TriggerVariable unrest = new TriggerVariable()
        {
            Name = "unrest",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.CountryORProvince,
            Description = "Returns true if the unrest in the province is at least X. If it is used in a country scope, it checks for the global_unrest modifier"
        };

        public static readonly TriggerVariable uses_authority = new TriggerVariable()
        {
            Name = "uses_authority",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country uses Pagan denominations#Inti|authority mechanics."
        };

        public static readonly TriggerVariable uses_church_aspects = new TriggerVariable()
        {
            Name = "uses_church_aspects",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country uses church aspects mechanics."
        };

        public static readonly TriggerVariable uses_blessings = new TriggerVariable()
        {
            Name = "uses_blessings",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country uses blessings mechanics."
        };

        public static readonly TriggerVariable uses_cults = new TriggerVariable()
        {
            Name = "uses_cults",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country uses cults mechanics."
        };

        public static readonly TriggerVariable uses_devotion = new TriggerVariable()
        {
            Name = "uses_devotion",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country uses devotion instead of legitimacy."
        };

        public static readonly TriggerVariable uses_doom = new TriggerVariable()
        {
            Name = "uses_doom",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country uses doom mechanics."
        };

        public static readonly TriggerVariable uses_fervor = new TriggerVariable()
        {
            Name = "uses_fervor",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country uses fervor mechanics."
        };

        public static readonly TriggerVariable uses_isolationism = new TriggerVariable()
        {
            Name = "uses_isolationism",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country uses the isolationism mechanic."
        };

        public static readonly TriggerVariable uses_karma = new TriggerVariable()
        {
            Name = "uses_karma",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country uses karma mechanics."
        };

        public static readonly TriggerVariable uses_papacy = new TriggerVariable()
        {
            Name = "uses_papacy",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country uses papacy mechanics."
        };

        public static readonly TriggerVariable uses_patriarch_authority = new TriggerVariable()
        {
            Name = "uses_patriarch_authority",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country uses patriarch authority mechanics."
        };

        public static readonly TriggerVariable uses_personal_deities = new TriggerVariable()
        {
            Name = "uses_personal_deities",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country uses personal deity|personal deities mechanics."
        };

        public static readonly TriggerVariable uses_piety = new TriggerVariable()
        {
            Name = "uses_piety",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country uses piety mechanics."
        };

        public static readonly TriggerVariable uses_religious_icons = new TriggerVariable()
        {
            Name = "uses_religious_icons",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true, if the country uses religious icons."
        };

        public static readonly TriggerVariable uses_syncretic_faiths = new TriggerVariable()
        {
            Name = "uses_syncretic_faiths",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country uses syncretic faiths mechanics."
        };

        public static readonly TriggerVariable vassal_of = new TriggerVariable()
        {
            Name = "vassal_of",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is a vassal of country X."
        };

        public static readonly TriggerVariable war_exhaustion = new TriggerVariable()
        {
            Name = "war_exhaustion",
            ExpectedValue = Value.IntegerORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a war exhaustion of at least X."
        };

        public static readonly TriggerVariable war_score = new TriggerVariable()
        {
            Name = "war_score",
            ExpectedValue = Value.Integer,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the coutry has a warscore of at least X%."
        };

        public static readonly TriggerVariable war_with = new TriggerVariable()
        {
            Name = "war_with",
            ExpectedValue = Value.TagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country is at war with country X."
        };

        public static readonly TriggerVariable was_player = new TriggerVariable()
        {
            Name = "was_player",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country was controlled by a human player."
        };

        public static readonly TriggerVariable was_tag = new TriggerVariable()
        {
            Name = "was_tag",
            ExpectedValue = Value.Tag,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country was a particular tag."
        };

        public static readonly TriggerVariable will_back_next_reform = new TriggerVariable()
        {
            Name = "will_back_next_reform",
            ExpectedValue = Value.Boolean,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the member of the HRE is backing the next imperial reform."
        };

        public static readonly TriggerVariable yearly_corruption_increase = new TriggerVariable()
        {
            Name = "yearly_corruption_increase",
            ExpectedValue = Value.Float,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country has a yearly corruption increase of at least X."
        };

        public static readonly TriggerVariable years_of_income = new TriggerVariable()
        {
            Name = "years_of_income",
            ExpectedValue = Value.FloatORTagORScope,
            AllowedScopes = Scope.Country,
            Description = "Returns true if the country's treasury contains ducats of at least X times their yearly income."
        };
    */
        public enum TriggerVar
        {
            NamedAdvisor, NamedBuilding, NamedIdeaGroup, NamedInstitution, NamedReligion, NamedSubjectType, NamedTradeGood,
            absolutism, accepted_culture, active_major_mission, adm, adm_power, adm_tech, advisor,
            advisor_exists, ai, alliance_with, allows_female_emperor, always, area, army_size,
            army_size_percentage, army_professionalism, army_tradition, artillery_fraction, artillery_in_province, at_war_with_religious_enemy, authority,
            average_autonomy, average_autonomy_above_min, average_effective_unrest, average_home_autonomy, average_unrest, base_manpower, base_production,
            base_tax, blockade, can_be_overlord, can_build, can_create_vassals, can_heir_be_child_of_consort, can_justify_trade_conflict,
            can_migrate, can_spawn_rebels, capital, cavalry_fraction, cavalry_in_province, province_has_center_of_trade_of_level, church_power,
            coalition_target, colonial_claim_by_anyone_of_religion, colonial_region, colony, colony_claim, colonysize, consort_adm,
            consort_age, consort_dip, consort_culture, consort_has_personality, consort_mil, consort_religion, construction_progress,
            continent, controlled_by, controls, core_claim, core_percentage, corruption, council_position,
            country_or_non_sovereign_subject_holds, country_or_subject_holds, crown_land_share, culture, culture_group, culture_group_claim, current_age,
            current_bribe, current_debate, current_icon, current_income_balance, current_institution, current_institution_growth, current_size_of_parliament,
            defensive_war_with, devastation, development, development_of_overlord_fraction, devotion, dip, diplomatic_reputation,
            dip_power, dip_tech, dominant_culture, dominant_religion, doom, dynasty, empire_of_china_reform_passed,
            estate_led_regency_influence, estate_led_regency_loyalty, exiled_same_dynasty_as_current, exists, faction_in_power, federation_size, fervor,
            fort_level, full_idea_group, galley_fraction, galleys_in_province, garrison, gives_military_access_to, gives_fleet_basing_rights_to,
            gold_income, gold_income_percentage, government, government_rank, grown_by_development, grown_by_states, great_power_rank,
            guaranteed_by, had_recent_war, harmonization_progress, harmony, has_active_debate, has_active_fervor, has_active_policy,
            has_active_triggered_province_modifier, has_adopted_cult, has_advisor, has_age_ability, has_any_disaster, has_border_with_religious_enemy, has_building,
            has_cardinal, has_casus_belli_against, has_center_of_trade_of_level, has_changed_nation, has_church_aspect, has_climate, has_colonial_parent,
            has_colonist, has_commanding_three_star, has_consort, has_consort_flag, has_consort_regency, has_construction, has_country_flag,
            has_country_modifier, has_custom_ideas, has_disaster, has_discovered, has_dlc, has_divert_trade, has_embargo_rivals,
            has_empty_adjacent_province, has_estate, has_estate_province, has_estate_loan, has_estate_privilege, has_faction, has_factions,
            has_female_consort, has_female_heir, has_first_revolution_started, has_flagship, has_foreign_consort, has_foreign_heir, has_friendly_reformation_center,
            has_game_started, has_given_consort_to, has_guaranteed, has_global_flag, has_government_mechanic, has_government_power, has_had_golden_age,
            has_harmonized_with, has_harsh_treatment, has_heir, has_heir_flag, has_heir_leader_from, has_hostile_reformation_center, has_idea,
            has_idea_group, has_influencing_fort, has_institution, has_latent_trade_goods, has_leader, has_matching_religion, has_merchant,
            has_mission, has_missionary, has_monsoon, has_most_province_trade_power, has_new_dynasty, has_owner_accepted_culture, has_owner_culture,
            has_owner_religion, has_pasha, has_parliament, has_personal_deity, has_pillaged_capital_against, has_port, has_privateers,
            has_promote_investments, has_province_flag, has_province_modifier, has_rebel_faction, has_regency, has_reform, government_reform_progress,
            has_removed_fow, has_revolution_in_province, has_ruler, has_ruler_flag, has_ruler_leader_from, has_ruler_modifier, has_saved_event_target,
            has_scutage, has_seat_in_parliament, has_secondary_religion, has_send_officers, has_siege, has_spawned_rebels, has_spawned_supported_rebels,
            has_state_edict, has_state_patriach, has_subsidize_armies, has_supply_depot, has_support_loyalists, has_subject_of_type, has_switched_nation,
            has_terrain, has_trader, has_truce, has_unembraced_institution, has_unified_culture_group, has_unit_type, has_unlocked_cult,
            has_wartaxes, has_winter, have_had_reform, heavy_ship_fraction, heavy_ships_in_province, heir_adm, heir_age,
            heir_dip, heir_claim, heir_culture, heir_has_consort_dynasty, heir_has_personality, heir_has_ruler_dynasty, heir_mil,
            heir_nationality, heir_religion, higher_development_than, highest_value_trade_node, historical_friend_with, historical_rival_with, holy_order,
            horde_unity, hre_heretic_religion, hre_leagues_enabled, hre_reform_passed, hre_religion, hre_religion_locked, hre_religion_treaty,
            hre_size, imperial_influence, imperial_mandate, in_golden_age, infantry_fraction, infantry_in_province, inflation,
            innovativeness, invested_papal_influence, in_league, ironman, is_advisor_employed, is_all_concessions_in_council_taken, is_at_war,
            is_backing_current_issue, is_bankrupt, is_blockaded, is_blockaded_by, is_capital, is_capital_of, is_city,
            is_claim, is_client_nation, is_client_nation_of, is_colonial_nation, is_colonial_nation_of, is_colony, is_core,
            is_council_enabled, is_crusade_target, is_defender_of_faith, is_defender_of_faith_of_tier, is_dynamic_tag, is_elector, is_emperor,
            is_emperor_of_china, is_empty, is_enemy, is_excommunicated, is_federation_leader, is_female, is_force_converted,
            is_former_colonial_nation, is_foreign_claim, is_great_power, is_harmonizing_with, is_heir_leader, is_hegemon, is_hegemon_of_type,
            is_imperial_ban_allowed, is_incident_active, is_incident_happened, is_incident_possible, is_incident_potential, is_institution_enabled, is_institution_origin,
            is_in_capital_area, is_in_coalition, is_in_coalition_war, is_in_deficit, is_in_extended_regency, is_in_league_war, is_in_trade_league,
            is_in_trade_league_with, is_island, is_league_enemy, is_lacking_institutions, is_league_friend, is_league_leader, is_lesser_in_union,
            is_looted, is_monarch_leader, is_month, is_march, is_neighbor_of, is_node_in_trade_company_region, is_nomad,
            is_orangists_in_power, is_origin_of_consort, is_overseas, is_overseas_subject, is_owned_by_trade_company, is_papal_controller, is_part_of_hre,
            is_permanent_claim, is_playing_custom_nation, is_possible_march, is_possible_vassal, is_previous_papal_controller, is_prosperous, is_protectorate,
            is_random_new_world, is_reformation_center, is_religion_grant_colonial_claim, is_religion_enabled, is_religion_reformed, is_renting_condottieri_to, is_revolution_target,
            is_rival, is_ruler_commanding_unit, is_sea, is_state, is_state_core, is_statists_in_power, is_strongest_trade_power,
            is_subject, is_subject_of, is_subject_of_type, is_supporting_independence_of, is_territorial_core, is_territory, is_threat,
            is_trade_league_leader, is_tribal, is_vassal, is_wasteland, is_year, island, isolationism,
            janissary_percentage, junior_union_with, karma, knows_country, land_forcelimit, land_maintenance, land_morale,
            last_mission, legitimacy, legitimacy_equivalent, legitimacy_or_horde_unity, liberty_desire, light_ship_fraction, light_ships_in_province,
            likely_rebels, local_autonomy, local_autonomy_above_min, luck, march_of, manpower, manpower_percentage,
            marriage_with, max_manpower, mercantilism, meritocracy, mil, militarised_society, mil_power,
            mil_tech, mission_completed, monthly_income, monthly_adm, monthly_dip, monthly_mil, months_of_ruling,
            months_since_defection, nationalism, national_focus, nation_designer_points, native_ferocity, native_hostileness, native_policy,
            native_size, naval_forcelimit, naval_maintenance, naval_morale, navy_size, navy_size_percentage, navy_tradition,
            normal_or_historical_nations, normal_province_values, num_accepted_cultures, num_free_building_slots, num_of_active_blessings, num_of_admirals, num_of_admirals_with_traits,
            num_of_allies, num_of_artillery, num_of_aspects, num_of_banners, num_of_buildings_in_province, num_of_captured_ships_with_boarding_doctrine, num_of_centers_of_trade,
            num_of_cardinals, num_of_cavalry, num_of_cawa, num_of_cities, num_of_coalition_members, num_of_colonies, num_of_colonists,
            num_of_conquistadors, num_of_consorts, num_of_continents, num_of_cossacks, num_of_custom_nations, num_of_diplomatic_relations, num_of_diplomats,
            num_of_electors, num_of_explorers, num_of_foreign_hre_provinces, num_of_free_diplomatic_relations, num_of_galley, num_of_generals, num_of_generals_with_traits,
            num_of_harmonized, num_of_heavy_ship, num_of_infantry, num_of_light_ship, num_of_loans, num_of_marches, num_of_marines,
            num_of_mercenaries, num_of_merchants, num_of_missionaries, num_of_owned_and_controlled_institutions, num_of_ports, num_of_ports_blockading, num_of_powerful_estates,
            num_of_protectorates, num_of_provinces_in_states, num_of_provinces_in_territories, num_of_rajput, num_of_rebel_armies, num_of_rebel_controlled_provinces, num_of_revolts,
            num_of_royal_marriages, num_of_ruler_traits, num_of_states, num_of_streltsy, num_of_strong_trade_companies, num_of_subjects, num_of_territories,
            num_of_times_improved, num_of_times_improved_by_owner, num_of_times_used_pillage_capital, num_of_times_used_transfer_development, num_of_total_ports, num_of_trade_companies, num_of_trade_embargos,
            num_of_trading_bonuses, num_of_transport, num_of_trusted_allies, num_of_unions, num_of_unlocked_cults, num_of_war_reparations, num_ships_privateering,
            offensive_war_with, overextension_percentage, overlord_of, overseas_provinces_percentage, owned_by, owns, owns_core_province,
            owns_or_non_sovereign_subject_of, owns_or_subject_of, papacy_active, papal_influence, patriarch_authority, percentage_backing_issue, personality,
            piety, preferred_emperor, prestige, previous_owner, power_projection, primary_culture, primitives,
            production_efficiency, production_income_percentage, province_id, province_is_on_an_island, province_getting_expelled_minority, province_group, province_size,
            province_trade_power, provinces_on_capital_continent_of, pure_unrest, range, real_day_of_year, real_month_of_year, reform_desire,
            receives_military_access_from, receives_fleet_basing_rights_from, reform_level, region, religion, religion_group, religious_unity,
            republican_tradition, revanchism, revolt_percentage, revolution_target_exists, ruler_age, ruler_consort_marriage_length, ruler_culture,
            ruler_has_personality, ruler_is_foreigner, ruler_religion, sailors, sailors_percentage, max_sailors, same_continent,
            secondary_religion, senior_union_with, sieged_by, splendor, stability, start_date, started_in,
            statists_vs_orangists, subsidised_percent_amount, succession_claim, superregion, tag, tariff_value, tax_income_percentage,
            tech_difference, technology_group, tolerance_to_this, total_base_tax, total_development, total_number_of_cardinals, trade_league_embargoed_by,
            total_own_and_non_tributary_subject_development, transports_in_province, trade_company_region, trade_company_size, trade_efficiency, trade_embargoing, trade_embargo_by,
            trade_goods, trade_income_percentage, trade_node_value, trade_range, transport_fraction, treasury, tribal_allegiance,
            tribal_development, truce_with, trust, unit_has_leader, unit_in_battle, unit_in_siege, units_in_province,
            unit_type, unrest, uses_authority, uses_church_aspects, uses_blessings, uses_cults, uses_devotion,
            uses_doom, uses_fervor, uses_isolationism, uses_karma, uses_papacy, uses_patriarch_authority, uses_personal_deities,
            uses_piety, uses_religious_icons, uses_syncretic_faiths, vassal_of, war_exhaustion, war_score, war_with,
            was_player, was_tag, will_back_next_reform, yearly_corruption_increase, years_of_income, UnknownTrigger,
        }
        public static string GetName(TriggerVar Variable)
        {
            switch (Variable)
            {
                default:
                    return "UnknownTrigger";
                case TriggerVar.NamedAdvisor:
                    return "<advisor>";
                case TriggerVar.NamedBuilding:
                    return "<building>";
                case TriggerVar.NamedIdeaGroup:
                    return "<idea group>";
                case TriggerVar.NamedInstitution:
                    return "<institution>";
                case TriggerVar.NamedReligion:
                    return "<religion>";
                case TriggerVar.NamedSubjectType:
                    return "<subject_type>";
                case TriggerVar.NamedTradeGood:
                    return "<trade_good>";
                case TriggerVar.absolutism:
                    return "absolutism";
                case TriggerVar.accepted_culture:
                    return "accepted_culture";
                case TriggerVar.active_major_mission:
                    return "active_major_mission";
                case TriggerVar.adm:
                    return "adm";
                case TriggerVar.adm_power:
                    return "adm_power";
                case TriggerVar.adm_tech:
                    return "adm_tech";
                case TriggerVar.advisor:
                    return "advisor";
                case TriggerVar.advisor_exists:
                    return "advisor_exists";
                case TriggerVar.ai:
                    return "ai";
                case TriggerVar.alliance_with:
                    return "alliance_with";
                case TriggerVar.allows_female_emperor:
                    return "allows_female_emperor";
                case TriggerVar.always:
                    return "always";
                case TriggerVar.area:
                    return "area";
                case TriggerVar.army_size:
                    return "army_size";
                case TriggerVar.army_size_percentage:
                    return "army_size_percentage";
                case TriggerVar.army_professionalism:
                    return "army_professionalism";
                case TriggerVar.army_tradition:
                    return "army_tradition";
                case TriggerVar.artillery_fraction:
                    return "artillery_fraction";
                case TriggerVar.artillery_in_province:
                    return "artillery_in_province";
                case TriggerVar.at_war_with_religious_enemy:
                    return "at_war_with_religious_enemy";
                case TriggerVar.authority:
                    return "authority";
                case TriggerVar.average_autonomy:
                    return "average_autonomy";
                case TriggerVar.average_autonomy_above_min:
                    return "average_autonomy_above_min";
                case TriggerVar.average_effective_unrest:
                    return "average_effective_unrest";
                case TriggerVar.average_home_autonomy:
                    return "average_home_autonomy";
                case TriggerVar.average_unrest:
                    return "average_unrest";
                case TriggerVar.base_manpower:
                    return "base_manpower";
                case TriggerVar.base_production:
                    return "base_production";
                case TriggerVar.base_tax:
                    return "base_tax";
                case TriggerVar.blockade:
                    return "blockade";
                case TriggerVar.can_be_overlord:
                    return "can_be_overlord";
                case TriggerVar.can_build:
                    return "can_build";
                case TriggerVar.can_create_vassals:
                    return "can_create_vassals";
                case TriggerVar.can_heir_be_child_of_consort:
                    return "can_heir_be_child_of_consort";
                case TriggerVar.can_justify_trade_conflict:
                    return "can_justify_trade_conflict";
                case TriggerVar.can_migrate:
                    return "can_migrate";
                case TriggerVar.can_spawn_rebels:
                    return "can_spawn_rebels";
                case TriggerVar.capital:
                    return "capital";
                case TriggerVar.cavalry_fraction:
                    return "cavalry_fraction";
                case TriggerVar.cavalry_in_province:
                    return "cavalry_in_province";
                case TriggerVar.province_has_center_of_trade_of_level:
                    return "province_has_center_of_trade_of_level";
                case TriggerVar.church_power:
                    return "church_power";
                case TriggerVar.coalition_target:
                    return "coalition_target";
                case TriggerVar.colonial_claim_by_anyone_of_religion:
                    return "colonial_claim_by_anyone_of_religion";
                case TriggerVar.colonial_region:
                    return "colonial_region";
                case TriggerVar.colony:
                    return "colony";
                case TriggerVar.colony_claim:
                    return "colony_claim";
                case TriggerVar.colonysize:
                    return "colonysize";
                case TriggerVar.consort_adm:
                    return "consort_adm";
                case TriggerVar.consort_age:
                    return "consort_age";
                case TriggerVar.consort_dip:
                    return "consort_dip";
                case TriggerVar.consort_culture:
                    return "consort_culture";
                case TriggerVar.consort_has_personality:
                    return "consort_has_personality";
                case TriggerVar.consort_mil:
                    return "consort_mil";
                case TriggerVar.consort_religion:
                    return "consort_religion";
                case TriggerVar.construction_progress:
                    return "construction_progress";
                case TriggerVar.continent:
                    return "continent";
                case TriggerVar.controlled_by:
                    return "controlled_by";
                case TriggerVar.controls:
                    return "controls";
                case TriggerVar.core_claim:
                    return "core_claim";
                case TriggerVar.core_percentage:
                    return "core_percentage";
                case TriggerVar.corruption:
                    return "corruption";
                case TriggerVar.council_position:
                    return "council_position";
                case TriggerVar.country_or_non_sovereign_subject_holds:
                    return "country_or_non_sovereign_subject_holds";
                case TriggerVar.country_or_subject_holds:
                    return "country_or_subject_holds";
                case TriggerVar.crown_land_share:
                    return "crown_land_share";
                case TriggerVar.culture:
                    return "culture";
                case TriggerVar.culture_group:
                    return "culture_group";
                case TriggerVar.culture_group_claim:
                    return "culture_group_claim";
                case TriggerVar.current_age:
                    return "current_age";
                case TriggerVar.current_bribe:
                    return "current_bribe";
                case TriggerVar.current_debate:
                    return "current_debate";
                case TriggerVar.current_icon:
                    return "current_icon";
                case TriggerVar.current_income_balance:
                    return "current_income_balance";
                case TriggerVar.current_institution:
                    return "current_institution";
                case TriggerVar.current_institution_growth:
                    return "current_institution_growth";
                case TriggerVar.current_size_of_parliament:
                    return "current_size_of_parliament";
                case TriggerVar.defensive_war_with:
                    return "defensive_war_with";
                case TriggerVar.devastation:
                    return "devastation";
                case TriggerVar.development:
                    return "development";
                case TriggerVar.development_of_overlord_fraction:
                    return "development_of_overlord_fraction";
                case TriggerVar.devotion:
                    return "devotion";
                case TriggerVar.dip:
                    return "dip";
                case TriggerVar.diplomatic_reputation:
                    return "diplomatic_reputation";
                case TriggerVar.dip_power:
                    return "dip_power";
                case TriggerVar.dip_tech:
                    return "dip_tech";
                case TriggerVar.dominant_culture:
                    return "dominant_culture";
                case TriggerVar.dominant_religion:
                    return "dominant_religion";
                case TriggerVar.doom:
                    return "doom";
                case TriggerVar.dynasty:
                    return "dynasty";
                case TriggerVar.empire_of_china_reform_passed:
                    return "empire_of_china_reform_passed";
                case TriggerVar.estate_led_regency_influence:
                    return "estate_led_regency_influence";
                case TriggerVar.estate_led_regency_loyalty:
                    return "estate_led_regency_loyalty";
                case TriggerVar.exiled_same_dynasty_as_current:
                    return "exiled_same_dynasty_as_current";
                case TriggerVar.exists:
                    return "exists";
                case TriggerVar.faction_in_power:
                    return "faction_in_power";
                case TriggerVar.federation_size:
                    return "federation_size";
                case TriggerVar.fervor:
                    return "fervor";
                case TriggerVar.fort_level:
                    return "fort_level";
                case TriggerVar.full_idea_group:
                    return "full_idea_group";
                case TriggerVar.galley_fraction:
                    return "galley_fraction";
                case TriggerVar.galleys_in_province:
                    return "galleys_in_province";
                case TriggerVar.garrison:
                    return "garrison";
                case TriggerVar.gives_military_access_to:
                    return "gives_military_access_to";
                case TriggerVar.gives_fleet_basing_rights_to:
                    return "gives_fleet_basing_rights_to";
                case TriggerVar.gold_income:
                    return "gold_income";
                case TriggerVar.gold_income_percentage:
                    return "gold_income_percentage";
                case TriggerVar.government:
                    return "government";
                case TriggerVar.government_rank:
                    return "government_rank";
                case TriggerVar.grown_by_development:
                    return "grown_by_development";
                case TriggerVar.grown_by_states:
                    return "grown_by_states";
                case TriggerVar.great_power_rank:
                    return "great_power_rank";
                case TriggerVar.guaranteed_by:
                    return "guaranteed_by";
                case TriggerVar.had_recent_war:
                    return "had_recent_war";
                case TriggerVar.harmonization_progress:
                    return "harmonization_progress";
                case TriggerVar.harmony:
                    return "harmony";
                case TriggerVar.has_active_debate:
                    return "has_active_debate";
                case TriggerVar.has_active_fervor:
                    return "has_active_fervor";
                case TriggerVar.has_active_policy:
                    return "has_active_policy";
                case TriggerVar.has_active_triggered_province_modifier:
                    return "has_active_triggered_province_modifier";
                case TriggerVar.has_adopted_cult:
                    return "has_adopted_cult";
                case TriggerVar.has_advisor:
                    return "has_advisor";
                case TriggerVar.has_age_ability:
                    return "has_age_ability";
                case TriggerVar.has_any_disaster:
                    return "has_any_disaster";
                case TriggerVar.has_border_with_religious_enemy:
                    return "has_border_with_religious_enemy";
                case TriggerVar.has_building:
                    return "has_building";
                case TriggerVar.has_cardinal:
                    return "has_cardinal";
                case TriggerVar.has_casus_belli_against:
                    return "has_casus_belli_against";
                case TriggerVar.has_center_of_trade_of_level:
                    return "has_center_of_trade_of_level";
                case TriggerVar.has_changed_nation:
                    return "has_changed_nation";
                case TriggerVar.has_church_aspect:
                    return "has_church_aspect";
                case TriggerVar.has_climate:
                    return "has_climate";
                case TriggerVar.has_colonial_parent:
                    return "has_colonial_parent";
                case TriggerVar.has_colonist:
                    return "has_colonist";
                case TriggerVar.has_commanding_three_star:
                    return "has_commanding_three_star";
                case TriggerVar.has_consort:
                    return "has_consort";
                case TriggerVar.has_consort_flag:
                    return "has_consort_flag";
                case TriggerVar.has_consort_regency:
                    return "has_consort_regency";
                case TriggerVar.has_construction:
                    return "has_construction";
                case TriggerVar.has_country_flag:
                    return "has_country_flag";
                case TriggerVar.has_country_modifier:
                    return "has_country_modifier";
                case TriggerVar.has_custom_ideas:
                    return "has_custom_ideas";
                case TriggerVar.has_disaster:
                    return "has_disaster";
                case TriggerVar.has_discovered:
                    return "has_discovered";
                case TriggerVar.has_dlc:
                    return "has_dlc";
                case TriggerVar.has_divert_trade:
                    return "has_divert_trade";
                case TriggerVar.has_embargo_rivals:
                    return "has_embargo_rivals";
                case TriggerVar.has_empty_adjacent_province:
                    return "has_empty_adjacent_province";
                case TriggerVar.has_estate:
                    return "has_estate";
                case TriggerVar.has_estate_province:
                    return "has_estate";
                case TriggerVar.has_estate_loan:
                    return "has_estate_loan";
                case TriggerVar.has_estate_privilege:
                    return "has_estate_privilege";
                case TriggerVar.has_faction:
                    return "has_faction";
                case TriggerVar.has_factions:
                    return "has_factions";
                case TriggerVar.has_female_consort:
                    return "has_female_consort";
                case TriggerVar.has_female_heir:
                    return "has_female_heir";
                case TriggerVar.has_first_revolution_started:
                    return "has_first_revolution_started";
                case TriggerVar.has_flagship:
                    return "has_flagship";
                case TriggerVar.has_foreign_consort:
                    return "has_foreign_consort";
                case TriggerVar.has_foreign_heir:
                    return "has_foreign_heir";
                case TriggerVar.has_friendly_reformation_center:
                    return "has_friendly_reformation_center";
                case TriggerVar.has_game_started:
                    return "has_game_started";
                case TriggerVar.has_given_consort_to:
                    return "has_given_consort_to";
                case TriggerVar.has_guaranteed:
                    return "has_guaranteed";
                case TriggerVar.has_global_flag:
                    return "has_global_flag";
                case TriggerVar.has_government_mechanic:
                    return "has_government_mechanic";
                case TriggerVar.has_government_power:
                    return "has_government_power";
                case TriggerVar.has_had_golden_age:
                    return "has_had_golden_age";
                case TriggerVar.has_harmonized_with:
                    return "has_harmonized_with";
                case TriggerVar.has_harsh_treatment:
                    return "has_harsh_treatment";
                case TriggerVar.has_heir:
                    return "has_heir";
                case TriggerVar.has_heir_flag:
                    return "has_heir_flag";
                case TriggerVar.has_heir_leader_from:
                    return "has_heir_leader_from";
                case TriggerVar.has_hostile_reformation_center:
                    return "has_hostile_reformation_center";
                case TriggerVar.has_idea:
                    return "has_idea";
                case TriggerVar.has_idea_group:
                    return "has_idea_group";
                case TriggerVar.has_influencing_fort:
                    return "has_influencing_fort";
                case TriggerVar.has_institution:
                    return "has_institution";
                case TriggerVar.has_latent_trade_goods:
                    return "has_latent_trade_goods";
                case TriggerVar.has_leader:
                    return "has_leader";
                case TriggerVar.has_matching_religion:
                    return "has_matching_religion";
                case TriggerVar.has_merchant:
                    return "has_merchant";
                case TriggerVar.has_mission:
                    return "has_mission";
                case TriggerVar.has_missionary:
                    return "has_missionary";
                case TriggerVar.has_monsoon:
                    return "has_monsoon";
                case TriggerVar.has_most_province_trade_power:
                    return "has_most_province_trade_power";
                case TriggerVar.has_new_dynasty:
                    return "has_new_dynasty";
                case TriggerVar.has_owner_accepted_culture:
                    return "has_owner_accepted_culture";
                case TriggerVar.has_owner_culture:
                    return "has_owner_culture";
                case TriggerVar.has_owner_religion:
                    return "has_owner_religion";
                case TriggerVar.has_pasha:
                    return "has_pasha";
                case TriggerVar.has_parliament:
                    return "has_parliament";
                case TriggerVar.has_personal_deity:
                    return "has_personal_deity";
                case TriggerVar.has_pillaged_capital_against:
                    return "has_pillaged_capital_against";
                case TriggerVar.has_port:
                    return "has_port";
                case TriggerVar.has_privateers:
                    return "has_privateers";
                case TriggerVar.has_promote_investments:
                    return "has_promote_investments";
                case TriggerVar.has_province_flag:
                    return "has_province_flag";
                case TriggerVar.has_province_modifier:
                    return "has_province_modifier";
                case TriggerVar.has_rebel_faction:
                    return "has_rebel_faction";
                case TriggerVar.has_regency:
                    return "has_regency";
                case TriggerVar.has_reform:
                    return "has_reform";
                case TriggerVar.government_reform_progress:
                    return "government_reform_progress";
                case TriggerVar.has_removed_fow:
                    return "has_removed_fow";
                case TriggerVar.has_revolution_in_province:
                    return "has_revolution_in_province";
                case TriggerVar.has_ruler:
                    return "has_ruler";
                case TriggerVar.has_ruler_flag:
                    return "has_ruler_flag";
                case TriggerVar.has_ruler_leader_from:
                    return "has_ruler_leader_from";
                case TriggerVar.has_ruler_modifier:
                    return "has_ruler_modifier";
                case TriggerVar.has_saved_event_target:
                    return "has_saved_event_target";
                case TriggerVar.has_scutage:
                    return "has_scutage";
                case TriggerVar.has_seat_in_parliament:
                    return "has_seat_in_parliament";
                case TriggerVar.has_secondary_religion:
                    return "has_secondary_religion";
                case TriggerVar.has_send_officers:
                    return "has_send_officers";
                case TriggerVar.has_siege:
                    return "has_siege";
                case TriggerVar.has_spawned_rebels:
                    return "has_spawned_rebels";
                case TriggerVar.has_spawned_supported_rebels:
                    return "has_spawned_supported_rebels";
                case TriggerVar.has_state_edict:
                    return "has_state_edict";
                case TriggerVar.has_state_patriach:
                    return "has_state_patriach";
                case TriggerVar.has_subsidize_armies:
                    return "has_subsidize_armies";
                case TriggerVar.has_supply_depot:
                    return "has_supply_depot";
                case TriggerVar.has_support_loyalists:
                    return "has_support_loyalists";
                case TriggerVar.has_subject_of_type:
                    return "has_subject_of_type";
                case TriggerVar.has_switched_nation:
                    return "has_switched_nation";
                case TriggerVar.has_terrain:
                    return "has_terrain";
                case TriggerVar.has_trader:
                    return "has_trader";
                case TriggerVar.has_truce:
                    return "has_truce";
                case TriggerVar.has_unembraced_institution:
                    return "has_unembraced_institution";
                case TriggerVar.has_unified_culture_group:
                    return "has_unified_culture_group";
                case TriggerVar.has_unit_type:
                    return "has_unit_type";
                case TriggerVar.has_unlocked_cult:
                    return "has_unlocked_cult";
                case TriggerVar.has_wartaxes:
                    return "has_wartaxes";
                case TriggerVar.has_winter:
                    return "has_winter";
                case TriggerVar.have_had_reform:
                    return "have_had_reform";
                case TriggerVar.heavy_ship_fraction:
                    return "heavy_ship_fraction";
                case TriggerVar.heavy_ships_in_province:
                    return "heavy_ships_in_province";
                case TriggerVar.heir_adm:
                    return "heir_adm";
                case TriggerVar.heir_age:
                    return "heir_age";
                case TriggerVar.heir_dip:
                    return "heir_dip";
                case TriggerVar.heir_claim:
                    return "heir_claim";
                case TriggerVar.heir_culture:
                    return "heir_culture";
                case TriggerVar.heir_has_consort_dynasty:
                    return "heir_has_consort_dynasty";
                case TriggerVar.heir_has_personality:
                    return "heir_has_personality";
                case TriggerVar.heir_has_ruler_dynasty:
                    return "heir_has_ruler_dynasty";
                case TriggerVar.heir_mil:
                    return "heir_mil";
                case TriggerVar.heir_nationality:
                    return "heir_nationality";
                case TriggerVar.heir_religion:
                    return "heir_religion";
                case TriggerVar.higher_development_than:
                    return "higher_development_than";
                case TriggerVar.highest_value_trade_node:
                    return "highest_value_trade_node";
                case TriggerVar.historical_friend_with:
                    return "historical_friend_with";
                case TriggerVar.historical_rival_with:
                    return "historical_rival_with";
                case TriggerVar.holy_order:
                    return "holy_order";
                case TriggerVar.horde_unity:
                    return "horde_unity";
                case TriggerVar.hre_heretic_religion:
                    return "hre_heretic_religion";
                case TriggerVar.hre_leagues_enabled:
                    return "hre_leagues_enabled";
                case TriggerVar.hre_reform_passed:
                    return "hre_reform_passed";
                case TriggerVar.hre_religion:
                    return "hre_religion";
                case TriggerVar.hre_religion_locked:
                    return "hre_religion_locked";
                case TriggerVar.hre_religion_treaty:
                    return "hre_religion_treaty";
                case TriggerVar.hre_size:
                    return "hre_size";
                case TriggerVar.imperial_influence:
                    return "imperial_influence";
                case TriggerVar.imperial_mandate:
                    return "imperial_mandate";
                case TriggerVar.in_golden_age:
                    return "in_golden_age";
                case TriggerVar.infantry_fraction:
                    return "infantry_fraction";
                case TriggerVar.infantry_in_province:
                    return "infantry_in_province";
                case TriggerVar.inflation:
                    return "inflation";
                case TriggerVar.innovativeness:
                    return "innovativeness";
                case TriggerVar.invested_papal_influence:
                    return "invested_papal_influence";
                case TriggerVar.in_league:
                    return "in_league";
                case TriggerVar.ironman:
                    return "ironman";
                case TriggerVar.is_advisor_employed:
                    return "is_advisor_employed";
                case TriggerVar.is_all_concessions_in_council_taken:
                    return "is_all_concessions_in_council_taken";
                case TriggerVar.is_at_war:
                    return "is_at_war";
                case TriggerVar.is_backing_current_issue:
                    return "is_backing_current_issue";
                case TriggerVar.is_bankrupt:
                    return "is_bankrupt";
                case TriggerVar.is_blockaded:
                    return "is_blockaded";
                case TriggerVar.is_blockaded_by:
                    return "is_blockaded_by";
                case TriggerVar.is_capital:
                    return "is_capital";
                case TriggerVar.is_capital_of:
                    return "is_capital_of";
                case TriggerVar.is_city:
                    return "is_city";
                case TriggerVar.is_claim:
                    return "is_claim";
                case TriggerVar.is_client_nation:
                    return "is_client_nation";
                case TriggerVar.is_client_nation_of:
                    return "is_client_nation_of";
                case TriggerVar.is_colonial_nation:
                    return "is_colonial_nation";
                case TriggerVar.is_colonial_nation_of:
                    return "is_colonial_nation_of";
                case TriggerVar.is_colony:
                    return "is_colony";
                case TriggerVar.is_core:
                    return "is_core";
                case TriggerVar.is_council_enabled:
                    return "is_council_enabled";
                case TriggerVar.is_crusade_target:
                    return "is_crusade_target";
                case TriggerVar.is_defender_of_faith:
                    return "is_defender_of_faith";
                case TriggerVar.is_defender_of_faith_of_tier:
                    return "is_defender_of_faith_of_tier";
                case TriggerVar.is_dynamic_tag:
                    return "is_dynamic_tag";
                case TriggerVar.is_elector:
                    return "is_elector";
                case TriggerVar.is_emperor:
                    return "is_emperor";
                case TriggerVar.is_emperor_of_china:
                    return "is_emperor_of_china";
                case TriggerVar.is_empty:
                    return "is_empty";
                case TriggerVar.is_enemy:
                    return "is_enemy";
                case TriggerVar.is_excommunicated:
                    return "is_excommunicated";
                case TriggerVar.is_federation_leader:
                    return "is_federation_leader";
                case TriggerVar.is_female:
                    return "is_female";
                case TriggerVar.is_force_converted:
                    return "is_force_converted";
                case TriggerVar.is_former_colonial_nation:
                    return "is_former_colonial_nation";
                case TriggerVar.is_foreign_claim:
                    return "is_foreign_claim";
                case TriggerVar.is_great_power:
                    return "is_great_power";
                case TriggerVar.is_harmonizing_with:
                    return "is_harmonizing_with";
                case TriggerVar.is_heir_leader:
                    return "is_heir_leader";
                case TriggerVar.is_hegemon:
                    return "is_hegemon";
                case TriggerVar.is_hegemon_of_type:
                    return "is_hegemon_of_type";
                case TriggerVar.is_imperial_ban_allowed:
                    return "is_imperial_ban_allowed";
                case TriggerVar.is_incident_active:
                    return "is_incident_active";
                case TriggerVar.is_incident_happened:
                    return "is_incident_happened";
                case TriggerVar.is_incident_possible:
                    return "is_incident_possible";
                case TriggerVar.is_incident_potential:
                    return "is_incident_potential";
                case TriggerVar.is_institution_enabled:
                    return "is_institution_enabled";
                case TriggerVar.is_institution_origin:
                    return "is_institution_origin";
                case TriggerVar.is_in_capital_area:
                    return "is_in_capital_area";
                case TriggerVar.is_in_coalition:
                    return "is_in_coalition";
                case TriggerVar.is_in_coalition_war:
                    return "is_in_coalition_war";
                case TriggerVar.is_in_deficit:
                    return "is_in_deficit";
                case TriggerVar.is_in_extended_regency:
                    return "is_in_extended_regency";
                case TriggerVar.is_in_league_war:
                    return "is_in_league_war";
                case TriggerVar.is_in_trade_league:
                    return "is_in_trade_league";
                case TriggerVar.is_in_trade_league_with:
                    return "is_in_trade_league_with";
                case TriggerVar.is_island:
                    return "is_island";
                case TriggerVar.is_league_enemy:
                    return "is_league_enemy";
                case TriggerVar.is_lacking_institutions:
                    return "is_lacking_institutions";
                case TriggerVar.is_league_friend:
                    return "is_league_friend";
                case TriggerVar.is_league_leader:
                    return "is_league_leader";
                case TriggerVar.is_lesser_in_union:
                    return "is_lesser_in_union";
                case TriggerVar.is_looted:
                    return "is_looted";
                case TriggerVar.is_monarch_leader:
                    return "is_monarch_leader";
                case TriggerVar.is_month:
                    return "is_month";
                case TriggerVar.is_march:
                    return "is_march";
                case TriggerVar.is_neighbor_of:
                    return "is_neighbor_of";
                case TriggerVar.is_node_in_trade_company_region:
                    return "is_node_in_trade_company_region";
                case TriggerVar.is_nomad:
                    return "is_nomad";
                case TriggerVar.is_orangists_in_power:
                    return "is_orangists_in_power";
                case TriggerVar.is_origin_of_consort:
                    return "is_origin_of_consort";
                case TriggerVar.is_overseas:
                    return "is_overseas";
                case TriggerVar.is_overseas_subject:
                    return "is_overseas_subject";
                case TriggerVar.is_owned_by_trade_company:
                    return "is_owned_by_trade_company";
                case TriggerVar.is_papal_controller:
                    return "is_papal_controller";
                case TriggerVar.is_part_of_hre:
                    return "is_part_of_hre";
                case TriggerVar.is_permanent_claim:
                    return "is_permanent_claim";
                case TriggerVar.is_playing_custom_nation:
                    return "is_playing_custom_nation";
                case TriggerVar.is_possible_march:
                    return "is_possible_march";
                case TriggerVar.is_possible_vassal:
                    return "is_possible_vassal";
                case TriggerVar.is_previous_papal_controller:
                    return "is_previous_papal_controller";
                case TriggerVar.is_prosperous:
                    return "is_prosperous";
                case TriggerVar.is_protectorate:
                    return "is_protectorate";
                case TriggerVar.is_random_new_world:
                    return "is_random_new_world";
                case TriggerVar.is_reformation_center:
                    return "is_reformation_center";
                case TriggerVar.is_religion_grant_colonial_claim:
                    return "is_religion_grant_colonial_claim";
                case TriggerVar.is_religion_enabled:
                    return "is_religion_enabled";
                case TriggerVar.is_religion_reformed:
                    return "is_religion_reformed";
                case TriggerVar.is_renting_condottieri_to:
                    return "is_renting_condottieri_to";
                case TriggerVar.is_revolution_target:
                    return "is_revolution_target";
                case TriggerVar.is_rival:
                    return "is_rival";
                case TriggerVar.is_ruler_commanding_unit:
                    return "is_ruler_commanding_unit";
                case TriggerVar.is_sea:
                    return "is_sea";
                case TriggerVar.is_state:
                    return "is_state";
                case TriggerVar.is_state_core:
                    return "is_state_core";
                case TriggerVar.is_statists_in_power:
                    return "is_statists_in_power";
                case TriggerVar.is_strongest_trade_power:
                    return "is_strongest_trade_power";
                case TriggerVar.is_subject:
                    return "is_subject";
                case TriggerVar.is_subject_of:
                    return "is_subject_of";
                case TriggerVar.is_subject_of_type:
                    return "is_subject_of_type";
                case TriggerVar.is_supporting_independence_of:
                    return "is_supporting_independence_of";
                case TriggerVar.is_territorial_core:
                    return "is_territorial_core";
                case TriggerVar.is_territory:
                    return "is_territory";
                case TriggerVar.is_threat:
                    return "is_threat";
                case TriggerVar.is_trade_league_leader:
                    return "is_trade_league_leader";
                case TriggerVar.is_tribal:
                    return "is_tribal";
                case TriggerVar.is_vassal:
                    return "is_vassal";
                case TriggerVar.is_wasteland:
                    return "is_wasteland";
                case TriggerVar.is_year:
                    return "is_year";
                case TriggerVar.island:
                    return "island";
                case TriggerVar.isolationism:
                    return "isolationism";
                case TriggerVar.janissary_percentage:
                    return "janissary_percentage";
                case TriggerVar.junior_union_with:
                    return "junior_union_with";
                case TriggerVar.karma:
                    return "karma";
                case TriggerVar.knows_country:
                    return "knows_country";
                case TriggerVar.land_forcelimit:
                    return "land_forcelimit";
                case TriggerVar.land_maintenance:
                    return "land_maintenance";
                case TriggerVar.land_morale:
                    return "land_morale";
                case TriggerVar.last_mission:
                    return "last_mission";
                case TriggerVar.legitimacy:
                    return "legitimacy";
                case TriggerVar.legitimacy_equivalent:
                    return "legitimacy_equivalent";
                case TriggerVar.legitimacy_or_horde_unity:
                    return "legitimacy_or_horde_unity";
                case TriggerVar.liberty_desire:
                    return "liberty_desire";
                case TriggerVar.light_ship_fraction:
                    return "light_ship_fraction";
                case TriggerVar.light_ships_in_province:
                    return "light_ships_in_province";
                case TriggerVar.likely_rebels:
                    return "likely_rebels";
                case TriggerVar.local_autonomy:
                    return "local_autonomy";
                case TriggerVar.local_autonomy_above_min:
                    return "local_autonomy_above_min";
                case TriggerVar.luck:
                    return "luck";
                case TriggerVar.march_of:
                    return "march_of";
                case TriggerVar.manpower:
                    return "manpower";
                case TriggerVar.manpower_percentage:
                    return "manpower_percentage";
                case TriggerVar.marriage_with:
                    return "marriage_with";
                case TriggerVar.max_manpower:
                    return "max_manpower";
                case TriggerVar.mercantilism:
                    return "mercantilism";
                case TriggerVar.meritocracy:
                    return "meritocracy";
                case TriggerVar.mil:
                    return "mil";
                case TriggerVar.militarised_society:
                    return "militarised_society";
                case TriggerVar.mil_power:
                    return "mil_power";
                case TriggerVar.mil_tech:
                    return "mil_tech";
                case TriggerVar.mission_completed:
                    return "mission_completed";
                case TriggerVar.monthly_income:
                    return "monthly_income";
                case TriggerVar.monthly_adm:
                    return "monthly_adm";
                case TriggerVar.monthly_dip:
                    return "monthly_dip";
                case TriggerVar.monthly_mil:
                    return "monthly_mil";
                case TriggerVar.months_of_ruling:
                    return "months_of_ruling";
                case TriggerVar.months_since_defection:
                    return "months_since_defection";
                case TriggerVar.nationalism:
                    return "nationalism";
                case TriggerVar.national_focus:
                    return "national_focus";
                case TriggerVar.nation_designer_points:
                    return "nation_designer_points";
                case TriggerVar.native_ferocity:
                    return "native_ferocity";
                case TriggerVar.native_hostileness:
                    return "native_hostileness";
                case TriggerVar.native_policy:
                    return "native_policy";
                case TriggerVar.native_size:
                    return "native_size";
                case TriggerVar.naval_forcelimit:
                    return "naval_forcelimit";
                case TriggerVar.naval_maintenance:
                    return "naval_maintenance";
                case TriggerVar.naval_morale:
                    return "naval_morale";
                case TriggerVar.navy_size:
                    return "navy_size";
                case TriggerVar.navy_size_percentage:
                    return "navy_size_percentage";
                case TriggerVar.navy_tradition:
                    return "navy_tradition";
                case TriggerVar.normal_or_historical_nations:
                    return "normal_or_historical_nations";
                case TriggerVar.normal_province_values:
                    return "normal_province_values";
                case TriggerVar.num_accepted_cultures:
                    return "num_accepted_cultures";
                case TriggerVar.num_free_building_slots:
                    return "num_free_building_slots";
                case TriggerVar.num_of_active_blessings:
                    return "num_of_active_blessings";
                case TriggerVar.num_of_admirals:
                    return "num_of_admirals";
                case TriggerVar.num_of_admirals_with_traits:
                    return "num_of_admirals_with_traits";
                case TriggerVar.num_of_allies:
                    return "num_of_allies";
                case TriggerVar.num_of_artillery:
                    return "num_of_artillery";
                case TriggerVar.num_of_aspects:
                    return "num_of_aspects";
                case TriggerVar.num_of_banners:
                    return "num_of_banners";
                case TriggerVar.num_of_buildings_in_province:
                    return "num_of_buildings_in_province";
                case TriggerVar.num_of_captured_ships_with_boarding_doctrine:
                    return "num_of_captured_ships_with_boarding_doctrine";
                case TriggerVar.num_of_centers_of_trade:
                    return "num_of_centers_of_trade";
                case TriggerVar.num_of_cardinals:
                    return "num_of_cardinals";
                case TriggerVar.num_of_cavalry:
                    return "num_of_cavalry";
                case TriggerVar.num_of_cawa:
                    return "num_of_cawa";
                case TriggerVar.num_of_cities:
                    return "num_of_cities";
                case TriggerVar.num_of_coalition_members:
                    return "num_of_coalition_members";
                case TriggerVar.num_of_colonies:
                    return "num_of_colonies";
                case TriggerVar.num_of_colonists:
                    return "num_of_colonists";
                case TriggerVar.num_of_conquistadors:
                    return "num_of_conquistadors";
                case TriggerVar.num_of_consorts:
                    return "num_of_consorts";
                case TriggerVar.num_of_continents:
                    return "num_of_continents";
                case TriggerVar.num_of_cossacks:
                    return "num_of_cossacks";
                case TriggerVar.num_of_custom_nations:
                    return "num_of_custom_nations";
                case TriggerVar.num_of_diplomatic_relations:
                    return "num_of_diplomatic_relations";
                case TriggerVar.num_of_diplomats:
                    return "num_of_diplomats";
                case TriggerVar.num_of_electors:
                    return "num_of_electors";
                case TriggerVar.num_of_explorers:
                    return "num_of_explorers";
                case TriggerVar.num_of_foreign_hre_provinces:
                    return "num_of_foreign_hre_provinces";
                case TriggerVar.num_of_free_diplomatic_relations:
                    return "num_of_free_diplomatic_relations";
                case TriggerVar.num_of_galley:
                    return "num_of_galley";
                case TriggerVar.num_of_generals:
                    return "num_of_generals";
                case TriggerVar.num_of_generals_with_traits:
                    return "num_of_generals_with_traits";
                case TriggerVar.num_of_harmonized:
                    return "num_of_harmonized";
                case TriggerVar.num_of_heavy_ship:
                    return "num_of_heavy_ship";
                case TriggerVar.num_of_infantry:
                    return "num_of_infantry";
                case TriggerVar.num_of_light_ship:
                    return "num_of_light_ship";
                case TriggerVar.num_of_loans:
                    return "num_of_loans";
                case TriggerVar.num_of_marches:
                    return "num_of_marches";
                case TriggerVar.num_of_marines:
                    return "num_of_marines";
                case TriggerVar.num_of_mercenaries:
                    return "num_of_mercenaries";
                case TriggerVar.num_of_merchants:
                    return "num_of_merchants";
                case TriggerVar.num_of_missionaries:
                    return "num_of_missionaries";
                case TriggerVar.num_of_owned_and_controlled_institutions:
                    return "num_of_owned_and_controlled_institutions";
                case TriggerVar.num_of_ports:
                    return "num_of_ports";
                case TriggerVar.num_of_ports_blockading:
                    return "num_of_ports_blockading";
                case TriggerVar.num_of_powerful_estates:
                    return "num_of_powerful_estates";
                case TriggerVar.num_of_protectorates:
                    return "num_of_protectorates";
                case TriggerVar.num_of_provinces_in_states:
                    return "num_of_provinces_in_states";
                case TriggerVar.num_of_provinces_in_territories:
                    return "num_of_provinces_in_territories";
                case TriggerVar.num_of_rajput:
                    return "num_of_rajput";
                case TriggerVar.num_of_rebel_armies:
                    return "num_of_rebel_armies";
                case TriggerVar.num_of_rebel_controlled_provinces:
                    return "num_of_rebel_controlled_provinces";
                case TriggerVar.num_of_revolts:
                    return "num_of_revolts";
                case TriggerVar.num_of_royal_marriages:
                    return "num_of_royal_marriages";
                case TriggerVar.num_of_ruler_traits:
                    return "num_of_ruler_traits";
                case TriggerVar.num_of_states:
                    return "num_of_states";
                case TriggerVar.num_of_streltsy:
                    return "num_of_streltsy";
                case TriggerVar.num_of_strong_trade_companies:
                    return "num_of_strong_trade_companies";
                case TriggerVar.num_of_subjects:
                    return "num_of_subjects";
                case TriggerVar.num_of_territories:
                    return "num_of_territories";
                case TriggerVar.num_of_times_improved:
                    return "num_of_times_improved";
                case TriggerVar.num_of_times_improved_by_owner:
                    return "num_of_times_improved_by_owner";
                case TriggerVar.num_of_times_used_pillage_capital:
                    return "num_of_times_used_pillage_capital";
                case TriggerVar.num_of_times_used_transfer_development:
                    return "num_of_times_used_transfer_development";
                case TriggerVar.num_of_total_ports:
                    return "num_of_total_ports";
                case TriggerVar.num_of_trade_companies:
                    return "num_of_trade_companies";
                case TriggerVar.num_of_trade_embargos:
                    return "num_of_trade_embargos";
                case TriggerVar.num_of_trading_bonuses:
                    return "num_of_trading_bonuses";
                case TriggerVar.num_of_transport:
                    return "num_of_transport";
                case TriggerVar.num_of_trusted_allies:
                    return "num_of_trusted_allies";
                case TriggerVar.num_of_unions:
                    return "num_of_unions";
                case TriggerVar.num_of_unlocked_cults:
                    return "num_of_unlocked_cults";
                case TriggerVar.num_of_war_reparations:
                    return "num_of_war_reparations";
                case TriggerVar.num_ships_privateering:
                    return "num_ships_privateering";
                case TriggerVar.offensive_war_with:
                    return "offensive_war_with";
                case TriggerVar.overextension_percentage:
                    return "overextension_percentage";
                case TriggerVar.overlord_of:
                    return "overlord_of";
                case TriggerVar.overseas_provinces_percentage:
                    return "overseas_provinces_percentage";
                case TriggerVar.owned_by:
                    return "owned_by";
                case TriggerVar.owns:
                    return "owns";
                case TriggerVar.owns_core_province:
                    return "owns_core_province";
                case TriggerVar.owns_or_non_sovereign_subject_of:
                    return "owns_or_non_sovereign_subject_of";
                case TriggerVar.owns_or_subject_of:
                    return "owns_or_subject_of";
                case TriggerVar.papacy_active:
                    return "papacy_active";
                case TriggerVar.papal_influence:
                    return "papal_influence";
                case TriggerVar.patriarch_authority:
                    return "patriarch_authority";
                case TriggerVar.percentage_backing_issue:
                    return "percentage_backing_issue";
                case TriggerVar.personality:
                    return "personality";
                case TriggerVar.piety:
                    return "piety";
                case TriggerVar.preferred_emperor:
                    return "preferred_emperor";
                case TriggerVar.prestige:
                    return "prestige";
                case TriggerVar.previous_owner:
                    return "previous_owner";
                case TriggerVar.power_projection:
                    return "power_projection";
                case TriggerVar.primary_culture:
                    return "primary_culture";
                case TriggerVar.primitives:
                    return "primitives";
                case TriggerVar.production_efficiency:
                    return "production_efficiency";
                case TriggerVar.production_income_percentage:
                    return "production_income_percentage";
                case TriggerVar.province_id:
                    return "province_id";
                case TriggerVar.province_is_on_an_island:
                    return "province_is_on_an_island";
                case TriggerVar.province_getting_expelled_minority:
                    return "province_getting_expelled_minority";
                case TriggerVar.province_group:
                    return "province_group";
                case TriggerVar.province_size:
                    return "province_size";
                case TriggerVar.province_trade_power:
                    return "province_trade_power";
                case TriggerVar.provinces_on_capital_continent_of:
                    return "provinces_on_capital_continent_of";
                case TriggerVar.pure_unrest:
                    return "pure_unrest";
                case TriggerVar.range:
                    return "range";
                case TriggerVar.real_day_of_year:
                    return "real_day_of_year";
                case TriggerVar.real_month_of_year:
                    return "real_month_of_year";
                case TriggerVar.reform_desire:
                    return "reform_desire";
                case TriggerVar.receives_military_access_from:
                    return "receives_military_access_from";
                case TriggerVar.receives_fleet_basing_rights_from:
                    return "receives_fleet_basing_rights_from";
                case TriggerVar.reform_level:
                    return "reform_level";
                case TriggerVar.region:
                    return "region";
                case TriggerVar.religion:
                    return "religion";
                case TriggerVar.religion_group:
                    return "religion_group";
                case TriggerVar.religious_unity:
                    return "religious_unity";
                case TriggerVar.republican_tradition:
                    return "republican_tradition";
                case TriggerVar.revanchism:
                    return "revanchism";
                case TriggerVar.revolt_percentage:
                    return "revolt_percentage";
                case TriggerVar.revolution_target_exists:
                    return "revolution_target_exists";
                case TriggerVar.ruler_age:
                    return "ruler_age";
                case TriggerVar.ruler_consort_marriage_length:
                    return "ruler_consort_marriage_length";
                case TriggerVar.ruler_culture:
                    return "ruler_culture";
                case TriggerVar.ruler_has_personality:
                    return "ruler_has_personality";
                case TriggerVar.ruler_is_foreigner:
                    return "ruler_is_foreigner";
                case TriggerVar.ruler_religion:
                    return "ruler_religion";
                case TriggerVar.sailors:
                    return "sailors";
                case TriggerVar.sailors_percentage:
                    return "sailors_percentage";
                case TriggerVar.max_sailors:
                    return "max_sailors";
                case TriggerVar.same_continent:
                    return "same_continent";
                case TriggerVar.secondary_religion:
                    return "secondary_religion";
                case TriggerVar.senior_union_with:
                    return "senior_union_with";
                case TriggerVar.sieged_by:
                    return "sieged_by";
                case TriggerVar.splendor:
                    return "splendor";
                case TriggerVar.stability:
                    return "stability";
                case TriggerVar.start_date:
                    return "start_date";
                case TriggerVar.started_in:
                    return "started_in";
                case TriggerVar.statists_vs_orangists:
                    return "statists_vs_orangists";
                case TriggerVar.subsidised_percent_amount:
                    return "subsidised_percent_amount";
                case TriggerVar.succession_claim:
                    return "succession_claim";
                case TriggerVar.superregion:
                    return "superregion";
                case TriggerVar.tag:
                    return "tag";
                case TriggerVar.tariff_value:
                    return "tariff_value";
                case TriggerVar.tax_income_percentage:
                    return "tax_income_percentage";
                case TriggerVar.tech_difference:
                    return "tech_difference";
                case TriggerVar.technology_group:
                    return "technology_group";
                case TriggerVar.tolerance_to_this:
                    return "tolerance_to_this";
                case TriggerVar.total_base_tax:
                    return "total_base_tax";
                case TriggerVar.total_development:
                    return "total_development";
                case TriggerVar.total_number_of_cardinals:
                    return "total_number_of_cardinals";
                case TriggerVar.trade_league_embargoed_by:
                    return "trade_league_embargoed_by";
                case TriggerVar.total_own_and_non_tributary_subject_development:
                    return "total_own_and_non_tributary_subject_development";
                case TriggerVar.transports_in_province:
                    return "transports_in_province";
                case TriggerVar.trade_company_region:
                    return "trade_company_region";
                case TriggerVar.trade_company_size:
                    return "trade_company_size";
                case TriggerVar.trade_efficiency:
                    return "trade_efficiency";
                case TriggerVar.trade_embargoing:
                    return "trade_embargoing";
                case TriggerVar.trade_embargo_by:
                    return "trade_embargo_by";
                case TriggerVar.trade_goods:
                    return "trade_goods";
                case TriggerVar.trade_income_percentage:
                    return "trade_income_percentage";
                case TriggerVar.trade_node_value:
                    return "trade_node_value";
                case TriggerVar.trade_range:
                    return "trade_range";
                case TriggerVar.transport_fraction:
                    return "transport_fraction";
                case TriggerVar.treasury:
                    return "treasury";
                case TriggerVar.tribal_allegiance:
                    return "tribal_allegiance";
                case TriggerVar.tribal_development:
                    return "tribal_development";
                case TriggerVar.truce_with:
                    return "truce_with";
                case TriggerVar.trust:
                    return "trust";
                case TriggerVar.unit_has_leader:
                    return "unit_has_leader";
                case TriggerVar.unit_in_battle:
                    return "unit_in_battle";
                case TriggerVar.unit_in_siege:
                    return "unit_in_siege";
                case TriggerVar.units_in_province:
                    return "units_in_province";
                case TriggerVar.unit_type:
                    return "unit_type";
                case TriggerVar.unrest:
                    return "unrest";
                case TriggerVar.uses_authority:
                    return "uses_authority";
                case TriggerVar.uses_church_aspects:
                    return "uses_church_aspects";
                case TriggerVar.uses_blessings:
                    return "uses_blessings";
                case TriggerVar.uses_cults:
                    return "uses_cults";
                case TriggerVar.uses_devotion:
                    return "uses_devotion";
                case TriggerVar.uses_doom:
                    return "uses_doom";
                case TriggerVar.uses_fervor:
                    return "uses_fervor";
                case TriggerVar.uses_isolationism:
                    return "uses_isolationism";
                case TriggerVar.uses_karma:
                    return "uses_karma";
                case TriggerVar.uses_papacy:
                    return "uses_papacy";
                case TriggerVar.uses_patriarch_authority:
                    return "uses_patriarch_authority";
                case TriggerVar.uses_personal_deities:
                    return "uses_personal_deities";
                case TriggerVar.uses_piety:
                    return "uses_piety";
                case TriggerVar.uses_religious_icons:
                    return "uses_religious_icons";
                case TriggerVar.uses_syncretic_faiths:
                    return "uses_syncretic_faiths";
                case TriggerVar.vassal_of:
                    return "vassal_of";
                case TriggerVar.war_exhaustion:
                    return "war_exhaustion";
                case TriggerVar.war_score:
                    return "war_score";
                case TriggerVar.war_with:
                    return "war_with";
                case TriggerVar.was_player:
                    return "was_player";
                case TriggerVar.was_tag:
                    return "was_tag";
                case TriggerVar.will_back_next_reform:
                    return "will_back_next_reform";
                case TriggerVar.yearly_corruption_increase:
                    return "yearly_corruption_increase";
                case TriggerVar.years_of_income:
                    return "years_of_income";
            }
        }
        public static Value GetValues(TriggerVar Variable)
        {
            switch (Variable)
            {
                default:
                case TriggerVar.NamedAdvisor:
                case TriggerVar.NamedBuilding:
                case TriggerVar.NamedIdeaGroup:
                case TriggerVar.NamedInstitution:
                case TriggerVar.NamedReligion:
                case TriggerVar.NamedSubjectType:
                case TriggerVar.NamedTradeGood:
                case TriggerVar.absolutism:
                case TriggerVar.adm_tech:
                case TriggerVar.average_autonomy:
                case TriggerVar.average_autonomy_above_min:
                case TriggerVar.average_effective_unrest:
                case TriggerVar.average_home_autonomy:
                case TriggerVar.average_unrest:
                case TriggerVar.base_manpower:
                case TriggerVar.base_production:
                case TriggerVar.base_tax:
                case TriggerVar.blockade:
                case TriggerVar.province_has_center_of_trade_of_level:
                case TriggerVar.colony:
                case TriggerVar.colonysize:
                case TriggerVar.consort_adm:
                case TriggerVar.consort_age:
                case TriggerVar.consort_dip:
                case TriggerVar.consort_mil:
                case TriggerVar.current_size_of_parliament:
                case TriggerVar.devastation:
                    return Value.Integer;


                case TriggerVar.accepted_culture:
                    return Value.IdentifierORProvinceScope;


                case TriggerVar.active_major_mission:
                    return Value.MissionIdentifier;

                case TriggerVar.adm:
                case TriggerVar.adm_power:
                case TriggerVar.army_size:
                case TriggerVar.army_tradition:
                case TriggerVar.artillery_in_province:
                case TriggerVar.authority:
                case TriggerVar.cavalry_in_province:
                case TriggerVar.church_power:
                    return Value.IntegerORTagORScope;

                case TriggerVar.advisor:
                case TriggerVar.advisor_exists:
                    return Value.AdvisorID;


                case TriggerVar.ai:
                case TriggerVar.allows_female_emperor:
                case TriggerVar.always:
                case TriggerVar.at_war_with_religious_enemy:
                case TriggerVar.can_create_vassals:
                case TriggerVar.can_heir_be_child_of_consort:
                case TriggerVar.can_migrate:
                    return Value.Boolean;


                case TriggerVar.alliance_with:
                case TriggerVar.can_justify_trade_conflict:
                case TriggerVar.coalition_target:
                case TriggerVar.controlled_by:
                case TriggerVar.core_claim:
                case TriggerVar.country_or_non_sovereign_subject_holds:
                case TriggerVar.country_or_subject_holds:
                case TriggerVar.defensive_war_with:
                    return Value.TagORScope;
              
                case TriggerVar.area:
                    return Value.AreaIdentifier;

                case TriggerVar.army_size_percentage:
                case TriggerVar.army_professionalism:
                case TriggerVar.artillery_fraction:
                case TriggerVar.cavalry_fraction:
                case TriggerVar.construction_progress:
                case TriggerVar.core_percentage:
                case TriggerVar.corruption:
                case TriggerVar.current_income_balance:
                case TriggerVar.current_institution:
                case TriggerVar.current_institution_growth:
                    return Value.Float;

             
                case TriggerVar.can_be_overlord:
                    return Value.SubjectIdentifier;


                case TriggerVar.can_build:
                    return Value.BuildingIdentifier;

                case TriggerVar.can_spawn_rebels:
                    return Value.RebelIdentifier;


                case TriggerVar.capital:
                case TriggerVar.controls:
                    return Value.ProvinceID;

                case TriggerVar.colonial_claim_by_anyone_of_religion:
                    return Value.ReligionIdentifier;

                case TriggerVar.colonial_region:
                    return Value.ColonialRegionIdentifier;

                case TriggerVar.colony_claim:
                case TriggerVar.culture_group_claim:
                    return Value.Tag;

                case TriggerVar.consort_culture:
                case TriggerVar.culture:
                case TriggerVar.culture_group:
                    return Value.CultureIdentifierORScope;

                case TriggerVar.consort_has_personality:
                    return Value.PersonalityIdentifier;

                case TriggerVar.consort_religion:
                    return Value.ReligionIdentifierORScope;


                case TriggerVar.continent:
                    return Value.ContinentIdetifierORTagORScopeORProvinceID;

                 case TriggerVar.council_position:
                    return Value.CouncilPositionIdentifier;

                case TriggerVar.crown_land_share:
                    return Value.IntegerOREstateIdentifier;

                case TriggerVar.current_age:
                    return Value.AgeIdentifier;

                case TriggerVar.current_bribe:
                    return Value.BribeIdentifier;

                case TriggerVar.current_debate:
                    return Value.DebateIdentifier;

                case TriggerVar.current_icon:
                    return Value.IconIdentifier;

                case TriggerVar.development:
                    return Value.IntegerORScope;
                case TriggerVar.development_of_overlord_fraction:
                    return Value.Float;
                case TriggerVar.devotion:
                    return Value.IntegerORTagORScope;
                case TriggerVar.dip:
                    return Value.IntegerORTagORScope;
                case TriggerVar.diplomatic_reputation:
                    return Value.IntegerORTagORScope;
                case TriggerVar.dip_power:
                    return Value.IntegerORTagORScope;
                case TriggerVar.dip_tech:
                    return Value.Integer;
                case TriggerVar.dominant_culture:
                    return Value.CultureIdentifierORCapital;
                case TriggerVar.dominant_religion:
                    return Value.ReligionIdentifierORCapital;
                case TriggerVar.doom:
                    return Value.IntegerORTagORScope;
                case TriggerVar.dynasty:
                    return Value.StringORTagORScope;
                case TriggerVar.empire_of_china_reform_passed:
                    return Value.ChinaReformIdentifier;
                case TriggerVar.estate_led_regency_influence:
                    return Value.Integer;
                case TriggerVar.estate_led_regency_loyalty:
                    return Value.Integer;
                case TriggerVar.exiled_same_dynasty_as_current:
                    return Value.Boolean;
                case TriggerVar.exists:
                    return Value.TagORBoolean;
                case TriggerVar.faction_in_power:
                    return Value.FactionIdentifier;
                case TriggerVar.federation_size:
                    return Value.Integer;
                case TriggerVar.fervor:
                    return Value.Integer;
                case TriggerVar.fort_level:
                    return Value.Integer;
                case TriggerVar.full_idea_group:
                    return Value.IdeaGroupIdentifier;
                case TriggerVar.galley_fraction:
                    return Value.Float;
                case TriggerVar.galleys_in_province:
                    return Value.IntegerORTagORScope;
                case TriggerVar.garrison:
                    return Value.Integer;
                case TriggerVar.gives_military_access_to:
                    return Value.TagORScope;
                case TriggerVar.gives_fleet_basing_rights_to:
                    return Value.TagORScope;
                case TriggerVar.gold_income:
                    return Value.Float;
                case TriggerVar.gold_income_percentage:
                    return Value.Float;
                case TriggerVar.government:
                    return Value.GovernmentIdentifier;
                case TriggerVar.government_rank:
                    return Value.Integer;
                case TriggerVar.grown_by_development:
                    return Value.Integer;
                case TriggerVar.grown_by_states:
                    return Value.Integer;
                case TriggerVar.great_power_rank:
                    return Value.Integer;
                case TriggerVar.guaranteed_by:
                    return Value.TagORScope;
                case TriggerVar.had_recent_war:
                    return Value.Integer;
                case TriggerVar.harmonization_progress:
                    return Value.Integer;
                case TriggerVar.harmony:
                    return Value.Integer;
                case TriggerVar.has_active_debate:
                    return Value.Boolean;
                case TriggerVar.has_active_fervor:
                    return Value.Boolean;
                case TriggerVar.has_active_policy:
                    return Value.PolicyIdentfier;
                case TriggerVar.has_active_triggered_province_modifier:
                    return Value.ModifierIdentifier;
                case TriggerVar.has_adopted_cult:
                    return Value.CultIdentifier;
                case TriggerVar.has_advisor:
                    return Value.Boolean;
                case TriggerVar.has_age_ability:
                    return Value.AgeAbilityIdentifier;
                case TriggerVar.has_any_disaster:
                    return Value.Boolean;
                case TriggerVar.has_border_with_religious_enemy:
                    return Value.Boolean;
                case TriggerVar.has_building:
                    return Value.BuildingIdentifier;
                case TriggerVar.has_cardinal:
                    return Value.Boolean;
                case TriggerVar.has_casus_belli_against:
                    return Value.TagORScope;
                case TriggerVar.has_center_of_trade_of_level:
                    return Value.Integer;
                case TriggerVar.has_changed_nation:
                    return Value.Boolean;
                case TriggerVar.has_church_aspect:
                    return Value.ChurchAspectIdentifier;
                case TriggerVar.has_climate:
                    return Value.ClimateIdentifier;
                case TriggerVar.has_colonial_parent:
                    return Value.Tag;
                case TriggerVar.has_colonist:
                    return Value.Boolean;
                case TriggerVar.has_commanding_three_star:
                    return Value.Boolean;
                case TriggerVar.has_consort:
                    return Value.Boolean;
                case TriggerVar.has_consort_flag:
                    return Value.ConsortFlagIdentifier;
                case TriggerVar.has_consort_regency:
                    return Value.Boolean;
                case TriggerVar.has_construction:
                    return Value.Construction;
                case TriggerVar.has_country_flag:
                    return Value.CountryFlagIdentifier;
                case TriggerVar.has_country_modifier:
                    return Value.ModifierIdentifier;
                case TriggerVar.has_custom_ideas:
                    return Value.Boolean;
                case TriggerVar.has_disaster:
                    return Value.DisasterIdentfier;
                case TriggerVar.has_discovered:
                    return Value.ProvinceIDORTagORScope;
                case TriggerVar.has_dlc:
                    return Value.DLCIdentifier;
                case TriggerVar.has_divert_trade:
                    return Value.Boolean;
                case TriggerVar.has_embargo_rivals:
                    return Value.Boolean;
                case TriggerVar.has_empty_adjacent_province:
                    return Value.Boolean;
                case TriggerVar.has_estate:
                    return Value.EstateIdentifier;
                case TriggerVar.has_estate_province:
                    return Value.BooleanOREstateIdentifier;
                case TriggerVar.has_estate_loan:
                    return Value.Boolean;
                case TriggerVar.has_estate_privilege:
                    return Value.PrivilegeIdentifier;
                case TriggerVar.has_faction:
                    return Value.FactionIdentifier;
                case TriggerVar.has_factions:
                    return Value.Boolean;
                case TriggerVar.has_female_consort:
                    return Value.Boolean;
                case TriggerVar.has_female_heir:
                    return Value.Boolean;
                case TriggerVar.has_first_revolution_started:
                    return Value.Boolean;
                case TriggerVar.has_flagship:
                    return Value.Boolean;
                case TriggerVar.has_foreign_consort:
                    return Value.Boolean;
                case TriggerVar.has_foreign_heir:
                    return Value.Boolean;
                case TriggerVar.has_friendly_reformation_center:
                    return Value.Boolean;
                case TriggerVar.has_game_started:
                    return Value.Boolean;
                case TriggerVar.has_given_consort_to:
                    return Value.TagORScope;
                case TriggerVar.has_guaranteed:
                    return Value.TagORScope;
                case TriggerVar.has_global_flag:
                    return Value.GlobalFlagIdentifier;
                case TriggerVar.has_government_mechanic:
                    return Value.GovernmentMechanicIdentfier;
                case TriggerVar.has_government_power:
                    return Value.GovernmentPowerIdentifier;
                case TriggerVar.has_had_golden_age:
                    return Value.Boolean;
                case TriggerVar.has_harmonized_with:
                    return Value.ReligionORReligionGroupORScope;
                case TriggerVar.has_harsh_treatment:
                    return Value.Boolean;
                case TriggerVar.has_heir:
                    return Value.StringORBoolean;
                case TriggerVar.has_heir_flag:
                    return Value.HeirFlagIdentifier;
                case TriggerVar.has_heir_leader_from:
                    return Value.TagORScope;
                case TriggerVar.has_hostile_reformation_center:
                    return Value.Boolean;
                case TriggerVar.has_idea:
                    return Value.IdeaIdentifier;
                case TriggerVar.has_idea_group:
                    return Value.IdeaGroupIdentifier;
                case TriggerVar.has_influencing_fort:
                    return Value.Boolean;
                case TriggerVar.has_institution:
                    return Value.InstitutionIdentifier;
                case TriggerVar.has_latent_trade_goods:
                    return Value.TradeGoodIdentifier;
                case TriggerVar.has_leader:
                    return Value.String;
                case TriggerVar.has_matching_religion:
                    return Value.ReligionIdentifierORScope;
                case TriggerVar.has_merchant:
                    return Value.TagORScope;
                case TriggerVar.has_mission:
                    return Value.MissionIdentifier;
                case TriggerVar.has_missionary:
                    return Value.Boolean;
                case TriggerVar.has_monsoon:
                    return Value.MonsoonIdentifier;
                case TriggerVar.has_most_province_trade_power:
                    return Value.TagORScope;
                case TriggerVar.has_new_dynasty:
                    return Value.Boolean;
                case TriggerVar.has_owner_accepted_culture:
                    return Value.Boolean;
                case TriggerVar.has_owner_culture:
                    return Value.Boolean;
                case TriggerVar.has_owner_religion:
                    return Value.Boolean;
                case TriggerVar.has_pasha:
                    return Value.Boolean;
                case TriggerVar.has_parliament:
                    return Value.Boolean;
                case TriggerVar.has_personal_deity:
                    return Value.PersonalDietyIdentifier;
                case TriggerVar.has_pillaged_capital_against:
                    return Value.TagORScope;
                case TriggerVar.has_port:
                    return Value.Boolean;
                case TriggerVar.has_privateers:
                    return Value.Boolean;
                case TriggerVar.has_promote_investments:
                    return Value.TradeCompanyIdentifier;
                case TriggerVar.has_province_flag:
                    return Value.ProvinceFlagIdentifier;
                case TriggerVar.has_province_modifier:
                    return Value.ModifierIdentifier;
                case TriggerVar.has_rebel_faction:
                    return Value.RebelIdentifier;
                case TriggerVar.has_regency:
                    return Value.Boolean;
                case TriggerVar.has_reform:
                    return Value.ReformIdentifier;
                case TriggerVar.government_reform_progress:
                    return Value.Float;
                case TriggerVar.has_removed_fow:
                    return Value.Boolean;
                case TriggerVar.has_revolution_in_province:
                    return Value.Boolean;
                case TriggerVar.has_ruler:
                    return Value.String;
                case TriggerVar.has_ruler_flag:
                    return Value.RulerFlagIdentifier;
                case TriggerVar.has_ruler_leader_from:
                    return Value.TagORScope;
                case TriggerVar.has_ruler_modifier:
                    return Value.ModifierIdentifier;
                case TriggerVar.has_saved_event_target:
                    return Value.EventTargetIdentifier;
                case TriggerVar.has_scutage:
                    return Value.Boolean;
                case TriggerVar.has_seat_in_parliament:
                    return Value.Boolean;
                case TriggerVar.has_secondary_religion:
                    return Value.Boolean;
                case TriggerVar.has_send_officers:
                    return Value.Boolean;
                case TriggerVar.has_siege:
                    return Value.Boolean;
                case TriggerVar.has_spawned_rebels:
                    return Value.RebelIdentifier;
                case TriggerVar.has_spawned_supported_rebels:
                    return Value.Scope;
                case TriggerVar.has_state_edict:
                    return Value.StateEdictIdentifier;
                case TriggerVar.has_state_patriach:
                    return Value.Boolean;
                case TriggerVar.has_subsidize_armies:
                    return Value.Boolean;
                case TriggerVar.has_supply_depot:
                    return Value.TagORScope;
                case TriggerVar.has_support_loyalists:
                    return Value.Boolean;
                case TriggerVar.has_subject_of_type:
                    return Value.SubjectIdentifier;
                case TriggerVar.has_switched_nation:
                    return Value.Boolean;
                case TriggerVar.has_terrain:
                    return Value.TerrainIdentifier;
                case TriggerVar.has_trader:
                    return Value.TagORScope;
                case TriggerVar.has_truce:
                    return Value.Boolean;
                case TriggerVar.has_unembraced_institution:
                    return Value.InstitutionIdentifier;
                case TriggerVar.has_unified_culture_group:
                    return Value.Boolean;
                case TriggerVar.has_unit_type:
                    return Value.UnitTypeIdentifier;
                case TriggerVar.has_unlocked_cult:
                    return Value.UnitTypeIdentifier;
                case TriggerVar.has_wartaxes:
                    return Value.Boolean;
                case TriggerVar.has_winter:
                    return Value.WinterIdentifier;
                case TriggerVar.have_had_reform:
                    return Value.ReformIdentifier;
                case TriggerVar.heavy_ship_fraction:
                    return Value.Float;
                case TriggerVar.heavy_ships_in_province:
                    return Value.IntegerORTagORScope;
                case TriggerVar.heir_adm:
                    return Value.Integer;
                case TriggerVar.heir_age:
                    return Value.Integer;
                case TriggerVar.heir_dip:
                    return Value.Integer;
                case TriggerVar.heir_claim:
                    return Value.Integer;
                case TriggerVar.heir_culture:
                    return Value.CultureIdentifierORScope;
                case TriggerVar.heir_has_consort_dynasty:
                    return Value.Boolean;
                case TriggerVar.heir_has_personality:
                    return Value.PersonalityIdentifier;
                case TriggerVar.heir_has_ruler_dynasty:
                    return Value.Boolean;
                case TriggerVar.heir_mil:
                    return Value.Integer;
                case TriggerVar.heir_nationality:
                    return Value.TagORScope;
                case TriggerVar.heir_religion:
                    return Value.ReligionIdentifierORScope;
                case TriggerVar.higher_development_than:
                    return Value.ProvinceIDORScope;
                case TriggerVar.highest_value_trade_node:
                    return Value.Boolean;
                case TriggerVar.historical_friend_with:
                    return Value.TagORScope;
                case TriggerVar.historical_rival_with:
                    return Value.TagORScope;
                case TriggerVar.holy_order:
                    return Value.HolyOrderIdentifier;
                case TriggerVar.horde_unity:
                    return Value.IntegerORTagORScope;
                case TriggerVar.hre_heretic_religion:
                    return Value.ReligionIdentifierORScope;
                case TriggerVar.hre_leagues_enabled:
                    return Value.Boolean;
                case TriggerVar.hre_reform_passed:
                    return Value.HREReformIdentifier;
                case TriggerVar.hre_religion:
                    return Value.ReligionIdentifierORScope;
                case TriggerVar.hre_religion_locked:
                    return Value.Boolean;
                case TriggerVar.hre_religion_treaty:
                    return Value.Boolean;
                case TriggerVar.hre_size:
                    return Value.Integer;
                case TriggerVar.imperial_influence:
                    return Value.Integer;
                case TriggerVar.imperial_mandate:
                    return Value.Integer;
                case TriggerVar.in_golden_age:
                    return Value.Boolean;
                case TriggerVar.infantry_fraction:
                    return Value.Float;
                case TriggerVar.infantry_in_province:
                    return Value.IntegerORTagORScope;
                case TriggerVar.inflation:
                    return Value.FloatORTagORScope;
                case TriggerVar.innovativeness:
                    return Value.Integer;
                case TriggerVar.invested_papal_influence:
                    return Value.Integer;
                case TriggerVar.in_league:
                    return Value.LeagueIdentifier;
                case TriggerVar.ironman:
                    return Value.Boolean;
                case TriggerVar.is_advisor_employed:
                    return Value.AdvisorID;
                case TriggerVar.is_all_concessions_in_council_taken:
                    return Value.Boolean;
                case TriggerVar.is_at_war:
                    return Value.Boolean;
                case TriggerVar.is_backing_current_issue:
                    return Value.Boolean;
                case TriggerVar.is_bankrupt:
                    return Value.Boolean;
                case TriggerVar.is_blockaded:
                    return Value.Boolean;
                case TriggerVar.is_blockaded_by:
                    return Value.TagORScope;
                case TriggerVar.is_capital:
                    return Value.Boolean;
                case TriggerVar.is_capital_of:
                    return Value.TagORScope;
                case TriggerVar.is_city:
                    return Value.Boolean;
                case TriggerVar.is_claim:
                    return Value.ProvinceIDORTagORScope;
                case TriggerVar.is_client_nation:
                    return Value.Boolean;
                case TriggerVar.is_client_nation_of:
                    return Value.TagORScope;
                case TriggerVar.is_colonial_nation:
                    return Value.Boolean;
                case TriggerVar.is_colonial_nation_of:
                    return Value.TagORScope;
                case TriggerVar.is_colony:
                    return Value.Boolean;
                case TriggerVar.is_core:
                    return Value.ProvinceIDORTagORScope;
                case TriggerVar.is_council_enabled:
                    return Value.Boolean;
                case TriggerVar.is_crusade_target:
                    return Value.Boolean;
                case TriggerVar.is_defender_of_faith:
                    return Value.Boolean;
                case TriggerVar.is_defender_of_faith_of_tier:
                    return Value.Integer;
                case TriggerVar.is_dynamic_tag:
                    return Value.Boolean;
                case TriggerVar.is_elector:
                    return Value.Boolean;
                case TriggerVar.is_emperor:
                    return Value.Boolean;
                case TriggerVar.is_emperor_of_china:
                    return Value.Boolean;
                case TriggerVar.is_empty:
                    return Value.Boolean;
                case TriggerVar.is_enemy:
                    return Value.Boolean;
                case TriggerVar.is_excommunicated:
                    return Value.Boolean;
                case TriggerVar.is_federation_leader:
                    return Value.Boolean;
                case TriggerVar.is_female:
                    return Value.Boolean;
                case TriggerVar.is_force_converted:
                    return Value.Boolean;
                case TriggerVar.is_former_colonial_nation:
                    return Value.Boolean;
                case TriggerVar.is_foreign_claim:
                    return Value.Boolean;
                case TriggerVar.is_great_power:
                    return Value.Boolean;
                case TriggerVar.is_harmonizing_with:
                    return Value.ReligionORReligionGroupORScope;
                case TriggerVar.is_heir_leader:
                    return Value.Boolean;
                case TriggerVar.is_hegemon:
                    return Value.Boolean;
                case TriggerVar.is_hegemon_of_type:
                    return Value.HegemonTypeIdentifier;
                case TriggerVar.is_imperial_ban_allowed:
                    return Value.Boolean;
                case TriggerVar.is_incident_active:
                    return Value.IncidentIdentifierORBooleanORAnyORNone;
                case TriggerVar.is_incident_happened:
                    return Value.IncidentIdentifier;
                case TriggerVar.is_incident_possible:
                    return Value.IncidentIdentifier;
                case TriggerVar.is_incident_potential:
                    return Value.IncidentIdentifier;
                case TriggerVar.is_institution_enabled:
                    return Value.IncidentIdentifier;
                case TriggerVar.is_institution_origin:
                    return Value.InstitutionIdentifier;
                case TriggerVar.is_in_capital_area:
                    return Value.Boolean;
                case TriggerVar.is_in_coalition:
                    return Value.Boolean;
                case TriggerVar.is_in_coalition_war:
                    return Value.Boolean;
                case TriggerVar.is_in_deficit:
                    return Value.Boolean;
                case TriggerVar.is_in_extended_regency:
                    return Value.Boolean;
                case TriggerVar.is_in_league_war:
                    return Value.Boolean;
                case TriggerVar.is_in_trade_league:
                    return Value.Boolean;
                case TriggerVar.is_in_trade_league_with:
                    return Value.TagORScope;
                case TriggerVar.is_island:
                    return Value.Boolean;
                case TriggerVar.is_league_enemy:
                    return Value.Scope;
                case TriggerVar.is_lacking_institutions:
                    return Value.Boolean;
                case TriggerVar.is_league_friend:
                    return Value.Scope;
                case TriggerVar.is_league_leader:
                    return Value.Boolean;
                case TriggerVar.is_lesser_in_union:
                    return Value.Boolean;
                case TriggerVar.is_looted:
                    return Value.Boolean;
                case TriggerVar.is_monarch_leader:
                    return Value.Boolean;
                case TriggerVar.is_month:
                    return Value.Integer;
                case TriggerVar.is_march:
                    return Value.Boolean;
                case TriggerVar.is_neighbor_of:
                    return Value.TagORScope;
                case TriggerVar.is_node_in_trade_company_region:
                    return Value.Boolean;
                case TriggerVar.is_nomad:
                    return Value.Boolean;
                case TriggerVar.is_orangists_in_power:
                    return Value.Boolean;
                case TriggerVar.is_origin_of_consort:
                    return Value.TagORScope;
                case TriggerVar.is_overseas:
                    return Value.Boolean;
                case TriggerVar.is_overseas_subject:
                    return Value.Boolean;
                case TriggerVar.is_owned_by_trade_company:
                    return Value.Boolean;
                case TriggerVar.is_papal_controller:
                    return Value.Boolean;
                case TriggerVar.is_part_of_hre:
                    return Value.Boolean;
                case TriggerVar.is_permanent_claim:
                    return Value.TagORScope;
                case TriggerVar.is_playing_custom_nation:
                    return Value.Boolean;
                case TriggerVar.is_possible_march:
                    return Value.TagORScope;
                case TriggerVar.is_possible_vassal:
                    return Value.TagORScope;
                case TriggerVar.is_previous_papal_controller:
                    return Value.Boolean;
                case TriggerVar.is_prosperous:
                    return Value.Boolean;
                case TriggerVar.is_protectorate:
                    return Value.Boolean;
                case TriggerVar.is_random_new_world:
                    return Value.Boolean;
                case TriggerVar.is_reformation_center:
                    return Value.Boolean;
                case TriggerVar.is_religion_grant_colonial_claim:
                    return Value.BooleanORTagORScope;
                case TriggerVar.is_religion_enabled:
                    return Value.ReligionIdentifier;
                case TriggerVar.is_religion_reformed:
                    return Value.Boolean;
                case TriggerVar.is_renting_condottieri_to:
                    return Value.Tag;
                case TriggerVar.is_revolution_target:
                    return Value.Boolean;
                case TriggerVar.is_rival:
                    return Value.TagORScope;
                case TriggerVar.is_ruler_commanding_unit:
                    return Value.Boolean;
                case TriggerVar.is_sea:
                    return Value.Boolean;
                case TriggerVar.is_state:
                    return Value.Boolean;
                case TriggerVar.is_state_core:
                    return Value.ProvinceIDORTagORScope;
                case TriggerVar.is_statists_in_power:
                    return Value.Boolean;
                case TriggerVar.is_strongest_trade_power:
                    return Value.TagORScope;
                case TriggerVar.is_subject:
                    return Value.Boolean;
                case TriggerVar.is_subject_of:
                    return Value.Tag;
                case TriggerVar.is_subject_of_type:
                    return Value.SubjectIdentifier;
                case TriggerVar.is_supporting_independence_of:
                    return Value.TagORScope;
                case TriggerVar.is_territorial_core:
                    return Value.ProvinceIDORTagORScope;
                case TriggerVar.is_territory:
                    return Value.Boolean;
                case TriggerVar.is_threat:
                    return Value.TagORScope;
                case TriggerVar.is_trade_league_leader:
                    return Value.Boolean;
                case TriggerVar.is_tribal:
                    return Value.Boolean;
                case TriggerVar.is_vassal:
                    return Value.Boolean;
                case TriggerVar.is_wasteland:
                    return Value.Boolean;
                case TriggerVar.is_year:
                    return Value.Integer;
                case TriggerVar.island:
                    return Value.Boolean;
                case TriggerVar.isolationism:
                    return Value.Integer;
                case TriggerVar.janissary_percentage:
                    return Value.Float;
                case TriggerVar.junior_union_with:
                    return Value.TagORScope;
                case TriggerVar.karma:
                    return Value.IntegerORTagORScope;
                case TriggerVar.knows_country:
                    return Value.TagORScope;
                case TriggerVar.land_forcelimit:
                    return Value.IntegerORTagORScope;
                case TriggerVar.land_maintenance:
                    return Value.Float;
                case TriggerVar.land_morale:
                    return Value.FloatORTagORScope;
                case TriggerVar.last_mission:
                    return Value.MissionIdentifier;
                case TriggerVar.legitimacy:
                    return Value.IntegerORTagORScope;
                case TriggerVar.legitimacy_equivalent:
                    return Value.IntegerORTagORScope;
                case TriggerVar.legitimacy_or_horde_unity:
                    return Value.IntegerORTagORScope;
                case TriggerVar.liberty_desire:
                    return Value.Integer;
                case TriggerVar.light_ship_fraction:
                    return Value.Float;
                case TriggerVar.light_ships_in_province:
                    return Value.IntegerORTagORScope;
                case TriggerVar.likely_rebels:
                    return Value.RebelIdentifier;
                case TriggerVar.local_autonomy:
                    return Value.Integer;
                case TriggerVar.local_autonomy_above_min:
                    return Value.Float;
                case TriggerVar.luck:
                    return Value.Boolean;
                case TriggerVar.march_of:
                    return Value.TagORScope;
                case TriggerVar.manpower:
                    return Value.Integer;
                case TriggerVar.manpower_percentage:
                    return Value.Float;
                case TriggerVar.marriage_with:
                    return Value.TagORScope;
                case TriggerVar.max_manpower:
                    return Value.Integer;
                case TriggerVar.mercantilism:
                    return Value.Float;
                case TriggerVar.meritocracy:
                    return Value.Integer;
                case TriggerVar.mil:
                    return Value.IntegerORTagORScope;
                case TriggerVar.militarised_society:
                    return Value.Integer;
                case TriggerVar.mil_power:
                    return Value.IntegerORTagORScope;
                case TriggerVar.mil_tech:
                    return Value.Integer;
                case TriggerVar.mission_completed:
                    return Value.MissionIdentifier;
                case TriggerVar.monthly_income:
                    return Value.IntegerORTagORScope;
                case TriggerVar.monthly_adm:
                    return Value.Integer;
                case TriggerVar.monthly_dip:
                    return Value.Integer;
                case TriggerVar.monthly_mil:
                    return Value.Integer;
                case TriggerVar.months_of_ruling:
                    return Value.Integer;
                case TriggerVar.months_since_defection:
                    return Value.Integer;
                case TriggerVar.nationalism:
                    return Value.Integer;
                case TriggerVar.national_focus:
                    return Value.NationalFocusIdentifier;
                case TriggerVar.nation_designer_points:
                    return Value.Integer;
                case TriggerVar.native_ferocity:
                    return Value.Integer;
                case TriggerVar.native_hostileness:
                    return Value.Integer;
                case TriggerVar.native_policy:
                    return Value.NativePolicyIdentifier;
                case TriggerVar.native_size:
                    return Value.Integer;
                case TriggerVar.naval_forcelimit:
                    return Value.IntegerORTagORScope;
                case TriggerVar.naval_maintenance:
                    return Value.Float;
                case TriggerVar.naval_morale:
                    return Value.FloatORTagORScope;
                case TriggerVar.navy_size:
                    return Value.IntegerORTagORScope;
                case TriggerVar.navy_size_percentage:
                    return Value.Integer;
                case TriggerVar.navy_tradition:
                    return Value.IntegerORTagORScope;
                case TriggerVar.normal_or_historical_nations:
                    return Value.Boolean;
                case TriggerVar.normal_province_values:
                    return Value.Boolean;
                case TriggerVar.num_accepted_cultures:
                    return Value.Integer;
                case TriggerVar.num_free_building_slots:
                    return Value.Integer;
                case TriggerVar.num_of_active_blessings:
                    return Value.Integer;
                case TriggerVar.num_of_admirals:
                    return Value.IntegerORTagORScope;
                case TriggerVar.num_of_admirals_with_traits:
                    return Value.Integer;
                case TriggerVar.num_of_allies:
                    return Value.IntegerORTagORScope;
                case TriggerVar.num_of_artillery:
                    return Value.IntegerORTagORScope;
                case TriggerVar.num_of_aspects:
                    return Value.IntegerORTagORScope;
                case TriggerVar.num_of_banners:
                    return Value.Integer;
                case TriggerVar.num_of_buildings_in_province:
                    return Value.Integer;
                case TriggerVar.num_of_captured_ships_with_boarding_doctrine:
                    return Value.Integer;
                case TriggerVar.num_of_centers_of_trade:
                    return Value.Integer;
                case TriggerVar.num_of_cardinals:
                    return Value.IntegerORTagORScope;
                case TriggerVar.num_of_cavalry:
                    return Value.IntegerORTagORScope;
                case TriggerVar.num_of_cawa:
                    return Value.Integer;
                case TriggerVar.num_of_cities:
                    return Value.IntegerORTagORScope;
                case TriggerVar.num_of_coalition_members:
                    return Value.Integer;
                case TriggerVar.num_of_colonies:
                    return Value.Integer;
                case TriggerVar.num_of_colonists:
                    return Value.Integer;
                case TriggerVar.num_of_conquistadors:
                    return Value.IntegerORTagORScope;
                case TriggerVar.num_of_consorts:
                    return Value.Integer;
                case TriggerVar.num_of_continents:
                    return Value.Integer;
                case TriggerVar.num_of_cossacks:
                    return Value.Integer;
                case TriggerVar.num_of_custom_nations:
                    return Value.Integer;
                case TriggerVar.num_of_diplomatic_relations:
                    return Value.Integer;
                case TriggerVar.num_of_diplomats:
                    return Value.Integer;
                case TriggerVar.num_of_electors:
                    return Value.Integer;
                case TriggerVar.num_of_explorers:
                    return Value.IntegerORTagORScope;
                case TriggerVar.num_of_foreign_hre_provinces:
                    return Value.Integer;
                case TriggerVar.num_of_free_diplomatic_relations:
                    return Value.Integer;
                case TriggerVar.num_of_galley:
                    return Value.IntegerORTagORScope;
                case TriggerVar.num_of_generals:
                    return Value.IntegerORTagORScope;
                case TriggerVar.num_of_generals_with_traits:
                    return Value.Integer;
                case TriggerVar.num_of_harmonized:
                    return Value.Integer;
                case TriggerVar.num_of_heavy_ship:
                    return Value.IntegerORTagORScope;
                case TriggerVar.num_of_infantry:
                    return Value.IntegerORTagORScope;
                case TriggerVar.num_of_light_ship:
                    return Value.IntegerORTagORScope;
                case TriggerVar.num_of_loans:
                    return Value.IntegerORTagORScope;
                case TriggerVar.num_of_marches:
                    return Value.IntegerORTagORScope;
                case TriggerVar.num_of_marines:
                    return Value.Integer;
                case TriggerVar.num_of_mercenaries:
                    return Value.IntegerORTagORScope;
                case TriggerVar.num_of_merchants:
                    return Value.Integer;
                case TriggerVar.num_of_missionaries:
                    return Value.Integer;
                case TriggerVar.num_of_owned_and_controlled_institutions:
                    return Value.Integer;
                case TriggerVar.num_of_ports:
                    return Value.IntegerORTagORScope;
                case TriggerVar.num_of_ports_blockading:
                    return Value.Integer;
                case TriggerVar.num_of_powerful_estates:
                    return Value.Integer;
                case TriggerVar.num_of_protectorates:
                    return Value.IntegerORTagORScope;
                case TriggerVar.num_of_provinces_in_states:
                    return Value.Integer;
                case TriggerVar.num_of_provinces_in_territories:
                    return Value.Integer;
                case TriggerVar.num_of_rajput:
                    return Value.Integer;
                case TriggerVar.num_of_rebel_armies:
                    return Value.IntegerORTagORScope;
                case TriggerVar.num_of_rebel_controlled_provinces:
                    return Value.IntegerORTagORScope;
                case TriggerVar.num_of_revolts:
                    return Value.IntegerORTagORScope;
                case TriggerVar.num_of_royal_marriages:
                    return Value.IntegerORTagORScope;
                case TriggerVar.num_of_ruler_traits:
                    return Value.TagORScope;
                case TriggerVar.num_of_states:
                    return Value.Integer;
                case TriggerVar.num_of_streltsy:
                    return Value.Integer;
                case TriggerVar.num_of_strong_trade_companies:
                    return Value.Integer;
                case TriggerVar.num_of_subjects:
                    return Value.Integer;
                case TriggerVar.num_of_territories:
                    return Value.Integer;
                case TriggerVar.num_of_times_improved:
                    return Value.Integer;
                case TriggerVar.num_of_times_improved_by_owner:
                    return Value.Integer;
                case TriggerVar.num_of_times_used_pillage_capital:
                    return Value.Integer;
                case TriggerVar.num_of_times_used_transfer_development:
                    return Value.Integer;
                case TriggerVar.num_of_total_ports:
                    return Value.IntegerORTagORScope;
                case TriggerVar.num_of_trade_companies:
                    return Value.Integer;
                case TriggerVar.num_of_trade_embargos:
                    return Value.Integer;
                case TriggerVar.num_of_trading_bonuses:
                    return Value.Integer;
                case TriggerVar.num_of_transport:
                    return Value.IntegerORTagORScope;
                case TriggerVar.num_of_trusted_allies:
                    return Value.Integer;
                case TriggerVar.num_of_unions:
                    return Value.IntegerORTagORScope;
                case TriggerVar.num_of_unlocked_cults:
                    return Value.Integer;
                case TriggerVar.num_of_war_reparations:
                    return Value.IntegerORTagORScope;
                case TriggerVar.num_ships_privateering:
                    return Value.IntegerORTagORScope;
                case TriggerVar.offensive_war_with:
                    return Value.TagORScope;
                case TriggerVar.overextension_percentage:
                    return Value.Float;
                case TriggerVar.overlord_of:
                    return Value.TagORScope;
                case TriggerVar.overseas_provinces_percentage:
                    return Value.Float;
                case TriggerVar.owned_by:
                    return Value.TagORScope;
                case TriggerVar.owns:
                    return Value.ProvinceID;
                case TriggerVar.owns_core_province:
                    return Value.ProvinceID;
                case TriggerVar.owns_or_non_sovereign_subject_of:
                    return Value.ProvinceID;
                case TriggerVar.owns_or_subject_of:
                    return Value.ProvinceIDORScope;
                case TriggerVar.papacy_active:
                    return Value.Boolean;
                case TriggerVar.papal_influence:
                    return Value.IntegerORTagORScope;
                case TriggerVar.patriarch_authority:
                    return Value.Float;
                case TriggerVar.percentage_backing_issue:
                    return Value.Float;
                case TriggerVar.personality:
                    return Value.AIPersonalityIdentifier;
                case TriggerVar.piety:
                    return Value.FloatORTagORScope;
                case TriggerVar.preferred_emperor:
                    return Value.TagORScope;
                case TriggerVar.prestige:
                    return Value.IntegerORTagORScope;
                case TriggerVar.previous_owner:
                    return Value.TagORScope;
                case TriggerVar.power_projection:
                    return Value.Integer;
                case TriggerVar.primary_culture:
                    return Value.CultureIdentifierORScope;
                case TriggerVar.primitives:
                    return Value.Boolean;
                case TriggerVar.production_efficiency:
                    return Value.FloatORTagORScope;
                case TriggerVar.production_income_percentage:
                    return Value.Float;
                case TriggerVar.province_id:
                    return Value.IntegerORScope;
                case TriggerVar.province_is_on_an_island:
                    return Value.Boolean;
                case TriggerVar.province_getting_expelled_minority:
                    return Value.Boolean;
                case TriggerVar.province_group:
                    return Value.ProvinceGroupIdentifier;
                case TriggerVar.province_size:
                    return Value.Integer;
                case TriggerVar.province_trade_power:
                    return Value.Integer;
                case TriggerVar.provinces_on_capital_continent_of:
                    return Value.Scope;
                case TriggerVar.pure_unrest:
                    return Value.Integer;
                case TriggerVar.range:
                    return Value.TagORScope;
                case TriggerVar.real_day_of_year:
                    return Value.Integer;
                case TriggerVar.real_month_of_year:
                    return Value.Integer;
                case TriggerVar.reform_desire:
                    return Value.Float;
                case TriggerVar.receives_military_access_from:
                    return Value.TagORScope;
                case TriggerVar.receives_fleet_basing_rights_from:
                    return Value.TagORScope;
                case TriggerVar.reform_level:
                    return Value.Integer;
                case TriggerVar.region:
                    return Value.RegionIdentifier;
                case TriggerVar.religion:
                    return Value.ReligionIdentifierORScope;
                case TriggerVar.religion_group:
                    return Value.ReligionGroupORScope;
                case TriggerVar.religious_unity:
                    return Value.Float;
                case TriggerVar.republican_tradition:
                    return Value.Integer;
                case TriggerVar.revanchism:
                    return Value.Integer;
                case TriggerVar.revolt_percentage:
                    return Value.Float;
                case TriggerVar.revolution_target_exists:
                    return Value.Boolean;
                case TriggerVar.ruler_age:
                    return Value.Integer;
                case TriggerVar.ruler_consort_marriage_length:
                    return Value.Integer;
                case TriggerVar.ruler_culture:
                    return Value.CultureIdentifierORScope;
                case TriggerVar.ruler_has_personality:
                    return Value.PersonalityIdentifier;
                case TriggerVar.ruler_is_foreigner:
                    return Value.Boolean;
                case TriggerVar.ruler_religion:
                    return Value.ReligionIdentifierORScope;
                case TriggerVar.sailors:
                    return Value.Integer;
                case TriggerVar.sailors_percentage:
                    return Value.Float;
                case TriggerVar.max_sailors:
                    return Value.Integer;
                case TriggerVar.same_continent:
                    return Value.IntegerORScope;
                case TriggerVar.secondary_religion:
                    return Value.ReligionIdentifier;
                case TriggerVar.senior_union_with:
                    return Value.TagORScope;
                case TriggerVar.sieged_by:
                    return Value.TagORScope;
                case TriggerVar.splendor:
                    return Value.Integer;
                case TriggerVar.stability:
                    return Value.IntegerORTagORScope;
                case TriggerVar.start_date:
                    return Value.Date;
                case TriggerVar.started_in:
                    return Value.Date;
                case TriggerVar.statists_vs_orangists:
                    return Value.Float;
                case TriggerVar.subsidised_percent_amount:
                    return Value.Float;
                case TriggerVar.succession_claim:
                    return Value.TagORScope;
                case TriggerVar.superregion:
                    return Value.SuperregionIdentifier;
                case TriggerVar.tag:
                    return Value.TagORScope;
                case TriggerVar.tariff_value:
                    return Value.Float;
                case TriggerVar.tax_income_percentage:
                    return Value.Float;
                case TriggerVar.tech_difference:
                    return Value.Integer;
                case TriggerVar.technology_group:
                    return Value.TechnologyGroupIdentifierORScope;
                case TriggerVar.tolerance_to_this:
                    return Value.Integer;
                case TriggerVar.total_base_tax:
                    return Value.IntegerORTagORScope;
                case TriggerVar.total_development:
                    return Value.IntegerORTagORScope;
                case TriggerVar.total_number_of_cardinals:
                    return Value.Integer;
                case TriggerVar.trade_league_embargoed_by:
                    return Value.TagORScope;
                case TriggerVar.total_own_and_non_tributary_subject_development:
                    return Value.TagORScope;
                case TriggerVar.transports_in_province:
                    return Value.IntegerORTagORScope;
                case TriggerVar.trade_company_region:
                    return Value.TradeCompanyIdentifier;
                case TriggerVar.trade_company_size:
                    return Value.Integer;
                case TriggerVar.trade_efficiency:
                    return Value.Float;
                case TriggerVar.trade_embargoing:
                    return Value.TagORScope;
                case TriggerVar.trade_embargo_by:
                    return Value.TagORScope;
                case TriggerVar.trade_goods:
                    return Value.TradeGoodIdentifier;
                case TriggerVar.trade_income_percentage:
                    return Value.Float;
                case TriggerVar.trade_node_value:
                    return Value.Float;
                case TriggerVar.trade_range:
                    return Value.TagORScope;
                case TriggerVar.transport_fraction:
                    return Value.Float;
                case TriggerVar.treasury:
                    return Value.IntegerORTagORScope;
                case TriggerVar.tribal_allegiance:
                    return Value.Integer;
                case TriggerVar.tribal_development:
                    return Value.IntegerORTagORScope;
                case TriggerVar.truce_with:
                    return Value.TagORScope;
                case TriggerVar.trust:
                    return Value.TagORScope;
                case TriggerVar.unit_has_leader:
                    return Value.Boolean;
                case TriggerVar.unit_in_battle:
                    return Value.Boolean;
                case TriggerVar.unit_in_siege:
                    return Value.Boolean;
                case TriggerVar.units_in_province:
                    return Value.IntegerORTagORScope;
                case TriggerVar.unit_type:
                    return Value.UnitTypeIdentifier;
                case TriggerVar.unrest:
                    return Value.Integer;
                case TriggerVar.uses_authority:
                    return Value.Boolean;
                case TriggerVar.uses_church_aspects:
                    return Value.Boolean;
                case TriggerVar.uses_blessings:
                    return Value.Boolean;
                case TriggerVar.uses_cults:
                    return Value.Boolean;
                case TriggerVar.uses_devotion:
                    return Value.Boolean;
                case TriggerVar.uses_doom:
                    return Value.Boolean;
                case TriggerVar.uses_fervor:
                    return Value.Boolean;
                case TriggerVar.uses_isolationism:
                    return Value.Boolean;
                case TriggerVar.uses_karma:
                    return Value.Boolean;
                case TriggerVar.uses_papacy:
                    return Value.Boolean;
                case TriggerVar.uses_patriarch_authority:
                    return Value.Boolean;
                case TriggerVar.uses_personal_deities:
                    return Value.Boolean;
                case TriggerVar.uses_piety:
                    return Value.Boolean;
                case TriggerVar.uses_religious_icons:
                    return Value.Boolean;
                case TriggerVar.uses_syncretic_faiths:
                    return Value.Boolean;
                case TriggerVar.vassal_of:
                    return Value.TagORScope;
                case TriggerVar.war_exhaustion:
                    return Value.IntegerORTagORScope;
                case TriggerVar.war_score:
                    return Value.Integer;
                case TriggerVar.war_with:
                    return Value.TagORScope;
                case TriggerVar.was_player:
                    return Value.Boolean;
                case TriggerVar.was_tag:
                    return Value.Tag;
                case TriggerVar.will_back_next_reform:
                    return Value.Boolean;
                case TriggerVar.yearly_corruption_increase:
                    return Value.Float;
                case TriggerVar.years_of_income:
                    return Value.FloatORTagORScope;
            }
        }
        public static Scope GetScopes(TriggerVar Variable)
        {
            switch (Variable)
            {
                default:
                    return Scope.Anywhere;
                case TriggerVar.NamedAdvisor:
                    return Scope.Country;
                case TriggerVar.NamedBuilding:
                    return Scope.Country;
                case TriggerVar.NamedIdeaGroup:
                    return Scope.Country;
                case TriggerVar.NamedInstitution:
                    return Scope.Province;
                case TriggerVar.NamedReligion:
                    return Scope.Country;
                case TriggerVar.NamedSubjectType:
                    return Scope.Country;
                case TriggerVar.NamedTradeGood:
                    return Scope.Country;
                case TriggerVar.absolutism:
                    return Scope.Country;
                case TriggerVar.accepted_culture:
                    return Scope.Country;
                case TriggerVar.active_major_mission:
                    return Scope.Country;
                case TriggerVar.adm:
                    return Scope.Country;
                case TriggerVar.adm_power:
                    return Scope.Country;
                case TriggerVar.adm_tech:
                    return Scope.Country;
                case TriggerVar.advisor:
                    return Scope.Country;
                case TriggerVar.advisor_exists:
                    return Scope.Country;
                case TriggerVar.ai:
                    return Scope.Country;
                case TriggerVar.alliance_with:
                    return Scope.Country;
                case TriggerVar.allows_female_emperor:
                    return Scope.Anywhere;
                case TriggerVar.always:
                    return Scope.Anywhere;
                case TriggerVar.area:
                    return Scope.Province;
                case TriggerVar.army_size:
                    return Scope.Country;
                case TriggerVar.army_size_percentage:
                    return Scope.Country;
                case TriggerVar.army_professionalism:
                    return Scope.Country;
                case TriggerVar.army_tradition:
                    return Scope.Country;
                case TriggerVar.artillery_fraction:
                    return Scope.Country;
                case TriggerVar.artillery_in_province:
                    return Scope.Province;
                case TriggerVar.at_war_with_religious_enemy:
                    return Scope.Country;
                case TriggerVar.authority:
                    return Scope.Country;
                case TriggerVar.average_autonomy:
                    return Scope.Country;
                case TriggerVar.average_autonomy_above_min:
                    return Scope.Country;
                case TriggerVar.average_effective_unrest:
                    return Scope.Country;
                case TriggerVar.average_home_autonomy:
                    return Scope.Country;
                case TriggerVar.average_unrest:
                    return Scope.Country;
                case TriggerVar.base_manpower:
                    return Scope.Province;
                case TriggerVar.base_production:
                    return Scope.Province;
                case TriggerVar.base_tax:
                    return Scope.Province;
                case TriggerVar.blockade:
                    return Scope.Country;
                case TriggerVar.can_be_overlord:
                    return Scope.Country;
                case TriggerVar.can_build:
                    return Scope.Province;
                case TriggerVar.can_create_vassals:
                    return Scope.Country;
                case TriggerVar.can_heir_be_child_of_consort:
                    return Scope.Country;
                case TriggerVar.can_justify_trade_conflict:
                    return Scope.Country;
                case TriggerVar.can_migrate:
                    return Scope.Country;
                case TriggerVar.can_spawn_rebels:
                    return Scope.Province;
                case TriggerVar.capital:
                    return Scope.Country;
                case TriggerVar.cavalry_fraction:
                    return Scope.Country;
                case TriggerVar.cavalry_in_province:
                    return Scope.Province;
                case TriggerVar.province_has_center_of_trade_of_level:
                    return Scope.Province;
                case TriggerVar.church_power:
                    return Scope.Country;
                case TriggerVar.coalition_target:
                    return Scope.Country;
                case TriggerVar.colonial_claim_by_anyone_of_religion:
                    return Scope.Province;
                case TriggerVar.colonial_region:
                    return Scope.Province;
                case TriggerVar.colony:
                    return Scope.Country;
                case TriggerVar.colony_claim:
                    return Scope.Country;
                case TriggerVar.colonysize:
                    return Scope.Province;
                case TriggerVar.consort_adm:
                    return Scope.Country;
                case TriggerVar.consort_age:
                    return Scope.Country;
                case TriggerVar.consort_dip:
                    return Scope.Country;
                case TriggerVar.consort_culture:
                    return Scope.Country;
                case TriggerVar.consort_has_personality:
                    return Scope.Country;
                case TriggerVar.consort_mil:
                    return Scope.Country;
                case TriggerVar.consort_religion:
                    return Scope.Country;
                case TriggerVar.construction_progress:
                    return Scope.Province;
                case TriggerVar.continent:
                    return Scope.Province;
                case TriggerVar.controlled_by:
                    return Scope.Province;
                case TriggerVar.controls:
                    return Scope.Country;
                case TriggerVar.core_claim:
                    return Scope.Country;
                case TriggerVar.core_percentage:
                    return Scope.Country;
                case TriggerVar.corruption:
                    return Scope.Country;
                case TriggerVar.council_position:
                    return Scope.Country;
                case TriggerVar.country_or_non_sovereign_subject_holds:
                    return Scope.Province;
                case TriggerVar.country_or_subject_holds:
                    return Scope.Province;
                case TriggerVar.crown_land_share:
                    return Scope.Country;
                case TriggerVar.culture:
                    return Scope.Province;
                case TriggerVar.culture_group:
                    return Scope.CountryORProvince;
                case TriggerVar.culture_group_claim:
                    return Scope.Country;
                case TriggerVar.current_age:
                    return Scope.Anywhere;
                case TriggerVar.current_bribe:
                    return Scope.Province;
                case TriggerVar.current_debate:
                    return Scope.Country;
                case TriggerVar.current_icon:
                    return Scope.Country;
                case TriggerVar.current_income_balance:
                    return Scope.Country;
                case TriggerVar.current_institution:
                    return Scope.Province;
                case TriggerVar.current_institution_growth:
                    return Scope.CountryORProvince;
                case TriggerVar.current_size_of_parliament:
                    return Scope.Country;
                case TriggerVar.defensive_war_with:
                    return Scope.Country;
                case TriggerVar.devastation:
                    return Scope.Province;
                case TriggerVar.development:
                    return Scope.Province;
                case TriggerVar.development_of_overlord_fraction:
                    return Scope.Country;
                case TriggerVar.devotion:
                    return Scope.Country;
                case TriggerVar.dip:
                    return Scope.Country;
                case TriggerVar.diplomatic_reputation:
                    return Scope.Country;
                case TriggerVar.dip_power:
                    return Scope.Country;
                case TriggerVar.dip_tech:
                    return Scope.Country;
                case TriggerVar.dominant_culture:
                    return Scope.Country;
                case TriggerVar.dominant_religion:
                    return Scope.Country;
                case TriggerVar.doom:
                    return Scope.Country;
                case TriggerVar.dynasty:
                    return Scope.Country;
                case TriggerVar.empire_of_china_reform_passed:
                    return Scope.Anywhere;
                case TriggerVar.estate_led_regency_influence:
                    return Scope.Country;
                case TriggerVar.estate_led_regency_loyalty:
                    return Scope.Country;
                case TriggerVar.exiled_same_dynasty_as_current:
                    return Scope.Country;
                case TriggerVar.exists:
                    return Scope.CountryORAnywhere;
                case TriggerVar.faction_in_power:
                    return Scope.Country;
                case TriggerVar.federation_size:
                    return Scope.Country;
                case TriggerVar.fervor:
                    return Scope.Country;
                case TriggerVar.fort_level:
                    return Scope.Province;
                case TriggerVar.full_idea_group:
                    return Scope.Country;
                case TriggerVar.galley_fraction:
                    return Scope.Country;
                case TriggerVar.galleys_in_province:
                    return Scope.Province;
                case TriggerVar.garrison:
                    return Scope.Province;
                case TriggerVar.gives_military_access_to:
                    return Scope.Country;
                case TriggerVar.gives_fleet_basing_rights_to:
                    return Scope.Country;
                case TriggerVar.gold_income:
                    return Scope.Country;
                case TriggerVar.gold_income_percentage:
                    return Scope.Country;
                case TriggerVar.government:
                    return Scope.Country;
                case TriggerVar.government_rank:
                    return Scope.Country;
                case TriggerVar.grown_by_development:
                    return Scope.Country;
                case TriggerVar.grown_by_states:
                    return Scope.Country;
                case TriggerVar.great_power_rank:
                    return Scope.Country;
                case TriggerVar.guaranteed_by:
                    return Scope.Country;
                case TriggerVar.had_recent_war:
                    return Scope.Country;
                case TriggerVar.harmonization_progress:
                    return Scope.Country;
                case TriggerVar.harmony:
                    return Scope.Country;
                case TriggerVar.has_active_debate:
                    return Scope.Country;
                case TriggerVar.has_active_fervor:
                    return Scope.Country;
                case TriggerVar.has_active_policy:
                    return Scope.Country;
                case TriggerVar.has_active_triggered_province_modifier:
                    return Scope.Province;
                case TriggerVar.has_adopted_cult:
                    return Scope.Country;
                case TriggerVar.has_advisor:
                    return Scope.Country;
                case TriggerVar.has_age_ability:
                    return Scope.Country;
                case TriggerVar.has_any_disaster:
                    return Scope.Country;
                case TriggerVar.has_border_with_religious_enemy:
                    return Scope.Country;
                case TriggerVar.has_building:
                    return Scope.Province;
                case TriggerVar.has_cardinal:
                    return Scope.Province;
                case TriggerVar.has_casus_belli_against:
                    return Scope.Country;
                case TriggerVar.has_center_of_trade_of_level:
                    return Scope.Country;
                case TriggerVar.has_changed_nation:
                    return Scope.Country;
                case TriggerVar.has_church_aspect:
                    return Scope.Country;
                case TriggerVar.has_climate:
                    return Scope.Province;
                case TriggerVar.has_colonial_parent:
                    return Scope.Country;
                case TriggerVar.has_colonist:
                    return Scope.Province;
                case TriggerVar.has_commanding_three_star:
                    return Scope.Country;
                case TriggerVar.has_consort:
                    return Scope.Country;
                case TriggerVar.has_consort_flag:
                    return Scope.Country;
                case TriggerVar.has_consort_regency:
                    return Scope.Country;
                case TriggerVar.has_construction:
                    return Scope.Province;
                case TriggerVar.has_country_flag:
                    return Scope.Country;
                case TriggerVar.has_country_modifier:
                    return Scope.Country;
                case TriggerVar.has_custom_ideas:
                    return Scope.Country;
                case TriggerVar.has_disaster:
                    return Scope.Country;
                case TriggerVar.has_discovered:
                    return Scope.CountryORProvince;
                case TriggerVar.has_dlc:
                    return Scope.Anywhere;
                case TriggerVar.has_divert_trade:
                    return Scope.Country;
                case TriggerVar.has_embargo_rivals:
                    return Scope.Country;
                case TriggerVar.has_empty_adjacent_province:
                    return Scope.Province;
                case TriggerVar.has_estate:
                    return Scope.Country;
                case TriggerVar.has_estate_province:
                    return Scope.Province;
                case TriggerVar.has_estate_loan:
                    return Scope.Country;
                case TriggerVar.has_estate_privilege:
                    return Scope.Country;
                case TriggerVar.has_faction:
                    return Scope.Country;
                case TriggerVar.has_factions:
                    return Scope.Country;
                case TriggerVar.has_female_consort:
                    return Scope.Country;
                case TriggerVar.has_female_heir:
                    return Scope.Country;
                case TriggerVar.has_first_revolution_started:
                    return Scope.Anywhere;
                case TriggerVar.has_flagship:
                    return Scope.Country;
                case TriggerVar.has_foreign_consort:
                    return Scope.Country;
                case TriggerVar.has_foreign_heir:
                    return Scope.Country;
                case TriggerVar.has_friendly_reformation_center:
                    return Scope.Country;
                case TriggerVar.has_game_started:
                    return Scope.Anywhere;
                case TriggerVar.has_given_consort_to:
                    return Scope.Country;
                case TriggerVar.has_guaranteed:
                    return Scope.Country;
                case TriggerVar.has_global_flag:
                    return Scope.Anywhere;
                case TriggerVar.has_government_mechanic:
                    return Scope.Country;
                case TriggerVar.has_government_power:
                    return Scope.Country;
                case TriggerVar.has_had_golden_age:
                    return Scope.Country;
                case TriggerVar.has_harmonized_with:
                    return Scope.Country;
                case TriggerVar.has_harsh_treatment:
                    return Scope.Province;
                case TriggerVar.has_heir:
                    return Scope.Country;
                case TriggerVar.has_heir_flag:
                    return Scope.Country;
                case TriggerVar.has_heir_leader_from:
                    return Scope.Province;
                case TriggerVar.has_hostile_reformation_center:
                    return Scope.Country;
                case TriggerVar.has_idea:
                    return Scope.Country;
                case TriggerVar.has_idea_group:
                    return Scope.Country;
                case TriggerVar.has_influencing_fort:
                    return Scope.Province;
                case TriggerVar.has_institution:
                    return Scope.Country;
                case TriggerVar.has_latent_trade_goods:
                    return Scope.Province;
                case TriggerVar.has_leader:
                    return Scope.Country;
                case TriggerVar.has_matching_religion:
                    return Scope.Country;
                case TriggerVar.has_merchant:
                    return Scope.Province;
                case TriggerVar.has_mission:
                    return Scope.Country;
                case TriggerVar.has_missionary:
                    return Scope.Province;
                case TriggerVar.has_monsoon:
                    return Scope.Province;
                case TriggerVar.has_most_province_trade_power:
                    return Scope.Province;
                case TriggerVar.has_new_dynasty:
                    return Scope.Country;
                case TriggerVar.has_owner_accepted_culture:
                    return Scope.Province;
                case TriggerVar.has_owner_culture:
                    return Scope.Province;
                case TriggerVar.has_owner_religion:
                    return Scope.Province;
                case TriggerVar.has_pasha:
                    return Scope.Province;
                case TriggerVar.has_parliament:
                    return Scope.Country;
                case TriggerVar.has_personal_deity:
                    return Scope.Country;
                case TriggerVar.has_pillaged_capital_against:
                    return Scope.Country;
                case TriggerVar.has_port:
                    return Scope.Province;
                case TriggerVar.has_privateers:
                    return Scope.Country;
                case TriggerVar.has_promote_investments:
                    return Scope.Country;
                case TriggerVar.has_province_flag:
                    return Scope.Province;
                case TriggerVar.has_province_modifier:
                    return Scope.Province;
                case TriggerVar.has_rebel_faction:
                    return Scope.Province;
                case TriggerVar.has_regency:
                    return Scope.Country;
                case TriggerVar.has_reform:
                    return Scope.Country;
                case TriggerVar.government_reform_progress:
                    return Scope.Country;
                case TriggerVar.has_removed_fow:
                    return Scope.Country;
                case TriggerVar.has_revolution_in_province:
                    return Scope.Province;
                case TriggerVar.has_ruler:
                    return Scope.Country;
                case TriggerVar.has_ruler_flag:
                    return Scope.Country;
                case TriggerVar.has_ruler_leader_from:
                    return Scope.Province;
                case TriggerVar.has_ruler_modifier:
                    return Scope.Country;
                case TriggerVar.has_saved_event_target:
                    return Scope.Anywhere;
                case TriggerVar.has_scutage:
                    return Scope.Country;
                case TriggerVar.has_seat_in_parliament:
                    return Scope.Province;
                case TriggerVar.has_secondary_religion:
                    return Scope.Country;
                case TriggerVar.has_send_officers:
                    return Scope.Country;
                case TriggerVar.has_siege:
                    return Scope.Province;
                case TriggerVar.has_spawned_rebels:
                    return Scope.Country;
                case TriggerVar.has_spawned_supported_rebels:
                    return Scope.Country;
                case TriggerVar.has_state_edict:
                    return Scope.State;
                case TriggerVar.has_state_patriach:
                    return Scope.Province;
                case TriggerVar.has_subsidize_armies:
                    return Scope.Country;
                case TriggerVar.has_supply_depot:
                    return Scope.Province;
                case TriggerVar.has_support_loyalists:
                    return Scope.Country;
                case TriggerVar.has_subject_of_type:
                    return Scope.Country;
                case TriggerVar.has_switched_nation:
                    return Scope.Anywhere;
                case TriggerVar.has_terrain:
                    return Scope.Province;
                case TriggerVar.has_trader:
                    return Scope.Province;
                case TriggerVar.has_truce:
                    return Scope.Country;
                case TriggerVar.has_unembraced_institution:
                    return Scope.Country;
                case TriggerVar.has_unified_culture_group:
                    return Scope.Country;
                case TriggerVar.has_unit_type:
                    return Scope.Country;
                case TriggerVar.has_unlocked_cult:
                    return Scope.Country;
                case TriggerVar.has_wartaxes:
                    return Scope.Country;
                case TriggerVar.has_winter:
                    return Scope.Province;
                case TriggerVar.have_had_reform:
                    return Scope.Country;
                case TriggerVar.heavy_ship_fraction:
                    return Scope.Country;
                case TriggerVar.heavy_ships_in_province:
                    return Scope.Province;
                case TriggerVar.heir_adm:
                    return Scope.Country;
                case TriggerVar.heir_age:
                    return Scope.Country;
                case TriggerVar.heir_dip:
                    return Scope.Country;
                case TriggerVar.heir_claim:
                    return Scope.Country;
                case TriggerVar.heir_culture:
                    return Scope.Country;
                case TriggerVar.heir_has_consort_dynasty:
                    return Scope.Country;
                case TriggerVar.heir_has_personality:
                    return Scope.Country;
                case TriggerVar.heir_has_ruler_dynasty:
                    return Scope.Country;
                case TriggerVar.heir_mil:
                    return Scope.Country;
                case TriggerVar.heir_nationality:
                    return Scope.Country;
                case TriggerVar.heir_religion:
                    return Scope.Country;
                case TriggerVar.higher_development_than:
                    return Scope.Country;
                case TriggerVar.highest_value_trade_node:
                    return Scope.Province;
                case TriggerVar.historical_friend_with:
                    return Scope.Country;
                case TriggerVar.historical_rival_with:
                    return Scope.Country;
                case TriggerVar.holy_order:
                    return Scope.Province;
                case TriggerVar.horde_unity:
                    return Scope.Country;
                case TriggerVar.hre_heretic_religion:
                    return Scope.Anywhere;
                case TriggerVar.hre_leagues_enabled:
                    return Scope.Anywhere;
                case TriggerVar.hre_reform_passed:
                    return Scope.Anywhere;
                case TriggerVar.hre_religion:
                    return Scope.Anywhere;
                case TriggerVar.hre_religion_locked:
                    return Scope.Anywhere;
                case TriggerVar.hre_religion_treaty:
                    return Scope.Anywhere;
                case TriggerVar.hre_size:
                    return Scope.Anywhere;
                case TriggerVar.imperial_influence:
                    return Scope.Anywhere;
                case TriggerVar.imperial_mandate:
                    return Scope.Anywhere;
                case TriggerVar.in_golden_age:
                    return Scope.Country;
                case TriggerVar.infantry_fraction:
                    return Scope.Country;
                case TriggerVar.infantry_in_province:
                    return Scope.Province;
                case TriggerVar.inflation:
                    return Scope.Country;
                case TriggerVar.innovativeness:
                    return Scope.Country;
                case TriggerVar.invested_papal_influence:
                    return Scope.Country;
                case TriggerVar.in_league:
                    return Scope.Country;
                case TriggerVar.ironman:
                    return Scope.Anywhere;
                case TriggerVar.is_advisor_employed:
                    return Scope.Anywhere;
                case TriggerVar.is_all_concessions_in_council_taken:
                    return Scope.Anywhere;
                case TriggerVar.is_at_war:
                    return Scope.Country;
                case TriggerVar.is_backing_current_issue:
                    return Scope.Province;
                case TriggerVar.is_bankrupt:
                    return Scope.Country;
                case TriggerVar.is_blockaded:
                    return Scope.Province;
                case TriggerVar.is_blockaded_by:
                    return Scope.Province;
                case TriggerVar.is_capital:
                    return Scope.Province;
                case TriggerVar.is_capital_of:
                    return Scope.Province;
                case TriggerVar.is_city:
                    return Scope.Province;
                case TriggerVar.is_claim:
                    return Scope.CountryORProvince;
                case TriggerVar.is_client_nation:
                    return Scope.Country;
                case TriggerVar.is_client_nation_of:
                    return Scope.Country;
                case TriggerVar.is_colonial_nation:
                    return Scope.Country;
                case TriggerVar.is_colonial_nation_of:
                    return Scope.Country;
                case TriggerVar.is_colony:
                    return Scope.Province;
                case TriggerVar.is_core:
                    return Scope.CountryORProvince;
                case TriggerVar.is_council_enabled:
                    return Scope.Anywhere;
                case TriggerVar.is_crusade_target:
                    return Scope.Country;
                case TriggerVar.is_defender_of_faith:
                    return Scope.Country;
                case TriggerVar.is_defender_of_faith_of_tier:
                    return Scope.Country;
                case TriggerVar.is_dynamic_tag:
                    return Scope.Country;
                case TriggerVar.is_elector:
                    return Scope.Country;
                case TriggerVar.is_emperor:
                    return Scope.Country;
                case TriggerVar.is_emperor_of_china:
                    return Scope.Country;
                case TriggerVar.is_empty:
                    return Scope.Province;
                case TriggerVar.is_enemy:
                    return Scope.Country;
                case TriggerVar.is_excommunicated:
                    return Scope.Country;
                case TriggerVar.is_federation_leader:
                    return Scope.Country;
                case TriggerVar.is_female:
                    return Scope.Country;
                case TriggerVar.is_force_converted:
                    return Scope.Country;
                case TriggerVar.is_former_colonial_nation:
                    return Scope.Country;
                case TriggerVar.is_foreign_claim:
                    return Scope.Province;
                case TriggerVar.is_great_power:
                    return Scope.Country;
                case TriggerVar.is_harmonizing_with:
                    return Scope.Country;
                case TriggerVar.is_heir_leader:
                    return Scope.Country;
                case TriggerVar.is_hegemon:
                    return Scope.Country;
                case TriggerVar.is_hegemon_of_type:
                    return Scope.Country;
                case TriggerVar.is_imperial_ban_allowed:
                    return Scope.Anywhere;
                case TriggerVar.is_incident_active:
                    return Scope.Country;
                case TriggerVar.is_incident_happened:
                    return Scope.Country;
                case TriggerVar.is_incident_possible:
                    return Scope.Country;
                case TriggerVar.is_incident_potential:
                    return Scope.Country;
                case TriggerVar.is_institution_enabled:
                    return Scope.Anywhere;
                case TriggerVar.is_institution_origin:
                    return Scope.Province;
                case TriggerVar.is_in_capital_area:
                    return Scope.Province;
                case TriggerVar.is_in_coalition:
                    return Scope.Country;
                case TriggerVar.is_in_coalition_war:
                    return Scope.Country;
                case TriggerVar.is_in_deficit:
                    return Scope.Country;
                case TriggerVar.is_in_extended_regency:
                    return Scope.Country;
                case TriggerVar.is_in_league_war:
                    return Scope.Country;
                case TriggerVar.is_in_trade_league:
                    return Scope.Country;
                case TriggerVar.is_in_trade_league_with:
                    return Scope.Country;
                case TriggerVar.is_island:
                    return Scope.Province;
                case TriggerVar.is_league_enemy:
                    return Scope.Country;
                case TriggerVar.is_lacking_institutions:
                    return Scope.Country;
                case TriggerVar.is_league_friend:
                    return Scope.Country;
                case TriggerVar.is_league_leader:
                    return Scope.Country;
                case TriggerVar.is_lesser_in_union:
                    return Scope.Country;
                case TriggerVar.is_looted:
                    return Scope.Province;
                case TriggerVar.is_monarch_leader:
                    return Scope.Country;
                case TriggerVar.is_month:
                    return Scope.Anywhere;
                case TriggerVar.is_march:
                    return Scope.Country;
                case TriggerVar.is_neighbor_of:
                    return Scope.Country;
                case TriggerVar.is_node_in_trade_company_region:
                    return Scope.Province;
                case TriggerVar.is_nomad:
                    return Scope.Country;
                case TriggerVar.is_orangists_in_power:
                    return Scope.Country;
                case TriggerVar.is_origin_of_consort:
                    return Scope.Country;
                case TriggerVar.is_overseas:
                    return Scope.Province;
                case TriggerVar.is_overseas_subject:
                    return Scope.Country;
                case TriggerVar.is_owned_by_trade_company:
                    return Scope.Province;
                case TriggerVar.is_papal_controller:
                    return Scope.Country;
                case TriggerVar.is_part_of_hre:
                    return Scope.CountryORProvince;
                case TriggerVar.is_permanent_claim:
                    return Scope.Province;
                case TriggerVar.is_playing_custom_nation:
                    return Scope.Country;
                case TriggerVar.is_possible_march:
                    return Scope.Country;
                case TriggerVar.is_possible_vassal:
                    return Scope.Country;
                case TriggerVar.is_previous_papal_controller:
                    return Scope.Country;
                case TriggerVar.is_prosperous:
                    return Scope.Province;
                case TriggerVar.is_protectorate:
                    return Scope.Country;
                case TriggerVar.is_random_new_world:
                    return Scope.Anywhere;
                case TriggerVar.is_reformation_center:
                    return Scope.Province;
                case TriggerVar.is_religion_grant_colonial_claim:
                    return Scope.Province;
                case TriggerVar.is_religion_enabled:
                    return Scope.Anywhere;
                case TriggerVar.is_religion_reformed:
                    return Scope.Country;
                case TriggerVar.is_renting_condottieri_to:
                    return Scope.Country;
                case TriggerVar.is_revolution_target:
                    return Scope.Country;
                case TriggerVar.is_rival:
                    return Scope.Country;
                case TriggerVar.is_ruler_commanding_unit:
                    return Scope.Unit;
                case TriggerVar.is_sea:
                    return Scope.Province;
                case TriggerVar.is_state:
                    return Scope.Province;
                case TriggerVar.is_state_core:
                    return Scope.CountryORProvince;
                case TriggerVar.is_statists_in_power:
                    return Scope.Province;
                case TriggerVar.is_strongest_trade_power:
                    return Scope.Province;
                case TriggerVar.is_subject:
                    return Scope.Country;
                case TriggerVar.is_subject_of:
                    return Scope.Country;
                case TriggerVar.is_subject_of_type:
                    return Scope.Country;
                case TriggerVar.is_supporting_independence_of:
                    return Scope.Country;
                case TriggerVar.is_territorial_core:
                    return Scope.CountryORProvince;
                case TriggerVar.is_territory:
                    return Scope.Province;
                case TriggerVar.is_threat:
                    return Scope.Country;
                case TriggerVar.is_trade_league_leader:
                    return Scope.Country;
                case TriggerVar.is_tribal:
                    return Scope.Country;
                case TriggerVar.is_vassal:
                    return Scope.Country;
                case TriggerVar.is_wasteland:
                    return Scope.Province;
                case TriggerVar.is_year:
                    return Scope.Anywhere;
                case TriggerVar.island:
                    return Scope.Province;
                case TriggerVar.isolationism:
                    return Scope.Country;
                case TriggerVar.janissary_percentage:
                    return Scope.Country;
                case TriggerVar.junior_union_with:
                    return Scope.Country;
                case TriggerVar.karma:
                    return Scope.Country;
                case TriggerVar.knows_country:
                    return Scope.Country;
                case TriggerVar.land_forcelimit:
                    return Scope.Country;
                case TriggerVar.land_maintenance:
                    return Scope.Country;
                case TriggerVar.land_morale:
                    return Scope.Country;
                case TriggerVar.last_mission:
                    return Scope.Country;
                case TriggerVar.legitimacy:
                    return Scope.Country;
                case TriggerVar.legitimacy_equivalent:
                    return Scope.Country;
                case TriggerVar.legitimacy_or_horde_unity:
                    return Scope.Country;
                case TriggerVar.liberty_desire:
                    return Scope.Country;
                case TriggerVar.light_ship_fraction:
                    return Scope.Country;
                case TriggerVar.light_ships_in_province:
                    return Scope.Province;
                case TriggerVar.likely_rebels:
                    return Scope.Province;
                case TriggerVar.local_autonomy:
                    return Scope.Province;
                case TriggerVar.local_autonomy_above_min:
                    return Scope.Province;
                case TriggerVar.luck:
                    return Scope.Country;
                case TriggerVar.march_of:
                    return Scope.Country;
                case TriggerVar.manpower:
                    return Scope.Country;
                case TriggerVar.manpower_percentage:
                    return Scope.Country;
                case TriggerVar.marriage_with:
                    return Scope.Country;
                case TriggerVar.max_manpower:
                    return Scope.Country;
                case TriggerVar.mercantilism:
                    return Scope.Country;
                case TriggerVar.meritocracy:
                    return Scope.Country;
                case TriggerVar.mil:
                    return Scope.Country;
                case TriggerVar.militarised_society:
                    return Scope.Country;
                case TriggerVar.mil_power:
                    return Scope.Country;
                case TriggerVar.mil_tech:
                    return Scope.Country;
                case TriggerVar.mission_completed:
                    return Scope.Country;
                case TriggerVar.monthly_income:
                    return Scope.Country;
                case TriggerVar.monthly_adm:
                    return Scope.Country;
                case TriggerVar.monthly_dip:
                    return Scope.Country;
                case TriggerVar.monthly_mil:
                    return Scope.Country;
                case TriggerVar.months_of_ruling:
                    return Scope.Country;
                case TriggerVar.months_since_defection:
                    return Scope.Province;
                case TriggerVar.nationalism:
                    return Scope.Province;
                case TriggerVar.national_focus:
                    return Scope.Country;
                case TriggerVar.nation_designer_points:
                    return Scope.Country;
                case TriggerVar.native_ferocity:
                    return Scope.Province;
                case TriggerVar.native_hostileness:
                    return Scope.Province;
                case TriggerVar.native_policy:
                    return Scope.Country;
                case TriggerVar.native_size:
                    return Scope.Province;
                case TriggerVar.naval_forcelimit:
                    return Scope.Country;
                case TriggerVar.naval_maintenance:
                    return Scope.Country;
                case TriggerVar.naval_morale:
                    return Scope.Country;
                case TriggerVar.navy_size:
                    return Scope.Country;
                case TriggerVar.navy_size_percentage:
                    return Scope.Country;
                case TriggerVar.navy_tradition:
                    return Scope.Country;
                case TriggerVar.normal_or_historical_nations:
                    return Scope.Anywhere;
                case TriggerVar.normal_province_values:
                    return Scope.Anywhere;
                case TriggerVar.num_accepted_cultures:
                    return Scope.Country;
                case TriggerVar.num_free_building_slots:
                    return Scope.Province;
                case TriggerVar.num_of_active_blessings:
                    return Scope.Country;
                case TriggerVar.num_of_admirals:
                    return Scope.Country;
                case TriggerVar.num_of_admirals_with_traits:
                    return Scope.Country;
                case TriggerVar.num_of_allies:
                    return Scope.Country;
                case TriggerVar.num_of_artillery:
                    return Scope.Country;
                case TriggerVar.num_of_aspects:
                    return Scope.Country;
                case TriggerVar.num_of_banners:
                    return Scope.Country;
                case TriggerVar.num_of_buildings_in_province:
                    return Scope.Province;
                case TriggerVar.num_of_captured_ships_with_boarding_doctrine:
                    return Scope.Country;
                case TriggerVar.num_of_centers_of_trade:
                    return Scope.Country;
                case TriggerVar.num_of_cardinals:
                    return Scope.Country;
                case TriggerVar.num_of_cavalry:
                    return Scope.Country;
                case TriggerVar.num_of_cawa:
                    return Scope.Country;
                case TriggerVar.num_of_cities:
                    return Scope.Country;
                case TriggerVar.num_of_coalition_members:
                    return Scope.Country;
                case TriggerVar.num_of_colonies:
                    return Scope.Country;
                case TriggerVar.num_of_colonists:
                    return Scope.Country;
                case TriggerVar.num_of_conquistadors:
                    return Scope.Country;
                case TriggerVar.num_of_consorts:
                    return Scope.Country;
                case TriggerVar.num_of_continents:
                    return Scope.Country;
                case TriggerVar.num_of_cossacks:
                    return Scope.Country;
                case TriggerVar.num_of_custom_nations:
                    return Scope.Country;
                case TriggerVar.num_of_diplomatic_relations:
                    return Scope.Country;
                case TriggerVar.num_of_diplomats:
                    return Scope.Country;
                case TriggerVar.num_of_electors:
                    return Scope.Anywhere;
                case TriggerVar.num_of_explorers:
                    return Scope.Country;
                case TriggerVar.num_of_foreign_hre_provinces:
                    return Scope.Anywhere;
                case TriggerVar.num_of_free_diplomatic_relations:
                    return Scope.Country;
                case TriggerVar.num_of_galley:
                    return Scope.Country;
                case TriggerVar.num_of_generals:
                    return Scope.Country;
                case TriggerVar.num_of_generals_with_traits:
                    return Scope.Country;
                case TriggerVar.num_of_harmonized:
                    return Scope.Country;
                case TriggerVar.num_of_heavy_ship:
                    return Scope.Country;
                case TriggerVar.num_of_infantry:
                    return Scope.Country;
                case TriggerVar.num_of_light_ship:
                    return Scope.Country;
                case TriggerVar.num_of_loans:
                    return Scope.Country;
                case TriggerVar.num_of_marches:
                    return Scope.Country;
                case TriggerVar.num_of_marines:
                    return Scope.Country;
                case TriggerVar.num_of_mercenaries:
                    return Scope.Country;
                case TriggerVar.num_of_merchants:
                    return Scope.Country;
                case TriggerVar.num_of_missionaries:
                    return Scope.Country;
                case TriggerVar.num_of_owned_and_controlled_institutions:
                    return Scope.Country;
                case TriggerVar.num_of_ports:
                    return Scope.Country;
                case TriggerVar.num_of_ports_blockading:
                    return Scope.Country;
                case TriggerVar.num_of_powerful_estates:
                    return Scope.Country;
                case TriggerVar.num_of_protectorates:
                    return Scope.Country;
                case TriggerVar.num_of_provinces_in_states:
                    return Scope.Country;
                case TriggerVar.num_of_provinces_in_territories:
                    return Scope.Country;
                case TriggerVar.num_of_rajput:
                    return Scope.Country;
                case TriggerVar.num_of_rebel_armies:
                    return Scope.Country;
                case TriggerVar.num_of_rebel_controlled_provinces:
                    return Scope.Country;
                case TriggerVar.num_of_revolts:
                    return Scope.Country;
                case TriggerVar.num_of_royal_marriages:
                    return Scope.Country;
                case TriggerVar.num_of_ruler_traits:
                    return Scope.Country;
                case TriggerVar.num_of_states:
                    return Scope.Country;
                case TriggerVar.num_of_streltsy:
                    return Scope.Country;
                case TriggerVar.num_of_strong_trade_companies:
                    return Scope.Country;
                case TriggerVar.num_of_subjects:
                    return Scope.Country;
                case TriggerVar.num_of_territories:
                    return Scope.Country;
                case TriggerVar.num_of_times_improved:
                    return Scope.Province;
                case TriggerVar.num_of_times_improved_by_owner:
                    return Scope.Province;
                case TriggerVar.num_of_times_used_pillage_capital:
                    return Scope.Country;
                case TriggerVar.num_of_times_used_transfer_development:
                    return Scope.Country;
                case TriggerVar.num_of_total_ports:
                    return Scope.Country;
                case TriggerVar.num_of_trade_companies:
                    return Scope.Country;
                case TriggerVar.num_of_trade_embargos:
                    return Scope.Country;
                case TriggerVar.num_of_trading_bonuses:
                    return Scope.Country;
                case TriggerVar.num_of_transport:
                    return Scope.Country;
                case TriggerVar.num_of_trusted_allies:
                    return Scope.Country;
                case TriggerVar.num_of_unions:
                    return Scope.Country;
                case TriggerVar.num_of_unlocked_cults:
                    return Scope.Country;
                case TriggerVar.num_of_war_reparations:
                    return Scope.Country;
                case TriggerVar.num_ships_privateering:
                    return Scope.Country;
                case TriggerVar.offensive_war_with:
                    return Scope.Country;
                case TriggerVar.overextension_percentage:
                    return Scope.Country;
                case TriggerVar.overlord_of:
                    return Scope.Country;
                case TriggerVar.overseas_provinces_percentage:
                    return Scope.Country;
                case TriggerVar.owned_by:
                    return Scope.Province;
                case TriggerVar.owns:
                    return Scope.Country;
                case TriggerVar.owns_core_province:
                    return Scope.Country;
                case TriggerVar.owns_or_non_sovereign_subject_of:
                    return Scope.Country;
                case TriggerVar.owns_or_subject_of:
                    return Scope.Country;
                case TriggerVar.papacy_active:
                    return Scope.Anywhere;
                case TriggerVar.papal_influence:
                    return Scope.Country;
                case TriggerVar.patriarch_authority:
                    return Scope.Country;
                case TriggerVar.percentage_backing_issue:
                    return Scope.Country;
                case TriggerVar.personality:
                    return Scope.Country;
                case TriggerVar.piety:
                    return Scope.Country;
                case TriggerVar.preferred_emperor:
                    return Scope.Country;
                case TriggerVar.prestige:
                    return Scope.Country;
                case TriggerVar.previous_owner:
                    return Scope.Province;
                case TriggerVar.power_projection:
                    return Scope.Country;
                case TriggerVar.primary_culture:
                    return Scope.Country;
                case TriggerVar.primitives:
                    return Scope.Country;
                case TriggerVar.production_efficiency:
                    return Scope.Country;
                case TriggerVar.production_income_percentage:
                    return Scope.Country;
                case TriggerVar.province_id:
                    return Scope.Province;
                case TriggerVar.province_is_on_an_island:
                    return Scope.Province;
                case TriggerVar.province_getting_expelled_minority:
                    return Scope.Province;
                case TriggerVar.province_group:
                    return Scope.Province;
                case TriggerVar.province_size:
                    return Scope.Province;
                case TriggerVar.province_trade_power:
                    return Scope.Province;
                case TriggerVar.provinces_on_capital_continent_of:
                    return Scope.Country;
                case TriggerVar.pure_unrest:
                    return Scope.Province;
                case TriggerVar.range:
                    return Scope.Province;
                case TriggerVar.real_day_of_year:
                    return Scope.Anywhere;
                case TriggerVar.real_month_of_year:
                    return Scope.Anywhere;
                case TriggerVar.reform_desire:
                    return Scope.Anywhere;
                case TriggerVar.receives_military_access_from:
                    return Scope.Country;
                case TriggerVar.receives_fleet_basing_rights_from:
                    return Scope.Country;
                case TriggerVar.reform_level:
                    return Scope.Country;
                case TriggerVar.region:
                    return Scope.Province;
                case TriggerVar.religion:
                    return Scope.CountryORProvince;
                case TriggerVar.religion_group:
                    return Scope.CountryORProvince;
                case TriggerVar.religious_unity:
                    return Scope.Country;
                case TriggerVar.republican_tradition:
                    return Scope.Country;
                case TriggerVar.revanchism:
                    return Scope.Country;
                case TriggerVar.revolt_percentage:
                    return Scope.Country;
                case TriggerVar.revolution_target_exists:
                    return Scope.Anywhere;
                case TriggerVar.ruler_age:
                    return Scope.Country;
                case TriggerVar.ruler_consort_marriage_length:
                    return Scope.Country;
                case TriggerVar.ruler_culture:
                    return Scope.Country;
                case TriggerVar.ruler_has_personality:
                    return Scope.Country;
                case TriggerVar.ruler_is_foreigner:
                    return Scope.Country;
                case TriggerVar.ruler_religion:
                    return Scope.Country;
                case TriggerVar.sailors:
                    return Scope.Country;
                case TriggerVar.sailors_percentage:
                    return Scope.Country;
                case TriggerVar.max_sailors:
                    return Scope.Country;
                case TriggerVar.same_continent:
                    return Scope.CountryORProvince;
                case TriggerVar.secondary_religion:
                    return Scope.Country;
                case TriggerVar.senior_union_with:
                    return Scope.Country;
                case TriggerVar.sieged_by:
                    return Scope.Province;
                case TriggerVar.splendor:
                    return Scope.Country;
                case TriggerVar.stability:
                    return Scope.Country;
                case TriggerVar.start_date:
                    return Scope.Anywhere;
                case TriggerVar.started_in:
                    return Scope.Anywhere;
                case TriggerVar.statists_vs_orangists:
                    return Scope.Country;
                case TriggerVar.subsidised_percent_amount:
                    return Scope.Country;
                case TriggerVar.succession_claim:
                    return Scope.Country;
                case TriggerVar.superregion:
                    return Scope.Province;
                case TriggerVar.tag:
                    return Scope.Country;
                case TriggerVar.tariff_value:
                    return Scope.Country;
                case TriggerVar.tax_income_percentage:
                    return Scope.Country;
                case TriggerVar.tech_difference:
                    return Scope.Country;
                case TriggerVar.technology_group:
                    return Scope.Country;
                case TriggerVar.tolerance_to_this:
                    return Scope.CountryORProvince;
                case TriggerVar.total_base_tax:
                    return Scope.Country;
                case TriggerVar.total_development:
                    return Scope.Country;
                case TriggerVar.total_number_of_cardinals:
                    return Scope.Anywhere;
                case TriggerVar.trade_league_embargoed_by:
                    return Scope.Country;
                case TriggerVar.total_own_and_non_tributary_subject_development:
                    return Scope.Country;
                case TriggerVar.transports_in_province:
                    return Scope.Province;
                case TriggerVar.trade_company_region:
                    return Scope.Province;
                case TriggerVar.trade_company_size:
                    return Scope.Province;
                case TriggerVar.trade_efficiency:
                    return Scope.Country;
                case TriggerVar.trade_embargoing:
                    return Scope.Country;
                case TriggerVar.trade_embargo_by:
                    return Scope.Country;
                case TriggerVar.trade_goods:
                    return Scope.Province;
                case TriggerVar.trade_income_percentage:
                    return Scope.Country;
                case TriggerVar.trade_node_value:
                    return Scope.Province;
                case TriggerVar.trade_range:
                    return Scope.Province;
                case TriggerVar.transport_fraction:
                    return Scope.Country;
                case TriggerVar.treasury:
                    return Scope.Country;
                case TriggerVar.tribal_allegiance:
                    return Scope.Country;
                case TriggerVar.tribal_development:
                    return Scope.CountryORProvince;
                case TriggerVar.truce_with:
                    return Scope.Country;
                case TriggerVar.trust:
                    return Scope.Country;
                case TriggerVar.unit_has_leader:
                    return Scope.Province;
                case TriggerVar.unit_in_battle:
                    return Scope.Province;
                case TriggerVar.unit_in_siege:
                    return Scope.Province;
                case TriggerVar.units_in_province:
                    return Scope.Province;
                case TriggerVar.unit_type:
                    return Scope.Country;
                case TriggerVar.unrest:
                    return Scope.CountryORProvince;
                case TriggerVar.uses_authority:
                    return Scope.Country;
                case TriggerVar.uses_church_aspects:
                    return Scope.Country;
                case TriggerVar.uses_blessings:
                    return Scope.Country;
                case TriggerVar.uses_cults:
                    return Scope.Country;
                case TriggerVar.uses_devotion:
                    return Scope.Country;
                case TriggerVar.uses_doom:
                    return Scope.Country;
                case TriggerVar.uses_fervor:
                    return Scope.Country;
                case TriggerVar.uses_isolationism:
                    return Scope.Country;
                case TriggerVar.uses_karma:
                    return Scope.Country;
                case TriggerVar.uses_papacy:
                    return Scope.Country;
                case TriggerVar.uses_patriarch_authority:
                    return Scope.Country;
                case TriggerVar.uses_personal_deities:
                    return Scope.Country;
                case TriggerVar.uses_piety:
                    return Scope.Country;
                case TriggerVar.uses_religious_icons:
                    return Scope.Country;
                case TriggerVar.uses_syncretic_faiths:
                    return Scope.Country;
                case TriggerVar.vassal_of:
                    return Scope.Country;
                case TriggerVar.war_exhaustion:
                    return Scope.Country;
                case TriggerVar.war_score:
                    return Scope.Country;
                case TriggerVar.war_with:
                    return Scope.Country;
                case TriggerVar.was_player:
                    return Scope.Country;
                case TriggerVar.was_tag:
                    return Scope.Country;
                case TriggerVar.will_back_next_reform:
                    return Scope.Country;
                case TriggerVar.yearly_corruption_increase:
                    return Scope.Country;
                case TriggerVar.years_of_income:
                    return Scope.Country;
            }
        }
        public static string GetDescription(TriggerVar Variable)
        {
            switch (Variable)
            {
                default:
                    return "No description!";
                case TriggerVar.NamedAdvisor:
                    return "Returns true if the country has hired an advisor of the specified type which has at least level X.";
                case TriggerVar.NamedBuilding:
                    return "Returns true if the country has at least X buildings from the specified building type.";
                case TriggerVar.NamedIdeaGroup:
                    return "Returns true if the country has at least X ideas from the specified idea group.";
                case TriggerVar.NamedInstitution:
                    return "Returns true if the support for the specified institution in the province is at least X.";
                case TriggerVar.NamedReligion:
                    return "Returns true if the country has a tolerance of at least X of the specified religion.\r\nNote: No correct localisation.";
                case TriggerVar.NamedSubjectType:
                    return "Returns true if the country has at least X subjects of the given type.";
                case TriggerVar.NamedTradeGood:
                    return "";
                case TriggerVar.absolutism:
                    return "Returns true if the country has at least X absolutism.";
                case TriggerVar.accepted_culture:
                    return "Returns true if the country accepts the specified culture.";
                case TriggerVar.active_major_mission:
                    return "Returns true if X is the current mission of the country.";
                case TriggerVar.adm:
                    return "Returns true if the country has a ruler with an administrative skill of at least X.";
                case TriggerVar.adm_power:
                    return "Returns true if the country has at least X administrative power.";
                case TriggerVar.adm_tech:
                    return "Returns true if the country has an administrative technology of at least X.";
                case TriggerVar.advisor:
                    return "Returns true if the country has an advisor of the specified type.";
                case TriggerVar.advisor_exists:
                    return "Returns true if the advisor X exists.";
                case TriggerVar.ai:
                    return "Returns true if the country is controlled by the AI.";
                case TriggerVar.alliance_with:
                    return "Returns true if the country has an alliance with X.";
                case TriggerVar.allows_female_emperor:
                    return "Returns true if females can become the emperor.";
                case TriggerVar.always:
                    return "Returns true under all circumstances if set to yes, returns false under all circumstances if set to no.";
                case TriggerVar.area:
                    return "Returns true if the province is part of the area X.";
                case TriggerVar.army_size:
                    return "Returns true if the country has an army of at least X k soldiers.";
                case TriggerVar.army_size_percentage:
                    return "Returns true if the total army size of the country is at least X% of its [[land force limit]].";
                case TriggerVar.army_professionalism:
                    return "Returns true if the country's army professionalism is of at least X.";
                case TriggerVar.army_tradition:
                    return "Returns true if the country has an army tradition of at least X.";
                case TriggerVar.artillery_fraction:
                    return "Returns true if the ratio of the artillery fraction to the army size of the country is at least X.";
                case TriggerVar.artillery_in_province:
                    return "Returns true if there are at least X artillery units in the province.";
                case TriggerVar.at_war_with_religious_enemy:
                    return "Returns true if the country is at war with any country of a different religion.";
                case TriggerVar.authority:
                    return "Returns true if the Inti country has at least X authority.\n\tReturns true if the Inti country has at least as much authority as the specified country.";
                case TriggerVar.average_autonomy:
                    return "Returns true if the country has an average autonomy in its provinces of at least X.";
                case TriggerVar.average_autonomy_above_min:
                    return "";
                case TriggerVar.average_effective_unrest:
                    return "";
                case TriggerVar.average_home_autonomy:
                    return "Returns true if the country has an average autonomy in its provinces excluding overseas provinces is at least X.";
                case TriggerVar.average_unrest:
                    return "Returns true if the country has an average unrest in its provinces of at least X.";
                case TriggerVar.base_manpower:
                    return "Returns true if the base manpower of the province is at least X.";
                case TriggerVar.base_production:
                    return "Returns true if the base production of the province is at least X.";
                case TriggerVar.base_tax:
                    return "Returns true if the base tax of the province is at least X.";
                case TriggerVar.blockade:
                    return "Returns true if the blockade penalty of the country is at least X%.";
                case TriggerVar.can_be_overlord:
                    return "Returns true, if country meets the conditions defined in the subject type's is_potential_overlord section.";
                case TriggerVar.can_build:
                    return "Returns true if the specified building can be built in the province.";
                case TriggerVar.can_create_vassals:
                    return "Returns true if the country can create a vassal.Warning: Interprets anything after ‘=’ as “yes”.";
            case TriggerVar.can_heir_be_child_of_consort:
                    return "Returns true if the country’s heir can potentially be the consort’s child.";
                case TriggerVar.can_justify_trade_conflict:
                    return "Returns true if the country can justify a trade conflict against country X.";
                case TriggerVar.can_migrate:
                    return "Returns true if the country can migrate to another province. Doesn't return true if the timer is still counting down.";
                case TriggerVar.can_spawn_rebels:
                    return "Returns true if the specified rebel faction is a valid rebel faction of the province.";
                case TriggerVar.capital:
                    return "Returns true if the country's capital is the province with the ID X.";
                case TriggerVar.cavalry_fraction:
                    return "Returns true if the ratio of the cavalry fraction to the army size of the country is at least X.";
                case TriggerVar.cavalry_in_province:
                    return "Returns true if there are at least X cavalry units in the province.";
                case TriggerVar.province_has_center_of_trade_of_level:
                    return "Returns true if the province has a center of trade of at least this level.";
                case TriggerVar.church_power:
                    return "Returns true if the country has at least X church power.";
                case TriggerVar.coalition_target:
                    return "Returns true if the country is the target of a coalition.";
                case TriggerVar.colonial_claim_by_anyone_of_religion:
                    return "Returns true if ... ''has gotten a colonial grant for the scope's colonial region from any potential pope-like entities.''";
                case TriggerVar.colonial_region:
                    return "Returns true if the province is part of the colonial region X.";
                case TriggerVar.colony:
                    return "Returns true if country has at least that many colonial subjects.";
                case TriggerVar.colony_claim:
                    return "Returns true if country has claim on colony";
                case TriggerVar.colonysize:
                    return "Returns true if colony is at least size X.";
                case TriggerVar.consort_adm:
                    return "Returns true if the country has a consort with an administrative skill of at least X.";
                case TriggerVar.consort_age:
                    return "Returns true if the country’s consort has an age of at least X.<br>Note: Always returns false if there is no consort.";
                case TriggerVar.consort_dip:
                    return "Returns true if the country has a consort with an diplomatic skill of at least X.";
                case TriggerVar.consort_culture:
                    return "Returns true, if the country's consort has the specified culture. Can utilise Variables.";
                case TriggerVar.consort_has_personality:
                    return "Returns true if the country’s consort has the specified personality.";
                case TriggerVar.consort_mil:
                    return "Returns true if the country has a consort with an military skill of at least X.";
                case TriggerVar.consort_religion:
                    return "Returns true, if the country's consort has the specified religion. Can utilise Variables.";
                case TriggerVar.construction_progress:
                    return "Returns true if the construction progress is at least X%.";
                case TriggerVar.continent:
                    return "Returns true if the province is located on continent X.";
                case TriggerVar.controlled_by:
                    return "Returns true if the province is controlled by X.";
                case TriggerVar.controls:
                    return "Returns true if the province with id X is controlled by the country.";
                case TriggerVar.core_claim:
                    return "Returns true if the country has a core on any province owned by country X.";
                case TriggerVar.core_percentage:
                    return "Returns true if the country has cored at least X% of its provinces.";
                case TriggerVar.corruption:
                    return "Returns true if the country has a [[corruption]] of at least X.";
                case TriggerVar.council_position:
                    return "Returns true if X is the country's council position in the Council of Trent.";
                case TriggerVar.country_or_non_sovereign_subject_holds:
                    return "Returns true if the province is part of the specified country or its non-tributary subjects.";
                case TriggerVar.country_or_subject_holds:
                    return "Returns true if the province is part of the specified country or its subjects.";
                case TriggerVar.crown_land_share:
                    return "Returns true if the % of the country's development is held by the Crown Land. If an estate is declared, returns true if % of Crown Land is higher than land owned by the estate.";
                case TriggerVar.culture:
                    return "Returns true if the province culture is X.";
                case TriggerVar.culture_group:
                    return "Returns true if the country/province has a culture of the specified culture group.";
                case TriggerVar.culture_group_claim:
                    return "Returns true if country's primary culture is the same culture group as any province owned by country X.";
                case TriggerVar.current_age:
                    return "Returns true, if the current age is X.";
                case TriggerVar.current_bribe:
                    return "Returns true, if the seat in the province wants bribe X.";
                case TriggerVar.current_debate:
                    return "Returns true if the parliament of the country debates about X.";
                case TriggerVar.current_icon:
                    return "Returns true, if the country has the specified (orthodox) icon.";
                case TriggerVar.current_income_balance:
                    return "Returns true if the current income balance is X.";
                case TriggerVar.current_institution:
                    return "Returns true if the current institution (technically the first unembraced meaning in case you have two unembraced, it would be the older one) progress is at least X";
                case TriggerVar.current_institution_growth:
                    return "Returns true, if the country/province has a annual (whereas monthly is printed in province screen) institution growth of at least X for the first not embraced institution.";
                case TriggerVar.current_size_of_parliament:
                    return "Returns true if the parliament of the country has a least X seats.";
                case TriggerVar.defensive_war_with:
                    return "Returns true if the country is in a defensive war with country X.";
                case TriggerVar.devastation:
                    return "Returns true if the devastation of the province is at least X.";
                case TriggerVar.development:
                    return "Returns true if the development of the province is at least X. Does not accept base scopes as of 1.32.";
                case TriggerVar.development_of_overlord_fraction:
                    return "Returns true, if the country has X percent of its overlord's development";
                case TriggerVar.devotion:
                    return "Returns true if the country has at least X devotion.";
                case TriggerVar.dip:
                    return "Returns true if the country has a ruler with a diplomatic skill of at least X.";
                case TriggerVar.diplomatic_reputation:
                    return "Returns true if the country has a diplomatic reputation of at least X.";
                case TriggerVar.dip_power:
                    return "Returns true if the country has at least X diplomatic power.";
                case TriggerVar.dip_tech:
                    return "Returns true if the country has an diplomatic technology of at least level X.";
                case TriggerVar.dominant_culture:
                    return "Returns true if the dominant culture in the country is X.";
                case TriggerVar.dominant_religion:
                    return "Returns true if the dominant religion in the country is X.";
                case TriggerVar.doom:
                    return "Returns true if the country has at least X doom.";
                case TriggerVar.dynasty:
                    return "Returns true if the ruling dynasty of the country is X.";
                case TriggerVar.empire_of_china_reform_passed:
                    return "Returns true, if X Empire of China reform is enacted.";
                case TriggerVar.estate_led_regency_influence:
                    return "Returns true if the estate leading the regency in the country has at least X influence.";
                case TriggerVar.estate_led_regency_loyalty:
                    return "Returns true if the estate leading the regency in the country has at least X loyalty.";
                case TriggerVar.exiled_same_dynasty_as_current:
                    return "Returns true if an exiled ruler has the same dynasty as the current one.";
                case TriggerVar.exists:
                    return "Returns true if country X exists.";
                case TriggerVar.faction_in_power:
                    return "Returns true if the faction in power of the country is X.";
                case TriggerVar.federation_size:
                    return "Returns true if the federation with the country has at least X members.";
                case TriggerVar.fervor:
                    return "Returns true if the country has stored at least X fervor points.";
                case TriggerVar.fort_level:
                    return "Returns true if the fort in the province has at least level X.";
                case TriggerVar.full_idea_group:
                    return "Returns true if the country has completed the X idea group.";
                case TriggerVar.galley_fraction:
                    return "Returns true if the ratio of the galley fraction to the navy size of the country is at least X.";
                case TriggerVar.galleys_in_province:
                    return "Returns true if there are at least X galleys in the province.";
                case TriggerVar.garrison:
                    return "Returns true if the garrison is at X men.";
                case TriggerVar.gives_military_access_to:
                    return "Returns true if the scoped country gives military access to the specified country.";
                case TriggerVar.gives_fleet_basing_rights_to:
                    return "Returns true if the scoped country gives fleet basing rights to the specified country.";
                case TriggerVar.gold_income:
                    return "Returns true if the country has an income from Trade_goods#Gold_mines|Gold of at least X.";
                case TriggerVar.gold_income_percentage:
                    return "Returns true if in the country the proportion of income from Trade_goods#Gold_mines|Gold is at least X.";
                case TriggerVar.government:
                    return "Returns true if the country has government type X, or government reforms that alter specific mechanics (Harem, Dictatorship).<br>Identifier: monarchy, has_harem, republic, dictatorship, theocracy, tribal, native.";
                case TriggerVar.government_rank:
                    return "Returns true if the country has a government rank of X or higher.";
                case TriggerVar.grown_by_development:
                    return "Returns true if the country's total development has grown by the specified amount";
                case TriggerVar.grown_by_states:
                    return "Returns true if the country's total number of states (not areas) has grown by the specified amount. The states don't have to be fully owned and the provinces don't need full cores.";
                case TriggerVar.great_power_rank:
                    return "Returns true if the country has a great power rank of X or worse.";
                case TriggerVar.guaranteed_by:
                    return "Returns true if the country is guaranteed by X.";
                case TriggerVar.had_recent_war:
                    return "Returns true if the country fought a war within the last X years.";
                case TriggerVar.harmonization_progress:
                    return "Returns true, if the country's current harmonization progress is at least at X.";
                case TriggerVar.harmony:
                    return "Returns true, if the country has at least X harmony.";
                case TriggerVar.has_active_debate:
                    return "Returns true if the country has ongoing debate in parliament.";
                case TriggerVar.has_active_fervor:
                    return "Returns true if the country has activated a fervor effect.";
                case TriggerVar.has_active_policy:
                    return "Returns true if country has the specified policy active.";
                case TriggerVar.has_active_triggered_province_modifier:
                    return "Returns true if province has the specified triggered province modifier.";
                case TriggerVar.has_adopted_cult:
                    return "Returns true if the country has adopted the specified cult.";
                case TriggerVar.has_advisor:
                    return "Returns true if the country has hired an advisor. Warning: Interprets every right side argument as <code>yes</code>.";
                case TriggerVar.has_age_ability:
                    return "Returns true, if the country has the specified age ability.";
                case TriggerVar.has_any_disaster:
                    return "Returns true if the country is currently in a disaster.";
                case TriggerVar.has_border_with_religious_enemy:
                    return "Returns true if the country borders any country of a different religion. Warning: Interprets anything after ‘=’ as “yes”.";
            case TriggerVar.has_building:
                    return "Returns true if there is the specified building in the province.";
                case TriggerVar.has_cardinal:
                    return "Returns true if the province has a cardinal in the curia.";
                case TriggerVar.has_casus_belli_against:
                    return "Returns true if the country has a casus belli against country X.";
                case TriggerVar.has_center_of_trade_of_level:
                    return "Returns true if the country has a center of trade of a least level X.";
                case TriggerVar.has_changed_nation:
                    return "Returns true if playing as a released vassal.";
                case TriggerVar.has_church_aspect:
                    return "Returns true if the country has enabled the church aspect X.";
                case TriggerVar.has_climate:
                    return "Returns true if the province has climate X.";
                case TriggerVar.has_colonial_parent:
                    return "Returns true if the specified country is the colonial parent of the country.";
                case TriggerVar.has_colonist:
                    return "Returns true if scoped province has an active colonist";
                case TriggerVar.has_commanding_three_star:
                    return "Returns true, if country has a three star general.";
                case TriggerVar.has_consort:
                    return "Returns true if the country has a consort.";
                case TriggerVar.has_consort_flag:
                    return "Returns true if the consort flag X is set.";
                case TriggerVar.has_consort_regency:
                    return "Returns true if the country has a consort | consort regency.";
                case TriggerVar.has_construction:
                    return "Returns true if there is the specified construction in progress in the province.<br>Possible values are core, culture, building, merchant, diplomat, missionary, army, navy, canal, great_project etc.";
                case TriggerVar.has_country_flag:
                    return "Returns true if the flag X is set for the country.";
                case TriggerVar.has_country_modifier:
                    return "Returns true if the country has the modifier X.";
                case TriggerVar.has_custom_ideas:
                    return "Returns true if the country has custom ideas.";
                case TriggerVar.has_disaster:
                    return "Returns true if the country is currently in the disaster X.";
                case TriggerVar.has_discovered:
                    return "Returns true if the country has discovered the province with the ID X.";
                case TriggerVar.has_dlc:
                    return "Returns true if the DLC X is enabled.";
                case TriggerVar.has_divert_trade:
                    return "Returns true if the protectorate has divert trade to its overlord.";
                case TriggerVar.has_embargo_rivals:
                    return "Returns true if the subject nation is embargoing overlord's rivals.";
                case TriggerVar.has_empty_adjacent_province:
                    return "Returns true if an adjacent province is uncolonized. Warning: Works only with 'yes'.";
                case TriggerVar.has_estate:
                    return "Returns true if the country has estate X.";
                case TriggerVar.has_estate_province:
                    return "Returns true if the province is controlled by an estate.";
                case TriggerVar.has_estate_loan:
                    return "Returns true if the country has an estate loan.";
                case TriggerVar.has_estate_privilege:
                    return "Returns true if the country has the listed privilege.";
                case TriggerVar.has_faction:
                    return "Returns true if the country has the specified faction.";
                case TriggerVar.has_factions:
                    return "Returns true if country has factions.";
                case TriggerVar.has_female_consort:
                    return "Returns true if the country has a female consort.";
                case TriggerVar.has_female_heir:
                    return "Returns true if the country has a female heir.";
                case TriggerVar.has_first_revolution_started:
                    return "Returns true if a revolution has happened in the world.";
                case TriggerVar.has_flagship:
                    return "Returns true if the country has a flagship.";
                case TriggerVar.has_foreign_consort:
                    return "Returns true if the country has foreign consort.";
                case TriggerVar.has_foreign_heir:
                    return "Returns true if the country has foreign heir.";
                case TriggerVar.has_friendly_reformation_center:
                    return "Returns true if the country has a friendly center of reformation.";
                case TriggerVar.has_game_started:
                    return "Returns true if the game has started.";
                case TriggerVar.has_given_consort_to:
                    return "Returns true if the scoped country has given a consort to the specified country.";
                case TriggerVar.has_guaranteed:
                    return "Returns true if the country has guaranteed country X.";
                case TriggerVar.has_global_flag:
                    return "Returns true if the specified global flag is set.";
                case TriggerVar.has_government_mechanic:
                    return "Returns true, if the country uses the specified government mechanic.";
                case TriggerVar.has_government_power:
                    return "''Description needed''";
                case TriggerVar.has_had_golden_age:
                    return "Returns true, if the country has a golden age.";
                case TriggerVar.has_harmonized_with:
                    return "Returns true, if the country has harmonized with the specified religion or religion group.";
                case TriggerVar.has_harsh_treatment:
                    return "''Description needed''";
                case TriggerVar.has_heir:
                    return "Returns true if the country has an heir (named X).";
                case TriggerVar.has_heir_flag:
                    return "Returns true if the heir flag X is set.";
                case TriggerVar.has_heir_leader_from:
                    return "Returns true if an army in the province is led by an heir from X.";
                case TriggerVar.has_hostile_reformation_center:
                    return "Returns true if the country has a hostile center of reformation.";
                case TriggerVar.has_idea:
                    return "Returns true if the country has the X idea.";
                case TriggerVar.has_idea_group:
                    return "Returns true if the country has chosen the X idea group.";
                case TriggerVar.has_influencing_fort:
                    return "Returns true if the province in the Zone of control of a fort.";
                case TriggerVar.has_institution:
                    return "Returns true if the country has the specified institution.";
                case TriggerVar.has_latent_trade_goods:
                    return "Returns true if the province has the specified latent trade good.";
                case TriggerVar.has_leader:
                    return "Returns true if the country has the leader \"X\".";
                case TriggerVar.has_matching_religion:
                    return "Returns true if the country has the specified religion or syncretic faith.";
                case TriggerVar.has_merchant:
                    return "Returns true if the scoped country has an active merchant in the trade node.";
                case TriggerVar.has_mission:
                    return "Returns true if the country has the specified mission.";
                case TriggerVar.has_missionary:
                    return "Returns true if the province has an active missionary.";
                case TriggerVar.has_monsoon:
                    return "Returns true if the province has monsoon X.";
                case TriggerVar.has_most_province_trade_power:
                    return "Returns true if the country X has most amount of trade power in trade node.";
                case TriggerVar.has_new_dynasty:
                    return "Returns true if the country has a new dynasty.";
                case TriggerVar.has_owner_accepted_culture:
                    return "Returns true if the culture of the province is an accepted culture(NOT primary!) of its owner.";
                case TriggerVar.has_owner_culture:
                    return "Returns true if the province has the primary culture of its owner.";
                case TriggerVar.has_owner_religion:
                    return "Returns true if the province has the religion of its owner.";
                case TriggerVar.has_pasha:
                    return "Returns true if the province has a pasha.";
                case TriggerVar.has_parliament:
                    return "Returns true if the country has a parliament.";
                case TriggerVar.has_personal_deity:
                    return "Returns true if the ruler of the country has picked the specified personal deity.";
                case TriggerVar.has_pillaged_capital_against:
                    return "Returns true if the country has used the Pillage Capital peace treaty against the specified country.";
                case TriggerVar.has_port:
                    return "Returns true if it is a coastal province.";
                case TriggerVar.has_privateers:
                    return "Returns true if the country has privateers in any trade node.";
                case TriggerVar.has_promote_investments:
                    return "Returns true if the country is promoting investments in the specified trade company (region).";
                case TriggerVar.has_province_flag:
                    return "Returns true if the province has the province flag X.";
                case TriggerVar.has_province_modifier:
                    return "Returns true if province has the province modifier X.<br /> (Checks also for triggered province modifiers.)";
                case TriggerVar.has_rebel_faction:
                    return "Returns true if the province is controlled by (the specified) rebel faction.";
                case TriggerVar.has_regency:
                    return "Returns true if the country has a regency.";
                case TriggerVar.has_reform:
                    return "Returns true if the country has the specific government reform.";
                case TriggerVar.government_reform_progress:
                    return "Returns true if the country has x or more reform progress saved up.";
                case TriggerVar.has_removed_fow:
                    return "Returns true if the country has lifted the fog of war from the specified country.";
                case TriggerVar.has_revolution_in_province:
                    return "Returns true if the revolution is present in the province.";
                case TriggerVar.has_ruler:
                    return "Returns true if the country has a ruler named \"X\".";
                case TriggerVar.has_ruler_flag:
                    return "Returns true if the ruler flag X is set for the country.";
                case TriggerVar.has_ruler_leader_from:
                    return "Returns true if an army in the province is led by a ruler from X.";
                case TriggerVar.has_ruler_modifier:
                    return "Returns true the country has the modifier X until the ruler changes.";
                case TriggerVar.has_saved_event_target:
                    return "Returns true if the specified event target has been saved.";
                case TriggerVar.has_scutage:
                    return "Returns true if the vassal pays scutage.";
                case TriggerVar.has_seat_in_parliament:
                    return "Returns true if the province has a seat in parliament.";
                case TriggerVar.has_secondary_religion:
                    return "Returns true if the county has a secondary religion.";
                case TriggerVar.has_send_officers:
                    return "Returns true if the protectorate has received officers.";
                case TriggerVar.has_siege:
                    return "Returns true if the province has siege.";
                case TriggerVar.has_spawned_rebels:
                    return "Returns true if rebels of the specified type are active in the country.";
                case TriggerVar.has_spawned_supported_rebels:
                    return "Returns true if rebels which were supported by the specified country are active in the country.";
                case TriggerVar.has_state_edict:
                    return "Returns true if the state has X edict enabled.";
                case TriggerVar.has_state_patriach:
                    return "Returns true, if the province has any state patriarch.";
                case TriggerVar.has_subsidize_armies:
                    return "Returns true if the march has subsidizes armies.";
                case TriggerVar.has_supply_depot:
                    return "Returns true if the province has a supply depot built by X";
                case TriggerVar.has_support_loyalists:
                    return "Returns true if the subject nation receives support for loyalists.";
                case TriggerVar.has_subject_of_type:
                    return "Returns true if the country has at least one subject of the specified subject type.";
                case TriggerVar.has_switched_nation:
                    return "Returns true if the player has changed nation by playing as a Vassal#Release_a_nation|released vassal.";
                case TriggerVar.has_terrain:
                    return "Returns true if the province has terrain X. Terrains are listed in base game file terrains.txt";
                case TriggerVar.has_trader:
                    return "Returns true if the specified country has a merchant in the trade node.";
                case TriggerVar.has_truce:
                    return "Returns true if the country has a truce.";
                case TriggerVar.has_unembraced_institution:
                    return "Returns true if the country has not embraced the specified institution.";
                case TriggerVar.has_unified_culture_group:
                    return "Returns true, if the country owns all provinces of its culture group.";
                case TriggerVar.has_unit_type:
                    return "Returns true if the country has selected unit type X as preferable unit.";
                case TriggerVar.has_unlocked_cult:
                    return "Returns true if the country has unlocked the specified cult.";
                case TriggerVar.has_wartaxes:
                    return "Returns true if the country has raised war taxes.";
                case TriggerVar.has_winter:
                    return "Returns true if the province has winter X.";
                case TriggerVar.have_had_reform:
                    return "Returns true if the country has previously had the specific government reform.";
                case TriggerVar.heavy_ship_fraction:
                    return "Returns true if the ratio of the heavy ship fraction to the navy size of the country is at least X.";
                case TriggerVar.heavy_ships_in_province:
                    return "Returns true if there are at least X heavy ships in the province.";
                case TriggerVar.heir_adm:
                    return "Returns true if the country has an heir with an administrative skill of at least X.";
                case TriggerVar.heir_age:
                    return "Returns true if the country has an heir that is at least X years old.<br>Note: Always returns false if there is no heir.";
                case TriggerVar.heir_dip:
                    return "Returns true if the country has an heir with a diplomatic skill of at least X.";
                case TriggerVar.heir_claim:
                    return "Returns true if the country has an heir with a claim strength of at least X.";
                case TriggerVar.heir_culture:
                    return "Returns true, if the country's heir has the specified culture. Can utilise Variables|Event Scope Values.";
                case TriggerVar.heir_has_consort_dynasty:
                    return "Returns true if the country’s heir and consort have the same dynasty.";
                case TriggerVar.heir_has_personality:
                    return "Returns true if the country’s heir has the specified personality.";
                case TriggerVar.heir_has_ruler_dynasty:
                    return "Returns true if the country’s heir and ruler have the same dynasty.";
                case TriggerVar.heir_mil:
                    return "Returns true if the country has an heir with a military skill of at least X.";
                case TriggerVar.heir_nationality:
                    return "Returns true if the country has an heir with nationality X.";
                case TriggerVar.heir_religion:
                    return "Returns true, if the country's heir has the specified religion. Can utilise Variables|Event Scope Values.";
                case TriggerVar.higher_development_than:
                    return "Returns true if the scope has a higher development than the province.";
                case TriggerVar.highest_value_trade_node:
                    return "Returns true if the trade node is the highest valued trade node in the world. The value is calculated as total trade value minus outgoing trade value.";
                case TriggerVar.historical_friend_with:
                    return "Returns true if the countries are historical friends.";
                case TriggerVar.historical_rival_with:
                    return "Returns true if the countries are historical rivals";
                case TriggerVar.holy_order:
                    return "Returns true if the province has the specific Holy Order active.";
                case TriggerVar.horde_unity:
                    return "Returns true if the country has a horde unity of at least X.";
                case TriggerVar.hre_heretic_religion:
                    return "Returns true if the specified religion is the opposition religion of the HRE.<br>Note: No localisation for the negation.";
                case TriggerVar.hre_leagues_enabled:
                    return "Returns true religious leagues are enabled.";
                case TriggerVar.hre_reform_passed:
                    return "Returns true if the specific imperial reform is enacted.";
                case TriggerVar.hre_religion:
                    return "Returns true if the specified religion is the dominant faith of the HRE.";
                case TriggerVar.hre_religion_locked:
                    return "Returns true if an official faith of the HRE has been permanently established.";
                case TriggerVar.hre_religion_treaty:
                    return "Returns true if the treaty of religious peace in the HRE has been signed.";
                case TriggerVar.hre_size:
                    return "Returns true if the HRE contains at least X members.";
                case TriggerVar.imperial_influence:
                    return "Returns true if the emperor of the HRE has an imperial authority of at least X.";
                case TriggerVar.imperial_mandate:
                    return "Returns true, if the emperor of China has at least X mandate.";
                case TriggerVar.in_golden_age:
                    return "Returns true, if the country currently is in a golden age.";
                case TriggerVar.infantry_fraction:
                    return "Returns true if the ratio of the infantry fraction to the army size of the country is at least X.";
                case TriggerVar.infantry_in_province:
                    return "Returns true if there are at least X infantry units in the province.";
                case TriggerVar.inflation:
                    return "Returns true if the country has an inflation of at least X";
                case TriggerVar.innovativeness:
                    return "Returns true if the countries has at least X innovativeness.";
                case TriggerVar.invested_papal_influence:
                    return "Returns true if the country has invested at least X papal influence in the election of the next papal controller.";
                case TriggerVar.in_league:
                    return "Returns true if the countries is in the X league.";
                case TriggerVar.ironman:
                    return "Returns true if the ironman mode is enabled";
                case TriggerVar.is_advisor_employed:
                    return "Returns true if the advisor with the ID X is employed.";
                case TriggerVar.is_all_concessions_in_council_taken:
                    return "Returns true if all concessions in the Council of Trent have been taken.";
                case TriggerVar.is_at_war:
                    return "Returns true if the country is at war.";
                case TriggerVar.is_backing_current_issue:
                    return "Returns true if the province is backing the current issue in the parliament.";
                case TriggerVar.is_bankrupt:
                    return "Returns true if the country is bankrupt.";
                case TriggerVar.is_blockaded:
                    return "Returns true if the province is blockaded.";
                case TriggerVar.is_blockaded_by:
                    return "Returns true if the province is blockaded by the specified country.";
                case TriggerVar.is_capital:
                    return "Returns true if the province is a capital.";
                case TriggerVar.is_capital_of:
                    return "Returns true if the province the capital of the specified country.";
                case TriggerVar.is_city:
                    return "Returns true if the province is a city, i.e. has a population of at least 1000.";
                case TriggerVar.is_claim:
                    return "Returns true if the country has a claim on the province with the ID X. Returns true if the specified country has a claim on the province.";
                case TriggerVar.is_client_nation:
                    return "Returns true if the country is a client state.";
                case TriggerVar.is_client_nation_of:
                    return "Returns true if the country is a client state of X.";
                case TriggerVar.is_colonial_nation:
                    return "Returns true if the country is a non-independent colonial nation.";
                case TriggerVar.is_colonial_nation_of:
                    return "Returns true if the country is a colonial nation of X.";
                case TriggerVar.is_colony:
                    return "Returns true if the province is a colony.";
                case TriggerVar.is_core:
                    return "Returns true if the country has a core on the province with the ID X. Returns true if the country X has a core on the province.";
                case TriggerVar.is_council_enabled:
                    return "Returns true if the Council of Trent has started.";
                case TriggerVar.is_crusade_target:
                    return "Returns true if the country is the target of a crusade.";
                case TriggerVar.is_defender_of_faith:
                    return "Returns true if the country is the defender of the faith.";
                case TriggerVar.is_defender_of_faith_of_tier:
                    return "Returns true if the country is the defender of the faith of at least tier X. Works even without the Emperor DLC, even though the tier is not shown in game in that case.";
                case TriggerVar.is_dynamic_tag:
                    return "Returns true, if the country is a dynamically created tag (e.g. client states).";
                case TriggerVar.is_elector:
                    return "Returns true if the country is an elector of the HRE.";
                case TriggerVar.is_emperor:
                    return "Returns true if the country is the Holy Roman Empire#Emperor|emperor of the HRE.";
                case TriggerVar.is_emperor_of_china:
                    return "Returns true if the country is the emperor of China.";
                case TriggerVar.is_empty:
                    return "Returns true if province is empty";
                case TriggerVar.is_enemy:
                    return "Returns true if country X has rivalled the country.";
                case TriggerVar.is_excommunicated:
                    return "Returns true if the ruler of the country is excommunicated.";
                case TriggerVar.is_federation_leader:
                    return "Returns true if the country is a federation leader.";
                case TriggerVar.is_female:
                    return "Returns true if ruler of the country is female.";
                case TriggerVar.is_force_converted:
                    return "Returns true if country has been force-converted via either religious rebels or an Enforce Religion peace treaty.<br>Note: The subject interaction Enforce Religion does not cause the subject to be considered as force-converted.";
                case TriggerVar.is_former_colonial_nation:
                    return "Returns true if the country is a colonial nation that has gained independence.";
                case TriggerVar.is_foreign_claim:
                    return "Returns true if the province is a (permanent) claim of any country which is not the owner of the province";
                case TriggerVar.is_great_power:
                    return "Returns true if the country is a great power.";
                case TriggerVar.is_harmonizing_with:
                    return "Returns true, if the country is currently harmonizing with the specified religion or religion group.";
                case TriggerVar.is_heir_leader:
                    return "Returns true if the heir of the country is a general.";
            case TriggerVar.is_hegemon:
                    return "Returns true if the country is considered one of the three Hegemon types.";
                case TriggerVar.is_hegemon_of_type:
                    return "Returns true if the country holds the specific Hegemony.";
                case TriggerVar.is_imperial_ban_allowed:
                    return "Returns true if the Holy Roman Empire#Emperor|emperor has a casus belli on occupiers of the Empire.<br>Note: Enabled/Disabled with Call for Reichsreform.";
                case TriggerVar.is_incident_active:
                    return "Returns true, if the specified incident is active.";
                case TriggerVar.is_incident_happened:
                    return "Returns true, if the specified incident already happened.";
                case TriggerVar.is_incident_possible:
                    return "Returns true, if the specified incident can possibly happen.";
                case TriggerVar.is_incident_potential:
                    return "Returns true, if the specified incident is visible and therefor can happen in the future.";
                case TriggerVar.is_institution_enabled:
                    return "Returns true if the specified institution has been discovered.";
                case TriggerVar.is_institution_origin:
                    return "Returns true if the province is the origin of the specified institution.";
                case TriggerVar.is_in_capital_area:
                    return "Returns true if the province is connected (i.e. has a land connection including straits) to the capital of its owner.";
                case TriggerVar.is_in_coalition:
                    return "Returns true if the country is in a coalition.";
                case TriggerVar.is_in_coalition_war:
                    return "Returns true if the country is fighting a coalition war.";
                case TriggerVar.is_in_deficit:
                    return "Returns true if the country is running a deficit.";
                case TriggerVar.is_in_extended_regency:
                    return "Returns true if the country is inside the period an extended regency.";
                case TriggerVar.is_in_league_war:
                    return "Returns true if the country is in a religious league war.";
                case TriggerVar.is_in_trade_league:
                    return "Returns true if the country is a member of a trade league.";
                case TriggerVar.is_in_trade_league_with:
                    return "Returns true if the country is a member of the same trade league as country X.";
                case TriggerVar.is_island:
                    return "Returns true if the province is an Island#is_island|island, i.e. has no neighboring land province and no reachable province via a strait.";
                case TriggerVar.is_league_enemy:
                    return "Returns true if country X is league enemy of the country('s league).";
                case TriggerVar.is_lacking_institutions:
                    return "Returns true if the country is lacking any institution.";
                case TriggerVar.is_league_friend:
                    return "Returns true, if the country is in the same religious league as the specified country.";
                case TriggerVar.is_league_leader:
                    return "Returns true if the country leads a religious league.";
                case TriggerVar.is_lesser_in_union:
                    return "Returns true if the country is the lesser partner in a personal union.";
                case TriggerVar.is_looted:
                    return "Returns true if the province is looted.";
                case TriggerVar.is_monarch_leader:
                    return "Returns true if the rulerof the country is a general";
            case TriggerVar.is_month:
                    return "Returns true if the current month is at least X (zero based).";
                case TriggerVar.is_march:
                    return "Returns true if the country is a march.";
                case TriggerVar.is_neighbor_of:
                    return "Returns true if the country is neighbor of X.";
                case TriggerVar.is_node_in_trade_company_region:
                    return "Returns true, if the province's trade node is in a trade company region.";
                case TriggerVar.is_nomad:
                    return "Returns true if country's government is nomadic.<br>(Note: Only the Steppe Nomads government is flagged as nomadic.)";
                case TriggerVar.is_orangists_in_power:
                    return "Returns true if in the country the orangists are in power.";
                case TriggerVar.is_origin_of_consort:
                    return "Returns true if the specified country is the origin country of the scoped country’s consort.";
                case TriggerVar.is_overseas:
                    return "Returns true if the province is overseas";
                case TriggerVar.is_overseas_subject:
                    return "Returns true if the subject is overseas.";
                case TriggerVar.is_owned_by_trade_company:
                    return "Returns true if the province belongs to a trade company.";
                case TriggerVar.is_papal_controller:
                    return "Returns true if the country is the papal controller.";
                case TriggerVar.is_part_of_hre:
                    return "Returns true if the country/province is part of the HRE.";
                case TriggerVar.is_permanent_claim:
                    return "Returns true if the province is a permanent claim of X.";
                case TriggerVar.is_playing_custom_nation:
                    return "Returns true if the country is a player-designed custom nation.";
                case TriggerVar.is_possible_march:
                    return "Returns true if vassal X is a possible march of the country.";
                case TriggerVar.is_possible_vassal:
                    return "Returns true if country X is releasable as vassal of the country.";
                case TriggerVar.is_previous_papal_controller:
                    return "Returns true if the country is the previous papal controller.";
                case TriggerVar.is_prosperous:
                    return "Returns true if the province is prosperous. NOTE: Does not have a tooltip.";
                case TriggerVar.is_protectorate:
                    return "Returns true if the country is a protectorate";
                case TriggerVar.is_random_new_world:
                    return "Returns true if playing with a random New World.";
                case TriggerVar.is_reformation_center:
                    return "Returns true if the province is a reformation center.";
                case TriggerVar.is_religion_grant_colonial_claim:
                    return "Returns true if the province has been granted to any country, to no country or to a specific country.";
                case TriggerVar.is_religion_enabled:
                    return "Returns true if the specified religion is enabled.";
                case TriggerVar.is_religion_reformed:
                    return "Returns true if the country has Pagan denominations#Mesoamerican_and_South_American_religions|reformed their religion.";
                case TriggerVar.is_renting_condottieri_to:
                    return "Returns true if the country is renting condottieri to the specified country.";
                case TriggerVar.is_revolution_target:
                    return "Returns true if the country is the revolution target.";
                case TriggerVar.is_rival:
                    return "Returns true if country X is a rival of the country.";
                case TriggerVar.is_ruler_commanding_unit:
                    return "Returns true if the unit is commanded by the owner's ruler, heir or consort.";
                case TriggerVar.is_sea:
                    return "Returns true if the province is sea.<br>Mostly used for trade nodes.";
                case TriggerVar.is_state:
                    return "Returns true if the province is in a state.";
                case TriggerVar.is_state_core:
                    return "Returns true if the country has a state core on the province with the ID X.";
                case TriggerVar.is_statists_in_power:
                    return "Returns true if in the country the statists are in power.";
                case TriggerVar.is_strongest_trade_power:
                    return "Returns true if the specified country has the most trade power in the area.";
                case TriggerVar.is_subject:
                    return "Returns true if the country is a subject.";
                case TriggerVar.is_subject_of:
                    return "Returns true if the country is a subject of X.";
                case TriggerVar.is_subject_of_type:
                    return "Returns true if the country is a subject of subject type.";
                case TriggerVar.is_supporting_independence_of:
                    return "Returns true if the country is supporting the Diplomacy#Support_independence|independence of the specified country.";
                case TriggerVar.is_territorial_core:
                    return "Returns true if the country has a territorial core on the province with the ID X.";
                case TriggerVar.is_territory:
                    return "Returns true if the province is in a territory.";
                case TriggerVar.is_threat:
                    return "Returns true if country X is threatened by the country.";
                case TriggerVar.is_trade_league_leader:
                    return "Returns true if the country is the leader of a trade league.";
                case TriggerVar.is_tribal:
                    return "Returns true if the country has a tribal government.";
            case TriggerVar.is_vassal:
                    return "Returns true if the country is a vassal.";
                case TriggerVar.is_wasteland:
                    return "Returns true if the province is wasteland.";
                case TriggerVar.is_year:
                    return "Returns true if the current year is at least X.";
                case TriggerVar.island:
                    return "Returns true if the province is an Island#island|island, i.e. has no neighboring land provinces.";
                case TriggerVar.isolationism:
                    return "Returns true, if the country has an isolationism level of at least X.";
                case TriggerVar.janissary_percentage:
                    return "Returns true if the country has an army of at least X k janissaries.";
                case TriggerVar.junior_union_with:
                    return "Returns true if the country is the junior partner in a personal union under country X.";
                case TriggerVar.karma:
                    return "Returns true if the country has a karma of at least X.";
                case TriggerVar.knows_country:
                    return "Returns true if the country has knowledge of country X.";
                case TriggerVar.land_forcelimit:
                    return "Returns true if the country has a land force limit of at least X.";
                case TriggerVar.land_maintenance:
                    return "Returns true if the country has set its land maintenance to X.";
                case TriggerVar.land_morale:
                    return "Returns true if the country has a morale of armies of at least X.";
                case TriggerVar.last_mission:
                    return "Returns true if the last mission of the country was the specified mission.";
                case TriggerVar.legitimacy:
                    return "Returns true if the country has at least X legitimacy.";
                case TriggerVar.legitimacy_equivalent:
                    return "Returns true, if the country's legitimacy equivalent (legitimacy, republican tradition, devotion, horde unity, meritocracy etc.) is at least X.";
                case TriggerVar.legitimacy_or_horde_unity:
                    return "Returns true if the country has at least X legitimacy or horde unity.";
                case TriggerVar.liberty_desire:
                    return "Return true if the subject has a liberty desire of at least X.";
                case TriggerVar.light_ship_fraction:
                    return "Returns true if the ratio of the light ship fraction to the navy size of the country is at least X.";
                case TriggerVar.light_ships_in_province:
                    return "Returns true if there are at least X light ships in the province.";
                case TriggerVar.likely_rebels:
                    return "Returns true if the province has the specified rebel faction as likely rebels.";
                case TriggerVar.local_autonomy:
                    return "Returns true if the province has a local autonomy of at least X.";
                case TriggerVar.local_autonomy_above_min:
                    return "Returns true, if the province's local autonomy is at least X above the province's minimum local autonomy.";
                case TriggerVar.luck:
                    return "Returns true if the country is a lucky nation. (AI controlled counties only.)";
                case TriggerVar.march_of:
                    return "Returns true if the country is a march under country X.";
                case TriggerVar.manpower:
                    return "Returns true if the country has at least X*1000 available manpower.";
                case TriggerVar.manpower_percentage:
                    return "Returns true if the country has a manpower level of at least X%.";
                case TriggerVar.marriage_with:
                    return "Returns true if the country has a royal marriage with X.";
                case TriggerVar.max_manpower:
                    return "Returns true if the country has at least X*1000 maximum manpower.";
                case TriggerVar.mercantilism:
                    return "Returns true if the country's mercantilism is at least X.";
                case TriggerVar.meritocracy:
                    return "Returns true, if the country has a meritocracy value of at least X.";
                case TriggerVar.mil:
                    return "Returns true if the country has a ruler with a military skill of at least X.";
                case TriggerVar.militarised_society:
                    return "Returns true if the country has a militarization of country of at least X.";
                case TriggerVar.mil_power:
                    return "Returns true if the country has at least X military power.";
                case TriggerVar.mil_tech:
                    return "Returns true if the country has an military technology of at least level X.";
                case TriggerVar.mission_completed:
                    return "Returns true if the country has completed mission X.";
                case TriggerVar.monthly_income:
                    return "Returns true if the country has a monthly income of at least X.";
                case TriggerVar.monthly_adm:
                    return "Returns true if the country gains at least X adm power every month.";
                case TriggerVar.monthly_dip:
                    return "Returns true if the country gains at least X dip power every month.";
                case TriggerVar.monthly_mil:
                    return "Returns true if the country gains at least X mil power every month.";
                case TriggerVar.months_of_ruling:
                    return "Returns true if the country has a ruler that has ruled for at least X months.";
                case TriggerVar.months_since_defection:
                    return "Returns true if the province defected within the last X months.";
                case TriggerVar.nationalism:
                    return "Returns true if province has at least X years of separatism remaining.";
                case TriggerVar.national_focus:
                    return "Returns true if country has set the national focus to X.";
                case TriggerVar.nation_designer_points:
                    return "Returns true if at least X points were used when creating the custom nation.";
                case TriggerVar.native_ferocity:
                    return "Returns true if ferocity of natives in the province is at least X.";
                case TriggerVar.native_hostileness:
                    return "Returns true if hostileness of natives in the province is at least X.";
                case TriggerVar.native_policy:
                    return "Returns true if country has the specified native policy.";
                case TriggerVar.native_size:
                    return "Returns true if size of natives in the province is at least X.";
                case TriggerVar.naval_forcelimit:
                    return "Returns true if the country has a naval force limit of at least X.";
                case TriggerVar.naval_maintenance:
                    return "Returns true if the country's naval maintenance slider is at least X fraction of maximum.";
                case TriggerVar.naval_morale:
                    return "Returns true if the country has a morale of navies of at least X.";
                case TriggerVar.navy_size:
                    return "Returns true if the current scope has a navy of at least X ships.";
                case TriggerVar.navy_size_percentage:
                    return "Returns true if the ratio of the total navy size of the country to its naval force limit is at least X.";
                case TriggerVar.navy_tradition:
                    return "Returns true if the country has a navy tradition of at least X.";
                case TriggerVar.normal_or_historical_nations:
                    return "Returns true if game is set to use Options#Nations|normal or historical nations.";
                case TriggerVar.normal_province_values:
                    return "Returns true if game is set to use Options#Province_Tax_and_Manpower|normal province values.";
                case TriggerVar.num_accepted_cultures:
                    return "Returns true, if the country has at least X accepted cultures.";
                case TriggerVar.num_free_building_slots:
                    return "Returns true if the province has at least X building slots remaining.";
                case TriggerVar.num_of_active_blessings:
                    return "Returns true if the country has enabled at least X blessings.";
                case TriggerVar.num_of_admirals:
                    return "Returns true if the country has at least X admirals.";
                case TriggerVar.num_of_admirals_with_traits:
                    return "Returns true if the country has at least X admirals with traits.";
                case TriggerVar.num_of_allies:
                    return "Return true if the country has at least X allies.";
                case TriggerVar.num_of_artillery:
                    return "Returns true if the country has at least X artillery regiments.";
                case TriggerVar.num_of_aspects:
                    return "Returns true if the country has at least X church aspects.";
                case TriggerVar.num_of_banners:
                    return "Returns true, if the country has at least X banner units.";
                case TriggerVar.num_of_buildings_in_province:
                    return "Returns true, if the province has at least X buildings.";
                case TriggerVar.num_of_captured_ships_with_boarding_doctrine:
                    return "Returns true if the country has captured at least X ships while having the ‘Ship Boarding’ naval doctrine.";
                case TriggerVar.num_of_centers_of_trade:
                    return "Returns true if the country has at least X centers of trade.";
                case TriggerVar.num_of_cardinals:
                    return "Returns true if the country has at least X cardinals in the Holy See.";
                case TriggerVar.num_of_cavalry:
                    return "Returns true if the country has at least X cavalry regiments.";
                case TriggerVar.num_of_cawa:
                    return "\tReturns true if the country has at least X cawa regiments.";
                case TriggerVar.num_of_cities:
                    return "Returns true if the country owns at least X cities.";
                case TriggerVar.num_of_coalition_members:
                    return "Returns true if the country is in a coalition of at least X members.";
                case TriggerVar.num_of_colonies:
                    return "Returns true if the country has at least X unfinished colonies.";
                case TriggerVar.num_of_colonists:
                    return "Returns true if the country has at least X colonists.";
                case TriggerVar.num_of_conquistadors:
                    return "Returns true if the country has at least X conquistadors.";
                case TriggerVar.num_of_consorts:
                    return "Returns true, if the ruler of the country had at least X separate consorts.";
                case TriggerVar.num_of_continents:
                    return "Returns true, if the country owns provinces on at least X continents. Only provinces owned by the country itself are taken into account, not provinces owned by subjects.";
                case TriggerVar.num_of_cossacks:
                    return "Returns true, if the country has at least X cossack units.";
                case TriggerVar.num_of_custom_nations:
                    return "Returns true if there are at least X custom nations in the game.";
                case TriggerVar.num_of_diplomatic_relations:
                    return "Returns true if the country has at least X diplomatic relations.";
                case TriggerVar.num_of_diplomats:
                    return "Returns true if the country has at least X diplomats.";
                case TriggerVar.num_of_electors:
                    return "Returns true if there are at least X electors of the HRE.";
                case TriggerVar.num_of_explorers:
                    return "Returns true if the country has at least X explorers.";
                case TriggerVar.num_of_foreign_hre_provinces:
                    return "Returns true if there are at least X provinces owned by non-member states or subjects of non-member states.";
                case TriggerVar.num_of_free_diplomatic_relations:
                    return "Returns true if the country has at least X free diplomatic relations slots.";
                case TriggerVar.num_of_galley:
                    return "Returns true if the country has at least X galleys.";
                case TriggerVar.num_of_generals:
                    return "Returns true if the country has at least X generals.";
                case TriggerVar.num_of_generals_with_traits:
                    return "Returns true if the country has at least X generals with traits.";
                case TriggerVar.num_of_harmonized:
                    return "Returns true, if the country has harmonized with at least X religions or religion groups.";
                case TriggerVar.num_of_heavy_ship:
                    return "Returns true if the country has at least X heavy ships.";
                case TriggerVar.num_of_infantry:
                    return "Returns true if the country has at least X infantry regiments.";
                case TriggerVar.num_of_light_ship:
                    return "Returns true if the country has at least X light ships.";
                case TriggerVar.num_of_loans:
                    return "Returns true if the country has at least X loans.";
                case TriggerVar.num_of_marches:
                    return "Returns true if the country has at least X marches.";
                case TriggerVar.num_of_marines:
                    return "Returns true if the country has enabled at least X marines.";
                case TriggerVar.num_of_mercenaries:
                    return "Returns true if the country has at least X mercenaries.";
                case TriggerVar.num_of_merchants:
                    return "Returns true if the country has at least X merchants.";
                case TriggerVar.num_of_missionaries:
                    return "Returns true if the country has at least X missionaries.";
                case TriggerVar.num_of_owned_and_controlled_institutions:
                    return "Returns true, if the country owns and controls at least X provinces that are institution origins.";
                case TriggerVar.num_of_ports:
                    return "Returns true if country owns at least X home ports (in lands contiguously connected to the capital).";
                case TriggerVar.num_of_ports_blockading:
                    return "Returns true if the country blockades at least X ports.";
                case TriggerVar.num_of_powerful_estates:
                    return "Returns true if the country has at least X estates with at least 70 influence.";
                case TriggerVar.num_of_protectorates:
                    return "Returns true if the country has at least X protectorates.";
                case TriggerVar.num_of_provinces_in_states:
                    return "Returns true if the country has at least X provinces is states.";
                case TriggerVar.num_of_provinces_in_territories:
                    return "Returns true if the country has at least X provinces is territories.";
                case TriggerVar.num_of_rajput:
                    return "Returns true if the country has at least X rajput regiments.";
                case TriggerVar.num_of_rebel_armies:
                    return "Returns true if the number of rebel armies in the country is at least X.";
                case TriggerVar.num_of_rebel_controlled_provinces:
                    return "Returns true if the number of rebel controlled provinces in the country is at least X.";
                case TriggerVar.num_of_revolts:
                    return "Returns true if the number of revolts in the country is at least X.<br>''The same as ‘num_of_rebel_controlled_provinces’.''";
                case TriggerVar.num_of_royal_marriages:
                    return "Returns true if the country has at least X royal marriages.";
                case TriggerVar.num_of_ruler_traits:
                    return "Returns true if the ruler has at least X personality traits.";
                case TriggerVar.num_of_states:
                    return "Returns true if the country has at least X states.";
                case TriggerVar.num_of_streltsy:
                    return "Returns true, if the country has at least X streltsy units.";
                case TriggerVar.num_of_strong_trade_companies:
                    return "Returns true if the country has at least X strong trade companies.";
                case TriggerVar.num_of_subjects:
                    return "Returns true if the country is the overlord of at least X subject countries of any type.";
                case TriggerVar.num_of_territories:
                    return "Returns true, if the country has at least X territories (areas that aren't states).";
                case TriggerVar.num_of_times_improved:
                    return "Returns true if the development of the province was improved at least X times.";
                case TriggerVar.num_of_times_improved_by_owner:
                    return "Returns true if the development of the province was improved at least X times by the current owner.";
                case TriggerVar.num_of_times_used_pillage_capital:
                    return "Returns true if the country has used the Pillage Capital peace treaty at least X times.";
                case TriggerVar.num_of_times_used_transfer_development:
                    return "Returns true if the country has used the Concentrate Development action at least X times.";
                case TriggerVar.num_of_total_ports:
                    return "Returns true if the country owns at least X total ports (anywhere in the world).";
                case TriggerVar.num_of_trade_companies:
                    return "Returns true if the country has at least X trade companies.";
                case TriggerVar.num_of_trade_embargos:
                    return "Returns true if the country has at least X trade embargos.";
                case TriggerVar.num_of_trading_bonuses:
                    return "Returns true if the country hasat least X ‘trading in’ bonuses.";
                case TriggerVar.num_of_transport:
                    return "Returns true if the country has at least X transports.";
                case TriggerVar.num_of_trusted_allies:
                    return "Returns true if the country has at least X allies with 100 trust.";
                case TriggerVar.num_of_unions:
                    return "Returns true if the country has at least X personal unions.";
                case TriggerVar.num_of_unlocked_cults:
                    return "Returns true if the country has unlocked at least X cults.";
                case TriggerVar.num_of_war_reparations:
                    return "Returns true if the country receives war reparations from at least X countries.";
                case TriggerVar.num_ships_privateering:
                    return "Returns true if country has at least X ships privateering.";
                case TriggerVar.offensive_war_with:
                    return "Returns true if the country is in an offensive war with country X.";
                case TriggerVar.overextension_percentage:
                    return "Returns true if country has at least X% overextension.";
                case TriggerVar.overlord_of:
                    return "Returns true if the country is the overlord of X.";
                case TriggerVar.overseas_provinces_percentage:
                    return "Returns true if the country has X percentage of overseas provinces.";
                case TriggerVar.owned_by:
                    return "Returns true if the province is owned by the country X.";
                case TriggerVar.owns:
                    return "Returns true if the country owns the specified province.";
                case TriggerVar.owns_core_province:
                    return "Returns true if the country owns and has a core on the specified province.";
                case TriggerVar.owns_or_non_sovereign_subject_of:
                    return "Returns true, if the country or a subject that is not categorized as \"sovereign\" (e.g. tributary states are excluded) owns the specified province.";
                case TriggerVar.owns_or_subject_of:
                    return "Returns true, if the country or a subject owns the specified province.";
                case TriggerVar.papacy_active:
                    return "Returns true if the papacy is active.";
                case TriggerVar.papal_influence:
                    return "Returns true if the country's papal influence is at least X.";
                case TriggerVar.patriarch_authority:
                    return "Returns true if the country's patriarch authority is at least X.";
                case TriggerVar.percentage_backing_issue:
                    return "Returns true if at least the Xth part of the seats in the parliament is backing for the current issue.";
                case TriggerVar.personality:
                    return "Returns true if the country has a ruler which personality is X. Is limited to ai_personalities listed in base game file 'common/ai_personalities/00_ai_personalities.txt'. For ruler personality, use ruler_has_personality";
                case TriggerVar.piety:
                    return "Returns true if the country's piety is at least X.";
                case TriggerVar.preferred_emperor:
                    return "Returns true if an elector has the country X is the preferred emperor.";
                case TriggerVar.prestige:
                    return "Return true if the country has a prestige of at least X.";
                case TriggerVar.previous_owner:
                    return "Returns true if the previous owner of the province was X.";
                case TriggerVar.power_projection:
                    return "Returns true if country has a Power_projection of at least X. Appeared in 1.30";
                case TriggerVar.primary_culture:
                    return "Returns true if the country's primary culture is X.";
                case TriggerVar.primitives:
                    return "Returns true if the country is primitive.";
                case TriggerVar.production_efficiency:
                    return "Returns true if the country has a production efficiency of at least X.";
                case TriggerVar.production_income_percentage:
                    return "Returns true if the ratio of production income to total income is at least X.";
                case TriggerVar.province_id:
                    return "Returns true if the province has the ID X.";
                case TriggerVar.province_is_on_an_island:
                    return "Returns true if the province is Island#province_is_on_an_island|on an island.";
                case TriggerVar.province_getting_expelled_minority:
                    return "Returns true if the province is getting minorities.";
                case TriggerVar.province_group:
                    return "Returns true if the province belongs to the specified province group.";
                case TriggerVar.province_size:
                    return "Returns true if the province, in '''provinces.bmp''', contains X pixels.";
                case TriggerVar.province_trade_power:
                    return "Returns true if trade power generated by the province is at least X.";
                case TriggerVar.provinces_on_capital_continent_of:
                    return "Returns true if the country has a province on the continent with the capital of the specified country.";
                case TriggerVar.pure_unrest:
                    return "Returns true if the province has a base unrest of at least X.";
                case TriggerVar.range:
                    return "Returns true if the province is within the range of the specified country.";
                case TriggerVar.real_day_of_year:
                    return "Returns true, if today is X. Refers to the actual real day (probably takes system time).";
                case TriggerVar.real_month_of_year:
                    return "Returns true if the month of the year in reality is at least X (January &#8793; 0)";
                case TriggerVar.reform_desire:
                    return "Returns true if the reform desire is at least X%.";
                case TriggerVar.receives_military_access_from:
                    return "Returns true if the scoped country receives military access from the specified country.";
                case TriggerVar.receives_fleet_basing_rights_from:
                    return "Returns true if the scoped country receives fleet basing rights from the specified country.";
                case TriggerVar.reform_level:
                    return "Retrurn true if the country has reached at least X levels of government reforms.";
                case TriggerVar.region:
                    return "Returns true if the province is part of the region X.";
                case TriggerVar.religion:
                    return "Returns true if the country/province has religion X.";
                case TriggerVar.religion_group:
                    return "Returns true if the country/province has a religion of the specified religious group.";
                case TriggerVar.religious_unity:
                    return "Returns true if the country's religious unity is at least X.";
                case TriggerVar.republican_tradition:
                    return "Returns true if country's republican tradition is at least X.";
                case TriggerVar.revanchism:
                    return "Returns true if country's revanchism is at least X.";
                case TriggerVar.revolt_percentage:
                    return "Returns true if at least the Xth part of the provinces of the country have revolts.";
                case TriggerVar.revolution_target_exists:
                    return "Returns true if there is a revolutionary target in the world.";
                case TriggerVar.ruler_age:
                    return "Returns true if the country has a ruler that is at least X years old.";
                case TriggerVar.ruler_consort_marriage_length:
                    return "''Needs description''";
                case TriggerVar.ruler_culture:
                    return "Returns true, if the country's ruler has the specified culture. Can utilise Variables|Event Scope Values.";
                case TriggerVar.ruler_has_personality:
                    return "Returns true if the country’s ruler has the specified personality.";
                case TriggerVar.ruler_is_foreigner:
                    return "Returns true if the country has foreign ruler.";
                case TriggerVar.ruler_religion:
                    return "Returns true, if the country's ruler has the specified religion. Can utilise Variables|Event Scope Values.";
                case TriggerVar.sailors:
                    return "Returns true if the country has at least X sailors.";
                case TriggerVar.sailors_percentage:
                    return "Returns true if the country has a sailor level of at least X%.";
                case TriggerVar.max_sailors:
                    return "Returns true if the country has at least X maximum sailors.";
                case TriggerVar.same_continent:
                    return "Returns true, if specified province is on the same continent as the province. If used in country scope, capitals are checked.";
                case TriggerVar.secondary_religion:
                    return "Returns true the secondary religion of the country is X.";
                case TriggerVar.senior_union_with:
                    return "Returns true if the country is the senior partner in a personal union over country X.";
                case TriggerVar.sieged_by:
                    return "Returns true if the province is being besieged by country X.";
                case TriggerVar.splendor:
                    return "Returns true, if the country has at least X splendor.";
                case TriggerVar.stability:
                    return "Returns true if the country has a stability of at least X.";
                case TriggerVar.start_date:
                    return "Returns true if the start date of the campaign is X.";
                case TriggerVar.started_in:
                    return "Returns true if the start date of the campaign is X or after.";
                case TriggerVar.statists_vs_orangists:
                    return "Returns true if Statists vs Orangists is at least X.";
                case TriggerVar.subsidised_percent_amount:
                    return "Returns true if the country receives subsidies of at least X% of its monthly income.";
                case TriggerVar.succession_claim:
                    return "Returns true if the country has claim throne|claimed the throne of the country X.";
                case TriggerVar.superregion:
                    return "Returns true if the province belongs to the superregion X.";
                case TriggerVar.tag:
                    return "Returns true if the country is the specified country.";
                case TriggerVar.tariff_value:
                    return "Returns true if the colonial nation pays at least X% tariffs.";
                case TriggerVar.tax_income_percentage:
                    return "Returns true if the ratio of tax income to total income is at least X.";
                case TriggerVar.tech_difference:
                    return "Returns true if the scoped country is at least X technologies ahead (compared to the country).";
                case TriggerVar.technology_group:
                    return "Returns true if the country has the technology group X.";
                case TriggerVar.tolerance_to_this:
                    return "Returns true if the country has a tolerance of at least X towards the religion of the country or scoped province.";
                case TriggerVar.total_base_tax:
                    return "Returns true if the base tax of the country's provinces totals at least X.";
                case TriggerVar.total_development:
                    return "Returns true if the country has a total development of at least X.";
                case TriggerVar.total_number_of_cardinals:
                    return "Returns true if the total number of cardinals is at least X.";
                case TriggerVar.trade_league_embargoed_by:
                    return "''Description needed''";
                case TriggerVar.total_own_and_non_tributary_subject_development:
                    return "Returns true if the country and its non-tributary subjects have more Total development than the specified country and its non-tributary subjects.";
                case TriggerVar.transports_in_province:
                    return "Returns true if there are at least X transports in the province.";
                case TriggerVar.trade_company_region:
                    return "Returns true if the province is in the trade company region.";
                case TriggerVar.trade_company_size:
                    return "Returns true if the trade company has at least X provinces.";
                case TriggerVar.trade_efficiency:
                    return "Returns true if the country has a trade efficiency over X.";
                case TriggerVar.trade_embargoing:
                    return "Returns true if the country is embargoing country X.";
                case TriggerVar.trade_embargo_by:
                    return "Returns true if country X is embargoing the country.";
                case TriggerVar.trade_goods:
                    return "Returns true if the province is producing the trade good X.";
                case TriggerVar.trade_income_percentage:
                    return "Returns true if the ratio of trade income to total income is at least X.";
                case TriggerVar.trade_node_value:
                    return "Returns true if total trade value in the node is at least X.";
                case TriggerVar.trade_range:
                    return "Returns true if the trade node is within the trade range of the specified country.";
                case TriggerVar.transport_fraction:
                    return "Returns true if the ratio of the transport fraction to the navy size of the country is at least X.";
                case TriggerVar.treasury:
                    return "Returns true if country's treasury contains at least X ducats.";
                case TriggerVar.tribal_allegiance:
                    return "Returns true, if the country has a tribal allegiance of at least X.";
                case TriggerVar.tribal_development:
                    return "Returns true, if the country or province has at least X tribal development, or the scoped country has more development than the defined country.";
                case TriggerVar.truce_with:
                    return "Returns true if the country has a truce with X.";
                case TriggerVar.trust:
                    return "Returns true if the current scope has a trust level of at least X with the specified tag.";
                case TriggerVar.unit_has_leader:
                    return "Returns true if any unit is in the province has a leader. Warning: Works only with 'yes'.";
                case TriggerVar.unit_in_battle:
                    return "Returns true if any unit in the province is in a battle.";
                case TriggerVar.unit_in_siege:
                    return "Returns true if any unit in the province is in a siege.";
                case TriggerVar.units_in_province:
                    return "Returns true if there are at least X units in the province.";
                case TriggerVar.unit_type:
                    return "Returns true if the country has units of type X.";
                case TriggerVar.unrest:
                    return "Returns true if the unrest in the province is at least X. If it is used in a country scope, it checks for the global_unrest modifier";
                case TriggerVar.uses_authority:
                    return "Returns true if the country uses Pagan denominations#Inti|authority mechanics.";
                case TriggerVar.uses_church_aspects:
                    return "Returns true if the country uses church aspects mechanics.";
                case TriggerVar.uses_blessings:
                    return "Returns true if the country uses blessings mechanics.";
                case TriggerVar.uses_cults:
                    return "Returns true if the country uses cults mechanics.";
                case TriggerVar.uses_devotion:
                    return "Returns true, if the country uses devotion instead of legitimacy.";
                case TriggerVar.uses_doom:
                    return "Returns true if the country uses doom mechanics.";
                case TriggerVar.uses_fervor:
                    return "Returns true if the country uses fervor mechanics.";
                case TriggerVar.uses_isolationism:
                    return "Returns true, if the country uses the isolationism mechanic.";
                case TriggerVar.uses_karma:
                    return "Returns true if the country uses karma mechanics.";
                case TriggerVar.uses_papacy:
                    return "Returns true if the country uses papacy mechanics.";
                case TriggerVar.uses_patriarch_authority:
                    return "Returns true if the country uses patriarch authority mechanics.";
                case TriggerVar.uses_personal_deities:
                    return "Returns true if the country uses personal deity|personal deities mechanics.";
                case TriggerVar.uses_piety:
                    return "Returns true if the country uses piety mechanics.";
                case TriggerVar.uses_religious_icons:
                    return "Returns true, if the country uses religious icons.";
                case TriggerVar.uses_syncretic_faiths:
                    return "Returns true if the country uses syncretic faiths mechanics.";
                case TriggerVar.vassal_of:
                    return "Returns true if the country is a vassal of country X.";
                case TriggerVar.war_exhaustion:
                    return "Returns true if the country has a war exhaustion of at least X.";
                case TriggerVar.war_score:
                    return "Returns true if the coutry has a warscore of at least X%.";
                case TriggerVar.war_with:
                    return "Returns true if the country is at war with country X.";
                case TriggerVar.was_player:
                    return "Returns true if the country was controlled by a human player.";
                case TriggerVar.was_tag:
                    return "Returns true if the country was a particular tag.";
                case TriggerVar.will_back_next_reform:
                    return "Returns true if the member of the HRE is backing the next imperial reform.";
                case TriggerVar.yearly_corruption_increase:
                    return "Returns true if the country has a yearly corruption increase of at least X.";
                case TriggerVar.years_of_income:
                    return "Returns true if the country's treasury contains ducats of at least X times their yearly income.";
            }
        }
        public static TriggerVar GetVariableByName(string name)
        {
            switch (name)
            {
                default:
                    return TriggerVar.UnknownTrigger;
                case "<advisor>":
                    return TriggerVar.NamedAdvisor;
                case "<building>":
                    return TriggerVar.NamedBuilding;
                case "<idea group>":
                    return TriggerVar.NamedIdeaGroup;
                case "<institution>":
                    return TriggerVar.NamedInstitution;
                case "<religion>":
                    return TriggerVar.NamedReligion;
                case "<subject_type>":
                    return TriggerVar.NamedSubjectType;
                case "<trade_good>":
                    return TriggerVar.NamedTradeGood;
                case "absolutism":
                    return TriggerVar.absolutism;
                case "accepted_culture":
                    return TriggerVar.accepted_culture;
                case "active_major_mission":
                    return TriggerVar.active_major_mission;
                case "adm":
                    return TriggerVar.adm;
                case "adm_power":
                    return TriggerVar.adm_power;
                case "adm_tech":
                    return TriggerVar.adm_tech;
                case "advisor":
                    return TriggerVar.advisor;
                case "advisor_exists":
                    return TriggerVar.advisor_exists;
                case "ai":
                    return TriggerVar.ai;
                case "alliance_with":
                    return TriggerVar.alliance_with;
                case "allows_female_emperor":
                    return TriggerVar.allows_female_emperor;
                case "always":
                    return TriggerVar.always;
                case "area":
                    return TriggerVar.area;
                case "army_size":
                    return TriggerVar.army_size;
                case "army_size_percentage":
                    return TriggerVar.army_size_percentage;
                case "army_professionalism":
                    return TriggerVar.army_professionalism;
                case "army_tradition":
                    return TriggerVar.army_tradition;
                case "artillery_fraction":
                    return TriggerVar.artillery_fraction;
                case "artillery_in_province":
                    return TriggerVar.artillery_in_province;
                case "at_war_with_religious_enemy":
                    return TriggerVar.at_war_with_religious_enemy;
                case "authority":
                    return TriggerVar.authority;
                case "average_autonomy":
                    return TriggerVar.average_autonomy;
                case "average_autonomy_above_min":
                    return TriggerVar.average_autonomy_above_min;
                case "average_effective_unrest":
                    return TriggerVar.average_effective_unrest;
                case "average_home_autonomy":
                    return TriggerVar.average_home_autonomy;
                case "average_unrest":
                    return TriggerVar.average_unrest;
                case "base_manpower":
                    return TriggerVar.base_manpower;
                case "base_production":
                    return TriggerVar.base_production;
                case "base_tax":
                    return TriggerVar.base_tax;
                case "blockade":
                    return TriggerVar.blockade;
                case "can_be_overlord":
                    return TriggerVar.can_be_overlord;
                case "can_build":
                    return TriggerVar.can_build;
                case "can_create_vassals":
                    return TriggerVar.can_create_vassals;
                case "can_heir_be_child_of_consort":
                    return TriggerVar.can_heir_be_child_of_consort;
                case "can_justify_trade_conflict":
                    return TriggerVar.can_justify_trade_conflict;
                case "can_migrate":
                    return TriggerVar.can_migrate;
                case "can_spawn_rebels":
                    return TriggerVar.can_spawn_rebels;
                case "capital":
                    return TriggerVar.capital;
                case "cavalry_fraction":
                    return TriggerVar.cavalry_fraction;
                case "cavalry_in_province":
                    return TriggerVar.cavalry_in_province;
                case "province_has_center_of_trade_of_level":
                    return TriggerVar.province_has_center_of_trade_of_level;
                case "church_power":
                    return TriggerVar.church_power;
                case "coalition_target":
                    return TriggerVar.coalition_target;
                case "colonial_claim_by_anyone_of_religion":
                    return TriggerVar.colonial_claim_by_anyone_of_religion;
                case "colonial_region":
                    return TriggerVar.colonial_region;
                case "colony":
                    return TriggerVar.colony;
                case "colony_claim":
                    return TriggerVar.colony_claim;
                case "colonysize":
                    return TriggerVar.colonysize;
                case "consort_adm":
                    return TriggerVar.consort_adm;
                case "consort_age":
                    return TriggerVar.consort_age;
                case "consort_dip":
                    return TriggerVar.consort_dip;
                case "consort_culture":
                    return TriggerVar.consort_culture;
                case "consort_has_personality":
                    return TriggerVar.consort_has_personality;
                case "consort_mil":
                    return TriggerVar.consort_mil;
                case "consort_religion":
                    return TriggerVar.consort_religion;
                case "construction_progress":
                    return TriggerVar.construction_progress;
                case "continent":
                    return TriggerVar.continent;
                case "controlled_by":
                    return TriggerVar.controlled_by;
                case "controls":
                    return TriggerVar.controls;
                case "core_claim":
                    return TriggerVar.core_claim;
                case "core_percentage":
                    return TriggerVar.core_percentage;
                case "corruption":
                    return TriggerVar.corruption;
                case "council_position":
                    return TriggerVar.council_position;
                case "country_or_non_sovereign_subject_holds":
                    return TriggerVar.country_or_non_sovereign_subject_holds;
                case "country_or_subject_holds":
                    return TriggerVar.country_or_subject_holds;
                case "crown_land_share":
                    return TriggerVar.crown_land_share;
                case "culture":
                    return TriggerVar.culture;
                case "culture_group":
                    return TriggerVar.culture_group;
                case "culture_group_claim":
                    return TriggerVar.culture_group_claim;
                case "current_age":
                    return TriggerVar.current_age;
                case "current_bribe":
                    return TriggerVar.current_bribe;
                case "current_debate":
                    return TriggerVar.current_debate;
                case "current_icon":
                    return TriggerVar.current_icon;
                case "current_income_balance":
                    return TriggerVar.current_income_balance;
                case "current_institution":
                    return TriggerVar.current_institution;
                case "current_institution_growth":
                    return TriggerVar.current_institution_growth;
                case "current_size_of_parliament":
                    return TriggerVar.current_size_of_parliament;
                case "defensive_war_with":
                    return TriggerVar.defensive_war_with;
                case "devastation":
                    return TriggerVar.devastation;
                case "development":
                    return TriggerVar.development;
                case "development_of_overlord_fraction":
                    return TriggerVar.development_of_overlord_fraction;
                case "devotion":
                    return TriggerVar.devotion;
                case "dip":
                    return TriggerVar.dip;
                case "diplomatic_reputation":
                    return TriggerVar.diplomatic_reputation;
                case "dip_power":
                    return TriggerVar.dip_power;
                case "dip_tech":
                    return TriggerVar.dip_tech;
                case "dominant_culture":
                    return TriggerVar.dominant_culture;
                case "dominant_religion":
                    return TriggerVar.dominant_religion;
                case "doom":
                    return TriggerVar.doom;
                case "dynasty":
                    return TriggerVar.dynasty;
                case "empire_of_china_reform_passed":
                    return TriggerVar.empire_of_china_reform_passed;
                case "estate_led_regency_influence":
                    return TriggerVar.estate_led_regency_influence;
                case "estate_led_regency_loyalty":
                    return TriggerVar.estate_led_regency_loyalty;
                case "exiled_same_dynasty_as_current":
                    return TriggerVar.exiled_same_dynasty_as_current;
                case "exists":
                    return TriggerVar.exists;
                case "faction_in_power":
                    return TriggerVar.faction_in_power;
                case "federation_size":
                    return TriggerVar.federation_size;
                case "fervor":
                    return TriggerVar.fervor;
                case "fort_level":
                    return TriggerVar.fort_level;
                case "full_idea_group":
                    return TriggerVar.full_idea_group;
                case "galley_fraction":
                    return TriggerVar.galley_fraction;
                case "galleys_in_province":
                    return TriggerVar.galleys_in_province;
                case "garrison":
                    return TriggerVar.garrison;
                case "gives_military_access_to":
                    return TriggerVar.gives_military_access_to;
                case "gives_fleet_basing_rights_to":
                    return TriggerVar.gives_fleet_basing_rights_to;
                case "gold_income":
                    return TriggerVar.gold_income;
                case "gold_income_percentage":
                    return TriggerVar.gold_income_percentage;
                case "government":
                    return TriggerVar.government;
                case "government_rank":
                    return TriggerVar.government_rank;
                case "grown_by_development":
                    return TriggerVar.grown_by_development;
                case "grown_by_states":
                    return TriggerVar.grown_by_states;
                case "great_power_rank":
                    return TriggerVar.great_power_rank;
                case "guaranteed_by":
                    return TriggerVar.guaranteed_by;
                case "had_recent_war":
                    return TriggerVar.had_recent_war;
                case "harmonization_progress":
                    return TriggerVar.harmonization_progress;
                case "harmony":
                    return TriggerVar.harmony;
                case "has_active_debate":
                    return TriggerVar.has_active_debate;
                case "has_active_fervor":
                    return TriggerVar.has_active_fervor;
                case "has_active_policy":
                    return TriggerVar.has_active_policy;
                case "has_active_triggered_province_modifier":
                    return TriggerVar.has_active_triggered_province_modifier;
                case "has_adopted_cult":
                    return TriggerVar.has_adopted_cult;
                case "has_advisor":
                    return TriggerVar.has_advisor;
                case "has_age_ability":
                    return TriggerVar.has_age_ability;
                case "has_any_disaster":
                    return TriggerVar.has_any_disaster;
                case "has_border_with_religious_enemy":
                    return TriggerVar.has_border_with_religious_enemy;
                case "has_building":
                    return TriggerVar.has_building;
                case "has_cardinal":
                    return TriggerVar.has_cardinal;
                case "has_casus_belli_against":
                    return TriggerVar.has_casus_belli_against;
                case "has_center_of_trade_of_level":
                    return TriggerVar.has_center_of_trade_of_level;
                case "has_changed_nation":
                    return TriggerVar.has_changed_nation;
                case "has_church_aspect":
                    return TriggerVar.has_church_aspect;
                case "has_climate":
                    return TriggerVar.has_climate;
                case "has_colonial_parent":
                    return TriggerVar.has_colonial_parent;
                case "has_colonist":
                    return TriggerVar.has_colonist;
                case "has_commanding_three_star":
                    return TriggerVar.has_commanding_three_star;
                case "has_consort":
                    return TriggerVar.has_consort;
                case "has_consort_flag":
                    return TriggerVar.has_consort_flag;
                case "has_consort_regency":
                    return TriggerVar.has_consort_regency;
                case "has_construction":
                    return TriggerVar.has_construction;
                case "has_country_flag":
                    return TriggerVar.has_country_flag;
                case "has_country_modifier":
                    return TriggerVar.has_country_modifier;
                case "has_custom_ideas":
                    return TriggerVar.has_custom_ideas;
                case "has_disaster":
                    return TriggerVar.has_disaster;
                case "has_discovered":
                    return TriggerVar.has_discovered;
                case "has_dlc":
                    return TriggerVar.has_dlc;
                case "has_divert_trade":
                    return TriggerVar.has_divert_trade;
                case "has_embargo_rivals":
                    return TriggerVar.has_embargo_rivals;
                case "has_empty_adjacent_province":
                    return TriggerVar.has_empty_adjacent_province;
                case "has_estate":
                    return TriggerVar.has_estate;
                //case "has_estate":
                //    return TriggerVar.has_estate_province;
                case "has_estate_loan":
                    return TriggerVar.has_estate_loan;
                case "has_estate_privilege":
                    return TriggerVar.has_estate_privilege;
                case "has_faction":
                    return TriggerVar.has_faction;
                case "has_factions":
                    return TriggerVar.has_factions;
                case "has_female_consort":
                    return TriggerVar.has_female_consort;
                case "has_female_heir":
                    return TriggerVar.has_female_heir;
                case "has_first_revolution_started":
                    return TriggerVar.has_first_revolution_started;
                case "has_flagship":
                    return TriggerVar.has_flagship;
                case "has_foreign_consort":
                    return TriggerVar.has_foreign_consort;
                case "has_foreign_heir":
                    return TriggerVar.has_foreign_heir;
                case "has_friendly_reformation_center":
                    return TriggerVar.has_friendly_reformation_center;
                case "has_game_started":
                    return TriggerVar.has_game_started;
                case "has_given_consort_to":
                    return TriggerVar.has_given_consort_to;
                case "has_guaranteed":
                    return TriggerVar.has_guaranteed;
                case "has_global_flag":
                    return TriggerVar.has_global_flag;
                case "has_government_mechanic":
                    return TriggerVar.has_government_mechanic;
                case "has_government_power":
                    return TriggerVar.has_government_power;
                case "has_had_golden_age":
                    return TriggerVar.has_had_golden_age;
                case "has_harmonized_with":
                    return TriggerVar.has_harmonized_with;
                case "has_harsh_treatment":
                    return TriggerVar.has_harsh_treatment;
                case "has_heir":
                    return TriggerVar.has_heir;
                case "has_heir_flag":
                    return TriggerVar.has_heir_flag;
                case "has_heir_leader_from":
                    return TriggerVar.has_heir_leader_from;
                case "has_hostile_reformation_center":
                    return TriggerVar.has_hostile_reformation_center;
                case "has_idea":
                    return TriggerVar.has_idea;
                case "has_idea_group":
                    return TriggerVar.has_idea_group;
                case "has_influencing_fort":
                    return TriggerVar.has_influencing_fort;
                case "has_institution":
                    return TriggerVar.has_institution;
                case "has_latent_trade_goods":
                    return TriggerVar.has_latent_trade_goods;
                case "has_leader":
                    return TriggerVar.has_leader;
                case "has_matching_religion":
                    return TriggerVar.has_matching_religion;
                case "has_merchant":
                    return TriggerVar.has_merchant;
                case "has_mission":
                    return TriggerVar.has_mission;
                case "has_missionary":
                    return TriggerVar.has_missionary;
                case "has_monsoon":
                    return TriggerVar.has_monsoon;
                case "has_most_province_trade_power":
                    return TriggerVar.has_most_province_trade_power;
                case "has_new_dynasty":
                    return TriggerVar.has_new_dynasty;
                case "has_owner_accepted_culture":
                    return TriggerVar.has_owner_accepted_culture;
                case "has_owner_culture":
                    return TriggerVar.has_owner_culture;
                case "has_owner_religion":
                    return TriggerVar.has_owner_religion;
                case "has_pasha":
                    return TriggerVar.has_pasha;
                case "has_parliament":
                    return TriggerVar.has_parliament;
                case "has_personal_deity":
                    return TriggerVar.has_personal_deity;
                case "has_pillaged_capital_against":
                    return TriggerVar.has_pillaged_capital_against;
                case "has_port":
                    return TriggerVar.has_port;
                case "has_privateers":
                    return TriggerVar.has_privateers;
                case "has_promote_investments":
                    return TriggerVar.has_promote_investments;
                case "has_province_flag":
                    return TriggerVar.has_province_flag;
                case "has_province_modifier":
                    return TriggerVar.has_province_modifier;
                case "has_rebel_faction":
                    return TriggerVar.has_rebel_faction;
                case "has_regency":
                    return TriggerVar.has_regency;
                case "has_reform":
                    return TriggerVar.has_reform;
                case "government_reform_progress":
                    return TriggerVar.government_reform_progress;
                case "has_removed_fow":
                    return TriggerVar.has_removed_fow;
                case "has_revolution_in_province":
                    return TriggerVar.has_revolution_in_province;
                case "has_ruler":
                    return TriggerVar.has_ruler;
                case "has_ruler_flag":
                    return TriggerVar.has_ruler_flag;
                case "has_ruler_leader_from":
                    return TriggerVar.has_ruler_leader_from;
                case "has_ruler_modifier":
                    return TriggerVar.has_ruler_modifier;
                case "has_saved_event_target":
                    return TriggerVar.has_saved_event_target;
                case "has_scutage":
                    return TriggerVar.has_scutage;
                case "has_seat_in_parliament":
                    return TriggerVar.has_seat_in_parliament;
                case "has_secondary_religion":
                    return TriggerVar.has_secondary_religion;
                case "has_send_officers":
                    return TriggerVar.has_send_officers;
                case "has_siege":
                    return TriggerVar.has_siege;
                case "has_spawned_rebels":
                    return TriggerVar.has_spawned_rebels;
                case "has_spawned_supported_rebels":
                    return TriggerVar.has_spawned_supported_rebels;
                case "has_state_edict":
                    return TriggerVar.has_state_edict;
                case "has_state_patriach":
                    return TriggerVar.has_state_patriach;
                case "has_subsidize_armies":
                    return TriggerVar.has_subsidize_armies;
                case "has_supply_depot":
                    return TriggerVar.has_supply_depot;
                case "has_support_loyalists":
                    return TriggerVar.has_support_loyalists;
                case "has_subject_of_type":
                    return TriggerVar.has_subject_of_type;
                case "has_switched_nation":
                    return TriggerVar.has_switched_nation;
                case "has_terrain":
                    return TriggerVar.has_terrain;
                case "has_trader":
                    return TriggerVar.has_trader;
                case "has_truce":
                    return TriggerVar.has_truce;
                case "has_unembraced_institution":
                    return TriggerVar.has_unembraced_institution;
                case "has_unified_culture_group":
                    return TriggerVar.has_unified_culture_group;
                case "has_unit_type":
                    return TriggerVar.has_unit_type;
                case "has_unlocked_cult":
                    return TriggerVar.has_unlocked_cult;
                case "has_wartaxes":
                    return TriggerVar.has_wartaxes;
                case "has_winter":
                    return TriggerVar.has_winter;
                case "have_had_reform":
                    return TriggerVar.have_had_reform;
                case "heavy_ship_fraction":
                    return TriggerVar.heavy_ship_fraction;
                case "heavy_ships_in_province":
                    return TriggerVar.heavy_ships_in_province;
                case "heir_adm":
                    return TriggerVar.heir_adm;
                case "heir_age":
                    return TriggerVar.heir_age;
                case "heir_dip":
                    return TriggerVar.heir_dip;
                case "heir_claim":
                    return TriggerVar.heir_claim;
                case "heir_culture":
                    return TriggerVar.heir_culture;
                case "heir_has_consort_dynasty":
                    return TriggerVar.heir_has_consort_dynasty;
                case "heir_has_personality":
                    return TriggerVar.heir_has_personality;
                case "heir_has_ruler_dynasty":
                    return TriggerVar.heir_has_ruler_dynasty;
                case "heir_mil":
                    return TriggerVar.heir_mil;
                case "heir_nationality":
                    return TriggerVar.heir_nationality;
                case "heir_religion":
                    return TriggerVar.heir_religion;
                case "higher_development_than":
                    return TriggerVar.higher_development_than;
                case "highest_value_trade_node":
                    return TriggerVar.highest_value_trade_node;
                case "historical_friend_with":
                    return TriggerVar.historical_friend_with;
                case "historical_rival_with":
                    return TriggerVar.historical_rival_with;
                case "holy_order":
                    return TriggerVar.holy_order;
                case "horde_unity":
                    return TriggerVar.horde_unity;
                case "hre_heretic_religion":
                    return TriggerVar.hre_heretic_religion;
                case "hre_leagues_enabled":
                    return TriggerVar.hre_leagues_enabled;
                case "hre_reform_passed":
                    return TriggerVar.hre_reform_passed;
                case "hre_religion":
                    return TriggerVar.hre_religion;
                case "hre_religion_locked":
                    return TriggerVar.hre_religion_locked;
                case "hre_religion_treaty":
                    return TriggerVar.hre_religion_treaty;
                case "hre_size":
                    return TriggerVar.hre_size;
                case "imperial_influence":
                    return TriggerVar.imperial_influence;
                case "imperial_mandate":
                    return TriggerVar.imperial_mandate;
                case "in_golden_age":
                    return TriggerVar.in_golden_age;
                case "infantry_fraction":
                    return TriggerVar.infantry_fraction;
                case "infantry_in_province":
                    return TriggerVar.infantry_in_province;
                case "inflation":
                    return TriggerVar.inflation;
                case "innovativeness":
                    return TriggerVar.innovativeness;
                case "invested_papal_influence":
                    return TriggerVar.invested_papal_influence;
                case "in_league":
                    return TriggerVar.in_league;
                case "ironman":
                    return TriggerVar.ironman;
                case "is_advisor_employed":
                    return TriggerVar.is_advisor_employed;
                case "is_all_concessions_in_council_taken":
                    return TriggerVar.is_all_concessions_in_council_taken;
                case "is_at_war":
                    return TriggerVar.is_at_war;
                case "is_backing_current_issue":
                    return TriggerVar.is_backing_current_issue;
                case "is_bankrupt":
                    return TriggerVar.is_bankrupt;
                case "is_blockaded":
                    return TriggerVar.is_blockaded;
                case "is_blockaded_by":
                    return TriggerVar.is_blockaded_by;
                case "is_capital":
                    return TriggerVar.is_capital;
                case "is_capital_of":
                    return TriggerVar.is_capital_of;
                case "is_city":
                    return TriggerVar.is_city;
                case "is_claim":
                    return TriggerVar.is_claim;
                case "is_client_nation":
                    return TriggerVar.is_client_nation;
                case "is_client_nation_of":
                    return TriggerVar.is_client_nation_of;
                case "is_colonial_nation":
                    return TriggerVar.is_colonial_nation;
                case "is_colonial_nation_of":
                    return TriggerVar.is_colonial_nation_of;
                case "is_colony":
                    return TriggerVar.is_colony;
                case "is_core":
                    return TriggerVar.is_core;
                case "is_council_enabled":
                    return TriggerVar.is_council_enabled;
                case "is_crusade_target":
                    return TriggerVar.is_crusade_target;
                case "is_defender_of_faith":
                    return TriggerVar.is_defender_of_faith;
                case "is_defender_of_faith_of_tier":
                    return TriggerVar.is_defender_of_faith_of_tier;
                case "is_dynamic_tag":
                    return TriggerVar.is_dynamic_tag;
                case "is_elector":
                    return TriggerVar.is_elector;
                case "is_emperor":
                    return TriggerVar.is_emperor;
                case "is_emperor_of_china":
                    return TriggerVar.is_emperor_of_china;
                case "is_empty":
                    return TriggerVar.is_empty;
                case "is_enemy":
                    return TriggerVar.is_enemy;
                case "is_excommunicated":
                    return TriggerVar.is_excommunicated;
                case "is_federation_leader":
                    return TriggerVar.is_federation_leader;
                case "is_female":
                    return TriggerVar.is_female;
                case "is_force_converted":
                    return TriggerVar.is_force_converted;
                case "is_former_colonial_nation":
                    return TriggerVar.is_former_colonial_nation;
                case "is_foreign_claim":
                    return TriggerVar.is_foreign_claim;
                case "is_great_power":
                    return TriggerVar.is_great_power;
                case "is_harmonizing_with":
                    return TriggerVar.is_harmonizing_with;
                case "is_heir_leader":
                    return TriggerVar.is_heir_leader;
                case "is_hegemon":
                    return TriggerVar.is_hegemon;
                case "is_hegemon_of_type":
                    return TriggerVar.is_hegemon_of_type;
                case "is_imperial_ban_allowed":
                    return TriggerVar.is_imperial_ban_allowed;
                case "is_incident_active":
                    return TriggerVar.is_incident_active;
                case "is_incident_happened":
                    return TriggerVar.is_incident_happened;
                case "is_incident_possible":
                    return TriggerVar.is_incident_possible;
                case "is_incident_potential":
                    return TriggerVar.is_incident_potential;
                case "is_institution_enabled":
                    return TriggerVar.is_institution_enabled;
                case "is_institution_origin":
                    return TriggerVar.is_institution_origin;
                case "is_in_capital_area":
                    return TriggerVar.is_in_capital_area;
                case "is_in_coalition":
                    return TriggerVar.is_in_coalition;
                case "is_in_coalition_war":
                    return TriggerVar.is_in_coalition_war;
                case "is_in_deficit":
                    return TriggerVar.is_in_deficit;
                case "is_in_extended_regency":
                    return TriggerVar.is_in_extended_regency;
                case "is_in_league_war":
                    return TriggerVar.is_in_league_war;
                case "is_in_trade_league":
                    return TriggerVar.is_in_trade_league;
                case "is_in_trade_league_with":
                    return TriggerVar.is_in_trade_league_with;
                case "is_island":
                    return TriggerVar.is_island;
                case "is_league_enemy":
                    return TriggerVar.is_league_enemy;
                case "is_lacking_institutions":
                    return TriggerVar.is_lacking_institutions;
                case "is_league_friend":
                    return TriggerVar.is_league_friend;
                case "is_league_leader":
                    return TriggerVar.is_league_leader;
                case "is_lesser_in_union":
                    return TriggerVar.is_lesser_in_union;
                case "is_looted":
                    return TriggerVar.is_looted;
                case "is_monarch_leader":
                    return TriggerVar.is_monarch_leader;
                case "is_month":
                    return TriggerVar.is_month;
                case "is_march":
                    return TriggerVar.is_march;
                case "is_neighbor_of":
                    return TriggerVar.is_neighbor_of;
                case "is_node_in_trade_company_region":
                    return TriggerVar.is_node_in_trade_company_region;
                case "is_nomad":
                    return TriggerVar.is_nomad;
                case "is_orangists_in_power":
                    return TriggerVar.is_orangists_in_power;
                case "is_origin_of_consort":
                    return TriggerVar.is_origin_of_consort;
                case "is_overseas":
                    return TriggerVar.is_overseas;
                case "is_overseas_subject":
                    return TriggerVar.is_overseas_subject;
                case "is_owned_by_trade_company":
                    return TriggerVar.is_owned_by_trade_company;
                case "is_papal_controller":
                    return TriggerVar.is_papal_controller;
                case "is_part_of_hre":
                    return TriggerVar.is_part_of_hre;
                case "is_permanent_claim":
                    return TriggerVar.is_permanent_claim;
                case "is_playing_custom_nation":
                    return TriggerVar.is_playing_custom_nation;
                case "is_possible_march":
                    return TriggerVar.is_possible_march;
                case "is_possible_vassal":
                    return TriggerVar.is_possible_vassal;
                case "is_previous_papal_controller":
                    return TriggerVar.is_previous_papal_controller;
                case "is_prosperous":
                    return TriggerVar.is_prosperous;
                case "is_protectorate":
                    return TriggerVar.is_protectorate;
                case "is_random_new_world":
                    return TriggerVar.is_random_new_world;
                case "is_reformation_center":
                    return TriggerVar.is_reformation_center;
                case "is_religion_grant_colonial_claim":
                    return TriggerVar.is_religion_grant_colonial_claim;
                case "is_religion_enabled":
                    return TriggerVar.is_religion_enabled;
                case "is_religion_reformed":
                    return TriggerVar.is_religion_reformed;
                case "is_renting_condottieri_to":
                    return TriggerVar.is_renting_condottieri_to;
                case "is_revolution_target":
                    return TriggerVar.is_revolution_target;
                case "is_rival":
                    return TriggerVar.is_rival;
                case "is_ruler_commanding_unit":
                    return TriggerVar.is_ruler_commanding_unit;
                case "is_sea":
                    return TriggerVar.is_sea;
                case "is_state":
                    return TriggerVar.is_state;
                case "is_state_core":
                    return TriggerVar.is_state_core;
                case "is_statists_in_power":
                    return TriggerVar.is_statists_in_power;
                case "is_strongest_trade_power":
                    return TriggerVar.is_strongest_trade_power;
                case "is_subject":
                    return TriggerVar.is_subject;
                case "is_subject_of":
                    return TriggerVar.is_subject_of;
                case "is_subject_of_type":
                    return TriggerVar.is_subject_of_type;
                case "is_supporting_independence_of":
                    return TriggerVar.is_supporting_independence_of;
                case "is_territorial_core":
                    return TriggerVar.is_territorial_core;
                case "is_territory":
                    return TriggerVar.is_territory;
                case "is_threat":
                    return TriggerVar.is_threat;
                case "is_trade_league_leader":
                    return TriggerVar.is_trade_league_leader;
                case "is_tribal":
                    return TriggerVar.is_tribal;
                case "is_vassal":
                    return TriggerVar.is_vassal;
                case "is_wasteland":
                    return TriggerVar.is_wasteland;
                case "is_year":
                    return TriggerVar.is_year;
                case "island":
                    return TriggerVar.island;
                case "isolationism":
                    return TriggerVar.isolationism;
                case "janissary_percentage":
                    return TriggerVar.janissary_percentage;
                case "junior_union_with":
                    return TriggerVar.junior_union_with;
                case "karma":
                    return TriggerVar.karma;
                case "knows_country":
                    return TriggerVar.knows_country;
                case "land_forcelimit":
                    return TriggerVar.land_forcelimit;
                case "land_maintenance":
                    return TriggerVar.land_maintenance;
                case "land_morale":
                    return TriggerVar.land_morale;
                case "last_mission":
                    return TriggerVar.last_mission;
                case "legitimacy":
                    return TriggerVar.legitimacy;
                case "legitimacy_equivalent":
                    return TriggerVar.legitimacy_equivalent;
                case "legitimacy_or_horde_unity":
                    return TriggerVar.legitimacy_or_horde_unity;
                case "liberty_desire":
                    return TriggerVar.liberty_desire;
                case "light_ship_fraction":
                    return TriggerVar.light_ship_fraction;
                case "light_ships_in_province":
                    return TriggerVar.light_ships_in_province;
                case "likely_rebels":
                    return TriggerVar.likely_rebels;
                case "local_autonomy":
                    return TriggerVar.local_autonomy;
                case "local_autonomy_above_min":
                    return TriggerVar.local_autonomy_above_min;
                case "luck":
                    return TriggerVar.luck;
                case "march_of":
                    return TriggerVar.march_of;
                case "manpower":
                    return TriggerVar.manpower;
                case "manpower_percentage":
                    return TriggerVar.manpower_percentage;
                case "marriage_with":
                    return TriggerVar.marriage_with;
                case "max_manpower":
                    return TriggerVar.max_manpower;
                case "mercantilism":
                    return TriggerVar.mercantilism;
                case "meritocracy":
                    return TriggerVar.meritocracy;
                case "mil":
                    return TriggerVar.mil;
                case "militarised_society":
                    return TriggerVar.militarised_society;
                case "mil_power":
                    return TriggerVar.mil_power;
                case "mil_tech":
                    return TriggerVar.mil_tech;
                case "mission_completed":
                    return TriggerVar.mission_completed;
                case "monthly_income":
                    return TriggerVar.monthly_income;
                case "monthly_adm":
                    return TriggerVar.monthly_adm;
                case "monthly_dip":
                    return TriggerVar.monthly_dip;
                case "monthly_mil":
                    return TriggerVar.monthly_mil;
                case "months_of_ruling":
                    return TriggerVar.months_of_ruling;
                case "months_since_defection":
                    return TriggerVar.months_since_defection;
                case "nationalism":
                    return TriggerVar.nationalism;
                case "national_focus":
                    return TriggerVar.national_focus;
                case "nation_designer_points":
                    return TriggerVar.nation_designer_points;
                case "native_ferocity":
                    return TriggerVar.native_ferocity;
                case "native_hostileness":
                    return TriggerVar.native_hostileness;
                case "native_policy":
                    return TriggerVar.native_policy;
                case "native_size":
                    return TriggerVar.native_size;
                case "naval_forcelimit":
                    return TriggerVar.naval_forcelimit;
                case "naval_maintenance":
                    return TriggerVar.naval_maintenance;
                case "naval_morale":
                    return TriggerVar.naval_morale;
                case "navy_size":
                    return TriggerVar.navy_size;
                case "navy_size_percentage":
                    return TriggerVar.navy_size_percentage;
                case "navy_tradition":
                    return TriggerVar.navy_tradition;
                case "normal_or_historical_nations":
                    return TriggerVar.normal_or_historical_nations;
                case "normal_province_values":
                    return TriggerVar.normal_province_values;
                case "num_accepted_cultures":
                    return TriggerVar.num_accepted_cultures;
                case "num_free_building_slots":
                    return TriggerVar.num_free_building_slots;
                case "num_of_active_blessings":
                    return TriggerVar.num_of_active_blessings;
                case "num_of_admirals":
                    return TriggerVar.num_of_admirals;
                case "num_of_admirals_with_traits":
                    return TriggerVar.num_of_admirals_with_traits;
                case "num_of_allies":
                    return TriggerVar.num_of_allies;
                case "num_of_artillery":
                    return TriggerVar.num_of_artillery;
                case "num_of_aspects":
                    return TriggerVar.num_of_aspects;
                case "num_of_banners":
                    return TriggerVar.num_of_banners;
                case "num_of_buildings_in_province":
                    return TriggerVar.num_of_buildings_in_province;
                case "num_of_captured_ships_with_boarding_doctrine":
                    return TriggerVar.num_of_captured_ships_with_boarding_doctrine;
                case "num_of_centers_of_trade":
                    return TriggerVar.num_of_centers_of_trade;
                case "num_of_cardinals":
                    return TriggerVar.num_of_cardinals;
                case "num_of_cavalry":
                    return TriggerVar.num_of_cavalry;
                case "num_of_cawa":
                    return TriggerVar.num_of_cawa;
                case "num_of_cities":
                    return TriggerVar.num_of_cities;
                case "num_of_coalition_members":
                    return TriggerVar.num_of_coalition_members;
                case "num_of_colonies":
                    return TriggerVar.num_of_colonies;
                case "num_of_colonists":
                    return TriggerVar.num_of_colonists;
                case "num_of_conquistadors":
                    return TriggerVar.num_of_conquistadors;
                case "num_of_consorts":
                    return TriggerVar.num_of_consorts;
                case "num_of_continents":
                    return TriggerVar.num_of_continents;
                case "num_of_cossacks":
                    return TriggerVar.num_of_cossacks;
                case "num_of_custom_nations":
                    return TriggerVar.num_of_custom_nations;
                case "num_of_diplomatic_relations":
                    return TriggerVar.num_of_diplomatic_relations;
                case "num_of_diplomats":
                    return TriggerVar.num_of_diplomats;
                case "num_of_electors":
                    return TriggerVar.num_of_electors;
                case "num_of_explorers":
                    return TriggerVar.num_of_explorers;
                case "num_of_foreign_hre_provinces":
                    return TriggerVar.num_of_foreign_hre_provinces;
                case "num_of_free_diplomatic_relations":
                    return TriggerVar.num_of_free_diplomatic_relations;
                case "num_of_galley":
                    return TriggerVar.num_of_galley;
                case "num_of_generals":
                    return TriggerVar.num_of_generals;
                case "num_of_generals_with_traits":
                    return TriggerVar.num_of_generals_with_traits;
                case "num_of_harmonized":
                    return TriggerVar.num_of_harmonized;
                case "num_of_heavy_ship":
                    return TriggerVar.num_of_heavy_ship;
                case "num_of_infantry":
                    return TriggerVar.num_of_infantry;
                case "num_of_light_ship":
                    return TriggerVar.num_of_light_ship;
                case "num_of_loans":
                    return TriggerVar.num_of_loans;
                case "num_of_marches":
                    return TriggerVar.num_of_marches;
                case "num_of_marines":
                    return TriggerVar.num_of_marines;
                case "num_of_mercenaries":
                    return TriggerVar.num_of_mercenaries;
                case "num_of_merchants":
                    return TriggerVar.num_of_merchants;
                case "num_of_missionaries":
                    return TriggerVar.num_of_missionaries;
                case "num_of_owned_and_controlled_institutions":
                    return TriggerVar.num_of_owned_and_controlled_institutions;
                case "num_of_ports":
                    return TriggerVar.num_of_ports;
                case "num_of_ports_blockading":
                    return TriggerVar.num_of_ports_blockading;
                case "num_of_powerful_estates":
                    return TriggerVar.num_of_powerful_estates;
                case "num_of_protectorates":
                    return TriggerVar.num_of_protectorates;
                case "num_of_provinces_in_states":
                    return TriggerVar.num_of_provinces_in_states;
                case "num_of_provinces_in_territories":
                    return TriggerVar.num_of_provinces_in_territories;
                case "num_of_rajput":
                    return TriggerVar.num_of_rajput;
                case "num_of_rebel_armies":
                    return TriggerVar.num_of_rebel_armies;
                case "num_of_rebel_controlled_provinces":
                    return TriggerVar.num_of_rebel_controlled_provinces;
                case "num_of_revolts":
                    return TriggerVar.num_of_revolts;
                case "num_of_royal_marriages":
                    return TriggerVar.num_of_royal_marriages;
                case "num_of_ruler_traits":
                    return TriggerVar.num_of_ruler_traits;
                case "num_of_states":
                    return TriggerVar.num_of_states;
                case "num_of_streltsy":
                    return TriggerVar.num_of_streltsy;
                case "num_of_strong_trade_companies":
                    return TriggerVar.num_of_strong_trade_companies;
                case "num_of_subjects":
                    return TriggerVar.num_of_subjects;
                case "num_of_territories":
                    return TriggerVar.num_of_territories;
                case "num_of_times_improved":
                    return TriggerVar.num_of_times_improved;
                case "num_of_times_improved_by_owner":
                    return TriggerVar.num_of_times_improved_by_owner;
                case "num_of_times_used_pillage_capital":
                    return TriggerVar.num_of_times_used_pillage_capital;
                case "num_of_times_used_transfer_development":
                    return TriggerVar.num_of_times_used_transfer_development;
                case "num_of_total_ports":
                    return TriggerVar.num_of_total_ports;
                case "num_of_trade_companies":
                    return TriggerVar.num_of_trade_companies;
                case "num_of_trade_embargos":
                    return TriggerVar.num_of_trade_embargos;
                case "num_of_trading_bonuses":
                    return TriggerVar.num_of_trading_bonuses;
                case "num_of_transport":
                    return TriggerVar.num_of_transport;
                case "num_of_trusted_allies":
                    return TriggerVar.num_of_trusted_allies;
                case "num_of_unions":
                    return TriggerVar.num_of_unions;
                case "num_of_unlocked_cults":
                    return TriggerVar.num_of_unlocked_cults;
                case "num_of_war_reparations":
                    return TriggerVar.num_of_war_reparations;
                case "num_ships_privateering":
                    return TriggerVar.num_ships_privateering;
                case "offensive_war_with":
                    return TriggerVar.offensive_war_with;
                case "overextension_percentage":
                    return TriggerVar.overextension_percentage;
                case "overlord_of":
                    return TriggerVar.overlord_of;
                case "overseas_provinces_percentage":
                    return TriggerVar.overseas_provinces_percentage;
                case "owned_by":
                    return TriggerVar.owned_by;
                case "owns":
                    return TriggerVar.owns;
                case "owns_core_province":
                    return TriggerVar.owns_core_province;
                case "owns_or_non_sovereign_subject_of":
                    return TriggerVar.owns_or_non_sovereign_subject_of;
                case "owns_or_subject_of":
                    return TriggerVar.owns_or_subject_of;
                case "papacy_active":
                    return TriggerVar.papacy_active;
                case "papal_influence":
                    return TriggerVar.papal_influence;
                case "patriarch_authority":
                    return TriggerVar.patriarch_authority;
                case "percentage_backing_issue":
                    return TriggerVar.percentage_backing_issue;
                case "personality":
                    return TriggerVar.personality;
                case "piety":
                    return TriggerVar.piety;
                case "preferred_emperor":
                    return TriggerVar.preferred_emperor;
                case "prestige":
                    return TriggerVar.prestige;
                case "previous_owner":
                    return TriggerVar.previous_owner;
                case "power_projection":
                    return TriggerVar.power_projection;
                case "primary_culture":
                    return TriggerVar.primary_culture;
                case "primitives":
                    return TriggerVar.primitives;
                case "production_efficiency":
                    return TriggerVar.production_efficiency;
                case "production_income_percentage":
                    return TriggerVar.production_income_percentage;
                case "province_id":
                    return TriggerVar.province_id;
                case "province_is_on_an_island":
                    return TriggerVar.province_is_on_an_island;
                case "province_getting_expelled_minority":
                    return TriggerVar.province_getting_expelled_minority;
                case "province_group":
                    return TriggerVar.province_group;
                case "province_size":
                    return TriggerVar.province_size;
                case "province_trade_power":
                    return TriggerVar.province_trade_power;
                case "provinces_on_capital_continent_of":
                    return TriggerVar.provinces_on_capital_continent_of;
                case "pure_unrest":
                    return TriggerVar.pure_unrest;
                case "range":
                    return TriggerVar.range;
                case "real_day_of_year":
                    return TriggerVar.real_day_of_year;
                case "real_month_of_year":
                    return TriggerVar.real_month_of_year;
                case "reform_desire":
                    return TriggerVar.reform_desire;
                case "receives_military_access_from":
                    return TriggerVar.receives_military_access_from;
                case "receives_fleet_basing_rights_from":
                    return TriggerVar.receives_fleet_basing_rights_from;
                case "reform_level":
                    return TriggerVar.reform_level;
                case "region":
                    return TriggerVar.region;
                case "religion":
                    return TriggerVar.religion;
                case "religion_group":
                    return TriggerVar.religion_group;
                case "religious_unity":
                    return TriggerVar.religious_unity;
                case "republican_tradition":
                    return TriggerVar.republican_tradition;
                case "revanchism":
                    return TriggerVar.revanchism;
                case "revolt_percentage":
                    return TriggerVar.revolt_percentage;
                case "revolution_target_exists":
                    return TriggerVar.revolution_target_exists;
                case "ruler_age":
                    return TriggerVar.ruler_age;
                case "ruler_consort_marriage_length":
                    return TriggerVar.ruler_consort_marriage_length;
                case "ruler_culture":
                    return TriggerVar.ruler_culture;
                case "ruler_has_personality":
                    return TriggerVar.ruler_has_personality;
                case "ruler_is_foreigner":
                    return TriggerVar.ruler_is_foreigner;
                case "ruler_religion":
                    return TriggerVar.ruler_religion;
                case "sailors":
                    return TriggerVar.sailors;
                case "sailors_percentage":
                    return TriggerVar.sailors_percentage;
                case "max_sailors":
                    return TriggerVar.max_sailors;
                case "same_continent":
                    return TriggerVar.same_continent;
                case "secondary_religion":
                    return TriggerVar.secondary_religion;
                case "senior_union_with":
                    return TriggerVar.senior_union_with;
                case "sieged_by":
                    return TriggerVar.sieged_by;
                case "splendor":
                    return TriggerVar.splendor;
                case "stability":
                    return TriggerVar.stability;
                case "start_date":
                    return TriggerVar.start_date;
                case "started_in":
                    return TriggerVar.started_in;
                case "statists_vs_orangists":
                    return TriggerVar.statists_vs_orangists;
                case "subsidised_percent_amount":
                    return TriggerVar.subsidised_percent_amount;
                case "succession_claim":
                    return TriggerVar.succession_claim;
                case "superregion":
                    return TriggerVar.superregion;
                case "tag":
                    return TriggerVar.tag;
                case "tariff_value":
                    return TriggerVar.tariff_value;
                case "tax_income_percentage":
                    return TriggerVar.tax_income_percentage;
                case "tech_difference":
                    return TriggerVar.tech_difference;
                case "technology_group":
                    return TriggerVar.technology_group;
                case "tolerance_to_this":
                    return TriggerVar.tolerance_to_this;
                case "total_base_tax":
                    return TriggerVar.total_base_tax;
                case "total_development":
                    return TriggerVar.total_development;
                case "total_number_of_cardinals":
                    return TriggerVar.total_number_of_cardinals;
                case "trade_league_embargoed_by":
                    return TriggerVar.trade_league_embargoed_by;
                case "total_own_and_non_tributary_subject_development":
                    return TriggerVar.total_own_and_non_tributary_subject_development;
                case "transports_in_province":
                    return TriggerVar.transports_in_province;
                case "trade_company_region":
                    return TriggerVar.trade_company_region;
                case "trade_company_size":
                    return TriggerVar.trade_company_size;
                case "trade_efficiency":
                    return TriggerVar.trade_efficiency;
                case "trade_embargoing":
                    return TriggerVar.trade_embargoing;
                case "trade_embargo_by":
                    return TriggerVar.trade_embargo_by;
                case "trade_goods":
                    return TriggerVar.trade_goods;
                case "trade_income_percentage":
                    return TriggerVar.trade_income_percentage;
                case "trade_node_value":
                    return TriggerVar.trade_node_value;
                case "trade_range":
                    return TriggerVar.trade_range;
                case "transport_fraction":
                    return TriggerVar.transport_fraction;
                case "treasury":
                    return TriggerVar.treasury;
                case "tribal_allegiance":
                    return TriggerVar.tribal_allegiance;
                case "tribal_development":
                    return TriggerVar.tribal_development;
                case "truce_with":
                    return TriggerVar.truce_with;
                case "trust":
                    return TriggerVar.trust;
                case "unit_has_leader":
                    return TriggerVar.unit_has_leader;
                case "unit_in_battle":
                    return TriggerVar.unit_in_battle;
                case "unit_in_siege":
                    return TriggerVar.unit_in_siege;
                case "units_in_province":
                    return TriggerVar.units_in_province;
                case "unit_type":
                    return TriggerVar.unit_type;
                case "unrest":
                    return TriggerVar.unrest;
                case "uses_authority":
                    return TriggerVar.uses_authority;
                case "uses_church_aspects":
                    return TriggerVar.uses_church_aspects;
                case "uses_blessings":
                    return TriggerVar.uses_blessings;
                case "uses_cults":
                    return TriggerVar.uses_cults;
                case "uses_devotion":
                    return TriggerVar.uses_devotion;
                case "uses_doom":
                    return TriggerVar.uses_doom;
                case "uses_fervor":
                    return TriggerVar.uses_fervor;
                case "uses_isolationism":
                    return TriggerVar.uses_isolationism;
                case "uses_karma":
                    return TriggerVar.uses_karma;
                case "uses_papacy":
                    return TriggerVar.uses_papacy;
                case "uses_patriarch_authority":
                    return TriggerVar.uses_patriarch_authority;
                case "uses_personal_deities":
                    return TriggerVar.uses_personal_deities;
                case "uses_piety":
                    return TriggerVar.uses_piety;
                case "uses_religious_icons":
                    return TriggerVar.uses_religious_icons;
                case "uses_syncretic_faiths":
                    return TriggerVar.uses_syncretic_faiths;
                case "vassal_of":
                    return TriggerVar.vassal_of;
                case "war_exhaustion":
                    return TriggerVar.war_exhaustion;
                case "war_score":
                    return TriggerVar.war_score;
                case "war_with":
                    return TriggerVar.war_with;
                case "was_player":
                    return TriggerVar.was_player;
                case "was_tag":
                    return TriggerVar.was_tag;
                case "will_back_next_reform":
                    return TriggerVar.will_back_next_reform;
                case "yearly_corruption_increase":
                    return TriggerVar.yearly_corruption_increase;
                case "years_of_income":
                    return TriggerVar.years_of_income;
            }
        }


    }
}
