package fr.intech.roomies.model.buiding;

import javax.persistence.*;
import java.util.List;

@Entity
public class Building {

    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private int buildingId;

    @ElementCollection
    private List<Coloc> colocs;

    public Building() {    }

    public int getBuildingId() {
        return buildingId;
    }

    public void setBuildingId(int buildingId) {
        this.buildingId = buildingId;
    }

    public List<Coloc> getColocs() {
        return colocs;
    }

    public void setColocs(List<Coloc> colocs) {
        this.colocs = colocs;
    }
}
