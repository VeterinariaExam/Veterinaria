# Proyecto Veterinaria ASP.NET Core

## Descripción
Este proyecto es una aplicación web para la gestión de una clínica veterinaria. Permite el registro y manejo de clientes, mascotas, veterinarios, vacunas y citas médicas, manteniendo la estructura completa de las relaciones entre estas entidades.

## Funcionalidades principales
- Registro de clientes y sus mascotas.
- Asignación de vacunas a las mascotas con fechas de aplicación y vencimiento.
- Creación y gestión de citas con diferentes veterinarios y fechas.
- Consulta avanzada para:
  - Historias clínicas completas (vacunas y citas) por mascota.
  - Veterinarios que atendieron más citas en un mes determinado.
  - Listado de mascotas por especie y edad.
  - Vacunas próximas a vencer.

## Tecnologías
- ASP.NET Core para el backend.
- LiteDB o base de datos similar para persistencia local.
- Swagger UI para documentación y pruebas de API.
- RESTful API para interacción con clientes.

## Cómo probar y verificar el proyecto
1. Inicia la aplicación y abre Swagger UI en: `http://localhost:<puerto>/swagger/index.html`.
2. Usa los endpoints para:
   - Registrar clientes, mascotas, veterinarios y vacunas.
   - Asignar vacunas a mascotas con POST.
   - Crear citas médicas asignadas a veterinarios y mascotas.
   - Consultar con GET los datos de mascotas incluyendo vacunas y citas, para verificar que la estructura se mantiene.
   - Consultar veterinarios con más citas en determinado mes.
3. Verifica que los datos guardados se recuperan correctamente sin perder la estructura de relaciones.
4. Consulta los resultados en Swagger UI visualizando los JSON o en consola si usas métodos de logging.

## Estructura clave
- Modelos: Cliente, Mascota, Vacuna, Veterinario, Cita.
- Servicios y controladores para cada entidad, con métodos CRUD.
- Relaciones anidadas: vacunas y citas dentro del historial clínico de cada mascota.

## Notas
- Es importante que el `veterinarioId` enviado en las citas exista previamente.
