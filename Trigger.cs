using Eu4ModEditor;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Globalization;
using System.Xml.Linq;

namespace Eu4ModEditor
{
    

    public enum Scope { Country, Province, Anywhere, CountryORProvince, CountryORAnywhere, State, Unit, TradeNode, Mission, Clause, AnyTrigger, Invalid};
    public enum Value
    {
        String, Integer, IdentifierORProvinceScope,
        MissionIdentifier, IntegerORTagORScope, AdvisorIdentifier,
        AdvisorID, Boolean, TagORScope, AreaIdentifier, Float,
        SubjectIdentifier, BuildingIdentifier, RebelIdentifier,
        ProvinceID, ReligionIdentifier, ColonialRegionIdentifier,
        Tag, CultureIdentifierORScope, PersonalityIdentifier,
        ReligionIdentifierORScope, ContinentIdetifierORTagORScopeORProvinceID,
        CouncilPositionIdentifier, IntegerOREstateIdentifier,
        AgeIdentifier, BribeIdentifier, DebateIdentifier, IconIdentifier,
        IntegerORScope, CultureIdentifierORCapital, ReligionIdentifierORCapital,
        StringORTagORScope, ChinaReformIdentifier, TagORBoolean,
        FactionIdentifier, IdeaGroupIdentifier, GovernmentIdentifier, PolicyIdentfier,
        ModifierIdentifier, CultIdentifier, AgeAbilityIdentifier,
        ClimateIdentifier, ChurchAspectIdentifier, ConsortFlagIdentifier,
        CountryFlagIdentifier, DisasterIdentfier, DLCIdentifier, EstateIdentifier,
        PrivilegeIdentifier, GlobalFlagIdentifier, GovernmentMechanicIdentfier,
        GovernmentAttributeIdentifier, Construction, ProvinceIDORTagORScope,
        BooleanOREstateIdentifier, GovernmentPowerIdentifier, ReligionORReligionGroupORScope,
        StringORBoolean, HeirFlagIdentifier, IdeaIdentifier, InstitutionIdentifier, TradeGoodIdentifier,
        TradeNodeProvince, MonsoonIdentifier, PersonalDietyIdentifier, TradeCompanyIdentifier,
        ProvinceFlagIdentifier, ReformIdentifier, RulerFlagIdentifier, EventTargetIdentifier,
        Scope, StateEdictIdentifier, TerrainIdentifier, UnitTypeIdentifier, WinterIdentifier,
        ProvinceIDORScope, HolyOrderIdentifier, HREReformIdentifier, FloatORTagORScope,
        LeagueIdentifier, HegemonTypeIdentifier, IncidentIdentifierORBooleanORAnyORNone,
        IncidentIdentifier, BooleanORTagORScope, NationalFocusIdentifier, NativePolicyIdentifier,
        AIPersonalityIdentifier, ProvinceGroupIdentifier, RegionIdentifier, ReligionGroupORScope,
        Date, SuperregionIdentifier, TechnologyGroupIdentifierORScope,








    }
    //Identifier

    public class TriggerItem
    {
        public int LineNumber = 0;
        public string FileName;
        public TriggerConnector Parent;
        public string GetFileName()
        {
            if (FileName == null && Parent != null)
                return Parent.GetFileName();
            else if (FileName != null)
                return FileName;
            else
                return "Unknown";
        }
        public int GetLineNumber()
        {
            return LineNumber;
        }
    }
    public class Trigger : TriggerItem
    {
        public Trigger(string variableChecked, string valueExpected)
        {
            VariableChecked = TriggerVariable.GetVariableByName(variableChecked);
            if (VariableChecked == TriggerVariable.TriggerVar.UnknownTrigger)
                UnknownVariableChecked = variableChecked;
            ValueExpected = valueExpected;
        }
        public TriggerVariable.TriggerVar VariableChecked;
        public string UnknownVariableChecked;
        public string ValueExpected = "";
        public bool Not = false;
    }

    public enum QueryVariables
    {
        Development, Tax, Production, Manpower, Religion, ReligionGroup, Culture, CultureGroup, HRE, TradeGood,
        LatentTradeGood, Area, Region, Superregion, CenterOfTrade, ID, All, AllChecked, Tag,
        QueryVariableNone
    };

    public class ValidatorProbe
    {
        private string ValidationOutput = "";
        public LogLevel Level = LogLevel.Error;
        public ValidatorProbe(LogLevel level)
        {
            Level = level;
        }

        public void Report(LogLevel level, string Text, string File, int Line)
        {
            ValidationOutput += "\n" + level.ToString() + ": " + Text + " -File: " + File + " Line: " + Line;
        }

        public enum LogLevel { Error, Alert }
    }

    public class TriggerConnector : TriggerItem
    {
        public Scope ConnectorScope;
        public Type ConnectorType = Type.OR;
        public string UnknownConnectorType;

        public void Validate(ValidatorProbe probe)
        {

            //TODO CHECK FOR MISSING / WRONG CLAUSE VALUES!

            foreach (TriggerItem ti in Contents)
            {
                if (ti is TriggerConnector)
                {
                    TriggerConnector tc = (TriggerConnector)ti;
                    tc.Validate(probe);
                }
                else
                {
                    Trigger t = (Trigger)ti;
                    //WRONG SCOPE
                    switch (TriggerVariable.GetScopes(t.VariableChecked))
                    {
                        case Scope.Clause:
                        case Scope.AnyTrigger:
                        case Scope.Anywhere:
                        case Scope.CountryORAnywhere:
                            break;
                        case Scope.Country:
                            if (ConnectorScope != Scope.Country)
                                probe.Report(ValidatorProbe.LogLevel.Error, "Wrong scope!", FileName, LineNumber);
                                break;
                        case Scope.CountryORProvince:
                            if (ConnectorScope != Scope.Country && ConnectorScope != Scope.Province && ConnectorScope != Scope.TradeNode)
                                probe.Report(ValidatorProbe.LogLevel.Error, "Wrong scope!", FileName, LineNumber);
                            break;
                        case Scope.Invalid:
                            probe.Report(ValidatorProbe.LogLevel.Error, "Wrong scope!", FileName, LineNumber);
                            break;
                        case Scope.Mission:
                            if (ConnectorScope != Scope.Mission)
                                probe.Report(ValidatorProbe.LogLevel.Error, "Wrong scope!", FileName, LineNumber);
                            break;
                        case Scope.Province:
                            if (ConnectorScope != Scope.Province && ConnectorScope != Scope.TradeNode)
                                probe.Report(ValidatorProbe.LogLevel.Error, "Wrong scope!", FileName, LineNumber);
                            break;
                        case Scope.State:
                            if (ConnectorScope != Scope.State)
                                probe.Report(ValidatorProbe.LogLevel.Error, "Wrong scope!", FileName, LineNumber);
                            break;
                        case Scope.TradeNode:
                            if (ConnectorScope != Scope.TradeNode && ConnectorScope != Scope.Province)
                                probe.Report(ValidatorProbe.LogLevel.Error, "Wrong scope!", FileName, LineNumber);
                            break;
                        case Scope.Unit:
                            if (ConnectorScope != Scope.Unit)
                                probe.Report(ValidatorProbe.LogLevel.Error, "Wrong scope!", FileName, LineNumber);
                            break;
                    }



                    //WRONG VALUE
                    
                    
                }
            }
        }

