using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComplaintLoggingSystem.DataModels;

namespace ComplaintLoggingSystem.Services
{
    public class MockComplaintDetailsSystem : IComplaintDetailsSystem
    {
        public Task CreateComplaintDetail(ComplaintDetailForCreationData complaintDetailForCreationData)
        {
            throw new NotImplementedException();
        }

        public Task DeleteComplaintDetail(Guid complaintId)
        {
            throw new NotImplementedException();
        }

        public async Task<ComplaintCompleteDetailData> GetComplaintDetail(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ComplaintDetailsData>> GetComplaintDetails(string emailId)
        {
            var complaintDetails = new List<ComplaintDetailsData>
            {
                new ComplaintDetailsData()
                {
                    City="Pune",
                    ContactNumber="",
                    Id=Guid.NewGuid(),
                    LastModifiedDate=DateTime.UtcNow,
                    Title=""

                },
                new ComplaintDetailsData()
                {
                    City="Pune",
                    ContactNumber="",
                    Id=Guid.NewGuid(),
                    LastModifiedDate=DateTime.UtcNow,
                    Title=""
                }
            };

            return complaintDetails;
        }
      
        public Task UpdateComplaintDetail(Guid complaintId, ComplaintDetailForUpdationData complaintDetailForCreationData)
        {
            throw new NotImplementedException();
        }
    }
}
