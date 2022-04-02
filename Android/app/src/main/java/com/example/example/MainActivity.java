package com.example.example;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.TextView;

import com.google.android.material.textfield.TextInputEditText;
import com.google.android.material.textfield.TextInputLayout;

public class MainActivity extends AppCompatActivity {

    TextInputLayout textFieldEmail;
    TextInputEditText txtEmail;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        textFieldEmail = findViewById(R.id.textFieldEmail);
        txtEmail = findViewById(R.id.txtEmail);
    }

    public void handleClick(View view) {
        textFieldEmail.setError("");
        if( txtEmail.getText().toString().equals(""))
            textFieldEmail.setError("Вкажіть пошту");

    }
}