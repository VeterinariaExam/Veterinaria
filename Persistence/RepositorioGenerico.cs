using LiteDB;
using System;
using System.Collections.Generic;

public class RepositorioGenerico<T> where T : class
{
    private readonly string _nombreColeccion;
    private readonly string _nombreArchivoBD = "VeterinariaDB.db";

    public RepositorioGenerico(string nombreColeccion)
    {
        _nombreColeccion = nombreColeccion;
    }

    public void Insertar(T entidad)
    {
        using var db = new LiteDatabase(_nombreArchivoBD);
        var coleccion = db.GetCollection<T>(_nombreColeccion);
        coleccion.Insert(entidad);
    }

    public void Actualizar(T entidad)
    {
        using var db = new LiteDatabase(_nombreArchivoBD);
        var coleccion = db.GetCollection<T>(_nombreColeccion);
        coleccion.Update(entidad);
    }

    public void Eliminar(Func<T, bool> predicado)
{
    using var db = new LiteDatabase(_nombreArchivoBD);
    var coleccion = db.GetCollection<T>(_nombreColeccion);
    var items = coleccion.FindAll();
    foreach (var item in items)
    {
        if (predicado(item))
        {
            var idProperty = item.GetType().GetProperty("Id")?.GetValue(item);
            if (idProperty != null)
                coleccion.Delete(new BsonValue(idProperty));
        }
    }
}

    public List<T> ListarTodos()
    {
        using var db = new LiteDatabase(_nombreArchivoBD);
        var coleccion = db.GetCollection<T>(_nombreColeccion);
        return new List<T>(coleccion.FindAll());
    }

    public T ObtenerPorId(Guid id)
    {
        using var db = new LiteDatabase(_nombreArchivoBD);
        var coleccion = db.GetCollection<T>(_nombreColeccion);
        return coleccion.FindById(new BsonValue(id));
    }
}
