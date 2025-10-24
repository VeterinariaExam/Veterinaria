using LiteDB;
using System;
using System.Collections.Generic;

// Repositorio genérico para operaciones básicas CRUD usando LiteDB
public class RepositorioGenerico<T> where T : class
{
    private readonly string _nombreColeccion;       // Nombre de la colección en la base de datos
    private readonly string _nombreArchivoBD = "VeterinariaDB.db"; // Archivo físico de la base de datos

    // Constructor que recibe el nombre de la colección para trabajar
    public RepositorioGenerico(string nombreColeccion)
    {
        _nombreColeccion = nombreColeccion;
    }

    // Inserta una nueva entidad en la colección correspondiente
    public void Insertar(T entidad)
    {
        using var db = new LiteDatabase(_nombreArchivoBD);
        var coleccion = db.GetCollection<T>(_nombreColeccion);
        coleccion.Insert(entidad);
    }

    // Actualiza una entidad existente en la base de datos
    public void Actualizar(T entidad)
    {
        using var db = new LiteDatabase(_nombreArchivoBD);
        var coleccion = db.GetCollection<T>(_nombreColeccion);
        coleccion.Update(entidad);
    }

    // Elimina entidades que cumplen con un predicado, usando reflexión para obtener la propiedad "Id"
    public void Eliminar(Func<T, bool> predicado)
    {
        using var db = new LiteDatabase(_nombreArchivoBD);
        var coleccion = db.GetCollection<T>(_nombreColeccion);
        var items = coleccion.FindAll();
        foreach (var item in items)
        {
            if (predicado(item))
            {
                // Obtiene el valor del Id dinámicamente
                var idProperty = item.GetType().GetProperty("Id")?.GetValue(item);
                if (idProperty != null)
                    coleccion.Delete(new BsonValue(idProperty)); // Borra por Id
            }
        }
    }

    // Lista todos los elementos de la colección
    public List<T> ListarTodos()
    {
        using var db = new LiteDatabase(_nombreArchivoBD);
        var coleccion = db.GetCollection<T>(_nombreColeccion);
        return new List<T>(coleccion.FindAll());
    }

    // Obtiene una entidad por su Id (GUID)
    public T ObtenerPorId(Guid id)
    {
        using var db = new LiteDatabase(_nombreArchivoBD);
        var coleccion = db.GetCollection<T>(_nombreColeccion);
        return coleccion.FindById(new BsonValue(id));
    }
}
