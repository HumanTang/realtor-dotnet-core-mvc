using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using realtor_dotnet_core_mvc.Models.Property;
namespace realtor_dotnet_core_mvc.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IConfiguration _configuration;

        public PropertyController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: PropertyController
        public ActionResult Index()
        {
            PropertyViewModel propertyViewModel = GetPropertyData(1); // Assuming you want to display property with ID 1
            return View(propertyViewModel);
            
        }

        private PropertyViewModel GetPropertyData(int propertyId)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Query property data with related land_type and property_type
                string propertyQuery = @"
                    SELECT p.*, lt.land_type, pt.property_type
                    FROM property p
                    INNER JOIN land_type_lookup lt ON p.land_type_id = lt.id
                    INNER JOIN property_type_lookup pt ON p.property_type_id = pt.id
                    WHERE p.id = @propertyId;
                "
                ;

                using (MySqlCommand command = new MySqlCommand(propertyQuery, connection))
                {
                    command.Parameters.AddWithValue("@propertyId", propertyId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            PropertyViewModel propertyViewModel = new PropertyViewModel
                            {
                                Property = new PropertyLookUp
                                {
                                    Id = reader.GetInt32("id"),
                                    Url = reader.GetString("url"),
                                    Title = reader.GetString("title"),
                                    Price = reader.GetString("price"),
                                    Address = reader.GetString("address"),
                                    Transport = reader.GetString("transport"),
                                    ImagesUrl = reader.GetString("images_url"),
                                    ImagesFloorplan = reader.GetString("images_floorplan"),
                                    YearBuilt = reader.GetInt32("year_built"),
                                    BuildArea = reader.GetString("build_area"),
                                    LandArea = reader.GetString("land_area"),
                                    Floor = reader.GetString("floor"),
                                    LandTypeId = reader.GetInt32("land_type_id"),
                                    PropertyTypeId = reader.GetInt32("property_type_id"),
                                    Pid = reader.GetString("pid"),
                                    Room = reader.GetInt32("room")
                                },
                                LandType = new PropertyLandType
                                {
                                    Id = reader.GetInt32("land_type_id"),
                                    LandTypeName = reader.GetString("land_type")
                                },
                                PropertyType = new PropertyType
                                {
                                    Id = reader.GetInt32("property_type_id"),
                                    PropertyTypeName = reader.GetString("property_type")
                                }
                            };

                            // Query attachments data
                            propertyViewModel.Attachments = GetAttachments(propertyId);

                            return propertyViewModel;
                        }
                    }
                }
            }

            return null;
        }

        private List<Attachment> GetAttachments(int propertyId)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Query attachments data
                string attachmentQuery = "SELECT * FROM attachments WHERE propertyid = @propertyId;";

                using (MySqlCommand command = new MySqlCommand(attachmentQuery, connection))
                {
                    command.Parameters.AddWithValue("@propertyId", propertyId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        List<Attachment> attachments = new List<Attachment>();

                        while (reader.Read())
                        {
                            attachments.Add(new Attachment
                            {
                                AttachmentId = reader.GetInt32("id"),
                                PropertyId = reader.GetInt32("propertyid"),
                                ImageUrl = reader.GetString("image_url")
                            });
                        }

                        return attachments;
                    }
                }
            }
        }

        public ActionResult Upload()
        {
            return View("upload");
        }

        public ActionResult Detail()
        {
            var property = new Property
            {
                Id = 1,
                Url = "https://www.example.com",
                Title = "Sample Property",
                Price = "500000",
                Address = "123 Main St",
                Transport = "Nearby Station",
                ImagesUrl = "https://i.ytimg.com/vi/AStqbypAzNU/hqdefault.jpg,https://i.ytimg.com/vi/AStqbypAzNU/hqdefault.jpg",
                ImagesFloorplan = "https://example.com/floorplan.jpg",
                YearBuilt = "2022年01月",
                BuildArea = "100m²",
                LandArea = "200m²",
                Floor = "2階建て",
                LandType = "Residential",
                PropertyType = "Single Family Home",
                Pid = "456",
                Room = "3BR"
            };

            return View("detail", property);
        }

        // GET: PropertyController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PropertyController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PropertyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PropertyController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PropertyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PropertyController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PropertyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
