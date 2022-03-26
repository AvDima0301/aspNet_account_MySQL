package com.example.example;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.TextView;

public class MainActivity extends AppCompatActivity {

    private TextView textHead;
    private EditText editTextData;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        textHead = findViewById(R.id.textHead);
        editTextData = findViewById(R.id.editTextData);
    }

    public void handleClick(View view) {
        String text = editTextData.getText().toString();
        textHead.setText(text);
    }
}