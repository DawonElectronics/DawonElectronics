using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using CIM.MES.Common.Data;
using ConnectorDEPKG.Models;
using ConnectorDEPKG.RuleServiceOI;

namespace ConnectorDEPKG
{
    public class DepkgHelper
    {
        private readonly string siteId = "1130";
        private readonly string userId = "103518";
        private readonly string ipAddr = "192.168.0.10";

        public static string getFullTrimString(object obj) => obj == null
            ? string.Empty
            : (string.IsNullOrWhiteSpace(obj.ToString()) ? string.Empty : obj.ToString().Replace(" ", string.Empty));

        public static string getTrimString(object obj) => obj == null
            ? string.Empty
            : (string.IsNullOrWhiteSpace(obj.ToString()) ? string.Empty : obj.ToString().Trim());

        public static string getString(object obj) => obj == null
            ? string.Empty
            : (string.IsNullOrWhiteSpace(obj.ToString()) ? string.Empty : obj.ToString());

        public static string getQueryVerString(object obj) => obj == null || getString(obj).Length <= 0
            ? string.Empty
            : getString(obj).PadLeft(5, '0');
        public static string getCommand(Constant.MessageCommand command) => Type.GetType("THiRA.MES.UI.CommonUtil.Constant+MessageCommand").GetField(command.ToString()).GetCustomAttributesData()[0].ConstructorArguments.First<CustomAttributeTypedArgument>().Value.ToString();
        public static string getLotID(string lotName)
        {
            string trimString = getTrimString((object) lotName);
            if (!trimString.Contains("-") || trimString.Length < 7)
                return trimString;
            int startIndex = -1;
            if (trimString.Length >= 7 && trimString.Contains("-"))
                startIndex = getFullTrimString((object) trimString).IndexOf("-", 0, 10);
            return startIndex < 0 ? trimString : trimString.Remove(startIndex, 1);
        }

