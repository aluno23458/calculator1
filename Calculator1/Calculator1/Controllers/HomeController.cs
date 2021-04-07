using Calculator1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Calculator1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private string display;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }



        // First call to the view
        [HttpGet] //first call to the view
        public IActionResult Index()
        {
            //to process the values and decide what to do with them
            ViewBag.Display = "0";
            ViewBag.ChoosenOperator = "S";
            ViewBag.Operator = "";
            ViewBag.PrimeiroOperando = "";
            ViewBag.Clear = "S";
            return View();
        }


        [HttpPost]
        public IActionResult Index(string button, string Display, string ChoosenOperator, string FirstOperator, string Operator, string Clear)
        {
            //process the value 'button' and decide what to do with it
            switch (button)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    //if we click a number, we are going to draq the number on the screen
                    if (Display == "0" || Clear == "S") { Display = button; }
                    else { Display += button; }
                    //i dont need to clear the display
                    Clear = "N";
                    break;
                case "+/-":
                    //invert the values shown in the display
                    if (Display.StartsWith('-')) { Display = Display.Substring(1); }
                    else { Display = "-" + Display; }
                    break;
                case ",":
                    //making the display double
                    if (!Display.Contains(',')) { Display += ","; }
                    break;
                case "/":
                case "x":
                case "-":
                case "+":
                case "=":
                    Clear = "S"; // Marking the display to being necessary to restart
                    if (ChoosenOperator != "S")
                    {
                        //the second time that a operator has been selected
                        //perform the operation with the previos Operator, and thevalues of the operands
                        double operator1 = Convert.ToDouble(FirstOperator);
                        double operator2 = Convert.ToDouble(Display);
                        //preform the arithmetic exercises
                        switch (Operator)
                        {
                            case "+":
                                Display = Convert.ToString(operator1 + operator2);
                                break;
                            case "-":
                                Display = Convert.ToString(operator1 - operator2);
                                break;
                            case "x":
                                Display = Convert.ToString(operator1 * operator2);
                                break;
                            case "/":
                                Display = Convert.ToString(operator1 / operator2);
                                break;
                        }
                    }
                    // store the current values for further more calculations
                    // showing the first operator
                    FirstOperator = Display;
                    // saving the value of the operator
                    Operator = button;
                    if (button == "=") { ChoosenOperator = "S"; }
                    else { ChoosenOperator = "N"; }
                    break;
                case "C":
                    Display = "0";
                    ChoosenOperator = "";
                    Operator = "";
                    FirstOperator = "";
                    Clear = "S";
                    break;
                    break;
            }
            //sending the value from the display to the view
            ViewBag.Display = Display;
            ViewBag.ChoosenOperator = ChoosenOperator;
            ViewBag.Operator = Operator;
            ViewBag.FirstOperator = FirstOperator;
            ViewBag.Clear = Clear;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
