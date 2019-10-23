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

    @Column(nullable = false)
    private String colocName;

    @Column(nullable = false)
    private String photo;

    @Column(nullable = false)
    private Date creationDate;

    private int adminId;

    @ElementCollection
    private List<Roomie> roomies;

    @ElementCollection
    private List<Item> savedItems;

    @ElementCollection
    private List<Category> categories;

    public Coloc() { }

}
