﻿using AutoMapper;
using KioskManagement.Model.MappingModels;
using KioskManagement.Model.Models;
using KioskManagement.Model.ViewModels;
using KioskManagement.Service;
using KioskManagement.WebApi.Infrastructure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KioskManagement.WebApi.Controllers
{
    [Route("api/kioskmanagement/[controller]")]
    [ApiController]
    [Authorize]
    public class TEmployeeController : ControllerBase
    {
        #region Intialize
        private readonly ITEmployeeService _TEmployeeService;
        private readonly IMapper _mapper;
        private ILogger<TEmployeeController> _logger;

        public TEmployeeController(ITEmployeeService tEmployeeService, IMapper mapper, ILogger<TEmployeeController> logger)
        {
            _TEmployeeService = tEmployeeService;
            _mapper = mapper;
            _logger = logger;
        }
        #endregion Intialize

        #region Properties
        /// <summary>
        /// Danh sách employee phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet("getAllByPaging")]
        [Authorize(Roles = "ViewTEmployee")]
        public async Task<IActionResult> GetAllByPaging(int page = 0, int pageSize = 10, string? keyword = null)
        {
            try
            {
                _logger.LogInformation("Run endpoint {endpoint} {verb}", "/api/aioaccesscontrol/TEmployee/getAllByPaging", "GET");
                var result = await _TEmployeeService.GetAllByMapping(keyword);
                int totalRow = 0;
                totalRow = result.Count();
                var paging = result.OrderByDescending(x => x.EmId).Skip(page * pageSize).Take(pageSize);
                var mapping = _mapper.Map<IEnumerable<TEmployeeMapping>>(paging);
                var response = new PaginationSet<TEmployeeMapping>()
                {
                    Items = mapping,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Danh sách employee
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet("getAllByMaping")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllByMaping(string? keyword = null)
        {
            try
            {
                _logger.LogInformation("Run endpoint {endpoint} {verb}", "/api/aioaccesscontrol/TEmployee/getAllByMaping", "GET");
                var result = await _TEmployeeService.GetAllByMapping(keyword);
                var mapping = _mapper.Map<IEnumerable<TEmployeeMapping>>(result);
                return Ok(mapping);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("getAllNoParam")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllNoParam()
        {
            try
            {
                _logger.LogInformation("Run endpoint {endpoint} {verb}", "/api/aioaccesscontrol/TEmployee/getAllNoParam", "GET");
                var result = await _TEmployeeService.GetAllNoParam();
                var mapping = _mapper.Map<IEnumerable<TEmployee>, IEnumerable<TEmployeeMapping>>(result);
                return Ok(mapping);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("getAllEmployee")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllEmployee()
        {
            try
            {
                _logger.LogInformation("Run endpoint {endpoint} {verb}", "/api/aioaccesscontrol/TEmployee/getAllEmployee", "GET");
                var result = await _TEmployeeService.GetAllEmployee();
                var mapping = _mapper.Map<IEnumerable<TEmployee>, IEnumerable<TEmployeeMapping>>(result);
                return Ok(mapping);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("getById/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                _logger.LogInformation("Run endpoint {endpoint} {verb}", "/api/aioaccesscontrol/TEmployee/getById/{id}", "GET");
                var result = await _TEmployeeService.GetById(id);
                var mapping = _mapper.Map<TEmployeeViewModel>(result);
                return Ok(mapping);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Thêm mới nhân viên
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [Authorize(Roles = "CreateTEmployee")]
        public async Task<IActionResult> Create(TEmployeeViewModel employee)
        {
            _logger.LogInformation("Run endpoint {endpoint} {verb}", "/api/aioaccesscontrol/TEmployee/create", "POST");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TEmployee em = _mapper.Map<TEmployee>(employee);
                em.EditStatus = true;
                var result = await _TEmployeeService.Add(em);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Chỉnh sửa employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [Authorize(Roles = "UpdateTEmployee")]
        public async Task<IActionResult> Update(TEmployeeViewModel employee)
        {
            _logger.LogInformation("Run endpoint {endpoint} {verb}", "/api/aioaccesscontrol/TEmployee/update", "PUT");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TEmployee em = _mapper.Map<TEmployee>(employee);
                em.EditStatus = true;
                var result = await _TEmployeeService.Update(em);
                var mapping = _mapper.Map<TEmployeeViewModel>(result);
                return Ok(mapping);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="checkedList"></param>
        /// <returns></returns>
        [HttpDelete("lockmulti")]
        [Authorize(Roles = "LockTEmployee")]
        public async Task<IActionResult> DeleteMulti(string checkedList)
        {
            _logger.LogInformation("Run endpoint {endpoint} {verb}", "/api/aioaccesscontrol/TEmployee/deletemulti", "DELETE");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                try
                {
                    int countSuccess = 0;
                    int countError = 0;
                    List<int> result = new List<int>();
                    var listItem = JsonConvert.DeserializeObject<List<int>>(checkedList);
                    foreach (var item in listItem)
                    {
                        try
                        {
                            var em = await _TEmployeeService.GetById(item);
                            em.EmStatus = false;
                            em.EditStatus = true;
                            var emNew = await _TEmployeeService.Update(em);
                            countSuccess++;
                        }
                        catch (Exception)
                        {
                            countError++;
                        }
                    }
                    result.Add(countSuccess);
                    result.Add(countError);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet("getEmployeeType")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTypeEmployee()
        {
            try
            {
                var result = await _TEmployeeService.GetTypeEmployee();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("getAllEmployeeByDepartment")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllEmployeeByDepartment(string strDepId)
        {
            try
            {
                List<int> depIds = JsonConvert.DeserializeObject<List<int>>(strDepId);
                _logger.LogInformation("Run endpoint {endpoint} {verb}", "/api/aioaccesscontrol/TEmployee/getAllEmployeeByDepartment", "GET");
                var result = await _TEmployeeService.GetEmployeesByDepartment(depIds);
                var mapping = _mapper.Map<IEnumerable<EmployeeDepMapping>>(result);
                return Ok(mapping);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion
    }
}
