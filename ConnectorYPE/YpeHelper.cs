using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using System.Data;
using System.Threading.Tasks;
using ConnectorYPE.Models;

namespace ConnectorYPE
{
    public class YpeHelper
    {
        Regex re_yplot = new Regex(@"[0-9]{6}.[0-9]{3}.[0-9].[A-Z]{2}[0-9]{2}.[0-9]{3}.[0-9]{3}");

        string url = "http://172.16.4.70:7010/do";
        HttpClient client = new HttpClient();

        JObject qry_wip_json = JObject.Parse(Encoding.UTF8.GetString(ResourceYPE.Qry_WIP));
        JObject qry_worker_json = JObject.Parse(Encoding.UTF8.GetString(ResourceYPE.Qry_Worker));
        JObject qry_equip_json = JObject.Parse(Encoding.UTF8.GetString(ResourceYPE.Qry_Equip));
        JObject qry_lotinfo_json = JObject.Parse(Encoding.UTF8.GetString(ResourceYPE.Qry_Lotinfo));
        JObject save_rcv_json = JObject.Parse(Encoding.UTF8.GetString(ResourceYPE.Save_Rcv));
        JObject save_start_json = JObject.Parse(Encoding.UTF8.GetString(ResourceYPE.Save_Start));
        JObject save_end_json = JObject.Parse(Encoding.UTF8.GetString(ResourceYPE.Save_End));
        JObject save_send_json = JObject.Parse(Encoding.UTF8.GetString(ResourceYPE.Save_Send));
        JObject qry_warein_json = JObject.Parse(Encoding.UTF8.GetString(ResourceYPE.Qry_WareIn));
        JObject qry_wareout_json = JObject.Parse(Encoding.UTF8.GetString(ResourceYPE.Qry_WareOut));
        JObject save_wareout_json = JObject.Parse(Encoding.UTF8.GetString(ResourceYPE.Save_WareOut));
        JObject save_warein_json = JObject.Parse(Encoding.UTF8.GetString(ResourceYPE.Save_WareIn));
        JObject save_specchg_json = JObject.Parse(Encoding.UTF8.GetString(ResourceYPE.Save_SpecChg));
        JObject qry_specchg_json = JObject.Parse(Encoding.UTF8.GetString(ResourceYPE.Qry_SpecChg));
        JObject qry_prdinfo_json = JObject.Parse(Encoding.UTF8.GetString(ResourceYPE.QryProductInfo));
        JObject qry_routinginfo_json = JObject.Parse(Encoding.UTF8.GetString(ResourceYPE.QryRoutingInfo));
        JObject qry_specinfo_json = JObject.Parse(Encoding.UTF8.GetString(ResourceYPE.Qry_SpecInfo));
        JObject qry_materialinfo_json = JObject.Parse(Encoding.UTF8.GetString(ResourceYPE.Qry_MaterialInfo));
        JObject qry_spec_comment_json = JObject.Parse(Encoding.UTF8.GetString(ResourceYPE.Qry_SpecComment));
        JObject qry_routingresource_json = JObject.Parse(Encoding.UTF8.GetString(ResourceYPE.Qry_RoutingResource));

        Regex revreg = new Regex(@"REV.(\w+)");
        Regex holesizereg = new Regex(@"Φ(\d.\d+)");
        Regex layerfromreg = new Regex(@"LASER.(\d+).\d+L.");
        Regex layertoreg = new Regex(@"LASER.\d.(\d+)L.");
        Regex picutlayerfromreg = new Regex(@"CUT[0-9]?.(\d+L[-]?[0-9]?).*?[\d+L]?[-]?[0-9]?.*\)");
        Regex picutlayertoreg = new Regex(@"CUT[0-9]?.\d+L[-]?[0-9]?.*?(\d+L[-]?[0-9]?).*\)");

        public async Task<JObject> Post(JObject post_json)
        {
            var data = new StringContent(post_json.ToString(), Encoding.UTF8, "application/json");
            var retrived = await client.PostAsync(url, data).ConfigureAwait(false);
            string result = await retrived.Content.ReadAsStringAsync().ConfigureAwait(false);
            var result_json = JObject.Parse(result);
            return result_json;
        }


        public string GetComment(string lot)
        {
            var result = string.Empty;
            return result;
        }

