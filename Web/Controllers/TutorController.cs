using AutoMapper;
using Domain.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class TutorController:Controller
{
    private  ITutorService _tutorService;
    private readonly IMapper _mapper;

    public TutorController(ITutorService tutorService,IMapper mapper)
    {
        _tutorService = tutorService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _tutorService.GetTutors());
    }

    [HttpGet]
    public IActionResult Add()
    {
        var empty = new Tutor();
        return View(empty);
    }

    [HttpPost]
    public async Task<IActionResult> Add(Tutor model)
    {
        if (ModelState.IsValid == false)
        {
            return View(model);
        }
        await _tutorService.AddTutor(model);
        return RedirectToAction("Index");
    }


    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var depart = await _tutorService.GetTutorById(id);
         var mapped = _mapper.Map<Tutor>(depart.Data);
        return View(mapped);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Tutor model)
    {
        if(ModelState.IsValid == false)
        {
            return View(model);
        }
        var emp = await _tutorService.UpdateTutor(model);
        var mapped = _mapper.Map<Tutor>(emp.Data);
        return RedirectToAction(nameof(Index));
    }


     [HttpGet]
     public async Task <IActionResult> Delete(int id)
    {
      await _tutorService.DeleteTutor(id);
      return RedirectToAction(nameof(Index));
    }

}

