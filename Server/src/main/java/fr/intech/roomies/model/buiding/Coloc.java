package fr.intech.roomies.model.buiding;

import fr.intech.roomies.model.finance.Category;
import fr.intech.roomies.model.grocery.Item;
import fr.intech.roomies.model.people.Roomie;

import javax.persistence.*;
import java.util.Date;
import java.util.List;

@Entity
public class Coloc {

    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private int colocId;

    private String name;
    private String photo;
    private Date creationDate;
    private Roomie admin;

    @ElementCollection
    private List<Roomie> roomies;

    @ElementCollection
    private List<Item> savedItems;

    @ElementCollection
    private List<Category> categories;

    public Coloc() {
    }

    public int getColocId() {
        return colocId;
    }

    public void setColocId(int colocId) {
        this.colocId = colocId;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getPhoto() {
        return photo;
    }

    public void setPhoto(String photo) {
        this.photo = photo;
    }

    public Date getCreationDate() {
        return creationDate;
    }

    public void setCreationDate(Date creationDate) {
        this.creationDate = creationDate;
    }

    public Roomie getAdmin() {
        return admin;
    }

    public void setAdmin(Roomie admin) {
        this.admin = admin;
    }

    public List<Roomie> getRoomies() {
        return roomies;
    }

    public void setRoomies(List<Roomie> roomies) {
        this.roomies = roomies;
    }

    public List<Item> getSavedItems() {
        return savedItems;
    }

    public void setSavedItems(List<Item> savedItems) {
        this.savedItems = savedItems;
    }

    public List<Category> getCategories() {
        return categories;
    }

    public void setCategories(List<Category> categories) {
        this.categories = categories;
    }
}
