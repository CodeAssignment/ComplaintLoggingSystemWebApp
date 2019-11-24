using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ComplaintLoggingSystem.DataModels;
using Microsoft.AspNetCore.Http;

namespace ComplaintLoggingSystem.Services
{
    public class ComplaintDetailsSystem : ServiceAgent, IComplaintDetailsSystem
    {

        IHttpClientFactory _httpClientFactory;
        public ComplaintDetailsSystem(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, httpContextAccessor, UserConstants.CORELIBRARYHTTPCLIENT)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task CreateComplaintDetail(ComplaintDetailForCreationData complaintDetailForCreationData)
        {
            var complaintDetailsUrl = $"complaintDetails";
            await PostData<string>(complaintDetailsUrl,complaintDetailForCreationData);
            
        }

        public async Task DeleteComplaintDetail(Guid complaintId)
        {
            var complaintDetailsUrl = $"complaintDetails/{complaintId}";
            await DeleteData<string>(complaintDetailsUrl);
        }

        public async Task<ComplaintCompleteDetailData> GetComplaintDetail(Guid id)
        {
            var complaintDetailsUrl = $"complaintDetails/{id}/ComplaintDetail";
            var response = await GetData<ComplaintCompleteDetailData>(complaintDetailsUrl);
            return response;
        }

        public async Task<List<ComplaintDetailsData>> GetComplaintDetails(string emailId)
        {
            var complaintDetailsUrl = $"complaintDetails/{emailId}";
            var response = await GetData<List<ComplaintDetailsData>>(complaintDetailsUrl);
            return response;
        }

        

        public async Task UpdateComplaintDetail(Guid complaintId, ComplaintDetailForUpdationData complaintDetailForCreationData)
        {
            var complaintDetailsUrl = $"complaintDetails/{complaintId}";
            await PutData<string>(complaintDetailsUrl, complaintDetailForCreationData);
        }
    }
}
