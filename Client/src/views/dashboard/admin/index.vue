<template>
  <div class="dashboard-editor-container">

    <panel-group></panel-group>

    <el-row :gutter="20">

      <el-col :span="24">
        <el-card class="box-card">
          <el-tag type="danger">一些说明</el-tag>
          <p></p>
          开源版演示地址：<a href="http://demo.openauth.me:1802">http://demo.openauth.me:1802</a>
          <p></p>
          官方网址：<a href="http://openauth.me">http://openauth.me</a>
          <p></p>
          官方博客：<a href="http://www.cnblogs.com/yubaolee/">http://www.cnblogs.com/yubaolee/</a>
          <p></p>
          系统默认System账号登录，可以查看所有权限，如果用其他账号（如：admin/test）可以查看相应的授权情况。 数据库密码明文存储，不要问为什么不加密，因为你要问这些账号的密码我也记不住啊O(∩_∩)O
          <p></p>
          郑重提示：为了大家体验的一致性，数据库每5分钟还原一次。
        </el-card>
      </el-col>
      <el-col :span="8">
        <div class="product standard">
          <div class="title">
            <h2> 标准版</h2>
          </div>
          <div class="content">
            <h2>500元</h2>
            <div class="tips">
              <div><i class="el-icon-check"></i>专属QQ群</div>
              <div><i class="el-icon-check"></i>内部文档</div>
              <div><i class="el-icon-check"></i>社区VIP1标识</div>
              <div><i class="el-icon-check"></i>专属授权文件</div>
              <div><i class="el-icon-check"></i>OpenAuth.Pro源码【不支持升级】</div>
            </div>
          </div>
          <div class="action"><a class="btn std" target="_blank" href="http://openauth.me/question/detail.html?id=a2be2d61-7fcb-4df8-8be2-9f296c22a89c">选择标准版</a></div>
        </div>
      </el-col>
      <el-col :span="8">
        <div class="product prime">
          <div class="title">
            <h2> 高级版【荐】</h2>
          </div>
          <div class="content">
            <h2>800元</h2>
            <div class="tips">
              <div><i class="el-icon-check"></i>专属QQ群</div>
              <div><i class="el-icon-check"></i>内部文档</div>
              <div><i class="el-icon-check"></i>社区VIP2标识</div>
              <div><i class="el-icon-check"></i>专属授权文件</div>
              <div class="color-prime"><i class="el-icon-check"></i>OpenAuth.Pro源码</div>
              <div class="color-prime"><i class="el-icon-check"></i>最新的代码生成工具</div>
              <div class="color-prime"><i class="el-icon-check"></i>企业版专属文档</div>
              <div class="color-prime"><i class="el-icon-check"></i>永久免费升级</div>
              <div class="color-prime"><i class="el-icon-check"></i>提供项目管理平台账号</div>
              <div class="color-prime"><i class="el-icon-check"></i>为个人提供技术咨询服务</div>
            </div>
          </div>
          <div class="action"><a class="btn premier" target="_blank" href="http://openauth.me/question/detail.html?id=a2be2d61-7fcb-4df8-8be2-9f296c22a89c">选择高级版</a></div>
        </div>
      </el-col>
      <el-col :span="8">
        <div class="product enterprise">
          <div class="title">
            <h2>企业版</h2>
          </div>
          <div class="content">
            <h2>2000元</h2>
            <div class="tips">
              <div><i class="el-icon-check"></i>专属QQ群</div>
              <div><i class="el-icon-check"></i>内部文档</div>
              <div><i class="el-icon-check"></i>社区VIP2标识</div>
              <div><i class="el-icon-check"></i>专属授权文件</div>
              <div class="color-prime"><i class="el-icon-check"></i>OpenAuth.Pro源码</div>
              <div class="color-prime"><i class="el-icon-check"></i>最新的代码生成工具</div>
              <div class="color-prime"><i class="el-icon-check"></i>企业版专属文档</div>
              <div class="color-prime"><i class="el-icon-check"></i>永久免费升级</div>
              <div class="color-prime"><i class="el-icon-check"></i>提供项目管理平台账号</div>
               <div class="color-enterprise"><i class="el-icon-check"></i>为公司开发团队提供技术咨询服务</div>
              <div class="color-enterprise"><i class="el-icon-check"></i>拥有独立的知识产权</div>
              <div class="color-enterprise"><i class="el-icon-check"></i>远程技术支持</div>
              <div class="color-enterprise"><i class="el-icon-check"></i>无任何分发限制</div>
              <div class="color-enterprise"><i class="el-icon-check"></i>提供正规发票</div>
            </div>
          </div>
          <div class="action"><a class="btn enterprise" target="_blank" href="http://openauth.me/question/detail.html?id=a2be2d61-7fcb-4df8-8be2-9f296c22a89c">选择企业版</a></div>
        </div>
      </el-col>
    </el-row>
  </div>
