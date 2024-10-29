using LogInOut.Inc;
using LogInOut.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LogInOut.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

		//DB�۾��� ����
        private readonly DBConnContext _dbConnContext;
		public HomeController(ILogger<HomeController> logger, DBConnContext dbConnContext)
        {
            _logger = logger;
            _dbConnContext = dbConnContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //�α���
        public IActionResult LogIn()
        {
            return View();
        }

        //�α��� ó��
        [HttpPost]
        public IActionResult LoginCheckOk(string userId, string userPwd)
        {
            DAL dal = new DAL(_dbConnContext);
            var loginDiv = dal.LoginCheck(userId, userPwd);

            if (loginDiv == null)
            {
                TempData["UserNull"] = "��ϵ� ȸ�� ������ �����ϴ�.";//���̵� �� ��й�ȣ �Է� ȭ�鿡 ������

                return RedirectToAction("LogIn", "Home");
            }
            else
            {
                //���� �Ҵ�
                HttpContext.Session.SetString("UserId", loginDiv.UserId.ToString());
                HttpContext.Session.SetString("UserName", loginDiv.UserName.ToString());

                return RedirectToAction("Index", "MyInfo");//�α����� �ؾ߸� ������ �� �ִ� �޴�
            }
        }

        //�α׾ƿ� ó��
        public IActionResult LogOutOk()
        {
            // ���� ���� ����
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("UserName");

            // Ȩ...���� �̵�
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
