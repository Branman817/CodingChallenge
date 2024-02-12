using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Api.Controllers;
using Api.Dtos;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Api.Repositories;
using Api.Services;
using Xunit;

namespace ApiTests.IntegrationTests;

// Have API running in a separate Visual Studio window to run tests
public class EmployeeIntegrationTests : IntegrationTest
{
    [Fact]
    public async Task WhenAskedForAllEmployees_ShouldReturnAllEmployees()
    {
        var response = await HttpClient.GetAsync("/api/v1/employees");

        var employees = new List<GetEmployeeDto>
        {
            new()
            {
                Id = 1,
                FirstName = "LeBron",
                LastName = "James",
                Salary = 75420.99m,
                DateOfBirth = new DateTime(1984, 12, 30)
            },
            new()
            {
                Id = 2,
                FirstName = "Ja",
                LastName = "Morant",
                Salary = 92365.22m,
                DateOfBirth = new DateTime(1999, 8, 10),
                Dependents = new List<GetDependentDto>
                {
                    new()
                    {
                        Id = 1,
                        FirstName = "Spouse",
                        LastName = "Morant",
                        Relationship = Relationship.Spouse,
                        DateOfBirth = new DateTime(1998, 3, 3)
                    },
                    new()
                    {
                        Id = 2,
                        FirstName = "Child1",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2020, 6, 23)
                    },
                    new()
                    {
                        Id = 3,
                        FirstName = "Child2",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2021, 5, 18)
                    }
                }
            },
            new()
            {
                Id = 3,
                FirstName = "Michael",
                LastName = "Jordan",
                Salary = 143211.12m,
                DateOfBirth = new DateTime(1963, 2, 17),
                Dependents = new List<GetDependentDto>
                {
                    new()
                    {
                        Id = 4,
                        FirstName = "DP",
                        LastName = "Jordan",
                        Relationship = Relationship.DomesticPartner,
                        DateOfBirth = new DateTime(1974, 1, 2)
                    }
                }
            }
        };

        
        await response.ShouldReturn(HttpStatusCode.OK, employees);
    }

    [Fact]
    public async Task WhenAskedForAnEmployee_ShouldReturnCorrectEmployee()
    {
        var response = await HttpClient.GetAsync("/api/v1/employees/1");
        var employee = new GetEmployeeDto
        {
            Id = 1,
            FirstName = "LeBron",
            LastName = "James",
            Salary = 75420.99m,
            DateOfBirth = new DateTime(1984, 12, 30)
        };
        await response.ShouldReturn(HttpStatusCode.OK, employee);
    }
    
    [Fact]
    public async Task WhenAskedForANonexistentEmployee_ShouldReturn404()
    {
        var response = await HttpClient.GetAsync($"/api/v1/employees/{int.MinValue}");
        await response.ShouldReturn(HttpStatusCode.NotFound);
    }

    // Add new test to test that paychecks are correctly calculated
    [Fact]
    public async Task CalculatePaycheckCorrectly_ForNoDependents()
    {
        var response = await HttpClient.GetAsync("/api/v1/employees/1/paycheck");

        var paycheck = new PaycheckDto
        {
            BaseValue = 2900.81m,
            Pay = 2439.27m,
            BenefitCosts = 461.54m
        };

        await response.ShouldReturn(HttpStatusCode.OK, paycheck);
    }

    [Fact]
    public async Task CalculatePaycheckCorrectly_WithDependents_AndOverEightyThousandSalary()
    {
        var response = await HttpClient.GetAsync("/api/v1/employees/2/paycheck");

        var paycheck = new PaycheckDto
        {
            BaseValue = 3552.51m,
            BenefitCosts = 1363.36m,
            Pay = 2189.15m
        };

        await response.ShouldReturn(HttpStatusCode.OK, paycheck);
    }

    [Fact]
    public async Task CalculatePaycheckCorrectly_WithDependentOverAgeFifty()
    {
        var response = await HttpClient.GetAsync("/api/v1/employees/3/paycheck");

        var paycheck = new PaycheckDto
        {
            BaseValue = 5508.12m,
            BenefitCosts = 940.93m,
            Pay = 4567.19m
        };

        await response.ShouldReturn(HttpStatusCode.OK, paycheck);
    }

    [Fact]
    public async Task WhenAskForANonExistentEmployeePaycheck_ShouldReturn404()
    {
        var response = await HttpClient.GetAsync($"/api/v1/employees/{int.MinValue}/paycheck");
        await response.ShouldReturn(HttpStatusCode.NotFound);
    }
}

