package fr.intech.roomies.model.buiding;

import fr.intech.roomies.model.finance.Category;
import fr.intech.roomies.model.finance.Spending;
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

    private int adminId;

    @ElementCollection
    @OneToMany
    private List<Roomie> roomies;

    @ElementCollection
    private List<GroceryList> groceryLists;

    @ElementCollection
    @OneToMany
    private List<Item> savedItems;

    @ElementCollection
    @OneToMany
    private List<Category> categories;

    @ElementCollection
    @OneToMany
    private List<Transaction> transactions;

    @ElementCollection
    @OneToMany
    private List<Spending> spendings;


    public Coloc() { }

}
