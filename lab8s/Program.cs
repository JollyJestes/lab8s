using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

[Serializable]
public class Movie
{
    public string Title { get; set; }
    public int Watched { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        var films = new List<Movie>();

        // Ввод фильмов с клавиатуры
        while (true)
        {
            Console.WriteLine("Введите название фильма (или 'q' для завершения):");
            string userInput = Console.ReadLine();

            if (userInput.ToLower() == "q")
                break;

            var film = new Movie { Title = userInput, Watched = GetRandomWatchedStatus() };
            films.Add(film);
        }

        // Создание объекта для сериализации/десериализации данных
        var dataSerializer = new DataSerializer<List<Movie>>();

        // Сохранение данных в бинарном файле
        dataSerializer.SerializeBinary(films, "movies.dat");

        // Восстановление данных из бинарного файла
        var restoredMoviesBinary = dataSerializer.DeserializeBinary("movies.dat");

        // Вывод восстановленных данных из бинарного файла
        Console.WriteLine("\nДанные после восстановления из бинарного файла:");
        PrintMovies(restoredMoviesBinary);

        // Сохранение данных в JSON-файле
        dataSerializer.SerializeJson(films, "movies.json");

        // Восстановление данных из JSON-файла
        var restoredMoviesJson = dataSerializer.DeserializeJson("movies.json");

        // Вывод восстановленных данных из JSON-файла
        Console.WriteLine("\nДанные после восстановления из JSON-файла:");
        PrintMovies(restoredMoviesJson);
    }

    static int GetRandomWatchedStatus()
    {
        // Генерация случайного числа 0 или 1 для статуса просмотра
        Random random = new Random();
        return random.Next(0, 2);
    }

    static void PrintMovies(IEnumerable<Movie> movies)
    {
        foreach (var movie in movies)
        {
            Console.WriteLine($"{movie.Title} - Смотрел: {(movie.Watched == 1 ? "Да" : "Нет")}");
        }
    }
}