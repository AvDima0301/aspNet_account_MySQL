package com.example.example.users;

import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.GridLayoutManager;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.os.Bundle;
import com.example.example.R;

import java.util.ArrayList;
import java.util.List;

public class UsersActivity extends AppCompatActivity {

    private UserAdapter adapter;
    private RecyclerView recyclerView;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_users);

        recyclerView = findViewById(R.id.rcv);
        recyclerView.setHasFixedSize(true);
        recyclerView.setLayoutManager(new GridLayoutManager(this, 2,
                LinearLayoutManager.VERTICAL, false));
        List<UserDTO> userDTOS = new ArrayList<>();
        UserDTO userDTO = new UserDTO();
        userDTO.setEmail("ss@gg.dd");
        userDTO.setImage("/images/5xfqagws.mgd.jpeg");
        userDTOS.add(userDTO);

        UserDTO userDTO2 = new UserDTO();
        userDTO2.setEmail("dd@vv.dd");
        userDTO2.setImage("/images/bzj0kmx0.rig.jpeg");
        userDTOS.add(userDTO2);
        adapter=new UserAdapter(userDTOS);
        recyclerView.setAdapter(adapter);
    }
}