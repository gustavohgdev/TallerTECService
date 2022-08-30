using Newtonsoft.Json;
using TallerTECService.Models;

namespace TallerTECService.Data
{
    //Implementacion de la logica para cada una de los ActionResult en TallerController,
    //esta clase extiende la interfaz ILoginRepo, e implementa los metodos relacionados
    //a la manipulacion de datos necesaria para cumplir con los requerimientos funcionales
    //de la aplicacion.
    public class JsonTallerRepo : ITallerRepo
    {
        
        //Logica de autenticacion de usuarios. Hace uso de libreria NewtonSoft.Json para el manejo de la base de datos.
        //Recibe una instancia de LoginData creada con el mensaje entrante desde el cliente en el endpoint POST api/login
        //Retorna una instancia de AuthResponse. 
        public AuthResponse authCheck(LoginData userData)
        {
            AuthResponse validation = new AuthResponse();
            List<LoginData> loginData = new List<LoginData>();
            using (StreamReader r = new StreamReader("Data/usuarios.json"))
            {
                string json = r.ReadToEnd();
                loginData = JsonConvert.DeserializeObject<List<LoginData>>(json);
            }
            
            var user = loginData.AsQueryable().Where(e => e.nombreUsuario == userData.nombreUsuario).FirstOrDefault();

            if(user != null && user.contrasena == userData.contrasena && user.tipoUsuario == userData.tipoUsuario)
            {
            
                validation.authenticated = true;
            }
            else
            {
                validation.authenticated = false;
            }
            
            return validation;

        }
    }
}