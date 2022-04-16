package com.example.example.network;

import com.example.example.dto.UserDTO;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.GET;

public interface UsersApi {
    @GET("/api/account/users")
    public Call<List<UserDTO>> users();
}