        public async Task<DataTable> QryWipDT()
        {
            var dt = new DataTable();

            //공정코드 적용
            qry_wip_json["body"]["result"]["parameter"]["p_PROCESSSEGMENTCLASSID_MIDDLE"] = "*";

            var result = await Post(qry_wip_json);
            //var resultList = JsonConvert.DeserializeObject<List<WipModel>>(result["result"]["data"]["result"]["Rows"].ToString());
            var resultdt = JsonConvert.DeserializeObject<DataTable>(result["result"]["data"]["result"]["Rows"].ToString());
            dt = resultdt;
            //var resultdt_filter = resultdt.DefaultView.ToTable(false, new string[] { "state", "lotid", "processsegmentname", "productdefname", "productdefid", "productrevision", "wiptotalpnl" });
            //resultdt_filter.DefaultView.Sort = "processsegmentname,state ASC";
            //resultdt_filter.Columns["state"].ColumnName = "상태";
            //resultdt_filter.Columns["lotid"].ColumnName = "LOT";
            //resultdt_filter.Columns["processsegmentname"].ColumnName = "공정명";
            //resultdt_filter.Columns["productdefname"].ColumnName = "모델명";
            //resultdt_filter.Columns["productdefid"].ColumnName = "제품코드";
            //resultdt_filter.Columns["productrevision"].ColumnName = "내부REV";
            //resultdt_filter.Columns["wiptotalpnl"].ColumnName = "PNL 수량";  


            //transitstate => 물류창고 입고 상태
            return dt;
        }

        public async Task<List<WipModel>> QryWipList()
        {
            var resultList = new List<WipModel>();

            //공정코드 적용
            qry_wip_json["body"]["result"]["parameter"]["p_PROCESSSEGMENTCLASSID_MIDDLE"] = "*";

            var result = await Post(qry_wip_json);
            resultList = JsonConvert.DeserializeObject<List<WipModel>>(result["result"]["data"]["result"]["Rows"].ToString()); 

            return resultList;
        }

        public async Task<string> QryRoutingResource(string opid)
        {
            var resultString = string.Empty;

            //공정코드 적용
            qry_routingresource_json["body"]["result"]["parameter"]["OPERATIONID"] = opid;

            var result = await Post(qry_routingresource_json);
            var resultList = JsonConvert.DeserializeObject<List<YPRoutingResource>>(result["result"]["data"]["result"]["Rows"].ToString());

            foreach (var item in resultList)
            {
                if (resultString == string.Empty)
                {
                    resultString = item.areaname;
                }
                else
                {
                    resultString += ","+item.areaname;
                }
            }


            return resultString;
        }

        public async Task<List<YPRoutingInfoModel>> QryRoutingInfo(string tool, string subrev)
        {
            var resultList = new List<YPRoutingInfoModel>();

            //공정코드 적용
            qry_routinginfo_json["body"]["result"]["parameter"]["p_PRODUCTDEFID"] = tool;
            qry_routinginfo_json["body"]["result"]["parameter"]["p_PRODUCTDEFVERSION"] = subrev;

            var result = await Post(qry_routinginfo_json);
            resultList = JsonConvert.DeserializeObject<List<YPRoutingInfoModel>>(result["result"]["data"]["result"]["Rows"].ToString());

            return resultList;
        }

        public async Task<List<YPMaterialInfoModel>> QryMaterialInfo(string tool, string subrev)
        {
            var resultList = new List<YPMaterialInfoModel>();

            //공정코드 적용
            qry_materialinfo_json["body"]["result"]["parameter"]["ITEMID"] = tool;
            qry_materialinfo_json["body"]["result"]["parameter"]["ITEMVERSION"] = subrev;

            var result = await Post(qry_materialinfo_json);


            resultList = JsonConvert.DeserializeObject<List<YPMaterialInfoModel>>(result["result"]["data"]["result"]["Rows"].ToString());

            return resultList;
        }
        public async Task<DataTable> QryMaterialInfoDT(string tool, string subrev)
        {
            var resultList = new DataTable();

            //공정코드 적용
            qry_materialinfo_json["body"]["result"]["parameter"]["ITEMID"] = tool;
            qry_materialinfo_json["body"]["result"]["parameter"]["ITEMVERSION"] = subrev;

            var result = await Post(qry_materialinfo_json);


            resultList = JsonConvert.DeserializeObject<DataTable>(result["result"]["data"]["result"]["Rows"].ToString());

            return resultList;
        }
        public async Task<List<YPSpecInfoModel>> QrySpecInfo(string tool, string subrev)
        {
            var resultList = new List<YPSpecInfoModel>();

            //공정코드 적용
            qry_specinfo_json["body"]["result"]["parameter"]["ITEMID"] = tool;
            qry_specinfo_json["body"]["result"]["parameter"]["ITEMVERSION"] = subrev;

            var result = await Post(qry_specinfo_json);
            resultList = JsonConvert.DeserializeObject<List<YPSpecInfoModel>>(result["result"]["data"]["result"]["Rows"].ToString());

            return resultList;
        }

