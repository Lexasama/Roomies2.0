package fr.intech.roomies.model;

import fr.intech.roomies.model.people.Roomie;

import javax.persistence.*;
import java.util.List;

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

    @Column(nullable = false)
    @ElementCollection
    @ManyToMany
    private List<Roomie> roomies;

    public Task() {    }

}
