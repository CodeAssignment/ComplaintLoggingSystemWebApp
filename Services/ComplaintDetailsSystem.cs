using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ComplaintLoggingSystem.DataModels;
using ComplaintLoggingSystem.Helpers;
using ComplaintLoggingSystem.Models;
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

        public async Task<string> CreateComplaintDetail(ComplaintDetailForCreationData complaintDetailForCreationData)
        {
            try
            {
                var complaintDetailsUrl = $"complaintDetails";
                await PostData<string>(complaintDetailsUrl, complaintDetailForCreationData);
                return Response.Success.ToString();
            }
            catch
            {
                return Response.Failure.ToString();
            }
        }

        public async Task<string> DeleteComplaintDetail(Guid complaintId)
        {
            try
            {
                var complaintDetailsUrl = $"complaintDetails/{complaintId}";
                await DeleteData<string>(complaintDetailsUrl);
                return Response.Success.ToString();
            }
            catch
            {
                return Response.Failure.ToString();
            }
            
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



        public async Task<string> UpdateComplaintDetail(Guid complaintId, ComplaintDetailForUpdationData complaintDetailForCreationData)
        {
            try
            {
                var complaintDetailsUrl = $"complaintDetails/{complaintId}";
                await PutData<string>(complaintDetailsUrl, complaintDetailForCreationData);
                return Response.Success.ToString();
            }
            catch
            {
                return Response.Failure.ToString();
            }
        }
    }
}
