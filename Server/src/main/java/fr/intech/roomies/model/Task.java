package fr.intech.roomies.model;

import javax.persistence.*;

@Entity
public class Task {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private int taskId;

    @Column(nullable = false)
    private String name;

    private String desc;

    @Column(nullable = false)
    private Boolean state;

    public Task() {    }

}
