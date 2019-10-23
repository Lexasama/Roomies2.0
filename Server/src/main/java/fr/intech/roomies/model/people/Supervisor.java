package fr.intech.roomies.model.people;

import fr.intech.roomies.model.buiding.Building;
import org.hibernate.annotations.Type;

import javax.persistence.*;
import java.util.Date;
import java.util.List;

@Entity
@Table(uniqueConstraints = @UniqueConstraint(
        columnNames = {"userName" }))
public class Supervisor extends User{

    @Column(nullable = false)
    private String userName;

    @Column(nullable = false, length = 25)
    private String lastName;

    @Column(nullable = false, length = 25)
    private String firstName;

    @Column(nullable = false)
    @Type(type = "date")
    private Date birthDate;


    @ElementCollection
    private List<Building> buildings;

    public Supervisor() {
    }

    public String getUserName() {
        return userName;
    }

    public void setUserName(String userName) {
        this.userName = userName;
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
