﻿//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.42000
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConnectorDEMS {
    using System;
    
    
    /// <summary>
    ///   지역화된 문자열 등을 찾기 위한 강력한 형식의 리소스 클래스입니다.
    /// </summary>
    // 이 클래스는 ResGen 또는 Visual Studio와 같은 도구를 통해 StronglyTypedResourceBuilder
    // 클래스에서 자동으로 생성되었습니다.
    // 멤버를 추가하거나 제거하려면 .ResX 파일을 편집한 다음 /str 옵션을 사용하여 ResGen을
    // 다시 실행하거나 VS 프로젝트를 다시 빌드하십시오.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class XmlResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal XmlResources() {
        }
        
        /// <summary>
        ///   이 클래스에서 사용하는 캐시된 ResourceManager 인스턴스를 반환합니다.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ConnectorDEMS.XmlResources", typeof(XmlResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   이 강력한 형식의 리소스 클래스를 사용하여 모든 리소스 조회에 대해 현재 스레드의 CurrentUICulture 속성을
        ///   재정의합니다.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   &lt;?xml version=&quot;1.0&quot; encoding=&quot;UTF-8&quot;?&gt;
        ///&lt;message&gt;
        ///	&lt;header&gt;
        ///		&lt;messagename&gt;CancelMoveLineReceive&lt;/messagename&gt;
        ///		&lt;transactionid&gt;20211224083613496274&lt;/transactionid&gt;
        ///	&lt;/header&gt;
        ///	&lt;body&gt;
        ///		&lt;EVENTUSER&gt;103518&lt;/EVENTUSER&gt;
        ///		&lt;EVENTCOMMENT /&gt;
        ///		&lt;LANGUAGE&gt;KOR&lt;/LANGUAGE&gt;
        ///		&lt;FACTORYNAME&gt;1150&lt;/FACTORYNAME&gt;
        ///		&lt;SITEID&gt;1150&lt;/SITEID&gt;
        ///		&lt;CANCELMOVELINERECEIVELIST&gt;
        ///			&lt;CANCELMOVELINERECEIVE&gt;
        ///				&lt;SITEID&gt;1150&lt;/SITEID&gt;
        ///				&lt;USERID&gt;103518&lt;/USERID&gt;
        ///				&lt;LOTID&gt;12844290001F&lt;/LOTID&gt;
        ///				&lt;FROMLOCATION&gt;103518&lt;/FROMLOC[나머지 문자열은 잘림]&quot;;과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        internal static string Exe_CancelRcvLot {
            get {
                return ResourceManager.GetString("Exe_CancelRcvLot", resourceCulture);
            }
        }
        
        /// <summary>
        ///   &lt;?xml version=&quot;1.0&quot; encoding=&quot;UTF-8&quot;?&gt;
        ///&lt;message&gt;
        ///	&lt;header&gt;
        ///		&lt;messagename&gt;MoveLineReceive&lt;/messagename&gt;
        ///		&lt;transactionid&gt;20211216101848443232&lt;/transactionid&gt;
        ///	&lt;/header&gt;
        ///	&lt;body&gt;
        ///		&lt;EVENTUSER&gt;103518&lt;/EVENTUSER&gt;
        ///		&lt;EVENTCOMMENT /&gt;
        ///		&lt;LANGUAGE&gt;KOR&lt;/LANGUAGE&gt;
        ///		&lt;FACTORYNAME&gt;1150&lt;/FACTORYNAME&gt;
        ///		&lt;SITEID&gt;1150&lt;/SITEID&gt;
        ///		&lt;MOVELINERECEIVELIST&gt;
        ///			&lt;MOVELINERECEIVE&gt;
        ///				&lt;SITEID&gt;1150&lt;/SITEID&gt;
        ///				&lt;USERID&gt;103518&lt;/USERID&gt;
        ///				&lt;LOTID&gt;12838640038F&lt;/LOTID&gt;
        ///				&lt;LOCATION&gt;103518&lt;/LOCATION&gt;
        ///				&lt;COMMENTS&gt;&lt;/CO[나머지 문자열은 잘림]&quot;;과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        internal static string Exe_RcvLot {
            get {
                return ResourceManager.GetString("Exe_RcvLot", resourceCulture);
            }
        }
        
        /// <summary>
        ///   &lt;?xml version=&quot;1.0&quot; encoding=&quot;UTF-8&quot;?&gt;
        ///&lt;message&gt;
        ///	&lt;header&gt;
        ///		&lt;messagename&gt;GetQueryResult&lt;/messagename&gt;
        ///		&lt;transactionid&gt;20211213105340379297&lt;/transactionid&gt;
        ///	&lt;/header&gt;
        ///	&lt;body&gt;
        ///		&lt;EVENTUSER&gt;103518&lt;/EVENTUSER&gt;
        ///		&lt;EVENTCOMMENT /&gt;
        ///		&lt;LANGUAGE&gt;KOR&lt;/LANGUAGE&gt;
        ///		&lt;QUERYID&gt;GetCboWorkCenterByVendor&lt;/QUERYID&gt;
        ///		&lt;VERSION&gt;1&lt;/VERSION&gt;
        ///		&lt;BINDV&gt;
        ///			&lt;SITEID&gt;1150&lt;/SITEID&gt;
        ///			&lt;FACTORYID&gt;FPC&lt;/FACTORYID&gt;
        ///			&lt;VENDORID&gt;103518&lt;/VENDORID&gt;
        ///		&lt;/BINDV&gt;
        ///	&lt;/body&gt;
        ///&lt;/message&gt;과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        internal static string Qry_CboWorkCenter {
            get {
                return ResourceManager.GetString("Qry_CboWorkCenter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   &lt;?xml version=&quot;1.0&quot; encoding=&quot;UTF-8&quot;?&gt;
        ///&lt;message&gt;
        ///	&lt;header&gt;
        ///		&lt;messagename&gt;GetQueryResult&lt;/messagename&gt;
        ///		&lt;transactionid&gt;&lt;/transactionid&gt;
        ///	&lt;/header&gt;
        ///	&lt;body&gt;
        ///		&lt;EVENTUSER&gt;FPCBB4&lt;/EVENTUSER&gt;
        ///		&lt;EVENTCOMMENT /&gt;
        ///		&lt;LANGUAGE&gt;KOR&lt;/LANGUAGE&gt;
        ///		&lt;QUERYID&gt;GetHollInfoList&lt;/QUERYID&gt;
        ///		&lt;VERSION&gt;1&lt;/VERSION&gt;
        ///		&lt;BINDV&gt;
        ///			&lt;SPECNR&gt;FSA03827-02&lt;/SPECNR&gt;
        ///			&lt;SEQNR&gt;&lt;/SEQNR&gt;
        ///		&lt;/BINDV&gt;
        ///	&lt;/body&gt;
        ///&lt;/message&gt;과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        internal static string Qry_HoleInfoList {
            get {
                return ResourceManager.GetString("Qry_HoleInfoList", resourceCulture);
            }
        }
        
        /// <summary>
        ///   &lt;?xml version=&quot;1.0&quot; encoding=&quot;UTF-8&quot;?&gt;
        ///&lt;message&gt;
        ///	&lt;header&gt;
        ///		&lt;messagename&gt;GetQueryResult&lt;/messagename&gt;
        ///		&lt;transactionid&gt;&lt;/transactionid&gt;
        ///	&lt;/header&gt;
        ///	&lt;body&gt;
        ///		&lt;EVENTUSER&gt;103518&lt;/EVENTUSER&gt;
        ///		&lt;EVENTCOMMENT /&gt;
        ///		&lt;LANGUAGE&gt;KOR&lt;/LANGUAGE&gt;
        ///		&lt;QUERYID&gt;GetFictionalList&lt;/QUERYID&gt;
        ///		&lt;VERSION&gt;2&lt;/VERSION&gt;
        ///		&lt;BINDV&gt;
        ///			&lt;SPECNR&gt;22RFP003-01&lt;/SPECNR&gt;
        ///			&lt;SEQNR&gt;&lt;/SEQNR&gt;
        ///		&lt;/BINDV&gt;
        ///	&lt;/body&gt;
        ///&lt;/message&gt;과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        internal static string Qry_LBodyCut {
            get {
                return ResourceManager.GetString("Qry_LBodyCut", resourceCulture);
            }
        }
        
        /// <summary>
        ///   &lt;?xml version=&quot;1.0&quot; encoding=&quot;UTF-8&quot;?&gt;
        ///&lt;message&gt;
        ///	&lt;header&gt;
        ///		&lt;messagename&gt;GetQueryResult&lt;/messagename&gt;
        ///		&lt;transactionid&gt;&lt;/transactionid&gt;
        ///	&lt;/header&gt;
        ///	&lt;body&gt;
        ///		&lt;EVENTUSER&gt;FPCBB4&lt;/EVENTUSER&gt;
        ///		&lt;EVENTCOMMENT /&gt;
        ///		&lt;LANGUAGE&gt;KOR&lt;/LANGUAGE&gt;
        ///		&lt;QUERYID&gt;GetLDrillInfoList&lt;/QUERYID&gt;
        ///		&lt;VERSION&gt;2&lt;/VERSION&gt;
        ///		&lt;BINDV&gt;
        ///			&lt;SPECNR&gt;&lt;/SPECNR&gt;
        ///      &lt;LOTID&gt;&lt;/LOTID&gt;
        ///			&lt;SEQNR&gt;&lt;/SEQNR&gt;
        ///      &lt;PROCSEQ&gt;&lt;/PROCSEQ&gt;
        ///		&lt;/BINDV&gt;
        ///	&lt;/body&gt;
        ///&lt;/message&gt;과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        internal static string Qry_LHoleInfoList {
            get {
                return ResourceManager.GetString("Qry_LHoleInfoList", resourceCulture);
            }
        }
        
        /// <summary>
        ///   &lt;?xml version=&quot;1.0&quot; encoding=&quot;UTF-8&quot;?&gt;&lt;message&gt;
        ///	&lt;header&gt;
        ///		&lt;messagename&gt;GetQueryResult&lt;/messagename&gt;
        ///		&lt;transactionid&gt;&lt;/transactionid&gt;
        ///	&lt;/header&gt;
        ///	&lt;body&gt;
        ///		&lt;EVENTUSER&gt;FPCBB4&lt;/EVENTUSER&gt;
        ///		&lt;EVENTCOMMENT /&gt;
        ///		&lt;LANGUAGE&gt;KOR&lt;/LANGUAGE&gt;
        ///		&lt;QUERYID&gt;GetLotDetailInfoList&lt;/QUERYID&gt;
        ///		&lt;VERSION&gt;14&lt;/VERSION&gt;
        ///		&lt;BINDV&gt;
        ///			&lt;PLANT&gt;1150&lt;/PLANT&gt;
        ///			&lt;LOTID&gt;12838390006F&lt;/LOTID&gt;
        ///			&lt;ITS_LOTID&gt;&lt;/ITS_LOTID&gt;
        ///			&lt;DELIVERYLOTID&gt;&lt;/DELIVERYLOTID&gt;
        ///			&lt;SPECNR&gt;&lt;/SPECNR&gt;
        ///		&lt;/BINDV&gt;
        ///	&lt;/body&gt;
        ///&lt;/message&gt;과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        internal static string Qry_LotDetailInfo {
            get {
                return ResourceManager.GetString("Qry_LotDetailInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   &lt;?xml version=&quot;1.0&quot; encoding=&quot;UTF-8&quot;?&gt;
        ///&lt;message&gt;
        ///	&lt;header&gt;
        ///		&lt;messagename&gt;GetQueryResult&lt;/messagename&gt;
        ///		&lt;transactionid&gt;&lt;/transactionid&gt;
        ///	&lt;/header&gt;
        ///	&lt;body&gt;
        ///		&lt;EVENTUSER&gt;FPCBB4&lt;/EVENTUSER&gt;
        ///		&lt;EVENTCOMMENT /&gt;
        ///		&lt;LANGUAGE&gt;KOR&lt;/LANGUAGE&gt;
        ///		&lt;QUERYID&gt;GetLotDetailInfoList&lt;/QUERYID&gt;
        ///		&lt;VERSION&gt;13&lt;/VERSION&gt;
        ///		&lt;BINDV&gt;
        ///			&lt;LOTID&gt;12729320028F&lt;/LOTID&gt;
        ///			&lt;SPECNR&gt;&lt;/SPECNR&gt;
        ///			&lt;SEQNR&gt;&lt;/SEQNR&gt;
        ///		&lt;/BINDV&gt;
        ///	&lt;/body&gt;
        ///&lt;/message&gt;과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        internal static string Qry_LotDetailInfo_13 {
            get {
                return ResourceManager.GetString("Qry_LotDetailInfo_13", resourceCulture);
            }
        }
        
        /// <summary>
        ///   &lt;message&gt;
        ///	&lt;header&gt;
        ///		&lt;messagename&gt;GetQueryResult&lt;/messagename&gt;
        ///		&lt;sourcesubject&gt;&lt;/sourcesubject&gt;
        ///		&lt;targetsubject&gt;&lt;/targetsubject&gt;
        ///		&lt;transactionid&gt;20211214132537837883&lt;/transactionid&gt;
        ///	&lt;/header&gt;
        ///	&lt;body&gt;
        ///		&lt;QUERYID&gt;GetLotDetailInfoList&lt;/QUERYID&gt;
        ///		&lt;VERSION&gt;13&lt;/VERSION&gt;
        ///		&lt;DATALIST&gt;
        ///			&lt;DATA&gt;
        ///				&lt;SPECNR&gt;
        ///					&lt;![CDATA[FLA00293-18]]&gt;
        ///				&lt;/SPECNR&gt;
        ///				&lt;SEQNR&gt;
        ///					&lt;![CDATA[8]]&gt;
        ///				&lt;/SEQNR&gt;
        ///				&lt;DISKNR&gt;
        ///					&lt;![CDATA[1000048612]]&gt;
        ///				&lt;/DISKNR&gt;
        ///				&lt;LAYERNR&gt;
        ///					&lt;![CDATA[4]]&gt;
        ///				&lt;/L[나머지 문자열은 잘림]&quot;;과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        internal static string Qry_LotDetailInfo_결과 {
            get {
                return ResourceManager.GetString("Qry_LotDetailInfo_결과", resourceCulture);
            }
        }
        
        /// <summary>
        ///   &lt;?xml version=&quot;1.0&quot; encoding=&quot;UTF-8&quot;?&gt;
        ///&lt;message&gt;
        ///	&lt;header&gt;
        ///		&lt;messagename&gt;GetQueryResult&lt;/messagename&gt;
        ///		&lt;transactionid&gt;&lt;/transactionid&gt;
        ///	&lt;/header&gt;
        ///	&lt;body&gt;
        ///		&lt;EVENTUSER&gt;FPCBB4&lt;/EVENTUSER&gt;
        ///		&lt;EVENTCOMMENT /&gt;
        ///		&lt;LANGUAGE&gt;KOR&lt;/LANGUAGE&gt;
        ///		&lt;QUERYID&gt;GetLot&lt;/QUERYID&gt;
        ///		&lt;VERSION&gt;2&lt;/VERSION&gt;
        ///		&lt;BINDV&gt;
        ///			&lt;LOTID&gt;&lt;/LOTID&gt;
        ///		&lt;/BINDV&gt;
        ///	&lt;/body&gt;
        ///&lt;/message&gt;과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        internal static string Qry_LotInfo {
            get {
                return ResourceManager.GetString("Qry_LotInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   &lt;?xml version=&quot;1.0&quot; encoding=&quot;UTF-8&quot;?&gt;
        ///&lt;message&gt;
        ///	&lt;header&gt;
        ///		&lt;messagename&gt;GetStoredProcedureResult&lt;/messagename&gt;
        ///		&lt;transactionid&gt;&lt;/transactionid&gt;
        ///	&lt;/header&gt;
        ///	&lt;body&gt;
        ///		&lt;EVENTUSER&gt;103518&lt;/EVENTUSER&gt;
        ///		&lt;EVENTCOMMENT /&gt;
        ///		&lt;LANGUAGE&gt;KOR&lt;/LANGUAGE&gt;
        ///		&lt;QUERYID&gt;GetLotList&lt;/QUERYID&gt;
        ///		&lt;VERSION&gt;35&lt;/VERSION&gt;
        ///		&lt;BINDV&gt;
        ///			&lt;LANGUAGE&gt;Korean&lt;/LANGUAGE&gt;
        ///			&lt;SITEID&gt;1150&lt;/SITEID&gt;
        ///			&lt;FACTORYID&gt;FPC&lt;/FACTORYID&gt;
        ///			&lt;WORKCENTERID&gt;FPCBB4&lt;/WORKCENTERID&gt;
        ///			&lt;PROCESSSEGMENTID&gt;&lt;/PROCESSSEGMENTID&gt;
        ///			&lt;LOTID&gt;&lt;/LOTID&gt;[나머지 문자열은 잘림]&quot;;과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        internal static string Qry_Lotlist {
            get {
                return ResourceManager.GetString("Qry_Lotlist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   &lt;?xml version=&quot;1.0&quot; encoding=&quot;UTF-8&quot;?&gt;
        ///&lt;message&gt;
        ///	&lt;header&gt;
        ///		&lt;messagename&gt;GetStoredProcedureResult&lt;/messagename&gt;
        ///		&lt;transactionid&gt;20211216101844247519&lt;/transactionid&gt;
        ///	&lt;/header&gt;
        ///	&lt;body&gt;
        ///		&lt;EVENTUSER&gt;103518&lt;/EVENTUSER&gt;
        ///		&lt;EVENTCOMMENT /&gt;
        ///		&lt;LANGUAGE&gt;KOR&lt;/LANGUAGE&gt;
        ///		&lt;QUERYID&gt;GetRecieveLot&lt;/QUERYID&gt;
        ///		&lt;VERSION&gt;9&lt;/VERSION&gt;
        ///		&lt;BINDV&gt;
        ///			&lt;SITEID&gt;1150&lt;/SITEID&gt;
        ///			&lt;LANG&gt;Korean&lt;/LANG&gt;
        ///			&lt;USERFACTORYID&gt;103518&lt;/USERFACTORYID&gt;
        ///			&lt;USERID&gt;103518&lt;/USERID&gt;
        ///			&lt;FACTORY&gt;FPC&lt;/FACTORY&gt;
        ///			&lt;WORKCENTER&gt;FPCBB[나머지 문자열은 잘림]&quot;;과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        internal static string Qry_RcvLotList {
            get {
                return ResourceManager.GetString("Qry_RcvLotList", resourceCulture);
            }
        }
        
        /// <summary>
        ///   &lt;?xml version=&quot;1.0&quot; encoding=&quot;UTF-8&quot;?&gt;
        ///&lt;message&gt;
        ///	&lt;header&gt;
        ///		&lt;messagename&gt;GetQueryResult&lt;/messagename&gt;
        ///		&lt;transactionid&gt;20211213105609964763&lt;/transactionid&gt;
        ///	&lt;/header&gt;
        ///	&lt;body&gt;
        ///		&lt;EVENTUSER&gt;103518&lt;/EVENTUSER&gt;
        ///		&lt;EVENTCOMMENT /&gt;
        ///		&lt;LANGUAGE&gt;KOR&lt;/LANGUAGE&gt;
        ///		&lt;QUERYID&gt;GetLotSpecInfoList&lt;/QUERYID&gt;
        ///		&lt;VERSION&gt;10&lt;/VERSION&gt;
        ///		&lt;BINDV&gt;
        ///			&lt;SPECNR&gt;FET01374-09&lt;/SPECNR&gt;
        ///			&lt;SEQNR&gt;7&lt;/SEQNR&gt;
        ///		&lt;/BINDV&gt;
        ///	&lt;/body&gt;
        ///&lt;/message&gt;과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        internal static string Qry_SpecInfo {
            get {
                return ResourceManager.GetString("Qry_SpecInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   			&lt;MOVELINERECEIVE&gt;
        ///				&lt;SITEID&gt;1150&lt;/SITEID&gt;
        ///				&lt;USERID&gt;103518&lt;/USERID&gt;
        ///				&lt;LOTID&gt;12855460009F&lt;/LOTID&gt;
        ///				&lt;LOCATION&gt;103518&lt;/LOCATION&gt;
        ///				&lt;COMMENTS&gt;&lt;/COMMENTS&gt;
        ///			&lt;/MOVELINERECEIVE&gt;과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        internal static string RcvLot_Item {
            get {
                return ResourceManager.GetString("RcvLot_Item", resourceCulture);
            }
        }
    }
}
