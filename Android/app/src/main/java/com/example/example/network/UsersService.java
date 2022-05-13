package com.example.example.network;

import com.example.example.constants.Urls;

import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class UsersService {
    private static UsersService instance;
    private Retrofit retrofit;
    private String token;

    public UsersService() {
        retrofit = new Retrofit.Builder()
                .baseUrl(Urls.BASE)
                .addConverterFactory(GsonConverterFactory.create())
                .build();
    }

    public static UsersService getInstance() {
        if(instance==null)
            instance=new UsersService();
        return instance;
    }

    public void setToken(String t) {
        token = t;
    }

    public String getToken() {
        return token;
    }

    public boolean isTokenEmpty() {
        if(token == "" || token.isEmpty()) return true;
        else return false;
    }
    public UsersApi jsonApi() { return retrofit.create(UsersApi.class); }
}