        public bool CheckIfInt(string input)
        {
            if (int.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
            {
                return true;
            }
            return false;
        }

        public bool CheckIfFloat(string input)
        {
            if (double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
            {
                return true;
            }
            return false;
        }

        public bool CheckIfString(string input)
        {
            if(!CheckIfInt(input) && !CheckIfFloat(input))
                return true;
            return false;
        }

        public bool CheckIfBool(string input)
        {
            if (input.ToLower() == "yes" || input.ToLower() == "no")
                return true;
            return false;

        }

        public bool CheckIfScope(string input)
        {
            string toLower = input.ToLower();
            if (toLower == "from" || toLower == "this" || toLower == "prev" || toLower == "root")
                return true;
            return false;
        }

        public bool CheckIfCanBeTag(string input)
        {
            if (input.Length == 3)
                return true;
            return false;
        }

        public bool CheckIfDate(string input)
        {
            string[] parts = input.Split('.');
            if (parts.Length != 3)
                return false;
            if (!int.TryParse(parts[0], out _))
                return false;
            if (!int.TryParse(parts[1], out _))
                return false;
            if (!int.TryParse(parts[2], out _))
                return false;
            return true;

        }

        public string CreateExpectedValueString(bool Int = false, bool Float = false, bool String = false, bool Scope = false, bool Tag = false, bool Bool = false, bool Date = false)
        {
            string ExpectedValues = "";
            bool moreThanOne = false;
            if (Int)
            {
                ExpectedValues += "INTEGER";
                moreThanOne = true;
            }
            if (Float)
            {
                if (moreThanOne)
                    ExpectedValues += "/";
                ExpectedValues += "FLOAT";
                moreThanOne = true;
            }
            if (String)
            {
                if (moreThanOne)
                    ExpectedValues += "/";
                ExpectedValues += "STRING";
                moreThanOne = true;
            }
            if (Bool)
            {
                if (moreThanOne)
                    ExpectedValues += "/";
                ExpectedValues += "BOOL";
                moreThanOne = true;
            }
            if (Scope)
            {
                if (moreThanOne)
                    ExpectedValues += "/";
                ExpectedValues += "SCOPE";
                moreThanOne = true;
            }
            if (Tag)
            {
                if (moreThanOne)
                    ExpectedValues += "/";
                ExpectedValues += "TAG";
                moreThanOne = true;
            }
            if (Date)
            {
                if (moreThanOne)
                    ExpectedValues += "/";
                ExpectedValues += "DATE";
                moreThanOne = true;
            }
            return ExpectedValues;
        }

        public string ValueRecognised(string input)
        {
            if (CheckIfInt(input))
                return "INTEGER";
            else if (CheckIfFloat(input))
                return "FLOAT";
            else if (CheckIfScope(input))
                return "SCOPE";
            else if (CheckIfCanBeTag(input))
                return "TAG";
            else if (CheckIfBool(input))
                return "BOOL";
            else if (CheckIfDate(input))
                return "DATE";
            else if (input == "")
                return "EMPTY";
            return "STRING";
        }

        public void ExpectingValues(Trigger t, ValidatorProbe probe, bool Int = false, bool Float = false, bool String = false, bool Scope = false, bool Tag = false, bool Bool = false, bool Date = false)
        {
            string input = t.ValueExpected;
            string ExpectedValues = CreateExpectedValueString(Int, Float, String, Scope, Tag);
            if (String && CheckIfString(input))
                ;
            else if (Int && CheckIfInt(input))
                ;
            else if (Float && CheckIfFloat(input))
                ;
            else if (Scope && CheckIfScope(input))
                ;
            else if (Tag && CheckIfCanBeTag(input))
                ;
            else if (Bool && CheckIfBool(input))
                ;
            else if (Date && CheckIfDate(input))
                ;
            else
                probe.Report(ValidatorProbe.LogLevel.Error, "Expected value type is " + ExpectedValues + ", not " + ValueRecognised(input), t.FileName, t.LineNumber);
        }

        public void ValidateExpectedValue(Trigger t, ValidatorProbe probe)
        {
            switch (TriggerVariable.GetValues(t.VariableChecked))
            {
                case Value.AdvisorID:
                case Value.Integer:
                case Value.ProvinceID:
                    //CHECK IF IT IS A NUMBER
                    ExpectingValues(t, probe, Int: true);
                    //TODO CHECK IF IT IS A RECOGNISED NUMBER
                    break;
                case Value.AdvisorIdentifier: //TODO READ THOSE
                case Value.AgeAbilityIdentifier: //TODO READ THOSE
                case Value.AgeIdentifier: //TODO READ THOSE
                case Value.AIPersonalityIdentifier: //TODO READ THOSE
                case Value.AreaIdentifier: 
                case Value.BribeIdentifier: //TODO READ THOSE
                case Value.BuildingIdentifier:
                case Value.ChinaReformIdentifier: //TODO READ THOSE
                case Value.ChurchAspectIdentifier: //TODO READ THOSE
                case Value.ClimateIdentifier: 
                case Value.ColonialRegionIdentifier: 
                case Value.ConsortFlagIdentifier: //TODO SEARCH FOR THOSE
                case Value.Construction:
                case Value.CouncilPositionIdentifier: //TODO READ THOSE
                case Value.CountryFlagIdentifier: //TODO SEARCH FOR THOSE
                case Value.CultIdentifier: //TODO SEARCH FOR THOSE
                case Value.CultureIdentifierORCapital: 
                case Value.DebateIdentifier: //TODO READ THOSE
                case Value.DisasterIdentfier: //TODO READ THOSE
                case Value.DLCIdentifier: //TODO ADD STATIC
                case Value.EstateIdentifier: //TODO READ THOSE
                case Value.EventTargetIdentifier: //TODO READ THOSE
                case Value.FactionIdentifier: //TODO READ THOSE
                case Value.GlobalFlagIdentifier: //TODO SEARCH FOR THOSE
                case Value.GovernmentAttributeIdentifier: //TODO READ THOSE
                case Value.GovernmentIdentifier: 
                case Value.GovernmentMechanicIdentfier:
                case Value.GovernmentPowerIdentifier:
                case Value.HegemonTypeIdentifier:
                case Value.HeirFlagIdentifier:
                case Value.HolyOrderIdentifier:
                case Value.HREReformIdentifier:
                case Value.IconIdentifier:
                case Value.IdeaGroupIdentifier:
                case Value.IdeaIdentifier:
                case Value.IncidentIdentifier:
                case Value.InstitutionIdentifier:
                case Value.LeagueIdentifier:
                case Value.MissionIdentifier:
                case Value.ModifierIdentifier:
                case Value.MonsoonIdentifier:
                case Value.NationalFocusIdentifier:
                case Value.NativePolicyIdentifier:
                case Value.PersonalDietyIdentifier:
                case Value.PersonalityIdentifier:
                case Value.PolicyIdentfier:
                case Value.PrivilegeIdentifier:
                case Value.ProvinceFlagIdentifier:
                case Value.ProvinceGroupIdentifier:
                case Value.RebelIdentifier:
                case Value.ReformIdentifier:
                case Value.RegionIdentifier:
                case Value.RulerFlagIdentifier:
                case Value.ReligionIdentifier:
                case Value.ReligionIdentifierORCapital:
                case Value.String:
                case Value.StateEdictIdentifier:
                case Value.SubjectIdentifier:
                case Value.SuperregionIdentifier:
                case Value.TerrainIdentifier:
                case Value.TradeCompanyIdentifier:
                case Value.UnitTypeIdentifier:
                case Value.WinterIdentifier:
                    ExpectingValues(t, probe, String: true);
                    break;
                case Value.Boolean:
                    ExpectingValues(t, probe, Bool: true);
                    break;
                case Value.BooleanOREstateIdentifier:
                    ExpectingValues(t, probe, Bool: true, String:true);
                    break;
                case Value.BooleanORTagORScope:
                    ExpectingValues(t, probe, Bool: true, Scope: true, Tag: true);
                    break;

                case Value.ContinentIdetifierORTagORScopeORProvinceID:
                    ExpectingValues(t, probe, String: true, Tag: true, Scope: true, Int: true);
                    break;

                case Value.CultureIdentifierORScope:
                case Value.ReligionGroupORScope:
                case Value.ReligionIdentifierORScope:
                case Value.ReligionORReligionGroupORScope:
                case Value.TechnologyGroupIdentifierORScope:
                    ExpectingValues(t, probe, String: true, Scope: true);
                    break;

                case Value.Date:
                    ExpectingValues(t, probe, Date: true);
                    break;

                case Value.Float:
                    ExpectingValues(t, probe, Float: true);
                    break;

                case Value.FloatORTagORScope:
                    ExpectingValues(t, probe, Float: true, Tag:true, Scope: true);
                    break;

                case Value.IdentifierORProvinceScope:
                    ExpectingValues(t, probe, String: true, Int: true);
                    break;

                case Value.IncidentIdentifierORBooleanORAnyORNone:
                case Value.StringORBoolean:
                    ExpectingValues(t, probe, String: true, Bool: true);
                        break;

                case Value.IntegerOREstateIdentifier:
                    ExpectingValues(t, probe, Int: true, String: true);
                    break;

                case Value.IntegerORScope:
                case Value.ProvinceIDORScope:
                case Value.TradeNodeProvince:
                    ExpectingValues(t, probe, Int: true, Scope: true);
                    break;

                case Value.IntegerORTagORScope:
                case Value.ProvinceIDORTagORScope:
                    ExpectingValues(t, probe, Int: true, Scope: true, Tag: true);
                    break;

                case Value.Scope:
                    ExpectingValues(t, probe, Scope: true);
                    break;
                case Value.StringORTagORScope:
                    ExpectingValues(t, probe, String: true, Tag:true, Scope: true);
                    break;
                case Value.Tag:
                    ExpectingValues(t, probe, Tag: true);
                    break;
                case Value.TagORBoolean:
                    ExpectingValues(t, probe, Tag: true, Bool: true);
                    break;
                case Value.TagORScope:
                    ExpectingValues(t, probe, Tag: true, Scope: true);
                    break;




            }
        }


        //STATIC VALUE ARRAYS
        public static readonly string[] AIPersonalities = new string[] { "human", "ai_capitalist", "ai_diplomat", "ai_militarist", "ai_colonialist", "ai_balanced" };



        public bool ValidateValue(Trigger t, ValidatorProbe probe)
        {           
            switch (TriggerVariable.GetValues(t.VariableChecked))
            {
                case Value.AdvisorID:
                    //TODO CHECK ADVISOR                   
                    break;
                case Value.AIPersonalityIdentifier:
                    if (!AIPersonalities.Contains(t.ValueExpected.ToLower()))
                        probe.Report(ValidatorProbe.LogLevel.Error, $"Value '{t.ValueExpected}' is incorrect for '{t.VariableChecked}' trigger.", t.FileName, t.LineNumber);
                    break;
                case Value.AreaIdentifier:
                    if(!Area.IsValid(t.ValueExpected))
                        probe.Report(ValidatorProbe.LogLevel.Alert, $"Area '{t.ValueExpected}' in a trigger doesn't exist anywhere in the files.", t.FileName, t.LineNumber);
                    break;
                case Value.Boolean:
                    if (t.ValueExpected != "yes" && t.ValueExpected != "no")
                        probe.Report(ValidatorProbe.LogLevel.Alert, $"Value '{t.ValueExpected}' is incorrect for '{t.VariableChecked}' trigger.", t.FileName, t.LineNumber);
                    break;
            }
            return false;
        }

        public override string ToString()
        {
            return ToString(0);
        }

        public TriggerVariable.TriggerVar[] GetAllCheckedVariables()
        {
            List<TriggerVariable.TriggerVar> VariablesInside = new List<TriggerVariable.TriggerVar>();
            foreach (TriggerItem ti in Contents)
            {
                if (ti is TriggerConnector)
                {
                    TriggerConnector tc = (TriggerConnector)ti;
                    VariablesInside.AddRange(tc.GetAllCheckedVariables());
                }
                else
                {
                    Trigger t = (Trigger)ti;
                    VariablesInside.Add(t.VariableChecked);
                    Console.WriteLine(t.VariableChecked);
                }
            }
            return VariablesInside.ToArray();
        }

        public string ToString(int indentation = 0)
        {
            string ret = "";
            for (int a = 0; a < indentation; a++)
                ret += "\t";

            if(ConnectorType != Type.UNKNOWN_TYPE)
            {
                if(ConnectorType == Type.AND || ConnectorType == Type.OR || ConnectorType == Type.NOT)
                    ret += ConnectorType.ToString();
                else
                    ret += ConnectorType.ToString().ToLower();
            }
                
            else
                ret += UnknownConnectorType;

            ret += " = {";

            foreach (TriggerItem ti in Contents)
            {
                ret += "\n";
                if (ti is TriggerConnector)
                {
                    TriggerConnector tc = (TriggerConnector)ti;
                    ret += tc.ToString(indentation + 1);
                }
                else
                {
                    Trigger t = (Trigger)ti;
                    for (int a = 0; a < indentation + 1; a++)
                        ret += "\t";
                    if(t.VariableChecked != TriggerVariable.TriggerVar.UnknownTrigger)
                        ret += t.VariableChecked + " = " + t.ValueExpected;
                    else
                        ret += t.UnknownVariableChecked + " = " + t.ValueExpected;
                }
            }
            ret += "\n";
            for (int a = 0; a < indentation; a++)
                ret += "\t";
            ret += "}";
            return ret;
        }

        public static TriggerConnector GetTriggerConnectorFromNode(Node input, Scope previous, Scope Root = Scope.Invalid, Scope From = Scope.Invalid, Scope Prev = Scope.Invalid, Scope This = Scope.Invalid, Scope EventTarget = Scope.Invalid)
        {
            if (input == null)
                return null;
            TriggerConnector TC = new TriggerConnector();

            TC.ConnectorType = GetTypeByName(input.Name.ToLower());
            if (TC.ConnectorType == Type.UNKNOWN_TYPE)
                TC.UnknownConnectorType = input.Name;

            TC.ConnectorScope = ScopesTo(TC.ConnectorType, previous, Root, From, Prev, This, EventTarget);

            foreach (NodeItem ni in input.ItemOrder)
            {
                if (ni is Node)
                {
                    Node node = (Node)ni;
                    TriggerConnector smallTC = GetTriggerConnectorFromNode(node, TC.ConnectorScope);
                    smallTC.Parent = TC;
                    smallTC.LineNumber = ni.LineNumber;
                    TC.Contents.Add(smallTC);
                }
                else if (ni is Variable)
                {
                    Variable variable = (Variable)ni;
                    Trigger tg = new Trigger(variable.Name.ToLower(), variable.Value) { Parent = TC };
                    tg.LineNumber = ni.LineNumber;
                    TC.Contents.Add(tg);
                }
            }

            TC.Contents = TC.Contents.OrderBy(x => x is Trigger ? 0 : 1).ToList();

            return TC;
        }

        public static Type GetTypeByName(string Name)
        {
            return GetTypeByName(Name, null , null, null, null, null, null, null, null, null);
        }
        public static Type GetTypeByName(string Name, Country[] tags, Province[] provinces, Area[] areas, Region[] regions, Superregion[] superregions, string[] provincegroups, Continent[] continents, TradeCompany[] tradecompanies, string[] ColonialRegions)
        {
            switch (Name)
            {
                case "trigger":
                    return Type.TRIGGER;
                case "or":
                    return Type.OR;
                case "and":
                    return Type.AND;
                case "not":
                    return Type.NOT;
                case "root":
                    return Type.ROOT;
                case "from":
                    return Type.FROM;
                case "prev":
                    return Type.PREV;
                case "this":
                    return Type.THIS;
                case "province_id":
                    return Type.PROVINCE_ID;
                case "tag_name":
                    return Type.TAG_NAME;
                case "area_name":
                    return Type.AREA_NAME;
                case "region_name":
                    return Type.REGION_NAME;
                case "superregion_name":
                    return Type.SUPERREGION_NAME;
                case "provincegroup_name":
                    return Type.PROVINCEGROUP_NAME;
                case "continent_name":
                    return Type.CONTINENT_NAME;
                case "trade_company_name":
                    return Type.TRADE_COMPANY_NAME;
                case "colonial_region_name":
                    return Type.COLONIAL_REGION_NAME;
                case "event_traget":
                    return Type.EVENT_TRAGET;
                case "emperor":
                    return Type.EMPEROR;
                case "revolultion_target":
                    return Type.REVOLULTION_TARGET;
                case "crusade_target":
                    return Type.CRUSADE_TARGET;
                case "owner":
                    return Type.OWNER;
                case "controller":
                    return Type.CONTROLLER;
                case "sea_zone":
                    return Type.SEA_ZONE;
                case "colonial_parent":
                    return Type.COLONIAL_PARENT;
                case "overlord":
                    return Type.OVERLORD;
                case "capital_scope":
                    return Type.CAPITAL_SCOPE;
                case "most_province_trade_power":
                    return Type.MOST_PROVINCE_TRADE_POWER;
                case "strongest_trade_power":
                    return Type.STRONGEST_TRADE_POWER;
                case "every_ally":
                    return Type.EVERY_ALLY;
                case "every_coalition_member":
                    return Type.EVERY_COALITION_MEMBER;
                case "every_country":
                    return Type.EVERY_COUNTRY;
                case "every_country_including_inactive":
                    return Type.EVERY_COUNTRY_INCLUDING_INACTIVE;
                case "every_elector":
                    return Type.EVERY_ELECTOR;
                case "every_enemy_country":
                    return Type.EVERY_ENEMY_COUNTRY;
                case "every_known_country":
                    return Type.EVERY_KNOWN_COUNTRY;
                case "every_local_enemy":
                    return Type.EVERY_LOCAL_ENEMY;
                case "every_neighbor_country":
                    return Type.EVERY_NEIGHBOR_COUNTRY;
                case "every_rival_country":
                    return Type.EVERY_RIVAL_COUNTRY;
                case "every_subject_country":
                    return Type.EVERY_SUBJECT_COUNTRY;
                case "every_war_enemy_country":
                    return Type.EVERY_WAR_ENEMY_COUNTRY;
                case "random_ally":
                    return Type.RANDOM_ALLY;
                case "random_coalition_member":
                    return Type.RANDOM_COALITION_MEMBER;
                case "random_country":
                    return Type.RANDOM_COUNTRY;
                case "random_elector":
                    return Type.RANDOM_ELECTOR;
                case "random_enemy_country":
                    return Type.RANDOM_ENEMY_COUNTRY;
                case "random_hired_mercenary_company":
                    return Type.RANDOM_HIRED_MERCENARY_COMPANY;
                case "random_known_country":
                    return Type.RANDOM_KNOWN_COUNTRY;
                case "random_local_enemy":
                    return Type.RANDOM_LOCAL_ENEMY;
                case "random_neighbor_country":
                    return Type.RANDOM_NEIGHBOR_COUNTRY;
                case "random_rival_country":
                    return Type.RANDOM_RIVAL_COUNTRY;
                case "random_subject_country":
                    return Type.RANDOM_SUBJECT_COUNTRY;
                case "random_war_enemy_country":
                    return Type.RANDOM_WAR_ENEMY_COUNTRY;
                case "every_core_province":
                    return Type.EVERY_CORE_PROVINCE;
                case "every_heretic_province":
                    return Type.EVERY_HERETIC_PROVINCE;
                case "every_owned_province":
                    return Type.EVERY_OWNED_PROVINCE;
                case "every_province":
                    return Type.EVERY_PROVINCE;
                case "random_core_province":
                    return Type.RANDOM_CORE_PROVINCE;
                case "random_heretic_province":
                    return Type.RANDOM_HERETIC_PROVINCE;
                case "random_owned_area":
                    return Type.RANDOM_OWNED_AREA;
                case "random_owned_province":
                    return Type.RANDOM_OWNED_PROVINCE;
                case "random_active_trade_node":
                    return Type.RANDOM_ACTIVE_TRADE_NODE;
                case "random_trade_node":
                    return Type.RANDOM_TRADE_NODE;
                case "home_trade_node_effect_scope":
                    return Type.HOME_TRADE_NODE_EFFECT_SCOPE;
                case "every_empty_neighbor_province":
                    return Type.EVERY_EMPTY_NEIGHBOR_PROVINCE;
                case "every_neighbor_province":
                    return Type.EVERY_NEIGHBOR_PROVINCE;
                case "every_province_in_state":
                    return Type.EVERY_PROVINCE_IN_STATE;
                case "random_empty_neighbor_province":
                    return Type.RANDOM_EMPTY_NEIGHBOR_PROVINCE;
                case "random_neighbor_province":
                    return Type.RANDOM_NEIGHBOR_PROVINCE;
                case "random_province_in_state":
                    return Type.RANDOM_PROVINCE_IN_STATE;
                case "random_province":
                    return Type.RANDOM_PROVINCE;
                case "every_core_country":
                    return Type.EVERY_CORE_COUNTRY;
                case "random_core_country":
                    return Type.RANDOM_CORE_COUNTRY;
                case "area":
                    return Type.AREA;
                case "region":
                    return Type.REGION;
                case "every_privateering_country":
                    return Type.EVERY_PRIVATEERING_COUNTRY;
                case "random_privateering_country":
                    return Type.RANDOM_PRIVATEERING_COUNTRY;
                case "every_trade_node_member_country":
                    return Type.EVERY_TRADE_NODE_MEMBER_COUNTRY;
                case "random_trade_node_member_country":
                    return Type.RANDOM_TRADE_NODE_MEMBER_COUNTRY;
                case "every_trade_node_member_province":
                    return Type.EVERY_TRADE_NODE_MEMBER_PROVINCE;
                case "random_trade_node_member_province":
                    return Type.RANDOM_TRADE_NODE_MEMBER_PROVINCE;
                case "unit_owner":
                    return Type.UNIT_OWNER;
                case "enemy_unit":
                    return Type.ENEMY_UNIT;
                case "location":
                    return Type.LOCATION;
                case "every_target_province":
                    return Type.EVERY_TARGET_PROVINCE;
                case "random_target_province":
                    return Type.RANDOM_TARGET_PROVINCE;
                case "any_army":
                    return Type.ANY_ARMY;
                case "all_ally":
                    return Type.ALL_ALLY;
                case "all_coalition_member":
                    return Type.ALL_COALITION_MEMBER;
                case "all_country":
                    return Type.ALL_COUNTRY;
                case "all_countries_including_self":
                    return Type.ALL_COUNTRIES_INCLUDING_SELF;
                case "all_elector":
                    return Type.ALL_ELECTOR;
                case "all_enemy_country":
                    return Type.ALL_ENEMY_COUNTRY;
                case "all_known_country":
                    return Type.ALL_KNOWN_COUNTRY;
                case "all_local_enemy":
                    return Type.ALL_LOCAL_ENEMY;
                case "all_neighbor_country":
                    return Type.ALL_NEIGHBOR_COUNTRY;
                case "all_rival_country":
                    return Type.ALL_RIVAL_COUNTRY;
                case "all_subject_country":
                    return Type.ALL_SUBJECT_COUNTRY;
                case "all_war_enemy_countries":
                    return Type.ALL_WAR_ENEMY_COUNTRIES;
                case "all_core_province":
                    return Type.ALL_CORE_PROVINCE;
                case "all_heretic_province":
                    return Type.ALL_HERETIC_PROVINCE;
                case "all_owned_province":
                    return Type.ALL_OWNED_PROVINCE;
                case "all_province":
                    return Type.ALL_PROVINCE;
                case "all_state_province":
                    return Type.ALL_STATE_PROVINCE;
                case "all_states":
                    return Type.ALL_STATES;
                case "all_active_trade_node":
                    return Type.ALL_ACTIVE_TRADE_NODE;
                case "all_trade_node":
                    return Type.ALL_TRADE_NODE;
                case "any_ally":
                    return Type.ANY_ALLY;
                case "any_coalition_member":
                    return Type.ANY_COALITION_MEMBER;
                case "any_country":
                    return Type.ANY_COUNTRY;
                case "any_elector":
                    return Type.ANY_ELECTOR;
                case "any_enemy_country":
                    return Type.ANY_ENEMY_COUNTRY;
                case "any_known_country":
                    return Type.ANY_KNOWN_COUNTRY;
                case "any_local_enemy":
                    return Type.ANY_LOCAL_ENEMY;
                case "any_neighbor_country":
                    return Type.ANY_NEIGHBOR_COUNTRY;
                case "any_rival_country":
                    return Type.ANY_RIVAL_COUNTRY;
                case "any_subject_country":
                    return Type.ANY_SUBJECT_COUNTRY;
                case "any_war_enemy_country":
                    return Type.ANY_WAR_ENEMY_COUNTRY;
                case "any_core_province":
                    return Type.ANY_CORE_PROVINCE;
                case "any_heretic_province":
                    return Type.ANY_HERETIC_PROVINCE;
                case "any_owned_province":
                    return Type.ANY_OWNED_PROVINCE;
                case "any_hired_mercenary_company":
                    return Type.ANY_HIRED_MERCENARY_COMPANY;
                case "any_province":
                    return Type.ANY_PROVINCE;
                case "any_state":
                    return Type.ANY_STATE;
                case "any_active_trade_node":
                    return Type.ANY_ACTIVE_TRADE_NODE;
                case "any_trade_node":
                    return Type.ANY_TRADE_NODE;
                case "home_trade_node":
                    return Type.HOME_TRADE_NODE;
                case "any_great_power":
                    return Type.ANY_GREAT_POWER;
                case "any_other_great_power":
                    return Type.ANY_OTHER_GREAT_POWER;
                case "all_empty_neighbor_province":
                    return Type.ALL_EMPTY_NEIGHBOR_PROVINCE;
                case "all_province_in_state":
                    return Type.ALL_PROVINCE_IN_STATE;
                case "all_neighbor_province":
                    return Type.ALL_NEIGHBOR_PROVINCE;
                case "any_empty_neighbor_province":
                    return Type.ANY_EMPTY_NEIGHBOR_PROVINCE;
                case "any_province_in_state":
                    return Type.ANY_PROVINCE_IN_STATE;
                case "any_friendly_coast_border_province":
                    return Type.ANY_FRIENDLY_COAST_BORDER_PROVINCE;
                case "any_neighbor_province":
                    return Type.ANY_NEIGHBOR_PROVINCE;
                case "all_core_country":
                    return Type.ALL_CORE_COUNTRY;
                case "any_core_country":
                    return Type.ANY_CORE_COUNTRY;
                case "area_for_scope_province":
                    return Type.AREA_FOR_SCOPE_PROVINCE;
                case "region_for_scope_province":
                    return Type.REGION_FOR_SCOPE_PROVINCE;
                case "all_privateering_country":
                    return Type.ALL_PRIVATEERING_COUNTRY;
                case "all_trade_node_member_country":
                    return Type.ALL_TRADE_NODE_MEMBER_COUNTRY;
                case "any_privateering_country":
                    return Type.ANY_PRIVATEERING_COUNTRY;
                case "any_trade_node_member_country":
                    return Type.ANY_TRADE_NODE_MEMBER_COUNTRY;
                case "all_trade_node_member_province":
                    return Type.ALL_TRADE_NODE_MEMBER_PROVINCE;
                case "any_trade_node_member_province":
                    return Type.ANY_TRADE_NODE_MEMBER_PROVINCE;
                case "all_target_province":
                    return Type.ALL_TARGET_PROVINCE;
                case "any_target_province":
                    return Type.ANY_TARGET_PROVINCE;
                case "ai_attitude":
                    return Type.AI_ATTITUDE;
                case "army_strength":
                    return Type.ARMY_STRENGTH;
                case "border_distance":
                    return Type.BORDER_DISTANCE;
                case "calc_true_if":
                    return Type.CALC_TRUE_IF;
                case "can_use_peace_treaty":
                    return Type.CAN_USE_PEACE_TREATY;
                case "capital_distance":
                    return Type.CAPITAL_DISTANCE;
                case "check_variable":
                    return Type.CHECK_VARIABLE;
                case "custom_trigger_tooltip":
                    return Type.CUSTOM_TRIGGER_TOOLTIP;
                case "development_in_provinces":
                    return Type.DEVELOPMENT_IN_PROVINCES;
                case "employed_advisor":
                    return Type.EMPLOYED_ADVISOR;
                case "estate_influence":
                    return Type.ESTATE_INFLUENCE;
                case "estate_loyalty":
                    return Type.ESTATE_LOYALTY;
                case "estate_territory":
                    return Type.ESTATE_TERRITORY;
                case "faction_influence":
                    return Type.FACTION_INFLUENCE;
                case "had_active_policy":
                    return Type.HAD_ACTIVE_POLICY;
                case "had_consort_flag":
                    return Type.HAD_CONSORT_FLAG;
                case "had_country_flag":
                    return Type.HAD_COUNTRY_FLAG;
                case "had_global_flag":
                    return Type.HAD_GLOBAL_FLAG;
                case "had_heir_flag":
                    return Type.HAD_HEIR_FLAG;
                case "had_province_flag":
                    return Type.HAD_PROVINCE_FLAG;
                case "had_ruler_flag":
                    return Type.HAD_RULER_FLAG;
                case "has_casus_belli":
                    return Type.HAS_CASUS_BELLI;
                case "has_disaster_progress":
                    return Type.HAS_DISASTER_PROGRESS;
                case "has_estate_influence_modifier":
                    return Type.HAS_ESTATE_INFLUENCE_MODIFIER;
                case "has_estate_loyalty_modifier":
                    return Type.HAS_ESTATE_LOYALTY_MODIFIER;
                case "has_great_project":
                    return Type.HAS_GREAT_PROJECT;
                case "has_global_modifier_value":
                    return Type.HAS_GLOBAL_MODIFIER_VALUE;
                case "has_leader_with":
                    return Type.HAS_LEADER_WITH;
                case "has_local_modifier_value":
                    return Type.HAS_LOCAL_MODIFIER_VALUE;
                case "has_opinion":
                    return Type.HAS_OPINION;
                case "has_opinion_diff":
                    return Type.HAS_OPINION_DIFF;
                case "has_opinion_modifier":
                    return Type.HAS_OPINION_MODIFIER;
                case "has_privateer_share_in_trade_node":
                    return Type.HAS_PRIVATEER_SHARE_IN_TRADE_NODE;
                case "has_spy_network_from":
                    return Type.HAS_SPY_NETWORK_FROM;
                case "has_spy_network_in":
                    return Type.HAS_SPY_NETWORK_IN;
                case "has_trade_company_investement_in_area":
                    return Type.HAS_TRADE_COMPANY_INVESTEMENT_IN_AREA;
                case "has_trade_modifier":
                    return Type.HAS_TRADE_MODIFIER;
                case "has_won_war_against":
                    return Type.HAS_WON_WAR_AGAINST;
                case "hidden_trigger":
                    return Type.HIDDEN_TRIGGER;
                case "incident_variable_value":
                    return Type.INCIDENT_VARIABLE_VALUE;
                case "institution_difference":
                    return Type.INSTITUTION_DIFFERENCE;
                case "is_in_war":
                    return Type.IS_IN_WAR;
                case "is_subject_of_type_with_overlord":
                    return Type.IS_SUBJECT_OF_TYPE_WITH_OVERLORD;
                case "is_variable_equal":
                    return Type.IS_VARIABLE_EQUAL;
                case "military_strength":
                    return Type.MILITARY_STRENGTH;
                case "naval_strength":
                    return Type.NAVAL_STRENGTH;
                case "num_investements_in_trade_company_region":
                    return Type.NUM_INVESTEMENTS_IN_TRADE_COMPANY_REGION;
                case "num_of_estate_privileges":
                    return Type.NUM_OF_ESTATE_PRIVILEGES;
                case "num_of_owned_provinces_with":
                    return Type.NUM_OF_OWNED_PROVINCES_WITH;
                case "num_of_provinces_owned_or_owned_by_non_sovereign_subjects_with":
                    return Type.NUM_OF_PROVINCES_OWNED_OR_OWNED_BY_NON_SOVEREIGN_SUBJECTS_WITH;
                case "num_of_provinces_owned_or_owned_by__subjects_with":
                    return Type.NUM_OF_PROVINCES_OWNED_OR_OWNED_BY__SUBJECTS_WITH;
                case "num_of_religion":
                    return Type.NUM_OF_RELIGION;
                case "num_of_units_in_province":
                    return Type.NUM_OF_UNITS_IN_PROVINCE;
                case "owes_favors":
                    return Type.OWES_FAVORS;
                case "owns_all_provinces":
                    return Type.OWNS_ALL_PROVINCES;
                case "privateer_power":
                    return Type.PRIVATEER_POWER;
                case "production_leader":
                    return Type.PRODUCTION_LEADER;
                case "province_distance":
                    return Type.PROVINCE_DISTANCE;
                case "religion_years":
                    return Type.RELIGION_YEARS;
                case "religious_school":
                    return Type.RELIGIOUS_SCHOOL;
                case "reverse_has_opinion":
                    return Type.REVERSE_HAS_OPINION;
                case "reverse_has_opinion_modifier":
                    return Type.REVERSE_HAS_OPINION_MODIFIER;
                case "total_losses_in_won_wars":
                    return Type.TOTAL_LOSSES_IN_WON_WARS;
                case "trade_goods_produced_amount":
                    return Type.TRADE_GOODS_PRODUCED_AMOUNT;
                case "trade_share":
                    return Type.TRADE_SHARE;
                case "trading_bonus":
                    return Type.TRADING_BONUS;
                case "trading_part":
                    return Type.TRADING_PART;
                case "trading_policy_in_node":
                    return Type.TRADING_POLICY_IN_NODE;
                case "variable_arithmetic_trigger":
                    return Type.VARIABLE_ARITHMETIC_TRIGGER;
                case "war_score_against":
                    return Type.WAR_SCORE_AGAINST;
                case "years_in_union_under":
                    return Type.YEARS_IN_UNION_UNDER;
                case "years_in_vassalage_under":
                    return Type.YEARS_IN_VASSALAGE_UNDER;

            }

            if(tags != null && tags.Any())
            {
                if (tags.FirstOrDefault(x => x.Tag == Name) != null)
                    return Type.TAG_NAME;
            }
            if (provinces != null && provinces.Any())
            {
                if (int.TryParse(Name, out int n)) {
                    if (provinces.FirstOrDefault(x => x.ID == n) != null)
                        return Type.PROVINCE_ID;

                    //TODO Province ID called but no suitable province found!
                    return Type.PROVINCE_ID;
                }

            }
            if (areas != null && areas.Any())
            {
                if (areas.FirstOrDefault(x => x.Name == Name) != null)
                    return Type.AREA_NAME;
            }
            if (regions != null && regions.Any())
            {
                if (regions.FirstOrDefault(x => x.Name == Name) != null)
                    return Type.REGION_NAME;
            }
            if (superregions != null && superregions.Any())
            {
                if (superregions.FirstOrDefault(x => x.Name == Name) != null)
                    return Type.SUPERREGION_NAME;
            }
            if (provincegroups != null && provincegroups.Any())
            {
                if (provincegroups.FirstOrDefault(x => x == Name) != null)
                    return Type.PROVINCEGROUP_NAME;
            }
            if (continents != null && continents.Any())
            {
                if (continents.FirstOrDefault(x => x.Name == Name) != null)
                    return Type.CONTINENT_NAME;
            }
            if (tradecompanies != null && tradecompanies.Any())
            {
                if (tradecompanies.FirstOrDefault(x => x.Name == Name) != null)
                    return Type.TRADE_COMPANY_NAME;
            }
            if (ColonialRegions != null && ColonialRegions.Any())
            {
                if (ColonialRegions.FirstOrDefault(x => x == Name) != null)
                    return Type.COLONIAL_REGION_NAME;
            }
            return Type.UNKNOWN_TYPE;
        }

        public static Scope CanBeUsedIn(Type type)
        {
            switch (type)
            {
                default:
                    return Scope.Anywhere;
                case Type.AI_ATTITUDE:
                case Type.ARMY_STRENGTH:
                case Type.BORDER_DISTANCE:
                case Type.CALC_TRUE_IF:
                case Type.CAN_USE_PEACE_TREATY:
                case Type.CAPITAL_DISTANCE:
                case Type.CHECK_VARIABLE:
                case Type.CUSTOM_TRIGGER_TOOLTIP:
                case Type.DEVELOPMENT_IN_PROVINCES:
                case Type.EMPLOYED_ADVISOR:
                case Type.ESTATE_INFLUENCE:
                case Type.ESTATE_LOYALTY:
                case Type.ESTATE_TERRITORY:
                case Type.FACTION_INFLUENCE:
                case Type.HAD_ACTIVE_POLICY:
                case Type.HAD_CONSORT_FLAG:
                case Type.HAD_COUNTRY_FLAG:
                case Type.HAD_GLOBAL_FLAG:
                case Type.HAD_HEIR_FLAG:
                case Type.HAD_PROVINCE_FLAG:
                case Type.HAD_RULER_FLAG:
                case Type.HAS_CASUS_BELLI:
                case Type.HAS_DISASTER_PROGRESS:
                case Type.HAS_ESTATE_INFLUENCE_MODIFIER:
                case Type.HAS_ESTATE_LOYALTY_MODIFIER:
                case Type.HAS_GREAT_PROJECT:
                case Type.HAS_GLOBAL_MODIFIER_VALUE:
                case Type.HAS_LEADER_WITH:
                case Type.HAS_LOCAL_MODIFIER_VALUE:
                case Type.HAS_OPINION:
                case Type.HAS_OPINION_DIFF:
                case Type.HAS_OPINION_MODIFIER:
                case Type.HAS_PRIVATEER_SHARE_IN_TRADE_NODE:
                case Type.HAS_SPY_NETWORK_FROM:
                case Type.HAS_SPY_NETWORK_IN:
                case Type.HAS_TRADE_COMPANY_INVESTEMENT_IN_AREA:
                case Type.HAS_TRADE_MODIFIER:
                case Type.HAS_WON_WAR_AGAINST:
                case Type.HIDDEN_TRIGGER:
                case Type.INCIDENT_VARIABLE_VALUE:
                case Type.INSTITUTION_DIFFERENCE:
                case Type.IS_IN_WAR:
                case Type.IS_SUBJECT_OF_TYPE_WITH_OVERLORD:
                case Type.IS_VARIABLE_EQUAL:
                case Type.MILITARY_STRENGTH:
                case Type.NAVAL_STRENGTH:
                case Type.NUM_INVESTEMENTS_IN_TRADE_COMPANY_REGION:
                case Type.NUM_OF_ESTATE_PRIVILEGES:
                case Type.NUM_OF_OWNED_PROVINCES_WITH:
                case Type.NUM_OF_PROVINCES_OWNED_OR_OWNED_BY_NON_SOVEREIGN_SUBJECTS_WITH:
                case Type.NUM_OF_PROVINCES_OWNED_OR_OWNED_BY__SUBJECTS_WITH:
                case Type.NUM_OF_RELIGION:
                case Type.NUM_OF_UNITS_IN_PROVINCE:
                case Type.OWES_FAVORS:
                case Type.OWNS_ALL_PROVINCES:
                case Type.PRIVATEER_POWER:
                case Type.PRODUCTION_LEADER:
                case Type.PROVINCE_DISTANCE:
                case Type.RELIGION_YEARS:
                case Type.RELIGIOUS_SCHOOL:
                case Type.REVERSE_HAS_OPINION:
                case Type.REVERSE_HAS_OPINION_MODIFIER:
                case Type.TOTAL_LOSSES_IN_WON_WARS:
                case Type.TRADE_GOODS_PRODUCED_AMOUNT:
                case Type.TRADE_SHARE:
                case Type.TRADING_BONUS:
                case Type.TRADING_PART:
                case Type.TRADING_POLICY_IN_NODE:
                case Type.VARIABLE_ARITHMETIC_TRIGGER:
                case Type.WAR_SCORE_AGAINST:
                case Type.YEARS_IN_UNION_UNDER:
                case Type.YEARS_IN_VASSALAGE_UNDER:
                    return Scope.AnyTrigger;

                case Type.ROOT:
                case Type.FROM:
                case Type.PREV:
                case Type.THIS:
                case Type.EVENT_TRAGET:
                case Type.OR:
                case Type.AND:
                case Type.NOT:
                    return Scope.Anywhere; //SPECIAL CASES

                case Type.ALL_ELECTOR:
                case Type.ALL_COUNTRY:
                case Type.EVERY_COUNTRY:
                case Type.EVERY_ELECTOR:
                case Type.RANDOM_COUNTRY:
                case Type.RANDOM_ELECTOR:
                case Type.EVERY_PROVINCE:
                case Type.RANDOM_TRADE_NODE:
                case Type.RANDOM_PROVINCE:
                case Type.ALL_PROVINCE:
                case Type.ALL_TRADE_NODE:
                case Type.ANY_COUNTRY:
                case Type.ANY_PROVINCE:
                case Type.ANY_TRADE_NODE:
                case Type.ANY_GREAT_POWER:
                    return Scope.Anywhere;

                case Type.TAG_NAME:
                case Type.EMPEROR:
                case Type.REVOLULTION_TARGET:
                case Type.CRUSADE_TARGET:
                case Type.PROVINCE_ID:
                case Type.AREA_NAME:
                case Type.REGION_NAME:
                case Type.SUPERREGION_NAME:
                case Type.PROVINCEGROUP_NAME:
                case Type.CONTINENT_NAME:
                case Type.TRADE_COMPANY_NAME:
                case Type.COLONIAL_REGION_NAME:
                    return Scope.CountryORProvince;

                case Type.COLONIAL_PARENT:
                case Type.OVERLORD:
                case Type.CAPITAL_SCOPE:
                case Type.EVERY_ALLY:
                case Type.EVERY_COALITION_MEMBER:
                case Type.EVERY_COUNTRY_INCLUDING_INACTIVE:
                case Type.EVERY_ENEMY_COUNTRY:
                case Type.EVERY_KNOWN_COUNTRY:
                case Type.EVERY_LOCAL_ENEMY:
                case Type.EVERY_NEIGHBOR_COUNTRY:
                case Type.EVERY_RIVAL_COUNTRY:
                case Type.EVERY_SUBJECT_COUNTRY:
                case Type.EVERY_WAR_ENEMY_COUNTRY:
                case Type.RANDOM_ALLY:
                case Type.RANDOM_COALITION_MEMBER:
                case Type.RANDOM_ENEMY_COUNTRY:
                case Type.RANDOM_HIRED_MERCENARY_COMPANY:
                case Type.RANDOM_KNOWN_COUNTRY:
                case Type.RANDOM_LOCAL_ENEMY: //I THINK?
                case Type.RANDOM_NEIGHBOR_COUNTRY:
                case Type.RANDOM_RIVAL_COUNTRY:
                case Type.RANDOM_SUBJECT_COUNTRY:
                case Type.RANDOM_WAR_ENEMY_COUNTRY:
                case Type.EVERY_CORE_PROVINCE:
                case Type.EVERY_OWNED_PROVINCE:
                case Type.RANDOM_CORE_PROVINCE:
                case Type.RANDOM_HERETIC_PROVINCE:
                case Type.RANDOM_OWNED_AREA:
                case Type.RANDOM_OWNED_PROVINCE:
                case Type.RANDOM_ACTIVE_TRADE_NODE:
                case Type.HOME_TRADE_NODE_EFFECT_SCOPE:
                case Type.ANY_ARMY:
                case Type.ALL_ALLY:
                case Type.ALL_COALITION_MEMBER:
                case Type.ALL_COUNTRIES_INCLUDING_SELF:
                case Type.ALL_ENEMY_COUNTRY:
                case Type.ALL_KNOWN_COUNTRY:
                case Type.ALL_LOCAL_ENEMY:
                case Type.ALL_NEIGHBOR_COUNTRY:
                case Type.ALL_RIVAL_COUNTRY:
                case Type.ALL_SUBJECT_COUNTRY:
                case Type.ALL_WAR_ENEMY_COUNTRIES:
                case Type.ALL_CORE_PROVINCE:
                case Type.ALL_HERETIC_PROVINCE:
                case Type.ALL_OWNED_PROVINCE:
                case Type.ALL_STATE_PROVINCE:
                case Type.ALL_STATES:
                case Type.ALL_ACTIVE_TRADE_NODE:
                case Type.ANY_ALLY:
                case Type.ANY_COALITION_MEMBER:
                case Type.ANY_ELECTOR:
                case Type.ANY_ENEMY_COUNTRY:
                case Type.ANY_KNOWN_COUNTRY:
                case Type.ANY_LOCAL_ENEMY:
                case Type.ANY_NEIGHBOR_COUNTRY:
                case Type.ANY_RIVAL_COUNTRY:
                case Type.ANY_SUBJECT_COUNTRY:
                case Type.ANY_WAR_ENEMY_COUNTRY:
                case Type.ANY_CORE_PROVINCE:
                case Type.ANY_HERETIC_PROVINCE:
                case Type.ANY_HIRED_MERCENARY_COMPANY:
                case Type.ANY_OWNED_PROVINCE:
                case Type.ANY_STATE:
                case Type.ANY_ACTIVE_TRADE_NODE:
                case Type.HOME_TRADE_NODE:
                case Type.ANY_OTHER_GREAT_POWER:
                    return Scope.Country;

                case Type.OWNER:
                case Type.CONTROLLER:
                case Type.SEA_ZONE:
                case Type.EVERY_EMPTY_NEIGHBOR_PROVINCE:
                case Type.EVERY_NEIGHBOR_PROVINCE:
                case Type.EVERY_PROVINCE_IN_STATE:
                case Type.RANDOM_EMPTY_NEIGHBOR_PROVINCE:
                case Type.RANDOM_NEIGHBOR_PROVINCE:
                case Type.RANDOM_PROVINCE_IN_STATE:
                case Type.EVERY_CORE_COUNTRY:
                case Type.RANDOM_CORE_COUNTRY:
                case Type.AREA:
                case Type.REGION:
                case Type.ALL_EMPTY_NEIGHBOR_PROVINCE:
                case Type.ALL_PROVINCE_IN_STATE:
                case Type.ALL_NEIGHBOR_PROVINCE:
                case Type.ANY_EMPTY_NEIGHBOR_PROVINCE:
                case Type.ANY_PROVINCE_IN_STATE:
                case Type.ANY_FRIENDLY_COAST_BORDER_PROVINCE:
                case Type.ANY_NEIGHBOR_PROVINCE:
                case Type.AREA_FOR_SCOPE_PROVINCE:
                case Type.REGION_FOR_SCOPE_PROVINCE:
                case Type.ALL_CORE_COUNTRY:
                case Type.ANY_CORE_COUNTRY:
                    return Scope.Province;

                case Type.MOST_PROVINCE_TRADE_POWER:
                case Type.STRONGEST_TRADE_POWER:
                case Type.EVERY_PRIVATEERING_COUNTRY:
                case Type.RANDOM_PRIVATEERING_COUNTRY:
                case Type.EVERY_TRADE_NODE_MEMBER_COUNTRY:
                case Type.RANDOM_TRADE_NODE_MEMBER_COUNTRY:
                case Type.EVERY_TRADE_NODE_MEMBER_PROVINCE:
                case Type.RANDOM_TRADE_NODE_MEMBER_PROVINCE:
                case Type.ALL_PRIVATEERING_COUNTRY:
                case Type.ALL_TRADE_NODE_MEMBER_COUNTRY:
                case Type.ANY_PRIVATEERING_COUNTRY:
                case Type.ANY_TRADE_NODE_MEMBER_COUNTRY:
                case Type.ALL_TRADE_NODE_MEMBER_PROVINCE:
                case Type.ANY_TRADE_NODE_MEMBER_PROVINCE:
                    return Scope.TradeNode;

                case Type.UNIT_OWNER:
                case Type.ENEMY_UNIT:
                case Type.LOCATION:
                    return Scope.Unit;

                case Type.EVERY_TARGET_PROVINCE:
                case Type.RANDOM_TARGET_PROVINCE:
                case Type.ALL_TARGET_PROVINCE:
                case Type.ANY_TARGET_PROVINCE:
                    return Scope.Mission;
            }
        }

        public enum ScopeType { Effect, Trigger, Both }

        public static ScopeType GetScopeType(Type type)
        {
            switch (type)
            {
                default:
                    return ScopeType.Both;

                case Type.OR:
                case Type.AND:
                case Type.NOT:
                    return ScopeType.Trigger;

                case Type.ROOT:
                case Type.FROM:
                case Type.PREV:
                case Type.THIS:
                case Type.EVENT_TRAGET:
                case Type.TAG_NAME:
                case Type.EMPEROR:
                case Type.REVOLULTION_TARGET:
                case Type.CRUSADE_TARGET:
                case Type.COLONIAL_PARENT:
                case Type.OVERLORD:
                case Type.OWNER:
                case Type.CONTROLLER:
                case Type.MOST_PROVINCE_TRADE_POWER:
                case Type.STRONGEST_TRADE_POWER:
                case Type.PROVINCE_ID:
                case Type.AREA_NAME:
                case Type.REGION_NAME:
                case Type.SUPERREGION_NAME:
                case Type.PROVINCEGROUP_NAME:
                case Type.CONTINENT_NAME:
                case Type.TRADE_COMPANY_NAME:
                case Type.COLONIAL_REGION_NAME:
                case Type.CAPITAL_SCOPE:
                case Type.SEA_ZONE:
                    return ScopeType.Both;

                case Type.EVERY_ALLY:
                case Type.EVERY_COALITION_MEMBER:
                case Type.EVERY_COUNTRY:
                case Type.EVERY_COUNTRY_INCLUDING_INACTIVE:
                case Type.EVERY_ELECTOR:
                case Type.EVERY_ENEMY_COUNTRY:
                case Type.EVERY_KNOWN_COUNTRY:
                case Type.EVERY_LOCAL_ENEMY:
                case Type.EVERY_NEIGHBOR_COUNTRY:
                case Type.EVERY_RIVAL_COUNTRY:
                case Type.EVERY_SUBJECT_COUNTRY:
                case Type.EVERY_WAR_ENEMY_COUNTRY:
                case Type.RANDOM_ALLY:
                case Type.RANDOM_COALITION_MEMBER:
                case Type.RANDOM_COUNTRY:
                case Type.RANDOM_ELECTOR:
                case Type.RANDOM_ENEMY_COUNTRY:
                case Type.RANDOM_HIRED_MERCENARY_COMPANY:
                case Type.RANDOM_KNOWN_COUNTRY:
                case Type.RANDOM_LOCAL_ENEMY:
                case Type.RANDOM_NEIGHBOR_COUNTRY:
                case Type.RANDOM_RIVAL_COUNTRY:
                case Type.RANDOM_SUBJECT_COUNTRY:
                case Type.RANDOM_WAR_ENEMY_COUNTRY:
                case Type.EVERY_CORE_PROVINCE:
                case Type.EVERY_HERETIC_PROVINCE:
                case Type.EVERY_OWNED_PROVINCE:
                case Type.EVERY_PROVINCE:
                case Type.RANDOM_CORE_PROVINCE:
                case Type.RANDOM_HERETIC_PROVINCE:
                case Type.RANDOM_OWNED_AREA:
                case Type.RANDOM_OWNED_PROVINCE:
                case Type.RANDOM_ACTIVE_TRADE_NODE:
                case Type.RANDOM_TRADE_NODE:
                case Type.HOME_TRADE_NODE_EFFECT_SCOPE:
                case Type.EVERY_EMPTY_NEIGHBOR_PROVINCE:
                case Type.EVERY_NEIGHBOR_PROVINCE:
                case Type.EVERY_PROVINCE_IN_STATE:
                case Type.RANDOM_EMPTY_NEIGHBOR_PROVINCE:
                case Type.RANDOM_NEIGHBOR_PROVINCE:
                case Type.RANDOM_PROVINCE_IN_STATE:
                case Type.RANDOM_PROVINCE:
                case Type.EVERY_CORE_COUNTRY:
                case Type.RANDOM_CORE_COUNTRY:
                case Type.AREA:
                case Type.REGION:
                case Type.EVERY_PRIVATEERING_COUNTRY:
                case Type.RANDOM_PRIVATEERING_COUNTRY:
                case Type.EVERY_TRADE_NODE_MEMBER_COUNTRY:
                case Type.RANDOM_TRADE_NODE_MEMBER_COUNTRY:
                case Type.EVERY_TRADE_NODE_MEMBER_PROVINCE:
                case Type.RANDOM_TRADE_NODE_MEMBER_PROVINCE:
                case Type.UNIT_OWNER:
                case Type.ENEMY_UNIT:
                case Type.LOCATION:
                case Type.EVERY_TARGET_PROVINCE:
                case Type.RANDOM_TARGET_PROVINCE:
                    return ScopeType.Effect;

                case Type.ANY_ARMY:
                case Type.ALL_ALLY:
                case Type.ALL_COALITION_MEMBER:
                case Type.ALL_COUNTRY:
                case Type.ALL_COUNTRIES_INCLUDING_SELF:
                case Type.ALL_ELECTOR:
                case Type.ALL_ENEMY_COUNTRY:
                case Type.ALL_KNOWN_COUNTRY:
                case Type.ALL_LOCAL_ENEMY:
                case Type.ALL_NEIGHBOR_COUNTRY:
                case Type.ALL_RIVAL_COUNTRY:
                case Type.ALL_SUBJECT_COUNTRY:
                case Type.ALL_WAR_ENEMY_COUNTRIES:
                case Type.ALL_CORE_PROVINCE:
                case Type.ALL_HERETIC_PROVINCE:
                case Type.ALL_OWNED_PROVINCE:
                case Type.ALL_PROVINCE:
                case Type.ALL_STATE_PROVINCE:
                case Type.ALL_STATES:
                case Type.ALL_ACTIVE_TRADE_NODE:
                case Type.ALL_TRADE_NODE:
                case Type.ANY_ALLY:
                case Type.ANY_COALITION_MEMBER:
                case Type.ANY_COUNTRY:
                case Type.ANY_ELECTOR:
                case Type.ANY_ENEMY_COUNTRY:
                case Type.ANY_KNOWN_COUNTRY:
                case Type.ANY_LOCAL_ENEMY:
                case Type.ANY_NEIGHBOR_COUNTRY:
                case Type.ANY_RIVAL_COUNTRY:
                case Type.ANY_SUBJECT_COUNTRY:
                case Type.ANY_WAR_ENEMY_COUNTRY:
                case Type.ANY_CORE_PROVINCE:
                case Type.ANY_HERETIC_PROVINCE:
                case Type.ANY_HIRED_MERCENARY_COMPANY:
                case Type.ANY_OWNED_PROVINCE:
                case Type.ANY_PROVINCE:
                case Type.ANY_STATE:
                case Type.ANY_ACTIVE_TRADE_NODE:
                case Type.ANY_TRADE_NODE:
                case Type.HOME_TRADE_NODE:
                case Type.ANY_GREAT_POWER:
                case Type.ANY_OTHER_GREAT_POWER:
                case Type.ALL_EMPTY_NEIGHBOR_PROVINCE:
                case Type.ALL_PROVINCE_IN_STATE:
                case Type.ALL_NEIGHBOR_PROVINCE:
                case Type.ANY_EMPTY_NEIGHBOR_PROVINCE:
                case Type.ANY_PROVINCE_IN_STATE:
                case Type.ANY_FRIENDLY_COAST_BORDER_PROVINCE:
                case Type.ANY_NEIGHBOR_PROVINCE:
                case Type.AREA_FOR_SCOPE_PROVINCE:
                case Type.REGION_FOR_SCOPE_PROVINCE:
                case Type.ALL_CORE_COUNTRY:
                case Type.ANY_CORE_COUNTRY:
                case Type.ALL_PRIVATEERING_COUNTRY:
                case Type.ALL_TRADE_NODE_MEMBER_COUNTRY:
                case Type.ANY_PRIVATEERING_COUNTRY:
                case Type.ANY_TRADE_NODE_MEMBER_COUNTRY:
                case Type.ALL_TRADE_NODE_MEMBER_PROVINCE:
                case Type.ANY_TRADE_NODE_MEMBER_PROVINCE:
                case Type.ALL_TARGET_PROVINCE:
                case Type.ANY_TARGET_PROVINCE:


                case Type.AI_ATTITUDE:
                case Type.ARMY_STRENGTH:
                case Type.BORDER_DISTANCE:
                case Type.CALC_TRUE_IF:
                case Type.CAN_USE_PEACE_TREATY:
                case Type.CAPITAL_DISTANCE:
                case Type.CHECK_VARIABLE:
                case Type.CUSTOM_TRIGGER_TOOLTIP:
                case Type.DEVELOPMENT_IN_PROVINCES:
                case Type.EMPLOYED_ADVISOR:
                case Type.ESTATE_INFLUENCE:
                case Type.ESTATE_LOYALTY:
                case Type.ESTATE_TERRITORY:
                case Type.FACTION_INFLUENCE:
                case Type.HAD_ACTIVE_POLICY:
                case Type.HAD_CONSORT_FLAG:
                case Type.HAD_COUNTRY_FLAG:
                case Type.HAD_GLOBAL_FLAG:
                case Type.HAD_HEIR_FLAG:
                case Type.HAD_PROVINCE_FLAG:
                case Type.HAD_RULER_FLAG:
                case Type.HAS_CASUS_BELLI:
                case Type.HAS_DISASTER_PROGRESS:
                case Type.HAS_ESTATE_INFLUENCE_MODIFIER:
                case Type.HAS_ESTATE_LOYALTY_MODIFIER:
                case Type.HAS_GREAT_PROJECT:
                case Type.HAS_GLOBAL_MODIFIER_VALUE:
                case Type.HAS_LEADER_WITH:
                case Type.HAS_LOCAL_MODIFIER_VALUE:
                case Type.HAS_OPINION:
                case Type.HAS_OPINION_DIFF:
                case Type.HAS_OPINION_MODIFIER:
                case Type.HAS_PRIVATEER_SHARE_IN_TRADE_NODE:
                case Type.HAS_SPY_NETWORK_FROM:
                case Type.HAS_SPY_NETWORK_IN:
                case Type.HAS_TRADE_COMPANY_INVESTEMENT_IN_AREA:
                case Type.HAS_TRADE_MODIFIER:
                case Type.HAS_WON_WAR_AGAINST:
                case Type.HIDDEN_TRIGGER:
                case Type.INCIDENT_VARIABLE_VALUE:
                case Type.INSTITUTION_DIFFERENCE:
                case Type.IS_IN_WAR:
                case Type.IS_SUBJECT_OF_TYPE_WITH_OVERLORD:
                case Type.IS_VARIABLE_EQUAL:
                case Type.MILITARY_STRENGTH:
                case Type.NAVAL_STRENGTH:
                case Type.NUM_INVESTEMENTS_IN_TRADE_COMPANY_REGION:
                case Type.NUM_OF_ESTATE_PRIVILEGES:
                case Type.NUM_OF_OWNED_PROVINCES_WITH:
                case Type.NUM_OF_PROVINCES_OWNED_OR_OWNED_BY_NON_SOVEREIGN_SUBJECTS_WITH:
                case Type.NUM_OF_PROVINCES_OWNED_OR_OWNED_BY__SUBJECTS_WITH:
                case Type.NUM_OF_RELIGION:
                case Type.NUM_OF_UNITS_IN_PROVINCE:
                case Type.OWES_FAVORS:
                case Type.OWNS_ALL_PROVINCES:
                case Type.PRIVATEER_POWER:
                case Type.PRODUCTION_LEADER:
                case Type.PROVINCE_DISTANCE:
                case Type.RELIGION_YEARS:
                case Type.RELIGIOUS_SCHOOL:
                case Type.REVERSE_HAS_OPINION:
                case Type.REVERSE_HAS_OPINION_MODIFIER:
                case Type.TOTAL_LOSSES_IN_WON_WARS:
                case Type.TRADE_GOODS_PRODUCED_AMOUNT:
                case Type.TRADE_SHARE:
                case Type.TRADING_BONUS:
                case Type.TRADING_PART:
                case Type.TRADING_POLICY_IN_NODE:
                case Type.VARIABLE_ARITHMETIC_TRIGGER:
                case Type.WAR_SCORE_AGAINST:
                case Type.YEARS_IN_UNION_UNDER:
                case Type.YEARS_IN_VASSALAGE_UNDER:
                    return ScopeType.Trigger;

            }
        }

        public static Scope ScopesTo(Type type, Scope CurrentScope, Scope Root = Scope.Invalid, Scope From = Scope.Invalid, Scope Prev = Scope.Invalid, Scope This = Scope.Invalid, Scope EventTarget = Scope.Invalid)
        {
            switch (type)
            {
                default:
                    return Scope.Anywhere;

                case Type.ROOT:
                    return Root;
                case Type.FROM:
                    return From;
                case Type.PREV:
                    return Prev;
                case Type.THIS:
                    return This;
                case Type.EVENT_TRAGET:
                    return EventTarget;


                case Type.OR:
                case Type.AND:
                case Type.NOT:                   
                    return CurrentScope;



                case Type.TAG_NAME:
                case Type.EMPEROR:
                case Type.REVOLULTION_TARGET:
                case Type.CRUSADE_TARGET:
                case Type.COLONIAL_PARENT:
                case Type.OVERLORD:
                case Type.OWNER:
                case Type.CONTROLLER:
                case Type.MOST_PROVINCE_TRADE_POWER:
                case Type.STRONGEST_TRADE_POWER:
                case Type.EVERY_ALLY:
                case Type.EVERY_COALITION_MEMBER:
                case Type.EVERY_COUNTRY:
                case Type.EVERY_COUNTRY_INCLUDING_INACTIVE:
                case Type.EVERY_ELECTOR:
                case Type.EVERY_ENEMY_COUNTRY:
                case Type.EVERY_KNOWN_COUNTRY:
                case Type.EVERY_LOCAL_ENEMY:
                case Type.EVERY_NEIGHBOR_COUNTRY:
                case Type.EVERY_RIVAL_COUNTRY:
                case Type.EVERY_SUBJECT_COUNTRY:
                case Type.EVERY_WAR_ENEMY_COUNTRY:
                case Type.RANDOM_ALLY:
                case Type.RANDOM_COALITION_MEMBER:
                case Type.RANDOM_COUNTRY:
                case Type.RANDOM_ELECTOR:
                case Type.RANDOM_ENEMY_COUNTRY:
                case Type.RANDOM_KNOWN_COUNTRY:
                case Type.RANDOM_LOCAL_ENEMY:
                case Type.RANDOM_NEIGHBOR_COUNTRY:
                case Type.RANDOM_RIVAL_COUNTRY:
                case Type.RANDOM_SUBJECT_COUNTRY:
                case Type.RANDOM_WAR_ENEMY_COUNTRY:
                case Type.EVERY_CORE_COUNTRY:
                case Type.RANDOM_CORE_COUNTRY:
                case Type.EVERY_PRIVATEERING_COUNTRY:
                case Type.RANDOM_PRIVATEERING_COUNTRY:
                case Type.EVERY_TRADE_NODE_MEMBER_COUNTRY:
                case Type.RANDOM_TRADE_NODE_MEMBER_COUNTRY:
                case Type.UNIT_OWNER:
                case Type.ALL_ALLY:
                case Type.ALL_COALITION_MEMBER:
                case Type.ALL_COUNTRY:
                case Type.ALL_COUNTRIES_INCLUDING_SELF:
                case Type.ALL_ELECTOR:
                case Type.ALL_ENEMY_COUNTRY:
                case Type.ALL_KNOWN_COUNTRY:
                case Type.ALL_LOCAL_ENEMY:
                case Type.ALL_NEIGHBOR_COUNTRY:
                case Type.ALL_RIVAL_COUNTRY:
                case Type.ALL_SUBJECT_COUNTRY:
                case Type.ALL_WAR_ENEMY_COUNTRIES:
                case Type.ANY_ALLY:
                case Type.ANY_COALITION_MEMBER:
                case Type.ANY_COUNTRY:
                case Type.ANY_ELECTOR:
                case Type.ANY_ENEMY_COUNTRY:
                case Type.ANY_KNOWN_COUNTRY:
                case Type.ANY_LOCAL_ENEMY:
                case Type.ANY_NEIGHBOR_COUNTRY:
                case Type.ANY_RIVAL_COUNTRY:
                case Type.ANY_SUBJECT_COUNTRY:
                case Type.ANY_WAR_ENEMY_COUNTRY:
                case Type.ANY_GREAT_POWER:
                case Type.ANY_OTHER_GREAT_POWER:
                case Type.ALL_CORE_COUNTRY:
                case Type.ANY_CORE_COUNTRY:
                case Type.ALL_PRIVATEERING_COUNTRY:
                case Type.ALL_TRADE_NODE_MEMBER_COUNTRY:
                case Type.ANY_PRIVATEERING_COUNTRY:
                case Type.ANY_TRADE_NODE_MEMBER_COUNTRY:
                    return Scope.Country;

                case Type.PROVINCE_ID:
                case Type.AREA_NAME:
                case Type.REGION_NAME:
                case Type.SUPERREGION_NAME:
                case Type.PROVINCEGROUP_NAME:
                case Type.CONTINENT_NAME:
                case Type.TRADE_COMPANY_NAME:
                case Type.COLONIAL_REGION_NAME:
                case Type.CAPITAL_SCOPE:
                case Type.SEA_ZONE:
                case Type.EVERY_CORE_PROVINCE:
                case Type.EVERY_HERETIC_PROVINCE:
                case Type.EVERY_OWNED_PROVINCE:
                case Type.EVERY_PROVINCE:
                case Type.RANDOM_CORE_PROVINCE:
                case Type.RANDOM_HERETIC_PROVINCE:
                case Type.RANDOM_OWNED_AREA:
                case Type.RANDOM_OWNED_PROVINCE:
                case Type.EVERY_EMPTY_NEIGHBOR_PROVINCE:
                case Type.EVERY_NEIGHBOR_PROVINCE:
                case Type.EVERY_PROVINCE_IN_STATE:
                case Type.RANDOM_EMPTY_NEIGHBOR_PROVINCE:
                case Type.RANDOM_NEIGHBOR_PROVINCE:
                case Type.RANDOM_PROVINCE_IN_STATE:
                case Type.RANDOM_PROVINCE:
                case Type.AREA:
                case Type.REGION:
                case Type.EVERY_TRADE_NODE_MEMBER_PROVINCE:
                case Type.RANDOM_TRADE_NODE_MEMBER_PROVINCE:
                case Type.LOCATION:
                case Type.EVERY_TARGET_PROVINCE:
                case Type.RANDOM_TARGET_PROVINCE:
                case Type.ALL_CORE_PROVINCE:
                case Type.ALL_HERETIC_PROVINCE:
                case Type.ALL_OWNED_PROVINCE:
                case Type.ALL_PROVINCE:
                case Type.ALL_STATE_PROVINCE:
                case Type.ALL_STATES:
                case Type.ANY_CORE_PROVINCE:
                case Type.ANY_HERETIC_PROVINCE:
                case Type.ANY_OWNED_PROVINCE:
                case Type.ANY_PROVINCE:
                case Type.ANY_STATE:
                case Type.ALL_EMPTY_NEIGHBOR_PROVINCE:
                case Type.ALL_PROVINCE_IN_STATE:
                case Type.ALL_NEIGHBOR_PROVINCE:
                case Type.ANY_EMPTY_NEIGHBOR_PROVINCE:
                case Type.ANY_PROVINCE_IN_STATE:
                case Type.ANY_FRIENDLY_COAST_BORDER_PROVINCE:
                case Type.ANY_NEIGHBOR_PROVINCE:
                case Type.AREA_FOR_SCOPE_PROVINCE:
                case Type.REGION_FOR_SCOPE_PROVINCE:
                case Type.ALL_TRADE_NODE_MEMBER_PROVINCE:
                case Type.ANY_TRADE_NODE_MEMBER_PROVINCE:
                case Type.ALL_TARGET_PROVINCE:
                case Type.ANY_TARGET_PROVINCE:
                    return Scope.Province;

                case Type.RANDOM_HIRED_MERCENARY_COMPANY:
                case Type.ENEMY_UNIT:
                case Type.ANY_ARMY:
                case Type.ANY_HIRED_MERCENARY_COMPANY:
                    return Scope.Unit;

                case Type.RANDOM_ACTIVE_TRADE_NODE:
                case Type.RANDOM_TRADE_NODE:
                case Type.HOME_TRADE_NODE_EFFECT_SCOPE:
                case Type.ALL_ACTIVE_TRADE_NODE:
                case Type.ALL_TRADE_NODE:
                case Type.ANY_ACTIVE_TRADE_NODE:
                case Type.ANY_TRADE_NODE:
                case Type.HOME_TRADE_NODE:
                    return Scope.TradeNode;

                case Type.AI_ATTITUDE:
                case Type.ARMY_STRENGTH:
                case Type.BORDER_DISTANCE:
                case Type.CALC_TRUE_IF:
                case Type.CAN_USE_PEACE_TREATY:
                case Type.CAPITAL_DISTANCE:
                case Type.CHECK_VARIABLE:
                case Type.CUSTOM_TRIGGER_TOOLTIP:
                case Type.DEVELOPMENT_IN_PROVINCES:
                case Type.EMPLOYED_ADVISOR:
                case Type.ESTATE_INFLUENCE:
                case Type.ESTATE_LOYALTY:
                case Type.ESTATE_TERRITORY:
                case Type.FACTION_INFLUENCE:
                case Type.HAD_ACTIVE_POLICY:
                case Type.HAD_CONSORT_FLAG:
                case Type.HAD_COUNTRY_FLAG:
                case Type.HAD_GLOBAL_FLAG:
                case Type.HAD_HEIR_FLAG:
                case Type.HAD_PROVINCE_FLAG:
                case Type.HAD_RULER_FLAG:
                case Type.HAS_CASUS_BELLI:
                case Type.HAS_DISASTER_PROGRESS:
                case Type.HAS_ESTATE_INFLUENCE_MODIFIER:
                case Type.HAS_ESTATE_LOYALTY_MODIFIER:
                case Type.HAS_GREAT_PROJECT:
                case Type.HAS_GLOBAL_MODIFIER_VALUE:
                case Type.HAS_LEADER_WITH:
                case Type.HAS_LOCAL_MODIFIER_VALUE:
                case Type.HAS_OPINION:
                case Type.HAS_OPINION_DIFF:
                case Type.HAS_OPINION_MODIFIER:
                case Type.HAS_PRIVATEER_SHARE_IN_TRADE_NODE:
                case Type.HAS_SPY_NETWORK_FROM:
                case Type.HAS_SPY_NETWORK_IN:
                case Type.HAS_TRADE_COMPANY_INVESTEMENT_IN_AREA:
                case Type.HAS_TRADE_MODIFIER:
                case Type.HAS_WON_WAR_AGAINST:
                case Type.HIDDEN_TRIGGER:
                case Type.INCIDENT_VARIABLE_VALUE:
                case Type.INSTITUTION_DIFFERENCE:
                case Type.IS_IN_WAR:
                case Type.IS_SUBJECT_OF_TYPE_WITH_OVERLORD:
                case Type.IS_VARIABLE_EQUAL:
                case Type.MILITARY_STRENGTH:
                case Type.NAVAL_STRENGTH:
                case Type.NUM_INVESTEMENTS_IN_TRADE_COMPANY_REGION:
                case Type.NUM_OF_ESTATE_PRIVILEGES:
                case Type.NUM_OF_OWNED_PROVINCES_WITH:
                case Type.NUM_OF_PROVINCES_OWNED_OR_OWNED_BY_NON_SOVEREIGN_SUBJECTS_WITH:
                case Type.NUM_OF_PROVINCES_OWNED_OR_OWNED_BY__SUBJECTS_WITH:
                case Type.NUM_OF_RELIGION:
                case Type.NUM_OF_UNITS_IN_PROVINCE:
                case Type.OWES_FAVORS:
                case Type.OWNS_ALL_PROVINCES:
                case Type.PRIVATEER_POWER:
                case Type.PRODUCTION_LEADER:
                case Type.PROVINCE_DISTANCE:
                case Type.RELIGION_YEARS:
                case Type.RELIGIOUS_SCHOOL:
                case Type.REVERSE_HAS_OPINION:
                case Type.REVERSE_HAS_OPINION_MODIFIER:
                case Type.TOTAL_LOSSES_IN_WON_WARS:
                case Type.TRADE_GOODS_PRODUCED_AMOUNT:
                case Type.TRADE_SHARE:
                case Type.TRADING_BONUS:
                case Type.TRADING_PART:
                case Type.TRADING_POLICY_IN_NODE:
                case Type.VARIABLE_ARITHMETIC_TRIGGER:
                case Type.WAR_SCORE_AGAINST:
                case Type.YEARS_IN_UNION_UNDER:
                case Type.YEARS_IN_VASSALAGE_UNDER:
                    return Scope.Clause;
            }
        } 
        public enum Type { 
            UNKNOWN_TYPE, TRIGGER,
            OR, AND, NOT, ROOT, FROM, PREV, THIS, PROVINCE_ID, TAG_NAME, AREA_NAME, REGION_NAME, SUPERREGION_NAME,
            PROVINCEGROUP_NAME, CONTINENT_NAME, TRADE_COMPANY_NAME, COLONIAL_REGION_NAME, EVENT_TRAGET, EMPEROR, REVOLULTION_TARGET, CRUSADE_TARGET,
        
            //PROVINCE
            OWNER, CONTROLLER, SEA_ZONE,

            //COUNTRY
            COLONIAL_PARENT, OVERLORD, CAPITAL_SCOPE,

            //TRADE_NODE
            MOST_PROVINCE_TRADE_POWER, STRONGEST_TRADE_POWER,

            //COUNTRY EFFECTS EVERY
            EVERY_ALLY, EVERY_COALITION_MEMBER, EVERY_COUNTRY, EVERY_COUNTRY_INCLUDING_INACTIVE,
            EVERY_ELECTOR, EVERY_ENEMY_COUNTRY, EVERY_KNOWN_COUNTRY, EVERY_LOCAL_ENEMY, EVERY_NEIGHBOR_COUNTRY,
            EVERY_RIVAL_COUNTRY, EVERY_SUBJECT_COUNTRY, EVERY_WAR_ENEMY_COUNTRY,
            //COUNTRY EFFECTS RANDOM
            RANDOM_ALLY, RANDOM_COALITION_MEMBER, RANDOM_COUNTRY, RANDOM_ELECTOR, RANDOM_ENEMY_COUNTRY,
            RANDOM_HIRED_MERCENARY_COMPANY, RANDOM_KNOWN_COUNTRY, RANDOM_LOCAL_ENEMY, RANDOM_NEIGHBOR_COUNTRY,
            RANDOM_RIVAL_COUNTRY, RANDOM_SUBJECT_COUNTRY, RANDOM_WAR_ENEMY_COUNTRY, 
            //COUNTRY EFFECTS -> PROVINCES
            EVERY_CORE_PROVINCE, EVERY_HERETIC_PROVINCE, EVERY_OWNED_PROVINCE, EVERY_PROVINCE, RANDOM_CORE_PROVINCE,
            RANDOM_HERETIC_PROVINCE, RANDOM_OWNED_AREA, RANDOM_OWNED_PROVINCE, RANDOM_ACTIVE_TRADE_NODE,
            RANDOM_TRADE_NODE, HOME_TRADE_NODE_EFFECT_SCOPE,


            //PROVINCE EFFECTS EVERY
            EVERY_EMPTY_NEIGHBOR_PROVINCE, EVERY_NEIGHBOR_PROVINCE, EVERY_PROVINCE_IN_STATE,
            RANDOM_EMPTY_NEIGHBOR_PROVINCE, RANDOM_NEIGHBOR_PROVINCE, RANDOM_PROVINCE_IN_STATE,
            RANDOM_PROVINCE, EVERY_CORE_COUNTRY, RANDOM_CORE_COUNTRY, AREA, REGION,

            //TRADENODE
            EVERY_PRIVATEERING_COUNTRY, RANDOM_PRIVATEERING_COUNTRY, EVERY_TRADE_NODE_MEMBER_COUNTRY,
            RANDOM_TRADE_NODE_MEMBER_COUNTRY, EVERY_TRADE_NODE_MEMBER_PROVINCE, RANDOM_TRADE_NODE_MEMBER_PROVINCE,

            //UNIT
            UNIT_OWNER, ENEMY_UNIT, LOCATION,

            //MISSIONS
            EVERY_TARGET_PROVINCE,
            RANDOM_TARGET_PROVINCE,

            //TRIGGERS

            //COUNTRY
            ANY_ARMY, ALL_ALLY, ALL_COALITION_MEMBER,
            ALL_COUNTRY, ALL_COUNTRIES_INCLUDING_SELF,
            ALL_ELECTOR, ALL_ENEMY_COUNTRY, ALL_KNOWN_COUNTRY,
            ALL_LOCAL_ENEMY, ALL_NEIGHBOR_COUNTRY, ALL_RIVAL_COUNTRY,
            ALL_SUBJECT_COUNTRY, ALL_WAR_ENEMY_COUNTRIES, ALL_CORE_PROVINCE,
            ALL_HERETIC_PROVINCE, ALL_OWNED_PROVINCE, ALL_PROVINCE, ALL_STATE_PROVINCE,
            ALL_STATES, ALL_ACTIVE_TRADE_NODE, ALL_TRADE_NODE, ANY_ALLY, ANY_COALITION_MEMBER,
            ANY_COUNTRY, ANY_ELECTOR, ANY_ENEMY_COUNTRY, ANY_KNOWN_COUNTRY, ANY_LOCAL_ENEMY,
            ANY_NEIGHBOR_COUNTRY, ANY_RIVAL_COUNTRY, ANY_SUBJECT_COUNTRY, ANY_WAR_ENEMY_COUNTRY,
            ANY_CORE_PROVINCE, ANY_HERETIC_PROVINCE, ANY_OWNED_PROVINCE, ANY_HIRED_MERCENARY_COMPANY,
            ANY_PROVINCE, ANY_STATE, ANY_ACTIVE_TRADE_NODE, ANY_TRADE_NODE, HOME_TRADE_NODE, ANY_GREAT_POWER,
            ANY_OTHER_GREAT_POWER,

            //PROVINCE
            ALL_EMPTY_NEIGHBOR_PROVINCE, ALL_PROVINCE_IN_STATE,
             ALL_NEIGHBOR_PROVINCE, ANY_EMPTY_NEIGHBOR_PROVINCE,
             ANY_PROVINCE_IN_STATE, ANY_FRIENDLY_COAST_BORDER_PROVINCE, ANY_NEIGHBOR_PROVINCE,
             ALL_CORE_COUNTRY, ANY_CORE_COUNTRY, AREA_FOR_SCOPE_PROVINCE, REGION_FOR_SCOPE_PROVINCE,

             //TRADENODE

             ALL_PRIVATEERING_COUNTRY, ALL_TRADE_NODE_MEMBER_COUNTRY, ANY_PRIVATEERING_COUNTRY,
             ANY_TRADE_NODE_MEMBER_COUNTRY, ALL_TRADE_NODE_MEMBER_PROVINCE, ANY_TRADE_NODE_MEMBER_PROVINCE,

             //MISSIONS

             ALL_TARGET_PROVINCE, ANY_TARGET_PROVINCE,


             //TODO DEAL WITH CLAUSES (SPECIAL TRIGGERS)
             //CLAUSES
             AI_ATTITUDE, ARMY_STRENGTH, BORDER_DISTANCE, CALC_TRUE_IF,
             CAN_USE_PEACE_TREATY, CAPITAL_DISTANCE, CHECK_VARIABLE,
             CUSTOM_TRIGGER_TOOLTIP, DEVELOPMENT_IN_PROVINCES,
             EMPLOYED_ADVISOR, ESTATE_INFLUENCE, ESTATE_LOYALTY,
             ESTATE_TERRITORY, FACTION_INFLUENCE, HAD_ACTIVE_POLICY,
             HAD_CONSORT_FLAG, HAD_COUNTRY_FLAG, HAD_GLOBAL_FLAG,
             HAD_HEIR_FLAG, HAD_PROVINCE_FLAG, HAD_RULER_FLAG,
             HAS_CASUS_BELLI, HAS_DISASTER_PROGRESS, HAS_ESTATE_INFLUENCE_MODIFIER,
             HAS_ESTATE_LOYALTY_MODIFIER, HAS_GREAT_PROJECT, HAS_GLOBAL_MODIFIER_VALUE,
             HAS_LEADER_WITH, HAS_LOCAL_MODIFIER_VALUE, HAS_OPINION, HAS_OPINION_DIFF,
             HAS_OPINION_MODIFIER, HAS_PRIVATEER_SHARE_IN_TRADE_NODE, HAS_SPY_NETWORK_FROM,
             HAS_SPY_NETWORK_IN, HAS_TRADE_COMPANY_INVESTEMENT_IN_AREA, HAS_TRADE_MODIFIER,
             HAS_WON_WAR_AGAINST, HIDDEN_TRIGGER, INCIDENT_VARIABLE_VALUE, INSTITUTION_DIFFERENCE,
             IS_IN_WAR, IS_SUBJECT_OF_TYPE_WITH_OVERLORD, IS_VARIABLE_EQUAL, MILITARY_STRENGTH,
             NAVAL_STRENGTH, NUM_INVESTEMENTS_IN_TRADE_COMPANY_REGION, NUM_OF_ESTATE_PRIVILEGES,
             NUM_OF_OWNED_PROVINCES_WITH, NUM_OF_PROVINCES_OWNED_OR_OWNED_BY_NON_SOVEREIGN_SUBJECTS_WITH,
            NUM_OF_PROVINCES_OWNED_OR_OWNED_BY__SUBJECTS_WITH, NUM_OF_RELIGION, NUM_OF_UNITS_IN_PROVINCE,
            OWES_FAVORS, OWNS_ALL_PROVINCES, PRIVATEER_POWER, PRODUCTION_LEADER, PROVINCE_DISTANCE, RELIGION_YEARS,
            RELIGIOUS_SCHOOL, REVERSE_HAS_OPINION, REVERSE_HAS_OPINION_MODIFIER, TOTAL_LOSSES_IN_WON_WARS,
            TRADE_GOODS_PRODUCED_AMOUNT, TRADE_SHARE, TRADING_BONUS, TRADING_PART, TRADING_POLICY_IN_NODE,
            VARIABLE_ARITHMETIC_TRIGGER, WAR_SCORE_AGAINST, YEARS_IN_UNION_UNDER, YEARS_IN_VASSALAGE_UNDER,
        };



        public List<TriggerItem> Contents = new List<TriggerItem>();
        bool SpecialCompare(string compartor, double Value1, double Value2)
        {
            switch (compartor)
            {
                case "less":
                    return Value1 < Value2;
                case "lesseq":
                    return Value1 <= Value2;
                case "more":
                    return Value1 > Value2;
                case "moreeq":
                    return Value1 >= Value2;
                default:
                    return Value1 == Value2;
            }
        }
        public double GetDoubleValueFromIdeaSet(IdeaSet st, ModifierType tp)
        {
            return double.Parse((string)st.GetVariable(tp), System.Globalization.CultureInfo.InvariantCulture);
        }

        public bool EvaluateInsides(object toCheck)
        {
            if (toCheck == null)
                return false;
            List<bool> InsideValues = new List<bool>();

            foreach (TriggerItem ti in Contents)
            {
                if (ti is TriggerConnector)
                {
                    TriggerConnector tc = (TriggerConnector)ti;
                    if (tc.ConnectorType == Type.CAPITAL_SCOPE)
                        InsideValues.Add((ti as TriggerConnector).EvaluateInsides(toCheck));
                    else
                        InsideValues.Add((ti as TriggerConnector).EvaluateInsides(toCheck));
                }
                else
                {
                    Trigger tr = (Trigger)ti;
                    switch (tr.VariableChecked)
                    {
                        case TriggerVariable.TriggerVar.has_reform:
                            if (toCheck is Country)
                                InsideValues.Add((toCheck as Country).GovernmentReform.ToLower() == tr.ValueExpected.ToLower());
                            else
                                InsideValues.Add(false);
                            break;
                        case TriggerVariable.TriggerVar.tag:
                            if (toCheck is Country)
                                InsideValues.Add((toCheck as Country).Tag.ToLower() == tr.ValueExpected.ToLower());
                            else
                                InsideValues.Add(false);
                            break;
                        case TriggerVariable.TriggerVar.culture_group:
                            if (toCheck is Country)
                                InsideValues.Add((toCheck as Country).PrimaryCulture.Group.Name.ToLower() == tr.ValueExpected.ToLower());
                            else
                                InsideValues.Add(false);
                            break;
                        case TriggerVariable.TriggerVar.primary_culture:
                            if (toCheck is Country)
                                InsideValues.Add((toCheck as Country).PrimaryCulture.Name.ToLower() == tr.ValueExpected.ToLower());
                            else
                                InsideValues.Add(false);
                            break;
                        case TriggerVariable.TriggerVar.religion_group:
                            if (toCheck is Country)
                                InsideValues.Add((toCheck as Country).Religion.Group.Name.ToLower() == tr.ValueExpected.ToLower());
                            else
                                InsideValues.Add(false);
                            break;
                        case TriggerVariable.TriggerVar.religion:
                            if (toCheck is Country)
                                InsideValues.Add((toCheck as Country).Religion.Name.ToLower() == tr.ValueExpected.ToLower());
                            else
                                InsideValues.Add(false);
                            break;
                        case TriggerVariable.TriggerVar.government:
                            if (toCheck is Country)
                                InsideValues.Add((toCheck as Country).Government.Type.ToLower() == tr.ValueExpected.ToLower());
                            else
                                InsideValues.Add(false);
                            break;
                        case TriggerVariable.TriggerVar.region:
                            if (toCheck is Country)
                                InsideValues.Add((toCheck as Country).Capital?.Area.Region.Name.ToLower() == tr.ValueExpected.ToLower());
                            else
                                InsideValues.Add(false);
                            break;
                        case TriggerVariable.TriggerVar.superregion:
                            if (toCheck is Country)
                                InsideValues.Add((toCheck as Country).Capital?.Area.Region.Superregion.Name.ToLower() == tr.ValueExpected.ToLower());
                            else
                                InsideValues.Add(false);
                            break;
                        case TriggerVariable.TriggerVar.technology_group:
                            if (toCheck is Country)
                                InsideValues.Add((toCheck as Country).TechnologyGroup.ToLower() == tr.ValueExpected.ToLower());
                            else
                                InsideValues.Add(false);
                            break;

                        default:
                            InsideValues.Add(false);
                            break;
                    }
                }
            }

            switch (ConnectorType)
            {
                case Type.NOT:
                    {
                        bool sum = InsideValues.Any(x => x);
                        return !sum;
                    }
                case Type.AND:
                    {
                        bool sum = InsideValues.All(x => x);
                        return sum;
                    }
                case Type.OR:
                    {
                        bool sum = InsideValues.Any(x => x);
                        return sum;
                    }
                default:
                    {
                        bool sum = InsideValues.All(x => x);
                        return sum;
                    }
            }
        }
    }
}
