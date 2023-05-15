using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eu4ModEditor
{
    public class IdeaSet
    {
        public string setName = "";
        public string localisedName = "";
        public string[] ideaNames = new string[7];
        public string ambitionName = "";
        public List<Modifier>[] ideaModifiers = new List<Modifier>[7];
        public List<Modifier> ambitionModifiers = new List<Modifier>();
        public TriggerConnector Trigger;
        public Node AiWillDo;
        public NodeFile ParentFile;
        public string GetIdentificator()
        {
            return "";
        }
        public string GetLocalisedName()
        {
            return localisedName;
        }
        public override string ToString()
        {
            return setName;
        }
        public virtual int FirstPositionOf(ModifierType tp)
        {
            int Pos = Array.FindIndex(ideaModifiers, x => x.Any(y => y.Type == tp));
            if (Pos == -1)
            {
                if (ambitionModifiers.Any(x => x.Type == tp))
                    Pos = 8;
            }
            else
                Pos += 1;
            return Pos;
        }



        public virtual object GetVariable(QueryVariables vr)
        {
            string name = vr.ToString().ToLower();
            Modifier f = ideaModifiers.FirstOrDefault(x => x.Any(y => y.Name.Replace("_", "") == name))?.FirstOrDefault(x => x.Name.Replace("_", "") == name) ?? null;
            if (f == null)
            {
                f = ambitionModifiers.FirstOrDefault(x => x.Name.Replace("_", "") == name);
            }
            if (f == null)
                return "0";
            else if (f.Value == null)
                return "0";
            else
                return f.Value.ToString();
        }
        public virtual object GetVariable(ModifierType tp)
        {
            Modifier f = ideaModifiers.FirstOrDefault(x => x.Any(y => y.Type == tp))?.FirstOrDefault(x => x.Type == tp) ?? null;
            if (f == null)
            {
                f = ambitionModifiers.FirstOrDefault(x => x.Type == tp);
            }
            if (f == null)
                return "0";
            else if (f.Value == null)
                return "0";
            else
                return f.Value.ToString();
        }
    }

    class BasicIdeas : IdeaSet
    {
        public string Type = "";
    }

    class NationalIdeas : IdeaSet
    {
        public string traditionName = "";
        public List<Modifier> traditionModifiers = new List<Modifier>();
       

        public override object GetVariable(QueryVariables vr)
        {
            string name = vr.ToString();
            Modifier f = ideaModifiers.FirstOrDefault(x => x.Any(y => y.Name.Replace("_", "") == name))?.FirstOrDefault(x => x.Name.Replace("_", "") == name) ?? null;
            if (f == null)
            {
                f = ambitionModifiers.FirstOrDefault(x => x.Name.Replace("_", "") == name);
            }
            if (f == null)
            {
                f = traditionModifiers.FirstOrDefault(x => x.Name.Replace("_", "") == name);
            }
            if (f == null)
                return "0";
            else if (f.Value == null)
                return "0";
            else
                return f.Value.ToString();
        }
        public override object GetVariable(ModifierType tp)
        {
            Modifier f = ideaModifiers.FirstOrDefault(x => x.Any(y => y.Type == tp))?.FirstOrDefault(x => x.Type == tp) ?? null;
            if (f == null)
            {
                f = ambitionModifiers.FirstOrDefault(x => x.Type == tp);
            }
            if (f == null)
            {
                f = traditionModifiers.FirstOrDefault(x => x.Type == tp);
            }
            if (f == null)
                return "0";
            else if (f.Value == null)
                return "0";
            else
                return f.Value.ToString();
        }

        public override int FirstPositionOf(ModifierType tp)
        {
            int Pos = Array.FindIndex(ideaModifiers, x => x.Any(y => y.Type == tp));
            if (Pos == -1)
            {
                if (ambitionModifiers.Any(x => x.Type == tp))
                    Pos = 8;
            }
            else
                Pos += 1;
            if (Pos == -1)
            {
                if (traditionModifiers.Any(x => x.Type == tp))
                    Pos = 0;
            }

            return Pos;
        }

    }




    public class Modifier
    {
        public static string[] ModifierNames = new string[] { "army_tradition", "ArmyTradition", "army_tradition_decay", "ArmyTraditionDecay",
        "army_tradition_from_battle", "ArmyTraditionFromBattle", "yearly_army_professionalism", "YearlyArmyProfessionalism", "drill_gain_modifier",
        "DrillGainModifier", "drill_decay_modifier", "DrillDecayModifier", "infantry_cost", "InfantryCost", "infantry_power", "InfantryPower",
        "infantry_fire", "InfantryFire", "infantry_shock", "InfantryShock", "cavalry_cost", "CavalryCost", "cavalry_power", "CavalryPower", "cavalry_fire",
        "CavalryFire", "cavalry_shock", "CavalryShock", "artillery_cost", "ArtilleryCost", "artillery_power", "ArtilleryPower", "artillery_fire", "ArtilleryFire",
        "artillery_shock", "ArtilleryShock", "cav_to_inf_ratio", "CavToInfRatio", "cavalry_flanking", "CavalryFlanking", "artillery_levels_available_vs_fort",
        "ArtilleryLevelsAvailableVsFort", "backrow_artillery_damage", "BackrowArtilleryDamage", "discipline", "Discipline", "mercenary_discipline",
        "MercenaryDiscipline", "land_morale", "LandMorale", "defensiveness", "Defensiveness", "siege_ability", "SiegeAbility", "movement_speed", "MovementSpeed",
        "fire_damage", "FireDamage", "fire_damage_received", "FireDamageReceived", "shock_damage", "ShockDamage", "shock_damage_received", "ShockDamageReceived",
        "morale_damage", "MoraleDamage", "morale_damage_received", "MoraleDamageReceived", "recover_army_morale_speed", "RecoverArmyMoraleSpeed",
        "siege_blockade_progress", "SiegeBlockadeProgress", "reserves_organisation", "ReservesOrganisation", "land_attrition", "LandAttrition",
        "reinforce_cost_modifier", "ReinforceCostModifier", "reinforce_speed", "ReinforceSpeed", "manpower_recovery_speed", "ManpowerRecoverySpeed",
        "global_manpower", "GlobalManpower", "global_manpower_modifier", "GlobalManpowerModifier", "global_regiment_cost", "GlobalRegimentCost",
        "global_regiment_recruit_speed", "GlobalRegimentRecruitSpeed", "global_supply_limit_modifier", "GlobalSupplyLimitModifier", "land_forcelimit",
        "LandForcelimit", "land_forcelimit_modifier", "LandForcelimitModifier", "land_maintenance_modifier", "LandMaintenanceModifier", "mercenary_cost",
        "MercenaryCost", "merc_maintenance_modifier", "MercMaintenanceModifier", "possible_condottieri", "PossibleCondottieri", "hostile_attrition",
        "HostileAttrition", "max_hostile_attrition", "MaxHostileAttrition", "garrison_size", "GarrisonSize", "global_garrison_growth", "GlobalGarrisonGrowth",
        "fort_maintenance_modifier", "FortMaintenanceModifier", "rival_border_fort_maintenance", "RivalBorderFortMaintenance", "war_exhaustion", "WarExhaustion",
        "war_exhaustion_cost", "WarExhaustionCost", "leader_land_fire", "LeaderLandFire", "leader_land_manuever", "LeaderLandManuever", "leader_land_shock",
        "LeaderLandShock", "leader_siege", "LeaderSiege", "general_cost", "GeneralCost", "free_leader_pool", "FreeLeaderPool", "free_land_leader_pool",
        "FreeLandLeaderPool", "free_navy_leader_pool", "FreeNavyLeaderPool", "raze_power_gain", "RazePowerGain", "loot_amount", "LootAmount",
        "available_province_loot", "AvailableProvinceLoot", "prestige_from_land", "PrestigeFromLand", "war_taxes_cost_modifier", "WarTaxesCostModifier",
        "leader_cost", "LeaderCost", "may_recruit_female_generals", "MayRecruitFemaleGenerals", "manpower_in_true_faith_provinces", "ManpowerInTrueFaithProvinces",
        "mercenary_manpower", "MercenaryManpower", "regiment_manpower_usage", "RegimentManpowerUsage", "military_tactics", "MilitaryTactics", "capped_by_forcelimit",
        "CappedByForcelimit", "global_attacker_dice_roll_bonus", "GlobalAttackerDiceRollBonus", "global_defender_dice_roll_bonus", "GlobalDefenderDiceRollBonus",
        "own_territory_dice_roll_bonus", "OwnTerritoryDiceRollBonus", "manpower_in_accepted_culture_provinces", "ManpowerInAcceptedCultureProvinces",
        "manpower_in_culture_group_provinces", "ManpowerInCultureGroupProvinces", "manpower_in_own_culture_provinces", "ManpowerInOwnCultureProvinces",
        "may_build_supply_depot", "MayBuildSupplyDepot", "may_refill_garrison", "MayRefillGarrison", "may_return_manpower_on_disband", "MayReturnManpowerOnDisband",
        "attack_bonus_in_capital_terrain", "AttackBonusInCapitalTerrain", "can_bypass_forts", "CanBypassForts", "force_march_free", "ForceMarchFree",
        "special_unit_forcelimit", "SpecialUnitForcelimit", "allowed_marine_fraction", "AllowedMarineFraction", "has_banners", "HasBanners", "amount_of_banners",
        "AmountOfBanners", "amount_of_cawa", "AmountOfCawa", "cawa_cost_modifier", "CawaCostModifier", "has_carolean", "HasCarolean", "amount_of_carolean",
        "AmountOfCarolean", "can_recruit_hussars", "CanRecruitHussars", "amount_of_hussars", "AmountOfHussars", "navy_tradition", "NavyTradition",
        "navy_tradition_decay", "NavyTraditionDecay	", "naval_tradition_from_battle", "NavalTraditionFromBattle", "naval_tradition_from_trade",
        "NavalTraditionFromTrade", "heavy_ship_cost", "HeavyShipCost", "heavy_ship_power", "HeavyShipPower", "light_ship_cost", "LightShipCost",
        "light_ship_power", "LightShipPower", "galley_cost", "GalleyCost", "galley_power", "GalleyPower", "transport_cost", "TransportCost",
        "transport_power", "TransportPower", "global_ship_cost", "GlobalShipCost", "global_ship_recruit_speed", "GlobalShipRecruitSpeed",
        "global_ship_repair", "GlobalShipRepair", "naval_forcelimit", "NavalForcelimit", "naval_forcelimit_modifier", "NavalForcelimitModifier",
        "naval_maintenance_modifier", "NavalMaintenanceModifier", "global_sailors", "GlobalSailors", "global_sailors_modifier", "GlobalSailorsModifier",
        "sailor_maintenance_modifer", "SailorMaintenanceModifer", "sailors_recovery_speed", "SailorsRecoverySpeed", "blockade_efficiency", "BlockadeEfficiency",
        "capture_ship_chance", "CaptureShipChance", "global_naval_engagement_modifier", "GlobalNavalEngagementModifier", "naval_attrition", "NavalAttrition",
        "naval_morale", "NavalMorale", "ship_durability", "ShipDurability", "sunk_ship_morale_hit_recieved", "SunkShipMoraleHitRecieved", "recover_navy_morale_speed",
        "RecoverNavyMoraleSpeed", "prestige_from_naval", "PrestigeFromNaval", "leader_naval_fire", "LeaderNavalFire", "leader_naval_manuever", "LeaderNavalManuever",
        "leader_naval_shock", "LeaderNavalShock", "own_coast_naval_combat_bonus", "OwnCoastNavalCombatBonus", "admiral_cost", "AdmiralCost",
        "global_naval_barrage_cost", "GlobalNavalBarrageCost", "flagship_cost", "FlagshipCost", "disengagement_chance", "DisengagementChance",
        "transport_attrition", "TransportAttrition", "landing_penalty", "LandingPenalty", "regiment_disembark_speed", "RegimentDisembarkSpeed",
        "may_perform_slave_raid", "MayPerformSlaveRaid", "may_perform_slave_raid_on_same_religion", "MayPerformSlaveRaidOnSameReligion", "sea_repair", "SeaRepair",
        "movement_speed_in_fleet_modifier", "MovementSpeedInFleetModifier", "admiral_skill_gain_modifier", "AdmiralSkillGainModifier", "flagship_durability",
        "FlagshipDurability", "flagship_morale", "FlagshipMorale", "flagship_naval_engagement_modifier", "FlagshipNavalEngagementModifier",
        "movement_speed_onto_off_boat_modifier", "MovementSpeedOntoOffBoatModifier", "flagship_landing_penalty", "FlagshipLandingPenalty",
        "number_of_cannons_flagship_modifier", "NumberOfCannonsFlagshipModifier", "number_of_cannons_flagship", "NumberOfCannonsFlagship",
        "naval_maintenance_flagship_modifier", "NavalMaintenanceFlagshipModifier", "trade_power_in_fleet_modifier", "TradePowerInFleetModifier",
        "morale_in_fleet_modifier", "MoraleInFleetModifier", "blockade_impact_on_siege_in_fleet_modifier", "BlockadeImpactOnSiegeInFleetModifier",
        "exploration_mission_range_in_fleet_modifier", "ExplorationMissionRangeInFleetModifier", "barrage_cost_in_fleet_modifier", "BarrageCostInFleetModifier",
        "naval_attrition_in_fleet_modifier", "NavalAttritionInFleetModifier", "privateering_efficiency_in_fleet_modifier", "PrivateeringEfficiencyInFleetModifier",
        "prestige_from_battles_in_fleet_modifier", "PrestigeFromBattlesInFleetModifier", "naval_tradition_in_fleet_modifier", "NavalTraditionInFleetModifier",
        "cannons_for_hunting_pirates_in_fleet", "CannonsForHuntingPiratesInFleet", "diplomats", "Diplomats", "diplomatic_reputation", "DiplomaticReputation",
        "diplomatic_upkeep", "DiplomaticUpkeep", "envoy_travel_time", "EnvoyTravelTime", "fabricate_claims_cost", "FabricateClaimsCost",
        "years_to_integrate_personal_union", "YearsToIntegratePersonalUnion", "improve_relation_modifier", "ImproveRelationModifier", "vassal_forcelimit_bonus",
        "VassalForcelimitBonus", "vassal_income", "VassalIncome", "ae_impact", "AeImpact", "claim_duration", "ClaimDuration", "diplomatic_annexation_cost",
        "DiplomaticAnnexationCost", "province_warscore_cost", "ProvinceWarscoreCost", "unjustified_demands", "UnjustifiedDemands", "rival_change_cost",
        "RivalChangeCost", "justify_trade_conflict_cost", "JustifyTradeConflictCost", "stability_cost_to_declare_war", "StabilityCostToDeclareWar",
        "accept_vassalization_reasons", "AcceptVassalizationReasons", "transfer_trade_power_reasons", "TransferTradePowerReasons", "monthly_federation_favor_growth",
        "MonthlyFederationFavorGrowth", "monthly_favor_modifier", "MonthlyFavorModifier", "can_chain_claim", "CanChainClaim", "cb_on_overseas", "CbOnOverseas",
        "cb_on_primitives", "CbOnPrimitives", "idea_claim_colonies", "IdeaClaimColonies", "cb_on_government_enemies", "CbOnGovernmentEnemies",
        "cb_on_religious_enemies", "CbOnReligiousEnemies", "reduced_stab_impacts", "ReducedStabImpacts", "can_fabricate_for_vassals", "CanFabricateForVassals",
        "global_tax_income", "GlobalTaxIncome", "global_tax_modifier", "GlobalTaxModifier", "production_efficiency", "ProductionEfficiency",
        "state_maintenance_modifier", "StateMaintenanceModifier", "inflation_action_cost", "InflationActionCost", "inflation_reduction", "InflationReduction",
        "monthly_gold_inflation_modifier", "MonthlyGoldInflationModifier", "gold_depletion_chance_modifier", "GoldDepletionChanceModifier", "interest", "Interest",
        "can_not_build_buildings", "CanNotBuildBuildings", "development_cost", "DevelopmentCost", "development_cost_modifier", "DevelopmentCostModifier",
        "tribal_development_growth", "TribalDevelopmentGrowth", "add_tribal_land_cost", "AddTribalLandCost", "settle_cost", "SettleCost",
        "global_allowed_num_of_buildings", "GlobalAllowedNumOfBuildings", "build_cost", "BuildCost", "build_time", "BuildTime", "great_project_upgrade_cost",
        "GreatProjectUpgradeCost", "global_monthly_devastation", "GlobalMonthlyDevastation", "global_prosperity_growth", "GlobalProsperityGrowth",
        "administrative_efficiency", "AdministrativeEfficiency", "core_creation", "CoreCreation", "core_decay_on_your_own", "CoreDecayOnYourOwn",
        "enemy_core_creation", "EnemyCoreCreation", "ignore_coring_distance", "IgnoreCoringDistance", "adm_cost_modifier", "AdmCostModifier", "dip_cost_modifier",
        "DipCostModifier", "mil_cost_modifier", "MilCostModifier", "technology_cost", "TechnologyCost", "idea_cost", "IdeaCost", "embracement_cost", "EmbracementCost",
        "global_institution_spread", "GlobalInstitutionSpread", "institution_spread_from_true_faith", "InstitutionSpreadFromTrueFaith", "native_advancement_cost",
        "NativeAdvancementCost", "all_power_cost", "AllPowerCost", "innovativeness_gain", "InnovativenessGain", "free_adm_policy", "FreeAdmPolicy", "free_dip_policy",
        "FreeDipPolicy", "free_mil_policy", "FreeMilPolicy", "possible_adm_policy", "PossibleAdmPolicy", "possible_dip_policy", "PossibleDipPolicy",
        "possible_mil_policy", "PossibleMilPolicy", "possible_policy", "PossiblePolicy", "free_policy", "FreePolicy", "country_admin_power", "CountryAdminPower",
        "country_diplomatic_power", "CountryDiplomaticPower", "country_military_power", "CountryMilitaryPower", "prestige", "Prestige", "prestige_decay",
        "PrestigeDecay", "monthly_splendor", "MonthlySplendor", "yearly_corruption", "YearlyCorruption", "advisor_cost", "AdvisorCost", "advisor_pool",
        "AdvisorPool", "female_advisor_chance", "FemaleAdvisorChance", "heir_chance", "HeirChance", "monthly_heir_claim_increase", "MonthlyHeirClaimIncrease",
        "monthly_heir_claim_increase_modifier", "MonthlyHeirClaimIncreaseModifier", "block_introduce_heir", "BlockIntroduceHeir", "monarch_admin_power",
        "MonarchAdminPower", "monarch_diplomatic_power", "MonarchDiplomaticPower", "monarch_military_power", "MonarchMilitaryPower", "adm_advisor_cost",
        "AdmAdvisorCost", "dip_advisor_cost", "DipAdvisorCost", "mil_advisor_cost", "MilAdvisorCost", "monthly_support_heir_gain", "MonthlySupportHeirGain",
        "power_projection_from_insults", "PowerProjectionFromInsults", "monarch_lifespan", "MonarchLifespan", "local_heir_adm", "LocalHeirAdm", "local_heir_dip",
        "LocalHeirDip", "local_heir_mil", "LocalHeirMil", "national_focus_years", "NationalFocusYears", "yearly_absolutism", "YearlyAbsolutism", "max_absolutism",
        "MaxAbsolutism", "legitimacy", "Legitimacy", "republican_tradition", "RepublicanTradition", "devotion", "Devotion", "horde_unity", "HordeUnity",
        "meritocracy", "Meritocracy", "monthly_militarized_society", "MonthlyMilitarizedSociety", "yearly_tribal_allegiance", "YearlyTribalAllegiance",
        "imperial_mandate", "ImperialMandate", "election_cycle", "ElectionCycle", "candidate_random_bonus", "CandidateRandomBonus", "reelection_cost",
        "ReelectionCost", "governing_capacity", "GoverningCapacity", "governing_capacity_modifier", "GoverningCapacityModifier", "governing_cost", "GoverningCost",
        "state_governing_cost", "StateGoverningCost", "trade_company_governing_cost", "TradeCompanyGoverningCost", "state_governing_cost_increase",
        "StateGoverningCostIncrease", "expand_administration_cost", "ExpandAdministrationCost", "yearly_revolutionary_zeal", "YearlyRevolutionaryZeal",
        "max_revolutionary_zeal", "MaxRevolutionaryZeal", "reform_progress_growth", "ReformProgressGrowth", "monthly_reform_progress", "MonthlyReformProgress",
        "monthly_reform_progress_modifier", "MonthlyReformProgressModifier", "move_capital_cost_modifier", "MoveCapitalCostModifier", "all_estate_influence_modifier",
        "AllEstateInfluenceModifier", "all_estate_loyalty_equilibrium", "AllEstateLoyaltyEquilibrium", "allow_free_estate_privilege_revocation",
        "AllowFreeEstatePrivilegeRevocation", "imperial_authority", "ImperialAuthority", "imperial_authority_value", "ImperialAuthorityValue",
        "free_city_imperial_authority", "FreeCityImperialAuthority", "reasons_to_elect", "ReasonsToElect", "imperial_mercenary_cost", "ImperialMercenaryCost",
        "max_free_cities", "MaxFreeCities", "max_electors", "MaxElectors", "legitimate_subject_elector", "LegitimateSubjectElector",
        "manpower_against_imperial_enemies", "ManpowerAgainstImperialEnemies", "imperial_reform_catholic_approval", "ImperialReformCatholicApproval",
        "culture_conversion_cost", "CultureConversionCost", "num_accepted_cultures", "NumAcceptedCultures", "same_culture_advisor_cost", "SameCultureAdvisorCost",
        "promote_culture_cost", "PromoteCultureCost", "relation_with_same_culture", "RelationWithSameCulture", "relation_with_same_culture_group",
        "RelationWithSameCultureGroup", "relation_with_accepted_culture", "RelationWithAcceptedCulture", "relation_with_other_culture", "RelationWithOtherCulture",
        "can_not_declare_war", "CanNotDeclareWar", "global_unrest", "GlobalUnrest", "stability_cost_modifier", "StabilityCostModifier", "global_autonomy",
        "GlobalAutonomy", "min_autonomy", "MinAutonomy", "autonomy_change_time", "AutonomyChangeTime", "harsh_treatment_cost", "HarshTreatmentCost",
        "global_rebel_suppression_efficiency", "GlobalRebelSuppressionEfficiency", "years_of_nationalism", "YearsOfNationalism", "min_autonomy_in_territories",
        "MinAutonomyInTerritories", "unrest_catholic_provinces", "UnrestCatholicProvinces", "no_stability_loss_on_monarch_death", "NoStabilityLossOnMonarchDeath",
        "can_transfer_vassal_wargoal", "CanTransferVassalWargoal", "liberty_desire", "LibertyDesire", "liberty_desire_from_subject_development",
        "LibertyDesireFromSubjectDevelopment", "reduced_liberty_desire", "ReducedLibertyDesire", "reduced_liberty_desire_on_same_continent",
        "ReducedLibertyDesireOnSameContinent", "allow_client_states", "AllowClientStates", "colonial_type_change_cost_modifier", "ColonialTypeChangeCostModifier",
        "colonial_subject_type_upgrade_cost_modifier", "ColonialSubjectTypeUpgradeCostModifier", "spy_offence", "SpyOffence", "global_spy_defence", "GlobalSpyDefence",
        "discovered_relations_impact", "DiscoveredRelationsImpact", "rebel_support_efficiency", "RebelSupportEfficiency", "global_missionary_strength",
        "GlobalMissionaryStrength", "global_heretic_missionary_strength", "GlobalHereticMissionaryStrength", "global_heathen_missionary_strength",
        "GlobalHeathenMissionaryStrength", "can_not_build_missionaries", "CanNotBuildMissionaries", "missionaries", "Missionaries", "missionary_maintenance_cost",
        "MissionaryMaintenanceCost", "religious_unity", "ReligiousUnity", "tolerance_own", "ToleranceOwn", "tolerance_heretic", "ToleranceHeretic",
        "tolerance_heathen", "ToleranceHeathen", "tolerance_of_heretics_capacity", "ToleranceOfHereticsCapacity", "tolerance_of_heathens_capacity",
        "ToleranceOfHeathensCapacity", "papal_influence", "PapalInfluence", "papal_influence_from_cardinals", "PapalInfluenceFromCardinals", "appoint_cardinal_cost",
        "AppointCardinalCost", "curia_treasury_contribution", "CuriaTreasuryContribution", "curia_powers_cost", "CuriaPowersCost", "monthly_church_power",
        "MonthlyChurchPower", "church_power_modifier", "ChurchPowerModifier", "monthly_fervor_increase", "MonthlyFervorIncrease", "yearly_patriarch_authority",
        "YearlyPatriarchAuthority", "monthly_piety", "MonthlyPiety", "monthly_piety_accelerator", "MonthlyPietyAccelerator", "harmonization_speed",
        "HarmonizationSpeed", "yearly_harmony", "YearlyHarmony", "monthly_karma", "MonthlyKarma", "monthly_karma_accelerator", "MonthlyKarmaAccelerator",
        "yearly_karma_decay", "YearlyKarmaDecay", "yearly_doom_reduction", "YearlyDoomReduction", "yearly_authority", "YearlyAuthority", "enforce_religion_cost",
        "EnforceReligionCost", "prestige_per_development_from_conversion", "PrestigePerDevelopmentFromConversion", "warscore_cost_vs_other_religion",
        "WarscoreCostVsOtherReligion", "establish_order_cost", "EstablishOrderCost", "global_religious_conversion_resistance", "GlobalReligiousConversionResistance",
        "relation_with_same_religion", "RelationWithSameReligion", "relation_with_heretics", "RelationWithHeretics", "relation_with_heathens", "RelationWithHeathens",
        "no_religion_penalty", "NoReligionPenalty", "extra_manpower_at_religious_war", "ExtraManpowerAtReligiousWar", "can_not_build_colonies", "CanNotBuildColonies",
        "colonists", "Colonists", "colonist_placement_chance", "ColonistPlacementChance", "global_colonial_growth", "GlobalColonialGrowth", "range", "Range",
        "native_uprising_chance", "NativeUprisingChance", "native_assimilation", "NativeAssimilation", "migration_cost", "MigrationCost", "global_tariffs",
        "GlobalTariffs", "treasure_fleet_income", "TreasureFleetIncome", "expel_minorities_cost", "ExpelMinoritiesCost", "may_explore", "MayExplore",
        "auto_explore_adjacent_to_colony", "AutoExploreAdjacentToColony", "may_establish_frontier", "MayEstablishFrontier", "can_colony_boost_development",
        "CanColonyBoostDevelopment", "free_maintenance_on_expl_conq", "FreeMaintenanceOnExplConq", "caravan_power", "CaravanPower", "can_not_send_merchants",
        "CanNotSendMerchants", "merchants", "Merchants", "placed_merchant_power", "PlacedMerchantPower", "global_trade_power", "GlobalTradePower",
        "global_foreign_trade_power", "GlobalForeignTradePower", "global_own_trade_power", "GlobalOwnTradePower", "global_prov_trade_power_modifier",
        "GlobalProvTradePowerModifier", "global_trade_goods_size_modifier", "GlobalTradeGoodsSizeModifier", "global_trade_goods_size", "GlobalTradeGoodsSize",
        "trade_efficiency", "TradeEfficiency", "trade_range_modifier", "TradeRangeModifier", "trade_steering", "TradeSteering", "global_ship_trade_power",
        "GlobalShipTradePower", "privateer_efficiency", "PrivateerEfficiency", "embargo_efficiency", "EmbargoEfficiency", "ship_power_propagation",
        "ShipPowerPropagation", "center_of_trade_upgrade_cost", "CenterOfTradeUpgradeCost", "trade_company_investment_cost", "TradeCompanyInvestmentCost",
        "mercantilism_cost", "MercantilismCost", "max_attrition", "MaxAttrition", "attrition", "Attrition", "local_hostile_attrition", "LocalHostileAttrition",
        "local_fort_maintenance_modifier", "LocalFortMaintenanceModifier", "local_garrison_size", "LocalGarrisonSize", "local_attacker_dice_roll_bonus",
        "LocalAttackerDiceRollBonus", "local_defender_dice_roll_bonus", "LocalDefenderDiceRollBonus", "fort_level", "FortLevel", "garrison_growth", "GarrisonGrowth",
        "local_defensiveness", "LocalDefensiveness", "local_friendly_movement_speed", "LocalFriendlyMovementSpeed", "local_hostile_movement_speed", "LocalHostileMovementSpeed",
        "local_manpower", "LocalManpower", "local_manpower_modifier", "LocalManpowerModifier", "local_regiment_cost", "LocalRegimentCost", "regiment_recruit_speed",
        "RegimentRecruitSpeed", "supply_limit", "SupplyLimit", "supply_limit_modifier", "SupplyLimitModifier", "local_own_coast_naval_combat_bonus", "LocalOwnCoastNavalCombatBonus", "local_has_banners", "LocalHasBanners", "local_amount_of_banners", "LocalAmountOfBanners", "local_amount_of_cawa", "LocalAmountOfCawa", "local_has_carolean", "LocalHasCarolean", "local_amount_of_carolean", "LocalAmountOfCarolean", "local_amount_of_hussars", "LocalAmountOfHussars", "local_naval_engagement_modifier", "LocalNavalEngagementModifier", "local_sailors", "LocalSailors", "local_sailors_modifier", "LocalSailorsModifier", "local_ship_cost", "LocalShipCost", "local_ship_repair", "LocalShipRepair", "ship_recruit_speed", "ShipRecruitSpeed", "blockade_force_required", "BlockadeForceRequired", "hostile_disembark_speed", "HostileDisembarkSpeed", "hostile_fleet_attrition", "HostileFleetAttrition", "block_slave_raid", "BlockSlaveRaid", "local_warscore_cost_modifier", "LocalWarscoreCostModifier", "inflation_reduction_local", "InflationReductionLocal", "local_state_maintenance_modifier", "LocalStateMaintenanceModifier", "local_build_cost", "LocalBuildCost", "local_build_time", "LocalBuildTime", "local_great_project_upgrade_cost", "LocalGreatProjectUpgradeCost", "local_monthly_devastation", "LocalMonthlyDevastation", "local_prosperity_growth", "LocalProsperityGrowth", "local_production_efficiency", "LocalProductionEfficiency", "local_tax_modifier", "LocalTaxModifier", "tax_income", "TaxIncome", "allowed_num_of_buildings", "AllowedNumOfBuildings", "allowed_num_of_manufactories", "AllowedNumOfManufactories", "local_development_cost", "LocalDevelopmentCost", "local_development_cost_modifier", "LocalDevelopmentCostModifier", "local_gold_depletion_chance_modifier", "LocalGoldDepletionChanceModifier", "local_institution_spread", "LocalInstitutionSpread", "local_core_creation", "LocalCoreCreation", "local_governing_cost", "LocalGoverningCost", "statewide_governing_cost", "StatewideGoverningCost", "local_governing_cost_increase", "LocalGoverningCostIncrease", "institution_growth", "InstitutionGrowth", "local_culture_conversion_cost", "LocalCultureConversionCost", "local_unrest", "LocalUnrest", "local_autonomy", "LocalAutonomy", "local_years_of_nationalism", "LocalYearsOfNationalism", "min_local_autonomy", "MinLocalAutonomy", "local_missionary_strength", "LocalMissionaryStrength", "local_religious_unity_contribution", "LocalReligiousUnityContribution", "local_missionary_maintenance_cost", "LocalMissionaryMaintenanceCost", "local_religious_conversion_resistance", "LocalReligiousConversionResistance", "local_colonial_growth", "LocalColonialGrowth", "local_colonist_placement_chance", "LocalColonistPlacementChance", "province_trade_power_modifier", "ProvinceTradePowerModifier", "province_trade_power_value", "ProvinceTradePowerValue", "trade_goods_size_modifier", "TradeGoodsSizeModifier", "trade_goods_size", "TradeGoodsSize", "trade_value_modifier", "TradeValueModifier", "trade_value", "TradeValue", "local_center_of_trade_upgrade_cost", "LocalCenterOfTradeUpgradeCost" };

        public static string[] ModifierNamesUnedited = new string[]
        {
            "army_tradition", "army_tradition_decay", "army_tradition_from_battle", "yearly_army_professionalism", "drill_gain_modifier", "drill_decay_modifier",
"infantry_cost", "infantry_power	", "infantry_fire", "infantry_shock", "cavalry_cost", "cavalry_power",
"cavalry_fire", "cavalry_shock", "artillery_cost", "artillery_power", "artillery_fire", "artillery_shock",
"cav_to_inf_ratio", "cavalry_flanking", "artillery_levels_available_vs_fort", "backrow_artillery_damage", "discipline", "mercenary_discipline",
"land_morale", "defensiveness", "siege_ability", "movement_speed", "fire_damage", "fire_damage_received",
"shock_damage", "shock_damage_received", "morale_damage", "morale_damage_received", "recover_army_morale_speed", "siege_blockade_progress",
"reserves_organisation", "land_attrition", "reinforce_cost_modifier", "reinforce_speed", "manpower_recovery_speed", "global_manpower",
"global_manpower_modifier", "global_regiment_cost", "global_regiment_recruit_speed", "global_supply_limit_modifier", "land_forcelimit", "land_forcelimit_modifier",
"land_maintenance_modifier", "mercenary_cost	", "merc_maintenance_modifier", "possible_condottieri", "hostile_attrition", "max_hostile_attrition",
"garrison_size", "global_garrison_growth", "fort_maintenance_modifier", "rival_border_fort_maintenance", "war_exhaustion", "war_exhaustion_cost",
"leader_land_fire", "leader_land_manuever", "leader_land_shock", "leader_siege", "general_cost", "free_leader_pool",
"free_land_leader_pool", "free_navy_leader_pool", "raze_power_gain", "loot_amount", "available_province_loot", "prestige_from_land",
"war_taxes_cost_modifier", "leader_cost", "may_recruit_female_generals", "manpower_in_true_faith_provinces", "mercenary_manpower", "regiment_manpower_usage",
"military_tactics", "capped_by_forcelimit", "global_attacker_dice_roll_bonus", "global_defender_dice_roll_bonus", "own_territory_dice_roll_bonus", "manpower_in_accepted_culture_provinces",
"manpower_in_culture_group_provinces", "manpower_in_own_culture_provinces", "may_build_supply_depot", "may_refill_garrison", "may_return_manpower_on_disband", "attack_bonus_in_capital_terrain",
"can_bypass_forts", "force_march_free", "special_unit_forcelimit", "allowed_marine_fraction", "has_banners", "amount_of_banners",
"amount_of_cawa", "cawa_cost_modifier", "has_carolean", "amount_of_carolean", "can_recruit_hussars", "amount_of_hussars",
"navy_tradition", "navy_tradition_decay	", "naval_tradition_from_battle", "naval_tradition_from_trade", "heavy_ship_cost	", "heavy_ship_power",
"light_ship_cost", "light_ship_power", "galley_cost", "galley_power", "transport_cost", "transport_power",
"global_ship_cost", "global_ship_recruit_speed", "global_ship_repair", "naval_forcelimit", "naval_forcelimit_modifier", "naval_maintenance_modifier",
"global_sailors", "global_sailors_modifier", "sailor_maintenance_modifer", "sailors_recovery_speed", "blockade_efficiency", "capture_ship_chance",
"global_naval_engagement_modifier", "naval_attrition", "naval_morale", "ship_durability", "sunk_ship_morale_hit_recieved", "recover_navy_morale_speed	",
"prestige_from_naval", "leader_naval_fire", "leader_naval_manuever", "leader_naval_shock", "own_coast_naval_combat_bonus", "admiral_cost",
"global_naval_barrage_cost", "flagship_cost", "disengagement_chance", "transport_attrition", "landing_penalty", "regiment_disembark_speed",
"may_perform_slave_raid", "may_perform_slave_raid_on_same_religion", "sea_repair", "movement_speed_in_fleet_modifier", "admiral_skill_gain_modifier", "flagship_durability",
"flagship_morale", "flagship_naval_engagement_modifier", "movement_speed_onto_off_boat_modifier", "flagship_landing_penalty", "number_of_cannons_flagship_modifier", "number_of_cannons_flagship",
"naval_maintenance_flagship_modifier", "trade_power_in_fleet_modifier", "morale_in_fleet_modifier", "blockade_impact_on_siege_in_fleet_modifier", "exploration_mission_range_in_fleet_modifier", "barrage_cost_in_fleet_modifier",
"naval_attrition_in_fleet_modifier", "privateering_efficiency_in_fleet_modifier", "prestige_from_battles_in_fleet_modifier", "naval_tradition_in_fleet_modifier", "cannons_for_hunting_pirates_in_fleet", "diplomats",
"diplomatic_reputation", "diplomatic_upkeep", "envoy_travel_time", "fabricate_claims_cost", "years_to_integrate_personal_union", "improve_relation_modifier",
"vassal_forcelimit_bonus", "vassal_income", "ae_impact", "claim_duration", "diplomatic_annexation_cost", "province_warscore_cost",
"unjustified_demands", "rival_change_cost", "justify_trade_conflict_cost", "stability_cost_to_declare_war", "accept_vassalization_reasons", "transfer_trade_power_reasons",
"monthly_federation_favor_growth", "monthly_favor_modifier", "can_chain_claim", "cb_on_overseas", "cb_on_primitives", "idea_claim_colonies",
"cb_on_government_enemies", "cb_on_religious_enemies", "reduced_stab_impacts", "can_fabricate_for_vassals", "global_tax_income", "global_tax_modifier",
"production_efficiency", "state_maintenance_modifier", "inflation_action_cost", "inflation_reduction", "monthly_gold_inflation_modifier", "gold_depletion_chance_modifier",
"interest", "can_not_build_buildings", "development_cost", "development_cost_modifier", "tribal_development_growth", "add_tribal_land_cost",
"settle_cost", "global_allowed_num_of_buildings", "build_cost", "build_time", "great_project_upgrade_cost", "global_monthly_devastation",
"global_prosperity_growth", "administrative_efficiency", "core_creation", "core_decay_on_your_own", "enemy_core_creation", "ignore_coring_distance",
"adm_cost_modifier", "dip_cost_modifier", "mil_cost_modifier", "technology_cost", "idea_cost", "embracement_cost",
"global_institution_spread", "institution_spread_from_true_faith", "native_advancement_cost", "all_power_cost", "innovativeness_gain", "free_adm_policy",
"free_dip_policy", "free_mil_policy", "possible_adm_policy", "possible_dip_policy", "possible_mil_policy", "possible_policy",
"free_policy", "country_admin_power", "country_diplomatic_power", "country_military_power", "prestige", "prestige_decay",
"monthly_splendor", "yearly_corruption", "advisor_cost", "advisor_pool", "female_advisor_chance", "heir_chance",
"monthly_heir_claim_increase", "monthly_heir_claim_increase_modifier", "block_introduce_heir", "monarch_admin_power", "monarch_diplomatic_power", "monarch_military_power",
"adm_advisor_cost", "dip_advisor_cost", "mil_advisor_cost", "monthly_support_heir_gain", "power_projection_from_insults", "monarch_lifespan",
"local_heir_adm", "local_heir_dip", "local_heir_mil", "national_focus_years", "yearly_absolutism", "max_absolutism",
"legitimacy", "republican_tradition", "devotion", "horde_unity", "meritocracy", "monthly_militarized_society",
"yearly_tribal_allegiance", "imperial_mandate", "election_cycle", "candidate_random_bonus", "reelection_cost", "governing_capacity",
"governing_capacity_modifier", "governing_cost", "state_governing_cost", "trade_company_governing_cost", "state_governing_cost_increase", "expand_administration_cost",
"yearly_revolutionary_zeal", "max_revolutionary_zeal", "reform_progress_growth", "monthly_reform_progress", "monthly_reform_progress_modifier", "move_capital_cost_modifier",
"all_estate_influence_modifier", "all_estate_loyalty_equilibrium", "allow_free_estate_privilege_revocation", "imperial_authority", "imperial_authority_value", "free_city_imperial_authority",
"reasons_to_elect", "imperial_mercenary_cost", "max_free_cities", "max_electors", "legitimate_subject_elector", "manpower_against_imperial_enemies",
"imperial_reform_catholic_approval", "culture_conversion_cost", "num_accepted_cultures", "same_culture_advisor_cost", "promote_culture_cost", "relation_with_same_culture",
"relation_with_same_culture_group", "relation_with_accepted_culture", "relation_with_other_culture", "can_not_declare_war", "global_unrest", "stability_cost_modifier",
"global_autonomy", "min_autonomy", "autonomy_change_time", "harsh_treatment_cost", "global_rebel_suppression_efficiency", "years_of_nationalism",
"min_autonomy_in_territories", "unrest_catholic_provinces", "no_stability_loss_on_monarch_death", "can_transfer_vassal_wargoal", "liberty_desire", "liberty_desire_from_subject_development",
"reduced_liberty_desire", "reduced_liberty_desire_on_same_continent", "allow_client_states", "colonial_type_change_cost_modifier", "colonial_subject_type_upgrade_cost_modifier", "spy_offence",
"global_spy_defence", "discovered_relations_impact", "rebel_support_efficiency", "global_missionary_strength", "global_heretic_missionary_strength", "global_heathen_missionary_strength",
"can_not_build_missionaries", "missionaries", "missionary_maintenance_cost", "religious_unity", "tolerance_own", "tolerance_heretic",
"tolerance_heathen", "tolerance_of_heretics_capacity", "tolerance_of_heathens_capacity", "papal_influence", "papal_influence_from_cardinals", "appoint_cardinal_cost",
"curia_treasury_contribution", "curia_powers_cost", "monthly_church_power", "church_power_modifier", "monthly_fervor_increase", "yearly_patriarch_authority",
"monthly_piety", "monthly_piety_accelerator", "harmonization_speed", "yearly_harmony", "monthly_karma", "monthly_karma_accelerator",
"yearly_karma_decay", "yearly_doom_reduction", "yearly_authority", "enforce_religion_cost", "prestige_per_development_from_conversion", "warscore_cost_vs_other_religion",
"establish_order_cost", "global_religious_conversion_resistance", "relation_with_same_religion", "relation_with_heretics", "relation_with_heathens", "no_religion_penalty",
"extra_manpower_at_religious_war", "can_not_build_colonies", "colonists", "colonist_placement_chance", "global_colonial_growth", "range",
"native_uprising_chance", "native_assimilation", "migration_cost", "global_tariffs", "treasure_fleet_income", "expel_minorities_cost",
"may_explore", "auto_explore_adjacent_to_colony", "may_establish_frontier", "can_colony_boost_development", "free_maintenance_on_expl_conq", "caravan_power",
"can_not_send_merchants", "merchants", "placed_merchant_power", "global_trade_power", "global_foreign_trade_power", "global_own_trade_power",
"global_prov_trade_power_modifier", "global_trade_goods_size_modifier", "global_trade_goods_size", "trade_efficiency", "trade_range_modifier", "trade_steering",
"global_ship_trade_power", "privateer_efficiency", "embargo_efficiency", "ship_power_propagation", "center_of_trade_upgrade_cost", "trade_company_investment_cost",
"mercantilism_cost", "max_attrition", "attrition", "local_hostile_attrition", "local_fort_maintenance_modifier", "local_garrison_size",
"local_attacker_dice_roll_bonus", "local_defender_dice_roll_bonus", "fort_level", "garrison_growth", "local_defensiveness", "local_friendly_movement_speed",
"local_hostile_movement_speed", "local_manpower", "local_manpower_modifier", "local_regiment_cost", "regiment_recruit_speed", "supply_limit",
"supply_limit_modifier", "local_own_coast_naval_combat_bonus", "local_has_banners", "local_amount_of_banners", "local_amount_of_cawa", "local_has_carolean",
"local_amount_of_carolean", "local_amount_of_hussars", "local_naval_engagement_modifier", "local_sailors", "local_sailors_modifier", "local_ship_cost",
"local_ship_repair", "ship_recruit_speed", "blockade_force_required", "hostile_disembark_speed", "hostile_fleet_attrition", "block_slave_raid",
"local_warscore_cost_modifier", "inflation_reduction_local", "local_state_maintenance_modifier", "local_build_cost", "local_build_time", "local_great_project_upgrade_cost",
"local_monthly_devastation", "local_prosperity_growth", "local_production_efficiency", "local_tax_modifier", "tax_income", "allowed_num_of_buildings",
"allowed_num_of_manufactories", "local_development_cost", "local_development_cost_modifier", "local_gold_depletion_chance_modifier", "local_institution_spread", "local_core_creation",
"local_governing_cost", "statewide_governing_cost", "local_governing_cost_increase", "institution_growth", "local_culture_conversion_cost", "local_unrest",
"local_autonomy", "local_years_of_nationalism", "min_local_autonomy", "local_missionary_strength", "local_religious_unity_contribution", "local_missionary_maintenance_cost",
"local_religious_conversion_resistance", "local_colonial_growth", "local_colonist_placement_chance", "province_trade_power_modifier", "province_trade_power_value", "trade_goods_size_modifier",
"trade_goods_size", "trade_value_modifier", "trade_value", "local_center_of_trade_upgrade_cost"};

        public static CountryModifierType ConvertToCountry(ModifierType t)
        {
            int n = (int)t;
            if (n > 415)
                return CountryModifierType.MercantilismCost;
            else if (n == 0)
                return CountryModifierType.ArmyTradition;
            else
                return (CountryModifierType)(n - 1);
        }
        public static ModifierType ConvertFromCountry(CountryModifierType t)
        {
            int n = (int)t;
            return (ModifierType)t;
        }

        public static ModifierType ConvertIntoModifierType(string input)
        {
            ModifierType Type = ModifierType.NoType;
            switch (input.ToLower())
            {
                case "army_tradition":
                case "at":
                case "armytradition":
                    Type = ModifierType.ArmyTradition;
                    break;
                case "army_tradition_decay":
                case "atd":
                case "atdecay":
                case "armytraditiondecay":
                    Type = ModifierType.ArmyTraditionDecay;
                    break;
                case "army_tradition_from_battle":
                case "armytraditionfrombattle":
                case "armytraditionfrombattles":
                case "army_tradition_from_battles":
                case "atfb":
                    Type = ModifierType.ArmyTraditionFromBattle;
                    break;
                case "yearly_army_professionalism":
                case "yap":
                case "yearlyarmyprofessionalism":
                case "yearlyap":
                    Type = ModifierType.YearlyArmyProfessionalism;
                    break;
                case "drill_gain_modifier":
                case "drillgainmodifier":
                case "drillgain":
                    Type = ModifierType.DrillGainModifier;
                    break;
                case "drill_decay_modifier":
                case "drilldecaymodifier":
                case "drilldecay":
                    Type = ModifierType.DrillDecayModifier;
                    break;
                case "infantry_cost":
                case "infantrycost":
                    Type = ModifierType.InfantryCost;
                    break;
                case "infantry_power":
                case "infantrypower":
                    Type = ModifierType.InfantryPower;
                    break;
                case "infantry_fire":
                case "infantryfire":
                    Type = ModifierType.InfantryFire;
                    break;
                case "infantry_shock":
                case "infantryshock":
                    Type = ModifierType.InfantryShock;
                    break;
                case "cavalry_cost":
                case "cavalrycost":
                case "cavcost":
                    Type = ModifierType.CavalryCost;
                    break;
                case "cavalry_power":
                case "cavalrypower":
                case "cavpower":
                    Type = ModifierType.CavalryPower;
                    break;
                case "cavalry_fire":
                case "cavalryfire":
                case "cavfire":
                    Type = ModifierType.CavalryFire;
                    break;
                case "cavalry_shock":
                case "cavalryshock":
                case "cavshock":
                    Type = ModifierType.CavalryShock;
                    break;
                case "artillery_cost":
                case "artillerycost":
                case "artcost":
                    Type = ModifierType.ArtilleryCost;
                    break;
                case "artillery_power":
                case "artillerypower":
                case "artpower":
                    Type = ModifierType.ArtilleryPower;
                    break;
                case "artillery_fire":
                case "artilleryfire":
                case "artfire":
                    Type = ModifierType.ArtilleryFire;
                    break;
                case "artillery_shock":
                case "artilleryshock":
                case "artshock":
                    Type = ModifierType.ArtilleryShock;
                    break;
                case "cav_to_inf_ratio":
                case "cavinfratio":
                case "cavtoinfratio":
                case "cavratio":
                    Type = ModifierType.CavToInfRatio;
                    break;
                case "cavalry_flanking":
                case "flanking":
                case "cav_flanking":
                case "cavalryflanking":
                    Type = ModifierType.CavalryFlanking;
                    break;
                case "artillery_levels_available_vs_fort":
                case "artfort":
                case "artillery_vs_fort":
                case "artillerylevelsavailablevsfort":
                    Type = ModifierType.ArtilleryLevelsAvailableVsFort;
                    break;
                case "backrow_artillery_damage":
                case "backrow_damage":
                case "backrowartillerydamage":
                case "backrowdamage":
                    Type = ModifierType.BackrowArtilleryDamage;
                    break;
                case "discipline":
                    Type = ModifierType.Discipline;
                    break;
                case "mercenary_discipline":
                case "mercenarydiscipline":
                case "mercdisc":
                case "mercdiscipline":
                    Type = ModifierType.MercenaryDiscipline;
                    break;
                case "land_morale":
                case "morale":
                    Type = ModifierType.LandMorale;
                    break;
                case "defensiveness":
                    Type = ModifierType.Defensiveness;
                    break;
                case "siege_ability":
                case "siegeability":
                    Type = ModifierType.SiegeAbility;
                    break;
                case "movement_speed":
                case "movementspeed":
                    Type = ModifierType.MovementSpeed;
                    break;
                case "fire_damage":
                case "firedamage":
                    Type = ModifierType.FireDamage;
                    break;
                case "fire_damage_received":
                case "firedamagereceived":
                    Type = ModifierType.FireDamageReceived;
                    break;
                case "shock_damage":
                case "shockdamage":
                    Type = ModifierType.ShockDamage;
                    break;
                case "shock_damage_received":
                case "shockdamagereceived":
                    Type = ModifierType.ShockDamageReceived;
                    break;
                case "morale_damage":
                case "moraledamage":
                    Type = ModifierType.MoraleDamage;
                    break;
                case "morale_damage_received":
                case "moraledamagereceived":
                    Type = ModifierType.MoraleDamageReceived;
                    break;
                case "recover_army_morale_speed":
                case "recoverarmymoralespeed":
                    Type = ModifierType.RecoverArmyMoraleSpeed;
                    break;
                case "siege_blockade_progress":
                case "siegeblockadeprogress":
                    Type = ModifierType.SiegeBlockadeProgress;
                    break;
                case "reserves_organisation":
                case "reservesorganisation":
                    Type = ModifierType.ReservesOrganisation;
                    break;
                case "land_attrition":
                case "landattrition":
                    Type = ModifierType.LandAttrition;
                    break;
                case "reinforce_cost_modifier":
                case "reinforcecostmodifier":
                case "reinforcecost":
                case "reinforce_cost":
                    Type = ModifierType.ReinforceCostModifier;
                    break;
                case "reinforce_speed":
                case "reinforcespeed":
                    Type = ModifierType.ReinforceSpeed;
                    break;
                case "manpower_recovery_speed":
                case "manpowerrecoveryspeed":
                case "manpower_recovery":
                case "manpowerrecovery":
                    Type = ModifierType.ManpowerRecoverySpeed;
                    break;
                case "global_manpower":
                case "globalmanpower":
                    Type = ModifierType.GlobalManpower;
                    break;
                case "global_manpower_modifier":
                case "globalmanpowermodifier":
                    Type = ModifierType.GlobalManpowerModifier;
                    break;
                case "global_regiment_cost":
                case "globalregimentcost":
                    Type = ModifierType.GlobalRegimentCost;
                    break;
                case "global_regiment_recruit_speed":
                case "globalregimentrecruitspeed":
                    Type = ModifierType.GlobalRegimentRecruitSpeed;
                    break;
                case "global_supply_limit_modifier":
                case "globalsupplylimitmodifier":
                    Type = ModifierType.GlobalSupplyLimitModifier;
                    break;
                case "land_forcelimit":
                case "landforcelimit":
                case "forcelimit":
                    Type = ModifierType.LandForcelimit;
                    break;
                case "land_forcelimit_modifier":
                case "landforcelimitmodifier":
                case "forcelimit_modifier":
                case "forcelimitmodifier":
                    Type = ModifierType.LandForcelimitModifier;
                    break;
                case "land_maintenance_modifier":
                case "land_maintenance":
                case "landmaintenance":
                case "landmaintenancemodifier":
                    Type = ModifierType.LandMaintenanceModifier;
                    break;
                case "mercenary_cost":
                case "mercenarycost":
                case "merccost":
                case "merc_cost":
                    Type = ModifierType.MercenaryCost;
                    break;
                case "merc_maintenance_modifier":
                case "mercmaintenancemodifier":
                    Type = ModifierType.MercMaintenanceModifier;
                    break;
                case "possible_condottieri":
                case "possbilecondottieri":
                    Type = ModifierType.PossibleCondottieri;
                    break;
                case "hostile_attrition":
                case "hostileattrition":
                    Type = ModifierType.HostileAttrition;
                    break;
                case "max_hostile_attrition":
                case "maxhostileattrition":
                    Type = ModifierType.MaxHostileAttrition;
                    break;
                case "garrison_size":
                case "garrisonsize":
                    Type = ModifierType.GarrisonSize;
                    break;
                case "global_garrison_growth":
                case "globalgarrisongrowth":
                    Type = ModifierType.GlobalGarrisonGrowth;
                    break;
                case "fort_maintenance_modifier":
                case "fortmaintenancemodifier":
                    Type = ModifierType.FortMaintenanceModifier;
                    break;
                case "rival_border_fort_maintenance":
                case "rivalborderfortmaintenance":
                    Type = ModifierType.RivalBorderFortMaintenance;
                    break;
                case "war_exhaustion":
                case "warexhaustion":
                    Type = ModifierType.WarExhaustion;
                    break;
                case "war_exhaustion_cost":
                case "warexhaustioncost":
                    Type = ModifierType.WarExhaustionCost;
                    break;
                case "leader_land_fire":
                case "leaderlandfire":
                    Type = ModifierType.LeaderLandFire;
                    break;
                case "leader_land_manuever":
                case "leaderlandmanuever":
                    Type = ModifierType.LeaderLandManuever;
                    break;
                case "leader_land_shock":
                case "leaderlandshock":
                    Type = ModifierType.LeaderLandShock;
                    break;
                case "leader_siege":
                case "leadersiege":
                    Type = ModifierType.LeaderSiege;
                    break;
                case "general_cost":
                case "generalcost":
                    Type = ModifierType.GeneralCost;
                    break;
                case "free_leader_pool":
                case "freeleaderpool":
                    Type = ModifierType.FreeLeaderPool;
                    break;
                case "free_land_leader_pool":
                case "freelandleaderpool":
                    Type = ModifierType.FreeLandLeaderPool;
                    break;
                case "free_navy_leader_pool":
                case "freenavyleaderpool":
                    Type = ModifierType.FreeNavyLeaderPool;
                    break;
                case "raze_power_gain":
                case "razepowergain":
                    Type = ModifierType.RazePowerGain;
                    break;
                case "loot_amount":
                case "lootamount":
                    Type = ModifierType.LootAmount;
                    break;
                case "available_province_loot":
                case "availableprovinceloot":
                    Type = ModifierType.AvailableProvinceLoot;
                    break;
                case "prestige_from_land":
                case "prestigefromland":
                    Type = ModifierType.PrestigeFromLand;
                    break;
                case "war_taxes_cost_modifier":
                case "wartaxescostmodifier":
                    Type = ModifierType.WarTaxesCostModifier;
                    break;
                case "leader_cost":
                case "leadercost":
                    Type = ModifierType.LeaderCost;
                    break;
                case "may_recruit_female_generals":
                case "mayrecruitfemalegenerals":
                    Type = ModifierType.MayRecruitFemaleGenerals;
                    break;
                case "manpower_in_true_faith_provinces":
                case "manpowerintruefaithprovinces":
                    Type = ModifierType.ManpowerInTrueFaithProvinces;
                    break;
                case "mercenary_manpower":
                case "mercenarymanpower":
                    Type = ModifierType.MercenaryManpower;
                    break;
                case "regiment_manpower_usage":
                case "regimentmanpowerusage":
                    Type = ModifierType.RegimentManpowerUsage;
                    break;
                case "military_tactics":
                case "militarytactics":
                    Type = ModifierType.MilitaryTactics;
                    break;
                case "capped_by_forcelimit":
                case "cappedbyforcelimit":
                    Type = ModifierType.CappedByForcelimit;
                    break;
                case "global_attacker_dice_roll_bonus":
                case "globalattackerdicerollbonus":
                    Type = ModifierType.GlobalAttackerDiceRollBonus;
                    break;
                case "global_defender_dice_roll_bonus":
                case "globaldefenderdicerollbonus":
                    Type = ModifierType.GlobalDefenderDiceRollBonus;
                    break;
                case "own_territory_dice_roll_bonus":
                case "ownterritorydicerollbonus":
                    Type = ModifierType.OwnTerritoryDiceRollBonus;
                    break;
                case "manpower_in_accepted_culture_provinces":
                case "manpowerinacceptedcultureprovinces":
                    Type = ModifierType.ManpowerInAcceptedCultureProvinces;
                    break;
                case "manpower_in_culture_group_provinces":
                case "manpowerinculturegroupprovinces":
                    Type = ModifierType.ManpowerInCultureGroupProvinces;
                    break;
                case "manpower_in_own_culture_provinces":
                case "manpowerinowncultureprovinces":
                    Type = ModifierType.ManpowerInOwnCultureProvinces;
                    break;
                case "may_build_supply_depot":
                case "maybuildsupplydepot":
                    Type = ModifierType.MayBuildSupplyDepot;
                    break;
                case "may_refill_garrison":
                case "mayrefillgarrison":
                    Type = ModifierType.MayRefillGarrison;
                    break;
                case "may_return_manpower_on_disband":
                case "mayreturnmanpowerondisband":
                    Type = ModifierType.MayReturnManpowerOnDisband;
                    break;
                case "attack_bonus_in_capital_terrain":
                case "attackbonusincapitalterrain":
                    Type = ModifierType.AttackBonusInCapitalTerrain;
                    break;
                case "can_bypass_forts":
                case "canbypassforts":
                    Type = ModifierType.CanBypassForts;
                    break;
                case "force_march_free":
                case "forcemarchfree":
                    Type = ModifierType.ForceMarchFree;
                    break;
                case "special_unit_forcelimit":
                case "specialunitforcelimit":
                    Type = ModifierType.SpecialUnitForcelimit;
                    break;
                case "allowed_marine_fraction":
                case "allowedmarinefraction":
                    Type = ModifierType.AllowedMarineFraction;
                    break;
                case "has_banners":
                case "hasbanners":
                    Type = ModifierType.HasBanners;
                    break;
                case "amount_of_banners":
                case "amountofbanners":
                    Type = ModifierType.AmountOfBanners;
                    break;
                case "amount_of_cawa":
                case "amountofcawa":
                    Type = ModifierType.AmountOfCawa;
                    break;
                case "cawa_cost_modifier":
                case "cawacostmodifier":
                    Type = ModifierType.CawaCostModifier;
                    break;
                case "has_carolean":
                case "hascarolean":
                    Type = ModifierType.HasCarolean;
                    break;
                case "amount_of_carolean":
                case "amountofcarolean":
                    Type = ModifierType.AmountOfCarolean;
                    break;
                case "can_recruit_hussars":
                case "canrecruithussars":
                    Type = ModifierType.CanRecruitHussars;
                    break;
                case "amount_of_hussars":
                case "amountofhussars":
                    Type = ModifierType.AmountOfHussars;
                    break;
                case "navy_tradition":
                case "navytradition":
                    Type = ModifierType.NavyTradition;
                    break;
                case "navy_tradition_decay":
                case "navytraditiondecay":
                    Type = ModifierType.NavyTraditionDecay;
                    break;
                case "naval_tradition_from_battle":
                case "navaltraditionfrombattle":
                    Type = ModifierType.NavalTraditionFromBattle;
                    break;
                case "naval_tradition_from_trade":
                case "navaltraditionfromtrade":
                    Type = ModifierType.NavalTraditionFromTrade;
                    break;
                case "heavy_ship_cost":
                case "heavyshipcost":
                    Type = ModifierType.HeavyShipCost;
                    break;
                case "heavy_ship_power":
                case "heavyshippower":
                    Type = ModifierType.HeavyShipPower;
                    break;
                case "light_ship_cost":
                case "lightshipcost":
                    Type = ModifierType.LightShipCost;
                    break;
                case "light_ship_power":
                case "lightshippower":
                    Type = ModifierType.LightShipPower;
                    break;
                case "galley_cost":
                case "galleycost":
                    Type = ModifierType.GalleyCost;
                    break;
                case "galley_power":
                case "galleypower":
                    Type = ModifierType.GalleyPower;
                    break;
                case "transport_cost":
                case "transportcost":
                    Type = ModifierType.TransportCost;
                    break;
                case "transport_power":
                case "transportpower":
                    Type = ModifierType.TransportPower;
                    break;
                case "global_ship_cost":
                case "globalshipcost":
                    Type = ModifierType.GlobalShipCost;
                    break;
                case "global_ship_recruit_speed":
                case "globalshiprecruitspeed":
                    Type = ModifierType.GlobalShipRecruitSpeed;
                    break;
                case "global_ship_repair":
                case "globalshiprepair":
                    Type = ModifierType.GlobalShipRepair;
                    break;
                case "naval_forcelimit":
                case "navalforcelimit":
                    Type = ModifierType.NavalForcelimit;
                    break;
                case "naval_forcelimit_modifier":
                case "navalforcelimitmodifier":
                    Type = ModifierType.NavalForcelimitModifier;
                    break;
                case "naval_maintenance_modifier":
                case "navalmaintenancemodifier":
                    Type = ModifierType.NavalMaintenanceModifier;
                    break;
                case "global_sailors":
                case "globalsailors":
                    Type = ModifierType.GlobalSailors;
                    break;
                case "global_sailors_modifier":
                case "globalsailorsmodifier":
                    Type = ModifierType.GlobalSailorsModifier;
                    break;
                case "sailor_maintenance_modifer":
                case "sailormaintenancemodifier":
                    Type = ModifierType.SailorMaintenanceModifer;
                    break;
                case "sailors_recovery_speed":
                case "sailorsrecoveryspeed":
                    Type = ModifierType.SailorsRecoverySpeed;
                    break;
                case "blockade_efficiency":
                case "blockadeefficiency":
                    Type = ModifierType.BlockadeEfficiency;
                    break;
                case "capture_ship_chance":
                case "captureshipchance":
                    Type = ModifierType.CaptureShipChance;
                    break;
                case "global_naval_engagement_modifier":
                case "globalnavalengagementmodifier":
                    Type = ModifierType.GlobalNavalEngagementModifier;
                    break;
                case "naval_attrition":
                case "navalattrition":
                    Type = ModifierType.NavalAttrition;
                    break;
                case "naval_morale":
                case "navalmorale":
                    Type = ModifierType.NavalMorale;
                    break;
                case "ship_durability":
                case "shipdurability":
                    Type = ModifierType.ShipDurability;
                    break;
                case "sunk_ship_morale_hit_recieved":
                case "sunkshipmoralehitrecieved":
                    Type = ModifierType.SunkShipMoraleHitRecieved;
                    break;
                case "recover_navy_morale_speed":
                case "recovernavymoralespeed":
                    Type = ModifierType.RecoverNavyMoraleSpeed;
                    break;
                case "prestige_from_naval":
                case "prestigefromnaval":
                    Type = ModifierType.PrestigeFromNaval;
                    break;
                case "leader_naval_fire":
                case "leadernavalfire":
                    Type = ModifierType.LeaderNavalFire;
                    break;
                case "leader_naval_manuever":
                case "leadernavalmanuever":
                    Type = ModifierType.LeaderNavalManuever;
                    break;
                case "leader_naval_shock":
                case "leadernavalshock":
                    Type = ModifierType.LeaderNavalShock;
                    break;
                case "own_coast_naval_combat_bonus":
                case "owncoastnavalcombatbonus":
                    Type = ModifierType.OwnCoastNavalCombatBonus;
                    break;
                case "admiral_cost":
                case "admiralcost":
                    Type = ModifierType.AdmiralCost;
                    break;
                case "global_naval_barrage_cost":
                case "globalnavalbarragecost":
                    Type = ModifierType.GlobalNavalBarrageCost;
                    break;
                case "flagship_cost":
                case "flagshipcost":
                    Type = ModifierType.FlagshipCost;
                    break;
                case "disengagement_chance":
                case "disengagementchance":
                    Type = ModifierType.DisengagementChance;
                    break;
                case "transport_attrition":
                case "transportattrition":
                    Type = ModifierType.TransportAttrition;
                    break;
                case "landing_penalty":
                case "landingpenalty":
                    Type = ModifierType.LandingPenalty;
                    break;
                case "regiment_disembark_speed":
                case "regimentdisembarkspeed":
                    Type = ModifierType.RegimentDisembarkSpeed;
                    break;
                case "may_perform_slave_raid":
                case "mayperformslaveraid":
                    Type = ModifierType.MayPerformSlaveRaid;
                    break;
                case "may_perform_slave_raid_on_same_religion":
                case "mayperformslaveraidonsamereligion":
                    Type = ModifierType.MayPerformSlaveRaidOnSameReligion;
                    break;
                case "sea_repair":
                case "searepair":
                    Type = ModifierType.SeaRepair;
                    break;
                case "movement_speed_in_fleet_modifier":
                case "movementspeedinfleetmodifier":
                    Type = ModifierType.MovementSpeedInFleetModifier;
                    break;
                case "admiral_skill_gain_modifier":
                case "admiralskillgainmodifier":
                    Type = ModifierType.AdmiralSkillGainModifier;
                    break;
                case "flagship_durability":
                case "flagshipdurability":
                    Type = ModifierType.FlagshipDurability;
                    break;
                case "flagship_morale":
                case "flagshipmorale":
                    Type = ModifierType.FlagshipMorale;
                    break;
                case "flagship_naval_engagement_modifier":
                case "flagshipnavalengagementmodifier":
                    Type = ModifierType.FlagshipNavalEngagementModifier;
                    break;
                case "movement_speed_onto_off_boat_modifier":
                case "movementspeedontooffboatmodifier":
                    Type = ModifierType.MovementSpeedOntoOffBoatModifier;
                    break;
                case "flagship_landing_penalty":
                case "flagshiplandingpenalty":
                    Type = ModifierType.FlagshipLandingPenalty;
                    break;
                case "number_of_cannons_flagship_modifier":
                case "numberofcannonsflagshipmodifier":
                    Type = ModifierType.NumberOfCannonsFlagshipModifier;
                    break;
                case "number_of_cannons_flagship":
                case "numberofcannonsflagship":
                    Type = ModifierType.NumberOfCannonsFlagship;
                    break;
                case "naval_maintenance_flagship_modifier":
                case "navalmaintenanceflagshipmodifier":
                    Type = ModifierType.NavalMaintenanceFlagshipModifier;
                    break;
                case "trade_power_in_fleet_modifier":
                case "tradepowerinfleetmodifier":
                    Type = ModifierType.TradePowerInFleetModifier;
                    break;
                case "morale_in_fleet_modifier":
                case "moraleinfleetmodifier":
                    Type = ModifierType.MoraleInFleetModifier;
                    break;
                case "blockade_impact_on_siege_in_fleet_modifier":
                case "blockadeimpactonsiegeinfleetmodifier":
                    Type = ModifierType.BlockadeImpactOnSiegeInFleetModifier;
                    break;
                case "exploration_mission_range_in_fleet_modifier":
                case "explorationmissionrangeinfleetmodifier":
                    Type = ModifierType.ExplorationMissionRangeInFleetModifier;
                    break;
                case "barrage_cost_in_fleet_modifier":
                case "barragecostinfleetmodifier":
                    Type = ModifierType.BarrageCostInFleetModifier;
                    break;
                case "naval_attrition_in_fleet_modifier":
                case "navalattritioninfleetmodifier":
                    Type = ModifierType.NavalAttritionInFleetModifier;
                    break;
                case "privateering_efficiency_in_fleet_modifier":
                case "privateeringefficiencyinfleetmodifier":
                    Type = ModifierType.PrivateeringEfficiencyInFleetModifier;
                    break;
                case "prestige_from_battles_in_fleet_modifier":
                case "prestigefrombattlesinfleetmodifier":
                    Type = ModifierType.PrestigeFromBattlesInFleetModifier;
                    break;
                case "naval_tradition_in_fleet_modifier":
                case "navaltraditioninfleetmodifier":
                    Type = ModifierType.NavalTraditionInFleetModifier;
                    break;
                case "cannons_for_hunting_pirates_in_fleet":
                case "cannonsforhuntingpiratesinfleet":
                    Type = ModifierType.CannonsForHuntingPiratesInFleet;
                    break;
                case "diplomats":
                    Type = ModifierType.Diplomats;
                    break;
                case "diplomatic_reputation":
                case "diplomaticreputation":
                    Type = ModifierType.DiplomaticReputation;
                    break;
                case "diplomatic_upkeep":
                case "diplomaticupkeep":
                    Type = ModifierType.DiplomaticUpkeep;
                    break;
                case "envoy_travel_time":
                case "envoytraveltime":
                    Type = ModifierType.EnvoyTravelTime;
                    break;
                case "fabricate_claims_cost":
                case "fabricateclaimscost":
                    Type = ModifierType.FabricateClaimsCost;
                    break;
                case "years_to_integrate_personal_union":
                case "yearstointegratepersonalunion":
                    Type = ModifierType.YearsToIntegratePersonalUnion;
                    break;
                case "improve_relation_modifier":
                case "improverelationmodifier":
                    Type = ModifierType.ImproveRelationModifier;
                    break;
                case "vassal_forcelimit_bonus":
                case "vassalforcelimitbonus":
                    Type = ModifierType.VassalForcelimitBonus;
                    break;
                case "vassal_income":
                case "vassalincome":
                    Type = ModifierType.VassalIncome;
                    break;
                case "ae_impact":
                case "aeimpact":
                    Type = ModifierType.AeImpact;
                    break;
                case "claim_duration":
                case "claimduration":
                    Type = ModifierType.ClaimDuration;
                    break;
                case "diplomatic_annexation_cost":
                case "diplomaticannexationcost":
                    Type = ModifierType.DiplomaticAnnexationCost;
                    break;
                case "province_warscore_cost":
                case "provincewarscorecost":
                    Type = ModifierType.ProvinceWarscoreCost;
                    break;
                case "unjustified_demands":
                case "unjustifieddemands":
                    Type = ModifierType.UnjustifiedDemands;
                    break;
                case "rival_change_cost":
                case "rivalchangecost":
                    Type = ModifierType.RivalChangeCost;
                    break;
                case "justify_trade_conflict_cost":
                case "justifytradeconflictcost":
                    Type = ModifierType.JustifyTradeConflictCost;
                    break;
                case "stability_cost_to_declare_war":
                case "stabilitycosttodeclarewar":
                    Type = ModifierType.StabilityCostToDeclareWar;
                    break;
                case "accept_vassalization_reasons":
                case "acceptvassalizationreasons":
                    Type = ModifierType.AcceptVassalizationReasons;
                    break;
                case "transfer_trade_power_reasons":
                case "transfertradepowerreasons":
                    Type = ModifierType.TransferTradePowerReasons;
                    break;
                case "monthly_federation_favor_growth":
                case "monthlyfederationfavorgrowth":
                    Type = ModifierType.MonthlyFederationFavorGrowth;
                    break;
                case "monthly_favor_modifier":
                case "monthlyfavormodifier":
                    Type = ModifierType.MonthlyFavorModifier;
                    break;
                case "can_chain_claim":
                case "canchainclaim":
                    Type = ModifierType.CanChainClaim;
                    break;
                case "cb_on_overseas":
                case "cbonoverseas":
                    Type = ModifierType.CbOnOverseas;
                    break;
                case "cb_on_primitives":
                case "cbonprimitives":
                    Type = ModifierType.CbOnPrimitives;
                    break;
                case "idea_claim_colonies":
                case "ideaclaimcolonies":
                    Type = ModifierType.IdeaClaimColonies;
                    break;
                case "cb_on_government_enemies":
                case "cbongovernmentenemies":
                    Type = ModifierType.CbOnGovernmentEnemies;
                    break;
                case "cb_on_religious_enemies":
                case "cbonreligiousenemies":
                    Type = ModifierType.CbOnReligiousEnemies;
                    break;
                case "reduced_stab_impacts":
                case "reducedstabimpacts":
                    Type = ModifierType.ReducedStabImpacts;
                    break;
                case "can_fabricate_for_vassals":
                case "canfabricateforvassals":
                    Type = ModifierType.CanFabricateForVassals;
                    break;
                case "global_tax_income":
                case "globaltaxincome":
                    Type = ModifierType.GlobalTaxIncome;
                    break;
                case "global_tax_modifier":
                case "globaltaxmodifier":
                    Type = ModifierType.GlobalTaxModifier;
                    break;
                case "production_efficiency":
                case "productionefficiency":
                    Type = ModifierType.ProductionEfficiency;
                    break;
                case "state_maintenance_modifier":
                case "statemaintenancemodifier":
                    Type = ModifierType.StateMaintenanceModifier;
                    break;
                case "inflation_action_cost":
                case "inflationactioncost":
                    Type = ModifierType.InflationActionCost;
                    break;
                case "inflation_reduction":
                case "inflationreduction":
                    Type = ModifierType.InflationReduction;
                    break;
                case "monthly_gold_inflation_modifier":
                case "monthlygoldinflationmodifier":
                    Type = ModifierType.MonthlyGoldInflationModifier;
                    break;
                case "gold_depletion_chance_modifier":
                case "golddepletionchancemodifier":
                    Type = ModifierType.GoldDepletionChanceModifier;
                    break;
                case "interest":
                    Type = ModifierType.Interest;
                    break;
                case "can_not_build_buildings":
                case "cannotbuildbuildings":
                    Type = ModifierType.CanNotBuildBuildings;
                    break;
                case "development_cost":
                case "developmentcost":
                    Type = ModifierType.DevelopmentCost;
                    break;
                case "development_cost_modifier":
                case "developmentcostmodifier":
                    Type = ModifierType.DevelopmentCostModifier;
                    break;
                case "tribal_development_growth":
                case "tribaldevelopmentgrowth":
                    Type = ModifierType.TribalDevelopmentGrowth;
                    break;
                case "add_tribal_land_cost":
                case "addtriballandcost":
                    Type = ModifierType.AddTribalLandCost;
                    break;
                case "settle_cost":
                case "settlecost":
                    Type = ModifierType.SettleCost;
                    break;
                case "global_allowed_num_of_buildings":
                case "globalallowednumofbuildings":
                    Type = ModifierType.GlobalAllowedNumOfBuildings;
                    break;
                case "build_cost":
                case "buildcost":
                    Type = ModifierType.BuildCost;
                    break;
                case "build_time":
                case "buildtime":
                    Type = ModifierType.BuildTime;
                    break;
                case "great_project_upgrade_cost":
                case "greatprojectupgradecost":
                    Type = ModifierType.GreatProjectUpgradeCost;
                    break;
                case "global_monthly_devastation":
                case "globalmonthlydevastation":
                    Type = ModifierType.GlobalMonthlyDevastation;
                    break;
                case "global_prosperity_growth":
                case "globalprosperitygrowth":
                    Type = ModifierType.GlobalProsperityGrowth;
                    break;
                case "administrative_efficiency":
                case "administrativeefficiency":
                    Type = ModifierType.AdministrativeEfficiency;
                    break;
                case "core_creation":
                case "corecreation":
                    Type = ModifierType.CoreCreation;
                    break;
                case "core_decay_on_your_own":
                case "coredecayonyourown":
                    Type = ModifierType.CoreDecayOnYourOwn;
                    break;
                case "enemy_core_creation":
                case "enemycorecreation":
                    Type = ModifierType.EnemyCoreCreation;
                    break;
                case "ignore_coring_distance":
                case "ignorecoringdistance":
                    Type = ModifierType.IgnoreCoringDistance;
                    break;
                case "adm_cost_modifier":
                case "admcostmodifier":
                    Type = ModifierType.AdmCostModifier;
                    break;
                case "dip_cost_modifier":
                case "dipcostmodifier":
                    Type = ModifierType.DipCostModifier;
                    break;
                case "mil_cost_modifier":
                case "milcostmodifier":
                    Type = ModifierType.MilCostModifier;
                    break;
                case "technology_cost":
                case "technologycost":
                    Type = ModifierType.TechnologyCost;
                    break;
                case "idea_cost":
                case "ideacost":
                    Type = ModifierType.IdeaCost;
                    break;
                case "embracement_cost":
                case "embracementcost":
                    Type = ModifierType.EmbracementCost;
                    break;
                case "global_institution_spread":
                case "globalinstitutionspread":
                    Type = ModifierType.GlobalInstitutionSpread;
                    break;
                case "institution_spread_from_true_faith":
                case "institutionspreadfromtruefaith":
                    Type = ModifierType.InstitutionSpreadFromTrueFaith;
                    break;
                case "native_advancement_cost":
                case "nativeadvancementcost":
                    Type = ModifierType.NativeAdvancementCost;
                    break;
                case "all_power_cost":
                case "allpowercost":
                    Type = ModifierType.AllPowerCost;
                    break;
                case "innovativeness_gain":
                case "innovativenessgain":
                    Type = ModifierType.InnovativenessGain;
                    break;
                case "free_adm_policy":
                case "freeadmpolicy":
                    Type = ModifierType.FreeAdmPolicy;
                    break;
                case "free_dip_policy":
                case "freedippolicy":
                    Type = ModifierType.FreeDipPolicy;
                    break;
                case "free_mil_policy":
                case "freemilpolicy":
                    Type = ModifierType.FreeMilPolicy;
                    break;
                case "possible_adm_policy":
                case "possibleadmpolicy":
                    Type = ModifierType.PossibleAdmPolicy;
                    break;
                case "possible_dip_policy":
                case "possibledippolicy":
                    Type = ModifierType.PossibleDipPolicy;
                    break;
                case "possible_mil_policy":
                case "possiblemilpolicy":
                    Type = ModifierType.PossibleMilPolicy;
                    break;
                case "possible_policy":
                case "possiblepolicy":
                    Type = ModifierType.PossiblePolicy;
                    break;
                case "free_policy":
                case "freepolicy":
                    Type = ModifierType.FreePolicy;
                    break;
                case "country_admin_power":
                case "countryadminpower":
                    Type = ModifierType.CountryAdminPower;
                    break;
                case "country_diplomatic_power":
                case "countrydiplomaticpower":
                    Type = ModifierType.CountryDiplomaticPower;
                    break;
                case "country_military_power":
                case "countrymilitarypower":
                    Type = ModifierType.CountryMilitaryPower;
                    break;
                case "prestige":
                    Type = ModifierType.Prestige;
                    break;
                case "prestige_decay":
                case "prestigedecay":
                    Type = ModifierType.PrestigeDecay;
                    break;
                case "monthly_splendor":
                case "monthlysplendor":
                    Type = ModifierType.MonthlySplendor;
                    break;
                case "yearly_corruption":
                case "yearlycorruption":
                    Type = ModifierType.YearlyCorruption;
                    break;
                case "advisor_cost":
                case "advisorcost":
                    Type = ModifierType.AdvisorCost;
                    break;
                case "advisor_pool":
                case "advisorpool":
                    Type = ModifierType.AdvisorPool;
                    break;
                case "female_advisor_chance":
                case "femaleadvisorchance":
                    Type = ModifierType.FemaleAdvisorChance;
                    break;
                case "heir_chance":
                case "heirchance":
                    Type = ModifierType.HeirChance;
                    break;
                case "monthly_heir_claim_increase":
                case "monthlyheirclaimincrease":
                    Type = ModifierType.MonthlyHeirClaimIncrease;
                    break;
                case "monthly_heir_claim_increase_modifier":
                case "monthlyheirclaimincreasemodifier":
                    Type = ModifierType.MonthlyHeirClaimIncreaseModifier;
                    break;
                case "block_introduce_heir":
                case "blockintroduceheir":
                    Type = ModifierType.BlockIntroduceHeir;
                    break;
                case "monarch_admin_power":
                case "monarchadminpower":
                    Type = ModifierType.MonarchAdminPower;
                    break;
                case "monarch_diplomatic_power":
                case "monarchdiplomaticpower":
                    Type = ModifierType.MonarchDiplomaticPower;
                    break;
                case "monarch_military_power":
                case "monarchmilitarypower":
                    Type = ModifierType.MonarchMilitaryPower;
                    break;
                case "adm_advisor_cost":
                case "admadvisorcost":
                    Type = ModifierType.AdmAdvisorCost;
                    break;
                case "dip_advisor_cost":
                case "dipadvisorcost":
                    Type = ModifierType.DipAdvisorCost;
                    break;
                case "mil_advisor_cost":
                case "miladvisorcost":
                    Type = ModifierType.MilAdvisorCost;
                    break;
                case "monthly_support_heir_gain":
                case "monthlysupportheirgain":
                    Type = ModifierType.MonthlySupportHeirGain;
                    break;
                case "power_projection_from_insults":
                case "powerprojectionfrominsults":
                    Type = ModifierType.PowerProjectionFromInsults;
                    break;
                case "monarch_lifespan":
                case "monarchlifespan":
                    Type = ModifierType.MonarchLifespan;
                    break;
                case "local_heir_adm":
                case "localheiradm":
                    Type = ModifierType.LocalHeirAdm;
                    break;
                case "local_heir_dip":
                case "localheirdip":
                    Type = ModifierType.LocalHeirDip;
                    break;
                case "local_heir_mil":
                case "localheirmil":
                    Type = ModifierType.LocalHeirMil;
                    break;
                case "national_focus_years":
                case "nationalfocusyears":
                    Type = ModifierType.NationalFocusYears;
                    break;
                case "yearly_absolutism":
                case "yearlyabsolutism":
                    Type = ModifierType.YearlyAbsolutism;
                    break;
                case "max_absolutism":
                case "maxabsolutism":
                    Type = ModifierType.MaxAbsolutism;
                    break;
                case "legitimacy":
                    Type = ModifierType.Legitimacy;
                    break;
                case "republican_tradition":
                case "republicantradition":
                    Type = ModifierType.RepublicanTradition;
                    break;
                case "devotion":
                    Type = ModifierType.Devotion;
                    break;
                case "horde_unity":
                case "hordeunity":
                    Type = ModifierType.HordeUnity;
                    break;
                case "meritocracy":
                    Type = ModifierType.Meritocracy;
                    break;
                case "monthly_militarized_society":
                case "monthlymilitarizedsociety":
                    Type = ModifierType.MonthlyMilitarizedSociety;
                    break;
                case "yearly_tribal_allegiance":
                case "yearlytribalallegiance":
                    Type = ModifierType.YearlyTribalAllegiance;
                    break;
                case "imperial_mandate":
                case "imperialmandate":
                    Type = ModifierType.ImperialMandate;
                    break;
                case "election_cycle":
                case "electioncycle":
                    Type = ModifierType.ElectionCycle;
                    break;
                case "candidate_random_bonus":
                case "candidaterandombonus":
                    Type = ModifierType.CandidateRandomBonus;
                    break;
                case "reelection_cost":
                case "reelectioncost":
                    Type = ModifierType.ReelectionCost;
                    break;
                case "governing_capacity":
                case "governingcapacity":
                    Type = ModifierType.GoverningCapacity;
                    break;
                case "governing_capacity_modifier":
                case "governingcapacitymodifier":
                    Type = ModifierType.GoverningCapacityModifier;
                    break;
                case "governing_cost":
                case "governingcost":
                    Type = ModifierType.GoverningCost;
                    break;
                case "state_governing_cost":
                case "stategoverningcost":
                    Type = ModifierType.StateGoverningCost;
                    break;
                case "trade_company_governing_cost":
                case "tradecompanygoverningcost":
                    Type = ModifierType.TradeCompanyGoverningCost;
                    break;
                case "state_governing_cost_increase":
                case "stategoverningcostincrease":
                    Type = ModifierType.StateGoverningCostIncrease;
                    break;
                case "expand_administration_cost":
                case "expandadministrationcost":
                    Type = ModifierType.ExpandAdministrationCost;
                    break;
                case "yearly_revolutionary_zeal":
                case "yearlyrevolutionaryzeal":
                    Type = ModifierType.YearlyRevolutionaryZeal;
                    break;
                case "max_revolutionary_zeal":
                case "maxrevolutionaryzeal":
                    Type = ModifierType.MaxRevolutionaryZeal;
                    break;
                case "reform_progress_growth":
                case "reformprogressgrowth":
                    Type = ModifierType.ReformProgressGrowth;
                    break;
                case "monthly_reform_progress":
                case "monthlyreformprogress":
                    Type = ModifierType.MonthlyReformProgress;
                    break;
                case "monthly_reform_progress_modifier":
                case "monthlyreformprogressmodifier":
                    Type = ModifierType.MonthlyReformProgressModifier;
                    break;
                case "move_capital_cost_modifier":
                case "movecapitalcostmodifier":
                    Type = ModifierType.MoveCapitalCostModifier;
                    break;
                case "all_estate_influence_modifier":
                case "allestateinfluencemodifier":
                    Type = ModifierType.AllEstateInfluenceModifier;
                    break;
                case "all_estate_loyalty_equilibrium":
                case "allestateloyaltyequilibrium":
                    Type = ModifierType.AllEstateLoyaltyEquilibrium;
                    break;
                case "allow_free_estate_privilege_revocation":
                case "allowfreeestateprivilegerevocation":
                    Type = ModifierType.AllowFreeEstatePrivilegeRevocation;
                    break;
                case "imperial_authority":
                case "imperialauthority":
                    Type = ModifierType.ImperialAuthority;
                    break;
                case "imperial_authority_value":
                case "imperialauthorityvalue":
                    Type = ModifierType.ImperialAuthorityValue;
                    break;
                case "free_city_imperial_authority":
                case "freecityimperialauthority":
                    Type = ModifierType.FreeCityImperialAuthority;
                    break;
                case "reasons_to_elect":
                case "reasonstoelect":
                    Type = ModifierType.ReasonsToElect;
                    break;
                case "imperial_mercenary_cost":
                case "imperialmercenarycost":
                    Type = ModifierType.ImperialMercenaryCost;
                    break;
                case "max_free_cities":
                case "maxfreecities":
                    Type = ModifierType.MaxFreeCities;
                    break;
                case "max_electors":
                case "maxelectors":
                    Type = ModifierType.MaxElectors;
                    break;
                case "legitimate_subject_elector":
                case "legitimatesubjectelector":
                    Type = ModifierType.LegitimateSubjectElector;
                    break;
                case "manpower_against_imperial_enemies":
                case "manpoweragainstimperialenemies":
                    Type = ModifierType.ManpowerAgainstImperialEnemies;
                    break;
                case "imperial_reform_catholic_approval":
                case "imperialreformcatholicapproval":
                    Type = ModifierType.ImperialReformCatholicApproval;
                    break;
                case "culture_conversion_cost":
                case "cultureconversioncost":
                    Type = ModifierType.CultureConversionCost;
                    break;
                case "num_accepted_cultures":
                case "numacceptedcultures":
                    Type = ModifierType.NumAcceptedCultures;
                    break;
                case "same_culture_advisor_cost":
                case "samecultureadvisorcost":
                    Type = ModifierType.SameCultureAdvisorCost;
                    break;
                case "promote_culture_cost":
                case "promoteculturecost":
                    Type = ModifierType.PromoteCultureCost;
                    break;
                case "relation_with_same_culture":
                case "relationwithsameculture":
                    Type = ModifierType.RelationWithSameCulture;
                    break;
                case "relation_with_same_culture_group":
                case "relationwithsameculturegroup":
                    Type = ModifierType.RelationWithSameCultureGroup;
                    break;
                case "relation_with_accepted_culture":
                case "relationwithacceptedculture":
                    Type = ModifierType.RelationWithAcceptedCulture;
                    break;
                case "relation_with_other_culture":
                case "relationwithotherculture":
                    Type = ModifierType.RelationWithOtherCulture;
                    break;
                case "can_not_declare_war":
                case "cannotdeclarewar":
                    Type = ModifierType.CanNotDeclareWar;
                    break;
                case "global_unrest":
                case "globalunrest":
                    Type = ModifierType.GlobalUnrest;
                    break;
                case "stability_cost_modifier":
                case "stabilitycostmodifier":
                    Type = ModifierType.StabilityCostModifier;
                    break;
                case "global_autonomy":
                case "globalautonomy":
                    Type = ModifierType.GlobalAutonomy;
                    break;
                case "min_autonomy":
                case "minautonomy":
                    Type = ModifierType.MinAutonomy;
                    break;
                case "autonomy_change_time":
                case "autonomychangetime":
                    Type = ModifierType.AutonomyChangeTime;
                    break;
                case "harsh_treatment_cost":
                case "harshtreatmentcost":
                    Type = ModifierType.HarshTreatmentCost;
                    break;
                case "global_rebel_suppression_efficiency":
                case "globalrebelsuppressionefficiency":
                    Type = ModifierType.GlobalRebelSuppressionEfficiency;
                    break;
                case "years_of_nationalism":
                case "yearsofnationalism":
                    Type = ModifierType.YearsOfNationalism;
                    break;
                case "min_autonomy_in_territories":
                case "minautonomyinterritories":
                    Type = ModifierType.MinAutonomyInTerritories;
                    break;
                case "unrest_catholic_provinces":
                case "unrestcatholicprovinces":
                    Type = ModifierType.UnrestCatholicProvinces;
                    break;
                case "no_stability_loss_on_monarch_death":
                case "nostabilitylossonmonarchdeath":
                    Type = ModifierType.NoStabilityLossOnMonarchDeath;
                    break;
                case "can_transfer_vassal_wargoal":
                case "cantransfervassalwargoal":
                    Type = ModifierType.CanTransferVassalWargoal;
                    break;
                case "liberty_desire":
                case "libertydesire":
                    Type = ModifierType.LibertyDesire;
                    break;
                case "liberty_desire_from_subject_development":
                case "libertydesirefromsubjectdevelopment":
                    Type = ModifierType.LibertyDesireFromSubjectDevelopment;
                    break;
                case "reduced_liberty_desire":
                case "reducedlibertydesire":
                    Type = ModifierType.ReducedLibertyDesire;
                    break;
                case "reduced_liberty_desire_on_same_continent":
                case "reducedlibertydesireonsamecontinent":
                    Type = ModifierType.ReducedLibertyDesireOnSameContinent;
                    break;
                case "allow_client_states":
                case "allowclientstates":
                    Type = ModifierType.AllowClientStates;
                    break;
                case "colonial_type_change_cost_modifier":
                case "colonialtypechangecostmodifier":
                    Type = ModifierType.ColonialTypeChangeCostModifier;
                    break;
                case "colonial_subject_type_upgrade_cost_modifier":
                case "colonialsubjecttypeupgradecostmodifier":
                    Type = ModifierType.ColonialSubjectTypeUpgradeCostModifier;
                    break;
                case "spy_offence":
                case "spyoffence":
                    Type = ModifierType.SpyOffence;
                    break;
                case "global_spy_defence":
                case "globalspydefence":
                    Type = ModifierType.GlobalSpyDefence;
                    break;
                case "discovered_relations_impact":
                case "discoveredrelationsimpact":
                    Type = ModifierType.DiscoveredRelationsImpact;
                    break;
                case "rebel_support_efficiency":
                case "rebelsupportefficiency":
                    Type = ModifierType.RebelSupportEfficiency;
                    break;
                case "global_missionary_strength":
                case "globalmissionarystrength":
                    Type = ModifierType.GlobalMissionaryStrength;
                    break;
                case "global_heretic_missionary_strength":
                case "globalhereticmissionarystrength":
                    Type = ModifierType.GlobalHereticMissionaryStrength;
                    break;
                case "global_heathen_missionary_strength":
                case "globalheathenmissionarystrength":
                    Type = ModifierType.GlobalHeathenMissionaryStrength;
                    break;
                case "can_not_build_missionaries":
                case "cannotbuildmissionaries":
                    Type = ModifierType.CanNotBuildMissionaries;
                    break;
                case "missionaries":
                    Type = ModifierType.Missionaries;
                    break;
                case "missionary_maintenance_cost":
                case "missionarymaintenancecost":
                    Type = ModifierType.MissionaryMaintenanceCost;
                    break;
                case "religious_unity":
                case "religiousunity":
                    Type = ModifierType.ReligiousUnity;
                    break;
                case "tolerance_own":
                case "toleranceown":
                    Type = ModifierType.ToleranceOwn;
                    break;
                case "tolerance_heretic":
                case "toleranceheretic":
                    Type = ModifierType.ToleranceHeretic;
                    break;
                case "tolerance_heathen":
                case "toleranceheathen":
                    Type = ModifierType.ToleranceHeathen;
                    break;
                case "tolerance_of_heretics_capacity":
                case "toleranceofhereticscapacity":
                    Type = ModifierType.ToleranceOfHereticsCapacity;
                    break;
                case "tolerance_of_heathens_capacity":
                case "toleranceofheathenscapacity":
                    Type = ModifierType.ToleranceOfHeathensCapacity;
                    break;
                case "papal_influence":
                case "papalinfluence":
                    Type = ModifierType.PapalInfluence;
                    break;
                case "papal_influence_from_cardinals":
                case "papalinfluencefromcardinals":
                    Type = ModifierType.PapalInfluenceFromCardinals;
                    break;
                case "appoint_cardinal_cost":
                case "appointcardinalcost":
                    Type = ModifierType.AppointCardinalCost;
                    break;
                case "curia_treasury_contribution":
                case "curiatreasurycontribution":
                    Type = ModifierType.CuriaTreasuryContribution;
                    break;
                case "curia_powers_cost":
                case "curiapowerscost":
                    Type = ModifierType.CuriaPowersCost;
                    break;
                case "monthly_church_power":
                case "monthlychurchpower":
                    Type = ModifierType.MonthlyChurchPower;
                    break;
                case "church_power_modifier":
                case "churchpowermodifier":
                    Type = ModifierType.ChurchPowerModifier;
                    break;
                case "monthly_fervor_increase":
                case "monthlyfervorincrease":
                    Type = ModifierType.MonthlyFervorIncrease;
                    break;
                case "yearly_patriarch_authority":
                case "yearlypatriarchauthority":
                    Type = ModifierType.YearlyPatriarchAuthority;
                    break;
                case "monthly_piety":
                case "monthlypiety":
                    Type = ModifierType.MonthlyPiety;
                    break;
                case "monthly_piety_accelerator":
                case "monthlypietyaccelerator":
                    Type = ModifierType.MonthlyPietyAccelerator;
                    break;
                case "harmonization_speed":
                case "harmonizationspeed":
                    Type = ModifierType.HarmonizationSpeed;
                    break;
                case "yearly_harmony":
                case "yearlyharmony":
                    Type = ModifierType.YearlyHarmony;
                    break;
                case "monthly_karma":
                case "monthlykarma":
                    Type = ModifierType.MonthlyKarma;
                    break;
                case "monthly_karma_accelerator":
                case "monthlykarmaaccelerator":
                    Type = ModifierType.MonthlyKarmaAccelerator;
                    break;
                case "yearly_karma_decay":
                case "yearlykarmadecay":
                    Type = ModifierType.YearlyKarmaDecay;
                    break;
                case "yearly_doom_reduction":
                case "yearlydoomreduction":
                    Type = ModifierType.YearlyDoomReduction;
                    break;
                case "yearly_authority":
                case "yearlyauthority":
                    Type = ModifierType.YearlyAuthority;
                    break;
                case "enforce_religion_cost":
                case "enforcereligioncost":
                    Type = ModifierType.EnforceReligionCost;
                    break;
                case "prestige_per_development_from_conversion":
                case "prestigeperdevelopmentfromconversion":
                    Type = ModifierType.PrestigePerDevelopmentFromConversion;
                    break;
                case "warscore_cost_vs_other_religion":
                case "warscorecostvsotherreligion":
                    Type = ModifierType.WarscoreCostVsOtherReligion;
                    break;
                case "establish_order_cost":
                case "establishordercost":
                    Type = ModifierType.EstablishOrderCost;
                    break;
                case "global_religious_conversion_resistance":
                case "globalreligiousconversionresistance":
                    Type = ModifierType.GlobalReligiousConversionResistance;
                    break;
                case "relation_with_same_religion":
                case "relationwithsamereligion":
                    Type = ModifierType.RelationWithSameReligion;
                    break;
                case "relation_with_heretics":
                case "relationwithheretics":
                    Type = ModifierType.RelationWithHeretics;
                    break;
                case "relation_with_heathens":
                case "relationwithheathens":
                    Type = ModifierType.RelationWithHeathens;
                    break;
                case "no_religion_penalty":
                case "noreligionpenalty":
                    Type = ModifierType.NoReligionPenalty;
                    break;
                case "extra_manpower_at_religious_war":
                case "extramanpoweratreligiouswar":
                    Type = ModifierType.ExtraManpowerAtReligiousWar;
                    break;
                case "can_not_build_colonies":
                case "cannotbuildcolonies":
                    Type = ModifierType.CanNotBuildColonies;
                    break;
                case "colonists":
                    Type = ModifierType.Colonists;
                    break;
                case "colonist_placement_chance":
                case "colonistplacementchance":
                    Type = ModifierType.ColonistPlacementChance;
                    break;
                case "global_colonial_growth":
                case "globalcolonialgrowth":
                    Type = ModifierType.GlobalColonialGrowth;
                    break;
                case "range":
                    Type = ModifierType.Range;
                    break;
                case "native_uprising_chance":
                case "nativeuprisingchance":
                    Type = ModifierType.NativeUprisingChance;
                    break;
                case "native_assimilation":
                case "nativeassimilation":
                    Type = ModifierType.NativeAssimilation;
                    break;
                case "migration_cost":
                case "migrationcost":
                    Type = ModifierType.MigrationCost;
                    break;
                case "global_tariffs":
                case "globaltariffs":
                    Type = ModifierType.GlobalTariffs;
                    break;
                case "treasure_fleet_income":
                case "treasurefleetincome":
                    Type = ModifierType.TreasureFleetIncome;
                    break;
                case "expel_minorities_cost":
                case "expelminoritiescost":
                    Type = ModifierType.ExpelMinoritiesCost;
                    break;
                case "may_explore":
                case "mayexplore":
                    Type = ModifierType.MayExplore;
                    break;
                case "auto_explore_adjacent_to_colony":
                case "autoexploreadjacenttocolony":
                    Type = ModifierType.AutoExploreAdjacentToColony;
                    break;
                case "may_establish_frontier":
                case "mayestablishfrontier":
                    Type = ModifierType.MayEstablishFrontier;
                    break;
                case "can_colony_boost_development":
                case "cancolonyboostdevelopment":
                    Type = ModifierType.CanColonyBoostDevelopment;
                    break;
                case "free_maintenance_on_expl_conq":
                case "freemaintenanceonexplconq":
                    Type = ModifierType.FreeMaintenanceOnExplConq;
                    break;
                case "caravan_power":
                case "caravanpower":
                    Type = ModifierType.CaravanPower;
                    break;
                case "can_not_send_merchants":
                case "cannotsendmerchants":
                    Type = ModifierType.CanNotSendMerchants;
                    break;
                case "merchants":
                    Type = ModifierType.Merchants;
                    break;
                case "placed_merchant_power":
                case "placedmerchantpower":
                    Type = ModifierType.PlacedMerchantPower;
                    break;
                case "global_trade_power":
                case "globaltradepower":
                    Type = ModifierType.GlobalTradePower;
                    break;
                case "global_foreign_trade_power":
                case "globalforeigntradepower":
                    Type = ModifierType.GlobalForeignTradePower;
                    break;
                case "global_own_trade_power":
                case "globalowntradepower":
                    Type = ModifierType.GlobalOwnTradePower;
                    break;
                case "global_prov_trade_power_modifier":
                case "globalprovtradepowermodifier":
                    Type = ModifierType.GlobalProvTradePowerModifier;
                    break;
                case "global_trade_goods_size_modifier":
                case "globaltradegoodssizemodifier":
                    Type = ModifierType.GlobalTradeGoodsSizeModifier;
                    break;
                case "global_trade_goods_size":
                case "globaltradegoodssize":
                    Type = ModifierType.GlobalTradeGoodsSize;
                    break;
                case "trade_efficiency":
                case "tradeefficiency":
                    Type = ModifierType.TradeEfficiency;
                    break;
                case "trade_range_modifier":
                case "traderangemodifier":
                    Type = ModifierType.TradeRangeModifier;
                    break;
                case "trade_steering":
                case "tradesteering":
                    Type = ModifierType.TradeSteering;
                    break;
                case "global_ship_trade_power":
                case "globalshiptradepower":
                    Type = ModifierType.GlobalShipTradePower;
                    break;
                case "privateer_efficiency":
                case "privateerefficiency":
                    Type = ModifierType.PrivateerEfficiency;
                    break;
                case "embargo_efficiency":
                case "embargoefficiency":
                    Type = ModifierType.EmbargoEfficiency;
                    break;
                case "ship_power_propagation":
                case "shippowerpropagation":
                    Type = ModifierType.ShipPowerPropagation;
                    break;
                case "center_of_trade_upgrade_cost":
                case "centeroftradeupgradecost":
                    Type = ModifierType.CenterOfTradeUpgradeCost;
                    break;
                case "trade_company_investment_cost":
                case "tradecompanyinvestmentcost":
                    Type = ModifierType.TradeCompanyInvestmentCost;
                    break;
                case "mercantilism_cost":
                case "mercantilismcost":
                    Type = ModifierType.MercantilismCost;
                    break;
                case "max_attrition":
                case "maxattrition":
                    Type = ModifierType.MaxAttrition;
                    break;
                case "attrition":
                    Type = ModifierType.Attrition;
                    break;
                case "local_hostile_attrition":
                case "localhostileattrition":
                    Type = ModifierType.LocalHostileAttrition;
                    break;
                case "local_fort_maintenance_modifier":
                case "localfortmaintenancemodifier":
                    Type = ModifierType.LocalFortMaintenanceModifier;
                    break;
                case "local_garrison_size":
                case "localgarrisonsize":
                    Type = ModifierType.LocalGarrisonSize;
                    break;
                case "local_attacker_dice_roll_bonus":
                case "localattackerdicerollbonus":
                    Type = ModifierType.LocalAttackerDiceRollBonus;
                    break;
                case "local_defender_dice_roll_bonus":
                case "localdefenderdicerollbonus":
                    Type = ModifierType.LocalDefenderDiceRollBonus;
                    break;
                case "fort_level":
                case "fortlevel":
                    Type = ModifierType.FortLevel;
                    break;
                case "garrison_growth":
                case "garrisongrowth":
                    Type = ModifierType.GarrisonGrowth;
                    break;
                case "local_defensiveness":
                case "localdefensiveness":
                    Type = ModifierType.LocalDefensiveness;
                    break;
                case "local_friendly_movement_speed":
                case "localfriendlymovementspeed":
                    Type = ModifierType.LocalFriendlyMovementSpeed;
                    break;
                case "local_hostile_movement_speed":
                case "localhostilemovementspeed":
                    Type = ModifierType.LocalHostileMovementSpeed;
                    break;
                case "local_manpower":
                case "localmanpower":
                    Type = ModifierType.LocalManpower;
                    break;
                case "local_manpower_modifier":
                case "localmanpowermodifier":
                    Type = ModifierType.LocalManpowerModifier;
                    break;
                case "local_regiment_cost":
                case "localregimentcost":
                    Type = ModifierType.LocalRegimentCost;
                    break;
                case "regiment_recruit_speed":
                case "regimentrecruitspeed":
                    Type = ModifierType.RegimentRecruitSpeed;
                    break;
                case "supply_limit":
                case "supplylimit":
                    Type = ModifierType.SupplyLimit;
                    break;
                case "supply_limit_modifier":
                case "supplylimitmodifier":
                    Type = ModifierType.SupplyLimitModifier;
                    break;
                case "local_own_coast_naval_combat_bonus":
                case "localowncoastnavalcombatbonus":
                    Type = ModifierType.LocalOwnCoastNavalCombatBonus;
                    break;
                case "local_has_banners":
                case "localhasbanners":
                    Type = ModifierType.LocalHasBanners;
                    break;
                case "local_amount_of_banners":
                case "localamountofbanners":
                    Type = ModifierType.LocalAmountOfBanners;
                    break;
                case "local_amount_of_cawa":
                case "localamountofcawa":
                    Type = ModifierType.LocalAmountOfCawa;
                    break;
                case "local_has_carolean":
                case "localhascarolean":
                    Type = ModifierType.LocalHasCarolean;
                    break;
                case "local_amount_of_carolean":
                case "localamountofcarolean":
                    Type = ModifierType.LocalAmountOfCarolean;
                    break;
                case "local_amount_of_hussars":
                case "localamountofhussars":
                    Type = ModifierType.LocalAmountOfHussars;
                    break;
                case "local_naval_engagement_modifier":
                case "localnavalengagementmodifier":
                    Type = ModifierType.LocalNavalEngagementModifier;
                    break;
                case "local_sailors":
                case "localsailors":
                    Type = ModifierType.LocalSailors;
                    break;
                case "local_sailors_modifier":
                case "localsailorsmodifier":
                    Type = ModifierType.LocalSailorsModifier;
                    break;
                case "local_ship_cost":
                case "localshipcost":
                    Type = ModifierType.LocalShipCost;
                    break;
                case "local_ship_repair":
                case "localshiprepair":
                    Type = ModifierType.LocalShipRepair;
                    break;
                case "ship_recruit_speed":
                case "shiprecruitspeed":
                    Type = ModifierType.ShipRecruitSpeed;
                    break;
                case "blockade_force_required":
                case "blockadeforcerequired":
                    Type = ModifierType.BlockadeForceRequired;
                    break;
                case "hostile_disembark_speed":
                case "hostiledisembarkspeed":
                    Type = ModifierType.HostileDisembarkSpeed;
                    break;
                case "hostile_fleet_attrition":
                case "hostilefleetattrition":
                    Type = ModifierType.HostileFleetAttrition;
                    break;
                case "block_slave_raid":
                case "blockslaveraid":
                    Type = ModifierType.BlockSlaveRaid;
                    break;
                case "local_warscore_cost_modifier":
                case "localwarscorecostmodifier":
                    Type = ModifierType.LocalWarscoreCostModifier;
                    break;
                case "inflation_reduction_local":
                case "inflationreductionlocal":
                    Type = ModifierType.InflationReductionLocal;
                    break;
                case "local_state_maintenance_modifier":
                case "localstatemaintenancemodifier":
                    Type = ModifierType.LocalStateMaintenanceModifier;
                    break;
                case "local_build_cost":
                case "localbuildcost":
                    Type = ModifierType.LocalBuildCost;
                    break;
                case "local_build_time":
                case "localbuildtime":
                    Type = ModifierType.LocalBuildTime;
                    break;
                case "local_great_project_upgrade_cost":
                case "localgreatprojectupgradecost":
                    Type = ModifierType.LocalGreatProjectUpgradeCost;
                    break;
                case "local_monthly_devastation":
                case "localmonthlydevastation":
                    Type = ModifierType.LocalMonthlyDevastation;
                    break;
                case "local_prosperity_growth":
                case "localprosperitygrowth":
                    Type = ModifierType.LocalProsperityGrowth;
                    break;
                case "local_production_efficiency":
                case "localproductionefficiency":
                    Type = ModifierType.LocalProductionEfficiency;
                    break;
                case "local_tax_modifier":
                case "localtaxmodifier":
                    Type = ModifierType.LocalTaxModifier;
                    break;
                case "tax_income":
                case "taxincome":
                    Type = ModifierType.TaxIncome;
                    break;
                case "allowed_num_of_buildings":
                case "allowednumofbuildings":
                    Type = ModifierType.AllowedNumOfBuildings;
                    break;
                case "allowed_num_of_manufactories":
                case "allowednumofmanufactories":
                    Type = ModifierType.AllowedNumOfManufactories;
                    break;
                case "local_development_cost":
                case "localdevelopmentcost":
                    Type = ModifierType.LocalDevelopmentCost;
                    break;
                case "local_development_cost_modifier":
                case "localdevelopmentcostmodifier":
                    Type = ModifierType.LocalDevelopmentCostModifier;
                    break;
                case "local_gold_depletion_chance_modifier":
                case "localgolddepletionchancemodifier":
                    Type = ModifierType.LocalGoldDepletionChanceModifier;
                    break;
                case "local_institution_spread":
                case "localinstitutionspread":
                    Type = ModifierType.LocalInstitutionSpread;
                    break;
                case "local_core_creation":
                case "localcorecreation":
                    Type = ModifierType.LocalCoreCreation;
                    break;
                case "local_governing_cost":
                case "localgoverningcost":
                    Type = ModifierType.LocalGoverningCost;
                    break;
                case "statewide_governing_cost":
                case "statewidegoverningcost":
                    Type = ModifierType.StatewideGoverningCost;
                    break;
                case "local_governing_cost_increase":
                case "localgoverningcostincrease":
                    Type = ModifierType.LocalGoverningCostIncrease;
                    break;
                case "institution_growth":
                case "institutiongrowth":
                    Type = ModifierType.InstitutionGrowth;
                    break;
                case "local_culture_conversion_cost":
                case "localcultureconversioncost":
                    Type = ModifierType.LocalCultureConversionCost;
                    break;
                case "local_unrest":
                case "localunrest":
                    Type = ModifierType.LocalUnrest;
                    break;
                case "local_autonomy":
                case "localautonomy":
                    Type = ModifierType.LocalAutonomy;
                    break;
                case "local_years_of_nationalism":
                case "localyearsofnationalism":
                    Type = ModifierType.LocalYearsOfNationalism;
                    break;
                case "min_local_autonomy":
                case "minlocalautonomy":
                    Type = ModifierType.MinLocalAutonomy;
                    break;
                case "local_missionary_strength":
                case "localmissionarystrength":
                    Type = ModifierType.LocalMissionaryStrength;
                    break;
                case "local_religious_unity_contribution":
                case "localreligiousunitycontribution":
                    Type = ModifierType.LocalReligiousUnityContribution;
                    break;
                case "local_missionary_maintenance_cost":
                case "localmissionarymaintenancecost":
                    Type = ModifierType.LocalMissionaryMaintenanceCost;
                    break;
                case "local_religious_conversion_resistance":
                case "localreligiousconversionresistance":
                    Type = ModifierType.LocalReligiousConversionResistance;
                    break;
                case "local_colonial_growth":
                case "localcolonialgrowth":
                    Type = ModifierType.LocalColonialGrowth;
                    break;
                case "local_colonist_placement_chance":
                case "localcolonistplacementchance":
                    Type = ModifierType.LocalColonistPlacementChance;
                    break;
                case "province_trade_power_modifier":
                case "provincetradepowermodifier":
                    Type = ModifierType.ProvinceTradePowerModifier;
                    break;
                case "province_trade_power_value":
                case "provincetradepowervalue":
                    Type = ModifierType.ProvinceTradePowerValue;
                    break;
                case "trade_goods_size_modifier":
                case "tradegoodssizemodifier":
                    Type = ModifierType.TradeGoodsSizeModifier;
                    break;
                case "trade_goods_size":
                case "tradegoodssize":
                    Type = ModifierType.TradeGoodsSize;
                    break;
                case "trade_value_modifier":
                case "tradevaluemodifier":
                    Type = ModifierType.TradeValueModifier;
                    break;
                case "trade_value":
                case "tradevalue":
                    Type = ModifierType.TradeValue;
                    break;
                case "local_center_of_trade_upgrade_cost":
                case "localcenteroftradeupgradecost":
                    Type = ModifierType.LocalCenterOfTradeUpgradeCost;
                    break;

            }
            return Type;
        }

        public static object CheckValueOrReturnDefault(ModifierType t, object value)
        {
            if(value.GetType() == GetExpectedType(t)){
                return value;
            }
            else
            {
                Type all = GetAllowedType(t);
                if (all == typeof(bool) && value.GetType() != typeof(bool))
                    return false;
                else if (all == typeof(float) && (value.GetType() != typeof(int) && value.GetType() != typeof(float)))
                    return 0;
                else
                    return value;
            }
        }
        public static int IsCorrectType(ModifierType t, object value)
        {
            Type type = value.GetType();
            Type expected = GetExpectedType(t);
            if (type.GetHashCode() == expected.GetHashCode())
                return 2;
            if (type.GetHashCode() == typeof(int).GetHashCode() && expected.GetHashCode() == typeof(float).GetHashCode())
                return 2;
            if (expected.GetHashCode() == typeof(int).GetHashCode() && type.GetHashCode() == typeof(float).GetHashCode())
                return 1;
            return 0;
            
        }
        public static Type GetAllowedType(ModifierType t)
        {
            switch (t)
            {
                default:
                    return typeof(float);
                case ModifierType.MayRecruitFemaleGenerals:
                case ModifierType.CappedByForcelimit:
                case ModifierType.MayBuildSupplyDepot:
                case ModifierType.MayRefillGarrison:
                case ModifierType.MayReturnManpowerOnDisband:
                case ModifierType.AttackBonusInCapitalTerrain:
                case ModifierType.CanBypassForts:
                case ModifierType.ForceMarchFree:
                case ModifierType.HasBanners:
                case ModifierType.HasCarolean:
                case ModifierType.CanRecruitHussars:
                case ModifierType.MayPerformSlaveRaid:
                case ModifierType.MayPerformSlaveRaidOnSameReligion:
                case ModifierType.SeaRepair:
                case ModifierType.CanChainClaim:
                case ModifierType.CbOnOverseas:
                case ModifierType.CbOnPrimitives:
                case ModifierType.IdeaClaimColonies:
                case ModifierType.CbOnGovernmentEnemies:
                case ModifierType.CbOnReligiousEnemies:
                case ModifierType.ReducedStabImpacts:
                case ModifierType.CanFabricateForVassals:
                case ModifierType.IgnoreCoringDistance:
                case ModifierType.BlockIntroduceHeir:
                case ModifierType.AllowFreeEstatePrivilegeRevocation:
                case ModifierType.CanNotDeclareWar:
                case ModifierType.NoStabilityLossOnMonarchDeath:
                case ModifierType.CanTransferVassalWargoal:
                case ModifierType.AllowClientStates:
                case ModifierType.CanNotBuildMissionaries:
                case ModifierType.ExtraManpowerAtReligiousWar:
                case ModifierType.CanNotBuildColonies:
                case ModifierType.MayExplore:
                case ModifierType.AutoExploreAdjacentToColony:
                case ModifierType.MayEstablishFrontier:
                case ModifierType.CanColonyBoostDevelopment:
                case ModifierType.FreeMaintenanceOnExplConq:
                case ModifierType.CanNotSendMerchants:
                case ModifierType.LocalHasBanners:
                case ModifierType.LocalHasCarolean:
                case ModifierType.BlockSlaveRaid:
                    return typeof(bool);
            }
        }
        public static Type GetExpectedType(ModifierType t)
        {

            if (GetAllowedType(t) == typeof(bool))
                return typeof(bool);

            switch (t)
            {
                default:
                    return typeof(float);
                case ModifierType.ArtilleryLevelsAvailableVsFort:
                case ModifierType.SiegeBlockadeProgress:
                case ModifierType.GlobalManpower:
                case ModifierType.LandForcelimit:
                case ModifierType.PossibleCondottieri:
                case ModifierType.LeaderLandFire:
                case ModifierType.LeaderLandManuever:
                case ModifierType.LeaderLandShock:
                case ModifierType.LeaderSiege:
                case ModifierType.FreeLeaderPool:
                case ModifierType.FreeLandLeaderPool:
                case ModifierType.FreeNavyLeaderPool:
                case ModifierType.GlobalAttackerDiceRollBonus:
                case ModifierType.GlobalDefenderDiceRollBonus:
                case ModifierType.OwnTerritoryDiceRollBonus:
                case ModifierType.NavalForcelimit:
                case ModifierType.GlobalSailors:
                case ModifierType.LeaderNavalFire:
                case ModifierType.LeaderNavalManuever:
                case ModifierType.LeaderNavalShock:
                case ModifierType.LandingPenalty:
                case ModifierType.FlagshipLandingPenalty:
                case ModifierType.NumberOfCannonsFlagship:
                case ModifierType.ExplorationMissionRangeInFleetModifier:
                case ModifierType.Diplomats:
                case ModifierType.DiplomaticUpkeep:
                case ModifierType.YearsToIntegratePersonalUnion:
                case ModifierType.StabilityCostToDeclareWar:
                case ModifierType.AcceptVassalizationReasons:
                case ModifierType.TransferTradePowerReasons:
                case ModifierType.GlobalAllowedNumOfBuildings:
                case ModifierType.FreeAdmPolicy:
                case ModifierType.FreeDipPolicy:
                case ModifierType.FreeMilPolicy:
                case ModifierType.PossiblePolicy:
                case ModifierType.FreePolicy:
                case ModifierType.CountryAdminPower:
                case ModifierType.CountryDiplomaticPower:
                case ModifierType.CountryMilitaryPower:
                case ModifierType.MonthlySplendor:
                case ModifierType.AdvisorPool:
                case ModifierType.MonarchAdminPower:
                case ModifierType.MonarchDiplomaticPower:
                case ModifierType.MonarchMilitaryPower:
                case ModifierType.LocalHeirAdm:
                case ModifierType.LocalHeirDip:
                case ModifierType.LocalHeirMil:
                case ModifierType.NationalFocusYears:
                case ModifierType.ElectionCycle:
                case ModifierType.CandidateRandomBonus:
                case ModifierType.GoverningCapacity:
                case ModifierType.ReasonsToElect:
                case ModifierType.MaxFreeCities:
                case ModifierType.MaxElectors:
                case ModifierType.LegitimateSubjectElector:
                case ModifierType.ImperialReformCatholicApproval:
                case ModifierType.NumAcceptedCultures:
                case ModifierType.RelationWithSameCulture:
                case ModifierType.RelationWithSameCultureGroup:
                case ModifierType.RelationWithAcceptedCulture:
                case ModifierType.RelationWithOtherCulture:
                case ModifierType.MinAutonomy:
                case ModifierType.LibertyDesire:
                case ModifierType.ReducedLibertyDesire:
                case ModifierType.ReducedLibertyDesireOnSameContinent:
                case ModifierType.Missionaries:
                case ModifierType.RelationWithSameReligion:
                case ModifierType.RelationWithHeretics:
                case ModifierType.RelationWithHeathens:
                case ModifierType.ColonistPlacementChance:
                case ModifierType.GlobalColonialGrowth:
                case ModifierType.Merchants:
                case ModifierType.PlacedMerchantPower:
                case ModifierType.LocalAttackerDiceRollBonus:
                case ModifierType.LocalDefenderDiceRollBonus:
                case ModifierType.FortLevel:
                case ModifierType.LocalManpower:
                case ModifierType.SupplyLimit:
                case ModifierType.LocalSailors:
                case ModifierType.AllowedNumOfBuildings:
                case ModifierType.AllowedNumOfManufactories:
                case ModifierType.MinLocalAutonomy:
                case ModifierType.LocalColonialGrowth:
                    return typeof(int);
            }
        }
        public static bool IsNegative(ModifierType t)
        {
            //NOT YET IMPLEMENTED
            return false;
        }

        public string Name;
        public ModifierType Type;
        public object Value;
        public Modifier(string name, object value)
        {
            Name = name;
            Value = value;
            Type = ConvertIntoModifierType(name);
        }
    }

    public enum CountryModifierType
    {
        ArmyTradition, ArmyTraditionDecay, ArmyTraditionFromBattle, YearlyArmyProfessionalism, DrillGainModifier, DrillDecayModifier,
        InfantryCost, InfantryPower, InfantryFire, InfantryShock, CavalryCost, CavalryPower, CavalryFire, CavalryShock, ArtilleryCost,
        ArtilleryPower, ArtilleryFire, ArtilleryShock, CavToInfRatio, CavalryFlanking, ArtilleryLevelsAvailableVsFort, BackrowArtilleryDamage,
        Discipline, MercenaryDiscipline, LandMorale, Defensiveness, SiegeAbility, MovementSpeed, FireDamage, FireDamageReceived, ShockDamage,
        ShockDamageReceived, MoraleDamage, MoraleDamageReceived, RecoverArmyMoraleSpeed, SiegeBlockadeProgress, ReservesOrganisation, LandAttrition,
        ReinforceCostModifier, ReinforceSpeed, ManpowerRecoverySpeed, GlobalManpower, GlobalManpowerModifier, GlobalRegimentCost, GlobalRegimentRecruitSpeed,
        GlobalSupplyLimitModifier, LandForcelimit, LandForcelimitModifier, LandMaintenanceModifier, MercenaryCost, MercMaintenanceModifier,
        PossibleCondottieri, HostileAttrition, MaxHostileAttrition, GarrisonSize, GlobalGarrisonGrowth, FortMaintenanceModifier, RivalBorderFortMaintenance,
        WarExhaustion, WarExhaustionCost, LeaderLandFire, LeaderLandManuever, LeaderLandShock, LeaderSiege, GeneralCost, FreeLeaderPool, FreeLandLeaderPool,
        FreeNavyLeaderPool, RazePowerGain, LootAmount, AvailableProvinceLoot, PrestigeFromLand, WarTaxesCostModifier, LeaderCost, MayRecruitFemaleGenerals,
        ManpowerInTrueFaithProvinces, MercenaryManpower, RegimentManpowerUsage, MilitaryTactics, CappedByForcelimit, GlobalAttackerDiceRollBonus,
        GlobalDefenderDiceRollBonus, OwnTerritoryDiceRollBonus, ManpowerInAcceptedCultureProvinces, ManpowerInCultureGroupProvinces,
        ManpowerInOwnCultureProvinces, MayBuildSupplyDepot, MayRefillGarrison, MayReturnManpowerOnDisband, AttackBonusInCapitalTerrain, CanBypassForts,
        ForceMarchFree, SpecialUnitForcelimit, AllowedMarineFraction, HasBanners, AmountOfBanners, AmountOfCawa, CawaCostModifier, HasCarolean,
        AmountOfCarolean, CanRecruitHussars, AmountOfHussars, NavyTradition, NavyTraditionDecay, NavalTraditionFromBattle, NavalTraditionFromTrade,
        HeavyShipCost, HeavyShipPower, LightShipCost, LightShipPower, GalleyCost, GalleyPower, TransportCost, TransportPower, GlobalShipCost,
        GlobalShipRecruitSpeed, GlobalShipRepair, NavalForcelimit, NavalForcelimitModifier, NavalMaintenanceModifier, GlobalSailors, GlobalSailorsModifier,
        SailorMaintenanceModifer, SailorsRecoverySpeed, BlockadeEfficiency, CaptureShipChance, GlobalNavalEngagementModifier, NavalAttrition, NavalMorale,
        ShipDurability, SunkShipMoraleHitRecieved, RecoverNavyMoraleSpeed, PrestigeFromNaval, LeaderNavalFire, LeaderNavalManuever, LeaderNavalShock,
        OwnCoastNavalCombatBonus, AdmiralCost, GlobalNavalBarrageCost, FlagshipCost, DisengagementChance, TransportAttrition, LandingPenalty,
        RegimentDisembarkSpeed, MayPerformSlaveRaid, MayPerformSlaveRaidOnSameReligion, SeaRepair, MovementSpeedInFleetModifier, AdmiralSkillGainModifier,
        FlagshipDurability, FlagshipMorale, FlagshipNavalEngagementModifier, MovementSpeedOntoOffBoatModifier, FlagshipLandingPenalty,
        NumberOfCannonsFlagshipModifier, NumberOfCannonsFlagship, NavalMaintenanceFlagshipModifier, TradePowerInFleetModifier, MoraleInFleetModifier,
        BlockadeImpactOnSiegeInFleetModifier, ExplorationMissionRangeInFleetModifier, BarrageCostInFleetModifier, NavalAttritionInFleetModifier,
        PrivateeringEfficiencyInFleetModifier, PrestigeFromBattlesInFleetModifier, NavalTraditionInFleetModifier, CannonsForHuntingPiratesInFleet, Diplomats,
        DiplomaticReputation, DiplomaticUpkeep, EnvoyTravelTime, FabricateClaimsCost, YearsToIntegratePersonalUnion, ImproveRelationModifier,
        VassalForcelimitBonus, VassalIncome, AeImpact, ClaimDuration, DiplomaticAnnexationCost, ProvinceWarscoreCost, UnjustifiedDemands, RivalChangeCost,
        JustifyTradeConflictCost, StabilityCostToDeclareWar, AcceptVassalizationReasons, TransferTradePowerReasons, MonthlyFederationFavorGrowth,
        MonthlyFavorModifier, CanChainClaim, CbOnOverseas, CbOnPrimitives, IdeaClaimColonies, CbOnGovernmentEnemies, CbOnReligiousEnemies, ReducedStabImpacts,
        CanFabricateForVassals, GlobalTaxIncome, GlobalTaxModifier, ProductionEfficiency, StateMaintenanceModifier, InflationActionCost, InflationReduction,
        MonthlyGoldInflationModifier, GoldDepletionChanceModifier, Interest, CanNotBuildBuildings, DevelopmentCost, DevelopmentCostModifier,
        TribalDevelopmentGrowth, AddTribalLandCost, SettleCost, GlobalAllowedNumOfBuildings, BuildCost, BuildTime, GreatProjectUpgradeCost,
        GlobalMonthlyDevastation, GlobalProsperityGrowth, AdministrativeEfficiency, CoreCreation, CoreDecayOnYourOwn, EnemyCoreCreation,
        IgnoreCoringDistance, AdmCostModifier, DipCostModifier, MilCostModifier, TechnologyCost, IdeaCost, EmbracementCost, GlobalInstitutionSpread,
        InstitutionSpreadFromTrueFaith, NativeAdvancementCost, AllPowerCost, InnovativenessGain, FreeAdmPolicy, FreeDipPolicy, FreeMilPolicy,
        PossibleAdmPolicy, PossibleDipPolicy, PossibleMilPolicy, PossiblePolicy, FreePolicy, CountryAdminPower, CountryDiplomaticPower, CountryMilitaryPower,
        Prestige, PrestigeDecay, MonthlySplendor, YearlyCorruption, AdvisorCost, AdvisorPool, FemaleAdvisorChance, HeirChance, MonthlyHeirClaimIncrease,
        MonthlyHeirClaimIncreaseModifier, BlockIntroduceHeir, MonarchAdminPower, MonarchDiplomaticPower, MonarchMilitaryPower, AdmAdvisorCost, DipAdvisorCost,
        MilAdvisorCost, MonthlySupportHeirGain, PowerProjectionFromInsults, MonarchLifespan, LocalHeirAdm, LocalHeirDip, LocalHeirMil, NationalFocusYears,
        YearlyAbsolutism, MaxAbsolutism, Legitimacy, RepublicanTradition, Devotion, HordeUnity, Meritocracy, MonthlyMilitarizedSociety,
        YearlyTribalAllegiance, ImperialMandate, ElectionCycle, CandidateRandomBonus, ReelectionCost, GoverningCapacity, GoverningCapacityModifier,
        GoverningCost, StateGoverningCost, TradeCompanyGoverningCost, StateGoverningCostIncrease, ExpandAdministrationCost, YearlyRevolutionaryZeal,
        MaxRevolutionaryZeal, ReformProgressGrowth, MonthlyReformProgress, MonthlyReformProgressModifier, MoveCapitalCostModifier,
        AllEstateInfluenceModifier, AllEstateLoyaltyEquilibrium, AllowFreeEstatePrivilegeRevocation, ImperialAuthority, ImperialAuthorityValue,
        FreeCityImperialAuthority, ReasonsToElect, ImperialMercenaryCost, MaxFreeCities, MaxElectors, LegitimateSubjectElector, ManpowerAgainstImperialEnemies,
        ImperialReformCatholicApproval, CultureConversionCost, NumAcceptedCultures, SameCultureAdvisorCost, PromoteCultureCost, RelationWithSameCulture,
        RelationWithSameCultureGroup, RelationWithAcceptedCulture, RelationWithOtherCulture, CanNotDeclareWar, GlobalUnrest, StabilityCostModifier,
        GlobalAutonomy, MinAutonomy, AutonomyChangeTime, HarshTreatmentCost, GlobalRebelSuppressionEfficiency, YearsOfNationalism, MinAutonomyInTerritories,
        UnrestCatholicProvinces, NoStabilityLossOnMonarchDeath, CanTransferVassalWargoal, LibertyDesire, LibertyDesireFromSubjectDevelopment,
        ReducedLibertyDesire, ReducedLibertyDesireOnSameContinent, AllowClientStates, ColonialTypeChangeCostModifier, ColonialSubjectTypeUpgradeCostModifier,
        SpyOffence, GlobalSpyDefence, DiscoveredRelationsImpact, RebelSupportEfficiency, GlobalMissionaryStrength, GlobalHereticMissionaryStrength,
        GlobalHeathenMissionaryStrength, CanNotBuildMissionaries, Missionaries, MissionaryMaintenanceCost, ReligiousUnity, ToleranceOwn, ToleranceHeretic,
        ToleranceHeathen, ToleranceOfHereticsCapacity, ToleranceOfHeathensCapacity, PapalInfluence, PapalInfluenceFromCardinals, AppointCardinalCost,
        CuriaTreasuryContribution, CuriaPowersCost, MonthlyChurchPower, ChurchPowerModifier, MonthlyFervorIncrease, YearlyPatriarchAuthority, MonthlyPiety,
        MonthlyPietyAccelerator, HarmonizationSpeed, YearlyHarmony, MonthlyKarma, MonthlyKarmaAccelerator, YearlyKarmaDecay, YearlyDoomReduction,
        YearlyAuthority, EnforceReligionCost, PrestigePerDevelopmentFromConversion, WarscoreCostVsOtherReligion, EstablishOrderCost,
        GlobalReligiousConversionResistance, RelationWithSameReligion, RelationWithHeretics, RelationWithHeathens, NoReligionPenalty,
        ExtraManpowerAtReligiousWar, CanNotBuildColonies, Colonists, ColonistPlacementChance, GlobalColonialGrowth, Range, NativeUprisingChance,
        NativeAssimilation, MigrationCost, GlobalTariffs, TreasureFleetIncome, ExpelMinoritiesCost, MayExplore, AutoExploreAdjacentToColony,
        MayEstablishFrontier, CanColonyBoostDevelopment, FreeMaintenanceOnExplConq, CaravanPower, CanNotSendMerchants, Merchants, PlacedMerchantPower,
        GlobalTradePower, GlobalForeignTradePower, GlobalOwnTradePower, GlobalProvTradePowerModifier, GlobalTradeGoodsSizeModifier, GlobalTradeGoodsSize,
        TradeEfficiency, TradeRangeModifier, TradeSteering, GlobalShipTradePower, PrivateerEfficiency, EmbargoEfficiency, ShipPowerPropagation,
        CenterOfTradeUpgradeCost, TradeCompanyInvestmentCost, MercantilismCost
    }

    public enum ModifierType
    {
        NoType,

        ArmyTradition, ArmyTraditionDecay, ArmyTraditionFromBattle, YearlyArmyProfessionalism, DrillGainModifier, DrillDecayModifier,
        InfantryCost, InfantryPower, InfantryFire, InfantryShock, CavalryCost, CavalryPower, CavalryFire, CavalryShock, ArtilleryCost,
        ArtilleryPower, ArtilleryFire, ArtilleryShock, CavToInfRatio, CavalryFlanking, ArtilleryLevelsAvailableVsFort, BackrowArtilleryDamage,
        Discipline, MercenaryDiscipline, LandMorale, Defensiveness, SiegeAbility, MovementSpeed, FireDamage, FireDamageReceived, ShockDamage,
        ShockDamageReceived, MoraleDamage, MoraleDamageReceived, RecoverArmyMoraleSpeed, SiegeBlockadeProgress, ReservesOrganisation, LandAttrition,
        ReinforceCostModifier, ReinforceSpeed, ManpowerRecoverySpeed, GlobalManpower, GlobalManpowerModifier, GlobalRegimentCost, GlobalRegimentRecruitSpeed,
        GlobalSupplyLimitModifier, LandForcelimit, LandForcelimitModifier, LandMaintenanceModifier, MercenaryCost, MercMaintenanceModifier,
        PossibleCondottieri, HostileAttrition, MaxHostileAttrition, GarrisonSize, GlobalGarrisonGrowth, FortMaintenanceModifier, RivalBorderFortMaintenance,
        WarExhaustion, WarExhaustionCost, LeaderLandFire, LeaderLandManuever, LeaderLandShock, LeaderSiege, GeneralCost, FreeLeaderPool, FreeLandLeaderPool,
        FreeNavyLeaderPool, RazePowerGain, LootAmount, AvailableProvinceLoot, PrestigeFromLand, WarTaxesCostModifier, LeaderCost, MayRecruitFemaleGenerals,
        ManpowerInTrueFaithProvinces, MercenaryManpower, RegimentManpowerUsage, MilitaryTactics, CappedByForcelimit, GlobalAttackerDiceRollBonus,
        GlobalDefenderDiceRollBonus, OwnTerritoryDiceRollBonus, ManpowerInAcceptedCultureProvinces, ManpowerInCultureGroupProvinces,
        ManpowerInOwnCultureProvinces, MayBuildSupplyDepot, MayRefillGarrison, MayReturnManpowerOnDisband, AttackBonusInCapitalTerrain, CanBypassForts,
        ForceMarchFree, SpecialUnitForcelimit, AllowedMarineFraction, HasBanners, AmountOfBanners, AmountOfCawa, CawaCostModifier, HasCarolean,
        AmountOfCarolean, CanRecruitHussars, AmountOfHussars, NavyTradition, NavyTraditionDecay, NavalTraditionFromBattle, NavalTraditionFromTrade,
        HeavyShipCost, HeavyShipPower, LightShipCost, LightShipPower, GalleyCost, GalleyPower, TransportCost, TransportPower, GlobalShipCost,
        GlobalShipRecruitSpeed, GlobalShipRepair, NavalForcelimit, NavalForcelimitModifier, NavalMaintenanceModifier, GlobalSailors, GlobalSailorsModifier,
        SailorMaintenanceModifer, SailorsRecoverySpeed, BlockadeEfficiency, CaptureShipChance, GlobalNavalEngagementModifier, NavalAttrition, NavalMorale,
        ShipDurability, SunkShipMoraleHitRecieved, RecoverNavyMoraleSpeed, PrestigeFromNaval, LeaderNavalFire, LeaderNavalManuever, LeaderNavalShock,
        OwnCoastNavalCombatBonus, AdmiralCost, GlobalNavalBarrageCost, FlagshipCost, DisengagementChance, TransportAttrition, LandingPenalty,
        RegimentDisembarkSpeed, MayPerformSlaveRaid, MayPerformSlaveRaidOnSameReligion, SeaRepair, MovementSpeedInFleetModifier, AdmiralSkillGainModifier,
        FlagshipDurability, FlagshipMorale, FlagshipNavalEngagementModifier, MovementSpeedOntoOffBoatModifier, FlagshipLandingPenalty,
        NumberOfCannonsFlagshipModifier, NumberOfCannonsFlagship, NavalMaintenanceFlagshipModifier, TradePowerInFleetModifier, MoraleInFleetModifier,
        BlockadeImpactOnSiegeInFleetModifier, ExplorationMissionRangeInFleetModifier, BarrageCostInFleetModifier, NavalAttritionInFleetModifier,
        PrivateeringEfficiencyInFleetModifier, PrestigeFromBattlesInFleetModifier, NavalTraditionInFleetModifier, CannonsForHuntingPiratesInFleet, Diplomats,
        DiplomaticReputation, DiplomaticUpkeep, EnvoyTravelTime, FabricateClaimsCost, YearsToIntegratePersonalUnion, ImproveRelationModifier,
        VassalForcelimitBonus, VassalIncome, AeImpact, ClaimDuration, DiplomaticAnnexationCost, ProvinceWarscoreCost, UnjustifiedDemands, RivalChangeCost,
        JustifyTradeConflictCost, StabilityCostToDeclareWar, AcceptVassalizationReasons, TransferTradePowerReasons, MonthlyFederationFavorGrowth,
        MonthlyFavorModifier, CanChainClaim, CbOnOverseas, CbOnPrimitives, IdeaClaimColonies, CbOnGovernmentEnemies, CbOnReligiousEnemies, ReducedStabImpacts,
        CanFabricateForVassals, GlobalTaxIncome, GlobalTaxModifier, ProductionEfficiency, StateMaintenanceModifier, InflationActionCost, InflationReduction,
        MonthlyGoldInflationModifier, GoldDepletionChanceModifier, Interest, CanNotBuildBuildings, DevelopmentCost, DevelopmentCostModifier,
        TribalDevelopmentGrowth, AddTribalLandCost, SettleCost, GlobalAllowedNumOfBuildings, BuildCost, BuildTime, GreatProjectUpgradeCost,
        GlobalMonthlyDevastation, GlobalProsperityGrowth, AdministrativeEfficiency, CoreCreation, CoreDecayOnYourOwn, EnemyCoreCreation,
        IgnoreCoringDistance, AdmCostModifier, DipCostModifier, MilCostModifier, TechnologyCost, IdeaCost, EmbracementCost, GlobalInstitutionSpread,
        InstitutionSpreadFromTrueFaith, NativeAdvancementCost, AllPowerCost, InnovativenessGain, FreeAdmPolicy, FreeDipPolicy, FreeMilPolicy,
        PossibleAdmPolicy, PossibleDipPolicy, PossibleMilPolicy, PossiblePolicy, FreePolicy, CountryAdminPower, CountryDiplomaticPower, CountryMilitaryPower,
        Prestige, PrestigeDecay, MonthlySplendor, YearlyCorruption, AdvisorCost, AdvisorPool, FemaleAdvisorChance, HeirChance, MonthlyHeirClaimIncrease,
        MonthlyHeirClaimIncreaseModifier, BlockIntroduceHeir, MonarchAdminPower, MonarchDiplomaticPower, MonarchMilitaryPower, AdmAdvisorCost, DipAdvisorCost,
        MilAdvisorCost, MonthlySupportHeirGain, PowerProjectionFromInsults, MonarchLifespan, LocalHeirAdm, LocalHeirDip, LocalHeirMil, NationalFocusYears,
        YearlyAbsolutism, MaxAbsolutism, Legitimacy, RepublicanTradition, Devotion, HordeUnity, Meritocracy, MonthlyMilitarizedSociety,
        YearlyTribalAllegiance, ImperialMandate, ElectionCycle, CandidateRandomBonus, ReelectionCost, GoverningCapacity, GoverningCapacityModifier,
        GoverningCost, StateGoverningCost, TradeCompanyGoverningCost, StateGoverningCostIncrease, ExpandAdministrationCost, YearlyRevolutionaryZeal,
        MaxRevolutionaryZeal, ReformProgressGrowth, MonthlyReformProgress, MonthlyReformProgressModifier, MoveCapitalCostModifier,
        AllEstateInfluenceModifier, AllEstateLoyaltyEquilibrium, AllowFreeEstatePrivilegeRevocation, ImperialAuthority, ImperialAuthorityValue,
        FreeCityImperialAuthority, ReasonsToElect, ImperialMercenaryCost, MaxFreeCities, MaxElectors, LegitimateSubjectElector, ManpowerAgainstImperialEnemies,
        ImperialReformCatholicApproval, CultureConversionCost, NumAcceptedCultures, SameCultureAdvisorCost, PromoteCultureCost, RelationWithSameCulture,
        RelationWithSameCultureGroup, RelationWithAcceptedCulture, RelationWithOtherCulture, CanNotDeclareWar, GlobalUnrest, StabilityCostModifier,
        GlobalAutonomy, MinAutonomy, AutonomyChangeTime, HarshTreatmentCost, GlobalRebelSuppressionEfficiency, YearsOfNationalism, MinAutonomyInTerritories,
        UnrestCatholicProvinces, NoStabilityLossOnMonarchDeath, CanTransferVassalWargoal, LibertyDesire, LibertyDesireFromSubjectDevelopment,
        ReducedLibertyDesire, ReducedLibertyDesireOnSameContinent, AllowClientStates, ColonialTypeChangeCostModifier, ColonialSubjectTypeUpgradeCostModifier,
        SpyOffence, GlobalSpyDefence, DiscoveredRelationsImpact, RebelSupportEfficiency, GlobalMissionaryStrength, GlobalHereticMissionaryStrength,
        GlobalHeathenMissionaryStrength, CanNotBuildMissionaries, Missionaries, MissionaryMaintenanceCost, ReligiousUnity, ToleranceOwn, ToleranceHeretic,
        ToleranceHeathen, ToleranceOfHereticsCapacity, ToleranceOfHeathensCapacity, PapalInfluence, PapalInfluenceFromCardinals, AppointCardinalCost,
        CuriaTreasuryContribution, CuriaPowersCost, MonthlyChurchPower, ChurchPowerModifier, MonthlyFervorIncrease, YearlyPatriarchAuthority, MonthlyPiety,
        MonthlyPietyAccelerator, HarmonizationSpeed, YearlyHarmony, MonthlyKarma, MonthlyKarmaAccelerator, YearlyKarmaDecay, YearlyDoomReduction,
        YearlyAuthority, EnforceReligionCost, PrestigePerDevelopmentFromConversion, WarscoreCostVsOtherReligion, EstablishOrderCost,
        GlobalReligiousConversionResistance, RelationWithSameReligion, RelationWithHeretics, RelationWithHeathens, NoReligionPenalty,
        ExtraManpowerAtReligiousWar, CanNotBuildColonies, Colonists, ColonistPlacementChance, GlobalColonialGrowth, Range, NativeUprisingChance,
        NativeAssimilation, MigrationCost, GlobalTariffs, TreasureFleetIncome, ExpelMinoritiesCost, MayExplore, AutoExploreAdjacentToColony,
        MayEstablishFrontier, CanColonyBoostDevelopment, FreeMaintenanceOnExplConq, CaravanPower, CanNotSendMerchants, Merchants, PlacedMerchantPower,
        GlobalTradePower, GlobalForeignTradePower, GlobalOwnTradePower, GlobalProvTradePowerModifier, GlobalTradeGoodsSizeModifier, GlobalTradeGoodsSize,
        TradeEfficiency, TradeRangeModifier, TradeSteering, GlobalShipTradePower, PrivateerEfficiency, EmbargoEfficiency, ShipPowerPropagation,
        CenterOfTradeUpgradeCost, TradeCompanyInvestmentCost, MercantilismCost, MaxAttrition, Attrition, LocalHostileAttrition, LocalFortMaintenanceModifier,
        LocalGarrisonSize, LocalAttackerDiceRollBonus, LocalDefenderDiceRollBonus, FortLevel, GarrisonGrowth, LocalDefensiveness, LocalFriendlyMovementSpeed,
        LocalHostileMovementSpeed, LocalManpower, LocalManpowerModifier, LocalRegimentCost, RegimentRecruitSpeed, SupplyLimit, SupplyLimitModifier,
        LocalOwnCoastNavalCombatBonus, LocalHasBanners, LocalAmountOfBanners, LocalAmountOfCawa, LocalHasCarolean, LocalAmountOfCarolean, LocalAmountOfHussars,
        LocalNavalEngagementModifier, LocalSailors, LocalSailorsModifier, LocalShipCost, LocalShipRepair, ShipRecruitSpeed, BlockadeForceRequired,
        HostileDisembarkSpeed, HostileFleetAttrition, BlockSlaveRaid, LocalWarscoreCostModifier, InflationReductionLocal, LocalStateMaintenanceModifier,
        LocalBuildCost, LocalBuildTime, LocalGreatProjectUpgradeCost, LocalMonthlyDevastation, LocalProsperityGrowth, LocalProductionEfficiency,
        LocalTaxModifier, TaxIncome, AllowedNumOfBuildings, AllowedNumOfManufactories, LocalDevelopmentCost, LocalDevelopmentCostModifier,
        LocalGoldDepletionChanceModifier, LocalInstitutionSpread, LocalCoreCreation, LocalGoverningCost, StatewideGoverningCost, LocalGoverningCostIncrease,
        InstitutionGrowth, LocalCultureConversionCost, LocalUnrest, LocalAutonomy, LocalYearsOfNationalism, MinLocalAutonomy, LocalMissionaryStrength,
        LocalReligiousUnityContribution, LocalMissionaryMaintenanceCost, LocalReligiousConversionResistance, LocalColonialGrowth, LocalColonistPlacementChance,
        ProvinceTradePowerModifier, ProvinceTradePowerValue, TradeGoodsSizeModifier, TradeGoodsSize, TradeValueModifier, TradeValue,
        LocalCenterOfTradeUpgradeCost
    }
}
