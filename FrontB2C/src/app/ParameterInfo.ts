export class ParameterInfo {
   public isLocal:boolean = true; //Si la bandera esta activa se conectara al pathApis configurado para el backend del administrador
   public isProd:boolean  = false;
   public serve:string ="https://25.56.189.76:7575"; //NUBE
   //public pathApis:string = "http://192.168.194.130:9992"; //Url para pruebas locales API CONNECT
   //public pathApis:string = "http://192.168.146.1:8089"; //Url para pruebas locales API CONNECT

   public pathApis:string = "https://dp.apigateway.ibm/turesbalon/touresbalon";
 
 }
 