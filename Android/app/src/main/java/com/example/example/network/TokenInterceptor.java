package com.example.example.network;

import com.example.example.application.HomeApplication;

import java.io.IOException;

import okhttp3.Interceptor;
import okhttp3.Request;
import okhttp3.Response;

public class TokenInterceptor implements Interceptor {
    @Override
    public Response intercept(Chain chain) throws IOException {
        TokenService jwtService = (TokenService) HomeApplication.getInstance();
        String token = jwtService.getToken();
        Request.Builder builder = chain.request().newBuilder();
        if(token!=null && !token.isEmpty())
        {
            builder.header("Authorization", "Bearer "+token);
        }
        return chain.proceed(builder.build());
    }
}
