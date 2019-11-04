package fr.intech.roomies.model.buiding;


import fr.intech.roomies.model.Task;
import fr.intech.roomies.model.finance.Category;
import fr.intech.roomies.model.finance.Transaction;
import fr.intech.roomies.model.grocery.GroceryList;
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

    @Column(nullable = false)
    private String colocName;

    @Column(nullable = false)
    private String photo;

    @Column(nullable = false)
    private Date creationDate;

    private int  adminId;

    @ElementCollection
    private List<Roomie> roomies;

    @ElementCollection
    private List<Category> categories;

    @ElementCollection
    private List<Task> tasks;

    @ElementCollection
    private List<Transaction> transactions;

    @ElementCollection
    private List<GroceryList> groceryLists;

    @ElementCollection
    private List<Item> savedItems;

    public Coloc() { }

    public int getColocId() {
        return colocId;
    }

    public void setColocId(int colocId) {
        this.colocId = colocId;
    }

    public String getColocName() {
        return colocName;
    }

    public void setColocName(String colocName) {
        this.colocName = colocName;
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

    public List<Roomie> getRoomies() {
        return roomies;
    }

    public void setRoomies(List<Roomie> roomies) {
        this.roomies = roomies;
    }

    public List<Category> getCategories() {
        return categories;
    }

    public void setCategories(List<Category> categories) {
        this.categories = categories;
    }

    public List<Task> getTasks() {
        return tasks;
    }

    public void setTasks(List<Task> tasks) {
        this.tasks = tasks;
    }

    public List<Transaction> getTransactions() {
        return transactions;
    }

    public void setTransactions(List<Transaction> transactions) {
        this.transactions = transactions;
    }

    public List<GroceryList> getGroceryLists() {
        return groceryLists;
    }

    public void setGroceryLists(List<GroceryList> groceryLists) {
        this.groceryLists = groceryLists;
    }

    public List<Item> getSavedItems() {
        return savedItems;
    }

    public void setSavedItems(List<Item> savedItems) {
        this.savedItems = savedItems;
    }
}
