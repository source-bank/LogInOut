using LogInOut.Inc;
using LogInOut.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LogInOut.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

		//DB작업을 위해
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

        //로그인
        public IActionResult LogIn()
        {
            return View();
        }

        //로그인 처리
        [HttpPost]
        public IActionResult LoginCheckOk(string userId, string userPwd)
        {
            DAL dal = new DAL(_dbConnContext);
            var loginDiv = dal.LoginCheck(userId, userPwd);

            if (loginDiv == null)
            {
                TempData["UserNull"] = "등록된 회원 정보가 없습니다.";//아이디 및 비밀번호 입력 화면에 보여줌

                return RedirectToAction("LogIn", "Home");
            }
            else
            {
                //세션 할당
                HttpContext.Session.SetString("UserId", loginDiv.UserId.ToString());
                HttpContext.Session.SetString("UserName", loginDiv.UserName.ToString());

                return RedirectToAction("Index", "MyInfo");//로그인을 해야만 접근할 수 있는 메뉴
            }
        }

        //로그아웃 처리
        public IActionResult LogOutOk()
        {
            // 세션 정보 삭제
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("UserName");

            // 홈...으로 이동
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
