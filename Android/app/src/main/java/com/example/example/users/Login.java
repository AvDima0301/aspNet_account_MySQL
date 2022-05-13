package com.example.example.users;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.TextView;

import com.example.example.R;
import com.example.example.constants.TextInputHelper;
import com.example.example.constants.Validator;
import com.example.example.dto.AccountResponseDTO;
import com.example.example.dto.LoginDTO;
import com.example.example.network.UsersService;

import java.io.IOException;
import java.util.Properties;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class Login extends Activity {

    TextInputHelper email;
    TextInputHelper password;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
        email = new TextInputHelper(findViewById(R.id.email), findViewById(R.id.txtEmail));
        password = new TextInputHelper(findViewById(R.id.password), findViewById(R.id.txtPassword));

    }

    public void handleClick(View view) {
        boolean validData = true;

        if (!Validator.emailValidate(email.editText.getText().toString())) {
            email.layout.setError("Вкажіть пошту правильно");
            validData = false;
        } else email.layout.setError(null);


        String password = this.password.editText.getText().toString();

        if (password.isEmpty()) {
            this.password.layout.setError("Введіть пароль");
            validData = false;
        }  this.password.layout.setError(null);


        LoginDTO loginDTO = new LoginDTO();

        loginDTO.setEmail(email.editText.getText().toString());
        loginDTO.setPassword(password);

        UsersService.getInstance().jsonApi().login(loginDTO)
                .enqueue(new Callback<AccountResponseDTO>() {
                    @Override
                    public void onResponse(Call<AccountResponseDTO> call, Response<AccountResponseDTO> response) {
                        if(response.isSuccessful()) {
                            AccountResponseDTO data = response.body();
                            if(!data.getToken().isEmpty()) {
                                UsersService.getInstance().setToken(data.getToken());
                                Intent intent = new Intent(getApplicationContext(), UsersActivity.class);
                                startActivity(intent);
                            }
                        }
                        else {
                            try {
                                String json = response.errorBody().string();
                                int a=12;
                            }
                            catch(Exception ex) {

                            }
                        }
                    }

                    @Override
                    public void onFailure(Call<AccountResponseDTO> call, Throwable t) {
                        String str = t.toString();
                        int a = 12;
                    }
                });
    }

}