        public async Task<List<YPSpecCommentModel>> QrySpecComment(string tool, string subrev)
        {
            var resultList = new List<YPSpecCommentModel>();

            //공정코드 적용
            qry_specinfo_json["body"]["result"]["parameter"]["ITEMID"] = tool;
            qry_specinfo_json["body"]["result"]["parameter"]["ITEMVERSION"] = subrev;

            var result = await Post(qry_specinfo_json);
            resultList = JsonConvert.DeserializeObject<List<YPSpecCommentModel>>(result["result"]["data"]["result"]["Rows"].ToString());

            return resultList;
        }

        public async Task<List<YPToolInfo>> QryYPToolinfoList(string tool, string subrev)
        {
            var resultList = new List<YPToolInfo>();

            var specinfo = await QrySpecInfo(tool, subrev);
            var materialinfo = await QryMaterialInfo(tool, subrev);
            var speccomment = await QrySpecComment(tool, subrev);
            var routinginfo = await QryRoutingInfo(tool, subrev);
            var uvroutingList = routinginfo.Where(x =>((Convert.ToInt16(x.processsegmentid.Substring(0,4)))>2015 && (Convert.ToInt16(x.processsegmentid.Substring(0, 4))) < 2030)).Select(x=>x.processsegmentid).ToList();

            foreach (var item in uvroutingList)
            {
                var tmpitem = new YPToolInfo();
                
                tmpitem.CreateDate = specinfo[0].registrationdate;
                tmpitem.EndCustomer = specinfo[0].shiptoname;
                tmpitem.Pcs = specinfo[0].pcspnl;
                tmpitem.ArrayBlk = specinfo[0].pcsary;
                tmpitem.WorksizeX = specinfo[0].pnlsizexaxis;
                tmpitem.WorksizeY = specinfo[0].pnlsizeyaxis;
                tmpitem.CustModelname = specinfo[0].itemname;
                tmpitem.CustToolno = specinfo[0].itemid;
                tmpitem.MesRevision = specinfo[0].itemversion;
                tmpitem.PrdCategory = specinfo[0].saleordercategory;
                tmpitem.ProductType = specinfo[0].producttype;
                tmpitem.MesPrcCode = item;
                tmpitem.MesPrcName = routinginfo.Where(x => x.processsegmentid == item).Select(x => x.processsegmentname).FirstOrDefault();
                tmpitem.MesSeqCode = routinginfo.Where(x => x.processsegmentid == item).Select(x => x.usersequence).FirstOrDefault();
                tmpitem.ToolNotes = routinginfo.Where(x => x.processsegmentid == item).Select(x => x.description).FirstOrDefault();
                tmpitem.DataRevision = revreg.Match(tmpitem.ToolNotes).Groups[1].Value;
                tmpitem.MainHoleSize = holesizereg.Match(tmpitem.ToolNotes).Groups[1].Value;
                if (item.Contains("2016"))
                {
                    tmpitem.PrcLayerFrom1 = layerfromreg.Match(tmpitem.ToolNotes).Groups[1].Value;
                    tmpitem.PrcLayerTo1 = layertoreg.Match(tmpitem.ToolNotes).Groups[1].Value;
                }
                else if (item.Contains("2024"))
                {
                    tmpitem.PrcLayerFrom1 = picutlayerfromreg.Match(tmpitem.ToolNotes).Groups[1].Value;
                    tmpitem.PrcLayerTo1 = picutlayerfromreg.Match(tmpitem.ToolNotes).Groups[1].Value;
                    if (picutlayertoreg.IsMatch(tmpitem.ToolNotes))
                    {
                        tmpitem.PrcLayerFrom2 = picutlayertoreg.Match(tmpitem.ToolNotes).Groups[1].Value;
                        tmpitem.PrcLayerTo2 = picutlayertoreg.Match(tmpitem.ToolNotes).Groups[1].Value;
                    }
                    
                }

                var tmpseq = routinginfo.Where(x => x.processsegmentid == item)
                    .Select(x => Convert.ToInt16(x.usersequence)).FirstOrDefault();
                tmpitem.NextOpid = routinginfo.Where(x => Convert.ToInt16(x.usersequence) > tmpseq)
                    .OrderBy(x=> Convert.ToInt16(x.usersequence)).Select(x => x.operationid).FirstOrDefault();
                tmpitem.InsulInfo = materialinfo[0].root_bomid;
                tmpitem.Layer = specinfo[0].layer;
                resultList.Add(tmpitem);
            }

            return resultList;
        }

        private async Task<bool> ExecuteRcv(string lot,string state, string transitstate)
        {
            bool result = false;

            if (state == "인수대기")
            {
                if (transitstate == "물류창고입고대기")
                {

                }
                else if (transitstate == "물류창고입고")
                {

                }
                else if (transitstate == "물류창고출고")
                {

                }
            }

            //var dt = client.ExecCommandAsync(qrymsg).Result.DATASET.Tables["Reply"].Rows[0]["STATE"];
            //if ((bool)dt == true)
            //{ result = true; }

            
            return result;
        }
       
    

    }
}
