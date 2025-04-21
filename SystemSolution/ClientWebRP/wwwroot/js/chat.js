const connection = new signalR.HubConnectionBuilder()
    .withUrl("/xatHub")
    .build();

connection.start()
    .then(() => console.log("Conexión establecida"))
    .catch(err => console.error("Error al conectar:", err));
