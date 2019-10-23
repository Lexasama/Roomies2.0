package fr.intech.roomies.model;

import fr.intech.roomies.model.people.Roomie;

import javax.persistence.*;
import java.util.List;

@Entity
public class Task {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private int id;

    @Column(nullable = false)
    private String name;

    private String desc;

    @Column(nullable = false)
    private Boolean state;

    @Column(nullable = false)
    @ElementCollection
    private List<Roomie> roomies;

    public Task() {    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getDesc() {
        return desc;
    }

    public void setDesc(String desc) {
        this.desc = desc;
    }

    public Boolean getState() {
        return state;
    }

    public void setState(Boolean state) {
        this.state = state;
    }

    public List<Roomie> getRoomies() {
        return roomies;
    }

    public void setRoomies(List<Roomie> roomies) {
        this.roomies = roomies;
    }
}
