// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/xatHub")
    .build();

connection.start()
    .then(() => console.log("Conexión establecida"))
    .catch(err => console.error("Error al conectar:", err));
