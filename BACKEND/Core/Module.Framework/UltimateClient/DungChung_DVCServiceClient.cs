using Business.Entities.Domain;
using Core.Common.Utilities;
using Module.Framework.UltimateClient;
using RestSharp;
using System;
using System.Web.Configuration;

namespace Module.Framework
{
    public class DungChung_DVCServiceClient : BaseClient, IDisposable
    {
        private bool _isDisposed;
        public DungChung_DVCServiceClient() : base(AppSetting.DC_DVC)
        {
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!_isDisposed && disposing)
            {
                DisposeCore();
            }
            _isDisposed = true;
        }

        protected virtual void DisposeCore()
        {
        }

        public Response_DVCMap<ChiTietHoSo_DVC> GetChiTietHoSo(long hoSoID)
        {
            var url = WebConfigurationManager.AppSettings["DVC_GetThongTinHoSo"];
            url = url + "/" + hoSoID.ToString();
            var request = new RestRequest(url, Method.GET);
            var restResponse = Execute<Response_DVCMap<ChiTietHoSo_DVC>>(request);
            Response_DVCMap<ChiTietHoSo_DVC> myDeserializedObjList = (Response_DVCMap<ChiTietHoSo_DVC>)Newtonsoft.Json.JsonConvert.DeserializeObject(restResponse.Content, typeof(Response_DVCMap<ChiTietHoSo_DVC>));
            return myDeserializedObjList;
        }
    }
}
