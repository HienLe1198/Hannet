﻿using KioskManagement.Data.Repositories;
using KioskManagement.Model.MappingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskManagement.Service
{
    public interface ISyncService
    {
        Task<IQueryable<AdditionalSyncMapping>> GetEmployeeEditStatusToAdditionalSync(string keyword);
    }

    public class SyncService : ISyncService
    {
        private readonly ISyncRepository _syncRepository;
        public SyncService(ISyncRepository syncRepository)
        {
            _syncRepository = syncRepository;
        }

        public async Task<IQueryable<AdditionalSyncMapping>> GetEmployeeEditStatusToAdditionalSync(string keyword)
        {
            return await _syncRepository.GetEmployeeEditStatusToAdditionalSync(keyword);
        }
    }
}