</template>


<script>
  import PanelGroup from './components/PanelGroup'

  export default {
    name: 'dashboard-admin',
    components: {
      PanelGroup
    }
  }

</script>

<style rel="stylesheet/scss" lang="scss" scoped>
  .dashboard-editor-container {
    padding: 20px;
    background-color: rgb(240, 242, 245);
    min-height: calc(100vh - 84px);

    .chart-wrapper {
      background: #fff;
      padding: 16px 16px 0;
      margin-bottom: 32px;
    }

    .component-item {
      min-height: 100px;
    }
  }

  .product {
    max-width: 350px;
    border: 1px solid #dfdfdf;
    padding: 0;
    border-radius: 8px;
    margin-bottom: 20px;
    background-color: white;

    .title {
      padding: 12px;
      text-align: center;
      background-color: #f4f4f4;
      border-radius: 8px 8px 0 0;
    }

    h2 {
      margin: 24px 0 12px;
    }

    .content {
      h2 {
        text-align: center;
        font-size: 30px;
      }

      .tips {
        padding: 0 35px 30px 30px;

        div {
          padding: 8px 0;
          font-size: 15px;

          .el-icon-check {
            width: 30px;
            font-size: 1.5em;
            color: #35affb;
            vertical-align: middle;
          }
        }

        .color-prime {
          color: #338ed6;
          font-weight: 650;

          .el-icon-check {
            color: gray;
          }
        }

        .color-enterprise {
          color: #f56c6c;
          font-weight: 800;

          .el-icon-check {
            color: #67c23a;
          }
        }
      }
    }
  }

  .action {
    text-align: center;
    padding-bottom: 24px;
    padding: 12px 18px;
    display: flex;
    background: #f4f4f4;

    .btn {
      flex: 1;
      margin: 6px;
      padding: 6px 12px;
      line-height: 26px;
      font-size: 16px;
      background: #fff;
      position: relative;
      cursor: pointer;
      display: inline-block;
      white-space: nowrap;
      border-radius: 4px;
      -ms-touch-action: manipulation;
      touch-action: manipulation;
      font-weight: 700;
      text-decoration: none;
      border: 2px solid #4893ff;
      background-color: transparent;
      color: #4893ff;
      transition: all 0.3s;
    }

    a.btn.std {
      color: #0dafff;
      border-color: #0dafff;
    }

    a.btn.premier {
      color: #d67735;
      border-color: #ffa669;
    }
  }

  .standard .title {
    background: #eaeaea;
    color: #666;
  }

  .prime {
    margin: 0 auto;
  }

  .prime .title {
    background: linear-gradient(#338ed6, #4f6eab);
    color: #fff;
  }

  .enterprise {
    margin-left: auto;
    margin-right: 0;
  }

  .enterprise .title {
    background: linear-gradient(#807979, #484a4e);
    color: #fff;
  }

</style>
