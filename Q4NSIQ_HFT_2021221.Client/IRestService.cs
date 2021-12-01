using System.Collections.Generic;

namespace Q4NSIQ_HFT_2021221.Client
{
    public interface IRestService
    {
        void Delete(int id, string endpoint);
        void Delete<T>(string endpoint);
        T Get<T>(int id, string endpoint);
        List<T> Get<T>(string endpoint);
        T GetSingle<T>(string endpoint);
        void Post<T>(T item, string endpoint);
        void Put<T>(T item, string endpoint);
    }
}