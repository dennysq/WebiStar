using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using asp_net_core;
using asp_net_core.Models;
using SendGrid.Helpers.Mail;
using SendGrid;
using Microsoft.Extensions.Configuration;
using TimeZoneConverter;
using System.Reflection;

namespace asp_net_core.Controllers
{
    public class WebinarMeetingController : Controller
    {
        private readonly EscuelaContext _context;
        private readonly IConfiguration _configuration;
        public WebinarMeetingController(EscuelaContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: WebinarMeeting
        public async Task<IActionResult> Index()
        {
            var escuelaContext = _context.WebinarMeeting.Include(w => w.User);
            return View(await escuelaContext.ToListAsync());
        }

        // GET: WebinarMeeting/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webinarMeeting = await _context.WebinarMeeting
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (webinarMeeting == null)
            {
                return NotFound();
            }

            return View(webinarMeeting);
        }

        // GET: WebinarMeeting/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.User, "Id", "FirstName");
            ViewData["TimeZoneId"] = new SelectList(generateTimeZones(), "Id", "Name", TZConvert.GetTimeZoneInfo(TimeZoneInfo.Local.Id).Id);
            return View();
        }

        // POST: WebinarMeeting/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StartDate,Duration,Password,HostVideoEnabled,ParticipantVideoEnabled,MaxParticipants,Description,Name,BannerUrl,UserId,Price,Id,Modified")] WebinarMeeting webinarMeeting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(webinarMeeting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "FirstName", webinarMeeting.UserId);
            return View(webinarMeeting);
        }

        // GET: WebinarMeeting/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webinarMeeting = await _context.WebinarMeeting.FindAsync(id);
            if (webinarMeeting == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "FirstName", webinarMeeting.UserId);
            return View(webinarMeeting);
        }

        // POST: WebinarMeeting/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StartDate,Duration,Password,HostVideoEnabled,ParticipantVideoEnabled,MaxParticipants,Description,Name,BannerUrl,UserId,Price,Id,Modified")] WebinarMeeting webinarMeeting)
        {
            if (id != webinarMeeting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(webinarMeeting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WebinarMeetingExists(webinarMeeting.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "FirstName", webinarMeeting.UserId);
            return View(webinarMeeting);
        }

        // GET: WebinarMeeting/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webinarMeeting = await _context.WebinarMeeting
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (webinarMeeting == null)
            {
                return NotFound();
            }

            return View(webinarMeeting);
        }

        // POST: WebinarMeeting/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var webinarMeeting = await _context.WebinarMeeting.FindAsync(id);
            _context.WebinarMeeting.Remove(webinarMeeting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebinarMeetingExists(int id)
        {
            return _context.WebinarMeeting.Any(e => e.Id == id);
        }
        public async Task<IActionResult> PostMessage()
        {
            var apiKey = _configuration.GetSection("SENDGRID_API_KEY").Value;
            Console.WriteLine(apiKey);
            var client = new SendGridClient(apiKey);

            var from = new EmailAddress("invitation@webistar.com", "Webistar Invitation");
            List<EmailAddress> tos = new List<EmailAddress>
            {
              new EmailAddress("dennysaurio@gmail.com", "Dennys"),
              new EmailAddress("dquiroz@temenos.com", "Dennys"),
              new EmailAddress("shanelaflaca@gmail.com", "Shanela"),
              new EmailAddress("moisqume@gmail.com","Mónica")
            };

            var subject = "Hello world email from Sendgrid ";
            var assembly = this.GetType().Assembly;
            var resourceStream = assembly.GetManifestResourceStream("asp_net_core.Data.email-inlined.html");
            var htmlContent = "";
            var listR = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            foreach (var tz in listR)
            {
                Console.WriteLine(tz);
            }
            var builder = new StringBuilder();
            using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
            {
               builder.Append( await reader.ReadToEndAsync());
            }

            // var displayRecipients = false; // set this to true if you want recipients to see each others mail id 
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, "plain text", htmlContent, false);
            var response = await client.SendEmailAsync(msg);
            //   Console.WriteLine("response body="+response.Body.;
            Console.WriteLine(response.StatusCode);
            var escuelaContext = _context.WebinarMeeting.Include(w => w.User);
            return View("Index", await escuelaContext.ToListAsync());
        }
        public async Task PostMessage(WebinarMeeting webinar, EmailAddress email,string link)
        {
            var apiKey = _configuration.GetSection("SENDGRID_API_KEY").Value;
            Console.WriteLine(apiKey);
            var client = new SendGridClient(apiKey);

            var from = new EmailAddress("invitation@webistar.com", "Webistar Invitation");
            List<EmailAddress> tos = new List<EmailAddress>
            {
              new EmailAddress("dennysaurio@gmail.com", "Dennys"),
              new EmailAddress("dquiroz@temenos.com", "Dennys"),
              new EmailAddress("shanelaflaca@gmail.com", "Shanela"),
              new EmailAddress("moisqume@gmail.com","Mónica")
            };

            var subject = "Hello world email from Sendgrid ";
            var assembly = this.GetType().Assembly;
            var resourceStream = assembly.GetManifestResourceStream("asp_net_core.Data.email-inlined.html");
            var htmlContent = "";
            var listR = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            foreach (var tz in listR)
            {
                Console.WriteLine(tz);
            }
            var builder = new StringBuilder();
            using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
            {
               builder.Append( await reader.ReadToEndAsync());
            }
    
builder.Replace("{{WebinarName}}", webinar.Name);
builder.Replace("{{Link}}", link);
builder.Replace("{{Name}}", email.Name);
htmlContent=builder.ToString();

            // var displayRecipients = false; // set this to true if you want recipients to see each others mail id 
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, "plain text", htmlContent, false);
            var response = await client.SendEmailAsync(msg);
            //   Console.WriteLine("response body="+response.Body.;
            Console.WriteLine(response.StatusCode);
            var escuelaContext = _context.WebinarMeeting.Include(w => w.User);
         //   return View("Index", await escuelaContext.ToListAsync());
        }
        private List<MTimeZone> generateTimeZones()
        {
            var list = TZConvert.KnownWindowsTimeZoneIds;
            var result = new List<MTimeZone>();
            foreach (var tz in list)
            {
                var timezone = TZConvert.GetTimeZoneInfo(tz);
                TimeSpan offset = timezone.GetUtcOffset(DateTime.UtcNow);
                result.Add(new MTimeZone()
                {

                    Name = timezone.DisplayName,

                    TimeZoneId = timezone.Id,
                    UtcOffset = offset.Hours.ToString(),
                    Dst = timezone.SupportsDaylightSavingTime
                });
            }

            return result;
        }
    }
}
