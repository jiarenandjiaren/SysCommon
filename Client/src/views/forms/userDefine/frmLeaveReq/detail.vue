<template>
  <el-container style="border: 1px solid #eee">
    <el-main>

      <el-form label-width="80px">
        <el-form-item label="">
          <el-tag>这是一个表单详情展示样例，你可以完全根据客户的需求进行排版</el-tag>
        </el-form-item>
        <el-form-item label="请假类型">
          <el-radio-group v-model="temp.requestType">
            <el-radio label="病假"></el-radio>
            <el-radio label="事假"></el-radio>
          </el-radio-group>
        </el-form-item>
        <el-form-item :label="'请假人'" prop="userName">
          <el-input name="name" :disabled="true" v-model="temp.userName"></el-input>
        </el-form-item>

        <el-form-item label="开始时间">
          <el-col :span="11">
            <el-date-picker :disabled="true" type="date" placeholder="选择日期" v-model="temp.startDate" style="width: 100%;"></el-date-picker>
          </el-col>
          <el-col class="line" :span="2" style="text-align: center;">-</el-col>
          <el-col :span="11">
            <el-time-picker :disabled="true" type="fixed-time" placeholder="选择时间" v-model="temp.startTime" style="width: 100%;"></el-time-picker>
          </el-col>
        </el-form-item>

        <el-form-item label="结束时间">
          <el-col :span="11">
            <el-date-picker :disabled="true" type="date" placeholder="选择日期" v-model="temp.endDate" style="width: 100%;"></el-date-picker>
          </el-col>
          <el-col class="line" :span="2" style="text-align: center;">-</el-col>
          <el-col :span="11">
            <el-time-picker :disabled="true" type="fixed-time" placeholder="选择时间" v-model="temp.endTime" style="width: 100%;"></el-time-picker>
          </el-col>
        </el-form-item>

        <el-form-item size="small" :label="'请假说明'" prop="requestComment">
          <el-input type="textarea" :disabled="true" :rows="3" v-model="temp.requestComment"></el-input>
        </el-form-item>


        <el-carousel :interval="4000" type="card" height="150px">
          <el-carousel-item v-for="file in temp.files" :key="file.filePath">
            <a :href="baseURL +'/' + file.filePath" target="_blank"><img height="150" :src="baseURL +'/' + file.filePath" :title="file.fileName"/></a>
          </el-carousel-item>
        </el-carousel>

      </el-form>
    </el-main>
  </el-container>

</template>

<script>
  export default {
    name: 'frm-leave-req-detail',
    props: ['frmData'],
    data() {
      return {
        baseURL: process.env.BASE_IMG_URL // 附件的根目录
      }
    },
    computed: {
      temp: function() {
        return JSON.parse(this.frmData)
      }
    }
  }

</script>

<style rel="stylesheet/scss" lang="scss" scoped>
  @import "src/styles/mixin.scss";

  .createPost-container {
    position: relative;

    .createPost-main-container {
      margin: 10px;
      background-color: white;
      padding: 10px;
      padding-top: 0;

      .postInfo-container {
        position: relative;
        @include clearfix;
        margin-bottom: 10px;

        .postInfo-container-item {
          float: left;
        }
      }

      .editor-container {
        min-height: 500px;
        margin: 0 0 30px;

        .editor-upload-btn-container {
          text-align: right;
          margin-right: 10px;

          .editor-upload-btn {
            display: inline-block;
          }
        }
      }
    }

    .el-carousel__item h3 {
    color: #475669;
    font-size: 14px;
    opacity: 0.75;
    line-height: 200px;
    margin: 0;
  }
  
  .el-carousel__item:nth-child(2n) {
    background-color: #99a9bf;
  }
  
  .el-carousel__item:nth-child(2n+1) {
    background-color: #d3dce6;
  }

    .word-counter {
      width: 40px;
      position: absolute;
      right: -10px;
      top: 0px;
    }
  }

</style>
