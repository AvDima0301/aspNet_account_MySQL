package com.example.example.network;

import com.example.example.dto.AccountResponseDTO;
import com.example.example.dto.LoginDTO;
import com.example.example.dto.RegisterDTO;
import com.example.example.dto.UserDTO;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.Header;
import retrofit2.http.POST;

public interface UsersApi {
    @POST("/api/account/register")
    public Call<AccountResponseDTO> register(@Body RegisterDTO model);
    @GET("/api/account/users")
    public Call<List<UserDTO>> users();
    @POST("/api/account/login")
    public Call<AccountResponseDTO> login(@Body LoginDTO model);
}
