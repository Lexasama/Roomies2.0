package fr.intech.roomies.model.people;

import fr.intech.roomies.model.buiding.Batiment;

import javax.persistence.ElementCollection;
import javax.persistence.Entity;
import javax.persistence.Id;
import java.util.Date;
import java.util.List;

@Entity
public class Superviser {

    @Id
    private int userId;

    private String lastName;
    private String firstName;
    private Date birthDate;

    @ElementCollection
    private List<Batiment> buildings;

    public Superviser() {
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

    public List<Batiment> getBuildings() {
        return buildings;
    }

    public void setBuildings(List<Batiment> buildings) {
        this.buildings = buildings;
    }
}
