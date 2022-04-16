package com.example.example.network;

import com.example.example.constants.Urls;

import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class UsersService {
    private static UsersService instance;
    private Retrofit retrofit;

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
    public UsersApi jsonApi() { return retrofit.create(UsersApi.class); }
}
