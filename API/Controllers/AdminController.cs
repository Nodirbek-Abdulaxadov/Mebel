﻿using Asp.Versioning;
using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using DTOs.UserDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(Roles = "SuperAdmin")]
public class AdminController(IUserService userService)
    : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAdmin([FromBody] RegisterUserDto request)
    {
        try
        {
            await _userService.CreateAsync(request, UserRoles.Admin);
            return Ok();
        }
        catch (ArgumentNullException ex)
        {
            return NotFound(ex.Message);
        }
        catch (MarketException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("get-all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllAdmins()
    {
        try
        {
            var admins = await _userService.GetUsersAsync(UserRoles.Admin);
            return Ok(admins);
        }
        catch (ArgumentNullException ex)
        {
            return NotFound(ex.Message);
        }
        catch (MarketException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAdmin(string id)
    {
        try
        {
            await _userService.DeleteUserAsync(id);
            return Ok();
        }
        catch (ArgumentNullException ex)
        {
            return NotFound(ex.Message);
        }
        catch (MarketException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}