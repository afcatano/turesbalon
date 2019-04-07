using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace CommonsWeb.Util
{
    public class CacheHandler
    {

        private ObjectCache ioc_cache { get; set; }

        public object GetCache(string as_cacheKey, object ao_request)
        {

            object lo_return;

            lo_return = null;

            try
            {

                ioc_cache = MemoryCache.Default;

                if (ioc_cache.Contains(as_cacheKey))
                {

                    Dictionary<object, object> ld_data;

                    ld_data = new Dictionary<object, object>();
                    ld_data = (Dictionary<object, object>)ioc_cache.Get(as_cacheKey);

                    if (ld_data.ContainsKey(ao_request))
                    {

                        if (!ld_data.TryGetValue(ao_request, out lo_return))
                            lo_return = null;

                    }

                }

            }
            catch (Exception ae_e)
            {

                lo_return = null;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR CacheHandler:GetCache");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " :: " + ae_e.Message);

            }

            return lo_return;

        }

        public void AddCache(string as_cacheKey, object ao_list)
        {

            try
            {

                CacheItemPolicy lcip_policy;

                lcip_policy = new CacheItemPolicy();
                lcip_policy.AbsoluteExpiration = DateTime.Now.AddMinutes(3);
                ioc_cache.Add(as_cacheKey, ao_list, lcip_policy);

            }
            catch (Exception ae_e)
            {

                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR CacheHandler:AddCache");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " : " + ae_e.Message);

            }

        }

    }
}