<template>
  <div>
    <div>
      <template>
        <el-table :data="itemList" style="width: 100%">
          <el-table-column prop="itemName" label="Name" width="180"></el-table-column>
          <el-table-column prop="itemPrice" label="Price" width="180"></el-table-column>
          <el-table-column label="Options">
            <template slot-scope="scope">
              <el-button size="mini" @click="handleEdit(scope.$index, scope.row)">Edit</el-button>
              <el-button
                size="mini"
                type="danger"
                @click="handleDelete(scope.$index, scope.row)"
              >Delete</el-button>
            </template>
          </el-table-column>
        </el-table>
      </template>
    </div>
  </div>
</template>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      itemList: [
        { itemId: 1, itemName: "ItemName", itemPrice: "100" },
        { itemId: 1, itemName: "ItemName2", itemPrice: "100" }
      ]
    };
  }, //end data
  props: {
    id: {
      type: Number,
      required: true
    }
  }, //end props
  mounted() {}, //end mounted
  methods: {
    handleEdit(index, row) {
      console.log(index, row);
    },
    handleDelete(index, row) {
      console.log(index, row);
    }
  } //end methods
};
</script>

<style>
</style>