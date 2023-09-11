using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using UnityEditor.MemoryProfiler;
using UnityEngine.Video;
using TMPro;
using System;

public class DBfavorit : MonoBehaviour
{
    private string connectionString = "Server=127.0.0.1;Database=cinemadb;User ID=root;Password=1488;";
    private MySqlConnection connection;

   


    public Text btnFavoritText; // Ссылка на текст btnFavorit
    public int movieId; // ID фильма, который вы хотите добавить в избранное

    private void Start()
    {
        connection = new MySqlConnection(connectionString);
        connection.Open();
        // Назначаем обработчик события для нажатия на текст btnFavorit
        Button btnFavoritButton = btnFavoritText.GetComponent<Button>();

        if (btnFavoritButton != null)
        {
            btnFavoritButton.onClick.AddListener(AddToFavoritesOnClick);
        }
        else
        {
            Debug.LogError("Button component not found on btnFavoritText.");
        }
    }

    private void AddToFavoritesOnClick()
    {
        // Вызываем метод для добавления фильма в избранное с заданным movieId
        AddToFavorites(movieId);
    }

    // Ваш метод AddToFavorites
    public void AddToFavorites(int movieId)
    {
        string query = $"INSERT INTO favourites (movie_id, user_id, title) VALUES (@movieId, @userId, @title)";
        MySqlCommand command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("@movieId", movieId);
        command.Parameters.AddWithValue("@userId", UserInfo.user_id);
        command.Parameters.AddWithValue("@title", "Favorite Movie");

        try
        {
            command.ExecuteNonQuery();
            Debug.Log("Фильм добавлен в избранное.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Произошла ошибка при добавлении фильма в избранное: " + ex);
        }
    }

    private void OnDestroy()
    {
        connection.Close();
    }
}