        public static int getInteger(object obj, int defaultValue)
        {
            try
            {
                if (obj != null)
                {
                    if (obj.ToString().IndexOf('.') >= 0)
                    {
                        double result = 0.0;
                        double.TryParse(obj.ToString(), out result);
                        defaultValue = Convert.ToInt32(Math.Truncate(result));
                    }
                    else
                        int.TryParse(obj.ToString(), out defaultValue);
                }
                return defaultValue;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public DataTable GetLotLDrillCCL(string tool, string from, string to)
        {
            var client = new MesRuleServiceOIClient();
            try
            {
                var result = client.ExecCommand(new MessageData()
                {
                    COMMAND = "GetDataByQueryIDMulti",
                    TID = Guid.NewGuid().ToString(),
                    USERID = "103518",
                    IPADDRESS = "192.168.0.1",
                    SITEID = "1130",
                    DATALIST = new List<Dictionary<string, object>>()
                    {
                        new Dictionary<string, object>()
                        {
                            {"queryId", (object) "GetLotLDrillCCL"},
                            {"queryClassId", (object) "MES_UI_PMS"},
                            {"queryVersion", (object) "00001"},
                            {"siteId".ToUpper(), (object) "1130"},
                            {
                                "paramList",
                                (object) new Dictionary<string, object>()
                                {
                                    {"@SPECNR", (object) tool},
                                    {"@LAYERFR", (object) from},
                                    {"@LAYERTO", (object) to}
                                }
                            },
                            {
                                "returnId", (object) "RESULT"
                            }
                        }
                    }
                }).DATASET.Tables["RESULT"];

                return result;

            }
            catch (Exception e)
            {
                client.Abort();
                throw;
            }
        }
        public DataTable GetLotLDrillPPG(string tool, string from, string to)
        {
            var client = new MesRuleServiceOIClient();
            try
            {
                var result = client.ExecCommand(new MessageData()
                {
                    COMMAND = "GetDataByQueryIDMulti",
                    TID = Guid.NewGuid().ToString(),
                    USERID = "103518",
                    IPADDRESS = "192.168.0.1",
                    SITEID = "1130",
                    DATALIST = new List<Dictionary<string, object>>()
                    {
                        new Dictionary<string, object>()
                        {
                            {"queryId", (object) "GetLotLDrillPPG"},
                            {"queryClassId", (object) "MES_UI_PMS"},
                            {"queryVersion", (object) "00001"},
                            {"siteId".ToUpper(), (object) "1130"},
                            {
                                "paramList",
                                (object) new Dictionary<string, object>()
                                {
                                    {"@SPECNR", (object) tool},
                                    {"@LAYERFR", (object) from},
                                    {"@LAYERTO", (object) to}
                                }
                            },
                            {
                                "returnId", (object) "RESULT"
                            }
                        }
                    }
                }).DATASET.Tables["RESULT"];

                return result;

            }
            catch (Exception e)
            {
                client.Abort();
                throw;
            }
        }
        public MesSpecInfo? MesSpecInfoQry(string lot, string tool)
        {
            var client = new MesRuleServiceOIClient();
            try
            {
                var qrymsg = new MessageData()
                {
                    COMMAND = "ExecuteProcedure",
                    TID = Guid.NewGuid().ToString(),
                    USERID = "103518",
                    IPADDRESS = "192.168.0.20",
                    SITEID = "1130",
                    DATALIST = new List<Dictionary<string, object>>()
                    {
                        new Dictionary<string, object>()
                        {
                            {
                                "queryId",
                                (object) "GetLotSpecInfoList"
                            },
                            {
                                "queryVersion",
                                (object) "00005"
                            },
                            {
                                "queryClassId",
                                (object) "MES_UI_PPS"
                            },
                            {
                                "paramList",
                                (object) new Dictionary<string, object>()
                                {

                                    {
                                        "@PLANT",
                                        (object) "1130"
                                    },
                                    {
                                        "@SPECNR",
                                        (object) tool
                                    }
                                }
                            },
                            {
                                "returnId",
                                (object) "LOTSPECLIST"
                            }
                        }
                    }
                };

                var dt = client.ExecCommand(qrymsg).DATASET.Tables["LOTSPECLIST"];

                var dt2 = dt.DefaultView.ToTable(false,
                    new string[]
                    {
                        "WORKSIZEX", "WORKSIZEY", "ARRAYX", "ARRAYY", "PCPPANEL", "UNITSIZEX", "UNITSIZEY", "UPNR", "UPCOL","UPROW","UPBLOCK","STPDAT"
                    });

                MesSpecInfo resultinfo = dt2.AsEnumerable().Select(row =>
                    new MesSpecInfo
                    {
                        Worksizex = Convert.ToInt16(row.Field<string>("WORKSIZEX")),
                        Worksizey = Convert.ToInt16(row.Field<string>("WORKSIZEY")),
                        Arrayx = Convert.ToInt16(row.Field<string>("ARRAYX")),
                        Arrayy = Convert.ToInt16(row.Field<string>("ARRAYY")),
                        Pcppanel = Convert.ToInt16(row.Field<string>("PCPPANEL")),
                        Unitsizex = Convert.ToDecimal(row.Field<string>("UNITSIZEX")),
                        Unitsizey = Convert.ToDecimal(row.Field<string>("UNITSIZEY")),
                        PcsPerStrip = Convert.ToInt16(row.Field<string>("UPNR")),
                        StripArrayBlock = Convert.ToInt16(row.Field<string>("UPBLOCK")),
                        StripArrayCol = Convert.ToInt16(row.Field<string>("UPCOL")),
                        StripArrayRow = Convert.ToInt16(row.Field<string>("UPROW")),
                        CreateDate = row.Field<string>("STPDAT")
                    }).First();

                client.Close();
                return resultinfo;
            }
            catch (Exception ex)
            {
                client.Abort();
                return null;
                throw;
            }
        }
        public List<MesLayerInfo>? MesLayerInfoQry(string lot, string tool)
        {
            var client = new MesRuleServiceOIClient();
            try
            {
                var qrymsg = new MessageData()
                {
                    COMMAND = "ExecuteProcedure",
                    TID = Guid.NewGuid().ToString(),
                    USERID = "103518",
                    IPADDRESS = "192.168.0.20",
                    SITEID = "1130",
                    DATALIST = new List<Dictionary<string, object>>()
                    {
                        new Dictionary<string, object>()
                        {
                            {
                                "queryId",
                                (object) "GetLayerList"
                            },
                            {
                                "queryVersion",
                                (object) "00003"
                            },
                            {
                                "queryClassId",
                                (object) "MES_UI_PPS"
                            },
                            {
                                "paramList",
                                (object) new Dictionary<string, object>()
                                {

                                    {
                                        "@PLANT",
                                        (object) "1130"
                                    },
                                    {
                                        "@LOTID",
                                        (object) lot
                                    },
                                    {
                                        "@SPECNR",
                                        (object) tool
                                    }
                                }
                            },
                            {
                                "returnId",
                                (object) "LOTLAYERLIST"
                            }
                        }
                    }
                };

                var dt = client.ExecCommand(qrymsg).DATASET.Tables["LOTLAYERLIST"];

                List<MesLayerInfo> resultinfo = dt.AsEnumerable().Select(row =>
                    new MesLayerInfo
                    {
                        InsulThickness = row.Field<string>("MATNRTIKT"),
                        CopperThickness = row.Field<string>("COPPERTIKT"),
                        MaterialInfo =new MaterialInfo()
                        {
                            BomNumber=row.Field<string>("BOMNR"),
                            FromLayer = row.Field<string>("LAYER1"),
                            ToLayer = row.Field<string>("LAYER2"),
                            MaterialType = row.Field<string>("RAWGR"),
                            MaterialCode = row.Field<string>("MaterialCode"),
                            MaterialName = row.Field<string>("MAKTX"),
                            MaterialMaker = row.Field<string>("MAKER"),
                            MaterialSpec1 = row.Field<string>("SPEC"),
                            MaterialSpec2 = row.Field<string>("SPEC2"),
                            MaterialSize = row.Field<string>("ZSIZE"),
                            MaterialThickness = row.Field<string>("MATNRTIK"),
                            CopperThickness1 = row.Field<string>("COPPERTIK"),
                            CopperThickness2 = row.Field<string>("COPPERTIK2")
                        }
                    }).ToList();

                client.Close();
                return resultinfo;
            }
            catch (Exception ex)
            {
                client.Abort();
                return null;
                throw;
            }
        }
        public DataSet MesHoleInfoQryDS(string lot, string tool)
        {
            var client = new MesRuleServiceOIClient();
            try
            {
                var qrymsg = new MessageData()
                {
                    COMMAND = "ExecuteProcedure",
                    TID = Guid.NewGuid().ToString(),
                    USERID = "103518",
                    IPADDRESS = "192.168.0.20",
                    SITEID = "1130",
                    DATALIST = new List<Dictionary<string, object>>()
                    {
                        new Dictionary<string, object>()
                        {
                            {
                                "queryId",
                                (object) "GetDrillInfoList"
                            },
                            {
                                "queryVersion",
                                (object) "00003"
                            },
                            {
                                "queryClassId",
                                (object) "MES_UI_PPS"
                            },
                            {
                                "paramList",
                                (object) new Dictionary<string, object>()
                                {

                                    {
                                        "@PLANT",
                                        (object) "1130"
                                    },
                                    {
                                        "@LOTID",
                                        (object) lot
                                    },
                                    {
                                        "@SPECNR",
                                        (object) tool
                                    }
                                }
                            },
                            {
                                "returnId",
                                (object) "DRILLINFO"
                            }
                        }
                    }
                };

                var resultinfo = client.ExecCommand(qrymsg).DATASET;



                client.Close();
                return resultinfo;
            }
            catch (Exception ex)
            {
                client.Abort();
                throw;
            }
        }
        public List<MesHoleInfo> MesHoleInfoQry(string lot, string tool)
        {
            var client = new MesRuleServiceOIClient();
            try
            {
                var qrymsg = new MessageData()
                {
                    COMMAND = "ExecuteProcedure",
                    TID = Guid.NewGuid().ToString(),
                    USERID = "103518",
                    IPADDRESS = "192.168.0.20",
                    SITEID = "1130",
                    DATALIST = new List<Dictionary<string, object>>()
                    {
                        new Dictionary<string, object>()
                        {
                            {
                                "queryId",
                                (object) "GetDrillInfoList"
                            },
                            {
                                "queryVersion",
                                (object) "00003"
                            },
                            {
                                "queryClassId",
                                (object) "MES_UI_PPS"
                            },
                            {
                                "paramList",
                                (object) new Dictionary<string, object>()
                                {

                                    {
                                        "@PLANT",
                                        (object) "1130"
                                    },
                                    {
                                        "@LOTID",
                                        (object) lot
                                    },
                                    {
                                        "@SPECNR",
                                        (object) tool
                                    }
                                }
                            },
                            {
                                "returnId",
                                (object) "DRILLINFO"
                            }
                        }
                    }
                };

                var ds = client.ExecCommand(qrymsg).DATASET;


                List<MesHoleInfo> resultinfo = ds.Tables[2].AsEnumerable().Select(row =>
                    new MesHoleInfo
                    {
                        ProcSeq = row.Field<string>("PROCSEQ"),
                        ProcLayerFrom = row.Field<string>("PRLAYERFR"),
                        ProcLayerTo = row.Field<string>("PRLAYERTO"),
                        ProcCode = row.Field<string>("PROCCD"),
                        ProcName = row.Field<string>("PROCNM"),
                        TotalLayerFrom = row.Field<string>("LAYERFR"),
                        TotalLayerTo = row.Field<string>("LAYERTO"),
                        LaserType = row.Field<string>("SPPROC"),
                        CapturePadSize = row.Field<string>("SVHPAD"),
                        LaserShot = row.Field<string>("SHOT"),
                        HoleCount = row.Field<string>("ZSUM1"),
                        HoleSizeTop = row.Field<string>("TOP1"),
                        HoleSizeBot = row.Field<string>("Bottom1")
                    }).ToList();

                client.Close();
                return resultinfo;
            }
            catch (Exception ex)
            {
                client.Abort();
                throw;
            }
        }
        public MesLotDetailInfo? MesLotDetailInfoQry(string lot, string tool)
        {
            var client = new MesRuleServiceOIClient();
            try
            {
                var qrymsg = new MessageData()
                {
                    COMMAND = "ExecuteProcedure",
                    TID = Guid.NewGuid().ToString(),
                    USERID = "103518",
                    IPADDRESS = "192.168.0.20",
                    SITEID = "1130",
                    DATALIST = new List<Dictionary<string, object>>()
                    {
                        new Dictionary<string, object>()
                        {
                            {
                                "queryId",
                                (object) "GetLotDetailInfoList"
                            },
                            {
                                "queryVersion",
                                (object) "00008"
                            },
                            {
                                "queryClassId",
                                (object) "MES_UI_COM"
                            },
                            {
                                "paramList",
                                (object) new Dictionary<string, object>()
                                {

                                    {
                                        "@PLANT",
                                        (object) "1130"
                                    },
                                    {
                                        "@LOTID",
                                        (object) lot
                                    },
                                    {
                                        "@LANGUAGE",
                                        (object) "KOR"
                                    },
                                    {
                                        "@LOTTYPE",
                                        (object) "LOT_ID"
                                    }
                                }
                            },
                            {
                                "returnId",
                                (object) "LOTINFOLIST"
                            }
                        }
                    }
                };

                var dt = client.ExecCommand(qrymsg).DATASET.Tables["LOTINFOLIST"];

                var dt2 = dt.DefaultView.ToTable(false,
                    new string[]
                    {
                        "SPECNR", "LAYERNR", "SPECTYPE", "CONMETHOD", "PRDLAYER", "KDMAT", "KNAME", "SHIPTONAME"
                    });

                MesLotDetailInfo resultinfo = dt2.AsEnumerable().Select(row =>
                    new MesLotDetailInfo
                    {
                        ToolNo = row.Field<string>("SPECNR"),
                        LayerTotal = Convert.ToInt16(row.Field<string>("LAYERNR")),
                        SpecType1 = row.Field<string>("SPECTYPE"),
                        SpecType2 = row.Field<string>("CONMETHOD"),
                        ProductType = row.Field<string>("PRDLAYER"),
                        ModelName = row.Field<string>("KDMAT").Split(';')[0],
                        ModelRev = row.Field<string>("KDMAT").Split(';')[1].Trim(),
                        Kname = row.Field<string>("KNAME"),
                        Shipto = row.Field<string>("SHIPTONAME")
                    }).First();

                client.Close();
                return resultinfo;
            }
            catch (Exception ex)
            {
                client.Abort();
                return null;
                throw;
            }
        }

        public MesLotInfo MesLotInfoQry(string lot,string tool)
        {
            var client = new MesRuleServiceOIClient();

            try
            {
                DataSet dataSet2 = client.ExecCommand(new MessageData()
                {
                    COMMAND = "GetDataByQueryIDMulti",
                    TID = Guid.NewGuid().ToString(),
                    USERID = "103518",
                    IPADDRESS = "192.168.0.1",
                    SITEID = "1130",
                    DATALIST = new List<Dictionary<string, object>>()
                    {
                        new Dictionary<string, object>()
                        {
                            {
                                "queryId",
                                (object)"GetLotInfo"
                            },
                            {
                                "queryVersion",
                                (object)"00105"
                            },
                            {
                                "queryClassId",
                                (object)"MES_UI_COM"
                            },
                            {
                                "paramList",
                                (object)new Dictionary<string, object>()
                                {
                                    {
                                        "@lang",
                                        (object)"KOR"
                                    },
                                    {
                                        "@lotid",
                                        (object)lot
                                    },
                                    {
                                        "@SITEID",
                                        (object)"1130"
                                    }
                                }
                            },
                            {
                                "returnId",
                                (object)"LOT"
                            }
                        }
                    }
                }).DATASET;

                var dt2 = dataSet2.Tables["LOT"].DefaultView.ToTable(false, new string[] { "specnr", "parentbumlotid", "processsegmentid", "location", "pannelqty", "sqaremeter", "kdmat", "kname" });

                MesLotInfo resultinfo = dt2.AsEnumerable().Select(row =>
                                                new MesLotInfo
                                                {
                                                    ToolNo = row.Field<string>("specnr"),
                                                    RootLotId = row.Field<string>("parentbumlotid"),
                                                    ProcessSegmentId = row.Field<string>("processsegmentid"),
                                                    Location = row.Field<string>("location"),
                                                    PanelQty = row.Field<double>("pannelqty").ToString(),
                                                    SquareMeter = row.Field<double>("sqaremeter").ToString(),
                                                    ModelNameFull = row.Field<string>("kdmat"),
                                                    CustomerName = row.Field<string>("kname")
                                                }).First();
                client.Close();
                return resultinfo;
            }

            catch (Exception ex)
            {
                client.Abort();
                throw;
            }
        }
        public DataTable getRcvlotListDataTable()
        {
            var client = new MesRuleServiceOIClient();

            try
            {
                var qrymsg = new MessageData()
                {
                    COMMAND = "ExecuteProcedure",
                    TID = Guid.NewGuid().ToString(),
                    USERID = "103518",
                    IPADDRESS = "192.168.0.20",
                    SITEID = "1130",
                    DATALIST = new List<Dictionary<string, object>>()
                    {
                        new Dictionary<string, object>()
                        {
                            {
                                "queryId",
                                (object) "GetMainReceiveList"
                            },
                            {
                                "queryVersion",
                                (object) "00101"
                            },
                            {
                                "queryClassId",
                                (object) "MES_UI_PMS"
                            },
                            {
                                "paramList",
                                (object) new Dictionary<string, object>()
                                {

                                    {
                                        "@BUSINESSUNIT",
                                        (object) "DPCENTER"
                                    },
                                    {
                                        "@SITEID",
                                        (object) "1130"
                                    },
                                    {
                                        "@USERID",
                                        (object) "103518"
                                    },
                                    {
                                        "@PROCCD",
                                        (object) "R0270,R0290,R0292,R0295"
                                    },
                                    {
                                        "@ORDERTYPE ",
                                        (object) ""
                                    },
                                    {
                                        "@KUNNRIN",
                                        (object) "IN"
                                    },
                                    {
                                        "@KUNNR",
                                        (object) ""
                                    },
                                    {
                                        "@VERIDIN",
                                        (object) "IN"
                                    },
                                    {
                                        "@VERID",
                                        (object) ""
                                    },
                                    {
                                        "@LOTID",
                                        (object) ""
                                    },
                                    {
                                        "@COMPANYID",
                                        (object) "103518"
                                    }
                                }
                            },
                            {
                                "returnId",
                                (object) "LotList"
                            }
                        }
                    }
                };

                var resultWorkcenter = client.ExecCommand(qrymsg).DATASET;

                DataTable dt = resultWorkcenter.Tables["LotList"];

                client.Close();
                return dt;
            }
            catch (Exception ex)
            {
                client.Abort();
                throw;
            }
        }
        public DataTable qryWorkcenterDataTable()
        {

            var client = new MesRuleServiceOIClient();
            try
            {
                var qrymsg = new MessageData()
                {
                    COMMAND = "ExecuteProcedure",
                    TID = Guid.NewGuid().ToString(),
                    USERID = "103518",
                    IPADDRESS = "192.168.0.20",
                    SITEID = "1130",
                    DATALIST = new List<Dictionary<string, object>>()
                    {
                        new Dictionary<string, object>()
                        {
                            {
                                "queryId",
                                (object) "GetWorkcenterList"
                            },
                            {
                                "queryVersion",
                                (object) "00100"
                            },
                            {
                                "queryClassId",
                                (object) "MES_UI_COM"
                            },
                            {
                                "paramList",
                                (object) new Dictionary<string, object>()
                                {

                                    {
                                        "@LIFNR",
                                        (object) "103518"
                                    },
                                    {
                                        "BUSINESSUNIT",
                                        (object) "DPCENTER,PKG2"
                                    }
                                    ,
                                    {
                                        "FACILITYTYPE",
                                        (object) "WORKCENTER"
                                    }
                                    ,
                                    {
                                        "SITEID",
                                        (object) "1130"
                                    }
                                    ,
                                    {
                                        "LANGUAGE",
                                        (object) "KOR"
                                    }
                                }
                            },
                            {
                                "returnId",
                                (object) "DataList"
                            }
                        }
                    }
                };

                var resultWorkcenter = client.ExecCommand(qrymsg).DATASET;

                DataTable dt = resultWorkcenter.Tables["DataList"];

                client.Close();
                return dt;
            }
            catch (Exception ex)
            {
               
                client.Abort();
                throw;
            }
        }
    }
}

