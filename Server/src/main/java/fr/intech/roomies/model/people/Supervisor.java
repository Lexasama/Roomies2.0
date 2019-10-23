package fr.intech.roomies.model.people;

import fr.intech.roomies.model.buiding.Building;

import javax.persistence.*;
import java.util.Date;
import java.util.List;

@Entity
public class Supervisor {

    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private int userId;

    private String lastName;
    private String firstName;
    private Date birthDate;


    @ElementCollection
    private List<Building> buildings;

    public Supervisor() {
    }

    public int getUserId() {
        return userId;
    }

    public void setUserId(int userId) {
        this.userId = userId;
    }

    public String getLastName() {
        return lastName;
    }

    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    public String getFirstName() {
        return firstName;
    }

    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    public Date getBirthDate() {
        return birthDate;
    }

    public void setBirthDate(Date birthDate) {
        this.birthDate = birthDate;
    }

    public List<Building> getBuildings() {
        return buildings;
    }

    public void setBuildings(List<Building> buildings) {
        this.buildings = buildings;
    }
}
